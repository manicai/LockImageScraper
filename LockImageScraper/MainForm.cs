using System;
using System.Linq;
using System.Windows.Forms;

namespace LockImageScraper
{
    using System.Drawing;
    using System.Drawing.Drawing2D;
    using System.Drawing.Imaging;

    public partial class MainForm : Form
    {
        private ImageList imageList;
        public MainForm()
        {
            this.InitializeComponent();
            this.imageList = new ImageList();
            this.GetImages();
        }

        void GetImages()
        {
            this.listBoxImages.Items.AddRange(this.imageList.Images().ToArray<string>());
        }

        private void ListBoxImagesSelectedIndexChanged(object sender, EventArgs e)
        {
            var index = this.listBoxImages.SelectedIndex;
            var item = (string)this.listBoxImages.Items[index];
            var image = this.imageList.GetImage(item);

            var scaled = ResizeImage(image, this.pictureBox.Width);
            this.pictureBox.Image = scaled;
        }

        /// <summary>
        /// Resize the image to the specified side and height.
        /// </summary>
        /// <param name="image">The image to resize.</param>
        /// <param name="side">The longest length of side to resize to.</param>
        /// <returns>The resized image.</returns>
        private static Bitmap ResizeImage(Image image, int side)
        {
            var height = side;
            var width = side;
            if (image.Width > image.Height)
            {
                height = (int)(image.Height * ((double)width / image.Width));
            }
            else
            {
                width = (int)(image.Width * ((double)height / image.Height));
            }

            var destRect = new Rectangle(0, 0, width, height);
            var destImage = new Bitmap(width, height);

            destImage.SetResolution(image.HorizontalResolution, 
                                    image.VerticalResolution);

            using (var graphics = Graphics.FromImage(destImage))
            {
                graphics.CompositingMode = CompositingMode.SourceCopy;
                graphics.CompositingQuality = CompositingQuality.HighQuality;
                graphics.InterpolationMode = InterpolationMode.HighQualityBicubic;
                graphics.SmoothingMode = SmoothingMode.HighQuality;
                graphics.PixelOffsetMode = PixelOffsetMode.HighQuality;

                using (var wrapMode = new ImageAttributes())
                {
                    wrapMode.SetWrapMode(WrapMode.TileFlipXY);
                    graphics.DrawImage(image, destRect, 0, 0, 
                        image.Width, image.Height, 
                        GraphicsUnit.Pixel, wrapMode);
                }
            }

            return destImage;
        }

        private void OnButtonSaveClicked(object sender, EventArgs e)
        {
            var currentImage = (string)this.listBoxImages.SelectedItem;

            using (var fileDialog = new SaveFileDialog())
            {
                fileDialog.AddExtension = true;
                fileDialog.OverwritePrompt = true;
                fileDialog.DefaultExt = ".jpg";
                var result = fileDialog.ShowDialog();
                if (result == DialogResult.OK)
                {
                    var filename = fileDialog.FileName;
                    this.imageList.SaveImage(currentImage, filename);
                }
            }
        }
    }
}
