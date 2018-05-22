using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RPG_Text_Based_Game_Engine.Engine {
    class UserInput {
        //Unformatted user input
        private String input;
        //Formatted user input, easier to grab specific word- always in lowercase
        private String[] fInput;

        //Constructors
        public UserInput() {
            GetInput();
        }
        public UserInput(String _input) {
            input = _input;
            fInput = FormatText(input);
        }

        //Functions
        public void GetInput() {
            input = Console.ReadLine();
            fInput = FormatText(input);
        }
        public void ForcedInput(String displayText) {
            bool answered = false;
            while (!answered) {
                Console.Write(displayText);
                String _input = Console.ReadLine();
                if (_input.Length > 0) {
                    input = _input;
                    fInput = FormatText(input);
                    answered = true;
                }
            }
        }
        public String[] FormatText(String textToFormat) {
            return textToFormat.ToLower().Split(' ');
        }

        //Read variables
        public String Input() {
            return input;
        }
        public String FInput(int wordLocation) {
            return fInput[wordLocation];
        }
        //Get entire [fInput]
        public String[] FInputFull() {
            return fInput;
        }
    }
}
