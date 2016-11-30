using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Drawing;
using System.Numerics;

namespace Projet_IMA
{
    class MelkMan
    {
        static private int currentIndex;
        static public LinkedList<V2> HP;

        private class CompV2 : IComparer<V2>
        {
            public int Compare(V2 p1, V2 p2)
            {
                if (p1.x < p2.x)
                    return 1;
                else if (p1.y < p2.y)
                    return 1;
                return 0;
            }
        }

        static public void start()
        {
            Console.WriteLine("Sart_MelkMan");
            List<V2> points = new List<V2>(SetofPoints.LP);
            List<V2> tempInitPoints = new List<V2>();

            /*for (int i = 0; i < 3; ++i)
                HP.AddLast(points[i]);*/
                
            for (int i = 0; i < 3; ++i)
                tempInitPoints.Add(points[i]);

            tempInitPoints = tempInitPoints.OrderBy(point => point.x).ThenBy(point => point.y).ToList<V2>();

            HP = new LinkedList<V2>(tempInitPoints);
            HP.AddLast(HP.First());
            currentIndex = 3;
            Affichage.DrawPolChain(HP.ToList<V2>(), Color.Red);
            Affichage.Show();
        }


        static public void Iteration()
        {
            Console.WriteLine("Iteration_MelkMan");
            List<V2> points = SetofPoints.LP;
            if (currentIndex == points.Count)
                return;

            int initSize = HP.Count();

            LinkedListNode<V2> first = HP.First;
            LinkedListNode<V2> last = HP.Last;
            V2 leftVector = first.Value - first.Next.Value;  // Debut liste
            V2 rightVector = last.Value - last.Previous.Value; // Fin liste

            while (currentIndex != points.Count)
            {
                Console.WriteLine("CurrentIndex_Iteration");

                V2 tempLeftVector = first.Value - points[currentIndex]; // Supposé gauche liste
                V2 tempRightVector = last.Value - points[currentIndex]; // Supposé droite liste
                BigInteger prodVecLeft = tempLeftVector ^ leftVector;
                BigInteger prodVecRight = tempRightVector ^ rightVector;

                if(prodVecLeft < 0 || prodVecRight > 0)
                {
                    while (prodVecLeft < 0 /*&& HP.Count > 3*/) // Boucle non utile si 3 éléments dans la liste
                    {
                        HP.RemoveFirst();
                        first = HP.First;
                        leftVector = first.Value - first.Next.Value;
                        tempLeftVector = first.Value - points[currentIndex];
                        prodVecLeft = tempLeftVector ^ leftVector;
                    }
                    HP.AddFirst(points[currentIndex]);

                    while(prodVecRight > 0/* && HP.Count > 3*/)
                    {
                        HP.RemoveLast();
                        last = HP.Last;
                        rightVector = last.Value - last.Previous.Value;
                        tempRightVector = last.Value - points[currentIndex];
                        prodVecRight = tempRightVector ^ rightVector;
                    }
                    HP.AddLast(points[currentIndex]);
                    break;
                }
                currentIndex++;
            }

            Affichage.RefreshScreen();
            Affichage.DrawSet(SetofPoints.LP, Color.Blue);
            Affichage.DrawPolChain(SetofPoints.LP, Color.Green);
            Affichage.DrawPolChain(HP.ToList<V2>(), Color.Red);
            Affichage.Show();
        }
    }
}
