using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RPG_Text_Based_Game_Engine.Engine {
    public class Location {
        public String name;
        //These are activities that player choses to perform
        public List<List<List<Command>>> spots = new List<List<List<Command>>>();
        //These are activities that can happen when a player enters a spot.
        //Run is automatically ran, so the first statement should contain a validation for whether it should run
        public List<List<List<Command>>> spotEnter = new List<List<List<Command>>>();
        //These are activities that can happen when a palyer exits a spot
        //Run is automatically ran, so the first statement should contain a validation for whether it should run
        public List<List<List<Command>>> spotExit = new List<List<List<Command>>>();

        //Add an action to a spot
        public void AddSpot(int x, int y, Command z) {
            spots[x][y].Add(z);
        }

        //Grab a list of all the possible player interactive commands
        public List<Command> GrabSpot(int x, int y) {
            List<Command> tempList = spots[x][y];
            return tempList;
        }

        //Inspect a spot for all available actions
        public List<Command> Inspect (int x, int y, bool list) {
            List<Command> inspection = spots[x][y];
            if (list) {
                foreach (Command command in inspection) {
                    Console.WriteLine("[" + x + "]" + "[" + y + "]" + "  " + command.Help);
                }
            }
            return inspection;
        }

        //Only run this whenever a player enters a spot
        public void OnEnter(int x, int y){
            List<Command> enter = spotEnter[x][y];
            foreach (Command command in enter) {
                if (command.IsActive()){// check if active for automatic run
                    command.Run();
                    command.timesRan += 1;
                    if (command.runOnce) {//if it can only run once, disable after run
                        command.Inactivate();
                    }
                }
            }
        }

        //Only run this whenever player leaves a spot. 
        public void OnExit(int x, int y) {
            List<Command> exit = spotExit[x][y];
            foreach (Command command in exit) {
                if (command.IsActive()) {// check if active for automatic run
                    command.Run();
                    command.timesRan += 1;
                    if (command.runOnce) {//if it can only run once, disable after run
                        command.Inactivate();
                    }
                }
            }
        }
    }
}
