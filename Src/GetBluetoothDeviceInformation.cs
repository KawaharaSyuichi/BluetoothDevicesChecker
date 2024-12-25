using System;
using System.Diagnostics;
using Windows.Devices.Bluetooth;
using Windows.Devices.Bluetooth.GenericAttributeProfile;
using Windows.Devices.Enumeration;
using Windows.Foundation;
using Windows.Networking;

namespace BluetoothDevicesChecker
{
    //Bluetoothデバイスの情報を保存する構造体
    public struct BluetoothDeviceInfo
    {
        public string DeviceName;
        public string HostName;
    }

    // Get Bluetooth Device HostName
    public class BluetoothBatteryService
    {
        public async Task<BluetoothDeviceInfo[]> GetBluetoothDeviceInformation()
        {
            List<BluetoothDeviceInfo> deviceInfoList = new List<BluetoothDeviceInfo>();

            // Bluetoothデバイスのセレクタを取得
            string selector = BluetoothDevice.GetDeviceSelector();

            // Bluetoothデバイスの情報を取得
            DeviceInformationCollection devices = await DeviceInformation.FindAllAsync(selector);

            foreach (var device in devices)
            {
                Debug.WriteLine($"Device Id: {device.Id}, Name: {device.Name}");
                deviceInfoList.Add(new BluetoothDeviceInfo
                {
                    DeviceName = device.Name,
                    HostName = device.Id
                });
            }

            return deviceInfoList.ToArray();
        }
    }
}