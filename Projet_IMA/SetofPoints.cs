using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Drawing;
using System.Drawing.Imaging;
using System.Numerics;


namespace Projet_IMA
{
    static class SetofPoints
    {
        static public List<V2> LP = new List<V2>();
        
       

        public static void RandomTest()
        {
           // creation d'une liste de points
          
           Scenario.CreateRandomPoint (LP,30);
           
           // affichage 
           Affichage.RefreshScreen();
           Affichage.DrawSet(LP,Color.Blue);
           Affichage.Show();
        }

        public static void ScenarioTest()
        {

            Scenario.GetScenario(LP);

            // affichage 
            Affichage.RefreshScreen();
            Affichage.DrawSet(LP, Color.Blue);
            Affichage.DrawPolChain(LP, Color.Green);
            Affichage.Show();
        }
    }
}
