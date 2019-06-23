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
            this.components = new System.ComponentModel.Container();
            this.easings = new System.Windows.Forms.ComboBox();
            this.bitmapDisplay = new System.Windows.Forms.PictureBox();
            this.easerFunctions = new System.Windows.Forms.ComboBox();
            this.bitmapDisplayActions = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.saveImageToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            ((System.ComponentModel.ISupportInitialize)(this.bitmapDisplay)).BeginInit();
            this.bitmapDisplayActions.SuspendLayout();
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
            this.bitmapDisplay.Location = new System.Drawing.Point(12, 66);
            this.bitmapDisplay.Name = "bitmapDisplay";
            this.bitmapDisplay.Size = new System.Drawing.Size(260, 283);
            this.bitmapDisplay.TabIndex = 1;
            this.bitmapDisplay.TabStop = false;
            // 
            // easerFunctions
            // 
            this.easerFunctions.FormattingEnabled = true;
            this.easerFunctions.Location = new System.Drawing.Point(12, 39);
            this.easerFunctions.Name = "easerFunctions";
            this.easerFunctions.Size = new System.Drawing.Size(260, 21);
            this.easerFunctions.TabIndex = 2;
            this.easerFunctions.SelectedIndexChanged += new System.EventHandler(this.easerFunctions_SelectedIndexChanged);
            // 
            // bitmapDisplayActions
            // 
            this.bitmapDisplayActions.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.saveImageToolStripMenuItem});
            this.bitmapDisplayActions.Name = "contextMenuStrip1";
            this.bitmapDisplayActions.Size = new System.Drawing.Size(153, 48);
            // 
            // saveImageToolStripMenuItem
            // 
            this.saveImageToolStripMenuItem.Name = "saveImageToolStripMenuItem";
            this.saveImageToolStripMenuItem.Size = new System.Drawing.Size(152, 22);
            this.saveImageToolStripMenuItem.Text = "&Save Image...";
            this.saveImageToolStripMenuItem.Click += new System.EventHandler(this.saveImageToolStripMenuItem_Click);
            // 
            // MainWindow
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(284, 361);
            this.Controls.Add(this.easerFunctions);
            this.Controls.Add(this.bitmapDisplay);
            this.Controls.Add(this.easings);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "MainWindow";
            this.Text = "GTween Easing Demos";
            this.Load += new System.EventHandler(this.Form1_Load);
            ((System.ComponentModel.ISupportInitialize)(this.bitmapDisplay)).EndInit();
            this.bitmapDisplayActions.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.ComboBox easings;
        private System.Windows.Forms.PictureBox bitmapDisplay;
        private System.Windows.Forms.ComboBox easerFunctions;
        private System.Windows.Forms.ContextMenuStrip bitmapDisplayActions;
        private System.Windows.Forms.ToolStripMenuItem saveImageToolStripMenuItem;
    }
}

