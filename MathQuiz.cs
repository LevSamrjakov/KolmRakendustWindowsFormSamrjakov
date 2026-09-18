using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace KolmRakendustWindowsFormSamrjakov
{
    public partial class MathQuiz : Form
    {
        Label timer, timerLabel;

        Label RightLabel, LeftLabel, ResultLabel;
        public MathQuiz(string title, int width, int height, Color Backcolor, Color Frontcolor)
        {
            // Konstruktor
            Text = title;
            Size = new Size(width, height);
            StartPosition = FormStartPosition.CenterScreen;
            BackColor = Backcolor;
            ForeColor = Frontcolor;

            // Timer
            timer = new Label();
            timer.AutoSize = false;
            timer.BorderStyle = BorderStyle.FixedSingle;
            timer.Size = new Size(200, 30);
            timer.Font = new Font("Arial", 15.75f);
            timer.Location = new Point(375, 15);
            

            timerLabel = new Label();
            timerLabel.Text = "Time Left";
            timerLabel.Font = new Font("Arial", 15.75f);
            timerLabel.Location = new Point(275, 15);

            Controls.Add(timer);
            Controls.Add(timerLabel);

            //Math
            //RightLabel = new Label();
            //RightLabel.Text = "?";
            //RightLabel.AutoSize = false;
            //RightLabel.Size = new Size(60, 50);
            //RightLabel.Font = new Font("Arial", 18f);
            //RightLabel.TextAlign = ContentAlignment.MiddleCenter;
            //RightLabel.Location = new Point(100, 100);

            //LeftLabel = new Label();
            //LeftLabel.Text = "?";
            //LeftLabel.AutoSize = false;
            //LeftLabel.Size = new Size(60, 50);
            //LeftLabel.Font = new Font("Arial", 18f);
            //LeftLabel.TextAlign = ContentAlignment.MiddleCenter;
            //LeftLabel.Location = new Point(200, 100);

            //Controls.Add(RightLabel);
            //Controls.Add(LeftLabel);

            // Settings
            System.Windows.Forms.MainMenu menu = new System.Windows.Forms.MainMenu();
            MenuItem menuSettings = new MenuItem("Settings");
            MenuItem menuDarkTheme = new MenuItem("Dark Theme", new EventHandler(DarkTheme));
            menuSettings.MenuItems.Add(menuDarkTheme);
            MenuItem menuBrightTheme = new MenuItem("Bright Theme", new EventHandler(BrightTheme));
            menuSettings.MenuItems.Add(menuBrightTheme);
            menu.MenuItems.Add(menuSettings);
            Menu = menu;
        }

        private void DarkTheme(object sender, EventArgs e)
        {
            BackColor = Color.Black;
            ForeColor = Color.White;
        }

        private void BrightTheme(object sender, EventArgs e)
        {
            BackColor = Color.White;
            ForeColor = Color.Black;
        }
    }
}
