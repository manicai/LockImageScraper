using System;
using System.Linq;
using System.Windows.Forms;

namespace LockImageScraper
{
    public partial class MainForm : Form
    {
        private readonly ImageFinder imageFinder;

        private bool showPortrait = true;

        private bool showLandscape = true;

        public MainForm()
        {
            this.InitializeComponent();
            this.imageFinder = new ImageFinder();
            this.GetImages();

            this.checkBoxPortrait.CheckState = this.showPortrait ? CheckState.Checked : CheckState.Unchecked;
            this.checkBoxLandscape.CheckState = this.showLandscape ? CheckState.Checked : CheckState.Unchecked;
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

        private void CheckBoxLandscapeStateChanged(object sender, EventArgs e)
        {
            var show = (this.checkBoxLandscape.CheckState == CheckState.Checked);
            if (show != this.showLandscape)
            {
                this.showLandscape = show;
                this.GetImages();
            }
        }

        private void CheckBoxPortraitStateChanged(object sender, EventArgs e)
        {
            var show = (this.checkBoxPortrait.CheckState == CheckState.Checked);
            if (show != this.showPortrait)
            {
                this.showPortrait = show;
                this.GetImages();
            }
        }
        #endregion

        private void GetImages()
        {
            try
            {
                this.listView.BeginUpdate();
                this.listView.Items.Clear();
                this.listView.LargeImageList = this.imageFinder.AsLargeImageList();
                this.listView.SmallImageList = this.imageFinder.AsSmallImageList();
                this.listView.Items.AddRange(this.imageFinder.Images(this.showLandscape, this.showPortrait).Select(this.ToListViewItem).ToArray());
            }
            finally
            {
                this.listView.EndUpdate();
            }
        }

        private ListViewItem ToListViewItem(string guid)
        {
            return new ListViewItem { Name = guid, ImageKey = guid };
        }
    }
}
