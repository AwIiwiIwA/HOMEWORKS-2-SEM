using System;
using System.Windows.Forms;
using matrixMemory;

namespace matrixMemory
{
    static class Program
    {
        /// <summary>
        /// ������� ����� ����� ��� ����������.
        /// </summary>
        [STAThread]
        static void Main()
        {
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            Application.Run(new MainForm());  // �������� Form1 �� MainForm
        }
    }
}
