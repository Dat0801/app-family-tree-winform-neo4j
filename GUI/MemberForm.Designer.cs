﻿namespace GUI
{
    partial class MemberForm
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
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.fullname_txt = new System.Windows.Forms.RichTextBox();
            this.label4 = new System.Windows.Forms.Label();
            this.dateTimePicker1 = new System.Windows.Forms.DateTimePicker();
            this.gender_combo = new System.Windows.Forms.ComboBox();
            this.address_txt = new System.Windows.Forms.RichTextBox();
            this.label5 = new System.Windows.Forms.Label();
            this.clear_btn = new System.Windows.Forms.Button();
            this.occupation_txt = new System.Windows.Forms.RichTextBox();
            this.label6 = new System.Windows.Forms.Label();
            this.datafamilytree = new System.Windows.Forms.DataGridView();
            this.id_data = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.hoten_data = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.birthdate_data = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.gender_data = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.phone_data = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.address_data = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.occupation = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.delete_btn = new System.Windows.Forms.Button();
            this.update_btn = new System.Windows.Forms.Button();
            this.detail_btn = new System.Windows.Forms.Button();
            this.search_btn = new System.Windows.Forms.Button();
            this.phonenum_txt = new System.Windows.Forms.RichTextBox();
            this.label7 = new System.Windows.Forms.Label();
            this.btnExcel = new System.Windows.Forms.Button();
            this.btnXemCayGiaPha = new System.Windows.Forms.Button();
            ((System.ComponentModel.ISupportInitialize)(this.datafamilytree)).BeginInit();
            this.SuspendLayout();
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Tahoma", 18F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.Location = new System.Drawing.Point(492, 11);
            this.label1.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(327, 36);
            this.label1.TabIndex = 1;
            this.label1.Text = "Tìm kiếm theo người";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("Tahoma", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label2.Location = new System.Drawing.Point(23, 78);
            this.label2.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(75, 24);
            this.label2.TabIndex = 2;
            this.label2.Text = "Họ Tên";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Font = new System.Drawing.Font("Tahoma", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label3.Location = new System.Drawing.Point(317, 78);
            this.label3.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(97, 24);
            this.label3.TabIndex = 4;
            this.label3.Text = "Ngày sinh";
            // 
            // fullname_txt
            // 
            this.fullname_txt.Font = new System.Drawing.Font("Tahoma", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.fullname_txt.Location = new System.Drawing.Point(28, 105);
            this.fullname_txt.Margin = new System.Windows.Forms.Padding(4);
            this.fullname_txt.Name = "fullname_txt";
            this.fullname_txt.Size = new System.Drawing.Size(247, 31);
            this.fullname_txt.TabIndex = 5;
            this.fullname_txt.Text = "";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Font = new System.Drawing.Font("Tahoma", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label4.Location = new System.Drawing.Point(23, 156);
            this.label4.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(84, 24);
            this.label4.TabIndex = 7;
            this.label4.Text = "Giới tính";
            // 
            // dateTimePicker1
            // 
            this.dateTimePicker1.Font = new System.Drawing.Font("Tahoma", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.dateTimePicker1.Location = new System.Drawing.Point(323, 105);
            this.dateTimePicker1.Margin = new System.Windows.Forms.Padding(4);
            this.dateTimePicker1.Name = "dateTimePicker1";
            this.dateTimePicker1.Size = new System.Drawing.Size(247, 32);
            this.dateTimePicker1.TabIndex = 9;
            // 
            // gender_combo
            // 
            this.gender_combo.Font = new System.Drawing.Font("Tahoma", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.gender_combo.FormattingEnabled = true;
            this.gender_combo.Items.AddRange(new object[] {
            "Nam",
            "Nữ"});
            this.gender_combo.Location = new System.Drawing.Point(28, 183);
            this.gender_combo.Margin = new System.Windows.Forms.Padding(4);
            this.gender_combo.Name = "gender_combo";
            this.gender_combo.Size = new System.Drawing.Size(160, 32);
            this.gender_combo.TabIndex = 10;
            // 
            // address_txt
            // 
            this.address_txt.Font = new System.Drawing.Font("Tahoma", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.address_txt.Location = new System.Drawing.Point(224, 183);
            this.address_txt.Margin = new System.Windows.Forms.Padding(4);
            this.address_txt.Name = "address_txt";
            this.address_txt.Size = new System.Drawing.Size(247, 31);
            this.address_txt.TabIndex = 12;
            this.address_txt.Text = "";
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Font = new System.Drawing.Font("Tahoma", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label5.Location = new System.Drawing.Point(219, 156);
            this.label5.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(71, 24);
            this.label5.TabIndex = 11;
            this.label5.Text = "Địa chỉ";
            // 
            // clear_btn
            // 
            this.clear_btn.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(143)))), ((int)(((byte)(227)))), ((int)(((byte)(193)))));
            this.clear_btn.BackgroundImageLayout = System.Windows.Forms.ImageLayout.None;
            this.clear_btn.Cursor = System.Windows.Forms.Cursors.Hand;
            this.clear_btn.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
            this.clear_btn.Font = new System.Drawing.Font("Tahoma", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.clear_btn.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.clear_btn.Location = new System.Drawing.Point(1135, 116);
            this.clear_btn.Margin = new System.Windows.Forms.Padding(4);
            this.clear_btn.Name = "clear_btn";
            this.clear_btn.Size = new System.Drawing.Size(178, 41);
            this.clear_btn.TabIndex = 15;
            this.clear_btn.Text = "Xóa lọc";
            this.clear_btn.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText;
            this.clear_btn.UseVisualStyleBackColor = false;
            this.clear_btn.Click += new System.EventHandler(this.clear_btn_Click);
            // 
            // occupation_txt
            // 
            this.occupation_txt.Font = new System.Drawing.Font("Tahoma", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.occupation_txt.Location = new System.Drawing.Point(499, 183);
            this.occupation_txt.Margin = new System.Windows.Forms.Padding(4);
            this.occupation_txt.Name = "occupation_txt";
            this.occupation_txt.Size = new System.Drawing.Size(247, 31);
            this.occupation_txt.TabIndex = 17;
            this.occupation_txt.Text = "";
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Font = new System.Drawing.Font("Tahoma", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label6.Location = new System.Drawing.Point(493, 156);
            this.label6.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(122, 24);
            this.label6.TabIndex = 16;
            this.label6.Text = "Nghề nghiệp";
            // 
            // datafamilytree
            // 
            this.datafamilytree.BackgroundColor = System.Drawing.Color.FromArgb(((int)(((byte)(243)))), ((int)(((byte)(254)))), ((int)(((byte)(214)))));
            this.datafamilytree.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.datafamilytree.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.id_data,
            this.hoten_data,
            this.birthdate_data,
            this.gender_data,
            this.phone_data,
            this.address_data,
            this.occupation});
            this.datafamilytree.GridColor = System.Drawing.SystemColors.ButtonHighlight;
            this.datafamilytree.Location = new System.Drawing.Point(-1, 254);
            this.datafamilytree.Margin = new System.Windows.Forms.Padding(4);
            this.datafamilytree.Name = "datafamilytree";
            this.datafamilytree.RowHeadersWidth = 51;
            this.datafamilytree.Size = new System.Drawing.Size(1314, 286);
            this.datafamilytree.TabIndex = 18;
            this.datafamilytree.RowHeaderMouseClick += new System.Windows.Forms.DataGridViewCellMouseEventHandler(this.datafamilytree_RowHeaderMouseClick);
            // 
            // id_data
            // 
            this.id_data.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.Fill;
            this.id_data.HeaderText = "ID";
            this.id_data.MinimumWidth = 6;
            this.id_data.Name = "id_data";
            // 
            // hoten_data
            // 
            this.hoten_data.HeaderText = "Họ tên";
            this.hoten_data.MinimumWidth = 6;
            this.hoten_data.Name = "hoten_data";
            this.hoten_data.Width = 230;
            // 
            // birthdate_data
            // 
            this.birthdate_data.HeaderText = "Ngày sinh";
            this.birthdate_data.MinimumWidth = 6;
            this.birthdate_data.Name = "birthdate_data";
            this.birthdate_data.Width = 150;
            // 
            // gender_data
            // 
            this.gender_data.HeaderText = "Giới tính";
            this.gender_data.MinimumWidth = 6;
            this.gender_data.Name = "gender_data";
            this.gender_data.Width = 80;
            // 
            // phone_data
            // 
            this.phone_data.HeaderText = "SĐT";
            this.phone_data.MinimumWidth = 6;
            this.phone_data.Name = "phone_data";
            this.phone_data.Width = 125;
            // 
            // address_data
            // 
            this.address_data.HeaderText = "Địa chỉ";
            this.address_data.MinimumWidth = 6;
            this.address_data.Name = "address_data";
            this.address_data.Width = 150;
            // 
            // occupation
            // 
            this.occupation.HeaderText = "Nghề nghiệp";
            this.occupation.MinimumWidth = 6;
            this.occupation.Name = "occupation";
            this.occupation.Width = 160;
            // 
            // delete_btn
            // 
            this.delete_btn.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(225)))), ((int)(((byte)(60)))), ((int)(((byte)(47)))));
            this.delete_btn.BackgroundImageLayout = System.Windows.Forms.ImageLayout.None;
            this.delete_btn.Cursor = System.Windows.Forms.Cursors.Hand;
            this.delete_btn.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
            this.delete_btn.Font = new System.Drawing.Font("Tahoma", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.delete_btn.Image = global::GUI.Properties.Resources.bin;
            this.delete_btn.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.delete_btn.Location = new System.Drawing.Point(1135, 178);
            this.delete_btn.Margin = new System.Windows.Forms.Padding(4);
            this.delete_btn.Name = "delete_btn";
            this.delete_btn.Size = new System.Drawing.Size(178, 41);
            this.delete_btn.TabIndex = 21;
            this.delete_btn.Text = "Xóa";
            this.delete_btn.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.delete_btn.TextImageRelation = System.Windows.Forms.TextImageRelation.TextBeforeImage;
            this.delete_btn.UseVisualStyleBackColor = false;
            this.delete_btn.Click += new System.EventHandler(this.delete_btn_Click);
            // 
            // update_btn
            // 
            this.update_btn.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(223)))), ((int)(((byte)(183)))), ((int)(((byte)(245)))));
            this.update_btn.BackgroundImageLayout = System.Windows.Forms.ImageLayout.None;
            this.update_btn.Cursor = System.Windows.Forms.Cursors.Hand;
            this.update_btn.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
            this.update_btn.Font = new System.Drawing.Font("Tahoma", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.update_btn.Image = global::GUI.Properties.Resources.refresh;
            this.update_btn.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.update_btn.Location = new System.Drawing.Point(968, 180);
            this.update_btn.Margin = new System.Windows.Forms.Padding(4);
            this.update_btn.Name = "update_btn";
            this.update_btn.Size = new System.Drawing.Size(140, 41);
            this.update_btn.TabIndex = 20;
            this.update_btn.Text = "Sửa";
            this.update_btn.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.update_btn.TextImageRelation = System.Windows.Forms.TextImageRelation.TextBeforeImage;
            this.update_btn.UseVisualStyleBackColor = false;
            this.update_btn.Click += new System.EventHandler(this.update_btn_Click);
            // 
            // detail_btn
            // 
            this.detail_btn.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(181)))), ((int)(((byte)(186)))));
            this.detail_btn.BackgroundImageLayout = System.Windows.Forms.ImageLayout.None;
            this.detail_btn.Cursor = System.Windows.Forms.Cursors.Hand;
            this.detail_btn.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
            this.detail_btn.Font = new System.Drawing.Font("Tahoma", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.detail_btn.Image = global::GUI.Properties.Resources.information;
            this.detail_btn.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.detail_btn.Location = new System.Drawing.Point(809, 178);
            this.detail_btn.Margin = new System.Windows.Forms.Padding(4);
            this.detail_btn.Name = "detail_btn";
            this.detail_btn.Size = new System.Drawing.Size(127, 41);
            this.detail_btn.TabIndex = 19;
            this.detail_btn.Text = "Thông tin";
            this.detail_btn.TextImageRelation = System.Windows.Forms.TextImageRelation.TextBeforeImage;
            this.detail_btn.UseVisualStyleBackColor = false;
            this.detail_btn.Click += new System.EventHandler(this.detail_btn_Click);
            // 
            // search_btn
            // 
            this.search_btn.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(143)))), ((int)(((byte)(227)))), ((int)(((byte)(193)))));
            this.search_btn.BackgroundImageLayout = System.Windows.Forms.ImageLayout.None;
            this.search_btn.Cursor = System.Windows.Forms.Cursors.Hand;
            this.search_btn.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
            this.search_btn.Font = new System.Drawing.Font("Tahoma", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.search_btn.Image = global::GUI.Properties.Resources.magnifying_glass1;
            this.search_btn.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.search_btn.Location = new System.Drawing.Point(968, 116);
            this.search_btn.Margin = new System.Windows.Forms.Padding(4);
            this.search_btn.Name = "search_btn";
            this.search_btn.Size = new System.Drawing.Size(140, 41);
            this.search_btn.TabIndex = 14;
            this.search_btn.Text = "Tìm kiếm";
            this.search_btn.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.search_btn.TextImageRelation = System.Windows.Forms.TextImageRelation.TextBeforeImage;
            this.search_btn.UseVisualStyleBackColor = false;
            this.search_btn.Click += new System.EventHandler(this.search_btn_Click);
            // 
            // phonenum_txt
            // 
            this.phonenum_txt.Font = new System.Drawing.Font("Tahoma", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.phonenum_txt.Location = new System.Drawing.Point(621, 105);
            this.phonenum_txt.Margin = new System.Windows.Forms.Padding(4);
            this.phonenum_txt.Name = "phonenum_txt";
            this.phonenum_txt.Size = new System.Drawing.Size(247, 31);
            this.phonenum_txt.TabIndex = 23;
            this.phonenum_txt.Text = "";
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Font = new System.Drawing.Font("Tahoma", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label7.Location = new System.Drawing.Point(616, 78);
            this.label7.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(135, 24);
            this.label7.TabIndex = 22;
            this.label7.Text = "Số Điện Thoại";
            // 
            // btnExcel
            // 
            this.btnExcel.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(143)))), ((int)(((byte)(227)))), ((int)(((byte)(193)))));
            this.btnExcel.BackgroundImageLayout = System.Windows.Forms.ImageLayout.None;
            this.btnExcel.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnExcel.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
            this.btnExcel.Font = new System.Drawing.Font("Tahoma", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnExcel.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.btnExcel.Location = new System.Drawing.Point(968, 51);
            this.btnExcel.Margin = new System.Windows.Forms.Padding(4);
            this.btnExcel.Name = "btnExcel";
            this.btnExcel.Size = new System.Drawing.Size(140, 41);
            this.btnExcel.TabIndex = 24;
            this.btnExcel.Text = "Xuất excel";
            this.btnExcel.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText;
            this.btnExcel.UseVisualStyleBackColor = false;
            // 
            // btnXemCayGiaPha
            // 
            this.btnXemCayGiaPha.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(143)))), ((int)(((byte)(227)))), ((int)(((byte)(193)))));
            this.btnXemCayGiaPha.BackgroundImageLayout = System.Windows.Forms.ImageLayout.None;
            this.btnXemCayGiaPha.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnXemCayGiaPha.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
            this.btnXemCayGiaPha.Font = new System.Drawing.Font("Tahoma", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnXemCayGiaPha.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.btnXemCayGiaPha.Location = new System.Drawing.Point(1135, 51);
            this.btnXemCayGiaPha.Margin = new System.Windows.Forms.Padding(4);
            this.btnXemCayGiaPha.Name = "btnXemCayGiaPha";
            this.btnXemCayGiaPha.Size = new System.Drawing.Size(178, 41);
            this.btnXemCayGiaPha.TabIndex = 25;
            this.btnXemCayGiaPha.Text = "Cây gia phả";
            this.btnXemCayGiaPha.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText;
            this.btnXemCayGiaPha.UseVisualStyleBackColor = false;
            // 
            // MemberForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.AutoSize = true;
            this.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(255)))), ((int)(((byte)(192)))));
            this.ClientSize = new System.Drawing.Size(1326, 554);
            this.Controls.Add(this.btnXemCayGiaPha);
            this.Controls.Add(this.btnExcel);
            this.Controls.Add(this.phonenum_txt);
            this.Controls.Add(this.label7);
            this.Controls.Add(this.delete_btn);
            this.Controls.Add(this.update_btn);
            this.Controls.Add(this.detail_btn);
            this.Controls.Add(this.datafamilytree);
            this.Controls.Add(this.occupation_txt);
            this.Controls.Add(this.label6);
            this.Controls.Add(this.clear_btn);
            this.Controls.Add(this.search_btn);
            this.Controls.Add(this.address_txt);
            this.Controls.Add(this.label5);
            this.Controls.Add(this.gender_combo);
            this.Controls.Add(this.dateTimePicker1);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.fullname_txt);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label1);
            this.Margin = new System.Windows.Forms.Padding(4);
            this.Name = "MemberForm";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "DanhSachCayGiaPha";
            this.Load += new System.EventHandler(this.DanhSachCayGiaPha_Load);
            ((System.ComponentModel.ISupportInitialize)(this.datafamilytree)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.RichTextBox fullname_txt;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.DateTimePicker dateTimePicker1;
        private System.Windows.Forms.ComboBox gender_combo;
        private System.Windows.Forms.RichTextBox address_txt;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Button search_btn;
        private System.Windows.Forms.Button clear_btn;
        private System.Windows.Forms.RichTextBox occupation_txt;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.DataGridView datafamilytree;
        private System.Windows.Forms.Button detail_btn;
        private System.Windows.Forms.Button update_btn;
        private System.Windows.Forms.Button delete_btn;
        private System.Windows.Forms.DataGridViewTextBoxColumn id_data;
        private System.Windows.Forms.DataGridViewTextBoxColumn hoten_data;
        private System.Windows.Forms.DataGridViewTextBoxColumn birthdate_data;
        private System.Windows.Forms.DataGridViewTextBoxColumn gender_data;
        private System.Windows.Forms.DataGridViewTextBoxColumn phone_data;
        private System.Windows.Forms.DataGridViewTextBoxColumn address_data;
        private System.Windows.Forms.DataGridViewTextBoxColumn occupation;
        private System.Windows.Forms.RichTextBox phonenum_txt;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.Button btnExcel;
        private System.Windows.Forms.Button btnXemCayGiaPha;
    }
}