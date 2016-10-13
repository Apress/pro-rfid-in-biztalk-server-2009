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
        public static void ExecuteWriteId(string readerName, byte[] id)
        {
            WriteIdCommand cmd = new WriteIdCommand(null, id, null, null);
            DeviceManagerProxy dmp = new DeviceManagerProxy();
            dmp.ExecuteDedicatedCommand(readerName, cmd);
        }
      
        static void Main(string[] args)
        {
            ExecuteWriteId("Sample Reader", new byte[] { 0x10, 0x20, 0x30, 0x40 });
        }

    }
}
