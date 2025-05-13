using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Management;
using Microsoft.Win32;

namespace MimiNose
{
    public partial class MimiNoseMain : Form
    {

        //Global Variables for multiple uses:
        bool isCurrentTaskFinished = true;
        

        public MimiNoseMain()
        {
            InitializeComponent();
            StartupSequence(); //start the program
        }

        private async Task StartupSequence()
        {
            await writeLineSlowly("Loading...", InformationBox1);
            getSystemInformation();
        }

        private async Task writeLineSlowly(string currentMessage, Label whereToOutPut) //Creates typewriter effect, usage is string to output, location to output
        {
            int textSpeedDelayTime = (20);
            string currentStackOfChars = "";
            for (int i = 0; i < currentMessage.Length; i++)
            {
                currentStackOfChars = currentStackOfChars + (currentMessage[i]);
                whereToOutPut.Text = currentStackOfChars;
                await Task.Delay(textSpeedDelayTime);
            }
            //make sure to always await this or it will write stuff over itself.
        }







        //--test for which information I can extract from system--//
        private async Task systemInformationTest() //reference guide for environment functions.
        {
            //TestBox.Text = (TestBox.Text + $" {Environment.CurrentDirectory}"); //prints C:/Users/KendallP/PIctures/MimiNose/Files/MimiNose/bin/debug
            //TestBox.Text = (TestBox.Text + $" {Environment.MachineName}");  //prints DESKTOP-D2DH3AQ
            //TestBox.Text = (TestBox.Text + $" {Environment.UserDomainName}"); //prints EG
            //TestBox.Text = (TestBox.Text + $" {Environment.UserName}"); //prints Kendallp
            //TestBox.Text = (TestBox.Text + $" {Environment.SystemPageSize.ToString()}"); //prints 4096
            //TestBox.Text = (TestBox.Text + $" {Environment.OSVersion.ToString()}"); //prints Microsoft Windows NT 6.2.9200.0
            //TestBox.Text = (TestBox.Text + $" {Environment.ProcessorCount.ToString()}"); //prints 16
            //TestBox.Text = (TestBox.Text + $" {String.Join(", ", Environment.GetLogicalDrives())}"); //prints C:Y, D:Y, S:Y, W:Y, Y:Y, Z:Y
            //TestBox.Text = (TestBox.Text + $" {System.Management.}"); //prints 16
        }
        //--End of code, use as reference when finished--//

        //---Method that obtains the system information of the computer through WMI database querying---\\
        public void getSystemInformation()
        {
            int parsedRowCounter = 0; //Counter for tallying up each row of the system properties
            String[] parsedInfo = new string[40]; //new array to fit all of the parsed system info into
            ManagementObjectSearcher searcher = new ManagementObjectSearcher("select * from " + "Win32_Processor");
            foreach (ManagementObject row in searcher.Get())
            {
                foreach (PropertyData property in row.Properties)
                {
                    try //have to catch null value returns as this version of C# can't have nullable strings...
                    {
                        parsedInfo[parsedRowCounter] = property.Value.ToString(); //current row of win32_processor is turned into a string
                    }
                    catch (Exception nullvalue)
                    {
                        parsedInfo[parsedRowCounter] = " "; //if it's null just make it blank.
                    }

                    TestBox.Text = TestBox.Text + parsedInfo[parsedRowCounter];
                    parsedRowCounter++;
                }

            }
            
        }







        //---Code that manages clicking and dragging the window despite not having actual OS borders---\\
        private bool mouseDown;
        private Point lastLocation;
        private void os9_MouseDown(object sender, MouseEventArgs e)
        {
            mouseDown = true;
            lastLocation = e.Location;
        }
        private void os9_MouseMove(object sender, MouseEventArgs e)
        {
            if (mouseDown)
            {
                this.Location = new Point(
                    (this.Location.X - lastLocation.X) + e.X, (this.Location.Y - lastLocation.Y) + e.Y);

                this.Update();
            }
        }
        private void os9_MouseUp(object sender, MouseEventArgs e)
        {
            mouseDown = false;
        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {

        }
        //---End of mouse clicking code section---\\
    }
}
