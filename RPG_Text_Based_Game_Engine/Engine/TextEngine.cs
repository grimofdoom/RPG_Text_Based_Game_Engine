using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace RPG_Text_Based_Game_Engine.Engine {
    class TextEngine {
        //TextEngine main Variables, states of game
        private bool QuitGame;
        private bool RestartGame;
        private bool playGame;
        static int LineSpacer = 6;
        public GameInstance game;

        //Generic character information
        int spawnX, spawnY = 0;
        int locX, locY = 0;
        string mapName;

        //These commands are forced into all game screens
        List<Command> engineCommands = new List<Command>() {
            new EngineCommand.ClearScreen(),
            new EngineCommand.LoadGame(),
            new EngineCommand.QuitGame(),
            new EngineCommand.RestartGame(),
            new EngineCommand.SaveGame(),
            new EngineCommand.ListEngineCommands()
        };

        public TextEngine() {
            QuitGame = false;
            RestartGame = false;
            playGame = false;
        }

        // =================================== Game Loop ===================================
        //Game Loop = Load -> Copyrights -> Setup -> Intro -> [GAMING] -> Close -> Save -> close/restart game
        //[GAMING] = Main Menu -> Game Play
        public void Play() {
            while (!QuitGame) {
                //Reset [RestartGame] to false, so if game restarts, it can re-run normally
                RestartGame = false;
                playGame = false;
                Credits();
                //Load game
                Load();
                //Game Instance startup
                game.Copyrights();
                game.Setup();
                Thread.Sleep(400);
                QuickSpacers();
                game.Intro();
                //This is the actual main game loop
                while (!RestartGame) {
                    //Main/home menu
                    if (!playGame) {
                        UserInput input = new UserInput(" ");
                        SpaceLines(2);
                        ListEngineCommands();
                        SpaceLines(2);
                        input.ForcedInput("Please enter what you want to do: ");
                        //If an [engineCommand] is not ran, do a check across found local commands
                        if (!CheckEngineCommand(input.Input())) {

                        }
                    } else {
                        //Actual Gameplay Exists HERE!!!!FINALLY!
                        Console.WriteLine("Game area does not exist yet!");
                    }
                }
                //Game is closing in this section. If [QuitGame] is false, then the game will go back to main menu
                game.Close();
                //ONLY save if [enableAutoSave] is true/enabled
                if (game.enableAutoSave) {
                    Save();
                }
            }
        }

        // =================================== Main Functions ===================================
        //Restart the game, same as going back to main menu. This is a safe method with saving, if enabled.
        public void Restart() {
            Console.Clear();
            RestartGame = true;
        }
        //Fully quits the game, should encorporate an auto-save like function into here with option to enable/disable
        public void Quit() {
            RestartGame = true;
            QuitGame = true;
        }
        //Plays the intro
        public void Credits() {
            Console.WriteLine("Welcome to this Text Based RPG Game Engine [version:20/5/18.001]");
            Console.WriteLine("Copyright 2018 Timothy Leitzke");
        }
        //Quickly create lines of space
        public void SpaceLines(int linesToSpace) {
            for (int i = 0; i < linesToSpace; i++) {
                Console.WriteLine(" ");
            }
        }
        public void QuickSpacers() {
            SpaceLines(LineSpacer);
        }
        //Save game state
        public void Save() {
            string dir = @"c:\temp";
            string serializationFile = Path.Combine(dir, "TextGameState.bin");
            using (Stream stream = File.Open(serializationFile, FileMode.Create)) {
                var bformatter = new System.Runtime.Serialization.Formatters.Binary.BinaryFormatter();
                bformatter.Serialize(stream, game);
            }
        }
        //Load game state
        public void Load() {
            string dir = @"c:\temp";
            string serializationFile = Path.Combine(dir, "TextGameState.bin");
            if (File.Exists(serializationFile)) {
                Game.MainGame tempGame;
                using (Stream stream = File.Open(serializationFile, FileMode.Open)) {
                    var bformatter = new System.Runtime.Serialization.Formatters.Binary.BinaryFormatter();
                    tempGame = (Game.MainGame)bformatter.Deserialize(stream);
                }
                game = tempGame;
            } else {
                game = new Game.MainGame();
            }
        }
        //Clear screen, adds more things to make it look more full
        public void Clear() {
            Console.Clear();
        }
        //Move Player Position
        public void moveChar(String direction, int spaces)
        {
            int x, y = 0;
            if (direction == "up"){
                y = 1 * spaces;
            } else if (direction == "down"){
                y = -1 * spaces;
            }
            if (direction == "left"){
                x = -1 * spaces;
            } else if (direction == "right"){
                x = 1 * spaces;
            }
        }
        //Check and run if [input] == [engineCommand]
        public bool CheckEngineCommand(String _input) {
            foreach (Command action in engineCommands) {
                foreach (String activator in action.Activators) {
                    if (_input == activator) {
                        action.Run();
                        return true;
                    }
                }
            }
            return false;
        }

        public void ListEngineCommands() {
            foreach (Command action in engineCommands) {
                Console.WriteLine(action.Activators[0] + " : " + action.Help);
            }
        }
    }
}