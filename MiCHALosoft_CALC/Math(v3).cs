using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace MiCHALosoft_CALC
{
    class Math_v3
    {
        public static bool IsInteger(string num)
        {
            //int carka = 0;

            /*for (int i = 0; i < num.Length; i++)
            {
                if (num[i] == '.' || num[i] == '/')
                    return false;
            }*/

            if (num.IndexOf(".") != -1 || num.IndexOf("/") != -1)
                return false;

            return true;
        }

        private static int DeletePoint(ref string input)
        {
            for (int i = 0; i < input.Length; i++)
            {
                if (input[i] == '.')
                {
                    input = input.Remove(i, 1);
                    return input.Length - i;
                }

            }

            return 0;
        }

        public static string ProductString(string op1, string op2)
        {
            int znamenko = 0;
            const int FOR_LENGTH = 8;            

            if (!IsInteger(op1) || !IsInteger(op2))
            {
                znamenko = DeletePoint(ref op1);
                znamenko += DeletePoint(ref op2);
            }

            // konzistence nasobeni
            if (op2.Length > op1.Length)
            {
                string buf = op1;
                op1 = op2;
                op1 = buf;
            }

            if (op1.Length > FOR_LENGTH && op2.Length <= FOR_LENGTH)
            {
                string[] ops = new string[op1.Length / FOR_LENGTH + 1];

                for (int i = 0; i < op1.Length / FOR_LENGTH + 1; i++)
                {
                    ops[i] = op1.Remove(0, i * FOR_LENGTH);
                    //if (op1.Length/8 != i)
                    if (ops[i].Length > FOR_LENGTH)
                        ops[i] = ops[i].Remove(FOR_LENGTH);
                }                
                StringBuilder res = new StringBuilder();
                ulong go_next = 0;

                for (int i = ops.Length-1; i >= 0 ; i--)
                {
                    if (ops[i] != "")
                    {
                      

                        ulong resi = ulong.Parse(ops[i]) * ulong.Parse(op2) + go_next;
                        string res2 = resi.ToString();

                        if (res2.Length > ops[i].Length)
                        {
                            if (i == 0)
                                res.Insert(0, res2);
                            else
                            {
                                go_next = ulong.Parse(res2.Remove(res2.Length - ops[i].Length));
                                res.Insert(0, res2.Remove(0, res2.Length - ops[i].Length));

                            }
                        }
                        else
                        {
                            res.Insert(0, res2);
                            //doplneni nulama
                            for (int ii = 0; ii < FOR_LENGTH - res2.Length; ii++)
                            {
                                if (i != 0)
                                    res.Insert(0, "0");

                            }
                            go_next = 0;
                        }

                    }
                    
                }

                if (znamenko > 0)
                    res.Insert(res.Length - znamenko, ".");
                return res.ToString();
            }
            else if (op1.Length <= FOR_LENGTH && op2.Length <= FOR_LENGTH)
            {
                string res = (ulong.Parse(op1) * ulong.Parse(op2)).ToString();
                if (znamenko > 0)
                    return res.Insert(res.Length - znamenko, ".");
                return res;
            }

            else 
            {                

                return Math_v2.ProductString(op1, op2);
                /*StringBuilder[] scitance = AppendZero(op2.Length);
                ulong[] go_next = new ulong[op1.Length / FOR_LENGTH];
                string[] ops1 = new string[op1.Length / FOR_LENGTH];
                string[] ops2 = new string[op2.Length / FOR_LENGTH];
                for (int i = 0; i < op1.Length / FOR_LENGTH; i++)
                    go_next[i] = 0;

                for (int i = 0; i < op1.Length / FOR_LENGTH; i++)
                {
                    ops1[i] = op1.Remove(0, i * FOR_LENGTH);
                    //if (op1.Length/8 != i)
                    if (ops1[i].Length > FOR_LENGTH)
                        ops1[i] = ops1[i].Remove(FOR_LENGTH);
                }
                for (int i = 0; i < op1.Length / FOR_LENGTH; i++)
                {
                    ops2[i] = op2.Remove(0, i * FOR_LENGTH);
                    //if (op1.Length/8 != i)
                    if (ops2[i].Length > FOR_LENGTH)
                        ops2[i] = ops2[i].Remove(FOR_LENGTH);
                } 

                for (int i = ops2.Length-1; i >= 0; i--)
                {
                    for (int ii = ops1.Length-1; ii >= 0; ii--)
                    {
                        ulong res = ulong.Parse(ops1[ii]) * ulong.Parse(ops2[i].ToString()) + go_next[ii];
                        string resi = res.ToString();
                        if (FOR_LENGTH == resi.Length)
                        {
                            scitance[ii].Append(resi);                            
                        }
                        else if (FOR_LENGTH > resi.Length)
                        {
                            scitance[ii].Append(resi);
                            for (int y = 0; y < FOR_LENGTH - resi.Length; y++)
                                scitance[ii].Append("0");
                        }
                        else
                        {
                            if (ii == 0)
                            {
                                scitance[ii].Append(resi);
                                continue;
                            }
                            go_next[ii - 1] = ulong.Parse(resi.Remove(resi.Length - FOR_LENGTH));
                            scitance[ii].Append(resi.Remove(0, resi.Length - FOR_LENGTH));
                        }
                    }

                    
                }

                // secist scitance
                string ret = "0";
                for (int i = 0; i < scitance.Length; i++)
                {
                    ret = SumString(ret, scitance[i].ToString());
                }

                return ret;*/

            }

            //return "0";
        
        
        }


        private static StringBuilder[] AppendZero(int pocet)
        {
            StringBuilder[] ret = new StringBuilder[pocet];

            for (int i = 0; i < pocet; i++)
            {
                ret[i] = new StringBuilder();
                for (int ii = 0; ii < i; ii++)
                    ret[i].Append("0000000");

            }

            return ret;
        }

        public static string SumString(string op1, string op2)
        {
            return Math_v2.SumString(op1, op2);
        }

        public static string Fakt(string input)
        {
            if (Math_v2.EqualString(input, "1") == 0)
                return "1";
            if (Math_v2.EqualString(input, "0") == 0)
                return "1";
            else
            {
                //return ProductString(input, Fakt(DesumString(input, "1")));
                string ret = "1";
                for (string i = "1"; Math_v2.EqualString("<=", i, input); i = Math_v2.Inc1(i))
                {
                    ret = ProductString(ret, i);
                }

                return ret;
            }


        }

        public static int MaxLength(string input, string operace)
        {
            if (IsInteger(input))
            {
                if (operace == "*")
                    return 8;
                else if (operace == "+")
                    return 16;
            }
            else
            {
                if (operace == "*")
                    return 8;
                else if (operace == "+")
                    return 16; 
                
            }

            return 8;
        }

    }
}
