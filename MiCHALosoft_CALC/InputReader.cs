using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace MiCHALosoft_CALC
{
    class InputReader
    {
        public const string INPUT_IS_VARIABLE = "input_variable",
                            INPUT_IS_EQUATION = "equation";
        private string input = "";
        private bool error = false;

        public bool ChceckInput(string input)
        {
            this.input = input;


            return false;
        }

        public string ReplaceString(string input)
        {
            if (input == "inf")
                return "∞";


            return "";
        }
    }
}
