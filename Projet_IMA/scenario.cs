using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Projet_IMA
{
    class Scenario
    {

        //////////////////////////////////////////////////////////////
        //
        //   retourne un nombre entier entre 0 et max (non compris)
        //

        private static Random Ran = new Random();
        static private int RandP(int max) { return (Ran.Next() % max); }

        static private double Gaussian()
        {
            double u1 = Ran.NextDouble();
            double u2 = Ran.NextDouble();
            double randStdNormal = Math.Sqrt(-2.0 * Math.Log(u1)) *
                      Math.Sin(2.0 * Math.PI * u2);
            if (randStdNormal < -1) randStdNormal = 0;
            if (randStdNormal > 1) randStdNormal = 0;
            return randStdNormal;
        }

        //////////////////////////////////////////////////////////////
        //
        //   Cree un ensemble de points aléatoires
        //

        static private void AddPoint(List<V2> LP)
        {
             V2 P = RandomPoint();
            // pour éviter d'insérer un point existant dans la liste
             foreach (V2 PP in LP)
                 if (P.Near(PP)) return;
            
              LP.Add(P);
        }

        static public void CreateRandomPoint(List<V2> LP, int nb)
        {
            LP.Clear();
            while ( LP.Count != nb )
                AddPoint(LP);
            typerandom = (typerandom + 1) % 3;
        }

        static int typerandom = 0;
        
        static private V2 RandomPoint()
        {
 
            // uniform
            if (typerandom == 0)
            {
                int Larg = 300;
                int Haut = 300;

                int x = RandP(Larg)  + 100;
                int y = RandP(Haut)  + 100;
                V2 P = new V2(x, y);
                return P;
            }

            // spherique
            if (typerandom == 1)
            {
                double t = Ran.NextDouble()*Math.PI*2;

                double R = 150;
                
                int x = (int) (R * Math.Cos(t) + R + 50);
                int y = (int) (R * Math.Sin(t) + R + 50);
                V2 P = new V2(x, y);
                return P;
            }

            // gaussien
            if (typerandom == 2)
            {
                int Larg = 200;
                int Haut = 200;

                int x = (int) (Gaussian() * Larg) + 250;
                int y = (int) (Gaussian() * Haut) + 250;
                V2 P = new V2(x, y);
                return P;
            }

            

            return new V2(0,0);
        }

        //////////////////////////////////////////////////////////////
        ///
        ///  retourne les points associés a un scenario
        ///  

        static int typescenar = 0;

        static V2[] Alignement = { new V2(100, 100), new V2(100, 150), new V2(100, 200), new V2(170, 270),
                                   new V2(240, 340), new V2(310, 410), new V2(500, 410), new V2(550, 380),
                                   new V2(550, 100), };

        static V2[] Peigne =  { new V2(1, 1), new V2(1, 3), new V2(2,3), new V2(2, 2),
                                    new V2(3, 2), new V2(3, 3), new V2(4,3), new V2(4,2),
                                    new V2(5, 2), new V2(5, 3), new V2(6,3), new V2(6,2),
                                    new V2(7,2),  new V2(7,3) , new V2(8,3), 
                                    new V2(8,0), new V2(7,0), new V2(7,1), new V2(6,1),
                                    new V2(6,0), new V2(5,0),  new V2(5,1), new V2(4,1),
                                    new V2(4,0), new V2(1,0)
                                  };





        static public void GetScenario(List<V2> LP)
        {
            LP.Clear();
             
            if (typescenar  == 0)
                foreach (V2 P in Alignement)
                    LP.Add(P);

            if (typescenar == 1)
                foreach (V2 P in Peigne)
                    LP.Add(P*100+new V2(0,100));

            if (typescenar == 2)
            {
                double deg = 0;
                double R = 20;
                for (int i = 0; i < 30; i++)
                {
                    int x = (int)(Math.Cos(deg) * R + 400);
                    int y = (int)(Math.Sin(deg) * R + 300);
                    LP.Add(new V2(x, y));
                    R *= 1.1;
                    deg += 1;
                }
            }

            if (typescenar == 3)
            {
                double deg = 0;
                double R = 250;
                for (int i = 0; i < 30; i++)
                {
                    int x = (int)(Math.Cos(deg) * R + 400);
                    int y = (int)(Math.Sin(deg) * R + 300);
                    LP.Add(new V2(x, y));
                    R *= 0.95;
                    deg += 0.8;
                }
            }

            if (typescenar == 4)
            {
                int xx = 100;
                int ry = 10;
                int h = 30;
                LP.Add(new V2(20, h));
                for (int i = 0; i < 10; i++)
                {

                    LP.Add(new V2(xx, h));
                    xx += 20;
                    LP.Add(new V2(xx, h+ry));
                    ry = (int) (ry * 1.3 + 2);
                    xx += 20;
                }
                LP.Add(new V2(xx, h ));
                LP.Add(new V2(xx+20, 300));

            }

            typescenar = (typescenar+1)%5;

        }
	}
}

//// creation ligne polygonale
//// non selfintersecting