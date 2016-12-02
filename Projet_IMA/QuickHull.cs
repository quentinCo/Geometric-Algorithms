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
        static public LinkedList<V2> HP;
        static private LinkedList<Tuple<Tuple<LinkedListNode<V2>, LinkedListNode<V2>>, List<V2>>> setToProcess; // List pair axe et set de point

        static public void start()
        {
            Console.WriteLine("Start_QuickHull");
            List<V2> points = SetofPoints.LP;
            HP = new LinkedList<V2>();
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

            HP.AddFirst(maxXPoint);
            HP.AddFirst(minXPoint);

            Tuple<LinkedListNode<V2>, LinkedListNode<V2>> pairPoints = new Tuple<LinkedListNode<V2>, LinkedListNode<V2>>(HP.First, HP.Last);
            List<V2> newSet = SplitSet(minXPoint, maxXPoint, points);
            if(newSet.Count > 0)
                setToProcess.AddFirst(new Tuple<Tuple<LinkedListNode<V2>, LinkedListNode<V2>>, List<V2>>(pairPoints, newSet));

            pairPoints = new Tuple<LinkedListNode<V2>, LinkedListNode<V2>>(HP.Last, HP.First);
            newSet = SplitSet(maxXPoint, minXPoint, points);
            if (newSet.Count > 0)
                setToProcess.AddFirst(new Tuple<Tuple<LinkedListNode<V2>, LinkedListNode<V2>>, List<V2>>(pairPoints, newSet));

            HP.AddLast(HP.First.Value);

            Affichage.DrawPolChain(HP.ToList<V2>(), Color.Red);
            Affichage.Show();
        }


        static public void Iteration()
        {
            if (setToProcess.Count == 0)
                return;
            
            Console.WriteLine("Iteration_QuickHull");

            Tuple<Tuple<LinkedListNode<V2>, LinkedListNode<V2>>, List<V2>> pairToProcess = setToProcess.First();
            Tuple<LinkedListNode<V2>, LinkedListNode<V2>> pointsMainVector = pairToProcess.Item1;
            List<V2> points = pairToProcess.Item2;

            V2 furthestPoint = findFurthestPoint(pointsMainVector.Item1.Value, pointsMainVector.Item2.Value, points);

            List<V2> newSet1 = SplitSet(pointsMainVector.Item1.Value, furthestPoint, points);
            List<V2> newSet2 = SplitSet(furthestPoint, pointsMainVector.Item2.Value, points);

            LinkedListNode<V2> furtherPointNode = HP.AddAfter(pointsMainVector.Item1, furthestPoint);

            setToProcess.RemoveFirst();

            Tuple<LinkedListNode<V2>, LinkedListNode<V2>> pairPoints;
            if (newSet2.Count > 0)
            {
                pairPoints = new Tuple<LinkedListNode<V2>, LinkedListNode<V2>>(furtherPointNode, pointsMainVector.Item2);
                setToProcess.AddFirst(new Tuple<Tuple<LinkedListNode<V2>, LinkedListNode<V2>>, List<V2>>(pairPoints, newSet2));
            }

            if (newSet1.Count > 0)
            {
                pairPoints = new Tuple<LinkedListNode<V2>, LinkedListNode<V2>>(pointsMainVector.Item1, furtherPointNode);
                setToProcess.AddFirst(new Tuple<Tuple<LinkedListNode<V2>, LinkedListNode<V2>>, List<V2>>(pairPoints, newSet1));
            }
            
            Affichage.DrawPolChain(HP.ToList<V2>(), Color.Red);
            Affichage.Show();
        }

        /* Separation des groupes de point droite */
        static private List<V2> SplitSet(V2 mp1, V2 mp2, List<V2> points)
        {
            List<V2> res = new List<V2>();
            V2 mainVector = mp2 - mp1;
            foreach(V2 point in points)
            {
                V2 vector = point - mp1;
                BigInteger prodVec = vector ^ mainVector;
                if (prodVec < 0)
                    res.Add(point);
            }
            return res;
        }

        /* Trouve le point le plus proche du vecteur */
        static private V2 findFurthestPoint(V2 mp1, V2 mp2, List<V2> points)
        {
            V2 mainVector = mp2 - mp1;
            V2 furthestPoint = points[0];
            double maxDistance = distanceFromMainVector(mainVector, furthestPoint);

            foreach (V2 point in points)
            {
                double distance = distanceFromMainVector(mainVector, point);
                if(distance > maxDistance)
                {
                    furthestPoint = point;
                    maxDistance = distance;
                }
            }

            return furthestPoint;
        }

        static private double distanceFromMainVector(V2 mainVector, V2 point)
        {
            // ax + by + c -> V(b; -a)
            double sqrt = Math.Exp( 0.5 * BigInteger.Log(mainVector.y * mainVector.y + mainVector.x * mainVector.x));
            return ((int)BigInteger.Abs(mainVector.y * point.x - mainVector.x * point.y)) / sqrt;
        }
    }
}