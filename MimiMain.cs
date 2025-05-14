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
        String[] CPUInformation = new string[40]; //stores the raw CPU values
        String[] DiskValues = new string[40]; //stores the raw disk values
        String currentUserInformation = ""; //Login name, computer name and domain of USER.
        String currentCPUInformation = ""; //Name of processor, speed etc
        String currentDiskInformation = ""; //Information about current main disk
        int diskUsageTotalForProgressBar = 0; //percentage of disk usage as an int, so use for a visual progress bar.

        public MimiNoseMain()
        {
            InitializeComponent();
            StartupSequence(); //start the program
        }

        private async Task StartupSequence()
        {
            
            await writeLineSlowly("Loading...", InformationBox1);
            //await boxAnimationLoading();
            await getUserInfo();
            await writeLineSlowly(currentUserInformation, InformationBox1);
            // await SystemInformationParserFULL("Win32_Processor");
            await getCPUInformation();
            await writeLineSlowly(currentCPUInformation, CpuInfoBox);
            await getDiskInfo();
            await writeLineSlowly(currentDiskInformation, DiskInfo);
            await displayDiskProgress(diskUsageTotalForProgressBar);
        }

        private async Task printCPUinformation()
        {
            for (int i = 0; i < 40; i++)
            {
                TestBox.Text = TestBox.Text + $"{i}. {CPUInformation[i]} ";
            }
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




        private async Task getUserInfo() //Loads the username, machine name and login domain of the user, store it as "currentUserInformation".
        {
            String currentUserName = Environment.UserName;
            String currentMachineName = Environment.MachineName;
            String currentDomainLogin = Environment.UserDomainName;
            currentUserInformation = $"User: {currentUserName} @ {currentMachineName} / {currentDomainLogin}";
        }

        private async Task getCPUInformation() //Searches for the current CPU information and stores it in the currentCPUInformation string.
        {
            int parsedRowCounter = 0; //Counter for tallying up each row of the system properties
            String[] parsedInfo = new string[200]; //new array to fit all of the parsed system info into
            ManagementObjectSearcher searcher = new ManagementObjectSearcher("select * from " + "Win32_Processor");
            foreach (ManagementObject row in searcher.Get())
            {
                foreach (PropertyData property in row.Properties)
                {
                    if (parsedRowCounter < 30)
                    {


                        try //have to catch null value returns as this version of C# can't have nullable strings...
                        {
                            parsedInfo[parsedRowCounter] = property.Value.ToString() as String; //current row of win32_processor is turned into a string
                        }
                        catch (Exception nullvalue)
                        {
                            parsedInfo[parsedRowCounter] = " "; //if it's null just make it blank.
                        }
                        finally
                        {
                            CPUInformation[parsedRowCounter] = parsedInfo[parsedRowCounter];
                            parsedRowCounter++;
                        }
                    }
                    
                }

            }
            currentCPUInformation = $"CPU: {CPUInformation[29]}.{Environment.NewLine}CPU Type: {CPUInformation[4]}.";
            //*values wanted: 4. Intel64 Family 6 Model 165 Stepping 5 (Architecture model)
            //                29. Intel(R) Core(TM) i7-10700 CPU @ 2.90GHz
        }

        private async Task getDiskInfo() //Searches for the current CPU information and stores it in the currentCPUInformation string.
        {
            int parsedRowCounter = 0; //Counter for tallying up each row of the system properties
            String[] parsedInfo = new string[200]; //new array to fit all of the parsed system info into
            ManagementObjectSearcher searcher = new ManagementObjectSearcher("select * from " + "Win32_LogicalDisk");
            foreach (ManagementObject row in searcher.Get())
            {
                foreach (PropertyData property in row.Properties)
                {
                    if (parsedRowCounter < 31) //Only searches up to the highest value required - if left to keep parsing null values it will break the program.
                    {


                        try //have to catch null value returns as this version of C# can't have nullable strings...
                        {
                            parsedInfo[parsedRowCounter] = property.Value.ToString() as String; //current row of win32_LogicalDisk is turned into a string
                        }
                        catch (Exception nullvalue)
                        {
                            parsedInfo[parsedRowCounter] = " "; //if it's null just make it blank.
                        }
                        finally
                        {
                            DiskValues[parsedRowCounter] = parsedInfo[parsedRowCounter];
                           
                            parsedRowCounter++;
                        }
                    }
                }
            }
            //--Calculations for disk info--\\
            decimal TotalSpaceParsed = decimal.Parse(DiskValues[30]); //Turns string into decimal
            TotalSpaceParsed = TotalSpaceParsed / 1000000000; //Convert from bytes to GB
            TotalSpaceParsed = Math.Round(TotalSpaceParsed, 2); //Round to two decimal places x.xxGB
            decimal freeSpaceParsed = decimal.Parse(DiskValues[15]); //turns free space string into decimal
            freeSpaceParsed = freeSpaceParsed / 1000000000; //Convert from bytes to GB
            freeSpaceParsed = Math.Round(freeSpaceParsed, 2); //round to two decimal places x.xxGB
            decimal sizeUsedUp = TotalSpaceParsed - freeSpaceParsed; //How much of disk has data on it.
            decimal percentageUsedUp = (sizeUsedUp / TotalSpaceParsed) * 100; //calculate percentage of disk used.
            percentageUsedUp = Math.Round(percentageUsedUp, 2); //Round to two decimal places.
            diskUsageTotalForProgressBar = (int) percentageUsedUp; //Stores value of percentage without any decimal places for the progress bar.
            sizeUsedUp = Math.Round(sizeUsedUp, 2); //convert space used to 2 decimal places
            currentDiskInformation = $"Local Disk {DiskValues[20]} | {DiskValues[14]} Format | {TotalSpaceParsed}GB Total{Environment.NewLine}" +
                                     $"{sizeUsedUp}GB Used / {percentageUsedUp}%";

            //*values wanted: 20. Drive Letter (e.g C:)
            //                14. Format (NTFS)
            //                15. Free space in bits (106297098240)
            //                30. Total space (510770802688)
        }

        private async Task displayDiskProgress(int percentageTotal)
        {
            for (int i = 1; i <= percentageTotal; i++)
            {
                DiskSpaceUsedBar.PerformStep();
            }
        }


        //--test for which information I can extract from system--//
        // private async Task systemInformationTest() //reference guide for environment functions.
        //{
        //TestBox.Text = (TestBox.Text + $" {Environment.CurrentDirectory}"); //prints C:/Users/KendallP/PIctures/MimiNose/Files/MimiNose/bin/debug
        //TestBox.Text = (TestBox.Text + $" {Environment.MachineName}");  //prints DESKTOP-D2DH3AQ
        //TestBox.Text = (TestBox.Text + $" {Environment.UserDomainName}"); //prints EG
        //TestBox.Text = (TestBox.Text + $" {Environment.UserName}"); //prints Kendallp
        //TestBox.Text = (TestBox.Text + $" {Environment.SystemPageSize.ToString()}"); //prints 4096
        //TestBox.Text = (TestBox.Text + $" {Environment.OSVersion.ToString()}"); //prints Microsoft Windows NT 6.2.9200.0
        //TestBox.Text = (TestBox.Text + $" {Environment.ProcessorCount.ToString()}"); //prints 16
        //TestBox.Text = (TestBox.Text + $" {String.Join(", ", Environment.GetLogicalDrives())}"); //prints C:Y, D:Y, S:Y, W:Y, Y:Y, Z:Y
        //TestBox.Text = (TestBox.Text + $" {System.Management.}"); //prints 16
        //}
        //--End of code, use as reference when finished--//



        //---Method that obtains the system information of the computer through WMI database querying---\\
        //--After parsing, use a new method to only obtain the information you want, as this one floods the program with null values that will break/not be used...--\\
        /*public async Task SystemInformationParserFULL(String Win32_key)
        {
            int parsedRowCounter = 0; //Counter for tallying up each row of the system properties
            String[] parsedInfo = new string[40]; //new array to fit all of the parsed system info into
            ManagementObjectSearcher searcher = new ManagementObjectSearcher("select * from " + Win32_key);
            foreach (ManagementObject row in searcher.Get())
            {
                foreach (PropertyData property in row.Properties)
                {
                    try //have to catch null value returns as this version of C# can't have nullable strings...
                    {
                        parsedInfo[parsedRowCounter] = property.Value.ToString() as String; //current row of win32_processor is turned into a string
                    }
                    catch (Exception nullvalue)
                    {
                        parsedInfo[parsedRowCounter] = " "; //if it's null just make it blank.
                    }
                    finally
                    {
                        CPUInformation[parsedRowCounter] = parsedInfo[parsedRowCounter];
                        TestBox.Text = (TestBox.Text + $"{parsedRowCounter}. {CPUInformation[parsedRowCounter]} ");
                        parsedRowCounter++;
                    }
                    //*values wanted: 4. Intel64 Family 6 Model 165 Stepping 5 (Architecture model)
                    //                29. Intel(R) Core(TM) i7-10700 CPU @ 2.90GHz.
                    
                }

            }
            
        } */

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
