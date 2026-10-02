using System;
using System.Drawing;
using System.Windows.Forms;

namespace KolmRakendustWindowsFormSamrjakov
{
    public partial class AlgusMenüü : Form
    {
        Button nupp1, nupp2, nupp3, nupp4;
        Label pealkiri;
        Color Taustavärv, Esiplaanivärv;

        public AlgusMenüü()
        {
            // Akna suurus
            Height = 600;
            Width = 600;

            // Akna algpositsioon
            StartPosition = FormStartPosition.CenterScreen;

            // Akna tekst / nimi
            Text = "Peamenüü";

            // Akna teema / värvid
            Taustavärv = BackColor = Color.White;
            Esiplaanivärv = ForeColor = Color.Black;

            // Pealkiri
            pealkiri = new Label();
            pealkiri.Text = "Peamenüü";
            pealkiri.Location = new Point(150, 75);
            pealkiri.Size = new Size(300, 70);
            pealkiri.Font = new Font("Arial", 30, FontStyle.Bold);
            pealkiri.TextAlign = ContentAlignment.MiddleCenter;
            pealkiri.AutoSize = false;
            Controls.Add(pealkiri);

            // 1. Nupp: Pildivaataja
            nupp1 = new Button();
            nupp1.Text = "Pildivaataja";
            nupp1.Font = new Font("Arial", 10, FontStyle.Bold);
            nupp1.Location = new Point(200, 200);
            nupp1.Height = 50;
            nupp1.Width = 200;
            nupp1.Click += AvaPildiVaataja;
            Controls.Add(nupp1);

            // 2. Nupp: Matemaatikaviktoriin
            nupp2 = new Button();
            nupp2.Text = "Matemaatikaviktoriin";
            nupp2.Font = new Font("Arial", 10, FontStyle.Bold);
            nupp2.Location = new Point(200, 275);
            nupp2.Height = 50;
            nupp2.Width = 200;
            nupp2.Click += AvaMatemaatikaviktoriin;
            Controls.Add(nupp2);

            // 3. Nupp: Piltide paaride mäng
            nupp3 = new Button();
            nupp3.Text = "Piltide paaride mäng";
            nupp3.Font = new Font("Arial", 10, FontStyle.Bold);
            nupp3.Location = new Point(200, 350);
            nupp3.Height = 50;
            nupp3.Width = 200;
            nupp3.Click += AvaPiltidePaarideMäng;
            Controls.Add(nupp3);

            // 4. Nupp: Välju
            nupp4 = new Button();
            nupp4.Text = "Välju";
            nupp4.Font = new Font("Arial", 10, FontStyle.Bold);
            nupp4.Location = new Point(200, 425);
            nupp4.Height = 50;
            nupp4.Width = 200;
            nupp4.Click += VäljuRakendusest;
            Controls.Add(nupp4);

            // Seaded
            System.Windows.Forms.MainMenu menüü = new System.Windows.Forms.MainMenu();

            MenuItem menüüSeaded = new MenuItem("Seaded");

            MenuItem menüüTumeTeema = new MenuItem("Tume teema", new EventHandler(TumeTeema));
            menüüSeaded.MenuItems.Add(menüüTumeTeema);

            MenuItem menüüHeleTeema = new MenuItem("Hele teema", new EventHandler(HeleTeema));
            menüüSeaded.MenuItems.Add(menüüHeleTeema);

            menüü.MenuItems.Add(menüüSeaded);

            Menu = menüü;
        }

        private void TumeTeema(object sender, EventArgs e)
        {
            Taustavärv = BackColor = Color.Black;
            Esiplaanivärv = ForeColor = Color.White;

            pealkiri.ForeColor = Color.White;

            nupp1.BackColor = Color.Black;
            nupp1.ForeColor = Color.White;

            nupp2.BackColor = Color.Black;
            nupp2.ForeColor = Color.White;

            nupp3.BackColor = Color.Black;
            nupp3.ForeColor = Color.White;

            nupp4.BackColor = Color.Black;
            nupp4.ForeColor = Color.White;
        }

        private void HeleTeema(object sender, EventArgs e)
        {
            Taustavärv = BackColor = Color.White;
            Esiplaanivärv = ForeColor = Color.Black;

            pealkiri.ForeColor = Color.Black;

            nupp1.BackColor = Color.White;
            nupp1.ForeColor = Color.Black;

            nupp2.BackColor = Color.White;
            nupp2.ForeColor = Color.Black;

            nupp3.BackColor = Color.White;
            nupp3.ForeColor = Color.Black;

            nupp4.BackColor = Color.White;
            nupp4.ForeColor = Color.Black;
        }

        private void AvaPildiVaataja(object sender, EventArgs e)
        {
            PildiVaataja pildiVaataja = new PildiVaataja("Pildivaataja", 1000, 600, Taustavärv, Esiplaanivärv);
            pildiVaataja.Show();
        }

        private void AvaMatemaatikaviktoriin(object sender, EventArgs e)
        {
            Matemaatikaviktoriin matemaatikaviktoriin = new Matemaatikaviktoriin("Matemaatikaviktoriin", 600, 600, Taustavärv, Esiplaanivärv);
            matemaatikaviktoriin.Show();
        }

        private void AvaPiltidePaarideMäng(object sender, EventArgs e)
        {
            PildiPaarid pildiPaarid = new PildiPaarid("Piltide paarid", 600, 600);
            pildiPaarid.Show();
        }

        private void VäljuRakendusest(object sender, EventArgs e)
        {
            Application.Exit();
        }
    }
}