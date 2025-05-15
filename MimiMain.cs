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
        String[] MemoryValues = new string[40]; //stores the raw memory values
        String[] MoboValues = new string[40]; //stores the raw motherboard values
        String[] GPUValues = new string[40]; //stores the raw GPU values
        String currentUserInformation = ""; //Login name, computer name and domain of USER.
        String currentCPUInformation = ""; //Name of processor, speed etc
        String currentDiskInformation = ""; //Information about current main disk
        int diskUsageTotalForProgressBar = 0; //percentage of disk usage as an int, so use for a visual progress bar.
        String currentMemoryInformation = ""; //Details about installed memory.
        String currentGPUInformation = ""; //Details about video drivers.
        String currentMoboInformation = ""; //details about current motherboard.

        public MimiNoseMain()
        {
            InitializeComponent();
            StartupSequence(); //start the program
        }

        private async Task StartupSequence()
        {
            
            await writeLineSlowly("Loading...", InformationBox1);
            //await boxAnimationLoading(); //Add this in after
            await getUserInfo(); //Get login details
            await writeLineSlowly(currentUserInformation, InformationBox1); //write login details to top label
            // await SystemInformationParserFULL("Win32_Processor"); //TEST
            await getCPUInformation(); //load Cpu information values
            await writeLineSlowly(currentCPUInformation, CpuInfoBox); //write CPU values to second label
            await getDiskInfo(); //load disk information
            await writeLineSlowly(currentDiskInformation, DiskInfo); //write disk values to next label
            await displayDiskProgress(diskUsageTotalForProgressBar); //display progress bar, increment to show total space used on disk.
            await getMemoryInformation(); //get information about RAM
            await writeLineSlowly(currentMemoryInformation, MemoryInfoBox);
            await getGPUInformation(); //get information about GPU
            await writeLineSlowly(currentGPUInformation, GPUInfoBox);
            await getMoboInformation(); //get information about motherboard.

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
            DiskSpaceUsedBar.Visible = true;
        }

        private async Task getMemoryInformation() //Searches for the current CPU information and stores it in the currentCPUInformation string.
        {
            int parsedRowCounter = 0; //Counter for tallying up each row of the system properties
            String[] parsedInfo = new string[200]; //new array to fit all of the parsed system info into
            ManagementObjectSearcher searcher = new ManagementObjectSearcher("select * from " + "Win32_PhysicalMemory");
            foreach (ManagementObject row in searcher.Get())
            {
                foreach (PropertyData property in row.Properties)
                {
                    if (parsedRowCounter < 31)
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
                            MemoryValues[parsedRowCounter] = parsedInfo[parsedRowCounter];
                            parsedRowCounter++;
                        }
                    }

                }

            }
            decimal memorySize = decimal.Parse(MemoryValues[2]); //get total bytes of memory
            memorySize = memorySize / 1000000000; //convert to GB
            memorySize = Math.Round(memorySize, 1); //Round to 1 decimal x.xGB
            string ramCompany = MemoryValues[15];
            string ramModel = MemoryValues[22];
            string ramSpeed = MemoryValues[30];
            currentMemoryInformation = $"Installed Memory: {ramCompany} brand RAM, model {ramModel}| {memorySize}GB clocked at {ramSpeed}MHz.";
            //*values wanted: 2. Size in bytes (8589934592)
            //                15. Manufacturer (Saumsung)
            //                22. Model (M378A1K43EB2-CWE)
            //                30. Memory Speed in Mhz (3200)
        }

        private async Task getGPUInformation() //Searches for the current CPU information and stores it in the currentCPUInformation string.
        {
            int parsedRowCounter = 0; //Counter for tallying up each row of the system properties
            String[] parsedInfo = new string[200]; //new array to fit all of the parsed system info into
            ManagementObjectSearcher searcher = new ManagementObjectSearcher("select * from " + "Win32_VideoController");
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
                            GPUValues[parsedRowCounter] = parsedInfo[parsedRowCounter];
                            parsedRowCounter++;
                        }
                    }

                }

            }
            String horizontalRes = GPUValues[12];
            String verticalRes = GPUValues[18];
            String GPUName = GPUValues[6];
            currentGPUInformation = $"GPU: {GPUName} @ {horizontalRes}x{verticalRes}";
            //*values wanted: 
            //               6. Graphics hardware name (Intel (R) UHD Graphics 630)
            //               12. Horizontal resolution  (1920)
            //               18. Vertical Resolution (1080)
        }

        private async Task getMoboInformation() //Searches for the current CPU information and stores it in the currentCPUInformation string.
        {
            int parsedRowCounter = 0; //Counter for tallying up each row of the system properties
            String[] parsedInfo = new string[200]; //new array to fit all of the parsed system info into
            ManagementObjectSearcher searcher = new ManagementObjectSearcher("select * from " + "Win32_BaseBoard");
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
                            MoboValues[parsedRowCounter] = parsedInfo[parsedRowCounter];
                            TestBox.Text = TestBox.Text + $"{parsedRowCounter}. {MoboValues[parsedRowCounter]}";
                            parsedRowCounter++;
                        }
                    }

                }

            }

            currentMoboInformation = $"";
            //*values wanted: 
            //               
            //               
            //               
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
