using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace WindowsFormsApp2
{
    public partial class form : Form
    {
        Timer timer1 = new Timer();
        bool bMouse = false;
        int x, y = 0;

        public form()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {

            StartTheTimer();
        }

        public void StartTheTimer()
        {
            if (string.IsNullOrEmpty(textBox1.Text) || string.IsNullOrEmpty(textBox2.Text))
                MessageBox.Show("Please enter wanted time in minutes to start the jiggler.Enter time in minutes and keys to be pressed to start the typing simulator. Read Help in menu strip for more information.", "Empty textboxes!");

            else
            {
                timer1.Enabled = true;

                timer1.Interval = int.Parse(textBox1.Text) * 60000; // *60000 so it will be converted in minutes

                timer1.Tick += new System.EventHandler(timer1_Tick);
            }

        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            if (!bMouse)
                SendKeys.Send(textBox2.Text);
            else
            {
                GetCurrentCursorPosition();
                if (Cursor.Position.X == x || Cursor.Position.X == y) //if user is currently moving his mouse dont fire the event
                    GenerateRandomCoordinates();

            }

        }
        public void GenerateRandomCoordinates()
        {
            Random rand = new Random();
            int x = rand.Next(0, 1000);
            int y = rand.Next(0, 1000);
            Cursor.Position = new Point(x, y);
        }

        public void GetCurrentCursorPosition()
        {
            x = Cursor.Position.X;
            y = Cursor.Position.Y;
        }

        private void button2_Click(object sender, EventArgs e)
        {
            timer1.Stop();
            bMouse = false;
            timer1.Enabled = false;
        }

        private void helpToolStripMenuItem1_Click(object sender, EventArgs e)
        {
            MessageBox.Show("In the time textbox enter time in minutes. In the key textbox enter the key you wish to be pressed. You can also enter special keys such as enter. Special keys should be written like this: {ENTER}. You can click on the test textbox to test out if the keys are being pressed.", "Help.");
        }

        private void aboutToolStripMenuItem_Click(object sender, EventArgs e)
        {
            MessageBox.Show("If you wanna apear online at work this is a great app for you.", "About.");
        }

        private void listOfSpecialKeysToolStripMenuItem_Click(object sender, EventArgs e)
        {
            System.Diagnostics.Process.Start("https://docs.microsoft.com/en-us/dotnet/api/system.windows.forms.sendkeys.send?view=net-5.0");
        }

        private void form_MouseMove(object sender, MouseEventArgs e)
        {
            label5.Text = "x:" + Cursor.Position.X + ", y: " + Cursor.Position.Y;

            //if (bMouse)  turn of the movement if user moves his mouse while the jiggler is enabled
            //    timer1.Stop();
        }

        private void button3_Click(object sender, EventArgs e)
        {
            timer1.Stop(); //stop the timer if its currently on

            bMouse = true;  //enable mouse movement

            StartTheTimer();
        }
    }
}
