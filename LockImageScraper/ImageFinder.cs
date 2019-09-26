using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Linq;

namespace LockImageScraper
{
    using System.Diagnostics;
    using System.Windows.Forms;

    class ImageFinder
    {
        struct ImageInfo
        {
            public string Key;

            public string Path;

            public bool IsLandscape;

            public bool IsPortrait;

            public Image LargeThumbnail;

            public Image SmallThumbnail;
        }

        private const int SmallSize = 100;

        private const int LargeSize = 200;

        private readonly Lazy<string> imageDirectory = new Lazy<string>(GetImageDirectory);

        private readonly Lazy<IList<ImageInfo>> images;

        private IList<ImageInfo> FindImages()
        {
            var path = this.ImageDirectory;
            var files = Directory.GetFiles(path);
            var imageList = new List<ImageInfo>();

            foreach (var file in files)
            {
                if (!IsJPEG(file))
                {
                    continue;
                }

                using (var image = this.GetImage(file))
                {
                    if (image == null || image.Height < 1000 || image.Width < 1000)
                    {
                        continue;
                    }

                    var info = new ImageInfo() {
                        Key = Path.GetFileName(file),
                        Path = file,
                        SmallThumbnail = ImageUtilities.ResizeImage(image, SmallSize, true),
                        LargeThumbnail = ImageUtilities.ResizeImage(image, LargeSize, true),
                        IsLandscape = (image.Width >= image.Height),
                        IsPortrait = (image.Height >= image.Width),
                    };
                    imageList.Add(info);
                }
            }

            return imageList;
        }

        public ImageFinder()
        {
            this.images = new Lazy<IList<ImageInfo>>(this.FindImages);
        }

        public IEnumerable<string> Images(bool includeLandscape, bool includePortrait)
        {
            foreach (var info in this.images.Value)
            {
                if (info.IsLandscape && includeLandscape)
                {
                    yield return info.Key;
                }
                else if (info.IsPortrait && includePortrait)
                {
                    yield return info.Key;
                }
            }
        }

        public ImageList AsLargeImageList()
        {
            return this.AsImageList(LargeSize, (info) => info.LargeThumbnail);
        }

        public ImageList AsSmallImageList()
        {
            return this.AsImageList(SmallSize, (info) => info.SmallThumbnail);
        }

        private ImageList AsImageList(int size, Func<ImageInfo, Image> thumbnailSelector)
        {
            var list = new ImageList
            {
                ImageSize = new Size(size, size),
                ColorDepth = ColorDepth.Depth32Bit
            };

            foreach (var info in this.images.Value)
            {
                var tn = thumbnailSelector(info);
                Console.WriteLine(tn.Size);
                list.Images.Add(info.Key, thumbnailSelector(info));
            }

            return list;
        }

        public Image GetImage(string name)
        {
            var path = Path.Combine(this.ImageDirectory, name);
            try
            {
                var image = Image.FromFile(path);
                return image;
            }
            catch 
            {
                return null;
            }
        }

        public void SaveImage(string currentImage, string outputPath)
        {
            var sourcePath = Path.Combine(this.ImageDirectory, currentImage);
            File.Copy(sourcePath, outputPath);
        }

        private string ImageDirectory => this.imageDirectory.Value;

        private static string GetImageDirectory()
        {
            var home = Environment.GetFolderPath(Environment.SpecialFolder.UserProfile);
            // AppData > Local > Packages > Microsoft.Windows.ContentDeliveryManager_cw5n1h2txyewy > LocalState > Assets
            var path = Path.Combine(home, "AppData", "Local", "Packages",
                "Microsoft.Windows.ContentDeliveryManager_cw5n1h2txyewy",
                "LocalState", "Assets");
            return path;
        }

        /// <summary>
        /// Return true if the path points to a JPEG file.
        /// </summary>
        /// <param name="path">Path of file to check.</param>
        /// <returns>True if the path corresponds to a JPEG file.</returns>
        private static bool IsJPEG(string path)
        {
            if (!File.Exists(path))
            {
                return false;
            }

            try
            {
                using (var reader = new FileStream(path, FileMode.Open))
                {
                    var signature = new byte[] { 0xFF, 0xD8, 0xFF };
                    var buffer = new byte[signature.Length];
                    reader.Read(buffer, 0, signature.Length);
                    return signature.SequenceEqual(buffer);
                }
            }
            catch (IOException)
            {
                Debug.Print("IOException on {0}", path);
                return false;
            }
        }
    }
}
