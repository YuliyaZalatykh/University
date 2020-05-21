using System;
using System.Windows.Forms;
using University.Controller;
using University.BO;

namespace University
{
    public partial class Login : Form
    {
        Query query = new Query(ConnectionString.ConnStr);

        public Login()
        {
            Program.LoginWindow = this;
            InitializeComponent();
        }

        private void txtUsername_KeyPress(object sender, KeyPressEventArgs e)
        {
            if(e.KeyChar == (char)13)
            {
                txtPassword.Focus();
            }
        }

        private void txtPassword_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == (char)13)
            {
                btnLogin.PerformClick();
            }
        }

        private void btnLogin_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(txtUsername.Text) || string.IsNullOrEmpty(txtPassword.Text))
            {
                MessageBox.Show("Пожалуйста, заполните поля.", "Сообщение", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                txtUsername.Focus();
                return;
            }
            try
            {
                // закодировать пароль
                string password = Eramake.eCryptography.Encrypt(txtPassword.Text);

                User user = query.GetUserByLogin(txtUsername.Text);

                if (password.Equals(user.password))
                {
                    MessageBox.Show("Вы успешно авторизировались!", "Сообщение", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    Dashboard dashboardWindow = new Dashboard();
                    Program.DashboardWindow.Show();
                    this.Hide();
                }
                else
                {
                    MessageBox.Show("Вы ввели неверный логин и/или пароль!", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
    }
}
