using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Microsoft.SensorServices.Rfid.Management;
using Microsoft.SensorServices.Rfid.Design;

namespace SyncSample2
{
    class Program
    {
        public static void DumpDeviceList()
        {
            DeviceManagerProxy dmp = new DeviceManagerProxy();
            DeviceDefinition[] devices = dmp.GetAllDevices();

            foreach (DeviceDefinition device in devices)
            {
                Console.WriteLine("Device {0}: {1}",
                device.Name, device.DeviceInformation);
            }
        }

        static void Main(string[] args)
        {
            DumpDeviceList();
        }

    }
}
