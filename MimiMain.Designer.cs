namespace MimiNose
{
    partial class MimiNoseMain
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(MimiNoseMain));
            this.pictureBox1 = new System.Windows.Forms.PictureBox();
            this.os9background = new System.Windows.Forms.PictureBox();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.os9background)).BeginInit();
            this.SuspendLayout();
            // 
            // pictureBox1
            // 
            this.pictureBox1.Image = global::MimiNose.Properties.Resources.cloudwOverlay;
            this.pictureBox1.Location = new System.Drawing.Point(6, 208);
            this.pictureBox1.Name = "pictureBox1";
            this.pictureBox1.Size = new System.Drawing.Size(451, 324);
            this.pictureBox1.TabIndex = 1;
            this.pictureBox1.TabStop = false;
            // 
            // os9background
            // 
            this.os9background.BackColor = System.Drawing.Color.Fuchsia;
            this.os9background.Dock = System.Windows.Forms.DockStyle.Fill;
            this.os9background.Image = global::MimiNose.Properties.Resources.os9window;
            this.os9background.Location = new System.Drawing.Point(0, 0);
            this.os9background.Name = "os9background";
            this.os9background.Size = new System.Drawing.Size(488, 546);
            this.os9background.TabIndex = 0;
            this.os9background.TabStop = false;
            this.os9background.MouseDown += new System.Windows.Forms.MouseEventHandler(this.os9_MouseDown);
            this.os9background.MouseMove += new System.Windows.Forms.MouseEventHandler(this.os9_MouseMove);
            this.os9background.MouseUp += new System.Windows.Forms.MouseEventHandler(this.os9_MouseUp);
            // 
            // MimiNoseMain
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(10F, 19F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.Fuchsia;
            this.ClientSize = new System.Drawing.Size(488, 546);
            this.Controls.Add(this.pictureBox1);
            this.Controls.Add(this.os9background);
            this.Cursor = System.Windows.Forms.Cursors.Help;
            this.Font = new System.Drawing.Font("MS Mincho", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Margin = new System.Windows.Forms.Padding(5, 4, 5, 4);
            this.Name = "MimiNoseMain";
            this.Text = "MimiNose";
            this.TransparencyKey = System.Drawing.Color.Fuchsia;
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.os9background)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.PictureBox os9background;
        private System.Windows.Forms.PictureBox pictureBox1;
    }
}

