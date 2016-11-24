using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Drawing;
using System.Drawing.Imaging;


namespace Projet_IMA
{
    class Affichage
    {
        static private Bitmap B;
        static private int Largeur;
        static private int Hauteur;
        static Graphics g;
        
        static public Bitmap Init(int largeur, int hauteur)
        {
            Largeur = largeur;
            Hauteur = hauteur;
            B = new Bitmap(largeur, hauteur);
            g = Graphics.FromImage(B);
            return B;
        }


       

        /// /////////////////   public methods ///////////////////////

        // efface l'écran
        static public void RefreshScreen()
        {
            g.Clear(Color.White);
        }

        // force l'affichage a l'ecran
        static public void Show()
        {
            Program.MyForm.PictureBoxInvalidate();
            Program.MyForm.PictureBoxRefresh();
        }

        // dessine un segment
        public static void Segment(V2 a, V2 b,Color c)
        {
            int x_ecran1 = (int) a.x;
            int y_ecran1 = (int) (Hauteur - a.y);

            int x_ecran2 = (int) b.x;
            int y_ecran2 = (int) (Hauteur - b.y);

            g.DrawLine(new Pen(c), x_ecran1, y_ecran1, x_ecran2, y_ecran2);
        }
        
        // dessine un point
        public static void Point(V2 P, Color c)
        {
            int x_ecran = (int)P.x;
            int y_ecran = (int)(Hauteur - P.y);

            g.DrawLine(new Pen(c), x_ecran-2, y_ecran-2,x_ecran+2,y_ecran+2);
            g.DrawLine(new Pen(c), x_ecran+2, y_ecran-2,x_ecran-2,y_ecran+2);
        }

        // dessine un ensemble de points
        public static void DrawSet(List<V2> L, Color c)
        {
            foreach (V2 P in L)
                Point(P, c);
        }

        public static void DrawPolChain(List<V2> L, Color c)
        {
            for (int i = 0 ; i < L.Count-1 ; i++)
                Segment(L[i],L[i+1],c);
        }

    }
}
