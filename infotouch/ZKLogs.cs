using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace infotouch
{
    public static class ZKLogs
    {
        private static bool bIsConnected = false;
        private static int iMachineNumber = 1;
        public static zkemkeeper.CZKEMClass axCZKEM1 = new zkemkeeper.CZKEMClass();
        public static string currentState = "";
        public static string sConnect = "DisConnect";

        public static void ConnectLan(string Ip, string Port)
        {
            if (Ip.Trim() == "" || Port.Trim() == "")
            {
                MessageBox.Show("IP and Port cannot be null", "Error");
                return;
            }
            int idwErrorCode = 0;

            if (sConnect == "DisConnect")
            {
                axCZKEM1.Disconnect();
                bIsConnected = false;
                sConnect = "Connect";
                currentState = "Current State:DisConnected";
                return;
            }

            bIsConnected = axCZKEM1.Connect_Net(Ip, Convert.ToInt32(Port));
            if (bIsConnected == true)
            {
                sConnect = "DisConnect";
                currentState = "Current State:Connected";
                iMachineNumber = 1;
                axCZKEM1.RegEvent(iMachineNumber, 65535);
            }
            else
            {
                axCZKEM1.GetLastError(ref idwErrorCode);
                MessageBox.Show("Unable to connect the device,ErrorCode=" + idwErrorCode.ToString(), "Error");
            }
        }

        public static List<AttendanceLogs> DownloadAttLogs()
        {
            List<AttendanceLogs> attLogs = new List<AttendanceLogs>();
            if (bIsConnected == false)
            {
                MessageBox.Show("Please connect the device first", "Error");
                return attLogs;
            }
            int idwErrorCode = 0;

            string sdwEnrollNumber = "";
            int idwVerifyMode = 0;
            int idwInOutMode = 0;
            int idwYear = 0;
            int idwMonth = 0;
            int idwDay = 0;
            int idwHour = 0;
            int idwMinute = 0;
            int idwSecond = 0;
            int idwWorkcode = 0;

            int iGLCount = 0;

            axCZKEM1.EnableDevice(iMachineNumber, false);
            if (axCZKEM1.ReadGeneralLogData(iMachineNumber))
            {
                while (axCZKEM1.SSR_GetGeneralLogData(iMachineNumber, out sdwEnrollNumber, out idwVerifyMode,
                            out idwInOutMode, out idwYear, out idwMonth, out idwDay, out idwHour, out idwMinute, out idwSecond, ref idwWorkcode))
                {
                    iGLCount++;
                    string sDate = idwYear.ToString() + "-" + idwMonth.ToString() + "-" + idwDay.ToString() + " " + idwHour.ToString() + ":" + idwMinute.ToString() + ":" + idwSecond.ToString();
                    attLogs.Add(new AttendanceLogs { Count = iGLCount, EnrollNumber = sdwEnrollNumber, VerifyMode = idwVerifyMode.ToString(), InOutMode = idwInOutMode.ToString(), Date = sDate, WorkCode = idwWorkcode.ToString() });
                    
                }
            }
            else
            {
                axCZKEM1.GetLastError(ref idwErrorCode);

                if (idwErrorCode != 0)
                {
                    MessageBox.Show("Reading data from terminal failed,ErrorCode: " + idwErrorCode.ToString(), "Error");
                }
                else
                {
                    MessageBox.Show("No data from terminal returns!", "Error");
                }
            }
            axCZKEM1.EnableDevice(iMachineNumber, true);
            return attLogs;
        }

        public static void deleteLogs()
        {
            if (bIsConnected == false)
            {
                MessageBox.Show("Please connect the device first", "Error");
                return;
            }
            int idwErrorCode = 0;

            axCZKEM1.EnableDevice(iMachineNumber, false);
            if (axCZKEM1.ClearGLog(iMachineNumber))
            {
                axCZKEM1.RefreshData(iMachineNumber);
                MessageBox.Show("All att Logs have been cleared from teiminal!", "Success");
            }
            else
            {
                axCZKEM1.GetLastError(ref idwErrorCode);
                MessageBox.Show("Operation failed,ErrorCode=" + idwErrorCode.ToString(), "Error");
            }
            axCZKEM1.EnableDevice(iMachineNumber, true);
        }

        public static string RsConnect = "Disconnect";
        public static void UsbConnect(string port, string baudRate, string MachineSN)
        {
            if (port.Trim() == "" || baudRate.Trim() == "" || MachineSN.Trim() == "")
            {
                MessageBox.Show("Port,BaudRate and MachineSN cannot be null", "Error");
                return;
            }
            int idwErrorCode = 0;
            int iPort;
            string sPort = port.Trim();
            for (iPort = 1; iPort < 10; iPort++)
            {
                if (sPort.IndexOf(iPort.ToString()) > -1)
                {
                    break;
                }
            }

            if (RsConnect == "Disconnect")
            {
                axCZKEM1.Disconnect();
                bIsConnected = false;
                RsConnect = "Connect";
                currentState = "Current State:Disconnected";
                return;
            }

            iMachineNumber = Convert.ToInt32(MachineSN.Trim());
            bIsConnected = axCZKEM1.Connect_Com(iPort, iMachineNumber, Convert.ToInt32(baudRate.Trim()));

            if (bIsConnected == true)
            {
                RsConnect = "Disconnect";
                currentState = "Current State:Connected";

                axCZKEM1.RegEvent(iMachineNumber, 65535);
            }
            else
            {
                axCZKEM1.GetLastError(ref idwErrorCode);
                MessageBox.Show("Unable to connect the device,ErrorCode=" + idwErrorCode.ToString(), "Error");
            }
        }
    }
}
