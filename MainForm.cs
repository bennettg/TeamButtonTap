using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Windows.Input;
using System.Timers;
using System.Media;

   // Team 1's Button to push is Enter
   // Team 2's Button to push is Space

namespace TeamButtonTap
{
    public partial class MainForm : Form
    {

        private bool TeamAnswering = false; //defines whether or not a team has already pressed the button to answer
        private int counter = 0;
        private System.Timers.Timer resetTimer;
        private const int ANSWER_LOCK_PERIOD = 8;

        public MainForm()
        {
            InitializeComponent();
        }

        private void MainWindow_Load(object sender, EventArgs e)
        {
            label1.Text = "Team 1 Ready \n (Push the Enter Key)";
            label2.Text = "Team 2 Ready \n (Push the Space Bar)";
            this.KeyDown += new System.Windows.Forms.KeyEventHandler(OnKeyDown);    
        }

        private int TeamPressedFirst(System.Windows.Forms.KeyEventArgs e)
        {

            if (e.KeyCode == Keys.Enter)
            {
                return 1;
            }
            else if (e.KeyCode == Keys.Space)
            {
                return 2;
            }
            else
            {
                return 0;
            }
        }

        public void resetTeamPanels ()
        {            
            panel1.BackColor = Color.White;
            panel2.BackColor = Color.White;
            label1.Text = "Team 1 Ready \n (Push the Enter Key)";
            label2.Text = "Team 2 Ready \n (Push the Space Bar)";

            Random r1 = new Random();
            if(r1.Next(1,10) == 5)
            {
                label1.Text += "\n Hehehe";
            }

        }

        public void startNoAnsweringPeriod ()
        {
            this.TeamAnswering = true; //stop any further answering until the counter = 10

            this.resetTimer = new System.Timers.Timer();
            // Hook up the Elapsed event for the timer. 
            this.resetTimer.Interval = 600;
            this.resetTimer.Enabled = true;
            this.resetTimer.Elapsed += timerTick;
           
        }

        void OnKeyDown(object sender, System.Windows.Forms.KeyEventArgs e)
        {
            label1.BackColor = Color.White;
            label2.BackColor = Color.White;


            if (!this.TeamAnswering)
            {
                System.Media.SystemSounds.Hand.Play();

                if (TeamPressedFirst(e) == 1)
                {
                    panel1.BackColor = Color.Green;
                    panel2.BackColor = Color.Red;
                    label1.Text = "Team 1's Answer!";
                    label2.Text = ":( Too Slow";
                        int rgen = getRandomNumber();
                        if (rgen == 5 || rgen == 9)
                        {
                            label2.Text += " Hahaha!";
                        }
                    e.Handled = true;

                    startNoAnsweringPeriod();
                }
                else if (TeamPressedFirst(e) == 2)
                {

                    panel1.BackColor = Color.Red;
                    panel2.BackColor = Color.Green;
                    label1.Text = ":( Too Slow";
                    label2.Text = "Team 2's Answer!";
                        int rgen = getRandomNumber();
                        if (rgen == 5 || rgen == 9)
                        {
                            label1.Text += " Hahaha!"; //add an extra rude remark to the loser's side
                        }
                    e.Handled = true;

                    startNoAnsweringPeriod();
                }
            }

            
        }

        private int getRandomNumber ()
        {
            Random r2 = new Random();
            int rgen = r2.Next(1, 10);
            return rgen;
        }

        private void timerTick(object sender, System.EventArgs e)
        {
            if (this.counter >= ANSWER_LOCK_PERIOD)
            {
                this.resetTimer.Enabled = false;
                this.TeamAnswering = false; //Now another team can answer
                resetTeamPanels();
                this.counter = 0;
            }
            else
            {
                this.counter = this.counter + 1;
            }
        }

    }
}
