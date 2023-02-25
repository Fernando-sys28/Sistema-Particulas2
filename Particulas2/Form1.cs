using Particulas2.Properties;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Drawing.Imaging;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Particulas2
{
    public partial class Form1 : Form
    {
        public int ballsize;
        private Bitmap bmp;
        private Graphics g;
        private int NumBalls = 10;
        private Random rand = new Random();
        private List<particles> balls = new List<particles>();
        private Image background;
        public Form1()
        {
            InitializeComponent();
            // Create the PictureBox and add it to the form
            bmp = new Bitmap(pictureBox1.Width, pictureBox1.Height);
            g = Graphics.FromImage(bmp);
            pictureBox1.Image = bmp;

            // Create some balls
            for (int i = 0; i < NumBalls; i++)
            {

                this.balls.Add(new particles
                {
                    ballsize = rand.Next(80, 120),
                    X = rand.Next(320, 360),
                    Y = rand.Next(300, 330),
                    DX = rand.Next(2, 5),
                    DY = rand.Next(-2, 2),
                    lifes = rand.Next(40, 60),
                    textura = Resource1.fuego
                }) ;

            }
        }
        private PointF emisor(int x, int y)
        {
            PointF emisor = new PointF(x, y);
            return emisor;
        }

        private void timer1_Tick(object sender, EventArgs e)
        {


             for (int i = 0; i < balls.Count; i++)
            {
                balls[i].X += balls[i].DX;
                balls[i].Y += balls[i].DY;

                if (balls[i].lifes == 0)
                {
                    balls[i].X = rand.Next(320, 350);
                    balls[i].Y = rand.Next(300, 330);
                    balls[i].DX = rand.Next(0, 5);
                    balls[i].DY = rand.Next(-3, 3);
                    balls[i].lifes = rand.Next(40, 60);
                }
                // Update ball's position          
                balls[i].lifes--;
            }
          
            // Draw the balls
            drawBalls();
            pictureBox1.Invalidate();
        }

        public void drawBalls()
        {

            background = Resource1.hasbullaOax;
            g.DrawImage(background, new Point(0, 0));
            // Draw the balls
            for (int i = 0; i < balls.Count; i++)
            {
                // Reduce the alpha value based on the 'lifes' property
                int alpha = 255 * balls[i].lifes / 60;
                ColorMatrix matrix = new ColorMatrix
                {
                    Matrix33 = alpha / 255f // Set the alpha value in the color matrix
                };
                ImageAttributes attributes = new ImageAttributes();
                attributes.SetColorMatrix(matrix, ColorMatrixFlag.Default, ColorAdjustType.Bitmap);
                g.DrawImage(balls[i].textura, new Rectangle(Convert.ToInt32(balls[i].X), Convert.ToInt32(balls[i].Y), balls[i].ballsize, balls[i].ballsize),0, 0, balls[i].textura.Width, balls[i].textura.Height, GraphicsUnit.Pixel, attributes); 
            }

            pictureBox1.Refresh();
        }
    }
}
