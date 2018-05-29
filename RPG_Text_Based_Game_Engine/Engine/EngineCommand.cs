using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RPG_Text_Based_Game_Engine.Engine {
    class EngineCommand {

        public class RestartGame : Command {
            public RestartGame() {
                Help = "Restarts the game";
                Activators = new List<string>(){
                "restartgame",
                "resetgame",
                "restart"
                };
            }

            public override void Run() {
                Program.engine.Restart();
            }
        }

        public class QuitGame : Command {
            public QuitGame() {
                Help = "Quits the game";
                Activators = new List<String>() {
                "quitgame"
                };
            }

            public override void Run() {
                Program.engine.Quit();
            }
        }

        public class SaveGame : Command {
            public SaveGame() {
                Help = "Saves the game state";
                Activators = new List<String>() {
                "save",
                "savegame"
                };
            }

            public override void Run() {
                Program.engine.Save();
            }
        }

        public class ClearScreen : Command {
            public ClearScreen() {
                Help = "Clears the screen";
                Activators = new List<String>() {
                "clear",
                "cls",
                "clearscreen"
                };
            }

            public override void Run() {
                Program.engine.Clear();
            }
        }

        public class LoadGame : Command {
            public LoadGame() {
                Help = "Loads the last saved game state";
                Activators = new List<String>() {
                "load",
                "loadGame"
                };
            }

            public override void Run() {
                Program.engine.Load();
            }
        }

        public class ListEngineCommands : Command {
            public ListEngineCommands() {
                Help = "List all engine commands";
                Activators = new List<String>(){
                    "ls",
                    "listcommands"
                };
            }

            public override void Run() {
                Program.engine.ListEngineCommands();
            }
        }

        public class Inspect : Command {
            public Inspect() {
                Help = "Inspect a location";
                Activators = new List<String>(){
                    "inspect"
                };
            }

            public override void Run() {
                
            }
        }

        public class PlayGame : Command {
            public PlayGame() {
                Help = "Begins the game";
                Activators = new List<String>(){
                    "play",
                    "playgame"
                };
            }

            public override void Run() {
                Program.engine.PlayGame();
            }
        }


        /*
         public class Example : Command {
            public Example(){
                Help = "This is an example of help";
                Activators = new List<String>(){
                    "ex",
                    "example"
                };
            }

            public override void Run() {
                
            }
        }
         */
    }
}
