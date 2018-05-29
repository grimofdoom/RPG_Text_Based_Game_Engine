using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RPG_Text_Based_Game_Engine.Engine {
    [Serializable]
    public abstract class GameInstance {

        //Game information
        public String gameName;
        public String gameVersion;

        //General Game Settings
        public bool enableAutoSave = true;

        //Logs
        private List<String> log = new List<string>();
        private int maxLogLength = 128;

        //List of all in-game locations
        public List<Location> world = new List<Location>();
        //Player location
        public int playerX, playerY;
        public String worldLocation;//Which location is player in, in world?

        //Instantiation process
        public GameInstance() {

        }

        //==================== Main Methods =============================
        //Set settings and startup variables here, such as game name
        public abstract void Setup();

        //Set the intro print here
        public abstract void Intro();

        //Set the closing properies and actions here
        public abstract void Close();

        //Set copyrights here
        public abstract void Copyrights();

        //Log functions
        public void AddLog(String newLog) {
            //Only add [newLog] to [log] if it was not last used to avoid duplicates
            if (log.Last() != newLog) {
                log.Add(newLog);
            }
            //If the size of [log] is over [maxLogLength], remove the first
            if (log.Count >= maxLogLength) {
                log.RemoveAt(0);
            }
        }
        public void ReadLog(int readLog) {
            Console.WriteLine(log[readLog]);
        }
        //Adjusting the maximum log length will change how big/small the game's save state will be
        public void ChangeMaxLog(int newMaxLog) {
            maxLogLength = newMaxLog;
            //Only adjust the [log] if there are more in it now than what the new max is
            if (log.Count < newMaxLog) {
                List<String> tempLog = new List<String>();
                for (int i = 0; i < newMaxLog; i++) {
                    tempLog.Add(log[log.Count - i]);
                }
                log = tempLog;
            }
        }
        //Game Message is the game response, which also ties into logs
        public void Message(String message) {
            Console.WriteLine(message);
            //Attempt to add game message to [log], is not added if it is duplicate of last message
            AddLog(message);
        }

        //========================================Player Movement===========================================
        //Move Player Position
        public void MoveChar(String direction, int spaces) {
            int currentX = Program.engine.game.playerX;
            int currentY = Program.engine.game.playerY;
            int x = 0;
            int y = 0;
            if (direction == "up") {
                y = 1 * spaces;
            } else if (direction == "down") {
                y = -1 * spaces;
            }
            if (direction == "left") {
                x = -1 * spaces;
            } else if (direction == "right") {
                x = 1 * spaces;
            }
            //Continue if new location is NOT a wall
            if (!FindLocation(worldLocation).walls[currentX + x][currentY + y]) {
                int newX = currentX + x;
                int newY = currentY + y;
                //Perform [OnExit] of current spot
                FindLocation(worldLocation).OnExit(newX, newY);
                //Check if the player has teleported/moved to a new location
                if (Program.engine.game.playerX == currentX && Program.engine.game.playerY == currentY) {
                    //If player did not teleport
                    Program.engine.game.playerX = newX;
                    Program.engine.game.playerY = newY;
                }
                //Perform [OnEnter] of new spot
                //Do NOT use [newX] or [newY] in case the player was teleported, and relook for current location if player teleported
                FindLocation(worldLocation).OnEnter(Program.engine.game.playerX, Program.engine.game.playerY);
            }
        }
        //Find [location] in [world] using specific [name]. No location should share names, only first of same name will be grabbed
        //This needs to be fixed later to store current [Location] as an int locatation of [world] list
        public Location FindLocation(String locationName) {
            Location location = new Location();
            foreach (Location loc in world) {
                if (loc.name == locationName) {
                    return loc;
                }
            }
            return location;
        }
    }
}
