﻿using System;
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
            btnRegister.Click += BtnRegister_Click;
        }

        private void BtnRegister_Click(object sender, EventArgs e)
        {
            RegisterForm registerForm = new RegisterForm();
            registerForm.FormClosed += (s, args) => this.Show();
            registerForm.Show();
            this.Hide();
        }

        private async void BtnLogin_Click(object sender, EventArgs e)
        {
            var username = txtUsername.Text;
            var password = txtPassword.Text;

            if(username == null || password == null)
            {
                MessageBox.Show("Vui lòng nhập đầy đủ thông tin đăng nhập");
                return;
            }

            var userName = await _userBll.LoginAsync(username, password);
            if (userName != null)
            {
                UserContext.CurrentUserName = userName;
                MessageBox.Show("Đăng nhập thành công!");
                MainForm mainForm = new MainForm();
                mainForm.FormClosed += (s, args) => this.Show();
                mainForm.Show();
                this.Hide();
            }
            else
            {
                MessageBox.Show("Đăng nhập không thành công. Vui lòng kiểm tra lại thông tin.", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
    }
}
