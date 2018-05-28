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
        public int worldLocation;//Which location is player in, in world?

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
    }
}
