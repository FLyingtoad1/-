using System;
using System.Windows;
using System.Windows.Media.Imaging;
using System.Runtime.InteropServices;

namespace 欧姆龙视觉示例
{
    public partial class MainWindow : Window
    {
        // 定义 DLL 的导入
        [DllImport("imageprocess.dll", CallingConvention = CallingConvention.Cdecl)]
        private static extern void ProcessImage(IntPtr imageData, int width, int height, int stride);

        public MainWindow()
        {
            InitializeComponent();
            LoadImage();
        }

        private void MenuItem_Click_CameraCalibration(object sender, RoutedEventArgs e)
        {
            // 相机标定窗口
            CameraCalibrationWindow cameraCalibrationWindow = new CameraCalibrationWindow();
            cameraCalibrationWindow.Show();
        }

        private void MenuItem_Click_ParameterSettings(object sender, RoutedEventArgs e)
        {
            // 参数设置窗口
            ParameterSettingsWindow parameterSettingsWindow = new ParameterSettingsWindow();
            parameterSettingsWindow.Show();
        }

        private void MenuItem_Click_OfflineDetection(object sender, RoutedEventArgs e)
        {
            // 离线检测窗口
            OfflineDetectionWindow offlineDetectionWindow = new OfflineDetectionWindow();
            offlineDetectionWindow.Show();
        }

        private void MenuItem_Click_CommunicationSettings(object sender, RoutedEventArgs e)
        {
            // 通讯设置窗口
            CommunicationSettingsWindow communicationSettingsWindow = new CommunicationSettingsWindow();
            communicationSettingsWindow.Show();
        }

        private void LoadImage()
        {
            try
            {
                // 确保图像文件路径正确
                string imagePath = System.IO.Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "download.jpg");
                if (!System.IO.File.Exists(imagePath))
                {
                    MessageBox.Show("图像文件不存在：" + imagePath);
                    return;
                }

                // 加载本地图像
                BitmapImage bitmap = new BitmapImage(new Uri(imagePath, UriKind.Absolute));
                ImageDisplay.Source = bitmap;
            }
            catch (Exception ex)
            {
                MessageBox.Show("加载图像时出错：" + ex.Message);
            }
        }

        private void StartButton_Click(object sender, RoutedEventArgs e)
        {
            // 获取图像数据
            BitmapSource bitmapSource = (BitmapSource)ImageDisplay.Source;
            int width = bitmapSource.PixelWidth;
            int height = bitmapSource.PixelHeight;
            int stride = width * ((bitmapSource.Format.BitsPerPixel + 7) / 8);
            byte[] pixelData = new byte[height * stride];
            bitmapSource.CopyPixels(pixelData, stride, 0);

            // 锁定非托管内存
            IntPtr unmanagedPointer = Marshal.AllocHGlobal(pixelData.Length);
            Marshal.Copy(pixelData, 0, unmanagedPointer, pixelData.Length);

            // 调用 DLL 函数
            ProcessImage(unmanagedPointer, width, height, stride);

            // 释放非托管内存
            Marshal.FreeHGlobal(unmanagedPointer);

            MessageBox.Show("图像数据已传递给动态链接库进行处理。");
        }
    }
}
