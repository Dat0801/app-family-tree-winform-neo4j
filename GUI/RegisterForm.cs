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
namespace GUI
{
    public partial class RegisterForm : Form
    {
        UserBLL userBLL = new UserBLL();
        public RegisterForm()
        {
            InitializeComponent();
            btnRegister.Click += BtnRegister_Click;
        }

        private async void BtnRegister_Click(object sender, EventArgs e)
        {
            string username = txtUsername.Text;
            string password = txtPassword.Text;
            string repassword = txtRePassword.Text;
            if (username == null || password == null || repassword == null)
            {
                MessageBox.Show("Vui lòng nhập đầy đủ thông tin đăng ký");
                return;
            }

            if(password != repassword)
            {
                MessageBox.Show("Mật khẩu không trùng khớp! Vui lòng nhập lại!");
                return;
            }
            bool result = await userBLL.RegisterUserAsync(username, password);
            if(result)
            {
                MessageBox.Show("Đăng ký thành công");
                this.Close();
            } else
            {
                MessageBox.Show("Đăng ký thất bại");
            }
        }
    }
}
