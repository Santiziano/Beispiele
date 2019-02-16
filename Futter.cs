using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing;

namespace Ameise
{
    class Futter
    {
        int Anzahl;
        int PositionX;
        int PositionY;
        public static int vorkommen = 0;

        public int Anz
        {
            get
            {
                return Anzahl;
            }
            set
            {
                Anzahl = value;
            }
        }

        public int PosiX
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

        public int PosiY
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
        //Futer erstellen
        public Futter(int x, int y, int A)
        {
            Anz = A;
            PositionY = y;
            PositionX = x;
            vorkommen++;
        }
        //Futter löschen
        public void DeFutter()
        {
            Anz = -1;
            PosiX = -1;
            PosiY = -1;
            vorkommen--;
        }
    }
}
