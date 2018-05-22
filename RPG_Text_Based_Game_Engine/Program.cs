using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RPG_Text_Based_Game_Engine {
    class Program {

        public static Engine.TextEngine engine;

        static void Main(string[] args) {
            //Creates the engine
            engine = new Engine.TextEngine();
            
            //Runs the engine
            engine.Play();
        }


    }
}
