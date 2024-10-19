
namespace GUI
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
            this.menuStrip1 = new System.Windows.Forms.MenuStrip();
            this.sToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.dsThanhVienToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.câyGiaPhảToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.themThanhVientoolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.menuStrip1.SuspendLayout();
            this.SuspendLayout();
            // 
            // menuStrip1
            // 
            this.menuStrip1.ImageScalingSize = new System.Drawing.Size(20, 20);
            this.menuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.sToolStripMenuItem});
            this.menuStrip1.Location = new System.Drawing.Point(0, 0);
            this.menuStrip1.Name = "menuStrip1";
            this.menuStrip1.Size = new System.Drawing.Size(1044, 38);
            this.menuStrip1.TabIndex = 0;
            this.menuStrip1.Text = "menuStrip1";
            // 
            // sToolStripMenuItem
            // 
            this.sToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.dsThanhVienToolStripMenuItem,
            this.câyGiaPhảToolStripMenuItem,
            this.themThanhVientoolStripMenuItem});
            this.sToolStripMenuItem.Name = "sToolStripMenuItem";
            this.sToolStripMenuItem.Size = new System.Drawing.Size(93, 34);
            this.sToolStripMenuItem.Text = "Chức năng";
            // 
            // dsThanhVienToolStripMenuItem
            // 
            this.dsThanhVienToolStripMenuItem.Name = "dsThanhVienToolStripMenuItem";
            this.dsThanhVienToolStripMenuItem.Size = new System.Drawing.Size(232, 26);
            this.dsThanhVienToolStripMenuItem.Text = "Danh sách thành viên";
            // 
            // câyGiaPhảToolStripMenuItem
            // 
            this.câyGiaPhảToolStripMenuItem.Name = "câyGiaPhảToolStripMenuItem";
            this.câyGiaPhảToolStripMenuItem.Size = new System.Drawing.Size(232, 26);
            this.câyGiaPhảToolStripMenuItem.Text = "Cây gia phả";
            // 
            // themThanhVientoolStripMenuItem
            // 
            this.themThanhVientoolStripMenuItem.Name = "themThanhVientoolStripMenuItem";
            this.themThanhVientoolStripMenuItem.Size = new System.Drawing.Size(232, 26);
            this.themThanhVientoolStripMenuItem.Text = "Thêm thành viên";
            // 
            // MainForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackgroundImage = global::GUI.Properties.Resources.istockphoto_457853955_612x612;
            this.ClientSize = new System.Drawing.Size(835, 536);
            this.Controls.Add(this.menuStrip1);
            this.MainMenuStrip = this.menuStrip1;
            this.Name = "MainForm";
            this.Text = "Trang chủ";
            this.menuStrip1.ResumeLayout(false);
            this.menuStrip1.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.MenuStrip menuStrip1;
        private System.Windows.Forms.ToolStripMenuItem sToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem dsThanhVienToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem câyGiaPhảToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem themThanhVientoolStripMenuItem;
    }
}