using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Media;
using System.IO;

namespace RCheck
{
    public partial class Form1 : Form
    {
        int theM = 60; //PLAY SOUND AFTER theM MINUTES
        int timeCount = 0;  //COUNTS SECONDS
        float currentM; //CALCULATES CURRENT MINUTE
        int countD = 0; //COUNTS HOW MANY TIMES THE SOUND HAS PLAYED
        string doc = System.Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments);

        Timer t = new Timer();
        SoundPlayer soundL; //REMIND SOUND

        public Form1()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            label2.Text = doc.ToString() + @"\Lucid\remind_sound.wav";

            //CREATE SOUNDPLAYER WITH SOUNDFILE
            soundL = new SoundPlayer(doc + @"\Lucid\remind_sound.wav");

            //VALUE OF MINUTE WHEN TO PLAY SOUND
            numericUpDown1.Value = 60;

            //CREATE TICK
            t.Interval = 1000;
            t.Tick += new EventHandler(this.t_Tick);
            t.Start();
        }

        private void t_Tick(object sender, EventArgs e)
        {
            TimeCheck();
        }

        void TimeCheck()
        {
            //GET TIME H:M:S
            int curS = DateTime.Now.Second; //CURRENT SECOND
            int curM = DateTime.Now.Minute; //CURRENT MINUTE
            int curH = DateTime.Now.Hour; //CURRENT HOUR

            timeCount++;
            currentM = timeCount / 60;

            float toTimeM = theM - currentM;

            theM = (int)numericUpDown1.Value; //CHANGE VALUE FOR MINUTE WHEN ALARM

            //label1.Text = curM.ToString() + " : " + curS.ToString(); //DISPLAY TIME
            label1.Text = toTimeM.ToString() + " MIN";

            if (currentM >= theM)
            {
                playSoundF(curH, curM, curS);
                timeCount = 0;
            }
        }

        void playSoundF(int h, int m, int s)
        {
            soundL.Load();
            soundL.Play();
            countD++; //DEBUG.TXT NUMBER IN FRONT OF TIME ++

            //DEBUG TIME WHEN SOUND IS PLAYED
            File.AppendAllText(doc + @"\Lucid\remind_sound.wav", countD + ". " + h.ToString() + " : " + m.ToString() + " : " + s.ToString() + Environment.NewLine);
        }

        private void button1_Click(object sender, EventArgs e)
        {
            soundL.Play();
        }
    }
}
