using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.SensorServices.Rfid.Design;
using Microsoft.SensorServices.Rfid.Management;
using Microsoft.SensorServices.Rfid.ProcessComponents;

namespace CustomEventHandler
{
    class ProcessCreator
    {
        public static void CreateProcess()
        {
            //create a new process called Exercise1
            RfidProcess process = new RfidProcess();
            process.Name = "Exercise1";

            //add a logical device called ExitGateReader
            process.LogicalSource.LogicalDeviceList.Add(
                new LogicalDevice("My first logical device", 
                "The device used in exercise 1"));

            //define the SqlServerSink event handler commponent
            string assemblyName = typeof (SqlServerSink).Assembly.FullName;
            string typeName = typeof (SqlServerSink).FullName;
            EventHandlerDefinition postToSqlServer =
                new EventHandlerDefinition("Post to Db", assemblyName, typeName);

            //add the definition to the event processing pipeline for 
            //the logical source
            process.LogicalSource.ComponentList.Add(postToSqlServer);

            //save the process
            ProcessManagerProxy pmp = new ProcessManagerProxy();
            pmp.SaveProcess(process);
        }
    }
}
