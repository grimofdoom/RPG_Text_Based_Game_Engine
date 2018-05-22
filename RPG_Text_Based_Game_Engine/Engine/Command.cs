using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RPG_Text_Based_Game_Engine.Engine {
    //This class is used for creating commands
    public abstract class Command {
        public Command() { } //Constructor

        public List<String> Activators;//List of words to call this command

        public String Help;//Information about the command

        //Main method that hosts what the command will do
        public abstract void Run();

        public void AddActivator(String activator) {
            Activators.Add(activator);
        }

        public void RemoveActivator(String activator) {
            Activators.Remove(activator);
        }
    }
}
