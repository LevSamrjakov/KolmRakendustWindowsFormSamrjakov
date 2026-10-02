using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Drawing.Imaging;
using System.Windows.Forms;

namespace KolmRakendustWindowsFormSamrjakov
{
    public partial class PildiVaataja : Form
    {
        PictureBox pildiKast;
        Button nupp1, nupp2, nupp3, nupp4, nupp5, nupp6;
        CheckBox märkeruut;
        OpenFileDialog avaFailiDialoog;
        ColorDialog värviDialoog;
        SaveFileDialog salvestaFailiDialoog;

        public PildiVaataja(string pealkiri, int laius, int kõrgus, Color Taustavärv, Color Esiplaanivärv)
        {
            // Konstruktor
            Text = pealkiri;
            Size = new Size(laius, kõrgus);
            StartPosition = FormStartPosition.CenterScreen;
            BackColor = Taustavärv;
            ForeColor = Esiplaanivärv;

            // Peamine tabeliraam
            TableLayoutPanel peamineTabel = new TableLayoutPanel();

            peamineTabel.Dock = DockStyle.Fill;

            // Veerud
            peamineTabel.ColumnCount = 2;

            peamineTabel.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 15F));

            peamineTabel.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 85F));

            // Read
            peamineTabel.RowCount = 2;

            peamineTabel.RowStyles.Add(new RowStyle(SizeType.Percent, 90F));

            peamineTabel.RowStyles.Add(new RowStyle(SizeType.Percent, 10F));

            // Eraldus
            peamineTabel.CellBorderStyle = TableLayoutPanelCellBorderStyle.Single;

            Controls.Add(peamineTabel);

            // Pildikast
            pildiKast = new PictureBox();

            pildiKast.Dock = DockStyle.Fill;

            pildiKast.SizeMode = PictureBoxSizeMode.Zoom;

            pildiKast.BackColor = Taustavärv;

            pildiKast.BorderStyle = BorderStyle.Fixed3D;

            // Pildikast kasutab mõlemat veergu
            peamineTabel.SetColumnSpan(pildiKast, 2);

            peamineTabel.Controls.Add(pildiKast, 0, 0);

            // Märkeruut
            märkeruut = new CheckBox();

            märkeruut.Text = "Venita";
            märkeruut.AutoSize = true;

            märkeruut.Anchor = AnchorStyles.Left;

            märkeruut.CheckedChanged += Märkeruut_CheckedChanged;

            peamineTabel.Controls.Add(märkeruut, 0, 1);

            // Nuppude paneel
            FlowLayoutPanel nupuPaneel = new FlowLayoutPanel();

            nupuPaneel.Dock = DockStyle.Fill;

            nupuPaneel.FlowDirection = FlowDirection.RightToLeft;

            nupuPaneel.WrapContents = false;

            peamineTabel.Controls.Add(nupuPaneel, 1, 1);

            // Nupp 1
            nupp1 = new Button();

            nupp1.Text = "Vali pilt";
            nupp1.Size = new Size(120, 40);

            nupp1.Click += NäitaPiltiNupp_Click;

            nupuPaneel.Controls.Add(nupp1);

            // Nupp 2
            nupp2 = new Button();

            nupp2.Text = "Tühjenda pilt";
            nupp2.Size = new Size(120, 40);

            nupp2.Click += TühjendaPiltNupp_Click;

            nupuPaneel.Controls.Add(nupp2);

            // Nupp 3
            nupp3 = new Button();

            nupp3.Text = "Määra taustavärv";

            nupp3.Size = new Size(150, 40);

            nupp3.Click += TaustavärviNupp_Click;

            nupuPaneel.Controls.Add(nupp3);

            // Nupp 4
            nupp4 = new Button();
            nupp4.Text = "Salvesta pilt";
            nupp4.Size = new Size(120, 40);
            nupp4.Click += SalvestaPiltNupp_Click;
            nupuPaneel.Controls.Add(nupp4);

            // Nupp 5
            nupp5 = new Button();
            nupp5.Text = "Sulge";
            nupp5.Size = new Size(100, 40);
            nupp5.Click += SuleNupp_Click;
            nupuPaneel.Controls.Add(nupp5);

            // Nupp 6
            nupp6 = new Button();
            nupp6.Text = "Pildid";
            nupp6.Size = new Size(80, 40);
            nupp6.Click += PiltideMenüüNupp_Click;
            nupuPaneel.Controls.Add(nupp6);

            // Faili avamise dialoog
            avaFailiDialoog = new OpenFileDialog();
            avaFailiDialoog.Filter = "Pildifailid|*.jpg;*.jpeg;*.png;*.bmp;*.gif|" + "Kõik failid|*.*";

            // Värvidialoog
            värviDialoog = new ColorDialog();

            // Faili salvestamise dialoog
            salvestaFailiDialoog = new SaveFileDialog();
            salvestaFailiDialoog.Title = "Salvesta pilt";
            salvestaFailiDialoog.Filter = "PNG pilt|*.png|" + "JPEG pilt|*.jpg|" + "Bitmap pilt|*.bmp|" + "GIF pilt|*.gif";


            //// Juhuslikud pildid
            //Random juhuslik = new Random();

            //int juhuslikNumber = juhuslik.Next(1, 5);

            //Dictionary<int, string> Pildid = new Dictionary<int, string>();

            //Pildid.Add(1, "Image1");
            //Pildid.Add(2, "Image2");
            //Pildid.Add(3, "Image3");
            //Pildid.Add(4, "Image4");

            //string juhuslikPilt = Pildid[juhuslikNumber];

            //pildiKast.Image = Image.FromFile($@"..\..\Images\{juhuslikPilt}.jpg");

            // Seadete menüü
            MainMenu menüü = new MainMenu();

            MenuItem menüüSeaded = new MenuItem("Seaded");

            MenuItem menüüTumeTeema = new MenuItem("Tume teema", new EventHandler(TumeTeema));

            menüüSeaded.MenuItems.Add(menüüTumeTeema);

            MenuItem menüüHeleTeema = new MenuItem("Hele teema", new EventHandler(HeleTeema));

            menüüSeaded.MenuItems.Add(menüüHeleTeema);

            menüü.MenuItems.Add(menüüSeaded);

            Menu = menüü;
        }

        private void PiltideMenüüNupp_Click(object sender, EventArgs e)
        {
            PildiMenüü pildiMenüü = new PildiMenüü(pildiKast);
            pildiMenüü.ShowDialog();
        }

        private void NäitaPiltiNupp_Click(object sender, EventArgs e)
        {
            if (avaFailiDialoog.ShowDialog() == DialogResult.OK)
            {
                pildiKast.Load(avaFailiDialoog.FileName);

                LisaPiltKausta(avaFailiDialoog.FileName);
            }
        }

        private void LisaPiltKausta(string pildiTee)
        {
            string pildiKaust = Path.GetFullPath(@"..\..\Images");

            if (!Directory.Exists(pildiKaust))
            {
                Directory.CreateDirectory(pildiKaust);
            }

            string pildiNimi = Path.GetFileName(pildiTee);

            string uusPildiTee = Path.Combine(pildiKaust, pildiNimi);

            if (File.Exists(uusPildiTee))
            {
                return;
            }

            File.Copy(pildiTee, uusPildiTee);
        }

        private void TühjendaPiltNupp_Click(object sender, EventArgs e)
        {
            pildiKast.Image = null;
        }

        private void TaustavärviNupp_Click(object sender, EventArgs e)
        {
            if (värviDialoog.ShowDialog() == DialogResult.OK)
            {
                pildiKast.BackColor = värviDialoog.Color;
            }
        }

        private void SuleNupp_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void Märkeruut_CheckedChanged(object sender, EventArgs e)
        {
            if (märkeruut.Checked)
            {
                pildiKast.SizeMode = PictureBoxSizeMode.StretchImage;
            }
            else
            {
                pildiKast.SizeMode = PictureBoxSizeMode.Zoom;
            }
        }

        private void SalvestaPiltNupp_Click(object sender, EventArgs e)
        {
            if (pildiKast.Image == null)
            {
                MessageBox.Show("Salvestatavat pilti pole.");
                return;
            }

            if (salvestaFailiDialoog.ShowDialog() == DialogResult.OK)
            {
                ImageFormat formaat = ImageFormat.Png;

                if (salvestaFailiDialoog.FilterIndex == 1)
                {
                    formaat = ImageFormat.Png;
                }
                else if (salvestaFailiDialoog.FilterIndex == 2)
                {
                    formaat = ImageFormat.Jpeg;
                }
                else if (salvestaFailiDialoog.FilterIndex == 3)
                {
                    formaat = ImageFormat.Bmp;
                }
                else if (salvestaFailiDialoog.FilterIndex == 4)
                {
                    formaat = ImageFormat.Gif;
                }

                pildiKast.Image.Save(salvestaFailiDialoog.FileName, formaat);
            }
        }

        private void TumeTeema(object sender, EventArgs e)
        {
            BackColor = Color.Black;
            ForeColor = Color.White;
            pildiKast.BackColor = Color.Black;
        }

        private void HeleTeema(object sender, EventArgs e)
        {
            BackColor = Color.White;
            ForeColor = Color.Black;
            pildiKast.BackColor = Color.White;
        }
    }
}