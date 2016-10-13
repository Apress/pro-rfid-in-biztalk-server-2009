using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Management;

namespace WmiListener
{
    class Program
    {
        static void Main(string[] args)
        {
            // Create an event query to capture all ManagementEvent events
            WqlEventQuery query = new WqlEventQuery(
                "SELECT * from ManagementEvent");

            // Within the root\Microsoft\Rfid namespace
            ManagementScope scope = new ManagementScope(
                @"root\Microsoft\Rfid");

            // Watch every 1 ms in this query and scope
            ManagementEventWatcher watcher = new ManagementEventWatcher(
                scope, query);
            EventWatcherOptions options = new EventWatcherOptions();
            options.Timeout = new TimeSpan(0, 0, 0, 0, 1);
            watcher.Options = options;

            // Route events to the watcher_EventArrived method
            watcher.EventArrived += new EventArrivedEventHandler(
                watcher_EventArrived);

            // Start listening for events
            watcher.Start();

            // Loop until the <Enter> key is pressed
            Console.WriteLine("Press the [Enter] key to exit the application");
            Console.ReadLine();

            watcher.Stop();
        }

        static void watcher_EventArrived(object sender,
            EventArrivedEventArgs e)
        {
            Console.WriteLine("Event is " + e.NewEvent.ToString());
            Console.WriteLine("Event type is " +
                 e.NewEvent.ClassPath.ToString());
            foreach (PropertyData o in e.NewEvent.SystemProperties)
            {
                Console.WriteLine("{0}:{1} ({2})", o.Name, o.Value, o.Type);
            }

            foreach (PropertyData o in e.NewEvent.Properties)
            {
                Console.WriteLine("{0}:{1} ({2})", o.Name, o.Value, o.Type);
            }
        }

    }


}
