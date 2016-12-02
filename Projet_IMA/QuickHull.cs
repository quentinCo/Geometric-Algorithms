using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Drawing;
using System.Numerics;

namespace Projet_IMA
{
    class QuickHull
    {
        static public LinkedList<V2> convexHull;
        static private LinkedList<Tuple<Tuple<LinkedListNode<V2>, LinkedListNode<V2>>, List<V2>>> setToProcess; // List pair cote de l'enveloppe et set de point

        static public void start()
        {
            Console.WriteLine("Start_QuickHull");

            List<V2> points = SetofPoints.LP;

            convexHull = new LinkedList<V2>();
            setToProcess = new LinkedList<Tuple<Tuple<LinkedListNode<V2>, LinkedListNode<V2>>, List<V2>>>();

            V2 minXPoint = points[0];
            V2 maxXPoint = minXPoint;

            foreach(V2 point in points)
            {
                if (minXPoint.x > point.x || (minXPoint.x == point.x && minXPoint.y > point.y))
                    minXPoint = point;

                if (maxXPoint.x < point.x || (maxXPoint.x == point.x && maxXPoint.y < point.y))
                    maxXPoint = point;
            }

            convexHull.AddFirst(maxXPoint);
            convexHull.AddFirst(minXPoint);

            addSetToSetToPrecess(convexHull.First, convexHull.Last, points);
            addSetToSetToPrecess(convexHull.Last, convexHull.First, points);

            convexHull.AddLast(convexHull.First.Value); // Pour créer la boucle

            Affichage.DrawPolChain(convexHull.ToList<V2>(), Color.Red);
            Affichage.Show();
        }


        static public void Iteration()
        {
            if (setToProcess.Count == 0)    // Tant qu'il y a des ensembles de point à traiter
                return;
            
            Console.WriteLine("Iteration_QuickHull");

            Tuple<Tuple<LinkedListNode<V2>, LinkedListNode<V2>>, List<V2>> pairToProcess = setToProcess.First();    // Récupération du coté et des points à traiter.
            Tuple<LinkedListNode<V2>, LinkedListNode<V2>> pointsMainVector = pairToProcess.Item1;                   // Récupération des points composant le coté.
            List<V2> points = pairToProcess.Item2;                                                                  // Récupération des l'ensemble où peut être le nouveau point

            V2 furthestPoint = findFurthestPoint(pointsMainVector.Item1.Value, pointsMainVector.Item2.Value, points);   // Point le plus éloigner de la droite.

            LinkedListNode<V2> furtherPointNode = convexHull.AddAfter(pointsMainVector.Item1, furthestPoint);       // Ajout du point entre les points délimitant le coté.

            setToProcess.RemoveFirst();

            addSetToSetToPrecess(furtherPointNode, pointsMainVector.Item2, points);     // Ajout d'un nouveau set à traiter et coté.
            addSetToSetToPrecess(pointsMainVector.Item1, furtherPointNode, points);

            /* Draw */
            Affichage.RefreshScreen();
            Affichage.DrawSet(SetofPoints.LP, Color.Blue);
            Affichage.DrawPolChain(convexHull.ToList<V2>(), Color.Red);
            Affichage.Show();
        }

        /* Ajout d'une association coté - ensemble */
        static private void addSetToSetToPrecess(LinkedListNode<V2> mp1, LinkedListNode<V2> mp2, List<V2> points)
        {
            Tuple<LinkedListNode<V2>, LinkedListNode<V2>> pairPoints = new Tuple<LinkedListNode<V2>, LinkedListNode<V2>>(mp1, mp2);
            List<V2> newSet = splitSet(mp1.Value, mp2.Value, points);

            if (newSet.Count > 0)
                setToProcess.AddFirst(new Tuple<Tuple<LinkedListNode<V2>, LinkedListNode<V2>>, List<V2>>(pairPoints, newSet));
        }

        /* Separation des groupes de point droite */
        static private List<V2> splitSet(V2 mp1, V2 mp2, List<V2> points)
        {
            List<V2> res = new List<V2>();  // Points restant à traiter.
            V2 mainVector = mp2 - mp1;      // Nouveau coté de l'enveloppe

            foreach(V2 point in points)
            {
                V2 vector = point - mp1;
                BigInteger prodVec = vector ^ mainVector;
                if (prodVec > 0)
                    res.Add(point);
            }
            return res;
        }

        /* Trouve le point le plus proche du vecteur */
        static private V2 findFurthestPoint(V2 mp1, V2 mp2, List<V2> points)
        {
            V2 mainVector = mp2 - mp1;
            V2 furthestPoint = points[0];

            double maxDistance = distanceFromMainVector(mp1, mainVector, furthestPoint);

            foreach (V2 point in points)
            {
                double distance = distanceFromMainVector(mp1, mainVector, point);
                if(distance > maxDistance)
                {
                    furthestPoint = point;
                    maxDistance = distance;
                }
            }

            return furthestPoint;
        }

        static private double distanceFromMainVector(V2 firstMainPoint, V2 mainVector, V2 point)
        {
            // ax + by + c -> V(b; -a)
            double numerator = (double)BigInteger.Abs(mainVector.x * (firstMainPoint.y - point.y) - mainVector.y * (firstMainPoint.x - point.x));
            double denominator = Math.Sqrt((double)(mainVector.x * mainVector.x + mainVector.y * mainVector.y));

            return numerator / denominator;
        }
    }
}