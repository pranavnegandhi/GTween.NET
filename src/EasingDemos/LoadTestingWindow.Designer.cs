namespace EasingDemos
{
    partial class LoadTestingWindow
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
            this.instanceCounter = new System.Windows.Forms.NumericUpDown();
            this.numberOfInstances = new System.Windows.Forms.Label();
            this.start = new System.Windows.Forms.Button();
            this.bitmapDisplay = new System.Windows.Forms.PictureBox();
            ((System.ComponentModel.ISupportInitialize)(this.instanceCounter)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.bitmapDisplay)).BeginInit();
            this.SuspendLayout();
            // 
            // instanceCounter
            // 
            this.instanceCounter.Location = new System.Drawing.Point(123, 12);
            this.instanceCounter.Maximum = new decimal(new int[] {
            1000000,
            0,
            0,
            0});
            this.instanceCounter.Name = "instanceCounter";
            this.instanceCounter.Size = new System.Drawing.Size(149, 20);
            this.instanceCounter.TabIndex = 0;
            this.instanceCounter.Value = new decimal(new int[] {
            100,
            0,
            0,
            0});
            // 
            // numberOfInstances
            // 
            this.numberOfInstances.AutoSize = true;
            this.numberOfInstances.Location = new System.Drawing.Point(12, 14);
            this.numberOfInstances.Name = "numberOfInstances";
            this.numberOfInstances.Size = new System.Drawing.Size(105, 13);
            this.numberOfInstances.TabIndex = 1;
            this.numberOfInstances.Text = "&Number of Instances";
            // 
            // start
            // 
            this.start.Location = new System.Drawing.Point(197, 38);
            this.start.Name = "start";
            this.start.Size = new System.Drawing.Size(75, 23);
            this.start.TabIndex = 2;
            this.start.Text = "&Start";
            this.start.UseVisualStyleBackColor = true;
            this.start.Click += new System.EventHandler(this.start_Click);
            // 
            // bitmapDisplay
            // 
            this.bitmapDisplay.Location = new System.Drawing.Point(12, 67);
            this.bitmapDisplay.Name = "bitmapDisplay";
            this.bitmapDisplay.Size = new System.Drawing.Size(260, 882);
            this.bitmapDisplay.TabIndex = 3;
            this.bitmapDisplay.TabStop = false;
            // 
            // LoadTestingWindow
            // 
            this.AcceptButton = this.start;
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(284, 961);
            this.Controls.Add(this.bitmapDisplay);
            this.Controls.Add(this.start);
            this.Controls.Add(this.numberOfInstances);
            this.Controls.Add(this.instanceCounter);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "LoadTestingWindow";
            this.Text = "GTween Load Testing";
            this.Load += new System.EventHandler(this.LoadTestingWindow_Load);
            ((System.ComponentModel.ISupportInitialize)(this.instanceCounter)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.bitmapDisplay)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.NumericUpDown instanceCounter;
        private System.Windows.Forms.Label numberOfInstances;
        private System.Windows.Forms.Button start;
        private System.Windows.Forms.PictureBox bitmapDisplay;
    }
}