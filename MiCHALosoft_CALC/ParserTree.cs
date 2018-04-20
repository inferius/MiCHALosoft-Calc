using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;

namespace MiCHALosoft_CALC
{
    public class ParserTree
    {
        class Function
        {
            
            public static int GetCountArgument(string function)
            {
                switch (function)
                {
                    default: return 1;                    
                }

            }
        }

        class TreeCalculation
        {
            public static string ExecuteOperation(string op1, string op2, string zn)
            {
                Regex keyWord = new Regex(@"sin|ln|tan|cos|cotg|sinh|cosh|tanh|^|arcsin|arccos");
                if (keyWord.IsMatch(op1))
                {
                    op1 = ExecuteOperation(op1);
                }
                if (keyWord.IsMatch(op2))
                {
                    op2 = ExecuteOperation(op2);
                }

                // osetrit promenne na vstupu
                if (zn == "+")
                    return Math_v2.SumString(op1, op2);
                if (zn == "-")
                    return Math_v2.DesumString(op1, op2);
                if (zn == "/")
                    return Math.DivisionStringBETA(op1, op2);
                if (zn == "*")
                    return Math_v2.ProductString(op1, op2);
                /*if (zn == "^")
                    return Math_v2.*/
                
                    

                return "0";
            }

            public static string ExecuteOperation(string operation)
            {
                Regex keyWord = new Regex(@"e|sin|ln|tan|cos|cotg|sinh|cosh|tanh|^|arcsin|arccos");
                Regex num = new Regex(@"[0-9+\.*0-9*]");

                if (operation.IndexOf('^') > 0)
                {
                    string num1 = operation.Remove(operation.IndexOf("^")),
                        num2 = operation.Remove(0, operation.IndexOf("^")+1);

                    return Math_v2.IntegerExpon(num1, num2);
                }

                switch (keyWord.Match(operation).Value)
                {
                    case "sin":
                        { return Math_v2.Trigonometric.Sin(num.Match(operation).Value); }
                    case "cos":
                        { return Math_v2.Trigonometric.Cos(num.Match(operation).Value); }
                    case "tan":
                        { return Math_v2.Trigonometric.Tan(num.Match(operation).Value); }
                    case "arcsin":
                        { return Math_v2.Trigonometric.Arcsin(num.Match(operation).Value); }
                    case "arccos":
                        { return Math_v2.Trigonometric.Arccos(num.Match(operation).Value); }
                    case "arctan":
                        { return Math_v2.Trigonometric.Arctan(num.Match(operation).Value); }
                    case "ln":
                        { return Math_v2.Logaritmic.ln(num.Match(operation).Value); }
                    case "e":
                        { return Math_v2.Exponential.e(num.Match(operation).Value); }   

                        
                }

                return operation;
            }            
        }

        class Nodes
        {
            /*public Nodes Parent
            {
                get { return this.parent; }
                set { this.parent = value; }
            }
            private Nodes parent;*/

            public string Name // znamenko, hodnota
            {
                get { return this.name; }
                set { this.name = value; }
            }
            private string name;

            public string Value
            {
                get { return this.value; }
                set { this.value = value; }
            }
            private string value;

            public object link_object{get;set;}

            public Nodes left { get; set; }
            public Nodes right { get; set; }

            /// <summary>
            /// Vytváří třídu pomoci odkazu na rodiče, jména a odkazu na objekt
            /// </summary>
            /// <param name="parent">nadřazeny list</param>
            /// <param name="name">jméno proměnné</param>
            /// <param name="link_object">odkaz na objekt</param>
            public Nodes(string name, object link_object)
            {
                //this.parent = parent;
                this.name = name;
                this.link_object = link_object;
                this.value = "";
            }

            public Nodes(Nodes nodes)
            {
                
            }

            public Nodes( string name, string value)
            {
                //this.parent = parent;
                this.name = name;
                this.value = value;
                this.link_object = null;
            }
        }

        public class Tree
        {
            private Nodes root;
            private List<Nodes> nodes = new List<Nodes>();
            // rezervuje klicova slova
            private static string[] KeyWord = { @"sin|ln|tg|e|pi|cos|cotg|arcsin|arcos|log|xor|or|and|nand|nor" };
            private static string[] priorityKeyWord = {"sin|ln|tan|cos|cotg|sinh|cosh|tanh|^" };
            private string result = "";

            public string GetResult()
            {
                return this.result;
            }

            public Tree(string input)
            {
                if (char.IsLetter(input[0]) && input.LastIndexOf('=') > 0)
                    ParseString(input.Insert(0, "0+"));
                else
                    ParseString(input);
            }

            // metody AppendRight ci left pridavaji na konec stromu hodnoty a provedou vyvazeni
            private bool AppendRight(Nodes znamenko, Nodes hodnota)
            {
                if (root == null)
                    return false;
                Nodes buf = root;
                Nodes previous = root;

                while (true)
                {
                    
                    if (buf.Name == "hodnota")
                    {
                        znamenko.right = hodnota;
                        znamenko.left = buf;
                        previous.left = znamenko;

                        return true;
                    }                    
                    else if (buf.right == null)
                    {
                        znamenko.right = hodnota;
                        znamenko.left = buf;
                        previous.left = znamenko;

                        return true;
                    }
                    previous = buf;
                    buf = buf.right;

                }

            }

            private bool AppendLeft(Nodes znamenko, Nodes hodnota)
            {
                if (root == null)
                    return false;
                Nodes buf = root;
                Nodes previous = root;

                while (true)
                {
                    if (buf.Name == "hodnota")
                    {
                        znamenko.left = hodnota;
                        znamenko.right = buf;
                        previous.right = znamenko;

                        return true;
                    }
                    else if (buf.left == null)
                    {
                        znamenko.left = hodnota;
                        znamenko.right = buf;
                        previous.right = znamenko;

                        return true;
                    }

                    previous = buf;
                    buf = buf.left;
                }
            }

            // projede string a vytvori strom            
            private void ParseString(string input)
            {
                if (input[0] == '(' && input[input.Length-1] == ')')
                {
                    input = input.Remove(0, 1);
                    input = input.Remove(input.Length-1, 1);
                }
                // sekce hlavniho rozparsovani
                List<StringBuilder> parsing = new List<StringBuilder>();
                bool pismeno = false;
                bool cislo = false;
                bool key_word = false;
                bool must_znamenko = false;
                int count_argument = 0; 
                int parsing_counter = 0; // udava aktualni zapisovaci polozku v seznamu
                int cout_bracket = 0;   //celkovy pocet zavorek
                int cout_priority = 0; //udava celkovy pocet prioritnich operaci
                int cout_operation = 0; // udava celkovy pocet operaci

                #region Rozparsování na jednotlivé části
                // rozparsovani input na jednotlive sekce
                for (int i = 0; i < input.Length; i++)
                {
                    if (parsing_counter >= parsing.Count)
                        parsing.Add(new StringBuilder());

                    if (char.IsLetter(input[i]))
                    {
                        // pokud narazi na pismeno, nastavi pismeno na true a zapise do aktualniho parsing
                        pismeno = true;
                        parsing[parsing_counter].Append(input[i]);
                    }
                    else if (char.IsWhiteSpace(input[i]))
                    {
                        // pokud narazi na mezeru overi, zda predtim nezapisoval cisla ci pismena
                        // pokud zapisoval cisla ukonci zapis cisel a prejde na dalsi parsing (parsing_counter++)
                        // pokud zapisoval pismena, overi, zda zapsana promenna neni registrovane klicove slovo
                        // pokud ano zjisti pocet argumentu a podle toho pokracuje
                        if (cislo)
                        {
                            if (key_word)
                            {
                                if (--count_argument > 0)
                                {
                                    parsing[parsing_counter].Append(" ");
                                    cislo = false;
                                    continue;
                                }
                                else
                                {
                                    must_znamenko = true;
                                    key_word = false;
                                    cislo = false;
                                    parsing_counter++;
                                    
                                }
                            }
                            else
                            {
                                cislo = false;
                                parsing_counter++;
                                must_znamenko = true;
                            }
                        }
                        else if (pismeno)
                        {
                            pismeno = false;
                            // porovnani s klicovymi slovy
                            /*for (int ii = 0; ii < Tree.KeyWord.Length; ii++)
                            {
                                if (Tree.KeyWord[ii] == parsing[parsing_counter].ToString())
                                {                                   
                                    // vrati pocet argumentu
                                    key_word = true;
                                    count_argument = Function.GetCountArgument(parsing[parsing_counter].ToString());
                                    parsing[parsing_counter].Append(" ");
                                    break;
                                }
                                if (ii == Tree.KeyWord.Length - 1)
                                {
                                    parsing_counter++;
                                    pismeno = false;
                                    must_znamenko = true;
                                    break;
                                }

 
                            }*/
                             Regex kwords = new Regex(Tree.KeyWord[0]);
                            if (kwords.IsMatch(parsing[parsing_counter].ToString()))
                            {
                                // vrati pocet argumentu
                                key_word = true;
                                count_argument = Function.GetCountArgument(parsing[parsing_counter].ToString());
                                parsing[parsing_counter].Append(" ");
                                continue;
                            }
                            else
                            {
                                parsing_counter++;
                                pismeno = false;
                                must_znamenko = true;
                                continue;
                            }
                        }
                    }
                    else if (char.IsNumber(input[i]) || input[i] == '.' || input[i] == '^')
                    {
                        if (key_word)
                        {
                            if (!cislo)
                                count_argument--;
                        }
                        cislo = true;
                        parsing[parsing_counter].Append(input[i]);
                    }
                    else if (input[i] == '+' || input[i] == '-' || input[i] == '/' || input[i] == '*')// || input[i] == '(')
                    {
                        cout_operation++;   // inkrementuje celkovy pocet operaci
                        if (input[i] == '/' || input[i] == '*') // pokud je to jedno z prioritnich znaminek
                            cout_priority++;
                        must_znamenko = false;
                        cislo = false;
                        pismeno = false;
                        if (parsing[parsing_counter].Length > 0)
                        {
                            parsing.Add(new StringBuilder());
                            parsing[++parsing_counter].Append(input[i]);
                            parsing_counter++;
                        }
                        else
                            parsing[parsing_counter++].Append(input[i]);
                    }
                    /*else if (input[i] == ')')
                    {
                        if (parsing[parsing_counter].Length > 0)
                        {
                            parsing.Add(new StringBuilder());
                            parsing[++parsing_counter].Append(input[i]);
                            parsing_counter++;
                        }
                        else
                            parsing[parsing_counter++].Append(input[i]);
                    }*/

                    else if (input[i] == '(')
                    {
                        int zavorka = 0;
                        cout_bracket++;
                        parsing.Add(new StringBuilder());

                        for (int ii = i; ii < input.Length; ii++)
                        {
                            parsing[parsing_counter].Append(input[ii]);

                            if (input[ii] == '(')
                                zavorka++;
                            if (input[ii] == ')')
                            {
                                if (--zavorka == 0)
                                {
                                    i = ii;
                                    break;
                                }
                            }
                        }

                        parsing_counter++;
                    }
                }
                #endregion

                if (parsing[parsing.Count - 1].Length == 0)
                    parsing.Remove(parsing[parsing.Count - 1]);
                // druhy pruchod rozdeleni do stromu
                //bool zavorka = false;
                
                //provedeme spocitani jednotlivych zavorek
                if (cout_bracket > 0)
                {
                    for (int i = 0; i < parsing.Count; i++)
                    {
                        if (parsing[i][0] == '(')
                        {
                            Tree bracket_calculation = new Tree(parsing[i].ToString());
                            parsing[i] = new StringBuilder(bracket_calculation.GetResult());
                            if (--cout_bracket == 0)
                                break;
                            
                        }
                    }
                }

                // pokud je pocet prioritnich operaci nula, neni treba tvorit strom
                // a nebo pokud jsou vsecny operace prioritni neni potreba tvori strom
                if (cout_priority == 0 || cout_priority == cout_operation)
                {
                    string res = parsing[0].ToString();
                    //string op1 = parsing[0].ToString();
                    string op2 = "";
                    string zn = "";
                    for (int i = 1; i < parsing.Count; i++)
                    {
                        if (parsing[i].ToString() == "+" || parsing[i].ToString() == "-" || parsing[i].ToString() == "*" || parsing[i].ToString() == "/")
                        {
                            // pokud se rovna "" tak znamenko jeste nebylo prirazeno a priradi
                            // pokud ne, tak uz je u dalsiho znamenka a provede vypocet
                            if (zn == "")
                                zn = parsing[i].ToString();
                            else
                            {
                                i--; // vrati, protoze pri dalsim inkrementu by znamenko vynechal
                                res = TreeCalculation.ExecuteOperation(res, op2, zn);
                                zn = "";
                            }

                        }
                        else
                            op2 = parsing[i].ToString();
                    }
                    res = TreeCalculation.ExecuteOperation(res, op2, zn);

                    this.result = res;

                    return;
                }

                // this.CreateFirst(parsing.ToArray()); // vytvori zaklad stromu
                #region Rozdělení do stromu
                for (int i = 0; i < parsing.Count; i++)
                {
                    Nodes buff = new Nodes(GetNameNode(parsing[i].ToString()), parsing[i].ToString());

                    if (IsValue(parsing[i].ToString()))
                    {
                        if (i + 1 >= parsing.Count)
                        {
                            //if (IsPlusMinus(parsing[i - 1]) && !IsPlusMinus(parsing[i - 3]))

                            break;
                        }
                        if (IsPlusMinus(parsing[i + 1].ToString()))
                        {
                            if (root != null) // pokud byl uz root vytvoren jen ho presune
                            {
                                Nodes old_root = this.root;
                                if (this.root.right != null)
                                    this.root.left = new Nodes(GetNameNode(parsing[i].ToString()), parsing[i].ToString());
                                else
                                    this.root.right = new Nodes(GetNameNode(parsing[i].ToString()), parsing[i].ToString());
                                this.root = new Nodes(GetNameNode(parsing[++i].ToString()), parsing[i].ToString());
                                // pokud koren obsahuje * tak je prirazen v pravo
                                if (!IsPlusMinus(old_root.Value))
                                    this.root.right = old_root;
                                else
                                    this.root.left = old_root;
                            }
                            else // pokud nebyl tak se podiva na dalsi znamenko, pokud je plus priradi hodnotu na prave, pokud je * tak inkrementuje a pokracuje
                            {
                                this.root = new Nodes(GetNameNode(parsing[++i].ToString()), parsing[i].ToString());
                                this.root.left = buff;
                                if (i + 2 >= parsing.Count || IsPlusMinus(parsing[i + 2].ToString()))
                                {
                                    Nodes old_root = root;
                                    this.root.right = new Nodes(GetNameNode(parsing[++i].ToString()), parsing[i++].ToString()); // prida na pravou stranu hodnotu /inkrementuje na znamenko
                                    this.root = new Nodes(GetNameNode(parsing[i].ToString()), parsing[i].ToString()); // a vytvori novy koren z nasledujiciho znamenka, pouze pokud je to plus ci minus
                                    this.root.left = old_root;
                                    
                                }


                            }
                        }
                        else
                        {
                            if (root != null) // pokud byl uz root vytvoren najde nejposlednejsi vetev a priradi se
                            {
                                i++; // inkrementuje se, aby odpovidal hodnote znamenka
                                if (i + 2 >= parsing.Count || IsPlusMinus(parsing[i + 2].ToString())) // pokud je nasledujici znamenko +/- ulozi hodnotu i napravo
                                {
                                    Nodes buf_right = this.root;
                                    // vyhledání posledniho predka napravo
                                    while (buf_right.right != null)
                                    {
                                        buf_right = buf_right.right;
                                    }

                                    Nodes buf2 = new Nodes(GetNameNode(parsing[i].ToString()), parsing[i].ToString());
                                    buf_right.right = buf2;
                                    buf2.left = new Nodes(GetNameNode(parsing[i - 1].ToString()), parsing[i - 1].ToString());
                                    buf2.right = new Nodes(GetNameNode(parsing[++i].ToString()), parsing[i++].ToString()); //inkrementace na uroven nasledujiciho znamenka
                                    // a je potreba pro hladky chod algoritmu vytvorit koren zde
                                    if (i >= parsing.Count) // kontrola parsovani
                                    {
                                        break;
                                    }
                                    buf_right = this.root;
                                    this.root = new Nodes(GetNameNode(parsing[i].ToString()), parsing[i].ToString());
                                    this.root.left = buf_right;
                                    if (i + 2 == parsing.Count)
                                        this.root.right = new Nodes(GetNameNode(parsing[++i].ToString()), parsing[i].ToString());
                                }
                                else // v ostatnich pripadech prida jen do prava
                                {
                                    Nodes buf_right = this.root;
                                    // vyhledání posledniho predka napravo
                                    while (buf_right.right != null)
                                    {
                                        buf_right = buf_right.right;
                                    }
                                    Nodes buf2 = new Nodes(GetNameNode(parsing[i].ToString()), parsing[i].ToString());
                                    buf_right.right = buf2;
                                    buf2.left = new Nodes(GetNameNode(parsing[i - 1].ToString()), parsing[i - 1].ToString());
                                }
                            }
                            else // pokud nebyl tak se podiva na dalsi znamenko, pokud je plus priradi hodnotu na prave, pokud je * tak inkrementuje a pokracuje
                            {
                                this.root = new Nodes(GetNameNode(parsing[++i].ToString()), parsing[i].ToString());
                                this.root.left = buff;
                                if (i + 2 >= parsing.Count || IsPlusMinus(parsing[i + 2].ToString()))
                                {
                                    Nodes old_root = root;
                                    this.root.right = new Nodes(GetNameNode(parsing[++i].ToString()), parsing[i++].ToString()); // prida na pravou stranu hodnotu /inkrementuje na znamenko
                                    this.root = new Nodes(GetNameNode(parsing[i].ToString()), parsing[i].ToString()); // a vytvori novy koren z nasledujiciho znamenka, pouze pokud je to plus ci minus
                                    this.root.right = old_root;
                                    if (i + 2 == parsing.Count)
                                        this.root.left = new Nodes(GetNameNode(parsing[++i].ToString()), parsing[i].ToString());
                                }


                            }

                        }
                    }

                }
                #endregion

                ExecuteTree();


            }


            private void ExecuteTree()
            {
                this.result = TreeCalculation.ExecuteOperation(ExecuteTreeLeft(this.root.left), ExecuteTreeRight(this.root.right), this.root.Value);
            }

            // pruchod doprava
            /*private string ExecuteTreeRight(Nodes list)
            {
                if (list.right.Name == "hodnota")
                    return TreeCalculation.ExecuteOperation(list.left.Value, list.right.Value, list.Value);
                return TreeCalculation.ExecuteOperation(ExecuteTreeLeft(list.left), ExecuteTreeRight(list.right), list.Value);
            }
            //pruchod doleva
            private string ExecuteTreeLeft(Nodes list)
            {
                if (list.left.Name == "hodnota")
                    return TreeCalculation.ExecuteOperation(list.left.Value, list.right.Value, list.Value);
                return TreeCalculation.ExecuteOperation(ExecuteTreeLeft(list.left), ExecuteTreeRight(list.right), list.Value);
            }*/

            private string ExecuteTreeRight(Nodes list)
            {
                if (list.right == null)
                    return list.Value;
                return TreeCalculation.ExecuteOperation(ExecuteTreeLeft(list.left), ExecuteTreeRight(list.right), list.Value);
            }
            //pruchod doleva
            private string ExecuteTreeLeft(Nodes list)
            {
                if (list.left == null)
                    return list.Value;
                return TreeCalculation.ExecuteOperation(ExecuteTreeLeft(list.left), ExecuteTreeRight(list.right), list.Value);
            }
            /*
            private void SetRoot(Nodes new_root)
            {
                //nasteveni korene, pokud je koren null provede se jen prirazeni
                //pokud neni koren null, provede se prirazeni i s vyvazenim stromu
                if (this.root != null)
                {
                    if (root.Value == "+" || root.Value == "-")
                    {
                        new_root.left = this.root;  // nastavi novemu korenu jako leveho potomka stary koren
                    }
                    else
                    {
                        new_root.right = this.root;
                    }

                    this.root = new_root;       // nastavi novy koren

                }
                else
                    this.root = new_root;
            }*/

            private static string GetNameNode(string input)
            {
                if (input == "+" || input == "-" || input == "*" || input == "/")
                    return "znamenko";
                else
                    return "hodnota";
            }

            private static bool IsValue(string input)
            {
                if (input == "+" || input == "-" || input == "*" || input == "/")
                    return false;
                else
                    return true;
            }

            private static bool IsPlusMinus(string input)
            {
                if (input == "+" || input == "-")
                    return true;
                else
                    return false;
            }   
        }

    }
}
