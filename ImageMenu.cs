using System;
using System.Drawing;
using System.IO;
using System.Windows.Forms;

namespace KolmRakendustWindowsFormSamrjakov
{
    public partial class ImageMenu : Form
    {
        PictureBox pictureBox;
        FlowLayoutPanel imagePanel;

        public ImageMenu(PictureBox mainPictureBox)
        {
            Text = "Images";
            Size = new Size(600, 400);
            StartPosition = FormStartPosition.CenterScreen;

            pictureBox = mainPictureBox;

            // Image Panel
            imagePanel = new FlowLayoutPanel();
            imagePanel.Dock = DockStyle.Fill;
            imagePanel.AutoScroll = true;

            Controls.Add(imagePanel);

            // Images folder
            string imageFolder = Path.GetFullPath(@"..\..\Images");

            if (Directory.Exists(imageFolder))
            {
                string[] images = Directory.GetFiles(imageFolder, "*.*");

                foreach (string image in images)
                {
                    if (image.EndsWith(".jpg") ||
                        image.EndsWith(".jpeg") ||
                        image.EndsWith(".png") ||
                        image.EndsWith(".bmp") ||
                        image.EndsWith(".gif"))
                    {
                        PictureBox imageBox = new PictureBox();

                        imageBox.Size = new Size(120, 100);
                        imageBox.SizeMode = PictureBoxSizeMode.Zoom;
                        imageBox.BorderStyle = BorderStyle.FixedSingle;
                        imageBox.Cursor = Cursors.Hand;

                        imageBox.Image = Image.FromFile(image);
                        imageBox.Tag = image;

                        imageBox.Click += ImageBox_Click;

                        imagePanel.Controls.Add(imageBox);
                    }
                }
            }
        }

        private void ImageBox_Click(object sender, EventArgs e)
        {
            PictureBox imageBox = (PictureBox)sender;

            string imagePath = (string)imageBox.Tag;

            pictureBox.Load(imagePath);

            Close();
        }
    }
}