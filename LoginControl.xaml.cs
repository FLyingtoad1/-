using System.Windows;
using System.Windows.Input;

namespace 欧姆龙视觉示例
{
    public partial class LoginWindow : Window
    {
        public LoginWindow()
        {
            InitializeComponent();
        }

        private void OnKeyDownHandler(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Enter)
            {
                LoginButton_Click(sender, e);
            }
        }

        private void LoginButton_Click(object sender, RoutedEventArgs e)
        {
            string username = UsernameTextBox.Text;
            string password = PasswordBox.Password;

            // 验证用户名和密码
            if (username == "admin" && password == "123456") // 示例，实际情况应使用更安全的验证方法
            {
                // 登录成功，显示主窗口
                MainWindow mainWindow = new MainWindow();
                mainWindow.Show();

                // 关闭登录窗口
                this.Close();
            }
            else
            {
                MessageBox.Show("用户名或密码错误，请重试。", "登录失败", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }
    }
}
