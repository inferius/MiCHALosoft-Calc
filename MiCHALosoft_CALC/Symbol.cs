using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;


namespace MiCHALosoft_CALC
{
    class Symbol
    {
        // Universalni konstanty navratovych hodnot
        // Universal constants the return values
        const int NUMBER = 0;
        const int COMPLEX_NUMBER = 1;
        const int MATRIX = 2;
        const int POINT = 3;
        const int SUMA = 4;
        const int EQUAL = 5;
        const int UNDEFINE = 6;

        // Promenne
        // Variable
        private string Value;
        private string Name;
        private int Type;

        private string Warning;
        private string Error;

        public Symbol(string value, string name_var)
        {
            this.Type = DetectType(value);
            this.Value = value;
            this.Name = name_var;

            if (this.Type == UNDEFINE)
                this.Warning = "This value is not defined. Probably is not right working!";

        }

        private int DetectType(string value)
        {

            return UNDEFINE;
        }
    }

}
