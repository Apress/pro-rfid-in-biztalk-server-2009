using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Microsoft.SensorServices.Rfid.Management;
using Microsoft.SensorServices.Rfid.Design;
using Microsoft.SensorServices.Rfid.Utilities;
using System.IO.SensorServices.Rfid.Client;
using System.Collections.ObjectModel;
using Microsoft.SensorServices.Rfid;

namespace SyncSample2
{
    class Program
    {
        private static string deviceName;

        public static void DumpDeviceList()
        {
            DeviceManagerProxy dmp = new DeviceManagerProxy();
            DeviceDefinition[] devices = dmp.GetAllDevices();

            foreach (DeviceDefinition device in devices)
            {
                Console.WriteLine("Device {0}: {1}",
                    device.Name, device.DeviceInformation);
                deviceName = device.Name;
            }
        }

        static public void ExecuteGetTags(string deviceName)
        {
            GetTagsCommand cmd = new GetTagsCommand(null);
            DeviceManagerProxy dmp = new DeviceManagerProxy();
            Command c = dmp.ExecuteDedicatedCommand(deviceName, cmd);
            cmd = c as GetTagsCommand;

            if (cmd != null && cmd.Response != null)
            {
                GetTagsResponse response = cmd.Response;
                ReadOnlyCollection<TagReadEvent> tags = response.Tags;
                Console.WriteLine("Tags Read:\r\n-----------------");

                foreach (TagReadEvent tre in tags)
                {
                    Console.WriteLine("Tag {0} from source {1}",
                    HexUtilities.HexEncode(tre.GetId()), tre.Source);
                }
            }
        }

        static void Main(string[] args)
        {
            DumpDeviceList();
            ExecuteGetTags(deviceName);
        }

    }
}
