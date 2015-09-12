using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace LightsOut_
{
    public partial class MainForm : Form
    {
        private const int GRID_OFFSET = 25;
        private int GRID_LENGTH = 200;
        private const int NUM_CELLS = 5;
        private int CELL_LENGTH;

        private bool[,] grid;
        private Random rand;
        private int board_size;

        public MainForm()
        {
            InitializeComponent();

            board_size = 3;
            CELL_LENGTH = GRID_LENGTH / board_size;
            rand = new Random();
            grid = new bool[NUM_CELLS, NUM_CELLS];

            randomizeBoard();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            randomizeBoard();
        }

        private void randomizeBoard()
        {
            for (int r = 0; r < board_size; r++)
                for (int c = 0; c < board_size; c++)
                    grid[r, c] = rand.Next(2) == 1;

            this.Invalidate();
        }

        private void MainForm_Load(object sender, EventArgs e)
        {

        }

        private void MainForm_Paint(object sender, PaintEventArgs e)
        {
            Graphics g = e.Graphics;
            CELL_LENGTH = GRID_LENGTH / board_size;
            for(int r = 0; r < board_size; r++)
            {
                for(int c = 0; c< board_size; c++)
                {
                    Brush brush;
                    Pen pen;

                    if(grid[r,c])
                    {
                        pen = Pens.MidnightBlue;
                        brush = Brushes.LightGoldenrodYellow;
                    }
                    else
                    {
                        pen = Pens.LightGoldenrodYellow;
                        brush = Brushes.MidnightBlue;
                    }

                    int x = c * CELL_LENGTH + GRID_OFFSET;
                    int y = r * CELL_LENGTH + GRID_OFFSET;

                    g.DrawRectangle(pen, x, y, CELL_LENGTH, CELL_LENGTH);
                    g.FillRectangle(brush, x + 1, y + 1, CELL_LENGTH - 1, CELL_LENGTH - 1);
                }
            }
        }

        private void MainForm_MouseDown(object sender, MouseEventArgs e)
        {
            if (e.X < GRID_OFFSET || e.X > CELL_LENGTH * board_size + GRID_OFFSET ||
                e.Y < GRID_OFFSET || e.Y > CELL_LENGTH * board_size + GRID_OFFSET)
                return;

            int r = (e.Y - GRID_OFFSET) / CELL_LENGTH;
            int c = (e.X - GRID_OFFSET) / CELL_LENGTH;

            for (int i = r - 1; i <= r + 1; i++)
                for (int j = c - 1; j <= c + 1; j++)
                    if (i >= 0 && i < board_size && j >= 0 && j < board_size)
                        grid[i, j] = !grid[i, j];

            this.Invalidate();

            if (PlayerWon())
            {
                MessageBox.Show(this, "Congratulations! You've won!", "Lights Out!",
                    MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }

        private bool PlayerWon()
        {
            for (int r = 0; r < board_size; r++){
                 for (int c = 0; c < board_size; c++)
                 {
                     if(grid[r,c] == true)
                     {
                         return false;
                     }
                 }
            }
            return true;
        }

        private void newToolStripMenuItem_Click(object sender, EventArgs e)
        {
            button1_Click(sender, e);
        }

        private void exitButton_Click(object sender, EventArgs e)
        {
            Close();
        }

        private void exitToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Close();
        }


        private void aboutToolStripMenuItem_Click_1(object sender, EventArgs e)
        {
            AboutForm aboutBox = new AboutForm();
            aboutBox.ShowDialog(this);
        }

        private void sizeStripMenuItem_Click(object sender, EventArgs e)
        {
            if (sender == x3ToolStripMenuItem)
            {
                x3ToolStripMenuItem.Checked = true;
                x4ToolStripMenuItem.Checked = false;
                x5ToolStripMenuItem.Checked = false;
                board_size = 3;
            }
            else if (sender == x4ToolStripMenuItem)
            {
                x3ToolStripMenuItem.Checked = false;
                x4ToolStripMenuItem.Checked = true;
                x5ToolStripMenuItem.Checked = false;
                board_size = 4;
            }
            else
            {
                x3ToolStripMenuItem.Checked = false;
                x4ToolStripMenuItem.Checked = false;
                x5ToolStripMenuItem.Checked = true;
                board_size = 5;
            }
            randomizeBoard();

        }

        private void MainForm_Resize(object sender, EventArgs e)
        {
            if (this.Width < this.Height)
            {
                GRID_LENGTH = this.Width - 100;
            }
            else
            {
                GRID_LENGTH = this.Height - 100;
            }
            this.Invalidate();
        }
    
    }
}
