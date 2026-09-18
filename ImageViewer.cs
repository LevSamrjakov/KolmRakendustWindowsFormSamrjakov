using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Drawing.Imaging;
using System.Windows.Forms;

namespace KolmRakendustWindowsFormSamrjakov
{
    public partial class ImageViewer : Form
    {
        PictureBox pictureBox;
        Button button1, button2, button3, button4, button5, button6;
        CheckBox checkBox;
        OpenFileDialog openFileDialog;
        ColorDialog colorDialog;
        SaveFileDialog saveFileDialog;

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

            // Columns
            mainTableLayout.ColumnCount = 2;

            mainTableLayout.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 15F));

            mainTableLayout.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 85F));

            // Rows
            mainTableLayout.RowCount = 2;

            mainTableLayout.RowStyles.Add(new RowStyle(SizeType.Percent, 90F));

            mainTableLayout.RowStyles.Add(new RowStyle(SizeType.Percent, 10F));

            // Separation
            mainTableLayout.CellBorderStyle = TableLayoutPanelCellBorderStyle.Single;

            Controls.Add(mainTableLayout);

            // Picture Box
            pictureBox = new PictureBox();

            pictureBox.Dock = DockStyle.Fill;

            pictureBox.SizeMode = PictureBoxSizeMode.Zoom;

            pictureBox.BackColor = Backcolor;

            pictureBox.BorderStyle = BorderStyle.Fixed3D;

            // PictureBox uses both columns
            mainTableLayout.SetColumnSpan(pictureBox, 2);

            mainTableLayout.Controls.Add(pictureBox, 0, 0);

            // CheckBox
            checkBox = new CheckBox();

            checkBox.Text = "Stretch";
            checkBox.AutoSize = true;

            checkBox.Anchor = AnchorStyles.Left;

            checkBox.CheckedChanged += CheckBox_CheckedChanged;

            mainTableLayout.Controls.Add(checkBox, 0, 1);

            // Buttons Panel
            FlowLayoutPanel buttonPanel = new FlowLayoutPanel();

            buttonPanel.Dock = DockStyle.Fill;

            buttonPanel.FlowDirection = FlowDirection.RightToLeft;

            buttonPanel.WrapContents = false;

            mainTableLayout.Controls.Add(buttonPanel, 1, 1);

            // Button 1
            button1 = new Button();

            button1.Text = "Select a picture";
            button1.Size = new Size(120, 40);

            button1.Click += ShowPictureButton_Click;

            buttonPanel.Controls.Add(button1);

            // Button 2
            button2 = new Button();

            button2.Text = "Clear the picture";
            button2.Size = new Size(120, 40);

            button2.Click += ClearImageButton_Click;

            buttonPanel.Controls.Add(button2);

            // Button 3
            button3 = new Button();

            button3.Text = "Set background color";

            button3.Size = new Size(150, 40);

            button3.Click += BackgroundColorButton_Click;

            buttonPanel.Controls.Add(button3);

            // Button 4
            button4 = new Button();
            button4.Text = "Save picture";
            button4.Size = new Size(120, 40);
            button4.Click += SavePictureButton_Click;
            buttonPanel.Controls.Add(button4);

            // Button 5
            button5 = new Button();
            button5.Text = "Close";
            button5.Size = new Size(100, 40);
            button5.Click += CloseButton_Click;
            buttonPanel.Controls.Add(button5);

            // Button 6
            button6 = new Button();
            button6.Text = "Images";
            button6.Size = new Size(80, 40);
            button6.Click += ImageMenuButton_Click;
            buttonPanel.Controls.Add(button6);

            // Open File Dialog
            openFileDialog = new OpenFileDialog();
            openFileDialog.Filter = "Image Files|*.jpg;*.jpeg;*.png;*.bmp;*.gif|" + "All Files|*.*";

            // Color Dialog
            colorDialog = new ColorDialog();

            // Save File Dialog
            saveFileDialog = new SaveFileDialog();
            saveFileDialog.Title = "Save picture";
            saveFileDialog.Filter = "PNG Image|*.png|" + "JPEG Image|*.jpg|" + "Bitmap Image|*.bmp|" + "GIF Image|*.gif";


            //// Random Images
            //Random random = new Random();

            //int RandomNumber = random.Next(1, 5);

            //Dictionary<int, string> Images = new Dictionary<int, string>();

            //Images.Add(1, "Image1");
            //Images.Add(2, "Image2");
            //Images.Add(3, "Image3");
            //Images.Add(4, "Image4");

            //string RandomImage = Images[RandomNumber];

            //pictureBox.Image = Image.FromFile($@"..\..\Images\{RandomImage}.jpg");

            // Settings Menu
            MainMenu menu = new MainMenu();

            MenuItem menuSettings = new MenuItem("Settings");

            MenuItem menuDarkTheme = new MenuItem("Dark Theme", new EventHandler(DarkTheme));

            menuSettings.MenuItems.Add(menuDarkTheme);

            MenuItem menuBrightTheme = new MenuItem("Bright Theme", new EventHandler(BrightTheme));

            menuSettings.MenuItems.Add(menuBrightTheme);

            menu.MenuItems.Add(menuSettings);

            Menu = menu;
        }

        private void ImageMenuButton_Click(object sender, EventArgs e)
        {
            ImageMenu imageMenu = new ImageMenu(pictureBox);
            imageMenu.ShowDialog();
        }

        private void ShowPictureButton_Click(object sender, EventArgs e)
        {
            if (openFileDialog.ShowDialog() == DialogResult.OK)
            {
                pictureBox.Load(openFileDialog.FileName);

                AddImageToFolder(openFileDialog.FileName);
            }
        }

        private void AddImageToFolder(string imagePath)
        {
            string imageFolder = Path.GetFullPath(@"..\..\Images");

            if (!Directory.Exists(imageFolder))
            {
                Directory.CreateDirectory(imageFolder);
            }

            string imageName = Path.GetFileName(imagePath);

            string newImagePath = Path.Combine(imageFolder, imageName);

            if (File.Exists(newImagePath))
            {
                return;
            }

            File.Copy(imagePath, newImagePath);
        }

        private void ClearImageButton_Click(object sender, EventArgs e)
        {
            pictureBox.Image = null;
        }

        private void BackgroundColorButton_Click(object sender, EventArgs e)
        {
            if (colorDialog.ShowDialog() == DialogResult.OK)
            {
                pictureBox.BackColor = colorDialog.Color;
            }
        }

        private void CloseButton_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void CheckBox_CheckedChanged(object sender, EventArgs e)
        {
            if (checkBox.Checked)
            {
                pictureBox.SizeMode = PictureBoxSizeMode.StretchImage;
            }
            else
            {
                pictureBox.SizeMode = PictureBoxSizeMode.Zoom;
            }
        }

        private void SavePictureButton_Click(object sender, EventArgs e)
        {
            if (pictureBox.Image == null)
            {
                MessageBox.Show("There is no image to save.");
                return;
            }

            if (saveFileDialog.ShowDialog() == DialogResult.OK)
            {
                ImageFormat format = ImageFormat.Png;

                if (saveFileDialog.FilterIndex == 1)
                {
                    format = ImageFormat.Png;
                }
                else if (saveFileDialog.FilterIndex == 2)
                {
                    format = ImageFormat.Jpeg;
                }
                else if (saveFileDialog.FilterIndex == 3)
                {
                    format = ImageFormat.Bmp;
                }
                else if (saveFileDialog.FilterIndex == 4)
                {
                    format = ImageFormat.Gif;
                }

                pictureBox.Image.Save(saveFileDialog.FileName, format);
            }
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
    }
}
