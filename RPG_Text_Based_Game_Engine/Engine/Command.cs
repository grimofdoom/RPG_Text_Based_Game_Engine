using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RPG_Text_Based_Game_Engine.Engine {
    //This class is used for creating commands
    public abstract class Command {

        //Whether or not the command is allowed to run automatically
        //[Run()] can still manually be called
        private bool active = true;
        //Whether or not the command can be used multiple times when automatically called
        public bool runOnce = false;

        //How many times the command has been ran
        public int timesRan = 0;

        //Constructor
        public Command() { } 

        //List of words to call this command
        public List<String> Activators;

        //Information about the command
        public String Help;

        //Main method that hosts what the command will do
        public abstract void Run();

        //add/remove activators for making commands work
        public void AddActivator(String activator) {
            Activators.Add(activator);
        }
        public void RemoveActivator(String activator) {
            Activators.Remove(activator);
        }

        //whether or not it is active
        public bool IsActive()
        {
            return active;
        }
        //Change the state of active
        public void Activate() {
            active = true;
        }
        public void Inactivate() {
            active = false;
        }
        public void SwitchActive() {
            active = !active;
        }
    }
}
