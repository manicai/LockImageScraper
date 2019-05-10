using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace LockImageScraper
{
    public partial class MainForm : Form
    {
        private ImageList imageList;
        public MainForm()
        {
            InitializeComponent();
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
            this.pictureBox.Image = image;
            this.pictureBox.SizeMode = PictureBoxSizeMode.StretchImage;
        }
    }
}
