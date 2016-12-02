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
        static private int currentIndex;        // Index du prochain point à traiter
        static public LinkedList<V2> convexHull;
        
        static public void start()
        {
            Console.WriteLine("Sart_MelkMan");

            List<V2> points = SetofPoints.LP;
            List<V2> toSortPoints = new List<V2>();         // Utiliser pour le trier les points dans le sens horaire (1 scénario n'est pas ordonné)

            /*for (int i = 0; i < 3; ++i)
                HP.AddLast(points[i]);*/
                
            for (int i = 0; i < 3; ++i)
                toSortPoints.Add(points[i]);

            toSortPoints = toSortPoints.OrderBy(point => point.x).ThenBy(point => point.y).ToList<V2>();

            convexHull = new LinkedList<V2>(toSortPoints);
            convexHull.AddLast(convexHull.First());         // Copie du premier en dernier pour simuler la structure de boucle

            currentIndex = 3;

            Affichage.DrawPolChain(convexHull.ToList<V2>(), Color.Red);
            Affichage.Show();
        }


        static public void Iteration()
        {
            Console.WriteLine("Iteration_MelkMan");

            List<V2> points = SetofPoints.LP;
            if (currentIndex == points.Count)       // Verifie s'il reste des points à traiter
                return;

            int initialSize = convexHull.Count();   // Taille de l'enveloppe avant ajout d'un nouveau point

            LinkedListNode<V2> first = convexHull.First;
            LinkedListNode<V2> last = convexHull.Last;
            V2 leftVector = first.Next.Value - first.Value;     // Vecteur à gauche du dernier point
            V2 rightVector = last.Previous.Value - last.Value;  // Vecteur à droite du dernier point

            while (currentIndex != points.Count)    // On parcours les points de l'essemble LP tant qu'un point n'a pas été ajouté à l'enveloppe
            {
                Console.WriteLine("CurrentIndex_Iteration");

                V2 tempLeftVector = points[currentIndex] - first.Value; // Supposé gauche liste
                V2 tempRightVector = points[currentIndex] - last.Value; // Supposé droite liste

                BigInteger prodVecLeft = tempLeftVector ^ leftVector;
                BigInteger prodVecRight = tempRightVector ^ rightVector;

                if(prodVecLeft < 0 || prodVecRight > 0)
                {
                    while (prodVecLeft < 0)     // Tant que le nouveau point peut être placé à gauche
                    {
                        convexHull.RemoveFirst();
                        first = convexHull.First;
                        leftVector = first.Value - first.Next.Value;
                        tempLeftVector = first.Value - points[currentIndex];
                        prodVecLeft = tempLeftVector ^ leftVector;
                    }
                    convexHull.AddFirst(points[currentIndex]);

                    while(prodVecRight > 0) // Tant que le nouveau point peut être placé à droite
                    {
                        convexHull.RemoveLast();
                        last = convexHull.Last;
                        rightVector = last.Value - last.Previous.Value;
                        tempRightVector = last.Value - points[currentIndex];
                        prodVecRight = tempRightVector ^ rightVector;
                    }
                    convexHull.AddLast(points[currentIndex]);
                    currentIndex++;
                    break;
                }
                currentIndex++;
            }

            /* Draw */
            Affichage.RefreshScreen();
            Affichage.DrawSet(SetofPoints.LP, Color.Blue);
            Affichage.DrawPolChain(convexHull.ToList<V2>(), Color.Red);
            Affichage.Show();
        }
    }
}
