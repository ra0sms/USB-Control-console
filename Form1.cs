using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO.Ports;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;


namespace USBSwitch
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
            
        }

        private void button1_Click(object sender, EventArgs e)
        {
            
            if (!serialPort1.IsOpen)
            {
                try
                {
                    String name = ((string)comboBox1.SelectedItem);
                    serialPort1 = new SerialPort(name, 9600, System.IO.Ports.Parity.None, 8, StopBits.One);
                    serialPort1.Open();
                    button1.Text = "Close";
                    return;
                }
                catch { MessageBox.Show("Please select COM port from list"); } 
            }
            if (serialPort1.IsOpen)
            {
                button1.Text = "Open";
                serialPort1.WriteLine("AM10000000000000000\r\n");
                serialPort1.Close();
                return;
            } 
        }

      
        private void button2_Click(object sender, EventArgs e)
        {
            if (serialPort1.IsOpen) serialPort1.WriteLine("AM11111100000000000\r\n");
            else MessageBox.Show("COM port is close!");
        }

        private void Form1_FormClosing(object sender, FormClosingEventArgs e)
        {
            serialPort1.Close();
            Properties.Settings.Default.comport = ((string)comboBox1.SelectedItem);
            Properties.Settings.Default.portindex = comboBox1.SelectedIndex;
            Properties.Settings.Default.Save();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            comboBox1.Items.Clear();
            foreach (string portName in SerialPort.GetPortNames())
            {
                comboBox1.Items.Add(portName);
            }
            comboBox1.SelectedItem= Properties.Settings.Default.comport;
            comboBox1.SelectedIndex = Properties.Settings.Default.portindex;
        }
    }
}
