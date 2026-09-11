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

namespace KolmRakendustWindowsFormSamrjakov
{
    public partial class Form1 : Form
    {
        Button button1, button2, button3;
        Label title;
        public Form1()
        {
            Height = 600;
            Width = 600;
            Text = "Main Menu";

            title = new Label();
            title.Text = "Main Menu";
            title.Location = new Point(180, 100);
            title.Size = new Size(200, 200);
            title.Font = new Font("Arial", 30, FontStyle.Bold);
            title.AutoSize = true;
            Controls.Add(title);

            button1 = new Button();
            button1.Text = "Image Viewer";
            button1.Font = new Font("Arial", 10, FontStyle.Bold);
            button1.Location = new Point(210, 175);
            button1.Height = 50;
            button1.Width = 150;
            button1.Click += (sender, e) => { MessageBox.Show("Button 1 clicked!"); };
            Controls.Add(button1);

            button2 = new Button();
            button2.Text = "Math Quiz";
            button2.Font = new Font("Arial", 10, FontStyle.Bold);
            button2.Location = new Point(210, 250);
            button2.Height = 50;
            button2.Width = 150;
            button2.Click += (sender, e) => { MessageBox.Show("Button 2 clicked!"); };
            Controls.Add(button2);

            button3 = new Button();
            button3.Text = "Image Pairs Game";
            button3.Font = new Font("Arial", 10, FontStyle.Bold);
            button3.Location = new Point(210, 325);
            button3.Height = 50;
            button3.Width = 150;
            button3.Click += (sender, e) => { MessageBox.Show("Button 3 clicked!"); };
            Controls.Add(button3);

            MainMenu menu = new MainMenu();
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
