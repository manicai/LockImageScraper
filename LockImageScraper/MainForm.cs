using System;
using System.Linq;
using System.Windows.Forms;

namespace LockImageScraper
{
    public partial class MainForm : Form
    {
        private readonly ImageFinder imageFinder;
        public MainForm()
        {
            this.InitializeComponent();
            this.imageFinder = new ImageFinder();
            this.GetImages();
        }

        #region Event Handlers
        private void OnButtonSaveClicked(object sender, EventArgs e)
        {
            if (this.listView.SelectedItems.Count == 0)
            {
                return;
            }

            var currentImage = this.listView.SelectedItems[0].Name;
            using (var fileDialog = new SaveFileDialog())
            {
                fileDialog.AddExtension = true;
                fileDialog.OverwritePrompt = true;
                fileDialog.DefaultExt = ".jpg";
                var result = fileDialog.ShowDialog();
                if (result == DialogResult.OK)
                {
                    var filename = fileDialog.FileName;
                    this.imageFinder.SaveImage(currentImage, filename);
                }
            }
        }
        #endregion

        private void GetImages()
        {
            this.listView.LargeImageList = this.imageFinder.AsLargeImageList();
            this.listView.SmallImageList = this.imageFinder.AsSmallImageList();
            this.listView.Items.AddRange(this.imageFinder.Images().Select(this.ToListViewItem).ToArray());
        }

        private ListViewItem ToListViewItem(string guid)
        {
            return new ListViewItem { Name = guid, ImageKey = guid };
        }
    }
}
