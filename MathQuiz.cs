using System;
using System.Drawing;
using System.Windows.Forms;

namespace KolmRakendustWindowsFormSamrjakov
{
    public partial class Matemaatikaviktoriin : Form
    {
        // Peamenüü

        Label pealkiri;
        Button alustaMänguNupp, seadeteNupp;

        // Seaded

        Label raskusLabel, mänguRežiimLabel, parameetridLabel;
        Label praeguneRaskusLabel, aegParameeterLabel;
        Label minimaalneParameeterLabel, maksimaalneParameeterLabel;
        Label praeguneRežiimLabel;

        Button lihtneNupp, keskmineNupp, raskeNupp, kohandatudNupp;
        Button standardneNupp, pidevNupp;
        Button tagasiNupp;

        NumericUpDown kohandatudAeg, kohandatudArv;

        // Viktoriin

        Label aegLabel, aegAllLabel;
        Label küsimus1Label, küsimus2Label;
        Label küsimus3Label, küsimus4Label;
        Label lahendatudKüsimusedLabel;

        NumericUpDown vastus1, vastus2, vastus3, vastus4;

        Button peaMenüüNupp;
        Button kontrolliVastusedNupp;

        // Taimer

        Timer taimer1;

        // Juhuslik

        Random juhuslik = new Random();

        // Küsimuste andmed

        int küsimus1Vastus;
        int küsimus2Vastus;
        int küsimus3Vastus;
        int küsimus4Vastus;

        // Vaikimisi raskusaste

        string raskus = "Lihtne";

        int mänguAeg = 60;
        int minimaalneArv = 1;
        int maksimaalneArv = 10;

        // Vaikimisi mänguviis

        string mänguRežiim = "Standardne";

        int lahendatudKüsimused = 0;

        // Vaikimisi aeg

        int aegAllesJäänud;

        // Vaikimisi teema

        Color Taustavärv;
        Color Esiplaanivärv;

        // Konstruktor

        public Matemaatikaviktoriin(string pealkirjaTekst, int laius, int kõrgus, Color Taustavärv, Color Esiplaanivärv)
        {
            Text = pealkirjaTekst;
            Size = new Size(laius, kõrgus);
            StartPosition = FormStartPosition.CenterScreen;

            // Teema
            this.Taustavärv = Taustavärv;
            this.Esiplaanivärv = Esiplaanivärv;

            BackColor = Taustavärv;
            ForeColor = Esiplaanivärv;

            // Vormi seaded
            FormBorderStyle = FormBorderStyle.Fixed3D;
            MaximizeBox = false;

            // Taimer
            taimer1 = new Timer();
            taimer1.Interval = 1000;
            taimer1.Tick += Taimer1_Tick;

            // Peamenüü
            NäitaPeaMenüüd();

            // Menüü
            MainMenu menüü = new MainMenu();

            MenuItem menüüSeaded = new MenuItem("Seaded");

            MenuItem menüüTumeTeema =
                new MenuItem("Tume teema", TumeTeema);

            MenuItem menüüHeleTeema =
                new MenuItem("Hele teema", HeleTeema);

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
            pealkiri.Text = "Matemaatikaviktoriin";
            pealkiri.Font = new Font("Arial", 30, FontStyle.Bold);
            pealkiri.AutoSize = true;
            pealkiri.Location = new Point(95, 70);
            Controls.Add(pealkiri);

            alustaMänguNupp = new Button();
            alustaMänguNupp.Text = "Alusta mängu";
            alustaMänguNupp.Font = new Font("Arial", 12, FontStyle.Bold);
            alustaMänguNupp.Size = new Size(160, 50);
            alustaMänguNupp.Location = new Point(220, 180);
            alustaMänguNupp.Click += AlustaMänguNupp_Click;
            Controls.Add(alustaMänguNupp);

            seadeteNupp = new Button();
            seadeteNupp.Text = "Seaded";
            seadeteNupp.Font = new Font("Arial", 12, FontStyle.Bold);
            seadeteNupp.Size = new Size(160, 50);
            seadeteNupp.Location = new Point(220, 250);
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
            pealkiri.AutoSize = true;
            pealkiri.Location = new Point(225, 30);
            Controls.Add(pealkiri);

            // Raskusaste
            raskusLabel = new Label();
            raskusLabel.Text = "Raskusaste";
            raskusLabel.Font = new Font("Arial", 14, FontStyle.Bold);
            raskusLabel.AutoSize = true;
            raskusLabel.Location = new Point(40, 90);
            Controls.Add(raskusLabel);

            lihtneNupp = new Button();
            lihtneNupp.Text = "Lihtne";
            lihtneNupp.Size = new Size(110, 40);
            lihtneNupp.Location = new Point(40, 130);
            lihtneNupp.Click += LihtneNupp_Click;
            Controls.Add(lihtneNupp);

            keskmineNupp = new Button();
            keskmineNupp.Text = "Keskmine";
            keskmineNupp.Size = new Size(110, 40);
            keskmineNupp.Location = new Point(40, 180);
            keskmineNupp.Click += KeskmineNupp_Click;
            Controls.Add(keskmineNupp);

            raskeNupp = new Button();
            raskeNupp.Text = "Raske";
            raskeNupp.Size = new Size(110, 40);
            raskeNupp.Location = new Point(40, 230);
            raskeNupp.Click += RaskeNupp_Click;
            Controls.Add(raskeNupp);

            kohandatudNupp = new Button();
            kohandatudNupp.Text = "Kohandatud";
            kohandatudNupp.Size = new Size(110, 40);
            kohandatudNupp.Location = new Point(40, 280);
            kohandatudNupp.Click += KohandatudNupp_Click;
            Controls.Add(kohandatudNupp);

            // Mänguviis
            mänguRežiimLabel = new Label();
            mänguRežiimLabel.Text = "Mänguviis";
            mänguRežiimLabel.Font = new Font("Arial", 14, FontStyle.Bold);
            mänguRežiimLabel.AutoSize = true;
            mänguRežiimLabel.Location = new Point(205, 90);
            Controls.Add(mänguRežiimLabel);

            standardneNupp = new Button();
            standardneNupp.Text = "Standardne";
            standardneNupp.Size = new Size(110, 40);
            standardneNupp.Location = new Point(205, 130);
            standardneNupp.Click += StandardneNupp_Click;
            Controls.Add(standardneNupp);

            pidevNupp = new Button();
            pidevNupp.Text = "Pidev";
            pidevNupp.Size = new Size(110, 40);
            pidevNupp.Location = new Point(205, 180);
            pidevNupp.Click += PidevNupp_Click;
            Controls.Add(pidevNupp);

            // Parameetrid
            parameetridLabel = new Label();
            parameetridLabel.Text = "Parameetrid";
            parameetridLabel.Font = new Font("Arial", 14, FontStyle.Bold);
            parameetridLabel.AutoSize = true;
            parameetridLabel.Location = new Point(365, 90);
            Controls.Add(parameetridLabel);

            praeguneRaskusLabel = new Label();
            praeguneRaskusLabel.AutoSize = true;
            praeguneRaskusLabel.Location = new Point(365, 135);
            Controls.Add(praeguneRaskusLabel);

            praeguneRežiimLabel = new Label();
            praeguneRežiimLabel.AutoSize = true;
            praeguneRežiimLabel.Location = new Point(365, 165);
            Controls.Add(praeguneRežiimLabel);

            aegParameeterLabel = new Label();
            aegParameeterLabel.AutoSize = true;
            aegParameeterLabel.Location = new Point(365, 195);
            Controls.Add(aegParameeterLabel);

            minimaalneParameeterLabel = new Label();
            minimaalneParameeterLabel.AutoSize = true;
            minimaalneParameeterLabel.Location = new Point(365, 225);
            Controls.Add(minimaalneParameeterLabel);

            maksimaalneParameeterLabel = new Label();
            maksimaalneParameeterLabel.AutoSize = true;
            maksimaalneParameeterLabel.Location = new Point(365, 255);
            Controls.Add(maksimaalneParameeterLabel);

            // Kohandatud aeg
            kohandatudAeg = new NumericUpDown();
            kohandatudAeg.Minimum = 10;
            kohandatudAeg.Maximum = 300;
            kohandatudAeg.Value = Math.Min(Math.Max(mänguAeg, kohandatudAeg.Minimum), kohandatudAeg.Maximum);
            kohandatudAeg.Size = new Size(100, 30);
            kohandatudAeg.Location = new Point(365, 285);
            kohandatudAeg.Visible = false;
            kohandatudAeg.ValueChanged += KohandatudAeg_ValueChanged;
            Controls.Add(kohandatudAeg);

            // Kohandatud maksimaalne arv
            kohandatudArv = new NumericUpDown();
            kohandatudArv.Minimum = 5;
            kohandatudArv.Maximum = 1000;
            kohandatudArv.Value = Math.Min(Math.Max(maksimaalneArv, kohandatudArv.Minimum), kohandatudArv.Maximum);

            kohandatudArv.Size = new Size(100, 30);
            kohandatudArv.Location = new Point(365, 320);
            kohandatudArv.Visible = false;
            kohandatudArv.ValueChanged += KohandatudArv_ValueChanged;
            Controls.Add(kohandatudArv);

            // Tagasi
            tagasiNupp = new Button();
            tagasiNupp.Text = "Tagasi";
            tagasiNupp.Size = new Size(100, 40);
            tagasiNupp.Location = new Point(250, 370);
            tagasiNupp.Click += TagasiNupp_Click;
            Controls.Add(tagasiNupp);

            UuendaSeadeid();
            RakendaTeema();
        }

        // Seadete muutmine

        private void UuendaSeadeid()
        {
            if (praeguneRaskusLabel == null)
            {
                return;
            }

            praeguneRaskusLabel.Text = "Raskusaste: " + raskus;

            praeguneRežiimLabel.Text = "Mänguviis: " + mänguRežiim;

            aegParameeterLabel.Text = "Aeg: " + mänguAeg + " sek";

            minimaalneParameeterLabel.Text = "Minimaalne: " + minimaalneArv;

            maksimaalneParameeterLabel.Text = "Maksimaalne: " + maksimaalneArv;

            if (raskus == "Kohandatud")
            {
                kohandatudAeg.Visible = true;
                kohandatudArv.Visible = true;

                kohandatudAeg.Value = Math.Min(Math.Max(mänguAeg, (int)kohandatudAeg.Minimum), (int)kohandatudAeg.Maximum);

                kohandatudArv.Value = Math.Min(Math.Max(maksimaalneArv, (int)kohandatudArv.Minimum), (int)kohandatudArv.Maximum);
            }
            else
            {
                kohandatudAeg.Visible = false;
                kohandatudArv.Visible = false;
            }
        }

        // Kohandatud seaded

        private void KohandatudAeg_ValueChanged(object sender, EventArgs e)
        {
            if (raskus == "Kohandatud")
            {
                mänguAeg = (int)kohandatudAeg.Value;
                UuendaSeadeid();
            }
        }

        private void KohandatudArv_ValueChanged(object sender, EventArgs e)
        {
            if (raskus == "Kohandatud")
            {
                maksimaalneArv = (int)kohandatudArv.Value;
                UuendaSeadeid();
            }
        }

        // Raskusaste

        private void SeaLihtne()
        {
            raskus = "Lihtne";
            mänguAeg = 60;
            minimaalneArv = 1;
            maksimaalneArv = 10;

            UuendaSeadeid();
        }

        private void SeaKeskmine()
        {
            raskus = "Keskmine";
            mänguAeg = 45;
            minimaalneArv = 1;
            maksimaalneArv = 25;

            UuendaSeadeid();
        }

        private void SeaRaske()
        {
            raskus = "Raske";
            mänguAeg = 30;
            minimaalneArv = 1;
            maksimaalneArv = 50;

            UuendaSeadeid();
        }

        private void SeaKohandatud()
        {
            raskus = "Kohandatud";

            UuendaSeadeid();
        }

        // Mänguviis

        private void SeaStandardne()
        {
            mänguRežiim = "Standardne";
            UuendaSeadeid();
        }

        private void SeaPidev()
        {
            mänguRežiim = "Pidev";
            UuendaSeadeid();
        }

        // Nuppude klõpsud

        private void AlustaMänguNupp_Click(object sender, EventArgs e)
        {
            // Veendu, et kohandatud väärtused on salvestatud.
            if (raskus == "Kohandatud")
            {
                mänguAeg = (int)kohandatudAeg.Value;
                maksimaalneArv = (int)kohandatudArv.Value;
            }

            lahendatudKüsimused = 0;

            NäitaViktoriini();
            AlustaViktoriini();
        }

        private void PeaMenüüNupp_Click(object sender, EventArgs e)
        {
            taimer1.Stop();

            NäitaPeaMenüüd();
        }

        private void SeadeteNupp_Click(object sender, EventArgs e)
        {
            NäitaSeadeid();
        }

        private void LihtneNupp_Click(object sender, EventArgs e)
        {
            SeaLihtne();
        }

        private void KeskmineNupp_Click(object sender, EventArgs e)
        {
            SeaKeskmine();
        }

        private void RaskeNupp_Click(object sender, EventArgs e)
        {
            SeaRaske();
        }

        private void KohandatudNupp_Click(object sender, EventArgs e)
        {
            SeaKohandatud();
        }

        private void StandardneNupp_Click(object sender, EventArgs e)
        {
            SeaStandardne();
        }

        private void PidevNupp_Click(object sender, EventArgs e)
        {
            SeaPidev();
        }

        private void TagasiNupp_Click(object sender, EventArgs e)
        {
            NäitaPeaMenüüd();
        }

        // Viktoriin

        private void NäitaViktoriini()
        {
            Controls.Clear();

            // Aega alles
            aegAllLabel = new Label();
            aegAllLabel.Text = "Aega alles:";
            aegAllLabel.Font = new Font("Arial", 12, FontStyle.Bold);
            aegAllLabel.AutoSize = true;
            aegAllLabel.Location = new Point(ClientSize.Width - 180, 20);
            Controls.Add(aegAllLabel);

            // Taimer
            aegLabel = new Label();
            aegLabel.Text = "";
            aegLabel.Font = new Font("Arial", 12, FontStyle.Bold);
            aegLabel.BorderStyle = BorderStyle.FixedSingle;
            aegLabel.TextAlign = ContentAlignment.MiddleCenter;
            aegLabel.Size = new Size(100, 30);
            aegLabel.Location = new Point(ClientSize.Width - 110, 15);
            Controls.Add(aegLabel);

            // Lahendatud küsimused
            lahendatudKüsimusedLabel = new Label();
            lahendatudKüsimusedLabel.Text = "Lahendatud küsimusi: " + lahendatudKüsimused;
            lahendatudKüsimusedLabel.Font = new Font("Arial", 12, FontStyle.Bold);
            lahendatudKüsimusedLabel.AutoSize = true;
            lahendatudKüsimusedLabel.Location = new Point(80, 20);
            Controls.Add(lahendatudKüsimusedLabel);

            // Küsimus 1
            küsimus1Label = LooKüsimuseLabel(80);
            Controls.Add(küsimus1Label);

            vastus1 = LooVastuseKast(85, 1);
            Controls.Add(vastus1);

            // Küsimus 2
            küsimus2Label = LooKüsimuseLabel(140);
            Controls.Add(küsimus2Label);

            vastus2 = LooVastuseKast(145, 2);
            Controls.Add(vastus2);

            // Küsimus 3
            küsimus3Label = LooKüsimuseLabel(200);
            Controls.Add(küsimus3Label);

            vastus3 = LooVastuseKast(205, 3);
            Controls.Add(vastus3);

            // Küsimus 4
            küsimus4Label = LooKüsimuseLabel(260);
            Controls.Add(küsimus4Label);

            vastus4 = LooVastuseKast(265, 4);
            Controls.Add(vastus4);

            // Kontrolli vastuseid
            kontrolliVastusedNupp = new Button();
            kontrolliVastusedNupp.Text = "Kontrolli vastuseid";
            kontrolliVastusedNupp.Font = new Font("Arial", 12, FontStyle.Bold);
            kontrolliVastusedNupp.Size = new Size(180, 50);
            kontrolliVastusedNupp.Location = new Point(210, 340);
            kontrolliVastusedNupp.Click += KontrolliVastusedNupp_Click;
            Controls.Add(kontrolliVastusedNupp);

            // Peamenüü
            peaMenüüNupp = new Button();
            peaMenüüNupp.Text = "Peamenüü";
            peaMenüüNupp.Font = new Font("Arial", 12, FontStyle.Bold);
            peaMenüüNupp.Size = new Size(180, 40);
            peaMenüüNupp.Location = new Point(210, 400);
            peaMenüüNupp.Click += PeaMenüüNupp_Click;
            Controls.Add(peaMenüüNupp);


            RakendaTeema();
        }

        private Label LooKüsimuseLabel(int y)
        {
            Label label = new Label();

            label.Font = new Font("Arial", 18);
            label.Size = new Size(250, 50);
            label.Location = new Point(80, y);
            label.TextAlign = ContentAlignment.MiddleLeft;

            return label;
        }

        private NumericUpDown LooVastuseKast(
            int y,
            int tabIndeks)
        {
            NumericUpDown kast = new NumericUpDown();

            kast.Font = new Font("Arial", 18);
            kast.Size = new Size(100, 40);
            kast.Location = new Point(350, y);
            kast.TabIndex = tabIndeks;

            // Piisavalt suur 1000 × 1000 jaoks.
            kast.Maximum = 1000000;

            kast.Enter += Vastus_Enter;

            return kast;
        }

        // Viktoriini algus

        private void AlustaViktoriini()
        {
            vastus1.Value = 0;
            vastus2.Value = 0;
            vastus3.Value = 0;
            vastus4.Value = 0;

            LooKüsimus(küsimus1Label, 1);
            LooKüsimus(küsimus2Label, 2);
            LooKüsimus(küsimus3Label, 3);
            LooKüsimus(küsimus4Label, 4);

            kontrolliVastusedNupp.Enabled = true;

            // Käivitab / lähtestab taimeri ainult uue mängu alguses.
            // Pideval režiimil taimerit EI lähtestata, kui genereeritakse uus küsimuste komplekt.
            if (!taimer1.Enabled)
            {
                aegAllesJäänud = mänguAeg;

                aegLabel.Text = aegAllesJäänud + " sek";

                aegLabel.BackColor = Taustavärv;

                aegLabel.ForeColor = Esiplaanivärv;

                taimer1.Start();
            }
        }


        // Juhuslik küsimus

        private void LooKüsimus(Label küsimuseLabel, int küsimuseNumber)
        {
            int küsimuseTüüp = juhuslik.Next(1, 5);

            int arv1;
            int arv2;
            int vastus;

            // Liitmine
            if (küsimuseTüüp == 1)
            {
                arv1 = juhuslik.Next(minimaalneArv, maksimaalneArv + 1);

                arv2 = juhuslik.Next(minimaalneArv, maksimaalneArv + 1);

                vastus = arv1 + arv2;

                küsimuseLabel.Text = arv1 + " + " + arv2 + " =";

                SeaKüsimuseVastus(küsimuseNumber, vastus);
            }

            // Lahutamine
            else if (küsimuseTüüp == 2)
            {
                arv1 = juhuslik.Next(minimaalneArv, maksimaalneArv + 1);

                arv2 = juhuslik.Next(minimaalneArv, arv1 + 1);

                vastus = arv1 - arv2;

                küsimuseLabel.Text = arv1 + " - " + arv2 + " =";

                SeaKüsimuseVastus(küsimuseNumber, vastus);
            }

            // Korrutamine
            else if (küsimuseTüüp == 3)
            {
                arv1 = juhuslik.Next(minimaalneArv, maksimaalneArv + 1);

                arv2 = juhuslik.Next(minimaalneArv, maksimaalneArv + 1);

                vastus = arv1 * arv2;

                küsimuseLabel.Text = arv1 + " × " + arv2 + " =";

                SeaKüsimuseVastus(küsimuseNumber, vastus);
            }

            // Jagamine
            else
            {
                arv2 = juhuslik.Next(minimaalneArv, maksimaalneArv + 1);

                int ajutineVastus = juhuslik.Next(minimaalneArv, maksimaalneArv + 1);

                arv1 = arv2 * ajutineVastus;

                vastus = arv1 / arv2;

                küsimuseLabel.Text = arv1 + " ÷ " + arv2 + " =";

                SeaKüsimuseVastus(küsimuseNumber, vastus);
            }
        }

        // Vastused

        private void SeaKüsimuseVastus(int küsimuseNumber, int vastus)
        {
            if (küsimuseNumber == 1)
            {
                küsimus1Vastus = vastus;
            }
            else if (küsimuseNumber == 2)
            {
                küsimus2Vastus = vastus;
            }
            else if (küsimuseNumber == 3)
            {
                küsimus3Vastus = vastus;
            }
            else if (küsimuseNumber == 4)
            {
                küsimus4Vastus = vastus;
            }
        }

        // Kontrolli vastuseid nupp

        private void KontrolliVastusedNupp_Click(object sender, EventArgs e)
        {
            if (!taimer1.Enabled)
            {
                return;
            }

            bool õige = true;

            if (vastus1.Value != küsimus1Vastus)
            {
                õige = false;
            }

            if (vastus2.Value != küsimus2Vastus)
            {
                õige = false;
            }

            if (vastus3.Value != küsimus3Vastus)
            {
                õige = false;
            }

            if (vastus4.Value != küsimus4Vastus)
            {
                õige = false;
            }

            if (õige)
            {
                if (mänguRežiim == "Pidev")
                {
                    lahendatudKüsimused += 4;

                    lahendatudKüsimusedLabel.Text = "Lahendatud küsimusi: " + lahendatudKüsimused;

                    // Genereeri järgmised neli küsimust
                    // ja lähtesta taimer.
                    AlustaViktoriini();

                    return;
                }

                taimer1.Stop();

                MessageBox.Show("Kõik vastused on õiged!", "Matemaatikaviktoriin");

                kontrolliVastusedNupp.Enabled = false;

                return;
            }

            MessageBox.Show("Mõned vastused on valed.", "Matemaatikaviktoriin");
        }

        // Taimer

        private void Taimer1_Tick(object sender, EventArgs e)
        {
            if (aegAllesJäänud > 0)
            {
                aegAllesJäänud--;

                aegLabel.Text = aegAllesJäänud + " sek";

                if (aegAllesJäänud <= 5)
                {
                    aegLabel.BackColor = Color.Red;

                    aegLabel.ForeColor = Color.White;
                }
            }
            else
            {
                taimer1.Stop();

                aegLabel.Text = "Aeg sai läbi!";

                aegLabel.BackColor = Color.Red;

                aegLabel.ForeColor = Color.White;

                kontrolliVastusedNupp.Enabled = false;

                if (mänguRežiim == "Pidev")
                {
                    MessageBox.Show("Aeg sai läbi!\nLahendatud küsimusi: " + lahendatudKüsimused, "Matemaatikaviktoriin");
                }
                else
                {
                    MessageBox.Show("Aeg sai läbi!", "Matemaatikaviktoriin");
                }
            }
        }

        // Vastuse kast

        private void Vastus_Enter(object sender, EventArgs e)
        {
            NumericUpDown vastuseKast = sender as NumericUpDown;

            if (vastuseKast != null)
            {
                int vastusePikkusTähtedes = vastuseKast.Value.ToString().Length;

                vastuseKast.Select(0, vastusePikkusTähtedes);
            }
        }

        // Tume teema

        private void TumeTeema(object sender, EventArgs e)
        {
            Taustavärv = Color.Black;
            Esiplaanivärv = Color.White;

            RakendaTeema();
        }

        // Hele teema

        private void HeleTeema(
            object sender,
            EventArgs e)
        {
            Taustavärv = Color.White;
            Esiplaanivärv = Color.Black;

            RakendaTeema();
        }

        // Rakenda teema

        private void RakendaTeema()
        {
            BackColor = Taustavärv;
            ForeColor = Esiplaanivärv;

            RakendaLabeliTeema(pealkiri);

            RakendaNupuTeema(alustaMänguNupp);
            RakendaNupuTeema(seadeteNupp);

            RakendaLabeliTeema(raskusLabel);
            RakendaLabeliTeema(mänguRežiimLabel);
            RakendaLabeliTeema(parameetridLabel);

            RakendaLabeliTeema(praeguneRaskusLabel);
            RakendaLabeliTeema(praeguneRežiimLabel);
            RakendaLabeliTeema(aegParameeterLabel);
            RakendaLabeliTeema(minimaalneParameeterLabel);
            RakendaLabeliTeema(maksimaalneParameeterLabel);

            RakendaNupuTeema(lihtneNupp);
            RakendaNupuTeema(keskmineNupp);
            RakendaNupuTeema(raskeNupp);
            RakendaNupuTeema(kohandatudNupp);

            RakendaNupuTeema(standardneNupp);
            RakendaNupuTeema(pidevNupp);
            RakendaNupuTeema(tagasiNupp);

            RakendaNumbriTeema(kohandatudAeg);
            RakendaNumbriTeema(kohandatudArv);

            RakendaLabeliTeema(aegAllLabel);
            RakendaLabeliTeema(lahendatudKüsimusedLabel);

            RakendaLabeliTeema(küsimus1Label);
            RakendaLabeliTeema(küsimus2Label);
            RakendaLabeliTeema(küsimus3Label);
            RakendaLabeliTeema(küsimus4Label);

            RakendaNumbriTeema(vastus1);
            RakendaNumbriTeema(vastus2);
            RakendaNumbriTeema(vastus3);
            RakendaNumbriTeema(vastus4);

            RakendaNupuTeema(kontrolliVastusedNupp);
            RakendaNupuTeema(peaMenüüNupp);


            if (aegLabel != null)
            {
                // Säilita punane taimer, kui jäänud on viis sekundit või vähem.
                if (aegAllesJäänud <= 5 && taimer1.Enabled)
                {
                    aegLabel.BackColor = Color.Red;
                    aegLabel.ForeColor = Color.White;
                }
                else if (aegLabel.Text == "Aeg sai läbi!")
                {
                    aegLabel.BackColor = Color.Red;
                    aegLabel.ForeColor = Color.White;
                }
                else
                {
                    aegLabel.BackColor = Taustavärv;
                    aegLabel.ForeColor = Esiplaanivärv;
                }
            }
        }

        // Teema meetodid

        private void RakendaLabeliTeema(Label label)
        {
            if (label != null)
            {
                label.ForeColor = Esiplaanivärv;
                label.BackColor = Taustavärv;
            }
        }

        private void RakendaNupuTeema(Button nupp)
        {
            if (nupp != null)
            {
                nupp.BackColor = Taustavärv;
                nupp.ForeColor = Esiplaanivärv;
            }
        }

        private void RakendaNumbriTeema(
            NumericUpDown number)
        {
            if (number != null)
            {
                number.BackColor = Taustavärv;
                number.ForeColor = Esiplaanivärv;
            }
        }
    }
}