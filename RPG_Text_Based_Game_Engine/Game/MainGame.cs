using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace RPG_Text_Based_Game_Engine.Game {

    [Serializable]
    public class MainGame : Engine.GameInstance {

        //Set overall game and engine settings
        public override void Setup() {
            gameName = "newGame";
            gameVersion = "20.05.18.01";
            enableAutoSave = true;
        }

        //Introduction text/animation played before the main menu shows up
        public override void Intro() {
            Console.WriteLine(gameName);
            Console.WriteLine("Version: " + gameVersion);
        }

        //Last bit of code ran before the game instance closes
        public override void Close() {
            Console.WriteLine("Closing Game");
            Thread.Sleep(500);
        }

        //Game copyrights, creator places other's and their copyright information here
        public override void Copyrights() {
            Console.WriteLine("This test game was developed by GrimOfDoom");
        }
    }
}