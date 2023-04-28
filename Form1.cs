using System;
using System.Windows.Forms;

namespace matrix_memory
{
    public partial class Form1 : Form
    {
        private int[,] matrix = new int[3, 3];
        private int numMarkedCells = 0;
        private int numCorrectGuesses = 0;
        private Random random = new Random();

        public Form1()
        {
            InitializeComponent();
            InitializeMatrix();
        }

        private void InitializeMatrix()
        {
            for (int i = 0; i < 3; i++)
            {
                for (int j = 0; j < 3; j++)
                {
                    matrix[i, j] = 0;
                }
            }

            int numCellsToMark = 3;
            while (numCellsToMark > 0)
            {
                int i = random.Next(3);
                int j = random.Next(3);
                if (matrix[i, j] == 0)
                {
                    matrix[i, j] = 1;
                    numCellsToMark--;
                }
            }
        }

        private void ShowMarkedCells()
        {
            for (int i = 0; i < 3; i++)
            {
                for (int j = 0; j < 3; j++)
                {
                    if (matrix[i, j] == 1)
                    {
                        Button cellButton = (Button)this.Controls["cell" + i + j];
                        cellButton.Text = "X";
                    }
                }
            }
            timer1.Start();
        }

        private void HideMarkedCells()
        {
            // Hide marked cells
            for (int i = 0; i < 3; i++)
            {
                for (int j = 0; j < 3; j++)
                {
                    Button cellButton = (Button)this.Controls["cell" + i + j];
                    cellButton.Text = "";
                }
            }
        }

        private void CheckGuess(int i, int j)
        {
            if (matrix[i, j] == 1)
            {
                numCorrectGuesses++;
                Button cellButton = (Button)this.Controls["cell" + i + j];
                cellButton.Enabled = false;
                if (numCorrectGuesses == 3)
                {
                    MessageBox.Show("Congratulations, you won!");
                }
            }
            else
            {
                MessageBox.Show("Game over, you lost XD.");
                Application.Restart();
            }
        }

        private void CellClick(object sender, EventArgs e)
        {
            Button cellButton = (Button)sender;
            int i = int.Parse(cellButton.Name[4].ToString());
            int j = int.Parse(cellButton.Name[5].ToString());
            if (cellButton.Enabled)
            {
                CheckGuess(i, j);
            }
        }

        private void Timer1Tick(object sender, EventArgs e)
        {
            HideMarkedCells();
            timer1.Stop();
        }

        private void StartButtonClick(object sender, EventArgs e)
        {
            numCorrectGuesses = 0;
            InitializeMatrix();
            ShowMarkedCells();
            for (int i = 0; i < 3; i++)
            {
                for (int j = 0; j < 3; j++)
                {
                    Button cellButton = (Button)this.Controls["cell" + i + j];
                    cellButton.Enabled = true;
                }
            }
        }

        private void timer1_Tick_1(object sender, EventArgs e)
        {

        }

        private void Form1_Load(object sender, EventArgs e)
        {

        }
    }
}
