using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using BLL;
using DTO;

namespace GUI
{
    public partial class LoginForm : Form
    {
        private readonly UserBLL _userBll;
        public LoginForm()
        {
            InitializeComponent();
            _userBll = new UserBLL();
            btnLogin.Click += BtnLogin_Click;
        }

        private async void BtnLogin_Click(object sender, EventArgs e)
        {
            var username = txtUsername.Text;
            var password = txtPassword.Text;

            var userId = await _userBll.LoginAsync(username, password);
            if (userId != null)
            {
                UserContext.CurrentUserId = userId; // Lưu ID người dùng vào biến toàn cục
                MessageBox.Show("Đăng nhập thành công!");
                FamilyTreeForm familyTreeForm = new FamilyTreeForm();
                familyTreeForm.FormClosed += (s, args) => this.Show();
                familyTreeForm.Show();
                this.Hide();
            }
            else
            {
                MessageBox.Show("Đăng nhập không thành công. Vui lòng kiểm tra lại thông tin.", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
    }
}
