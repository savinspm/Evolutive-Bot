using Labyrinth.Kernel;
using System;
using System.Collections.Generic;
using System.Linq;

namespace EvolutiveTank.Creature
{
    public static class ETVenus
    {
        public static List<ETTank> evaluate(List<ETTank> creatures, List<Point> mapSolution)
        {
            List<ETTank> creaturesTemp = creatures;
            List<Point> pathTravelled = new List<Point>();
            for (int i = 0; i < creaturesTemp.Count; i++)
            {
                pathTravelled = creaturesTemp.ElementAt(i).PathTravelled.Intersect(mapSolution).Distinct<Point>().ToList<Point>();
                creaturesTemp.ElementAt(i).PathTravelled = pathTravelled;
            }
            return creaturesTemp;
        }
        public static List<ETTank> order(List<ETTank> creatures)
        {
            List<ETTank> creaturesTemp = creatures;
            creaturesTemp = creaturesTemp.OrderByDescending(o => o.PathTravelled.Count).ToList();
            return creaturesTemp;
        }

        //ESTE SELECT ELIGE SOLO A LOS N/2 MEJORES
         public static List<ETTank> select(List<ETTank> creatures)
         {
             List<ETTank> sublist = creatures.GetRange(0, creatures.Count/2);
             return sublist;
         }
       /* public static List<ETTank> select(List<ETTank> creatures)
        {
            List<ETTank> creaturesTemp = new List<ETTank>(creatures);
            List<ETTank> survivors = new List<ETTank>();
            Random rnd = new Random(DateTime.Now.Millisecond);
            int n = 0;
            ETTank e1, e2;
            while (creaturesTemp.Count > 0)
            {
                n = rnd.Next(0, creaturesTemp.Count);
                e1 = creaturesTemp.ElementAt(n);
                creaturesTemp.RemoveAt(n);

                n = rnd.Next(0, creaturesTemp.Count);
                e2 = creaturesTemp.ElementAt(n);
                creaturesTemp.RemoveAt(n);
                if (e1.PathTravelled.Count > e2.PathTravelled.Count)
                {
                    survivors.Add(e1);
                }
                else
                {
                    survivors.Add(e2);
                }
            }
            return survivors;
        }*/
        public static List<ETTank> crossing(List<ETTank> creatures, Matrix map)
        {
            List<ETTank> newGeneration = new List<ETTank>();
            ETTank e1 = null, e2 = null, off1 = null, off2 = null;
            Random rnd = new Random(DateTime.Now.Millisecond);
            IDictionary<string, int> firstHalf, secondHalf, firstHalf2, secondHalf2;
            int n = 0;
          
            while (creatures.Count>0)
            {
                n = rnd.Next(0, creatures.Count);
                e1 = creatures.ElementAt(n);
                creatures.RemoveAt(n);
                n = rnd.Next(0, creatures.Count);
                e2 = creatures.ElementAt(n);
                creatures.RemoveAt(n);
                n = rnd.Next(0, 256);
               if(e1.InitialPositionMap.Column==e2.InitialPositionMap.Column && e1.InitialPositionMap.Row == e2.InitialPositionMap.Row)
                {
                    off1 = new ETTank(e1.InitialPositionMap);
                    off2 = new ETTank(e1.InitialPositionMap);
                }
                else
                {
                    off1 = new ETTank(map);
                    off2 = new ETTank(map);
                }
              
               
               
                firstHalf = e1.Brain.Situations.Take(n).ToDictionary(kv => kv.Key, kv => kv.Value);
                secondHalf = e1.Brain.Situations.Skip(n).ToDictionary(kv => kv.Key, kv => kv.Value);

                firstHalf2 = e2.Brain.Situations.Take(n).ToDictionary(kv => kv.Key, kv => kv.Value);
                secondHalf2 = e2.Brain.Situations.Skip(n).ToDictionary(kv => kv.Key, kv => kv.Value);

                secondHalf2.ToList().ForEach(x => firstHalf.Add(x.Key, x.Value));

                secondHalf.ToList().ForEach(x => firstHalf2.Add(x.Key, x.Value));

                off1.Brain.Situations = firstHalf;
                off2.Brain.Situations = firstHalf2;
               
                newGeneration.Add(off1);
                newGeneration.Add(off2);
            }


            return newGeneration;
        }

        public static List<ETTank> killWorst(List<ETTank> creatures, int number)
        {
            List<ETTank> sublist = creatures.GetRange(0, number);
            return sublist;
        }
        public static List<ETTank> mutation(List<ETTank> creatures)
        {
            List<ETTank> creaturesTemp = creatures;
            Random rnd = new Random(DateTime.Now.Millisecond);
            int n = 0;
            string key;
            for (int i = 0; i < creaturesTemp.Count; i++)
            {
                n = rnd.Next(0, 100);
                if (n < 5)
                {
                    n = rnd.Next(0, 256);
                    key = creaturesTemp.ElementAt(i).Brain.Situations.ElementAt(n).Key;
                    creaturesTemp.ElementAt(i).Brain.Situations[key] = rnd.Next(0, 4);
                }
            }
            return creaturesTemp;
        }
    }
}
