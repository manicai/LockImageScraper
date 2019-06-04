using System;
using System.Linq;
using System.Windows.Forms;

namespace LockImageScraper
{
    public partial class MainForm : Form
    {
        private ImageList imageList;
        public MainForm()
        {
            this.InitializeComponent();
            this.imageList = new ImageList();
            this.GetImages();
        }

        #region Event Handlers
        private void ListBoxImagesSelectedIndexChanged(object sender, EventArgs e)
        {
            this.ShowSelectedImage();
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

        private void OnPictureBoxResized(object sender, EventArgs e)
        {
            this.ShowSelectedImage();
        }

        private void GetImages()
        {
            this.listBoxImages.Items.AddRange(this.imageList.Images().ToArray<string>());
        }
        #endregion

        private void ShowSelectedImage()
        {
            var index = this.listBoxImages.SelectedIndex;
            if (index == -1)
            {
                return;
            }

            var item = (string)this.listBoxImages.Items[index];
            var image = this.imageList.GetImage(item);

            var scaled = ImageUtilities.ResizeImage(image, this.pictureBox.Width);
            this.pictureBox.Image = scaled;
        }
    }
}
