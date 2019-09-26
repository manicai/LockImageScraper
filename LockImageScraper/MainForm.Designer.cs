namespace LockImageScraper
{
    partial class MainForm
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.buttonSave = new System.Windows.Forms.Button();
            this.listView = new System.Windows.Forms.ListView();
            this.checkBoxLandscape = new System.Windows.Forms.CheckBox();
            this.checkBoxPortrait = new System.Windows.Forms.CheckBox();
            this.label1 = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // buttonSave
            // 
            this.buttonSave.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.buttonSave.Location = new System.Drawing.Point(697, 528);
            this.buttonSave.Name = "buttonSave";
            this.buttonSave.Size = new System.Drawing.Size(75, 23);
            this.buttonSave.TabIndex = 2;
            this.buttonSave.Text = "Save";
            this.buttonSave.UseVisualStyleBackColor = true;
            this.buttonSave.Click += new System.EventHandler(this.OnButtonSaveClicked);
            // 
            // listView
            // 
            this.listView.Alignment = System.Windows.Forms.ListViewAlignment.SnapToGrid;
            this.listView.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.listView.GridLines = true;
            this.listView.Location = new System.Drawing.Point(12, 12);
            this.listView.MultiSelect = false;
            this.listView.Name = "listView";
            this.listView.Size = new System.Drawing.Size(760, 510);
            this.listView.TabIndex = 3;
            this.listView.UseCompatibleStateImageBehavior = false;
            // 
            // checkBoxLandscape
            // 
            this.checkBoxLandscape.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.checkBoxLandscape.AutoSize = true;
            this.checkBoxLandscape.Checked = true;
            this.checkBoxLandscape.CheckState = System.Windows.Forms.CheckState.Checked;
            this.checkBoxLandscape.Location = new System.Drawing.Point(52, 532);
            this.checkBoxLandscape.Name = "checkBoxLandscape";
            this.checkBoxLandscape.Size = new System.Drawing.Size(79, 17);
            this.checkBoxLandscape.TabIndex = 4;
            this.checkBoxLandscape.Text = "Landscape";
            this.checkBoxLandscape.UseVisualStyleBackColor = true;
            this.checkBoxLandscape.CheckStateChanged += new System.EventHandler(this.CheckBoxLandscapeStateChanged);
            // 
            // checkBoxPortrait
            // 
            this.checkBoxPortrait.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.checkBoxPortrait.AutoSize = true;
            this.checkBoxPortrait.Checked = true;
            this.checkBoxPortrait.CheckState = System.Windows.Forms.CheckState.Checked;
            this.checkBoxPortrait.Location = new System.Drawing.Point(137, 532);
            this.checkBoxPortrait.Name = "checkBoxPortrait";
            this.checkBoxPortrait.Size = new System.Drawing.Size(59, 17);
            this.checkBoxPortrait.TabIndex = 5;
            this.checkBoxPortrait.Text = "Portrait";
            this.checkBoxPortrait.UseVisualStyleBackColor = true;
            this.checkBoxPortrait.CheckStateChanged += new System.EventHandler(this.CheckBoxPortraitStateChanged);
            // 
            // label1
            // 
            this.label1.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(12, 533);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(34, 13);
            this.label1.TabIndex = 6;
            this.label1.Text = "Show";
            // 
            // MainForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(784, 561);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.checkBoxPortrait);
            this.Controls.Add(this.checkBoxLandscape);
            this.Controls.Add(this.listView);
            this.Controls.Add(this.buttonSave);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.Name = "MainForm";
            this.SizeGripStyle = System.Windows.Forms.SizeGripStyle.Hide;
            this.Text = "Lock Image Scraper";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion
        private System.Windows.Forms.Button buttonSave;
        private System.Windows.Forms.ListView listView;
        private System.Windows.Forms.CheckBox checkBoxLandscape;
        private System.Windows.Forms.CheckBox checkBoxPortrait;
        private System.Windows.Forms.Label label1;
    }
}

