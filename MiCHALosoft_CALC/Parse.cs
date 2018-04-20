using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace MiCHALosoft_CALC
{
    class Parse
    {
        private Variable [] ListVars;
        private string input;

    }

    struct Variable
    {
        private string name;
        private double value;
        
        public string Name
        {
            get { return name; }
            set { this.name = value; }
        }
        public double Value
        {
            get { return this.value; }
            set { this.value = value; }
        }
    }
}
