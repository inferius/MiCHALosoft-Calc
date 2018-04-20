﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

/*
 * Math.cs
 * 
 * Třida pro matematické operace s řetezci
 * 
 * verze : 1.0
 * 
 * Michal Diviš
 * */

namespace MiCHALosoft_CALC
{
    class Symbol
    {
        private string name;
        private string value;
        private string type;
    }

    class Math
    {
        public static string SumString(string s1, string s2)
        {
            int TotalLength = 0,
                MinLength = 0;
            string is1 = s1,
                is2 = s2, buff, buff2 = "", go_next = "0",
                is1_mant = "",
                is2_mant = "";
            int des_tec = -1,
                des_tec2 = -1;

            des_tec = DeletePoint(ref is1, ref is1_mant);
            des_tec2 = DeletePoint(ref is2, ref is2_mant);

            if (des_tec != -1 || des_tec2 != -1)
                if (des_tec < des_tec2)
                    des_tec = des_tec2;


            StringBuilder result = new StringBuilder();
            StringBuilder mant_res = new StringBuilder();

            if (!TestStringAnFloat(s1) || !TestStringAnFloat(s2))
            {
                return "#err:0";    //chyba cislo 0 : cislo zadano ve spatnem formatu
            }

             //int carka = 0;
             TotalLength = is1_mant.Length > is2_mant.Length ? is1_mant.Length : is2_mant.Length;
             MinLength = is1_mant.Length < is2_mant.Length ? is1_mant.Length : is2_mant.Length;

             if (des_tec != -1 || des_tec2 != -1)
             {
                 for (int i = MinLength; i < TotalLength; i++)
                 {
                     if (is1_mant.Length > is2_mant.Length)
                         is2_mant += '0';
                     else
                         is1_mant += '0';
                 }
             }

             if (is1.Length != is2.Length)
             {
                 TotalLength = is1.Length > is2.Length ? is1.Length : is2.Length;
                 MinLength = is1.Length < is2.Length ? is1.Length : is2.Length;


                 int CountZero = TotalLength - MinLength;
                 StringBuilder zero = new StringBuilder();

                 for (int i = 0; i < CountZero; i++)
                     zero.Append('0');
                 if (is1.Length > is2.Length)
                     is2 = zero.ToString() + s2;
                 else
                     is1 = zero.ToString() + s1;


             }


            for (int i = is1.Length-1; i >= 0; i--)
            {
                buff = SumOneChar(is1, is2, i, i, go_next);
                

                if (i == 0 && buff.Length > 1)
                {
                    result.Append(buff[1].ToString());
                    result.Append(buff[0].ToString());

                    break;
                }

                if (buff.Length > 1)
                {
                    buff2 = buff[1].ToString();
                    go_next = buff[0].ToString();

                    result.Append(buff2);

                    if (i != 0)
                        continue;
                }
                else
                {
                    result.Append(buff);
                    go_next = "0";
                }
            }

            if (des_tec != -1)
            {
                for (int i = is1_mant.Length - 1; i >= 0; i--)
                {
                    buff = SumOneChar(is1_mant, is2_mant, i, i, go_next);


                    if (i == 0 && buff.Length > 1)
                    {
                        mant_res.Append(buff[1].ToString());
                        mant_res.Append(buff[0].ToString());

                        break;
                    }

                    if (buff.Length > 1)
                    {
                        buff2 = buff[1].ToString();
                        go_next = buff[0].ToString();

                        mant_res.Append(buff2);

                        if (i != 0)
                            continue;
                    }
                    else
                        mant_res.Append(buff);
                }
            }


            if (des_tec != -1)
            {
                return AddPoint(ReverseString(result.ToString()),ReverseString(mant_res.ToString()) , des_tec, 1);

            }

            return ReverseString(result.ToString());
            //return result.ToString();
        }

        private static bool TestStringAnFloat(string s1)
        {
            int carka = 0;
            
            for (int i = 0; i < s1.Length; i++)
            {
                if (!char.IsDigit(s1, i) && s1[i] != '.' && s1[i] != '-')
                    return false;

                if (s1[i] == ',' || s1[i] == '.')
                {
                    if (++carka > 1)
                    {
                        return false;
                    }
                }
            }

            return true;
        }

        private static string SumOneChar(string one, string two, int index_one, int index_two, string slo_dal)
        {
            int res = Int32.Parse(one[index_one].ToString()) + Int32.Parse(two[index_two].ToString()) + Int32.Parse(slo_dal);

            return res.ToString();
        }

        private static string ProductOneChar(string one, string two, int index_one, int index_two, string slo_dal)
        {
            int res = Int32.Parse(one[index_one].ToString()) * Int32.Parse(two[index_two].ToString());

            res += Int32.Parse(slo_dal);

            return res.ToString();
        }

        private static string DesumOneChar(string one, string two, int index_one, int index_two, string slo_dal)
        {
            int prvni = Int32.Parse(one[index_one].ToString()),
                druhy = Int32.Parse(two[index_two].ToString()),
                go_next = Int32.Parse(slo_dal),
                res = 0;

            if (((druhy + go_next) > prvni) || (((druhy + go_next) == 0) && go_next != 0))
            {
                prvni += 10;
                res = prvni - (druhy + go_next);
                res += 10;
            }
            else
                res = prvni - (druhy + go_next);

            return res.ToString();
        }

        private static string ReverseString(string input)
        {
            string res = "";

            for (int i = input.Length-1; i >= 0; i--)
            {
                res += input[i];
            }

            return res;
        }


        public static string DesumString(string s1, string s2)
        {
            int TotalLength = 0,
                MinLength = 0;
            string is1 = s1,
                is2 = s2, buff, buff2 = "", go_next = "0", znamenko = "",
                is1_mant = "",
                is2_mant = "";
            int des_tec = -1,
                des_tec2 = -1;

            des_tec = DeletePoint(ref is1, ref is1_mant);
            des_tec2 = DeletePoint(ref is2, ref is2_mant);

            if (des_tec != -1 || des_tec2 != -1)
                if (des_tec < des_tec2)
                    des_tec = des_tec2;


            StringBuilder result = new StringBuilder();
            StringBuilder mant_res = new StringBuilder();

            //if (float.Parse(s1) < float.Parse(s2))
            //    znamenko = "-";

            if (EqualString(s1, s2) == -1)
                znamenko = "-";

            if (EqualString(s1, s2) == 0)
                return "0";

            //StringBuilder result = new StringBuilder();

            if (!TestStringAnFloat(s1) || !TestStringAnFloat(s2))
            {
                return "#err:0";    //chyba cislo 0 : cislo zadano ve spatnem formatu
            }

            TotalLength = is1_mant.Length > is2_mant.Length ? is1_mant.Length : is2_mant.Length;
            MinLength = is1_mant.Length < is2_mant.Length ? is1_mant.Length : is2_mant.Length;

            if (des_tec != -1 || des_tec2 != -1)
            {
                for (int i = MinLength; i < TotalLength; i++)
                {
                    if (is1_mant.Length > is2_mant.Length)
                        is2_mant += '0';
                    else
                        is1_mant += '0';
                }
            }

            if (is1.Length != is2.Length)
            {
                TotalLength = is1.Length > is2.Length ? is1.Length : is2.Length;
                MinLength = is1.Length < is2.Length ? is1.Length : is2.Length;


                int CountZero = TotalLength - MinLength;
                StringBuilder zero = new StringBuilder();

                for (int i = 0; i < CountZero; i++)
                    zero.Append('0');
                if (is1.Length > is2.Length)
                    is2 = zero.ToString() + s2;
                else
                    is1 = zero.ToString() + s1;


            }


            for (int i = is1.Length - 1; i >= 0; i--)
            {
                if (znamenko == "-")
                    buff = DesumOneChar(is2, is1, i, i, go_next);
                else
                    buff = DesumOneChar(is1, is2, i, i, go_next);

                if (buff.Length > 1)
                {
                    buff2 = buff[1].ToString();
                    go_next = buff[0].ToString();
                }

                if (i == 0 && buff.Length > 1)
                {
                    result.Append(buff2);
                    result.Append(go_next);
                }
                else
                    result.Append(buff);
            }

            if (des_tec != -1)
            {
                for (int i = is1_mant.Length - 1; i >= 0; i--)
                {
                    
                    if (znamenko == "-")
                        buff = DesumOneChar(is2_mant, is1_mant, i, i, go_next);
                    else
                        buff = DesumOneChar(is1_mant, is2_mant, i, i, go_next);


                    if (i == 0 && buff.Length > 1)
                    {
                        mant_res.Append(buff[1].ToString());
                        mant_res.Append(buff[0].ToString());

                        break;
                    }

                    if (buff.Length > 1)
                    {
                        buff2 = buff[1].ToString();
                        go_next = buff[0].ToString();

                        mant_res.Append(buff2);

                        if (i != 0)
                            continue;
                    }
                    else
                        mant_res.Append(buff);
                }
            }

            for (int i = result.Length-1; i >= 0 ; i--)
            {
                if (result[i] != '0')
                    break;
                result.Remove(i, 1);
            }

            

            if (des_tec != -1)
            {
                StringBuilder b_ret = new StringBuilder();
                b_ret.Append(AddPoint(ReverseString(result.ToString()), ReverseString(mant_res.ToString()), des_tec, 2));
                if (znamenko == "-")
                    b_ret.Insert(0, "-");

                return b_ret.ToString() ;

            }

            if (znamenko == "-")
                result.Append('-');


            return ReverseString(result.ToString());
            //return result.ToString();
        }

        /*
         * Tato metoda odstrani desetinou carku a rozdeli retezec na hlavni cas a mantisu, ktere vrati pomoci reference
         * input = cely vstupni retezec (napr. 20,51), pomoci reference je vraceno 20
         * mantisa = pomoci reference zde bude pro vyse uvedeny priklad vraceno 51
         * navratova hodnota je umisteni desetine carky
         * */
        private static int DeletePoint(ref string input, ref string mantisa)
        {
            StringBuilder res = new StringBuilder();
            StringBuilder mant = new StringBuilder();
            int ret = -1;
            bool tecka = false;

            for (int i = 0; i < input.Length; i++)
            {
                if (input[i] == '.' || input[i] == ',')
                {
                    ret = input.Length - i - 1;
                    tecka = true;
                    continue;
                }

                if (tecka)
                    mant.Append(input[i]);
                else
                    res.Append(input[i]);
            }

            mantisa = mant.ToString();
            input = res.ToString();

            return ret;
        }

        /*
         * Tato funkce spojuje znovu dva retezce hlavni a mantisu
         * poloha = urcuje polohu desetine carky, kterou vrati funkce DeletePoint
         * scitat = 1 - ze se pripadne posledni prvek mantisy bude pricitat
         *          2 - odecitat
         *          3 - nasobit
         *                    
         * vraci spojeny retezec
         * */
        private static string AddPoint(string input, string mantisa, int poloha, int scitat)
        {
            StringBuilder res = new StringBuilder();

            if (mantisa.Length > poloha)
            {
                if (scitat == 1)
                    res.Append(SumString(input, mantisa[0].ToString()));
                else if (scitat == 2)
                    res.Append(DesumString(input, mantisa[0].ToString()));
                else if (scitat == 3)
                {
                    res.Append(input);
                    res.Insert(poloha, '.');
                }

                res.Append(".");
                for (int i = 1; i < mantisa.Length; i++)
                {
                    res.Append(mantisa[i]);
                }
            }
            else
            {
                res.Append(input);
                res.Append(".");
                res.Append(mantisa);
            }
                


            return res.ToString();
        }

        /*
         * Tato metoda porovnava dva string
         * vraci 
         * 1 - pokud je prvni větší než druhy
         * 0 - pokud jsou stejne
         * -1 - pokud je druhy vetsi nez prvni
         * -2 - spatny vstupni format
         * */
        public static int EqualString(string s1, string s2)
        {
            string is1 = s1, is1_mant = "",
                is2 = s2, is2_mant = "";
            int delka = 0;

            if (!TestStringAnFloat(s1) || !TestStringAnFloat(s2))
            {
                //return "#err:0";    //chyba cislo 0 : cislo zadano ve spatnem formatu
                return -2;
            }

            DeletePoint(ref is1, ref is1_mant);
            DeletePoint(ref is2, ref is2_mant);

            if (is1_mant.Length == 0)                
                is1_mant = "0";

            if (is2_mant.Length == 0)
                is2_mant = "0";

            if (is1_mant.Length >= is2_mant.Length)
                delka = is2_mant.Length;
            else
                delka = is1_mant.Length;

            if (is1.Length > is2.Length)
            {
                return 1;
            }
            else if (is1.Length < is2.Length)
            {
                return -1;
            }
            else if (is1.Length == is2.Length)
            {
                for (int i = 0; i < is1.Length; i++)
                {
                    if (Int32.Parse(is1[i].ToString()) > Int32.Parse(is2[i].ToString()))
                        return 1;
                    else if (Int32.Parse(is1[i].ToString()) < Int32.Parse(is2[i].ToString()))
                        return -1;
                }

                for (int i = 0; i < delka; i++)
                {
                    if (Int32.Parse(is1_mant[i].ToString()) > Int32.Parse( is2_mant[i].ToString()))
                        return 1;
                    else if (Int32.Parse(is1_mant[i].ToString()) < Int32.Parse(is2_mant[i].ToString()))
                        return -1;
                }
                if (is1_mant.Length == is2_mant.Length)
                    return 0;
                else if (is1_mant.Length > is2_mant.Length)
                    return 1;
                else
                    return -1;
            }

            return -2;


        }

        public static string ProductString(string s1, string s2)
        {
            string is1 = Math.EqualString(s1, s2) == 1 ? s1 : s2,
                is2 = Math.EqualString(s1, s2) == 1 ? s2 : s1, 
                buff, buff2 = "", go_next = "0", znamenko = "",
                is1_mant = "",
                is2_mant = "";
            int des_tec = -1,
                des_tec2 = -1;

            

            List<string> soucty = new List<string>();

            if (s1[0] == '-' && s2[0] == '-')
            {
                znamenko = "";
                is1.Remove(0, 1);
                is2.Remove(0, 1);
            }
            else if (s1[0] == '-' || s2[0] == '-')
            {
                if (is1[0] == '-')
                    is1.Remove(0, 1);
                else
                    is2.Remove(0, 1);

                znamenko = "-";
            }


            des_tec = DeletePoint(ref is1, ref is1_mant);
            des_tec2 = DeletePoint(ref is2, ref is2_mant);

            is1 = is1 + is1_mant;
            is2 = is2 + is2_mant;

            if (des_tec != -1 || des_tec2 != -1)
                if (des_tec != -1)
                {
                    if (des_tec2 != -1)
                        des_tec += des_tec2;
                }
                else if (des_tec2 != -1)
                {
                    des_tec = des_tec2;
                }
                


            StringBuilder result = new StringBuilder();
            StringBuilder mant_res = new StringBuilder();

            if (!TestStringAnFloat(s1) || !TestStringAnFloat(s2))
            {
                return "#err:0";    //chyba cislo 0 : cislo zadano ve spatnem formatu
            }

            for (int i = is2.Length-1, zero = 0; i >= 0; i--)
            {
                for (int ii = is1.Length-1; ii >= 0; ii--)
                {
                    buff = ProductOneChar(is1, is2, ii, i, go_next);

                    if (ii == 0 && buff.Length > 1)
                    {
                        result.Append(buff[1].ToString());
                        result.Append(buff[0].ToString());

                        break;
                    }

                    if (buff.Length > 1)
                    {
                        buff2 = buff[1].ToString();
                        go_next = buff[0].ToString();

                        result.Append(buff2);

                        if (ii != 0)
                            continue;
                    }
                    else
                        result.Append(buff);
                }
                soucty.Add(ReturnZero(i) + ReverseString(result.ToString()) + ReturnZero(zero));
                zero++;
                go_next = "0";
                result.Remove(0, result.Length);
            }
            string res_s = "";

            if (soucty.Count == 1)
                res_s = soucty[0];
            else if (soucty.Count > 1 && soucty.Count <= 2)
                res_s = SumString(soucty[0], soucty[1]);
            else if (soucty.Count > 2)
            {
                for (int i = 2; i < soucty.Count; i++)
                {
                    res_s = SumString(soucty[i], res_s);
                }
            }



            if (des_tec != -1)
            {
                //return AddPoint(ReverseString(result.ToString()), null, des_tec, 3);
                res_s = res_s.Insert(res_s.Length - des_tec, ".");
            }

            if (znamenko == "-")
            {
                res_s.Insert(0, "-");
            }

            return res_s.ToString();
        }

        private static string ReturnZero(int pocet)
        {
            if (pocet > 0)
            {
                StringBuilder zero = new StringBuilder();

                for (int i = 0; i < pocet; i++)
                    zero.Append('0');

                return zero.ToString();
            }

            return "";            
        }

        public static string Faktorial(string input)
        {
            //StringBuilder res = new StringBuilder(input);
            string res = input;
            int poloha = Console.CursorLeft;

            if (EqualString(input, "1") != 1)
                return "1";

            Console.Write("Calculate-Wait please: 0%");
            Console.CursorLeft = poloha;

            for (int i = 1, prog = 0; Math.EqualString(i.ToString(), input) == -1; i++)
            {
                //res.Insert(0, ProductString(res.ToString(), DesumString(input, i.ToString())));
                if (i == ((Int64.Parse(input) / 100) * (prog + 1)))
                {
                    Console.Write("Calculate-Wait please: {0}%", prog);
                    Console.CursorLeft = poloha;
                    prog++;
                }
                res = ProductString(res.ToString(), DesumString(input, i.ToString()));

            }

            return res.ToString();
        }

        public static string Fakt(string input)
        {
            if (EqualString(input, "1") == 0)
                return "1";
            else
            {
                return ProductString(input, Fakt(DesumString(input, "1")));
            }
        }





    }
}
                    