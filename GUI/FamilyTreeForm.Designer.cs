
namespace GUI
{
    partial class FamilyTreeForm
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
            this.btnSearch = new System.Windows.Forms.Button();
            this.txtMember = new System.Windows.Forms.TextBox();
            this.treeViewGiaPha = new System.Windows.Forms.TreeView();
            this.btnThem = new System.Windows.Forms.Button();
            this.label1 = new System.Windows.Forms.Label();
            this.btnViewDetails = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // btnSearch
            // 
            this.btnSearch.Location = new System.Drawing.Point(402, 50);
            this.btnSearch.Name = "btnSearch";
            this.btnSearch.Size = new System.Drawing.Size(94, 43);
            this.btnSearch.TabIndex = 2;
            this.btnSearch.Text = "Tìm kiếm";
            this.btnSearch.UseVisualStyleBackColor = true;
            // 
            // txtMember
            // 
            this.txtMember.Location = new System.Drawing.Point(199, 60);
            this.txtMember.Name = "txtMember";
            this.txtMember.Size = new System.Drawing.Size(187, 22);
            this.txtMember.TabIndex = 3;
            // 
            // treeViewGiaPha
            // 
            this.treeViewGiaPha.Location = new System.Drawing.Point(84, 120);
            this.treeViewGiaPha.Name = "treeViewGiaPha";
            this.treeViewGiaPha.Size = new System.Drawing.Size(722, 355);
            this.treeViewGiaPha.TabIndex = 4;
            // 
            // btnThem
            // 
            this.btnThem.Location = new System.Drawing.Point(665, 50);
            this.btnThem.Name = "btnThem";
            this.btnThem.Size = new System.Drawing.Size(141, 43);
            this.btnThem.TabIndex = 5;
            this.btnThem.Text = "Thêm thành viên";
            this.btnThem.UseVisualStyleBackColor = true;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.Location = new System.Drawing.Point(81, 61);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(103, 18);
            this.label1.TabIndex = 6;
            this.label1.Text = "Tên thành viên";
            // 
            // btnViewDetails
            // 
            this.btnViewDetails.Location = new System.Drawing.Point(512, 50);
            this.btnViewDetails.Name = "btnViewDetails";
            this.btnViewDetails.Size = new System.Drawing.Size(141, 43);
            this.btnViewDetails.TabIndex = 7;
            this.btnViewDetails.Text = "Xem chi tiết";
            this.btnViewDetails.UseVisualStyleBackColor = true;
            // 
            
            // 
            // FamilyTreeForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(891, 521);
            this.Controls.Add(this.btnViewDetails);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.btnThem);
            this.Controls.Add(this.treeViewGiaPha);
            this.Controls.Add(this.txtMember);
            this.Controls.Add(this.btnSearch);
            this.Name = "FamilyTreeForm";
            this.Text = "Cây gia phả";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button btnSearch;
        private System.Windows.Forms.TextBox txtMember;
        private System.Windows.Forms.TreeView treeViewGiaPha;
        private System.Windows.Forms.Button btnThem;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Button btnViewDetails;
    }
}

