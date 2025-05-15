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
            this.InformationBox1 = new System.Windows.Forms.Label();
            this.TestBox = new System.Windows.Forms.Label();
            this.CpuInfoBox = new System.Windows.Forms.Label();
            this.DiskInfo = new System.Windows.Forms.Label();
            this.DiskSpaceUsedBar = new System.Windows.Forms.ProgressBar();
            this.MemoryInfoBox = new System.Windows.Forms.Label();
            this.GPUInfoBox = new System.Windows.Forms.Label();
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
            this.os9background.Size = new System.Drawing.Size(1243, 700);
            this.os9background.TabIndex = 0;
            this.os9background.TabStop = false;
            this.os9background.MouseDown += new System.Windows.Forms.MouseEventHandler(this.os9_MouseDown);
            this.os9background.MouseMove += new System.Windows.Forms.MouseEventHandler(this.os9_MouseMove);
            this.os9background.MouseUp += new System.Windows.Forms.MouseEventHandler(this.os9_MouseUp);
            // 
            // InformationBox1
            // 
            this.InformationBox1.AutoSize = true;
            this.InformationBox1.BackColor = System.Drawing.Color.WhiteSmoke;
            this.InformationBox1.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.InformationBox1.Font = new System.Drawing.Font("MS Mincho", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.InformationBox1.Location = new System.Drawing.Point(12, 217);
            this.InformationBox1.MaximumSize = new System.Drawing.Size(420, 17);
            this.InformationBox1.Name = "InformationBox1";
            this.InformationBox1.Size = new System.Drawing.Size(89, 17);
            this.InformationBox1.TabIndex = 2;
            this.InformationBox1.Text = "Loading...";
            // 
            // TestBox
            // 
            this.TestBox.BackColor = System.Drawing.Color.WhiteSmoke;
            this.TestBox.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.TestBox.Font = new System.Drawing.Font("MS Mincho", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.TestBox.Location = new System.Drawing.Point(508, 9);
            this.TestBox.MinimumSize = new System.Drawing.Size(400, 200);
            this.TestBox.Name = "TestBox";
            this.TestBox.Size = new System.Drawing.Size(553, 337);
            this.TestBox.TabIndex = 3;
            this.TestBox.Text = "Test Data Output Here";
            // 
            // CpuInfoBox
            // 
            this.CpuInfoBox.AutoSize = true;
            this.CpuInfoBox.BackColor = System.Drawing.Color.WhiteSmoke;
            this.CpuInfoBox.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.CpuInfoBox.Font = new System.Drawing.Font("MS Mincho", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.CpuInfoBox.Location = new System.Drawing.Point(12, 236);
            this.CpuInfoBox.MaximumSize = new System.Drawing.Size(420, 36);
            this.CpuInfoBox.Name = "CpuInfoBox";
            this.CpuInfoBox.Size = new System.Drawing.Size(89, 17);
            this.CpuInfoBox.TabIndex = 4;
            this.CpuInfoBox.Text = "Loading...";
            // 
            // DiskInfo
            // 
            this.DiskInfo.AutoSize = true;
            this.DiskInfo.BackColor = System.Drawing.Color.WhiteSmoke;
            this.DiskInfo.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.DiskInfo.Font = new System.Drawing.Font("MS Mincho", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.DiskInfo.Location = new System.Drawing.Point(12, 270);
            this.DiskInfo.MaximumSize = new System.Drawing.Size(420, 36);
            this.DiskInfo.Name = "DiskInfo";
            this.DiskInfo.Size = new System.Drawing.Size(89, 17);
            this.DiskInfo.TabIndex = 5;
            this.DiskInfo.Text = "Loading...";
            // 
            // DiskSpaceUsedBar
            // 
            this.DiskSpaceUsedBar.Location = new System.Drawing.Point(13, 306);
            this.DiskSpaceUsedBar.Name = "DiskSpaceUsedBar";
            this.DiskSpaceUsedBar.Size = new System.Drawing.Size(337, 10);
            this.DiskSpaceUsedBar.Step = 1;
            this.DiskSpaceUsedBar.TabIndex = 6;
            this.DiskSpaceUsedBar.Visible = false;
            // 
            // MemoryInfoBox
            // 
            this.MemoryInfoBox.AutoSize = true;
            this.MemoryInfoBox.BackColor = System.Drawing.Color.WhiteSmoke;
            this.MemoryInfoBox.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.MemoryInfoBox.Font = new System.Drawing.Font("MS Mincho", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.MemoryInfoBox.Location = new System.Drawing.Point(12, 319);
            this.MemoryInfoBox.MaximumSize = new System.Drawing.Size(420, 36);
            this.MemoryInfoBox.Name = "MemoryInfoBox";
            this.MemoryInfoBox.Size = new System.Drawing.Size(89, 17);
            this.MemoryInfoBox.TabIndex = 7;
            this.MemoryInfoBox.Text = "Loading...";
            // 
            // GPUInfoBox
            // 
            this.GPUInfoBox.AutoSize = true;
            this.GPUInfoBox.BackColor = System.Drawing.Color.WhiteSmoke;
            this.GPUInfoBox.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.GPUInfoBox.Font = new System.Drawing.Font("MS Mincho", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.GPUInfoBox.Location = new System.Drawing.Point(12, 352);
            this.GPUInfoBox.MaximumSize = new System.Drawing.Size(420, 36);
            this.GPUInfoBox.Name = "GPUInfoBox";
            this.GPUInfoBox.Size = new System.Drawing.Size(89, 17);
            this.GPUInfoBox.TabIndex = 8;
            this.GPUInfoBox.Text = "Loading...";
            // 
            // MimiNoseMain
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(10F, 19F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.Fuchsia;
            this.ClientSize = new System.Drawing.Size(1243, 700);
            this.Controls.Add(this.GPUInfoBox);
            this.Controls.Add(this.MemoryInfoBox);
            this.Controls.Add(this.DiskSpaceUsedBar);
            this.Controls.Add(this.DiskInfo);
            this.Controls.Add(this.CpuInfoBox);
            this.Controls.Add(this.TestBox);
            this.Controls.Add(this.InformationBox1);
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
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.PictureBox os9background;
        private System.Windows.Forms.PictureBox pictureBox1;
        private System.Windows.Forms.Label InformationBox1;
        private System.Windows.Forms.Label TestBox;
        private System.Windows.Forms.Label CpuInfoBox;
        private System.Windows.Forms.Label DiskInfo;
        private System.Windows.Forms.ProgressBar DiskSpaceUsedBar;
        private System.Windows.Forms.Label MemoryInfoBox;
        private System.Windows.Forms.Label GPUInfoBox;
    }
}

