using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.SensorServices.Rfid.Management;
using Microsoft.SensorServices.Rfid.Design;
using System.IO.SensorServices.Rfid.Client;

namespace Exercise8_1
{
    class Program
    {
        static void Main(string[] args)
        {
            DumpProcessList();
        }

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

        static public void DumpProcessList()
        {
            // Create a process manager proxy
            ProcessManagerProxy pmp = new ProcessManagerProxy();

            // Obtain the list of process names
            string[] processNames = pmp.GetAllProcesses();

            foreach (string procName in processNames)
            {
                Console.WriteLine("Process {0}:\r\n--------------------------");

                DumpLogicalDevices(pmp, procName);
                DumpEventHandlers(pmp, procName);
            }
        }

        static public void DumpLogicalDevices(ProcessManagerProxy pmp, 
            string procName)
        {
            // Get detailed information on this process
            RfidProcess process = pmp.GetProcess(procName);

            // Obtain the list of logical devices
            ICollection<LogicalDevice> logicalDevices =
                process.GetAllLogicalDevices();
            Console.Write("Logical devices: ");

            foreach (LogicalDevice d in logicalDevices)
                Console.Write(" {0},", d.Name);
            Console.WriteLine("");
        }

        static public void DumpEventHandlers(ProcessManagerProxy pmp, 
            string procName)
        {
            // Get detailed information on this process
            RfidProcess process = pmp.GetProcess(procName);

            // Obtain the list of event handlers
            ICollection<EventHandlerDefinition> eventHandlers =
                process.GetAllEventHandlers();
            Console.Write("Event Handlers: ");

            foreach (EventHandlerDefinition eh in eventHandlers)
                Console.Write(" {0}, ", eh.ComponentName);
            Console.WriteLine("");
        }

    }
}
