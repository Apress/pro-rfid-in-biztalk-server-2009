using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Microsoft.SensorServices.Rfid.Management;
using Microsoft.SensorServices.Rfid.Design;
using Microsoft.SensorServices.Rfid.Dspi;
using System.IO.SensorServices.Rfid.Client;

namespace SyncSample2
{
    class Program
    {
        public static void ExecuteCustomCommand(string readerName)
        {
            VendorSpecificInformation parms = new VendorSpecificInformation();
            parms.Add("Port", "o1");
            parms.Add("TimeInterval", 5000);

            VendorDefinedCommand cmd = new VendorDefinedCommand(
                null, "PulseOutputCommand", null, parms);

            DeviceManagerProxy dmp = new DeviceManagerProxy();
            Command resp = dmp.ExecuteDedicatedCommand(readerName, cmd);
        }

        static void Main(string[] args)
        {
            ExecuteCustomCommand("Alien Reader");
        }
    }
}
