using System;
using System.Collections.Generic;
using System.Drawing;

namespace LockImageScraper
{
    using System.IO;

    class ImageList
    {
        private readonly Lazy<string> imageDirectory = new Lazy<string>(GetImageDirectory);

        public IEnumerable<string> Images()
        {
            var path = this.ImageDirectory;
            var files = System.IO.Directory.GetFiles(path);
            foreach (var file in files)
            {
                if (!this.IsJPEG(file))
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
            var path = System.IO.Path.Combine(ImageDirectory, name);
            try
            {
                var image = Image.FromFile(path);
                return image;
            }
            catch (OutOfMemoryException)
            {
                System.Diagnostics.Debug.Print("Out of memory for " + name);
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

        private bool IsJPEG(string path)
        {
            using (var reader = new FileStream(path, FileMode.Open))
            {
                var expected = new byte[] { 0xFF, 0xD8, 0xFF };
                var buffer = new byte[expected.Length];
                reader.Read(buffer, 0, expected.Length);
                for (var i = 0; i < expected.Length; ++i)
                {
                    if (expected[i] != buffer[i])
                    {
                        return false;
                    }
                }

                return true;
            }
        }
    }
}
