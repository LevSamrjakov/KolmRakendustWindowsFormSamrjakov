using System;
using System.Drawing;
using System.IO;
using System.Windows.Forms;

namespace KolmRakendustWindowsFormSamrjakov
{
    public partial class PildiMenüü : Form
    {
        PictureBox pildiKast;
        FlowLayoutPanel pildiPaneel;

        public PildiMenüü(PictureBox peamineKast)
        {
            Text = "Pildid";
            Size = new Size(600, 400);
            StartPosition = FormStartPosition.CenterScreen;

            pildiKast = peamineKast;

            // Pildipaneel
            pildiPaneel = new FlowLayoutPanel();
            pildiPaneel.Dock = DockStyle.Fill;
            pildiPaneel.AutoScroll = true;

            Controls.Add(pildiPaneel);

            // Piltide kaust
            string pildiKaust = Path.GetFullPath(@"..\..\Images");

            if (Directory.Exists(pildiKaust))
            {
                string[] pildid = Directory.GetFiles(pildiKaust, "*.*");

                foreach (string pilt in pildid)
                {
                    if (pilt.EndsWith(".jpg") ||
                        pilt.EndsWith(".jpeg") ||
                        pilt.EndsWith(".png") ||
                        pilt.EndsWith(".bmp") ||
                        pilt.EndsWith(".gif"))
                    {
                        PictureBox pildiKastike = new PictureBox();

                        pildiKastike.Size = new Size(120, 100);
                        pildiKastike.SizeMode = PictureBoxSizeMode.Zoom;
                        pildiKastike.BorderStyle = BorderStyle.FixedSingle;
                        pildiKastike.Cursor = Cursors.Hand;

                        pildiKastike.Image = Image.FromFile(pilt);
                        pildiKastike.Tag = pilt;

                        pildiKastike.Click += PildiKastike_Click;

                        pildiPaneel.Controls.Add(pildiKastike);
                    }
                }
            }
        }

        private void PildiKastike_Click(object sender, EventArgs e)
        {
            PictureBox pildiKastike = (PictureBox)sender;

            string pildiTee = (string)pildiKastike.Tag;

            pildiKast.Load(pildiTee);

            Close();
        }
    }
}