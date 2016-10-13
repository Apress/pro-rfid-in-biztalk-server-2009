using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.SensorServices.Rfid;
using Microsoft.SensorServices.Rfid.Utilities;
using Microsoft.SensorServices.Rfid.Dspi;
using System.Messaging;
using System.Globalization;
using System.Xml;
using System.Runtime.Serialization;

namespace ProBiztalkRfid.EventHandlers.MSMQ
{
    public class HandlerMSMQ : RfidEventHandlerBase
    {
        /// <summary>
        /// Singleton object for exposing configuration metadata
        /// </summary>
        private static RfidEventHandlerMetadata metadata;

        /// <summary>
        /// When attempting to capture a log lock, wait no more than 100 ms
        /// </summary>
        private const int lockTimeout = 100;

        /// <summary>
        /// The timeout on attempting to acquire a queue
        /// </summary>
        private int queueTimeout = 5000;

        /// <summary>
        /// Private logging object; created from the RfidProcessContext logging object
        /// </summary>
        private ILogger log;

        /// <summary>
        /// Locking object for accessing the log variable
        /// </summary>
        private object logLock;

        /// <summary>
        /// The destination MSMQ queue to which we post events
        /// </summary>
        private System.Messaging.MessageQueue destinationQueue;

        /// <summary>
        /// The locking object for accessing the queue 
        /// </summary>
        private object queueLock;

        public override void Init(Dictionary<string, object> parameters, 
            RfidProcessContext container)
        {
            // Initialize the synchronization objects
            logLock = new object();
            queueLock = new object();

            // Initialize and configure the logging object to have the 
            // same logging level as the parent process
            string name = base.GetType().Name;
            this.log = RfidProcessContext.GetLogger(name);

            // Instantiate the log
            if (this.log != null)
            {
                this.log.CurrentLevel = RfidProcessContext.GetLogger(container.ProcessName).CurrentLevel;

                log.Info("Loading process component {0} version {1}",
                    this.GetType().Name, System.Reflection.Assembly.GetExecutingAssembly().GetName().Version.ToString());

                // One of the common challenges/problems during event handler development is not updating
                // the registered event handler.  We include the last modified time in the event handler 
                // log as a check to see if the modified time is changing (i.e. that we're executing the 
                // updated event handler during development).
                try
                {
                    System.IO.FileInfo info = new System.IO.FileInfo(System.Reflection.Assembly.GetExecutingAssembly().CodeBase);
                    DateTime lastMod = info.LastWriteTime;
                    LogMessage(Level.Verbose, "Assembly last modified at {0}", lastMod.ToLongTimeString());
                }
                catch (Exception)
                {
                    LogMessage(Level.Warning, "Could not extract assembly modification time");
                }
            }

            // Log the account name of the hosting process to assist in debugging if the queue is not accessible
            string userName = System.Security.Principal.WindowsIdentity.GetCurrent().Name;
            LogMessage(Level.Info, "EventHandler MSMQ Sender executing as user {0}", userName);
                

            // Process the configuration as passed in from the handler parameters.                        
            if (parameters != null)
            {
                if (parameters.ContainsKey("QueueName"))
                {
                    string queueName = parameters["QueueName"].ToString();

                    if (System.Messaging.MessageQueue.Exists(queueName))
                    {
                        destinationQueue = new System.Messaging.MessageQueue(queueName);
                        if (destinationQueue.CanWrite)
                        {
                            // Configure the formatter for the destination queue
                            destinationQueue.Formatter = new System.Messaging.XmlMessageFormatter();
                        }
                        else
                        {
                            throw new RfidException(String.Format("Could not open queue {0} for writing as user {1}", 
                                queueName, userName), "CannotWriteQueue", queueName);
                        }
                    }
                    else
                    {                        
                        throw new RfidException(String.Format("Could not find/access queue {0} as user {1}", 
                            queueName, userName), "CannotReadQueue", queueName);
                    }
                }
                else
                {
                    throw new RfidException("No queue name specified", "NoQueue");
                }
            }

            System.Diagnostics.Debugger.Launch();
        }
     
        /// <summary>
        /// TODO
        /// </summary>
        /// <param name="evt"></param>
        /// <returns></returns>
        [RfidEventHandlerMethod]
        public RfidEventBase PostEventToQueue(RfidEventBase evt)
        {
            if (evt == null)
                return evt;

            LogMessage(Level.Verbose, "Logging event {0} to queue {1}", evt.ToString(), destinationQueue.QueueName);
            PostEvent(evt);

            return evt;
        }

        /// <summary>
        /// TODO
        /// </summary>
        /// <param name="evts"></param>
        /// <returns></returns>
        [RfidEventHandlerMethod]
        public RfidEventBase[] PostEventsToQueue(RfidEventBase[] evts)
        {
            foreach (RfidEventBase evt in evts)
            {
                LogMessage(Level.Verbose, "Logging event {0} to queue {1}", evt.ToString(), destinationQueue.QueueName);
                PostEvent(evt);
            }
            return evts;
        }

        /// <summary>
        /// Method to synchronize posting events to the MSMQ.  The queue timeout is fairly
        /// high to account for posting to remote queues.
        /// </summary>
        /// <param name="evt"></param>
        private void PostEvent(RfidEventBase evt)
        {
            if (System.Threading.Monitor.TryEnter(queueLock, queueTimeout))
            {
                try
                {                    
                    destinationQueue.Send(SerializeEvent(evt), "RFID Event " + evt.);
                }
                catch (MessageQueueException ex0)
                {
                    LogMessage(Level.Warning, "received message queue exception: {0}", ex0.ToString());
                }
                catch (Exception ex1)
                {
                    LogMessage(Level.Error, "received exception on queue send: {0}", ex1.ToString());
                }
                finally
                {
                    System.Threading.Monitor.Exit(queueLock);
                }
            }
            else
            {
                throw new RfidException("Monitor timed out on posting event: " + evt.ToString());
            }
        }

        /// <summary>
        /// Wrapper object to manage access to the log variable
        /// </summary>
        /// <param name="level"></param>
        /// <param name="fmt"></param>
        /// <param name="vars"></param>
        private void LogMessage(Level level, string fmt, params object[] vars)
        {
            if (this.log != null)
            {
                if (System.Threading.Monitor.TryEnter(logLock, lockTimeout))
                {
                    try
                    {
                        log.Log(level, fmt, vars);
                    }
                    finally
                    {
                        System.Threading.Monitor.Exit(logLock);
                    }
                }
                else
                {
                    System.Diagnostics.Debug.WriteLine(                        
                        "Failed to log message - process component has locked logging object (msg: " +
                        String.Format(fmt, vars) + ")");
                }
            }
        }

        /// <summary>
        /// Given a generic RFID event, serialize it into a textual representation
        /// (most of the tag event types do not have a parameterless constructor and
        /// thus cannot be handled by the default serializer).
        /// </summary>
        /// <param name="evt"></param>
        /// <returns></returns>
        private object SerializeEvent(RfidEventBase evt)
        {
            if (evt == null)
                return null;

            System.IO.StringWriter sw = new System.IO.StringWriter(CultureInfo.InvariantCulture);
            XmlWriter writer = new XmlTextWriter(sw);

            writer.WriteStartElement("EventData");
            DataContractSerializer dcs = new DataContractSerializer(evt.GetType());
            dcs.WriteObject(writer, evt);

            return sw.ToString();            
        }


        public static RfidEventHandlerMetadata GetEventHandlerMetadata(bool getVendorExtensionsAlso)
        {
            if (metadata == null)
            {
                Dictionary<string, RfidEventHandlerParameterMetadata> metadict = 
                    new Dictionary<string, RfidEventHandlerParameterMetadata>();

                RfidEventHandlerParameterMetadata parm = new RfidEventHandlerParameterMetadata(
                    typeof(string),
                    "The name of the destination MSMQ queue.  This queue must be writable by the Rfid service account",
                    null, true);
                metadict.Add("QueueName", parm);

                metadata = new RfidEventHandlerMetadata("MSMQ Sender", metadict);
            }
            return metadata;
        }
    }
}
