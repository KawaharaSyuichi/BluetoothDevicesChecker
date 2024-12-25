using System.Text;
using System.Transactions;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using Windows.Devices.Bluetooth;
using Windows.Devices.Bluetooth.GenericAttributeProfile;
using Windows.Devices.Enumeration;
using Windows.Storage.Streams;
using System.Collections.ObjectModel;

namespace BluetoothDevicesChecker
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {

        public MainWindow()
        {
            InitializeComponent();

            BluetoothBatteryService batteryService = new BluetoothBatteryService();
            var bluetoothDeviceInfoArrayTask = batteryService.GetBluetoothDeviceInformation();
            bluetoothDeviceInfoArrayTask.ContinueWith(task =>
            {
                var bluetoothDeviceInfoArray = task.Result;
                Dispatcher.Invoke(() => InitializeButtonPanel(bluetoothDeviceInfoArray));
                Dispatcher.Invoke(() => InitializeBluetoothDeiceInfoPanel(bluetoothDeviceInfoArray));
            });
        }

        private void InitializeButtonPanel(BluetoothDeviceInfo[] bluetoothDeviceInfo)
        {
            for (int i = 0; i < bluetoothDeviceInfo.Length; i++)
            {
                Button button = new Button
                {
                    Width = 100,
                    Content = $"{bluetoothDeviceInfo[i].DeviceName}",
                    Margin = new Thickness(0)
                };

                ButtonPanel.Children.Add(button);
            }
        }

        private void InitializeBluetoothDeiceInfoPanel(BluetoothDeviceInfo[] bluetoothDeviceInfo) 
        {
            for (int i = 0; i < bluetoothDeviceInfo.Length; i++)
            {
                TextBlock textBlock = new TextBlock
                {
                    Text = $"Host Name: {bluetoothDeviceInfo[i].HostName}",
                    Margin = new Thickness(0),
                    FontSize = 24
                };

                BluetoothDeiceInfoPanel.Children.Add(textBlock);
            }
        }
    }
}