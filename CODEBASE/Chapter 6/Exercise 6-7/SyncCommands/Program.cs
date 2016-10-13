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
        private static void GetReaderTime(string readerName)
        {
            DeviceManagerProxy dmp = new DeviceManagerProxy();
            PropertyKey key = new PropertyKey("General", "Time");
            Command myCmd = new GetPropertyCommand(key);
            Command resp = dmp.ExecuteDedicatedCommand(readerName, myCmd);
            if (resp != null && resp is GetPropertyCommand)
            {
                GetPropertyResponse response =
                (resp as GetPropertyCommand).Response;
                object respValue = response.Property.PropertyValue;
                DateTime readerTime = (DateTime)respValue;

                Console.WriteLine(String.Format("Time on reader is: {0} {1}",
                    readerTime.ToLongDateString(), readerTime.ToLongTimeString()));
            }
        }
      
        static void Main(string[] args)
        {
            GetReaderTime("Sample Reader");
        }

    }
}
