using System;
using System.Drawing;
using System.Windows.Forms;

namespace Snake
{
    public partial class Form1 : Form
    {
        private int direction = 0;
        private int score = 1;
        private int bestScore = 1;
        private string playerName = "Player1";
        private Timer gameLoop = new Timer();
        private Random rand = new Random();
        private Graphics graphics;
        private Snake snake;
        private Food food;


        public Form1()
        {
            InitializeComponent();
            snake = new Snake();
            food = new Food(rand);
            gameLoop.Interval = 75;
            gameLoop.Tick += Update;
        }

        private void label2_Click(object sender, EventArgs e)
        {

        }

        private void Form1_KeyDown(object sender, KeyEventArgs e)
        {
            switch(e.KeyData)
            {
                case Keys.Enter:
                    if (label1.Visible && label2.Visible && label3.Visible)
                    {
                        label1.Visible = false;
                        label2.Visible = false;
                        label3.Visible = false;
                        panel1.Visible = true;
                        gameLoop.Start();
                    }
                    break;

                case Keys.Space:
                    if (!label1.Visible && !label2.Visible && !label3.Visible)
                    {                        
                        gameLoop.Enabled = (gameLoop.Enabled) ? false : true ;
                    }
                    break;

                case Keys.Right:
                    if (direction != 2)
                        direction = 0;
                    break;

                case Keys.Down:
                    if (direction != 3)
                        direction = 1;
                    break;
                case Keys.Left:
                    if (direction != 0)
                        direction = 2;
                    break;
                case Keys.Up:
                    if (direction != 1)
                        direction = 3;
                    break;
            }
        }

        private void Form1_Paint(object sender, PaintEventArgs e)
        {
            graphics = this.CreateGraphics();
            snake.Draw(graphics);
            food.Draw(graphics);

        }

        private void Update(object sender, EventArgs e)
        {
            if (score > bestScore)
                bestScore = score;
            this.Text = string.Format("Snake - Score {0} ", score);
            label7.Text = string.Format("{0}", playerName);
            label8.Text = string.Format("{0}", score);
            label9.Text = string.Format("{0}", bestScore);
            snake.Move(direction);
            for (int i = 1; i < snake.Body.Length; i++)
            {
                if (snake.Body[0].IntersectsWith(snake.Body[i]))
                Restart();
            }
                if (snake.Body[0].X < 0 || snake.Body[0].X > 430)
                    Restart();
                if (snake.Body[0].Y < 0 || snake.Body[0].Y > 430)
                    Restart();
                if (snake.Body[0].IntersectsWith(food.Piece))
                {
                    score++;
                    snake.Grow();
                    food.Generate(rand);
                }
            this.Invalidate();


        }

        private void Restart()
        {
            panel1.Visible = false;
            gameLoop.Stop();
            graphics.Clear(SystemColors.Control);
            snake = new Snake();
            food = new Food(rand);
            direction = 0;
            score = 1;
            label1.Visible = true;
            label2.Visible = true;
            label3.Visible = true;

        }

        private void label4_Click(object sender, EventArgs e)
        {

        }

        private void panel1_Paint(object sender, PaintEventArgs e)
        {

        }

        private void label5_Click(object sender, EventArgs e)
        {

        }

        private void label6_Click(object sender, EventArgs e)
        {

        }

        private void label10_Click(object sender, EventArgs e)
        {

        }
    }
}
