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

        static public void start()
        {
            Console.WriteLine("Sart_MelkMan");
            List<V2> points = new List<V2>(SetofPoints.LP);
            HP = new LinkedList<V2>();

            for (int i = 0; i < 3; ++i)
                HP.AddLast(points[i]);

            HP.AddFirst(points[2]);
            currentIndex = 3;
        }


        static public void Iteration()
        {
            Console.WriteLine("Iteration_MelkMan");
            List<V2> points = new List<V2>(SetofPoints.LP);
            int initSize = HP.Count();

            LinkedListNode<V2> first = HP.First;
            LinkedListNode<V2> last = HP.Last;

            V2 leftVector = first.Value - first.Next.Value;  // Debut liste
            V2 rightVector = last.Value - last.Previous.Value; // Fin liste

            while (currentIndex != points.Count && initSize == HP.Count())
            {
                Console.WriteLine("CurrentIndex_Iteration");

                V2 tempLeftVector = first.Value - points[currentIndex]; // Supposé gauche liste
                V2 tempRightVector = last.Value - points[currentIndex]; // Supposé droite liste

                BigInteger prodVecLeft = tempLeftVector ^ leftVector;
                BigInteger prodVecRight = tempRightVector ^ rightVector;

                if(prodVecLeft < 0 || prodVecRight > 0)
                {
                    while(prodVecLeft < 0)
                    {
                        HP.RemoveFirst();
                        first = HP.First;
                        leftVector = first.Value - first.Next.Value;
                        tempLeftVector = first.Value - points[currentIndex];
                        prodVecLeft = tempLeftVector ^ leftVector;
                    }
                    HP.AddFirst(points[currentIndex]);

                    while (prodVecRight > 0)
                    {
                        HP.RemoveLast();
                        last = HP.Last;
                        rightVector = last.Value - last.Previous.Value;
                        tempRightVector = last.Value - points[currentIndex];
                        prodVecRight = tempRightVector ^ rightVector;
                    }
                    HP.AddLast(points[currentIndex]);
                }

                currentIndex++;
            }

            Affichage.DrawPolChain(HP.ToList<V2>(), Color.Red);
            Affichage.Show();
        }
    }
}
