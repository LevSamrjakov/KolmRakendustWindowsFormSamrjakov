using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace KolmRakendustWindowsFormSamrjakov
{
    public partial class ImageViewer : Form
    {
        PictureBox pictureBox;
        Button button1, button2, button3;
        //CheckBox checkBox;
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
            button1.Text = "New Image";
            button1.Size = new Size(100, 40);
            button1.Click += RandomImageButton_Click;

            button2 = new Button();
            button2.Text = "Clear Image";
            button2.Size = new Size(100, 40);
            button2.Click += ClearImageButton_Click;

            button3 = new Button();
            button3.Text = "Close";
            button3.Size = new Size(100, 40);
            button3.Click += CloseButton_Click;

            buttonPanel.Controls.Add(button1);
            buttonPanel.Controls.Add(button2);
            buttonPanel.Controls.Add(button3);

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

            //checkBox = new CheckBox();
            //checkBox.Text = "Stretch";
            //checkBox.Size = new Size(50, 50);
            //checkBox.Location = new Point(20, 500);

            // Random Images
            Random random = new Random();
            int RandomNumber = random.Next(1, 5);
            Dictionary<int, string> Images = new Dictionary<int, string>();
            Images.Add(1, "Image1");
            Images.Add(2, "Image2");
            Images.Add(3, "Image3");
            Images.Add(4, "Image4");
            string RandomImage = Images[RandomNumber];

            // Picture Box
            pictureBox = new PictureBox();
            pictureBox.Dock = DockStyle.Fill;
            pictureBox.SizeMode = PictureBoxSizeMode.Zoom;
            pictureBox.Image = Image.FromFile($@"..\..\Images\{RandomImage}.jpg");
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

        private void CloseButton_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void ClearImageButton_Click(object sender, EventArgs e)
        {
            pictureBox.Image = null;
        }

        private void RandomImageButton_Click(object sender, EventArgs e)
        {
            Random random = new Random();
            int RandomNumber = random.Next(1, 5);
            Dictionary<int, string> Images = new Dictionary<int, string>();
            Images.Add(1, "Image1");
            Images.Add(2, "Image2");
            Images.Add(3, "Image3");
            Images.Add(4, "Image4");
            string RandomImage = Images[RandomNumber];
            pictureBox.Image = Image.FromFile($@"..\..\Images\{RandomImage}.jpg");
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
