using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using Microsoft.SensorServices.Rfid.Management;
using Microsoft.SensorServices.Rfid.Design;
using Microsoft.SensorServices.Rfid;
using System.IO.SensorServices.Rfid.Client;

namespace Exercise6_11
{
    public partial class Form1 : Form
    {
        private ProcessManagerProxy processProxy;
        
        public Form1()
        {
            InitializeComponent();
        }

        private void cbProcessList_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (cbProcessList.SelectedIndex > -1)
            {
                string procName = this.cbProcessList.SelectedItem.ToString();
                this.cbLogicalDevices.Items.Clear();
                this.cbLogicalDevices.Items.AddRange(
                    this.logicalDevices[procName].ToArray<string>());

                if (cbLogicalDevices.Items.Count > 0)
                    this.cbLogicalDevices.SelectedIndex = 0;
            }
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            // Initialize the proxy objects
            processProxy = new ProcessManagerProxy();

            // Load the initial process and logical device lists
            RefreshProcessLists();
        }

        private string[] processNames;
        private Dictionary<string, List<string>> logicalDevices
            = new Dictionary<string,List<string>>();

        /// <summary>
        /// Load a list of currently registered RFID processes along with their
        /// logical devices
        /// </summary>
        private void RefreshProcessLists()
        {
            ShowMessage("Retrieving process list..");            
            processNames = processProxy.GetAllProcesses();
            if (processNames == null)
                processNames = new string[0];

            ShowMessage("Retrieving logical devices..");
            logicalDevices.Clear();
            foreach (string processName in processNames)
            {
                List<string> devices = new List<string>();
                RfidProcess processInfo = processProxy.GetProcess(processName);

                foreach (LogicalDevice d in processInfo.GetAllLogicalDevices())
                    devices.Add(d.Name);

                logicalDevices.Add(processName, devices);
            }

            // Update the ComboBox control containing the process list
            cbProcessList.Items.Clear();
            cbProcessList.Items.AddRange(processNames);
            cbProcessList.SelectedIndex = -1;

            // Clear the logical device list (this is updated when the selected
            // index changes for the process list)
            cbLogicalDevices.Items.Clear();
        }

        private void btnRefresh_Click(object sender, EventArgs e)
        {
            // Refresh the process and logical device lists
            RefreshProcessLists();
        }

        private void ShowMessage(string msg)
        {
            this.rtbStatus.AppendText(msg);
            this.rtbStatus.AppendText("\r\n");
        }

        private void ShowMessage(string fmt, params object[] vars)
        {
            ShowMessage(string.Format(fmt, vars));
        }

        private void btnSendTags_Click(object sender, EventArgs e)
        {
            // Must have a process selected
            if (this.cbProcessList.SelectedIndex == -1)
            {
                ShowMessage("Must select a process before sending tags");
                return;
            }
            string processName = this.cbProcessList.SelectedItem.ToString();

            // Must have a logical device selected
            if (this.cbLogicalDevices.SelectedIndex == -1)
            {
                ShowMessage("Must select a logical device before sending tags");
                return;
            }
            string logicalDevice = this.cbLogicalDevices.SelectedItem.ToString();

            // Generate a set of tag read events, 
            List<TagReadEvent> tagEvents = 
                GenerateTagEvents((int)this.numTagCount.Value);

            // If sending as a tag list, package them up and send to the server
            if (rbTagList.Checked)
            {
                try
                {
                    ShowMessage("Sending {0} events to server as TagListEvent",
                        this.numTagCount.Value);
                    TagListEvent tle = new TagListEvent(tagEvents, "SIMULATED");
                    tle.DeviceName = logicalDevice;
                    tle.Time = DateTime.Now;
                    
                    processProxy.AddEventToProcessPipeline(processName, tle,
                        logicalDevice);
                    ShowMessage("Event delivered");
                }
                catch (Exception ex0)
                {
                    ShowMessage("Could not send tags:\r\n{0}", ex0.ToString());
                }
            }
            else
            {
                ShowMessage("Sending {0} events to server as TagListEvent", 
                    this.numTagCount.Value);
                int sent = 0;
                foreach (TagReadEvent tre in tagEvents)
                {
                    processProxy.AddEventToProcessPipeline(processName, 
                        tre, logicalDevice);
                    
                    
                    ShowMessage("Sent event {0} of {1}", ++sent, 
                        this.numTagCount.Value);
                }
            }
        }

        private List<TagReadEvent> GenerateTagEvents(int count)
        {
            string logicalDevice = this.cbLogicalDevices.SelectedItem.ToString();

            // Generate a set of count TagReadEvent objects, each with a random
            // 96-bit tag ID, and a timestamp value of DateTime.Now.
            List<TagReadEvent> ret = new List<TagReadEvent>();
            System.Random rand = new Random();

            for (int i = 0; i < count; i++)
            {
                byte[] data = new byte[12]; // 96 bit / 8 bits / byte = 12 bytes
                rand.NextBytes(data);

                TagReadEvent tre = new TagReadEvent(data, TagType.EpcClass1Gen2,
                    null, "SIMULATED", DateTime.Now, null, TagDataSelector.All);
                tre.DeviceName = logicalDevice;
                ret.Add(tre);
            }
            return ret;
        }
    }
}
