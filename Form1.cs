using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Runtime.InteropServices;

namespace Projekt
{
    public partial class Form1 : Form
    {
        //objects
        class MyClass
        {
            public PictureBox obj { get; set; }
            public Timer objTimer { get; set; }
        }
        //Korzinka position
        int KposX;
        int KposY;
        //movement
        [DllImport("User32.dll")]
        private static extern short GetAsyncKeyState(System.Windows.Forms.Keys vKey);
        //
        int Points = 0;
        int life = 10;
        int T = 10;

        Random rnd = new Random();

        List<MyClass> objektai;
        private void CreateObjects()
        {
            objektai = new List<MyClass>()
        {
            new MyClass()
            {
                obj=new PictureBox()
                {
                    Image = Properties.Resources.A,
                    Size=new Size(60,60),
                    SizeMode= PictureBoxSizeMode.StretchImage,
                    BackColor=Color.Transparent,
                    Location=new Point(rnd.Next(100, this.Width - 160),rnd.Next(100, this.Height / 2))
                },
                objTimer = new Timer()
                {
                    Interval = 450, Tag="Apple"
                }
            },
            new MyClass()
            {
                obj=new PictureBox()
                {
                    Image = Properties.Resources.A,
                    Size=new Size(60,60),
                    SizeMode= PictureBoxSizeMode.StretchImage,
                    BackColor=Color.Transparent,
                    Location=new Point(rnd.Next(100, this.Width - 160),rnd.Next(100, this.Height / 2))
                },
                objTimer = new Timer()
                {
                    Interval = 410, Tag="Apple2"
                }
            },
            new MyClass()
            {
                obj=new PictureBox()
                {
                    Image = Properties.Resources.A,
                    Size=new Size(60,60),
                    SizeMode= PictureBoxSizeMode.StretchImage,
                    BackColor=Color.Transparent,
                    Location=new Point(rnd.Next(100, this.Width - 160),rnd.Next(100, this.Height / 2))
                },
                objTimer = new Timer()
                {
                    Interval = 239, Tag="Apple3"
                }
            },
            new MyClass()
            {
                obj=new PictureBox()
                {
                    Image = Properties.Resources.BadA,
                    Size=new Size(60,60),
                    SizeMode= PictureBoxSizeMode.StretchImage,
                    BackColor=Color.Transparent,
                    Location=new Point(rnd.Next(100, this.Width - 160),rnd.Next(100, this.Height / 2))
                },
                objTimer = new Timer()
                {
                    Interval = 577, Tag="BadApple"
                }
            },
              new MyClass()
            {
                obj=new PictureBox()
                {
                    Image = Properties.Resources.pear,
                    Size=new Size(60,60),
                    SizeMode= PictureBoxSizeMode.StretchImage,
                    BackColor=Color.Transparent,
                    Location=new Point(rnd.Next(100, this.Width- 160),rnd.Next(100, this.Height / 2))
                },
                objTimer = new Timer()
                {
                    Interval = 480, Tag="Pear"
                }
            },
              new MyClass()
            {
                obj=new PictureBox()
                {
                    Image = Properties.Resources.pear,
                    Size=new Size(60,60),
                    SizeMode= PictureBoxSizeMode.StretchImage,
                    BackColor=Color.Transparent,
                    Location=new Point(rnd.Next(100, this.Width- 160),rnd.Next(100, this.Height / 2))
                },
                objTimer = new Timer()
                {
                    Interval = 650, Tag="Pear2"
                }
            },
              new MyClass()
            {
                obj=new PictureBox()
                {
                    Image = Properties.Resources.pear,
                    Size=new Size(60,60),
                    SizeMode= PictureBoxSizeMode.StretchImage,
                    BackColor=Color.Transparent,
                    Location=new Point(rnd.Next(100, this.Width- 160),rnd.Next(100, this.Height / 2))
                },
                objTimer = new Timer()
                {
                    Interval = 390, Tag="Pear3"
                }
            },
             new MyClass()
            {
                obj=new PictureBox()
                {
                    Image = Properties.Resources.BadPear,
                    Size=new Size(60,60),
                    SizeMode= PictureBoxSizeMode.StretchImage,
                    BackColor=Color.Transparent,
                    Location=new Point(rnd.Next(100, this.Width - 160),rnd.Next(100, this.Height / 2))
                },
                objTimer = new Timer()
                {
                    Interval = 333, Tag="BadPear"
                }
            },
             
             new MyClass()
            {
                obj=new PictureBox()
                {
                    Image = Properties.Resources.minus10,
                    Size=new Size(60,60),
                    SizeMode= PictureBoxSizeMode.StretchImage,
                    BackColor=Color.Transparent,
                    Location=new Point(rnd.Next(100, this.Width - 160),rnd.Next(100, this.Height / 2))
                },
                objTimer = new Timer()
                {
                    Interval = 1000, Tag="minus10"
                }
            },
             new MyClass()
            {
                obj=new PictureBox()
                {
                    Image = Properties.Resources.plus10,
                    Size=new Size(60,60),
                    SizeMode= PictureBoxSizeMode.StretchImage,
                    BackColor=Color.Transparent,
                    Location=new Point(rnd.Next(100, this.Width - 160),rnd.Next(100, this.Height / 2))
                },
                objTimer = new Timer()
                {
                    Interval = 780, Tag="plus10"
                }
            },


        };
            for (int i = 0; i < objektai.Count; i++)
            {
                objektai[i].objTimer.Tick += obj_Time;
                this.Controls.Add(objektai[i].obj);
            }
        }

        public Form1()
        {
            InitializeComponent();
            GenerateNewPosition();
            //функции, чтобы объекты нормально падали
            this.SetStyle(ControlStyles.OptimizedDoubleBuffer, true);
            this.SetStyle(ControlStyles.AllPaintingInWmPaint, true);
            this.SetStyle(ControlStyles.UserPaint, true);
            //добавляем объекты на экран и заставляем двигаться(падать)
            CreateObjects();

        }
        private void obj_Time(object sender, EventArgs e)
        {
            var i = objektai.FindIndex(a => a.objTimer.Tag == ((Timer)sender).Tag);
            objektai[i].obj.Location = new Point(objektai[i].obj.Location.X, objektai[i].obj.Location.Y + T);
            Position(objektai[i]);

        }

        void GenerateNewPosition()
        {
            KposY = this.Bottom - Korzinka.Height - 60;
            KposX = this.Width / 2;
            Korzinka.Location = new Point(KposX, KposY);
            Korzinka.BringToFront();
        }

        private void Form1_KeyDown(object sender, KeyEventArgs e)
        {

            if (GetAsyncKeyState(Keys.Right) < 0 || GetAsyncKeyState(Keys.Left) < 0)
            {

                StartAppleFalling();
                if (GetAsyncKeyState(Keys.Right) < 0)
                {
                    KposX += 10;
                    if (Korzinka.Location.X + Korzinka.Width + 20 >= this.Width)
                    {
                        KposX -= 10;
                    }
                }
                if (GetAsyncKeyState(Keys.Left) < 0)
                {
                    KposX -= 10;
                    if (Korzinka.Location.X <= 0)
                    {

                        KposX += 10;
                    }
                }
                Korzinka.Location = new Point(KposX, KposY);
            }
        }

        private void CheckWin()
        {

            if (Points < 0 || life == 0)
            {
                for (int i = 0; i < objektai.Count; i++)
                {
                    objektai[i].objTimer.Stop();
                }
                DialogResult result = MessageBox.Show("You Lose :( Do you want to try again?", "Continue?", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
                if (result == DialogResult.Yes)
                {
                    for (int i = 0; i < objektai.Count; i++)
                    {
                        objektai[i].obj.Visible = false;
                    }
                    Points = 0;
                    T = 10;
                    life = 10;
                    Updatelabels();
                    GenerateNewPosition();
                    CreateObjects();
                }
                else this.Close();
            }
        }

        private void Updatelabels()
        {
            lblPoints.Text = Points.ToString();
            lblLife.Text = life.ToString();
        }

        private void StartAppleFalling()
        {
            for (int i = 0; i < objektai.Count; i++)
            {
                objektai[i].objTimer.Enabled = true;
            }
        }

        private void Position(MyClass sender)
        {

            if (Korzinka.Bounds.IntersectsWith(sender.obj.Bounds))
            {
                if (sender.objTimer.Tag.ToString() == "Apple" || sender.objTimer.Tag.ToString() == "Apple2" || sender.objTimer.Tag.ToString() == "Apple3" || sender.objTimer.Tag.ToString() == "Pear" || sender.objTimer.Tag.ToString() == "Pear2" || sender.objTimer.Tag.ToString() == "Pear3")
                {
                    Points++;
                }
                if (sender.objTimer.Tag.ToString() == "BadApple" || sender.objTimer.Tag.ToString() == "BadPear" || sender.objTimer.Tag.ToString() == "BadApple2" || sender.objTimer.Tag.ToString() == "BadPear2")
                {
                    Points--;
                }
                if (sender.objTimer.Tag.ToString() == "minus10")
                {
                    Points -= 10;
                }
                if (sender.objTimer.Tag.ToString() == "plus10")
                {
                    Points += 10;
                }
                Updatelabels();
                sender.obj.Location = new Point(rnd.Next(100, this.Width - 100), rnd.Next(100, this.Height / 2 - 100));
                CheckSpeed();
            }
            if (sender.obj.Bounds.IntersectsWith(pnlBottom.Bounds))
            {
                if (sender.objTimer.Tag.ToString() == "Apple" || sender.objTimer.Tag.ToString() == "Apple2" || sender.objTimer.Tag.ToString() == "Apple3" || sender.objTimer.Tag.ToString() == "Pear" || sender.objTimer.Tag.ToString() == "Pear2" || sender.objTimer.Tag.ToString() == "Pear3")
                {
                    life--;
                    Updatelabels();
                }
                sender.obj.Location = new Point(rnd.Next(100, this.Width - 100), rnd.Next(100, this.Height / 2));
                CheckSpeed();
            }
            CheckWin();
        }

        private void CheckSpeed()
        {
            if (Points > 1 && Points % 10 == 0)
            {
                T += 1;
            }
        }
    }
}
