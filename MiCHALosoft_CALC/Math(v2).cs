using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Globalization;
using System.Text.RegularExpressions;

/*
 * MiCHALosoft(c) 2010, Michal Diviš
 * michalosoft@gmail.com
 * 
 * Matematicka knihovna Math
 * verze: 2.0
 * 
 * Porovnani casu verze 1.1.1 s verzi 2.0
 * Faktorial cisla 200:
 * v1: 37 343 ms
 * v2:  9 078 ms
 * 
 * Mocnina 100 000*((-1)^1350)
 * v1: 741 410 ms
 * v2: 171 ms
 */


namespace MiCHALosoft_CALC
{
    public static class Math_v2
    {
        public static int OmezeniDesetinnychMist = 15;      //zadejte pocet vypisovanych desetinnyc mist
        public const string pi = "3.141592653589793238462643383279502884197169399375105820974944592307816406286208998628034825342117067982148086513282306647093844609550582231725359408128481117450284102701938521105559644622948954930381964428810975665933446128475648233786783165271201909145648566923460348610454326648213393607260249141273724587006606315588174881520920962829254091715364367892590360011330530548820466521384146951941511609433057270365759591953092186117381932611793105118548074462379962749567351885752724891227938183011949129833673362440656643086021394946395224737190702179860943702770539217176293176752384674818467669";
        public const string e = "2.7182818284590452353602874713526624977572470936999595749669676277240766303535475945713821785251664274274663919320030599218174135966290435729003342952605956307381323286279434907632338298807531952510190115738341879307021540891499348841675092447614606680822648001684774118537423454424371075390777449920695517027618386062613313845830007520449338265602976067371132007093287091274437470472306969772093101416928368190255151086574637721112523897844250569536967707854499699679468644549059879316368892300987931277361782154249992295763514822082698951936680331825288693984964651058209392398294887933203625094431173012381970684161403970198376793206832823764648042953118023287825098194558153017567173613320698112509961818815930416903515988885193458072738667385894228792284998920868058257492796104841984443634632449684875602336248270419786232090021609902353043699418491463140934317381436405462531520961836908887070167683964243781405927145635490613031072085103837505101157477041718986106873969655212671546889570350";
        public const string inf = "inf";
        public const string minus_inf = "-inf";

        public static string Fakt(string input)
        {
            if (EqualString(input, "1") == 0)
                return "1";
            if (EqualString(input, "0") == 0)
                return "1";
            else
            {
                //return ProductString(input, Fakt(DesumString(input, "1")));
                string ret = "1";
                for (string i = "1"; EqualString("<=", i, input); i = Inc1(i))
                {
                    ret = ProductString(ret, i);
                }

                return ret;
            }

            
        }

        public static string ReturnInteger(string num)
        {
            StringBuilder ret = new StringBuilder();

            for (int i = 0; i < num.Length; i++)
            {
                if (num[i] == '.')
                    return ret.ToString();

                ret.Append(num[i]);
            }

            return num;
        }

        public static string ReturnDecimal(string num)
        {
            StringBuilder ret = new StringBuilder();
            bool tecka = false;

            for (int i = 0; i < num.Length; i++)
            {
                if (tecka)
                    ret.Append(num[i]);
                if (num[i] == '.')
                    tecka = true;
            }

            if (tecka)
            {
                if (ret.ToString() == "")
                    return "0";
                else
                    return ret.ToString();
            }
            else
                return "0";
        }

        private static string ReturnDec(int pocet)
        {
            StringBuilder ret = new StringBuilder("1");

            for (int i = 0; i < pocet; i++)
                ret.Append("0");

            return ret.ToString();

        }


        // metoda vycisti retezec od nul na zacatku a na konci
        public static string CleanString(string vstup)
        {
            bool cislo = false,
                tecka = false,
                zaporne = false;
            StringBuilder input = new StringBuilder(vstup);
            if (vstup[0] == '-')
            {
                zaporne = true;
                input.Remove(0, 1);
            }

            for (int i = 0; i < input.Length; i++)
            {
                if (input[i] != '0')
                    cislo = true;



                if (input[i] == '.')
                {
                    for (int ii = input.Length - 1; ii > 0; ii--)
                    {
                        if (input[ii] != '0')
                        {
                            if (input[ii] == '.')
                                tecka = true;
                            break;
                        }
                        else
                            input.Remove(ii, 1);
                    }

                    if (tecka)
                        input.Remove(i, 1);

                    if (i >= input.Length)
                        break;

                    tecka = true;
                }

                if (input[i] == '0' && (input.Length == i + 1 ? true : input[i + 1] != '.') && !(cislo == true))
                {
                    input.Remove(i, 1);
                    i--;
                    if (i >= input.Length)
                        break;
                }
            }

            if (input.Length == 0)
                input.Append("0");
            return zaporne ? input.Insert(0, "-").ToString() : input.ToString();
        }

        public static string Sub1(string inp)
        {
            return DesumString(inp, "1");
        }

        public static string Inc1(string inp)
        {
            return SumString(inp, "1");
        }

        public static void Sub1(ref string inp)
        {
            inp = DesumString(inp, "1");
        }

        public static void Inc1(ref string inp)
        {
            inp= SumString(inp, "1");
        }


        public static string Ackermann(string x, string y)
        {
            string ret = "0";

            do
            {
                if (EqualString("==", x, "0"))
                {
                    ret = SumString(ret, SumString(y, "1"));
                }
                else if (EqualString("==", y, "0"))
                {
                    Sub1(ref x); y = "1";
                }
                else
                {
                    Sub1(ref x);
                    Ackermann(x, Sub1(y));
                }

            } while (EqualString("==", x, "0") && EqualString("==", y, "0"));

            return ret;

        }




        // jelikoz, sem si k predchozi tride nedelal temer zadne komentare tak to musim vsechno prepracovat :-)

        // metoda na soucet retezcu
        // 
        /*
         * Tato fce doplni nuly, dle retezce ktery je vetsi a dle argumentu begin a vrati pocet nul
         * begin = true/false true = doplneni nul na zacatek
         * priklad 
         * (input1 = 5035, input2 = 35, begin = true
         * konec fce, input1 = 5035, input2 = 0035)
         * 
         * pirklad 2
         * (input1 = 5035, input2 = 35, begin = false
         * konec fce, input1 = 5035, input2 = 3500)
         */
        private static int AppendZero(ref string s1, ref string s2, bool begin)
        {
            if (s1.Length == s2.Length)
                return 0;

            StringBuilder zero = new StringBuilder();
            bool first;
            int zeroCount = 0;

            if (s1.Length > s2.Length)
            {
                first = false;
                zeroCount = s1.Length - s2.Length;
            }
            else
            {
                first = true;
                zeroCount = s2.Length - s1.Length;
            }

            for (int i = 0; i < zeroCount; i++)
                zero.Append('0');

            if (first == true)
            {
                if (begin == true)
                    s1 = zero.ToString() + s1;
                else
                    s1 = s1 + zero.ToString();
            }
            else
            {
                if (begin == true)
                    s2 = zero.ToString() + s2;
                else
                    s2 = s2 + zero.ToString();
            }

            return zeroCount;
        }


        /// <summary>
        /// Secte vstupni cisla (string) a vrati vysledek jako string
        /// </summary>
        /// <param name="s1"></param>
        /// <param name="s2"></param>
        /// <returns></returns>
        public static string SumString(string s1, string s2, bool convert = true)
        {
            if (convert)
            {
                if (IsInteger(s1) && IsInteger(s2))
                {
                    if (s1[0] == '-' || s2[0] == '-')
                    {
                        if (s1.Length < 17 && s2.Length < 17)
                            return (long.Parse(s1) + long.Parse(s2)).ToString();
                    }
                    else
                    {
                        //ulong z = 18446744073709551615 / 18 446 744 073 709 551 615;
                        if (s1.Length < 18 && s2.Length < 18)
                            return (ulong.Parse(s1) + ulong.Parse(s2)).ToString();
                    }
                }
                /*else
                {
                    if (s1.Length < 18 && s2.Length < 18)
                        return (double.Parse(s1) + double.Parse(s2)).ToString();
                }*/
            }

            bool zaporny = false;

            if (s1[0] == '-' && s2[0] == '-')
            {
                zaporny = true;
                s1 = s1.Remove(0, 1);
                s2 = s2.Remove(0, 1);
            }
            else if (s1[0] == '-' || s2[0] == '-')
            {
                if (s1[0] == '-') return DesumString(s2, s1);
                else return DesumString(s1, s2);
            }
            int point_position_s1 = -1,
                point_position_s2 = -1;
            string buff_s1 = s1,
                buff_s2 = s2,
                mant_s1 = "",
                mant_s2 = "";
            
            

            // rozdeleni desetinnecasti od normalni
            if (TestStringAnFloat(s1))
            {
                point_position_s1 = DeletePoint(ref buff_s1, ref mant_s1);
            }
            if (TestStringAnFloat(s2))
            {
                point_position_s2 = DeletePoint(ref buff_s2, ref mant_s2);
            }
            // pripoji nuly
            AppendZero(ref buff_s1, ref buff_s2, true);
            AppendZero(ref mant_s1, ref mant_s2, false);

            //pomocne promenne k souctu
            string buff;
            int go_next = 0;
            StringBuilder int_res = new StringBuilder();
            StringBuilder int_res_mant = new StringBuilder();

            // provede soucet mantisy
            for (int i = mant_s1.Length-1; i >= 0; i--)
            {
                buff = SumOneChar(mant_s1[i], mant_s2[i], go_next);

                if (buff.Length > 1)
                {
                    go_next = 1;
                    //int_res_mant.Append(buff[1]);
                    int_res_mant.Insert(0, buff[1]);
                }
                else
                {
                    go_next = 0;
                    //int_res_mant.Append(buff);
                    int_res_mant.Insert(0, buff);
                }
            }

            // provede soucet celociselne casti
            for (int i = buff_s1.Length-1; i >= 0; i--)
            {
                buff = SumOneChar(buff_s1[i], buff_s2[i], go_next);

                if (buff.Length > 1)
                {
                    go_next = 1;
                    //int_res.Append(buff[0]);
                    int_res.Insert(0, buff[1]);
                }
                else
                {
                    go_next = 0;
                    //int_res.Append(buff);
                    int_res.Insert(0, buff);
                }
            }
            if (go_next == 1)
                int_res.Insert(0, "1");
            if (zaporny)
                int_res.Insert(0, "-");


            // doplnit omezeni desetinnych mist

            if (int_res_mant.Length == 0)
                return int_res.ToString();
            else
                return int_res.ToString() + "." + int_res_mant.ToString();
        }

        // secte c1 s c2
        private static string SumOneChar(char c1, char c2, int go_next)
        {
            int res = Int32.Parse(c1.ToString()) + Int32.Parse(c2.ToString()) + go_next;
            return res.ToString();
        }

        public static string DesumString(string s1, string s2, bool convert = true)
        {
            if (convert)
            {
                if (IsInteger(s1) && IsInteger(s2))
                {
                    
                    if (s1.Length < 17 && s2.Length < 17)
                        return (long.Parse(s1) + long.Parse(s2)).ToString();

                }
                /*else
                {
                    if (s1.Length < 18 && s2.Length < 18)
                        return (double.Parse(s1) + double.Parse(s2)).ToString();
                }*/
            }
            int point_position_s1 = -1,
                point_position_s2 = -1;
            string buff_s1 = s1,
                buff_s2 = s2,
                mant_s1 = "",
                mant_s2 = "";
            string zn = "";

            if (s1[0] == '-' && s2[0] == '-')
                return SumString(s1, s2);
            else if (s1[0] == '-' || s2[0] == '-')
            {
                if (s2[0] == '-')
                {
                    s2 = s2.Remove(0, 1);
                    buff_s1 = s2;
                    buff_s2 = s1;
                }
            }

            if (EqualString("=", s1, s2))
                return "0";
            if (EqualString("<", s1, s2))
                zn = "-";

            // rozdeleni desetinnecasti od normalni
            if (TestStringAnFloat(s1))
            {
                point_position_s1 = DeletePoint(ref buff_s1, ref mant_s1);
            }
            if (TestStringAnFloat(s2))
            {
                point_position_s2 = DeletePoint(ref buff_s2, ref mant_s2);
            }
            // pripoji nuly
            AppendZero(ref buff_s1, ref buff_s2, true);
            AppendZero(ref mant_s1, ref mant_s2, false);

            //pomocne promenne k souctu
            string buff;
            int go_next = 0;
            StringBuilder int_res = new StringBuilder();
            StringBuilder int_res_mant = new StringBuilder();

            // provede soucet mantisy
            for (int i = mant_s1.Length - 1; i >= 0; i--)
            {
                buff = DesumOneChar(mant_s1[i], mant_s2[i], go_next);

                if (buff.Length > 1)
                {
                    go_next = 1;
                    //int_res_mant.Append(buff[1]);
                    int_res_mant.Insert(0, buff[1]);
                }
                else
                {
                    go_next = 0;
                    //int_res_mant.Append(buff);
                    int_res_mant.Insert(0, buff);
                }
            }

            // provede soucet celociselne casti
            for (int i = buff_s1.Length - 1; i >= 0; i--)
            {
                buff = DesumOneChar(buff_s1[i], buff_s2[i], go_next);

                if (buff.Length > 1)
                {
                    go_next = 1;
                    //int_res.Append(buff[0]);
                    int_res.Insert(0, buff[1]);
                }
                else
                {
                    go_next = 0;
                    //int_res.Append(buff);
                    int_res.Insert(0, buff);
                }
            }
            if (go_next == 1)
                int_res.Insert(0, "1");


            // doplnit omezeni desetinnych mist

            if (int_res_mant.Length == 0)
                return CleanString( zn + int_res.ToString());
            else
                return CleanString(zn + int_res.ToString() + "." + int_res_mant.ToString());
        }
        
        private static string DesumOneChar(char c1, char c2, int go_next)
        {
            string cs1 = c1.ToString();
            int res = 0;
            if (EqualString("<", cs1, c2.ToString()) || (EqualString("==", "0", c2.ToString()) && EqualString("==", "0", cs1)) )
            {
                cs1 = cs1.Insert(0, "1");
                res = 10;
            }
            res += Int32.Parse(cs1) - (Int32.Parse(c2.ToString()) + go_next);

            return res.ToString();
        }

        public static string ProductString(string s1, string s2, bool convert = true)
        {
            if (convert)
            {
                if (IsInteger(s1) && IsInteger(s2))
                {
                    if (s1[0] == '-' || s2[0] == '-')
                    {
                        if (s1.Length < 6 && s2.Length < 6)
                            return (long.Parse(s1) * long.Parse(s2)).ToString();
                    }
                    else
                    {
                        //ulong z = 18446744073709551615 / 18 446 744 073 709 551 615;

                        if (s1.Length < 11 && s2.Length < 11)
                            return (ulong.Parse(s1) * ulong.Parse(s2)).ToString();
                    }
                }
            }

            if (EqualString(s2, "1") == 0)
                return s1;
            if (EqualString(s1, "1") == 0)
                return s2;
            if ((EqualString(s2, "0") == 0) || (EqualString(s1, "0") == 0))
                return "0";

            
            bool zaporny = false;

            if (s1[0] == '-' && s2[0] == '-')
            {
                s1 = s1.Remove(0, 1);
                s2 = s2.Remove(0, 1);
            }
            else if (s1[0] == '-' || s2[0] == '-')
            {
                if (s2[0] == '-')
                    s2 = s2.Remove(0, 1);
                else s1 = s1.Remove(0, 1);

                zaporny = true;
            }

            string buff_s1 = Math.EqualString(s1, s2) == 1 ? s1 : s2,
                buff_s2 = Math.EqualString(s1, s2) == 1 ? s2 : s1;
            int count_mant = 0;

            // odstraneni tecky a nastaveni poctu desetinnych mist vysledku
            if (TestStringAnFloat(s1))
            {
                count_mant = DeletePoint(ref buff_s1);
            }
            if (TestStringAnFloat(s2))
            {
                count_mant += DeletePoint(ref buff_s2);
            }

            StringBuilder [] scitance = new StringBuilder[buff_s2.Length];
            char go_next = '0';
            string res = "";
            // provede rozdeleni na jednotlive scitance
            // jako pri rucnim deleni
            for (int i = buff_s2.Length-1, nuly = 0; i >= 0; i--, nuly++)
            {
                scitance[i] = new StringBuilder();
                for (int ii = 0; ii < nuly; ii++) scitance[i].Append("0"); // kazdy scitanec je treba doplni o spravny pocet nul
                for (int ii = buff_s1.Length - 1; ii >= 0; ii--)
                // provadi samotne nasobeni po jednotlivych elementech
                {
                    res = ProductOneChar(buff_s1[ii], buff_s2[i], go_next);
                    if (res.Length > 1)
                    {
                        if (ii == 0) // pokud ii == 0, provede vlozeni hotoveho scitance a ukonci vnitrni smycku
                        {
                            scitance[i].Insert(0, res);
                            go_next = '0';
                            break;
                        }
                        go_next = res[0];
                        scitance[i].Insert(0, res[1]);
                    }
                    else
                    {
                        go_next = '0';
                        scitance[i].Insert(0, res[0]);
                    }
                }
            }

            res = "0";
            for (int i = 0; i < buff_s2.Length; i++)
            {
                res = SumString(res, scitance[i].ToString());
            }
            if (count_mant > 0)
            {
                if (res.Length < count_mant)
                {
                    for (int i = 0; i < count_mant; i++)
                        res = res.Insert(0, "0");
                }
                
                res = res.Insert(res.Length - count_mant, ".");
            }

            if (zaporny)
                return res.Insert(0, "-");
            return res;

        }

        private static string ProductOneChar(char one, char two, char go_next)
        {
            return (Int32.Parse(one.ToString()) * Int32.Parse(two.ToString()) + Int32.Parse(go_next.ToString())).ToString();
        }


        public static string DivisionString(string s1, string s2)
        {
            if (s2 == "0")
            {
                //return "ERROR:You can't divide by zero";
                throw new DivideByZeroException("Pokus o dělení nulou.\nYou can't divide by zero.");
            }
            if (s2 == "1")
                return s1;
            if (s1 == "0")
            {
                return "0";
            }
            return Math.DivisionStringBETA(s1, s2);
            /*if (s2 == "0")
                return "#err:1 - deleni nulou";
            if (s2 == "1")
                return s1;

            return "";*/
        }


        
        private static string DivisonStringZbytek(string zbytek, string delitel)
        {
            StringBuilder mantisa = new StringBuilder();
            string zb = zbytek;

            for (int i = 0; i < OmezeniDesetinnychMist; i++)
            {
                if (EqualString("=", zb, "0"))
                    break;

                zb = ProductString(zb, "10");
                mantisa.Append(IntegerDivision(zb, delitel));
                zb = Modulo(zb, delitel);

            }

            return mantisa.ToString();

        }

        public static string Modulo(string s1, string s2)
        {
            string podil = IntegerDivision(s1, s2);

            string soucin = ProductString(podil, s2);

            return DesumString(s1, soucin);
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



        private static string IntegerDivision(string s1, string s2)
        {
            string num = "0";
            int i = 0;

            for (i = 0; EqualString("<=", num, s1); i++)
            {
                num = SumString(num, s2);
            }

            if (EqualString(">", num, s1)) i--;

            return i.ToString();
        }


        public static bool EqualString(string znamenko, string s1, string s2)
        {
            int ret, ret2;

            if (znamenko == "<")
                ret = ret2 = -1;
            else if (znamenko == ">")
                ret = ret2 = 1;
            else if (znamenko == ">=")
            {
                ret = 1;
                ret2 = 0;
            }
            else if (znamenko == "<=")
            {
                ret = -1;
                ret2 = 0;
            }
            else if (znamenko == "!=")
            {
                ret = 1;
                ret2 = -1;
            }
            else if (znamenko == "==" || znamenko == "=")
            {
                ret = ret2 = 0;
            }
            else
                return false;
            //else ret = ret2 = 0;

            if (EqualString(s1, s2) == ret || EqualString(s1, s2) == ret2)
                return true;

            return false;
        }

        /*
        * Tato metoda porovnava dva string
        * vraci 
        * 1 - pokud je prvni větší než druhy
        * 0 - pokud jsou stejne
        * -1 - pokud je druhy vetsi nez prvni
        * -2 - spatny vstupni format
        * */

        /// <summary>
        /// This method is compare two strings.
        /// </summary>
        /// <param name="s1"></param>
        /// <param name="s2"></param>
        /// <returns>If input1 greater input2 = 1
        /// if input2 greater input1 = -1
        /// if input1 equal input2 = 0</returns>
        public static int EqualString(string s1, string s2)
        {
            string is1 = s1, is1_mant = "",
                 is2   = s2, is2_mant = "";
            int delka  = 0;
            bool otoc  = false;

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

            if (is1[0] == '-' && is2[0] == '-')
            {
                otoc = true;
            }
            else if (is1[0] == '-' || is2[0] == '-')
            {
                if (is1[0] == '-')
                    return -1;
                else
                    return 1;
            }

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
                        if (otoc)
                            return -1;
                        else
                            return 1;
                    else if (Int32.Parse(is1[i].ToString()) < Int32.Parse(is2[i].ToString()))
                        if (!otoc)
                            return -1;
                        else
                            return 1;
                }

                for (int i = 0; i < delka; i++)
                {
                    if (Int32.Parse(is1_mant[i].ToString()) > Int32.Parse(is2_mant[i].ToString()))
                        if (otoc)
                            return -1;
                        else
                            return 1;
                    else if (Int32.Parse(is1_mant[i].ToString()) < Int32.Parse(is2_mant[i].ToString()))
                        if (!otoc)
                            return -1;
                        else
                            return 1;
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

        private static bool DetectPoint(string input)
        {
            if (input.IndexOf('.') >= 0)
                return true;
            return false;
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

        // stejna jako predchozi akorat pouze odstrani tecku a vraci string vcelku
        // vraci pocet desetinnych mist
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
            /*int i = input.IndexOf('.');
            if (i >= 0)
                input = input.Remove(i, 1);
            return i >= 0 ? i : 0;*/

            return 0;
        }

        private static bool TestStringAnFloat(string s1)
        {
            int carka = 0;

            for (int i = 0; i < s1.Length; i++)
            {
                if (!char.IsDigit(s1, i) && s1[i] != '.' && s1[i] != '-')
                    return false;

                if (s1[i] == '.')
                {
                    if (++carka > 1)
                    {
                        return false;
                    }
                }
            }

            return true;
        }

        public static bool IsDegree(string input)
        {
            StringBuilder input2 = new StringBuilder();
            for (int i = 0; i < input.Length; i++)
            {
                if (input[i] == ' ' || input[i] == '\t')
                    continue;
                else
                    input2.Append(input[i]);

            }
            Regex deg = new Regex("^([0-9]+)°{1}([0-9]+)'{1}?([0-9]+)\"{1}?");

            if (deg.IsMatch(input))
                return true;
            else return false;
        }

        /// <summary>
        /// Vrati true, pokud je vstupni hodnota cele cislo.
        /// </summary>
        /// <param name="num"></param>
        /// <returns></returns>
        public static bool IsInteger(string num)
        {
            //int carka = 0;

            for (int i = 0; i < num.Length; i++)
            {
                if (num[i] == '.' || num[i] == '/')
                    return false;
            }

            return true;
        }

        //sudost
        public static bool GetEven(string input, bool convert = true)
        {
            if (convert)
            {
                if (EqualString (">=", "18446744073709551615", input))
                {
                    if (ulong.Parse(input) % 2 == 0)
                        return true;
                    else return false;
                }
                else if (EqualString("<=", "9223372036854775807", input))
                    if (long.Parse(input) % 2 == 0)
                        return true;
                    else return false;
            }

            if (Modulo(input, "2") == "0")
                return true;
            else return false;

        }

        public static string IntegerExpon(string zaklad, string exponent)
        {
            string ret = zaklad;
            bool zaporny = false;

            if (zaklad == "-1")
            {
                if (GetEven(exponent))
                    return "1";
                else
                    return "-1";
            }

            if (exponent[0] == '-')
            {
                zaporny = true;
                exponent = exponent.Remove(0, 1);
            }

            if (EqualString(exponent, "0") == 0)
                return "1";

            if (EqualString(exponent, "1") == 0)
                return zaklad;
            
            if (EqualString(">", "9223372036854775807", exponent))
            {
                for (int i = 0; i < Int64.Parse(ReturnInteger(exponent)) - 1; i++)
                {
                    ret = ProductString(ret, zaklad);
                }
            }
            else
            {
                for (string i = "0"; EqualString("<", i, DesumString(exponent, "1")); i = SumString(i, "1"))
                {
                    ret = ProductString(ret, zaklad);
                }
            }

            if (zaporny)
                return "1/" + ret;
            return ret;
        }

        public static string DegreeToNumber(string degree)
        {
            //int index_h = degree.IndexOf('°');
            //int index_m = degree.IndexOf('\'');
            //int index_s = degree.IndexOf('"');
            string h = "0";
            string m = "0";
            string s = "0";
            //if (index_h == -1)
            //    h = "0";
            //else
            //    h = degree.Remove(degree.IndexOf('°'));
            //if (index_m == -1)
            //    m = "0";
            //else
            //{
            //    if (index_s == -1)
            //    {
            //        m = degree.Remove(0, index_h + 1);
            //        m = m.Remove(m.IndexOf('"'), 1);
            //    }
            //    else
            //    {
            //        m = degree.Remove(0, index_h + 1);
            //        int i = m.IndexOf('\'');
            //        m = m.Remove(i, m.Length - i);
            //    }
            //    m = DivisionString(m, "60");

            //}
            //if (index_s == -1)
            //    s = "0";
            //else
            //{
            //    s = degree.Remove(0, index_m+1);
            //    s = DivisionString(s.Remove(s.IndexOf('"'), 1), "3600");


            //}

            Regex deg = new Regex("^([0-9]+)°{1}([0-9]+)'{1}?([0-9]+)\"{1}?");
            int i = -1;
            foreach (string degs in deg.Split(degree))
            {
                if (i == 0)
                    h = degs;
                else if (i == 1)
                    m = DivisionString(degs, "60");
                else if (i == 2)
                    s = DivisionString(degs, "3600");
                i++;
            }

            return SumString(h, SumString(m, s));
        }

        public static string NumberToDegree(string number, bool absolut = false)
        {
            string h = ReturnInteger(number);
            string mant = ReturnDecimal(number);
            string m = ProductString(mant[0].ToString(), "6");
            StringBuilder nasobek = new StringBuilder("0.6");
            for (int i = 1; i < mant.Length; i++)
            {
                m = SumString(m, ProductString(mant[i].ToString(), nasobek.ToString()));
                nasobek.Insert(2, "0");
            }
            mant = ReturnDecimal(m);
            string s = ProductString(mant[0].ToString(), "6");
            nasobek.Remove(0, nasobek.Length);
            nasobek.Insert(0, "0.6");
            for (int i = 1; i < mant.Length; i++)
            {
                s = SumString(s, ProductString(mant[i].ToString(), nasobek.ToString()));
                nasobek.Insert(2, "0");
            }

            return String.Format("{0}°{1}'{2}\"", h, ReturnInteger( m), String.Format("{0}.{1}", ReturnInteger(s), (absolut?ReturnDecimal(s):ReturnDecimal(s).Remove(2))));

        }

        public static string DegreeToRadian(string degree)
        {
            return DivisionString(ProductString(DegreeToNumber(degree), pi.Remove(OmezeniDesetinnychMist+1)), "180");
        }

        public static string RadianToDegree(string rad)
        {
            return NumberToDegree(DivisionString(ProductString(rad,"180"), pi.Remove(OmezeniDesetinnychMist+1)));
        }

        public static string DegreeToGrad(string degree)
        {
            return DivisionString(ProductString(DegreeToNumber(degree), pi.Remove(OmezeniDesetinnychMist+1)), "200");
        }
        

        public static class Logic
        {
            public static bool IsBinaryNumber(string input)
            {
                for (int i = 0; i < input.Length; i++)
                {
                    if (input[i] != '0' || input[i] != '1')
                        return false;
                }

                return true;
            }

            public static string LogicProductString(string num1, string num2)
            {

                return "0";
            }

            public static string LogicSumString(string num1, string num2)
            {

                return "0";
            }

            public static string ConvertTo(string code, string number10)
            {
                //code: bcd

                return number10;
            }

        }

        public static class Exponential
        {
            public static string e(string input)
            {
                if (input.Length < 18)
                {
                    return System.Math.Exp(double.Parse(input)).ToString().Replace(",", ".");
                }

                string res = "0";
                //string buf_fakt = "0";
                //string buf_div = "0";

                for (int i = 0; i < 25; i++)
                {
                    // buf_fakt = Fakt(i.ToString());
                    // buf_div = DivisionStringBETA(Expon(input, i.ToString()), buf_fakt);
                    //buf_div = DivisionStringBETA(Expon(input, i.ToString()), Fakt(i.ToString()) );

                    //                res = SumString(res, buf_div);

                    //res = SumString(res, DivisionStringBETA(Expon(input, i.ToString()), Fakt(i.ToString()) ));

                    //debug
                    //res = DivisionStringBETA(Expon(input, i.ToString()), Fakt(i.ToString()));

                    res = Math_v2.SumString(res, Math.DivisionStringBETA(IntegerExpon(input, i.ToString()), Fakt(i.ToString())));

                }

                return res;
            }
        }

        public static class NumericMethod
        {
            public static string Solve(string input)
            {
                return "0";
            }
        }

        public static class Logaritmic
        {
            public static string ln(string input)
            {
                string res = "1";
                //string buf_fakt = "0";
                //string buf_div = "0";
               /* if (OmezeniDesetinnychMist < 13)
                {
                    double ln = System.Math.Log(double.Parse(input));
                    return String.Format("{0}", ln).Remove(OmezeniDesetinnychMist+DeletePoint(ref (string ln_s = ln.ToString())));
                }*/
                if (input.Length < 18)
                {
                    return System.Math.Log(double.Parse(input)).ToString().Replace(",",".");
                }

                string t = "1";
                string y = DivisionString(DesumString(input, "1"), SumString(input, "1"));
                
                for (int i = 1; i < 5; i++)
                {
                    t = DivisionString(IntegerExpon(y, (2 * i - 1).ToString()), (2 * i - 1).ToString());
                    res = SumString(t, res);

                }

                return DesumString(ProductString("2", res), "2");

                
            }
        }

        public static class Matrix
        {
            public static string[,] MatrixProductString(string[,] op1, string[,] op2)
            {
                if (op1.GetLength(0) != op2.GetLength(1))
                {
                    return null;
                }

                int radky1    = op1.GetLength(0);
                int sloupce1  = op1.GetLength(1);
                int sloupce2  = op2.GetLength(1);
                string[,] ret = new string[radky1, sloupce2];

                for (int i = 0; i < radky1; i++)
                {
                    for (int j = 0; j < sloupce2; j++)
                    {
                        ret[i, j] = "0";
                        for (int z = 0; z < op2.GetLength(0); z++)
                        {
                            ret[i,j] = SumString(ret[i,j], ProductString(op1[i, z], op2[z, j]));
                        }
                    }
                }
                

                return ret;
            }

            public static string[,] MatrixProductString(string[,] op1, string op2)
            {
                int radky1 = op1.GetLength(0);
                int sloupce1 = op1.GetLength(1);
                string[,] ret = new string[radky1, sloupce1];

                for (int i = 0; i < radky1; i++)
                {
                    for (int j = 0; j < sloupce1; j++)
                    {
                        ret[i, j] = "0";
                        ret[i, j] = ProductString(op1[i, j], op2);
                    }
                }


                return ret;
            }
        }


        public static class Trigonometric
        {
            public static string Sin(string input)
            {
                if (input == "90")
                    return "1";
                else if (input == "180")
                    return "0";
                else if (input == "270")
                    return "-1";
                else if (input == "360")
                    return "0";

                if (input.Length < 18)
                {
                    return System.Math.Sin(double.Parse(input, new CultureInfo("en-US"))).ToString().Replace(",", ".");
                }

                string res = "0";
                
                // i use taylor series for aproximate function sinus
                // (-1)^n*(x^(2n-1)/(2n-1)!

                for (int i = 0; i < 10; i++)
                {
                    res = SumString(res, ProductString(IntegerExpon("-1", i.ToString()), DivisionString(IntegerExpon(input, (2 * i - 1).ToString()), Fakt((2 * i - 1).ToString()))));
                }

                return res;
            }

            public static string Arcsin(string input)
            {
                if (input == "1")
                    return "90";
                else if (input == "0")
                    return "0";
                else if (input == "-1")
                    return "-90";

                if (input.Length < 18)
                {
                    return System.Math.Asin(double.Parse(input)).ToString().Replace(",", ".");
                }


                return "";
            }

            public static string Cos(string input)
            {
                if (input.Length < 18)
                {
                    return System.Math.Cos(double.Parse(input)).ToString().Replace(",", ".");
                }

                return "0";
            }

            public static string Arccos (string input)
            {
                if (input.Length < 18)
                {
                    return System.Math.Acos(double.Parse(input)).ToString().Replace(",", ".");
                }
                return "0";
            }

            public static string Tan(string input)
            {
                if (input.Length < 18)
                {
                    return System.Math.Tan(double.Parse(input)).ToString().Replace(",", ".");
                }
                return "0";
            }

            public static string Arctan(string input)
            {
                if (input.Length < 18)
                {
                    return System.Math.Atan(double.Parse(input)).ToString().Replace(",", ".");
                }
                return "0";
            }

        }

    }
}
