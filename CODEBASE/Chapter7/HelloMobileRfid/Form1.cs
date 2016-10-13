using System;
using System.Linq;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

using Microsoft.SensorServices.Rfid.Management;
using System.IO.SensorServices.Rfid.Client;

using Microsoft.SensorServices.Rfid;
using Microsoft.SensorServices.Rfid.Utilities;

namespace HelloMobileRfid
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        // Shared DeviceConnection object – used to communicate with RFID module
        private DeviceConnection connection;

        private void btnOpen_Click(object sender, EventArgs e)
        {
            // Create a device manager proxy to obtain the device list
            DeviceManagerProxy proxy = new DeviceManagerProxy();

            // Obtain the name of the first (usually the only) device
            string deviceName = proxy.GetAllDevices()[0].Name;

            // Establish a connection to that device
            connection = new DeviceConnection(deviceName);
            connection.Open();

        }

        private void btnClose_Click(object sender, EventArgs e)
        {
            // If an active connection exists, close it
            if (connection != null)
            {
                connection.Close();
                connection = null;
            }

        }

        private void btnReadTag_Click(object sender, EventArgs e)
        {
            // Confirm the connection is available
            if (connection != null &&
                connection.ConnectionState == ClientConnectionState.Open)
            {
                // Read tags through the synchronous GetTags function and display
                // in the textbox
                foreach (TagReadEvent tre in connection.GetTags(TagDataSelector.All))
                {
                    string hex = HexUtilities.HexEncode(tre.GetId());
                    tbTag.Text = hex;
                    tbTag.Refresh();
                }
            }

        }
    }
}