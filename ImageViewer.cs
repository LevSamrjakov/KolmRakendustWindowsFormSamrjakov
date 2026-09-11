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
    public partial class ImageViewer : Form
    {
        PictureBox pictureBox;
        Button button1, button2, button3, button4;
        public ImageViewer(string title, int width, int height, Color Backcolor, Color Frontcolor)
        {
            // Konstruktor
            Text = title;
            Size = new Size(width, height);
            StartPosition = FormStartPosition.CenterScreen;
            BackColor = Backcolor;
            ForeColor = Frontcolor;

            // Main Table Layout
            TableLayoutPanel mainTableLayout = new TableLayoutPanel();
            mainTableLayout.Dock = DockStyle.Fill;

            // Rows
            mainTableLayout.RowCount = 2;
            mainTableLayout.RowStyles.Add(new RowStyle(SizeType.Percent, 95F));
            mainTableLayout.RowStyles.Add(new RowStyle(SizeType.Percent, 15F));

            // Separation
            mainTableLayout.CellBorderStyle = TableLayoutPanelCellBorderStyle.Single;

            Controls.Add(mainTableLayout);

            // Lower Table Layout
            TableLayoutPanel lowerTableLayout = new TableLayoutPanel();
            lowerTableLayout.Dock = DockStyle.Fill;

            mainTableLayout.Controls.Add(lowerTableLayout, 0, 0);

            // Buttons Panel
            FlowLayoutPanel buttonPanel = new FlowLayoutPanel();

            buttonPanel.Dock = DockStyle.Fill;

            button1 = new Button();
            button1.Text = "Button 1";
            button1.Size = new Size(100, 40);

            button2 = new Button();
            button2.Text = "Button 2";
            button2.Size = new Size(100, 40);

            button3 = new Button();
            button3.Text = "Button 3";
            button3.Size = new Size(100, 40);

            button4 = new Button();
            button4.Text = "Button 4";
            button4.Size = new Size(100, 40);

            buttonPanel.Controls.Add(button1);
            buttonPanel.Controls.Add(button2);
            buttonPanel.Controls.Add(button3);
            buttonPanel.Controls.Add(button4);

            buttonPanel.FlowDirection = FlowDirection.RightToLeft;

            buttonPanel.WrapContents = false;

            lowerTableLayout.Controls.Add(buttonPanel, 1, 0);

            // Buttons
            //Button menuButton = new Button();
            //menuButton.Size = new Size(100, 50);
            //menuButton.Location = new Point(550, 100);
            //menuButton.Text = "Exit to Main Menu";
            //menuButton.Click += OpenMainMenu;
            //Controls.Add(menuButton);

            // Label
            Label labeltext = new Label();
            labeltext.Text = "Test";
            labeltext.Dock = DockStyle.Fill;
            labeltext.TextAlign = ContentAlignment.MiddleCenter;
            labeltext.Font = new Font("Arial", 20, FontStyle.Bold);

            Controls.Add(labeltext);

            // Picture Box
            pictureBox = new PictureBox();
            pictureBox.Dock = DockStyle.Fill;
            pictureBox.SizeMode = PictureBoxSizeMode.Zoom;
            pictureBox.Image = Image.FromFile(@"..\..\Images\LordSolarMacharius.jpg");
            pictureBox.BackColor = Backcolor;

            mainTableLayout.Controls.Add(pictureBox, 0, 0);

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
            pictureBox.BackColor = Color.Black;
        }

        private void BrightTheme(object sender, EventArgs e)
        {
            BackColor = Color.White;
            ForeColor = Color.Black;
            pictureBox.BackColor = Color.White;
        }

        //private void OpenMainMenu(object sender, EventArgs e)
        //{
        //    StartingMenu startingMenu = new StartingMenu();
        //    startingMenu.Show();

        //}
    }
}
