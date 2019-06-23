namespace EasingDemos
{
    partial class MainWindow
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
            this.easings = new System.Windows.Forms.ComboBox();
            this.bitmapDisplay = new System.Windows.Forms.PictureBox();
            ((System.ComponentModel.ISupportInitialize)(this.bitmapDisplay)).BeginInit();
            this.SuspendLayout();
            // 
            // easings
            // 
            this.easings.FormattingEnabled = true;
            this.easings.Location = new System.Drawing.Point(12, 12);
            this.easings.Name = "easings";
            this.easings.Size = new System.Drawing.Size(260, 21);
            this.easings.TabIndex = 0;
            this.easings.SelectedIndexChanged += new System.EventHandler(this.easings_SelectedIndexChanged);
            // 
            // bitmapDisplay
            // 
            this.bitmapDisplay.Location = new System.Drawing.Point(12, 39);
            this.bitmapDisplay.Name = "bitmapDisplay";
            this.bitmapDisplay.Size = new System.Drawing.Size(260, 210);
            this.bitmapDisplay.TabIndex = 1;
            this.bitmapDisplay.TabStop = false;
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(284, 261);
            this.Controls.Add(this.bitmapDisplay);
            this.Controls.Add(this.easings);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "Form1";
            this.Text = "GTween Easing Demos";
            this.Load += new System.EventHandler(this.Form1_Load);
            ((System.ComponentModel.ISupportInitialize)(this.bitmapDisplay)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.ComboBox easings;
        private System.Windows.Forms.PictureBox bitmapDisplay;
    }
}

