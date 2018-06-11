
using Labyrinth.Kernel;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EvolutiveTank.Creature
{
    public class ETTank
    {

        public ETBrain Brain
        {
            get; set;
        }
        public Point IndexMapPosition
        {
            get; set;
        }
        public int Step
        {
            get; set;
        }
        public Point InitialPositionMap { get; set; }
        public List<Point> PathTravelled { get; set; }

        public ETTank(Point position)
        {
            this.Brain = new ETBrain();
            this.Brain.loadSituations();

            this.IndexMapPosition = position;
            this.InitialPositionMap = position;
            this.PathTravelled = new List<Point>();
            this.PathTravelled.Add(position);
            this.Step = 0;

        }

        public ETTank(Point position, string brain)
        {
            this.Brain = new ETBrain();
           
            this.IndexMapPosition = position;
            this.InitialPositionMap = position;
            this.PathTravelled = new List<Point>();
            this.PathTravelled.Add(position);
            this.Step = 0;
            this.loadBrain(brain);

        }
        public ETTank(Matrix map)
        {
            this.Brain = new ETBrain();
            Point p = generatePoint(map);
            this.IndexMapPosition = p;
            this.InitialPositionMap = p;
            this.PathTravelled = new List<Point>();
            this.PathTravelled.Add(p);
            this.Step = 0;
            this.Brain.loadSituations();

        }
        public Point generatePoint(Matrix map)
        {

            Random rnd = new Random(DateTime.Now.Millisecond);
            Point p = new Point(0, 0);
            while (map.m_Buffer[p.Row, p.Column] == CellType.Fix)
            {
                p = new Point(Convert.ToByte(rnd.Next(0, map.Size.Row)), Convert.ToByte(rnd.Next(0, map.Size.Column)));
            }
            return p;
        }
        public ETTank(string brain, Matrix map)
        {
            this.Brain = new ETBrain();
            Point p = generatePoint(map);
            this.IndexMapPosition = p;
            this.InitialPositionMap = p;
            this.PathTravelled = new List<Point>();
            this.PathTravelled.Add(p);
            this.Step = 0;
            this.loadBrain(brain);
        }

        public string scanner(CellType[,] map)
        {
            string key = "[";
            key += map[this.IndexMapPosition.Row-1, this.IndexMapPosition.Column-1] == CellType.Fix ? "1," : "0,";
            key += map[this.IndexMapPosition.Row - 1, this.IndexMapPosition.Column] == CellType.Fix ? "1," : "0,";
            key += map[this.IndexMapPosition.Row - 1, this.IndexMapPosition.Column + 1] == CellType.Fix ? "1," : "0,";
            key += map[this.IndexMapPosition.Row, this.IndexMapPosition.Column - 1] == CellType.Fix ? "1," : "0,";
            key += map[this.IndexMapPosition.Row, this.IndexMapPosition.Column + 1] == CellType.Fix ? "1," : "0,";
            key += map[this.IndexMapPosition.Row + 1, this.IndexMapPosition.Column - 1] == CellType.Fix ? "1," : "0,";
            key += map[this.IndexMapPosition.Row + 1, this.IndexMapPosition.Column] == CellType.Fix ? "1," : "0,";
            key += map[this.IndexMapPosition.Row + 1, this.IndexMapPosition.Column + 1] == CellType.Fix ? "1]" : "0]";

            return key;
        }

        public Point move(int direction, Matrix map)
        {
            Point newMapPosition = this.IndexMapPosition;
            if (direction == 0)//UP
            {
                newMapPosition = new Point(Convert.ToByte(this.IndexMapPosition.Row - 1), this.IndexMapPosition.Column);
            }else if(direction == 1)//RIGHT
            {
                newMapPosition = new Point(this.IndexMapPosition.Row, Convert.ToByte(this.IndexMapPosition.Column +1));
            }
            else if(direction == 2)//DOWN
            {
                newMapPosition = new Point(Convert.ToByte(this.IndexMapPosition.Row + 1), this.IndexMapPosition.Column);
            }
            else if(direction == 3)//LEFT
            {
                newMapPosition = new Point(this.IndexMapPosition.Row, Convert.ToByte(this.IndexMapPosition.Column - 1));
            }
            if(posibleMove(newMapPosition, map)){
                this.IndexMapPosition = newMapPosition;
                this.Step += 1;
                this.PathTravelled.Add(newMapPosition);
                return newMapPosition;
            }
            return this.IndexMapPosition;
        }
        public static bool posibleMove(Point newMapPosition, Matrix map)
        {
            if ((newMapPosition.Row < 0) || (newMapPosition.Column < 0))
                return false;
            if ((newMapPosition.Row >map.Size.Row) || (newMapPosition.Column > map.Size.Column))
                return false;
            if (map.m_Buffer[newMapPosition.Row,newMapPosition.Column]==CellType.Fix)
            {
                return false;
            }
            return true;
        }

        public void loadBrain(string saveCreature)
        {
            char[] firstDelimiter = { '-' };
            string[] words = saveCreature.Split(firstDelimiter);

            //
            char[] secondDelimiter = { ';', };

            string[] brain = words[3].Split(secondDelimiter);
            string key;
            int value;
            this.Brain.Situations.Clear();
            for (int i = 0; i < brain.Length - 1; i++)
            {
                key = brain[i].Substring(1,17);
                value = Convert.ToInt32(brain[i].Substring(20, 1));
                this.Brain.Situations.Add(key, value);
            }
            
        }
    }
}
