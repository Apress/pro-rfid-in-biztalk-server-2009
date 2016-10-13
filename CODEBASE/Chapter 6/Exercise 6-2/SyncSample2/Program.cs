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

        static public void DumpLogicalDevices(ProcessManagerProxy pmp,
            string procName)
        {
            // Get detailed information on this process
            RfidProcess process = pmp.GetProcess(procName);

            // Obtain the list of logical devices
            ICollection<LogicalDevice> logicalDevices = process.GetAllLogicalDevices();

            Console.Write("Logical devices: ");

            foreach (LogicalDevice d in logicalDevices)
                Console.Write(" {0},", d.Name);

            Console.WriteLine("");
        }

        static void Main(string[] args)
        {
            DumpProcessList();
        }

    }
}
