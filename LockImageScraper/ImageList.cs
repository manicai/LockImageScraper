using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Linq;

namespace LockImageScraper
{
    class ImageList
    {
        private readonly Lazy<string> imageDirectory = new Lazy<string>(GetImageDirectory);

        public IEnumerable<string> Images()
        {
            var path = this.ImageDirectory;
            var files = Directory.GetFiles(path);
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
                }

                yield return Path.GetFileName(file);
            }
        }

        public Image GetImage(string name)
        {
            var path = Path.Combine(ImageDirectory, name);
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

            using (var reader = new FileStream(path, FileMode.Open))
            {
                var signature = new byte[] { 0xFF, 0xD8, 0xFF };
                var buffer = new byte[signature.Length];
                reader.Read(buffer, 0, signature.Length);
                return signature.SequenceEqual(buffer);
            }
        }
    }
}
