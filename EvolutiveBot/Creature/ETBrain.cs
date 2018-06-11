using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace EvolutiveTank.Creature
{
    public class ETBrain
    {
        public IDictionary<string, int> Situations { get; set; }
        public ETBrain()
        {
            this.Situations = new Dictionary<string, int>();

        }

        public void loadSituations()
        {
            int rest = 0, div = 0;
            string situation = "";
            Random rnd = new Random(DateTime.Now.Millisecond);
            Thread.Sleep(10);
            for (int i = 0; i < 256; i++)
            {
                situation = "[";
                rest = i;
                div = 128;
                for (int j = 0; j < 8; j++)
                {
                    situation += rest / div;
                    rest = rest % div;
                    div = div / 2;
                    if (j != 7)
                    {
                        situation += ",";
                    }
                }
                situation += "]";
                this.Situations.Add(situation, rnd.Next(0, 4));
            }
        }
    }
}
