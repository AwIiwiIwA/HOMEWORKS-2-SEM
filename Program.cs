using System;
using System.Windows.Forms;
using matrixMemory;

namespace matrixMemory
{
    static class Program
    {
        /// <summary>
        /// Главная точка входа для приложения.
        /// </summary>
        [STAThread]
        static void Main()
        {
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            Application.Run(new MainForm());  // Замените Form1 на MainForm
        }
    }
}
