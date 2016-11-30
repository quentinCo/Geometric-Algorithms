using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Drawing;
using System.Numerics;

namespace Projet_IMA
{
    class GiftWrap
    {
        static public List<V2> HP;

        static public void start()
        {
            Console.WriteLine("Sart_GiftWrap");
            List<V2> points = SetofPoints.LP;
            HP = new List<V2>();
            V2 initPoint = points[0];
            foreach (V2 point in points)
                if (initPoint.x > point.x)
                    initPoint = point;
                else if (initPoint.x == point.x && initPoint.y > point.y)
                    initPoint = point;

            HP.Add(initPoint);
        }


        static public void Iteration()
        {
            Console.WriteLine("Iteration_GiftWrap");
            if (HP.Count != 1 && HP[0].Equals(HP.Last()))
                return;

            V2 extremPoint = HP.Last();     // Dernier point du contour.
            List<V2> points = SetofPoints.LP; // voire, deplacer vers dans la classe.
            V2 newP = points[0];    // nouveau point supposer pour le contour.

            for(int i = 1; i < points.Count; ++i)
            {
                V2 leftMost = extremPoint - newP;   // Vecteur supposé le plus à gauche.
                V2 tempVector = extremPoint - points[i];    // Vecteur à tester.

                BigInteger prodVec = leftMost ^ tempVector;
                if (newP.Equals(extremPoint) || prodVec > 0 || (prodVec == 0 && (leftMost.Norme2() < tempVector.Norme2())))
                    newP = points[i];
            }
            HP.Add(newP);
            Affichage.DrawPolChain(HP, Color.Red);
            Affichage.Show();
        }
   }
}
