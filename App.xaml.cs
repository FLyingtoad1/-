using System.Windows;

namespace 欧姆龙视觉示例
{
    public partial class App : Application
    {
        protected override void OnStartup(StartupEventArgs e)
        {
            base.OnStartup(e);

            // 显示登录窗口
            LoginWindow loginWindow = new LoginWindow();
            loginWindow.Show();
        }
    }
}
