using EvolutiveTank.Creature;
using Labyrinth.Kernel;
using Labyrinth.UI;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EvolutiveTank.Management
{
    public class ETGod
    {
        public Matrix map { get; set; }
        public ETGod(Matrix map)
        {
            this.map = map;
        }


        public void evolution(int numberOfCreatures, int numberOfIterations, int numberOfEvolutions, frmMain window, bool positionrandom, Point p)
        {

            List<ETTank> creatures = new List<ETTank>();
            int n = 0;
            ETTank creature;
            // INICIALIZACION
            if (!positionrandom)
            {
                while (n < numberOfCreatures)
                {
                    creature = new ETTank(p);
                    creatures.Add(creature);
                    n++;
                }
            }
            else
            {
                while (n < numberOfCreatures)
                {
                    creature = new ETTank(window.f_BoardBox.Matrix);
                    creatures.Add(creature);
                    n++;
                }
            }

            // EVLUACION
            n = 0;
            while (n < numberOfIterations)
            {
                Parallel.For(0, creatures.Count, i =>
                {
                    string radar = creatures.ElementAt(i).scanner(this.map.m_Buffer);
                    int direction = creatures.ElementAt(i).Brain.Situations[radar];
                    creatures.ElementAt(i).move(direction, this.map);
                });
                n++;
            }

            creatures = ETVenus.evaluate(creatures, window.f_BoardBox.tileSolutions);
            creatures = ETVenus.order(creatures);
            n = 0;
            int n2 = 0;
            List<ETTank> creaturesSelected, offsprings;

            while (n < numberOfEvolutions)
            {
                if (creatures.ElementAt(0).PathTravelled.Count == window.f_BoardBox.tileSolutions.Count)
                {
                    break;
                }
                //RESET CREATURES
                Parallel.For(0, creatures.Count, i =>
                {
                    creatures.ElementAt(i).IndexMapPosition = creatures.ElementAt(i).InitialPositionMap;
                });
                creaturesSelected = ETVenus.select(creatures);
                offsprings = ETVenus.crossing(creaturesSelected, this.map);
                creatures.AddRange(offsprings);
                creatures = ETVenus.mutation(creatures);
                while (n2 < numberOfIterations)
                {
                    Parallel.For(0, creatures.Count, i =>
                     {
                         string radar = creatures.ElementAt(i).scanner(this.map.m_Buffer);
                         int direction = creatures.ElementAt(i).Brain.Situations[radar];
                         creatures.ElementAt(i).move(direction, this.map);
                     });
                    n2++;
                }
                n2 = 0;
                creatures = ETVenus.evaluate(creatures, window.f_BoardBox.tileSolutions);
                creatures = ETVenus.order(creatures);
                creatures = ETVenus.killWorst(creatures, (creatures.Count / 3) * 2);
                window.addBestCreatureEvolution(creatures.ElementAt(0), n);

                n++;
                if (n % 100 == 0)
                {
                    System.IO.StreamWriter fileTemp = new System.IO.StreamWriter(@"C:\maps\evoluciones\solucion" + n + ".txt");
                    fileTemp.WriteLine("Distancia recorrida: " + creatures.ElementAt(0).PathTravelled.Count);
                    for (int i = 0; i < 256; i++)
                    {
                        fileTemp.WriteLine(creatures.ElementAt(0).Brain.Situations.ElementAt(i));
                    }
                    fileTemp.Close();
                }
                window.updateChart(creatures.ElementAt(0).PathTravelled.Count);
                Console.WriteLine("Evolucion: " + n);
            }

            //ESCRIBIR LA SOLUCION
            System.IO.StreamWriter file = new System.IO.StreamWriter(@"C:\maps\solucion.txt");
            file.WriteLine("Mejores 10 resultados");
            for (int i = 0; i < 10; i++)
            {
                file.WriteLine("Distancia recorrida:" + creatures.ElementAt(i).PathTravelled.Count);
            }

            file.Close();
            System.IO.StreamWriter file2 = new System.IO.StreamWriter(@"C:\maps\solucionMejor.txt");
            for (int i = 0; i < 256; i++)
            {
                file2.WriteLine(creatures.ElementAt(0).Brain.Situations.ElementAt(i));
            }

            file2.Close();

        }


    }
}
