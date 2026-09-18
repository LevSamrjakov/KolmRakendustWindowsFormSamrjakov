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
    public partial class StartingMenu : Form
    {
        Button button1, button2, button3;
        Label title;
        Color Backcolor, Frontcolor;
        public StartingMenu()
        {
            // Window Size
            Height = 600;
            Width = 600;

            // Window Starting Position
            StartPosition = FormStartPosition.CenterScreen;

            // Window Text / Name
            Text = "Main Menu";

            // Window Theme / Colors
            Backcolor = BackColor = Color.White;
            Frontcolor = ForeColor = Color.Black;

            // Title
            title = new Label();
            title.Text = "Main Menu";
            title.Location = new Point(180, 100);
            title.Size = new Size(200, 200);
            title.Font = new Font("Arial", 30, FontStyle.Bold);
            title.AutoSize = true;
            Controls.Add(title);

            // 1. Button: Image Viewer
            button1 = new Button();
            button1.Text = "Image Viewer";
            button1.Font = new Font("Arial", 10, FontStyle.Bold);
            button1.Location = new Point(210, 175);
            button1.Height = 50;
            button1.Width = 150;
            button1.Click += OpenImageViewer;
            Controls.Add(button1);

            // 2. Button: Math Quiz
            button2 = new Button();
            button2.Text = "Math Quiz";
            button2.Font = new Font("Arial", 10, FontStyle.Bold);
            button2.Location = new Point(210, 250);
            button2.Height = 50;
            button2.Width = 150;
            button2.Click += OpenMathQuiz;
            Controls.Add(button2);

            // 3. Button: Image Pairs Game
            button3 = new Button();
            button3.Text = "Image Pairs Game";
            button3.Font = new Font("Arial", 10, FontStyle.Bold);
            button3.Location = new Point(210, 325);
            button3.Height = 50;
            button3.Width = 150;
            button3.Click += OpenImagePairsGame;
            Controls.Add(button3);

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
            Backcolor = BackColor = Color.Black;
            Frontcolor = ForeColor = Color.White;
        }

        private void BrightTheme(object sender, EventArgs e)
        {
            Backcolor = BackColor = Color.White;
            Frontcolor = ForeColor = Color.Black;
        }

        private void OpenImageViewer(object sender, EventArgs e)
        {
            ImageViewer imageViewer = new ImageViewer("Image Viewer", 1000, 600, Backcolor, Frontcolor);
            imageViewer.Show();
            //Close();
        }

        private void OpenMathQuiz(object sender, EventArgs e)
        {
            MathQuiz mathQuiz = new MathQuiz("Math Quiz", 600, 600, Backcolor, Frontcolor);
            mathQuiz.Show();
        }

        private void OpenImagePairsGame(object sender, EventArgs e)
        {

        }

    }
}
