using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace CatchGame
{
    public partial class Form1 : Form
    {
        Rectangle hero = new Rectangle(280, 540, 40, 10);
        int heroSpeed = 10;

        List<Rectangle> balls = new List<Rectangle>();
        List<int> ballSpeeds = new List<int>();
        List<string> ballColours = new List<string>();
        int ballSize = 10;

        int score = 0;
        int time = 500;

        bool leftDown = false;
        bool rightDown = false;

        SolidBrush redBrush = new SolidBrush(Color.Red);
        SolidBrush greenBrush = new SolidBrush(Color.Green);
        SolidBrush goldBrush = new SolidBrush(Color.Gold);
        SolidBrush whiteBrush = new SolidBrush(Color.White);

        Random randGen = new Random();
        int randValue = 0;

        int groundHeight = 50;


        public Form1()
        {
            InitializeComponent();
        }

        private void Form1_KeyDown(object sender, KeyEventArgs e)
        {
            switch (e.KeyCode)
            {
                case Keys.Left:
                    leftDown = true;
                    break;
                case Keys.Right:
                    rightDown = true;
                    break;
            }

        }

        private void Form1_KeyUp(object sender, KeyEventArgs e)
        {
            switch (e.KeyCode)
            {
                case Keys.Left:
                    leftDown = false;
                    break;
                case Keys.Right:
                    rightDown = false;
                    break;
            }

        }

        private void gameTimer_Tick(object sender, EventArgs e)
        {
            //move hero
            if (leftDown == true && hero.X > 0)
            {
                hero.X -= heroSpeed;
            }

            if (rightDown == true && hero.X < this.Width - hero.Width)
            {
                hero.X += heroSpeed;
            }

            //move balls
            for (int i = 0; i < balls.Count(); i++)
            {
                //find the new postion of y based on speed
                int y = balls[i].Y + ballSpeeds[i];

                //replace the rectangle in the list with updated one using new y
                balls[i] = new Rectangle(balls[i].X, y, ballSize, ballSize);
            }

            //create a ball if it is time
            randValue = randGen.Next(0, 101);

            if (randValue < 1) //1% chance of gold ball, (extra time)
            {
                int x = randGen.Next(10, this.Width - ballSize * 2);
                balls.Add(new Rectangle(x, 10, ballSize, ballSize));
                ballSpeeds.Add(randGen.Next(2, 10));
                ballColours.Add("gold");

            }
            else if (randValue < 6) //5% change of red ball, (lose points)
            {
                int x = randGen.Next(10, this.Width - ballSize * 2);
                balls.Add(new Rectangle(x, 10, ballSize, ballSize));
                ballSpeeds.Add(randGen.Next(2, 10));
                ballColours.Add("red");

            }
            else if (randValue < 11) //5% change of green ball, (get points)
            {
                int x = randGen.Next(10, this.Width - ballSize * 2);
                balls.Add(new Rectangle(x, 10, ballSize, ballSize));
                ballSpeeds.Add(randGen.Next(2, 10));
                ballColours.Add("green");
            }


            Refresh();
        }

        private void Form1_Paint(object sender, PaintEventArgs e)
        {
            //update labels
            timeLabel.Text = $"Time Left: {time}";
            scoreLabel.Text = $"Score: {score}";

            //draw ground
            e.Graphics.FillRectangle(greenBrush, 0, this.Height - groundHeight,
                this.Width, groundHeight);

            //draw hero
            e.Graphics.FillRectangle(whiteBrush, hero);

            //draw balls
            for (int i = 0; i < balls.Count(); i++)
            {
                if (ballColours[i] == "red")
                {
                    e.Graphics.FillEllipse(redBrush, balls[i]);
                }
                else if (ballColours[i] == "green")
                {
                    e.Graphics.FillEllipse(greenBrush, balls[i]);
                }
                else if (ballColours[i] == "gold")
                {
                    e.Graphics.FillEllipse(goldBrush, balls[i]);
                }
            }

        }
    }
}
