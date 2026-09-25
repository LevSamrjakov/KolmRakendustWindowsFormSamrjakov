using System;
using System.Drawing;
using System.Windows.Forms;

namespace KolmRakendustWindowsFormSamrjakov
{
    public partial class StartingMenu : Form
    {
        Button button1, button2, button3, button4;
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
            title.Location = new Point(150, 75);
            title.Size = new Size(300, 70);
            title.Font = new Font("Arial", 30, FontStyle.Bold);
            title.TextAlign = ContentAlignment.MiddleCenter;
            title.AutoSize = false;
            Controls.Add(title);

            // 1. Button: Image Viewer
            button1 = new Button();
            button1.Text = "Image Viewer";
            button1.Font = new Font("Arial", 10, FontStyle.Bold);
            button1.Location = new Point(200, 200);
            button1.Height = 50;
            button1.Width = 200;
            button1.Click += OpenImageViewer;
            Controls.Add(button1);

            // 2. Button: Math Quiz
            button2 = new Button();
            button2.Text = "Math Quiz";
            button2.Font = new Font("Arial", 10, FontStyle.Bold);
            button2.Location = new Point(200, 275);
            button2.Height = 50;
            button2.Width = 200;
            button2.Click += OpenMathQuiz;
            Controls.Add(button2);

            // 3. Button: Image Pairs Game
            button3 = new Button();
            button3.Text = "Image Pairs Game";
            button3.Font = new Font("Arial", 10, FontStyle.Bold);
            button3.Location = new Point(200, 350);
            button3.Height = 50;
            button3.Width = 200;
            button3.Click += OpenImageMatchGame;
            Controls.Add(button3);

            // 4. Button: Exit
            button4 = new Button();
            button4.Text = "Exit";
            button4.Font = new Font("Arial", 10, FontStyle.Bold);
            button4.Location = new Point(200, 425);
            button4.Height = 50;
            button4.Width = 200;
            button4.Click += ExitApplication;
            Controls.Add(button4);

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

            title.ForeColor = Color.White;

            button1.BackColor = Color.Black;
            button1.ForeColor = Color.White;

            button2.BackColor = Color.Black;
            button2.ForeColor = Color.White;

            button3.BackColor = Color.Black;
            button3.ForeColor = Color.White;

            button4.BackColor = Color.Black;
            button4.ForeColor = Color.White;
        }

        private void BrightTheme(object sender, EventArgs e)
        {
            Backcolor = BackColor = Color.White;
            Frontcolor = ForeColor = Color.Black;

            title.ForeColor = Color.Black;

            button1.BackColor = Color.White;
            button1.ForeColor = Color.Black;

            button2.BackColor = Color.White;
            button2.ForeColor = Color.Black;

            button3.BackColor = Color.White;
            button3.ForeColor = Color.Black;

            button4.BackColor = Color.White;
            button4.ForeColor = Color.Black;
        }

        private void OpenImageViewer(object sender, EventArgs e)
        {
            ImageViewer imageViewer = new ImageViewer("Image Viewer", 1000, 600, Backcolor, Frontcolor);
            imageViewer.Show();
        }

        private void OpenMathQuiz(object sender, EventArgs e)
        {
            MathQuiz mathQuiz = new MathQuiz("Math Quiz", 600, 600, Backcolor, Frontcolor);
            mathQuiz.Show();
        }

        private void OpenImageMatchGame(object sender, EventArgs e)
        {
            ImageMatch imageMatch = new ImageMatch("Image Matcher", 600, 600);
            imageMatch.Show();
        }

        private void ExitApplication(object sender, EventArgs e)
        {
            Application.Exit();
        }
    }
}
