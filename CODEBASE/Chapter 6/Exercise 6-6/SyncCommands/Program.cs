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
        static public void SetDigitalPort(string readerName,
            string source, byte[] val)
        {
            DeviceManagerProxy dmp = new DeviceManagerProxy();
            Guid connId = Guid.Empty;
            try
            {
                connId = dmp.OpenConnection(readerName);
                EntityProperty prop = new EntityProperty(
                new PropertyKey("Source", "Port Output Value"), val);
                Command mycmd = new SetPropertyCommand(prop);
                Command resp = dmp.ExecuteCommandForConnection(
                readerName, source, connId, mycmd);
            }
            finally
            {
                dmp.CloseConnection(readerName, connId);
            }
        }
      
        static void Main(string[] args)
        {
            byte[] off = new byte[] { 0 };
            SetDigitalPort("SampleReader", "GPO_1", off);
        }

    }
}
