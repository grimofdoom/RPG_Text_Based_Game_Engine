using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RPG_Text_Based_Game_Engine.Engine {
    public class Location {
        public String name;
        public List<List<List<Command>>> spots = new List<List<List<Command>>>(); 
        
        public void AddSpot(int x, int y, Command z) {
            spots[x][y].Add(z);
        }

        public List<Command> GrabSpot(int x, int y) {
            List<Command> tempList = new List<Command>();
            tempList = spots[x][y];
            return tempList;
        }
    }
}
