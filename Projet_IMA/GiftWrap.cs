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
        static public List<V2> convexHull;

        static public void start()
        {
            Console.WriteLine("Sart_GiftWrap");

            List<V2> points = SetofPoints.LP;
            convexHull = new List<V2>();

            V2 initPoint = points[0];
            foreach (V2 point in points)
            {
                if (initPoint.x > point.x || (initPoint.x == point.x && initPoint.y > point.y))
                    initPoint = point;
            }

            convexHull.Add(initPoint);
        }


        static public void Iteration()
        {
            Console.WriteLine("Iteration_GiftWrap");

            if (convexHull.Count != 1 && convexHull[0].Equals(convexHull.Last()))
                return;

            V2 lastPoint = convexHull.Last();             // Dernier point du contour.
            List<V2> points = SetofPoints.LP;               
            V2 leftmostPoint = points[0];                 // Nouveau point supposer pour le contour.

            for(int i = 1; i < points.Count; ++i)
            {
                V2 leftmostVector = leftmostPoint - lastPoint;        // Vecteur supposé le plus à gauche.
                V2 tempVector = points[i] - lastPoint;                // Vecteur à tester.

                BigInteger prodVec = leftmostVector ^ tempVector;
                if (leftmostPoint.Equals(lastPoint) || prodVec > 0 || (prodVec == 0 && (leftmostVector.Norme2() < tempVector.Norme2())))
                    leftmostPoint = points[i];
            }
            convexHull.Add(leftmostPoint);

            Affichage.DrawPolChain(convexHull, Color.Red);
            Affichage.Show();
        }
   }
}
