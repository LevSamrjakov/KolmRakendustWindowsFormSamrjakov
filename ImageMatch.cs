using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Windows.Forms;

namespace KolmRakendustWindowsFormSamrjakov
{
    public partial class PildiPaarid : Form
    {
        // Peamenüü
        Label pealkiri;

        Button alustaMänguNupp, seadeteNupp;

        // Seaded
        Label suurusLabel, parameetridLabel;
        Label praeguSuurusLabel;

        Button suurus4Nupp, suurus8Nupp;
        Button muudaPilteNupp, tagasiNupp;

        // Mäng
        TableLayoutPanel mängTabel;

        Label scoorLabel, paaridLabel;

        Button peaMenüüNupp;

        // Mängu andmed
        int lauaSuurus = 4;
        int skoor = 3200;
        int leitudPaare = 0;

        List<string> pildiTeed = new List<string>();
        List<PictureBox> kaardid = new List<PictureBox>();
        List<int> kaardiVäärtused = new List<int>();
        List<bool> sobitatudKaardid = new List<bool>();

        PictureBox esimeneKaart;
        PictureBox teineKaart;

        int esimeneKaartIndeks = -1;
        int teineKaartIndeks = -1;

        // Taimer
        Timer peidaTaimer;

        // Juhuslik
        Random juhuslik = new Random();

        // Teema
        Color Taustavärv = Color.White;
        Color Esiplaanivärv = Color.Black;

        // Konstruktor

        public PildiPaarid(string pealkirjaTekst, int laius, int kõrgus)
        {
            Text = pealkirjaTekst;
            Size = new Size(laius, kõrgus);
            StartPosition = FormStartPosition.CenterScreen;

            // Vormi seaded
            FormBorderStyle = FormBorderStyle.Fixed3D;
            MaximizeBox = false;

            // Vaikimisi pildid

            LaadiVaikimisiPildid();

            // Taimer
            peidaTaimer = new Timer();
            peidaTaimer.Interval = 700;
            peidaTaimer.Tick += PeidaTaimer_Tick;

            // Peamenüü
            NäitaPeaMenüüd();

            // Menüü
            MainMenu menüü = new MainMenu();
            MenuItem menüüSeaded = new MenuItem("Seaded");
            MenuItem menüüTumeTeema = new MenuItem("Tume teema", TumeTeema);
            MenuItem menüüHeleTeema = new MenuItem("Hele teema", HeleTeema);
            menüüSeaded.MenuItems.Add(menüüTumeTeema);
            menüüSeaded.MenuItems.Add(menüüHeleTeema);
            menüü.MenuItems.Add(menüüSeaded);
            Menu = menüü;
        }

        // Peamenüü
        private void NäitaPeaMenüüd()
        {
            Controls.Clear();

            pealkiri = new Label();
            pealkiri.Text = "Piltide paaride mäng";
            pealkiri.Font = new Font("Arial", 28, FontStyle.Bold);
            pealkiri.AutoSize = false;
            pealkiri.TextAlign = ContentAlignment.MiddleCenter;
            pealkiri.Size = new Size(500, 70);
            pealkiri.Location = new Point(50, 60);
            Controls.Add(pealkiri);

            alustaMänguNupp = new Button();
            alustaMänguNupp.Text = "Alusta mängu";
            alustaMänguNupp.Font = new Font("Arial", 12, FontStyle.Bold);
            alustaMänguNupp.Size = new Size(180, 50);
            alustaMänguNupp.Location = new Point(210, 180);
            alustaMänguNupp.Click += AlustaMänguNupp_Click;
            Controls.Add(alustaMänguNupp);

            seadeteNupp = new Button();
            seadeteNupp.Text = "Seaded";
            seadeteNupp.Font = new Font("Arial", 12, FontStyle.Bold);
            seadeteNupp.Size = new Size(180, 50);
            seadeteNupp.Location = new Point(210, 250);
            seadeteNupp.Click += SeadeteNupp_Click;
            Controls.Add(seadeteNupp);

            RakendaTeema();
        }

        // Seaded
        private void NäitaSeadeid()
        {
            Controls.Clear();

            pealkiri = new Label();
            pealkiri.Text = "Seaded";
            pealkiri.Font = new Font("Arial", 28, FontStyle.Bold);
            pealkiri.AutoSize = false;
            pealkiri.TextAlign = ContentAlignment.MiddleCenter;
            pealkiri.Size = new Size(500, 60);
            pealkiri.Location = new Point(50, 25);
            Controls.Add(pealkiri);

            // Laua suurus
            suurusLabel = new Label();
            suurusLabel.Text = "Laua suurus";
            suurusLabel.Font = new Font("Arial", 14, FontStyle.Bold);
            suurusLabel.AutoSize = true;
            suurusLabel.Location = new Point(50, 110);
            Controls.Add(suurusLabel);

            suurus4Nupp = new Button();
            suurus4Nupp.Text = "4 x 4";
            suurus4Nupp.Size = new Size(120, 45);
            suurus4Nupp.Location = new Point(50, 150);
            suurus4Nupp.Click += Suurus4Nupp_Click;
            Controls.Add(suurus4Nupp);

            suurus8Nupp = new Button();
            suurus8Nupp.Text = "8 x 8";
            suurus8Nupp.Size = new Size(120, 45);
            suurus8Nupp.Location = new Point(50, 205);
            suurus8Nupp.Click += Suurus8Nupp_Click;
            Controls.Add(suurus8Nupp);

            // Parameetrid
            parameetridLabel = new Label();
            parameetridLabel.Text = "Parameetrid";
            parameetridLabel.Font = new Font("Arial", 14, FontStyle.Bold);
            parameetridLabel.AutoSize = true;
            parameetridLabel.Location = new Point(250, 110);
            Controls.Add(parameetridLabel);

            praeguSuurusLabel = new Label();
            praeguSuurusLabel.AutoSize = true;
            praeguSuurusLabel.Location = new Point(250, 155);
            Controls.Add(praeguSuurusLabel);

            // Muuda pilte
            muudaPilteNupp = new Button();
            muudaPilteNupp.Text = "Muuda pilte";
            muudaPilteNupp.Font = new Font("Arial", 10, FontStyle.Bold);
            muudaPilteNupp.Size = new Size(180, 45);
            muudaPilteNupp.Location = new Point(200, 230);
            muudaPilteNupp.Click += MuudaPilteNupp_Click;
            Controls.Add(muudaPilteNupp);

            // Tagasi
            tagasiNupp = new Button();
            tagasiNupp.Text = "Tagasi";
            tagasiNupp.Size = new Size(120, 45);
            tagasiNupp.Location = new Point(240, 310);
            tagasiNupp.Click += TagasiNupp_Click;
            Controls.Add(tagasiNupp);

            UuendaSeadeid();
            RakendaTeema();
        }

        private void UuendaSeadeid()
        {
            if (praeguSuurusLabel == null)
            {
                return;
            }

            praeguSuurusLabel.Text =
                "Laua suurus: " + lauaSuurus + " x " + lauaSuurus;

            if (lauaSuurus == 4)
            {
                praeguSuurusLabel.Text += "\nPaare: 8";
            }
            else
            {
                praeguSuurusLabel.Text += "\nPaare: 32";
            }
        }

        // Seadete nupud
        private void SeadeteNupp_Click(object sender, EventArgs e)
        {
            NäitaSeadeid();
        }

        private void Suurus4Nupp_Click(object sender, EventArgs e)
        {
            lauaSuurus = 4;
            UuendaSeadeid();
        }

        private void Suurus8Nupp_Click(object sender, EventArgs e)
        {
            lauaSuurus = 8;
            UuendaSeadeid();
        }

        private void TagasiNupp_Click(object sender, EventArgs e)
        {
            NäitaPeaMenüüd();
        }

        private void MuudaPilteNupp_Click(object sender, EventArgs e)
        {
            PildiValik pildiValik = new PildiValik(pildiTeed, lauaSuurus);

            if (pildiValik.ShowDialog() == DialogResult.OK)
            {
                pildiTeed = pildiValik.ValitudPildid;
            }
        }

        // Alusta mängu
        private void AlustaMänguNupp_Click(object sender, EventArgs e)
        {
            AlustaMängu();
        }

        private void AlustaMängu()
        {
            peidaTaimer.Stop();
            skoor = 3200;
            leitudPaare = 0;
            esimeneKaart = null;
            teineKaart = null;
            esimeneKaartIndeks = -1;
            teineKaartIndeks = -1;
            Controls.Clear();

            LooMänguLaud();
            LooKaardid();
        }

        // Mängulaud
        private void LooMänguLaud()
        {
            scoorLabel = new Label();
            scoorLabel.Text = "Skoor: " + skoor;
            scoorLabel.Font = new Font("Arial", 12, FontStyle.Bold);
            scoorLabel.AutoSize = true;
            scoorLabel.Location = new Point(15, 15);
            Controls.Add(scoorLabel);

            paaridLabel = new Label();
            paaridLabel.Text = "Leitud paare: 0 / " + ((lauaSuurus * lauaSuurus) / 2);
            paaridLabel.Font = new Font("Arial", 12, FontStyle.Bold);
            paaridLabel.AutoSize = true;
            paaridLabel.Location = new Point(180, 15);
            Controls.Add(paaridLabel);

            peaMenüüNupp = new Button();
            peaMenüüNupp.Text = "Peamenüü";
            peaMenüüNupp.Size = new Size(100, 30);
            peaMenüüNupp.Location = new Point(ClientSize.Width - 120, 10);
            peaMenüüNupp.Click += PeaMenüüMänguNupp_Click;
            Controls.Add(peaMenüüNupp);

            mängTabel = new TableLayoutPanel();
            mängTabel.RowCount = lauaSuurus;
            mängTabel.ColumnCount = lauaSuurus;
            mängTabel.Location = new Point(15, 55);

            int laua_laius = ClientSize.Width - 45;
            int laua_kõrgus = ClientSize.Height - 105;

            mängTabel.Size = new Size(laua_laius, laua_kõrgus);
            mängTabel.CellBorderStyle = TableLayoutPanelCellBorderStyle.Single;
            mängTabel.BackColor = Color.Gray;

            for (int i = 0; i < lauaSuurus; i++)
            {
                mängTabel.RowStyles.Add(new RowStyle(SizeType.Percent, 100f / lauaSuurus));
                mängTabel.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 100f / lauaSuurus));
            }

            Controls.Add(mängTabel);

            RakendaTeema();
        }

        // Kaardid
        private void LooKaardid()
        {
            kaardid.Clear();
            kaardiVäärtused.Clear();
            sobitatudKaardid.Clear();

            int kaartideArv = lauaSuurus * lauaSuurus;
            int paaride_arv = kaartideArv / 2;

            while (pildiTeed.Count < paaride_arv)
            {
                pildiTeed.Add("");
            }

            List<int> väärtused = new List<int>();

            for (int i = 0; i < paaride_arv; i++)
            {
                väärtused.Add(i);
                väärtused.Add(i);
            }

            // Segamine
            for (int i = väärtused.Count - 1; i > 0; i--)
            {
                int j = juhuslik.Next(i + 1);
                int ajutine = väärtused[i];
                väärtused[i] = väärtused[j];
                väärtused[j] = ajutine;
            }

            // Loo kaardid
            for (int i = 0; i < kaartideArv; i++)
            {
                PictureBox kaart = new PictureBox();
                kaart.Dock = DockStyle.Fill;
                kaart.Margin = new Padding(3);
                kaart.SizeMode = PictureBoxSizeMode.Zoom;
                kaart.BackColor = Color.DarkBlue;
                kaart.BorderStyle = BorderStyle.FixedSingle;
                kaart.Cursor = Cursors.Hand;
                kaart.Tag = i;
                kaart.Click += Kaart_Click;

                kaardid.Add(kaart);
                kaardiVäärtused.Add(väärtused[i]);
                sobitatudKaardid.Add(false);

                mängTabel.Controls.Add(kaart, i % lauaSuurus, i / lauaSuurus);

                NäitaKaardiTagakülge(kaart);
            }
        }

        // Kaardi klõps
        private void Kaart_Click(object sender, EventArgs e)
        {
            if (peidaTaimer.Enabled)
            {
                return;
            }

            PictureBox kaart = (PictureBox)sender;

            int indeks = (int)kaart.Tag;

            if (sobitatudKaardid[indeks])
            {
                return;
            }

            if (indeks == esimeneKaartIndeks)
            {
                return;
            }

            NäitaKaardiPilti(kaart, kaardiVäärtused[indeks]);

            if (esimeneKaart == null)
            {
                esimeneKaart = kaart;
                esimeneKaartIndeks = indeks;
                return;
            }

            teineKaart = kaart;
            teineKaartIndeks = indeks;

            // Sobivus
            if (kaardiVäärtused[esimeneKaartIndeks] == kaardiVäärtused[teineKaartIndeks])
            {
                sobitatudKaardid[esimeneKaartIndeks] = true;
                sobitatudKaardid[teineKaartIndeks] = true;
                leitudPaare++;
                skoor += 200;

                UuendaSkoori();

                esimeneKaart = null;
                teineKaart = null;
                esimeneKaartIndeks = -1;
                teineKaartIndeks = -1;

                if (leitudPaare == (lauaSuurus * lauaSuurus) / 2)
                {
                    MängVõidetud();
                }
            }
            else
            {
                skoor -= 100;

                if (skoor < 0)
                {
                    skoor = 0;
                }

                UuendaSkoori();
                peidaTaimer.Start();
            }
        }

        // Peida kaardid
        private void PeidaTaimer_Tick(object sender, EventArgs e)
        {
            peidaTaimer.Stop();

            if (esimeneKaart != null)
            {
                NäitaKaardiTagakülge(esimeneKaart);
            }

            if (teineKaart != null)
            {
                NäitaKaardiTagakülge(teineKaart);
            }

            esimeneKaart = null;
            teineKaart = null;
            esimeneKaartIndeks = -1;
            teineKaartIndeks = -1;
        }

        // Näita kaarti
        private void NäitaKaardiTagakülge(PictureBox kaart)
        {
            kaart.Image = null;
            kaart.BackColor = Color.DarkBlue;
        }

        private void NäitaKaardiPilti(PictureBox kaart, int pildiNumber)
        {
            if (pildiNumber >= 0 && pildiNumber < pildiTeed.Count && File.Exists(pildiTeed[pildiNumber]))
            {
                using (Image ajutine = Image.FromFile(pildiTeed[pildiNumber]))
                {
                    kaart.Image = new Bitmap(ajutine);
                }

                kaart.BackColor = Color.White;
            }
            else
            {
                Bitmap bitmap = new Bitmap(100, 100);

                using (Graphics graafika = Graphics.FromImage(bitmap))
                {
                    graafika.Clear(Color.White);

                    string tekst = "Pilt " + (pildiNumber + 1);

                    using (Font font = new Font("Arial", 16, FontStyle.Bold))
                    {
                        SizeF suurus = graafika.MeasureString(tekst, font);

                        graafika.DrawString(tekst, font, Brushes.Black, (100 - suurus.Width) / 2, (100 - suurus.Height) / 2);
                    }
                }

                kaart.Image = bitmap;
                kaart.BackColor = Color.White;
            }
        }

        // Skoor
        private void UuendaSkoori()
        {
            if (scoorLabel != null)
            {
                scoorLabel.Text = "Skoor: " + skoor;
            }

            if (paaridLabel != null)
            {
                paaridLabel.Text = "Leitud paare: " + leitudPaare + " / " + ((lauaSuurus * lauaSuurus) / 2);
            }
        }

        // Mäng võidetud
        private void MängVõidetud()
        {
            peidaTaimer.Stop();

            MessageBox.Show("Sa võitsid!\n\nLõppskoor: " + skoor, "Piltide paaride mäng");

            NäitaMänguTulemust();
        }

        private void NäitaMänguTulemust()
        {
            Controls.Clear();

            Label tulemusLabel = new Label();
            tulemusLabel.Text = "SA VÕITSID!\n\nSkoor: " + skoor;
            tulemusLabel.Font = new Font("Arial", 24, FontStyle.Bold);
            tulemusLabel.TextAlign = ContentAlignment.MiddleCenter;
            tulemusLabel.Size = new Size(500, 160);
            tulemusLabel.Location = new Point(50, 100);
            Controls.Add(tulemusLabel);

            Button mängiUuestiNupp = new Button();
            mängiUuestiNupp.Text = "Mängi uuesti";
            mängiUuestiNupp.Font = new Font("Arial", 11, FontStyle.Bold);
            mängiUuestiNupp.Size = new Size(160, 45);
            mängiUuestiNupp.Location = new Point(120, 300);
            mängiUuestiNupp.Click += MängiUuestiNupp_Click;
            Controls.Add(mängiUuestiNupp);

            Button tulemusPeaMenüüNupp = new Button();
            tulemusPeaMenüüNupp.Text = "Peamenüü";
            tulemusPeaMenüüNupp.Font = new Font("Arial", 11, FontStyle.Bold);
            tulemusPeaMenüüNupp.Size = new Size(160, 45);
            tulemusPeaMenüüNupp.Location = new Point(310, 300);
            tulemusPeaMenüüNupp.Click += PeaMenüüMänguNupp_Click;
            Controls.Add(tulemusPeaMenüüNupp);

            RakendaTeema();
        }

        private void MängiUuestiNupp_Click(object sender, EventArgs e)
        {
            AlustaMängu();
        }

        // Peamenüü (mängu ajal)
        private void PeaMenüüMänguNupp_Click(object sender, EventArgs e)
        {
            peidaTaimer.Stop();
            NäitaPeaMenüüd();
        }

        // Vaikimisi pildid
        private void LaadiVaikimisiPildid()
        {
            pildiTeed.Clear();

            string pildiKaust = Path.GetFullPath(@"..\..\Images");

            if (!Directory.Exists(pildiKaust))
            {
                return;
            }

            string[] failid = Directory.GetFiles(pildiKaust);

            foreach (string fail in failid)
            {
                string laiend = Path.GetExtension(fail).ToLower();

                if (OnPildiFail(laiend))
                {
                    pildiTeed.Add(fail);
                }
            }
        }

        private bool OnPildiFail(string laiend)
        {
            return
                laiend == ".jpg" ||
                laiend == ".jpeg" ||
                laiend == ".png" ||
                laiend == ".bmp" ||
                laiend == ".gif";
        }

        // Teema
        private void TumeTeema(object sender, EventArgs e)
        {
            Taustavärv = Color.Black;
            Esiplaanivärv = Color.White;

            RakendaTeema();
        }

        private void HeleTeema(object sender, EventArgs e)
        {
            Taustavärv = Color.White;
            Esiplaanivärv = Color.Black;

            RakendaTeema();
        }

        private void RakendaTeema()
        {
            BackColor = Taustavärv;
            ForeColor = Esiplaanivärv;

            RakendaLabeliTeema(pealkiri);

            RakendaNupuTeema(alustaMänguNupp);
            RakendaNupuTeema(seadeteNupp);

            RakendaLabeliTeema(suurusLabel);
            RakendaLabeliTeema(parameetridLabel);
            RakendaLabeliTeema(praeguSuurusLabel);

            RakendaNupuTeema(suurus4Nupp);
            RakendaNupuTeema(suurus8Nupp);
            RakendaNupuTeema(muudaPilteNupp);
            RakendaNupuTeema(tagasiNupp);

            RakendaLabeliTeema(scoorLabel);
            RakendaLabeliTeema(paaridLabel);

            RakendaNupuTeema(peaMenüüNupp);
        }

        private void RakendaLabeliTeema(Label label)
        {
            if (label != null)
            {
                label.BackColor = Taustavärv;

                label.ForeColor = Esiplaanivärv;
            }
        }

        private void RakendaNupuTeema(
            Button nupp)
        {
            if (nupp != null)
            {
                nupp.BackColor = Taustavärv;

                nupp.ForeColor = Esiplaanivärv;
            }
        }
    }

    // Pildi valimise aken
    public class PildiValik : Form
    {
        // Pildid
        List<string> valitudPildid = new List<string>();
        List<PictureBox> eelvaated = new List<PictureBox>();
        List<TextBox> teeKastid = new List<TextBox>();

        int nõutudPildid;

        // Juhtelemendid
        FlowLayoutPanel pildiPaneel;

        Button salvestaNupp;

        // Valitud pildid
        public List<string> ValitudPildid
        {
            get
            {
                return valitudPildid;
            }
        }

        // Konstruktor
        public PildiValik(List<string> praegusedPildid, int lauaSuurus)
        {
            Text = "Muuda mängu pilte";
            Size = new Size(800, 600);
            StartPosition = FormStartPosition.CenterScreen;

            nõutudPildid = (lauaSuurus * lauaSuurus) / 2;

            // Kopeeri praegused pildid
            for (int i = 0; i < praegusedPildid.Count && i < nõutudPildid; i++)
            {
                valitudPildid.Add(praegusedPildid[i]);
            }

            while (valitudPildid.Count < nõutudPildid)
            {
                valitudPildid.Add("");
            }

            LooLiides();
            VärskendaPilte();
        }

        // Liides
        private void LooLiides()
        {
            Label pealkiri = new Label();
            pealkiri.Text = "Vali pildid";
            pealkiri.Font = new Font("Arial", 20, FontStyle.Bold);
            pealkiri.Size = new Size(760, 45);
            pealkiri.Location = new Point(20, 15);
            pealkiri.TextAlign = ContentAlignment.MiddleCenter;
            Controls.Add(pealkiri);

            Label teave = new Label();
            teave.Text = "Vali iga paari jaoks üks pilt.";
            teave.AutoSize = true;
            teave.Location = new Point(20, 65);
            Controls.Add(teave);

            // Salvesta
            salvestaNupp = new Button();

            salvestaNupp.Text = "Salvesta";

            salvestaNupp.Size = new Size(120, 35);

            salvestaNupp.Location = new Point(630, 90);

            salvestaNupp.Click +=
                SalvestaNupp_Click;

            Controls.Add(
                salvestaNupp);

            // Pildipaneel

            pildiPaneel =
                new FlowLayoutPanel();

            pildiPaneel.Location =
                new Point(
                    20,
                    140);

            pildiPaneel.Size =
                new Size(
                    750,
                    390);

            pildiPaneel.AutoScroll = true;

            pildiPaneel.FlowDirection =
                FlowDirection.TopDown;

            pildiPaneel.WrapContents = false;

            Controls.Add(
                pildiPaneel);
        }

        // =========================================================
        // VÄRSKENDA PILTE
        // =========================================================

        private void VärskendaPilte()
        {
            pildiPaneel.Controls.Clear();

            eelvaated.Clear();

            teeKastid.Clear();

            for (int i = 0;
                 i < nõutudPildid;
                 i++)
            {
                Panel rida =
                    new Panel();

                rida.Size =
                    new Size(
                        700,
                        90);

                // Pildi number

                Label number =
                    new Label();

                number.Text =
                    "Pilt " + (i + 1);

                number.Font =
                    new Font(
                        "Arial",
                        10,
                        FontStyle.Bold);

                number.Size =
                    new Size(
                        80,
                        30);

                number.Location =
                    new Point(
                        0,
                        30);

                rida.Controls.Add(
                    number);

                // Eelvaade

                PictureBox eelvaade =
                    new PictureBox();

                eelvaade.Size =
                    new Size(
                        80,
                        80);

                eelvaade.Location =
                    new Point(
                        90,
                        5);

                eelvaade.SizeMode =
                    PictureBoxSizeMode.Zoom;

                eelvaade.BorderStyle =
                    BorderStyle.FixedSingle;

                rida.Controls.Add(
                    eelvaade);

                eelvaated.Add(
                    eelvaade);

                // Tee

                TextBox teeKast =
                    new TextBox();

                teeKast.Size =
                    new Size(
                        400,
                        25);

                teeKast.Location =
                    new Point(
                        185,
                        30);

                teeKast.Text =
                    valitudPildid[i];

                rida.Controls.Add(
                    teeKast);

                teeKastid.Add(
                    teeKast);

                // Vali

                Button vali =
                    new Button();

                vali.Text =
                    "Vali";

                vali.Size =
                    new Size(
                        80,
                        30);

                vali.Location =
                    new Point(
                        600,
                        28);

                int indeks = i;

                vali.Click +=
                    delegate
                    {
                        ValiPildiAllikas(indeks);
                    };

                rida.Controls.Add(
                    vali);

                pildiPaneel.Controls.Add(
                    rida);

                LaadiEelvaade(i);
            }
        }

        // =========================================================
        // VALI PILDI ALLIKAS
        // =========================================================

        private void ValiPildiAllikas(
            int indeks)
        {
            PildiAllikaValik allikaValik =
                new PildiAllikaValik();

            if (allikaValik.ShowDialog() ==
                DialogResult.OK)
            {
                string valitudTee =
                    allikaValik.ValitudPildiTee;

                if (!string.IsNullOrEmpty(
                    valitudTee))
                {
                    valitudPildid[indeks] =
                        valitudTee;

                    teeKastid[indeks].Text =
                        valitudTee;

                    LaadiEelvaade(indeks);
                }
            }
        }

        // =========================================================
        // EELVAADE
        // =========================================================

        private void LaadiEelvaade(
            int indeks)
        {
            if (indeks < 0 ||
                indeks >= eelvaated.Count)
            {
                return;
            }

            // Vabasta vana pilt

            if (eelvaated[indeks].Image != null)
            {
                eelvaated[indeks].Image.Dispose();

                eelvaated[indeks].Image =
                    null;
            }

            if (indeks >=
                valitudPildid.Count)
            {
                return;
            }

            string tee =
                valitudPildid[indeks];

            if (!string.IsNullOrEmpty(tee) &&
                File.Exists(tee))
            {
                try
                {
                    using (Image pilt =
                        Image.FromFile(tee))
                    {
                        eelvaated[indeks].Image =
                            new Bitmap(pilt);
                    }
                }
                catch
                {
                    eelvaated[indeks].Image =
                        null;
                }
            }
        }

        // =========================================================
        // SALVESTA
        // =========================================================

        private void SalvestaNupp_Click(
            object sender,
            EventArgs e)
        {
            for (int i = 0;
                 i < nõutudPildid;
                 i++)
            {
                valitudPildid[i] =
                    teeKastid[i].Text;
            }

            DialogResult =
                DialogResult.OK;

            Close();
        }
    }


    // =============================================================
    // PILDI ALLIKA VALIK
    // =============================================================

    public class PildiAllikaValik : Form
    {
        // Selle akna poolt valitud pilt.

        public string ValitudPildiTee
        {
            get;
            private set;
        }

        Button arvutiNupp;
        Button projektiPildidNupp;

        // =========================================================
        // KONSTRUKTOR
        // =========================================================

        public PildiAllikaValik()
        {
            Text =
                "Vali pilt";

            Size =
                new Size(
                    330,
                    180);

            StartPosition =
                FormStartPosition.CenterParent;

            FormBorderStyle =
                FormBorderStyle.FixedDialog;

            MaximizeBox = false;

            MinimizeBox = false;

            LooLiides();
        }

        // =========================================================
        // LIIDES
        // =========================================================

        private void LooLiides()
        {
            Label pealkiri =
                new Label();

            pealkiri.Text =
                "Vali pildi allikas";

            pealkiri.Font =
                new Font(
                    "Arial",
                    12,
                    FontStyle.Bold);

            pealkiri.AutoSize = false;

            pealkiri.TextAlign =
                ContentAlignment.MiddleCenter;

            pealkiri.Size =
                new Size(
                    290,
                    35);

            pealkiri.Location =
                new Point(
                    20,
                    15);

            Controls.Add(
                pealkiri);

            // Arvuti nupp

            arvutiNupp =
                new Button();

            arvutiNupp.Text =
                "Vali arvutist";

            arvutiNupp.Size =
                new Size(
                    125,
                    50);

            arvutiNupp.Location =
                new Point(
                    25,
                    70);

            arvutiNupp.Click +=
                ArvutiNupp_Click;

            Controls.Add(
                arvutiNupp);

            // Projekti piltide nupp

            projektiPildidNupp =
                new Button();

            projektiPildidNupp.Text =
                "Projekti pildid";

            projektiPildidNupp.Size =
                new Size(
                    125,
                    50);

            projektiPildidNupp.Location =
                new Point(
                    175,
                    70);

            projektiPildidNupp.Click +=
                ProjektiPildidNupp_Click;

            Controls.Add(
                projektiPildidNupp);
        }

        // =========================================================
        // VALI ARVUTIST
        // =========================================================

        private void ArvutiNupp_Click(
            object sender,
            EventArgs e)
        {
            using (OpenFileDialog dialoog =
                new OpenFileDialog())
            {
                dialoog.Title =
                    "Vali pilt";

                dialoog.Filter =
                    "Pildifailid|*.jpg;*.jpeg;*.png;*.bmp;*.gif";

                dialoog.Multiselect = false;

                if (dialoog.ShowDialog() ==
                    DialogResult.OK)
                {
                    ValitudPildiTee =
                        dialoog.FileName;

                    DialogResult =
                        DialogResult.OK;

                    Close();
                }
            }
        }

        // =========================================================
        // VALI PROJEKTI PILTIDEST
        // =========================================================

        private void ProjektiPildidNupp_Click(
            object sender,
            EventArgs e)
        {
            ProjektiPiltideGalerii galerii =
                new ProjektiPiltideGalerii();

            if (galerii.ShowDialog(this) ==
                DialogResult.OK)
            {
                ValitudPildiTee =
                    galerii.ValitudPildiTee;

                DialogResult =
                    DialogResult.OK;

                Close();
            }
        }
    }


    // =============================================================
    // PROJEKTI PILTIDE GALERII
    // =============================================================

    public class ProjektiPiltideGalerii : Form
    {
        FlowLayoutPanel pildiPaneel;

        public string ValitudPildiTee
        {
            get;
            private set;
        }

        // =========================================================
        // KONSTRUKTOR
        // =========================================================

        public ProjektiPiltideGalerii()
        {
            Text =
                "Projekti pildid";

            Size =
                new Size(
                    600,
                    400);

            StartPosition =
                FormStartPosition.CenterParent;

            LooLiides();

            LaadiPildid();
        }

        // Liides

        private void LooLiides()
        {
            pildiPaneel = new FlowLayoutPanel();

            pildiPaneel.Dock = DockStyle.Fill;

            pildiPaneel.AutoScroll = true;

            pildiPaneel.Padding = new Padding(5);

            pildiPaneel.WrapContents = true;

            pildiPaneel.FlowDirection = FlowDirection.LeftToRight;

            Controls.Add(pildiPaneel);
        }

        // Laadi pildid (projekti piltide kaustast)

        private void LaadiPildid()
        {
            string pildiKaust = Path.GetFullPath(@"..\..\Images");

            if (!Directory.Exists(pildiKaust))
            {
                Label piltidePole = new Label();

                piltidePole.Text = "Projekti Images kausta ei eksisteeri.";

                piltidePole.AutoSize = true;

                piltidePole.Location = new Point(20, 20);

                pildiPaneel.Controls.Add(piltidePole);

                return;
            }

            string[] pildid = Directory.GetFiles(pildiKaust);

            foreach (string pildiTee in pildid)
            {
                string laiend = Path.GetExtension(pildiTee).ToLower();

                if (!OnPildiFail(laiend))
                {
                    continue;
                }

                LooPildiKast(pildiTee);
            }

            if (pildiPaneel.Controls.Count == 0)
            {
                Label piltidePole = new Label();

                piltidePole.Text = "Projekti Images kaustas pole pilte.";

                piltidePole.AutoSize = true;

                piltidePole.Location = new Point(20, 20);

                pildiPaneel.Controls.Add(piltidePole);
            }
        }

        // Pildikasti loomine

        private void LooPildiKast(string pildiTee)
        {
            PictureBox pildiKast = new PictureBox();

            pildiKast.Size = new Size(120, 100);

            pildiKast.SizeMode = PictureBoxSizeMode.Zoom;

            pildiKast.BorderStyle = BorderStyle.FixedSingle;

            pildiKast.Cursor = Cursors.Hand;

            pildiKast.Tag = pildiTee;

            try
            {
                // Tee koopia, et fail ei jääks lukku.

                using (Image pilt = Image.FromFile(pildiTee))
                {
                    pildiKast.Image = new Bitmap(pilt);
                }
            }
            catch
            {
                return;
            }

            pildiKast.Click += PildiKast_Click;

            pildiPaneel.Controls.Add(pildiKast);
        }

        // Pildi klõps

        private void PildiKast_Click(object sender, EventArgs e)
        {
            PictureBox pildiKast = (PictureBox)sender;

            ValitudPildiTee = (string)pildiKast.Tag;

            DialogResult = DialogResult.OK;

            Close();
        }

        // Pildifaili kontroll

        private bool OnPildiFail(string laiend)
        {
            return
                laiend == ".jpg" ||
                laiend == ".jpeg" ||
                laiend == ".png" ||
                laiend == ".bmp" ||
                laiend == ".gif";
        }
    }
}