using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Threading;
using System.Media;
using System.IO;

namespace Ameise
{

    public partial class Form1 : Form
    {
        //Variablen
        Ameise Joe;
        Futter[] Essen = new Futter[121];
        SoundPlayer sound;
        bool playing = false;
        bool enable = true;
        bool sucht = false;

        public Form1()
        {
            InitializeComponent();
            Joe = new Ameise(this);

            //Lesen ob Musik 1 oder 0 1=An 0=Aus
            FileStream fs = new FileStream("Speicher", FileMode.OpenOrCreate, FileAccess.ReadWrite);
            StreamReader file1 = new StreamReader(fs);

            if (new FileInfo("Speicher").Length == 0)
            {
                enable = true;
                mnu_an.Checked = true;
                btn_mute.Text = "Musik An";
                sound = new SoundPlayer((Environment.CurrentDirectory + "\\90.wav"));
            }
            else
            {
                if (Convert.ToInt32(file1.ReadLine()) == 1)
                {
                    enable = true;
                    mnu_an.Checked = true;
                    btn_mute.Text = "Musik An";
                }
                else if (Convert.ToInt32(file1.ReadLine()) == 0)
                {
                    enable = false;
                    mnu_aus.Checked = true;
                    btn_mute.Text = "Musik Aus";
                }
                file1.Close();
                sound = new SoundPlayer((Environment.CurrentDirectory + "\\90.wav"));
            }
        }

        public void Button_click(object sender, EventArgs e)
        {
            if (txt_anzahl.Text != "" && Convert.ToInt32(txt_anzahl.Text) >= 0)
            {

            }
        }

        private void Btn_Legen_Click(object sender, EventArgs e)
        {
            //Textbox Farben auf Weiß
            txt_x.BackColor = Color.White;
            txt_y.BackColor = Color.White;
            txt_anzahl.BackColor = Color.White;
            lbl_err.Visible = false;
            int fehler = 0;

            //Überprüfungen ob Das Futter gesetzt werden darf
            if (Futter.vorkommen >= 120)
            {
                Error("So viel kann die Ameise gar nicht Essen");
            }
            else
            {
                if (txt_x.Text == "" || Convert.ToInt32(txt_x.Text) < 0 || Convert.ToInt32(txt_x.Text) > 10)
                {
                    fehler = 1;
                }
                else if (txt_y.Text == "" || Convert.ToInt32(txt_y.Text) < 0 || Convert.ToInt32(txt_y.Text) > 10)
                {
                    fehler = 2;
                }
                else if (txt_anzahl.Text == "" || Convert.ToInt32(txt_anzahl.Text) <= 0)
                {
                    fehler = 3;
                }
                else if (txt_x.Text == "5" && txt_y.Text == "5")
                {
                    fehler = 4;
                }
                else
                {
                    //Nur beim erstenmal Futter setzen
                    if (Futter.vorkommen == 0)
                    {
                        //Futter setzen
                        Essen[Futter.vorkommen] = new Futter(Convert.ToInt32(txt_x.Text), Convert.ToInt32(txt_y.Text), Convert.ToInt32(txt_anzahl.Text));
                        Joe.Färben(Convert.ToInt32(txt_x.Text), Convert.ToInt32(txt_y.Text), this, Essen[Futter.vorkommen - 1]);
                    }
                    else
                    {
                        bool x = false;
                        bool y = false;
                        bool done = false;

                        //Überprüfung ob Futter an dieser Stelle schon vorkommt
                        for (int i = 0; i < Futter.vorkommen && done != true; i++)
                        {
                            if (Essen[i].PosiX == Convert.ToInt32(txt_x.Text))
                            {
                                x = true;
                            }
                            if (Essen[i].PosiY == Convert.ToInt32(txt_y.Text))
                            {
                                y = true;
                            }
                            if (x != true || y != true)
                            {
                                //Futter setzen
                                Essen[Futter.vorkommen] = new Futter(Convert.ToInt32(txt_x.Text), Convert.ToInt32(txt_y.Text), Convert.ToInt32(txt_anzahl.Text));
                                Joe.Färben(Convert.ToInt32(txt_x.Text), Convert.ToInt32(txt_y.Text), this, Essen[Futter.vorkommen - 1]);
                                done = true;
                            }
                        }
                    }

                }
                //Bei Fehlern Fehlercode auslesen und betreffende Textbox Markieren
                if (fehler != 0)
                {
                    switch (fehler)
                    {
                        case 1:
                            {
                                txt_x.BackColor = Color.Red;
                                break;
                            }
                        case 2:
                            {
                                txt_y.BackColor = Color.Red;
                                break;
                            }
                        case 3:
                            {
                                txt_anzahl.BackColor = Color.Red;
                                break;
                            }
                        case 4:
                            {
                                txt_x.BackColor = Color.Red;
                                txt_y.BackColor = Color.Red;
                                break;
                            }
                        default:
                            {
                                break;
                            }
                    }
                }
            }
        }
        public void Error(string a)
        {
            lbl_err.Text = a;
            lbl_err.Visible = true;
        }

        private void Btn_Home_Click(object sender, EventArgs e)
        {
            //Home Button
            timer.Stop();
            timer2.Start();
            sucht = false;
        }

        private void Btn_Such_Click(object sender, EventArgs e)
        {
            //Suchen Button
            timer.Start();
            if (playing == false && enable == true)
            {
                playing = true;
                sound.Play();
            }
        }

        private void Timer_Tick(object sender, EventArgs e)
        {
            sucht = true;
            if (Joe.Suchen(this, Essen) == true)
            {
                timer.Stop();
                timer2.Start();
                sucht = false;
            }
        }

        private void Timer2_Tick(object sender, EventArgs e)
        {
            if (Joe.Home(this) == true)
            {
                timer2.Stop();
                sound.Stop();
                playing = false;
            }
        }

        private void Btn_mute_Click(object sender, EventArgs e)
        {
            //Musik ein und aus schalten, kommt auf den Momentanen status an
            if (enable == true)
            {
                enable = false;
                btn_mute.Text = "Musik Aus";
            }
            else
            {
                enable = true;
                btn_mute.Text = "Musik An";
            }
            if (playing == true)
            {
                sound.Stop();
                playing = false;
            }
            else
            {
                if (sucht == true && playing == false)
                {
                    sound.Play();
                    playing = true;
                }
            }
        }

        private void Btn_delete_Click(object sender, EventArgs e)
        {
            //Futter löschen
            if (txt_x.Text != "" && txt_y.Text != "")
            {
                Joe.Delete(Essen, Convert.ToInt32(txt_x.Text), Convert.ToInt32(txt_y.Text));
            }

        }

        private void AnToolStripMenuItem_Click(object sender, EventArgs e)
        {
            //Musik ein in den Speicher schreiben
            if (mnu_an.Checked == false)
            {
                FileStream fs = new FileStream("Speicher", FileMode.OpenOrCreate, FileAccess.ReadWrite);
                mnu_an.Checked = true;
                mnu_aus.Checked = false;
                StreamWriter file2 = new StreamWriter(fs);
                file2.WriteLine("1");
                file2.Close();
            }
        }

        private void Mnu_aus_Click(object sender, EventArgs e)
        {
            //Musik aus in den Speicher schreiben
            if (mnu_aus.Checked == false)
            {
                mnu_aus.Checked = true;
                mnu_an.Checked = false;
                FileStream fs = new FileStream("Speicher", FileMode.OpenOrCreate, FileAccess.ReadWrite);
                StreamWriter file3 = new StreamWriter(fs);
                file3.WriteLine("0");
                file3.Close();
            }
        }

        private void ÜberToolStripMenuItem_Click(object sender, EventArgs e)
        {
            //Über Button
            MessageBox.Show("Dieses Programm wurde von Tizian Weinert geschrieben und DAU gesichert", "Über", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }

        private void HilfeToolStripMenuItem_Click(object sender, EventArgs e)
        {
            //Hilfe Button
            MessageBox.Show("Version 1.0 \n\n -Die Musik können Sie unten Rechts deaktivieren und\n  Aktivieren\n \n -Wenn Sie die Musik immer deaktiviert oder aktiviert haben\n  möchten, können Sie unter dem Menü Punkt Spiel die\n  Einstellung für den Start des Programms Festlegen.", "Hilfe", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }

        private void Form1_FormClosing_1(object sender, FormClosingEventArgs e)
        {
            //Form Schließen
            if (e.CloseReason == CloseReason.UserClosing)
            {
                DialogResult result = MessageBox.Show("Sind Sie sicher das Sie dieses Programm beenden möchten", "Beenden", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
                if (result == DialogResult.Yes)
                {
                    Application.ExitThread();
                }
                else if (result == DialogResult.No)
                {
                    e.Cancel = true;
                }
            }
        }
    }
}
