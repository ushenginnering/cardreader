using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Runtime.InteropServices;

namespace read_and_write_card
{

    internal class DLL
    {
        public const string CppFunctionsDll = @"proRFL.dll";

        //intialize (Open) USB
        [DllImport(CppFunctionsDll, CallingConvention = CallingConvention.StdCall, EntryPoint = "initializeUSB")]

        public static extern int initializeUSB(byte d12);

        //get DLL Version
        [DllImport(CppFunctionsDll, CallingConvention = CallingConvention.StdCall, EntryPoint = "GetDLLVersion")]

        public static extern int GetDLLVersion(byte bufVer);

        //ReadCard (uchar d12, uchar * buffData) 
        //get DLL Version
        [DllImport(CppFunctionsDll, CallingConvention = CallingConvention.StdCall, EntryPoint = "ReadCard")]

        public static extern int ReadCard(int d12, byte[] buffData);

        //get DLL Version
        [DllImport(CppFunctionsDll, CallingConvention = CallingConvention.StdCall, EntryPoint = "Buzzer")]

        public static extern int Buzzer(byte d12, byte t);
        [DllImport(CppFunctionsDll, CallingConvention = CallingConvention.StdCall, EntryPoint = "GuestCard")]
        public static extern int GuestCard(byte d12, int dlsCoID, byte CardNo, byte dai, byte LLock, byte pdoors, string BDate, string EDate, string LockNo, byte[] cardHexStr);


        public string startCard(byte flag)
        {
            int output = initializeUSB(flag);
            if (output != 0)
            {
                string tim = "failed";
                return tim;
            }
            else
            {
                string tim = "success";
                return tim;
            }
        }
        public void shortbuzz()
        {
            byte flag = 1;
            byte time = 25;
            int tom = Buzzer(flag, time);
        }
        public void longbuzz()
        {
            byte flag = 1;
            byte time = 100;
            int tom = Buzzer(flag, time);
        }

        public string generateCardId(byte value1, byte value2, byte value3)
        {
            byte flag = value1;
            int dlsCoID = 1; // hospital logo
            byte CardNo = value2; // card No  // random number then chack if it is stored in the bank 
            byte dai = 0;
            byte LLock = 1; // able to open locked door  
            byte pdoors = value3; // able to open public door
            string BDate = "2209170700";
            string EDate = "2209171700";
            string LockNo = "1";
            byte[] d = new byte[100];

            int ki = DLL.GuestCard(flag, dlsCoID, CardNo, dai, LLock, pdoors, BDate, EDate, LockNo, d);
            if (ki != 0)
            {
                string result = "failed";
                return result;
            }
            else
            {
                var result = string.Empty;
                foreach (byte b in d)
                {
                    char c = Convert.ToChar(b);
                    result += c.ToString();
                }
                return result;
            }
        }

        public string RCard()
        {
            byte[] d = new byte[50];
            int g = DLL.ReadCard(d.Length, d);
            if (g != 0)
            {
                return "Cannot read card for some reason";
            }
            else
            {
                var result = string.Empty;
                string res = "";
                foreach (byte b in d)
                {
                    char c = Convert.ToChar(b);
                    result += c.ToString();
                }
                if (result == "55010054FFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFF")
                {
                    return "No card";
                    // MessageBox.Show("No card detected: Please place a new card on the device");
                }
                else
                {

                    for (int fu = 6; fu < 46; fu++)
                    {
                        if (fu == 9)
                        {
                            res += 0;
                        }
                        else if (fu < 38)
                        {
                            char c = Convert.ToChar(d[fu]);
                            res += c.ToString();
                        }
                        else
                        {
                            res += "C";
                        }
                    }
                    string ji = res;
                    return ji;
                }

            }
        }



    }
}
