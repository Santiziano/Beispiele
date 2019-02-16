using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing;
using System.Threading;

namespace Ameise
{
    class Ameise
    {
        //Variablen
        readonly int HomePosX;
        readonly int HomePosY;
        int HomeAnzahl;
        bool Traegt;
        int PositionX;
        int PositionY;
        int PrepoX;
        int PrepoY;
        Feld Wiese;
        bool Grün = false;
        
        public int HmPosX
        {
            get
            {
                return HomePosX;
            }
        }

        public int HmPosY
        {
            get
            {
                return HomePosY;
            }
        }

        public int HmAnzahl
        {
            get
            {
                return HomeAnzahl;
            }
            set
            {
                HomeAnzahl = value;
            }
        }

        public bool Trgt
        {
            get
            {
                return Traegt;
            }
            set
            {
                Traegt = value;
            }
        }

        public int PositX
        {
            get
            {
                return PositionX;
            }
            set
            {
                PositionX = value;
            }
        }

        public int PositY
        {
            get
            {
                return PositionY;
            }
            set
            {
                PositionY = value;
            }
        }

        public Feld Wiesen
        {
            get
            {
                return Wiese;
            }
            set
            {
                Wiese = value;
            }

        }

        
        //Ameise erstellen
        public Ameise(Form1 f)
        {

            HomePosX = 5;
            HomePosY = 5;
            HomeAnzahl = 0;
            Trgt = false;
            PositionX = 5;
            PositionY = 5;
            Wiese = new Feld(f);
            PrepoX = 5;
            PrepoY = 5;


            Wiese.Butbut[HomePosX, HomePosY].BackColor = Color.Black;
            Wiese.Butbut[HomePosX, HomePosY].Text = "A";
            Wiese.Butbut[HomePosX, HomePosY].ForeColor = Color.White;
        }
        //Position malen und aktuallisieren
        public void Aktuell(Form1 f)
        {
            //Vorherige Position säubern
            if (PrepoX != 5 || PrepoY != 5)
            {
                if (Grün == true)
                {
                    Wiese.Butbut[PrepoX, PrepoY].BackColor = Color.Green;
                    Wiese.Butbut[PrepoX, PrepoY].BackgroundImage = null;
                }
                else
                {
                    Wiese.Butbut[PrepoX, PrepoY].BackgroundImage = null;
                }
                f.Update();
            }
            //Neue Position Setzen
            if (PositX != 5 || PositY != 5)
            {

                if (Wiese.Butbut[PositX, PositY].BackColor == Color.Green)
                {
                    Grün = true;
                }
                else
                {
                    Grün = false;
                }
                Wiese.Butbut[PositX, PositY].BackgroundImage = Image.FromFile(Environment.CurrentDirectory + "\\Ameise.png");
                f.Update();

            }

        }
        //Futter Färben
        public void Färben(int x, int y, Form1 f, Futter e)
        {
            if (x != HmPosX || y != HmPosY)
            {
                Wiese.Butbut[x, y].BackColor = Color.Green;
                Wiese.Butbut[x, y].Text = Convert.ToString(e.Anz);
            }
            else
            {
                f.Error("Futter kann nicht auf die Home Position Gesetzt werden");

            }
        }
        //Nach Hause gehen
        public bool Home(Form1 f)
        {


            if (PositY < HmPosY)
            {
                PrepoX = PositX;
                PrepoY = PositY;
                PositionY++;
                Aktuell(f);
            }
            else
            {
                if (PositY > HmPosY)
                {
                    PrepoX = PositX;
                    PrepoY = PositY;
                    PositionY--;
                    Aktuell(f);
                }
            }

            if (PositX < HmPosX)
            {
                PrepoX = PositX;
                PrepoY = PositY;
                PositX++;
                Aktuell(f);
            }
            else
            {
                if (PositX > HmPosX)
                {
                    PrepoX = PositX;
                    PrepoY = PositionY;
                    PositX--;
                    Aktuell(f);
                }
            }
            //Futter Abladen
            if (PositX == HmPosX && PositY == HmPosY)
            {
                Wiese.Butbut[HmPosX, HmPosY].Text = "A";
                Aktuell(f);
                if (Trgt == true)
                {
                    HomeAnzahl++;
                    Trgt = false;
                }
                return true;
            }
            return false;
        }

        //Futter suchen
        public bool Suchen(Form1 f, Futter[] e)
        {

            Random Rnd = new Random();
            Wiese.Butbut[HmPosX, HmPosY].Text = "";
            Trgt = false;
            Traegt = false;
            Wiese.Butbut[HmPosX, HmPosY].Text = Convert.ToString(HomeAnzahl);
            bool fertig = false;

            int zahl = 0;

            zahl = Rnd.Next(1, 5);

            //Positions Entscheidung
            //1=Left, 2=Up, 3=Right, 4=Down
            PrepoX = PositX;
            PrepoY = PositY;
            switch (zahl)
            {
                case 1:
                    {
                        if (PositX > 0)
                        {
                            PositX--;
                            Aktuell(f);
                            break;
                        }
                        else
                        {
                            break;
                        }
                    }

                case 2:
                    {
                        if (PositY > 0)
                        {
                            PositY--;
                            Aktuell(f);
                            break;
                        }
                        else
                        {
                            break;
                        }
                    }

                case 3:
                    {
                        if (PositX < 10)
                        {
                            PositX++;
                            Aktuell(f);
                            break;
                        }
                        else
                        {
                            break;
                        }
                    }

                case 4:
                    {
                        if (PositY < 10)
                        {
                            PositY++;
                            Aktuell(f);
                            break;
                        }
                        else
                        {
                            break;
                        }
                    }


            }
            //Futter nehmen
            for (int i = 0; i < Futter.vorkommen; i++)
            {
                if (PositX == e[i].PosiX && PositY == e[i].PosiY)
                {
                    if (e[i].Anz > 0)
                    {

                        e[i].Anz--;

                        Wiese.Butbut[PositX, PositY].Text = Convert.ToString(e[i].Anz);
                        f.Update();
                        Trgt = true;

                        fertig = true;


                        if (e[i].Anz == 0)
                        {
                            Wiese.Butbut[e[i].PosiX, e[i].PosiY].Text = "";
                            Wiese.Butbut[e[i].PosiX, e[i].PosiY].BackColor = Color.Transparent;

                            //Defragmentieren
                            for (int a = 0; a < Futter.vorkommen; a++)
                            {
                                if (e[a].Anz == 0 && e[a + 1] != null)
                                {
                                    e[a].Anz = e[a + 1].Anz;
                                    e[a].PosiX = e[a + 1].PosiX;
                                    e[a].PosiY = e[a + 1].PosiY;

                                    e[a + 1].Anz = 0;
                                    e[a + 1].PosiX = 0;
                                    e[a + 1].PosiY = 0;

                                }
                            }
                            Futter.vorkommen--;
                            Grün = false;
                            i = 0;
                        }

                    }


                }

            }
            return fertig;

        }
        //Futer löschen
        public void Delete(Futter[] e, int x, int y)
        {
            for (int i = 0; i < Futter.vorkommen; i++)
            {
                if (e[i].PosiX == x && e[i].PosiY == y)
                {
                    Wiese.Butbut[e[i].PosiX, e[i].PosiY].Text = "";
                    Wiese.Butbut[e[i].PosiX, e[i].PosiY].BackColor = Color.Transparent;


                    //Defragmentieren
                    for (int a = 0; a < Futter.vorkommen; a++)
                    {
                        if (e[a].Anz == 0 && e[a + 1] != null)
                        {
                            e[a].Anz = e[a + 1].Anz;
                            e[a].PosiX = e[a + 1].PosiX;
                            e[a].PosiY = e[a + 1].PosiY;

                            e[a + 1].Anz = 0;
                            e[a + 1].PosiX = 0;
                            e[a + 1].PosiY = 0;

                        }
                    }
                    Futter.vorkommen--;
                }
            }
        }

    }
}
