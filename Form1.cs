using System;
using System.Collections.Generic;
using System.Drawing;
using System.Windows.Forms;
using System.Linq;
using System.Threading.Tasks;

namespace matrixMemory
{
    public partial class MainForm : Form
    {
        private Button[,] buttons = new Button[3, 3];
        private bool[,] markedCells = new bool[3, 3];
        private int markedCount = 0;
        private const int MarkedCellsNumber = 3; // количество помеченных клеток
        private System.Windows.Forms.Timer timer = new System.Windows.Forms.Timer();

        public MainForm()
        {
            InitializeComponent();

            for (int i = 0; i < 3; i++)
            {
                for (int j = 0; j < 3; j++)
                {
                    buttons[i, j] = new Button
                    {
                        Location = new Point(i * 60, j * 60),
                        Size = new Size(50, 50)
                    };
                    buttons[i, j].Click += ButtonClick;
                    this.Controls.Add(buttons[i, j]);
                }
            }

            timer.Interval = 5000; // 5 секунд
            timer.Tick += TimerTick;
            timer.Start();

            Random rnd = new Random();
            while (markedCount < MarkedCellsNumber)
            {
                int x = rnd.Next(3);
                int y = rnd.Next(3);
                if (!markedCells[x, y])
                {
                    markedCells[x, y] = true;
                    buttons[x, y].Text = "X";
                    markedCount++;
                }
            }
        }

        private void ButtonClick(object sender, EventArgs e)
        {
            Button button = sender as Button;
            if (button == null) return;

            int x = button.Location.X / 60;
            int y = button.Location.Y / 60;

            if (markedCells[x, y])
            {
                button.Text = "X";
                button.Enabled = false;
                markedCount--;

                if (markedCount == 0)
                {
                    MessageBox.Show("Вы выиграли!");
                    timer.Stop();
                }
            }
            else
            {
                MessageBox.Show("Вы проиграли!");
                timer.Stop();
            }
        }

        private void TimerTick(object sender, EventArgs e)
        {
            foreach (Button button in buttons)
            {
                button.Text = "";
            }

            timer.Stop();
        }
    }
}
