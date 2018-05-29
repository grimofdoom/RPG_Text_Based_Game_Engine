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
        static readonly int LineSpacer = 6;
        public GameInstance game;

        public UserInput input;

        //These commands are forced into all game screens
        List<Command> engineMainMenuCommands = new List<Command>() {
            new EngineCommand.ClearScreen(),
            new EngineCommand.LoadGame(),
            new EngineCommand.QuitGame(),
            new EngineCommand.RestartGame(),
            new EngineCommand.SaveGame(),
            new EngineCommand.ListEngineCommands(),
            new EngineCommand.PlayGame()
        };

        //These commands are in normal game play
        List<Command> engineGamePlayCommands = new List<Command>() {
            new EngineCommand.QuitGame(),
            new EngineCommand.SaveGame(),
            new EngineCommand.ClearScreen(),
            new EngineCommand.Inspect()
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
                        input = new UserInput(" ");
                        SpaceLines(2);
                        ListEngineCommands();
                        SpaceLines(2);
                        input.ForcedInput("Please enter what you want to do: ");
                        //If an [engineCommand] is not ran, do a check across found local commands
                        if (!CheckEngineMainMenuCommand(input.Input())) {

                        }
                    } else {
                        input.ForcedInput("#");
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
        //Plays the game
        public void PlayGame() {
            Console.Clear();
            playGame = true;
        }
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
        //Check and run if [input] == [engineCommand]
        public bool CheckEngineMainMenuCommand(String _input) {
            foreach (Command action in engineMainMenuCommands) {
                foreach (String activator in action.Activators) {
                    if (_input == activator) {
                        action.Run();
                        return true;
                    }
                }
            }
            return false;
        }
        //Lists all engine commands
        public void ListEngineCommands() {
            foreach (Command action in engineMainMenuCommands) {
                Console.WriteLine(action.Activators[0] + " : " + action.Help);
            }
        }
    }
}