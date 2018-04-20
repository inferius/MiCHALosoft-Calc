//#define DEBUG

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

/*
 * Math.cs
 * 
 * Třida pro matematické operace s řetezci
 * 
 * verze : 1.1
 * 
 * Michal Diviš
 * 
 * seznam metod pro praci s retezci jako s cisly:
 * static string SumString(string s1, string s2) - secte dva retezce
 * static string DesumString(string s1, string s2) - odecte dva retezce
 * static string ProductString(string s1, string s2) - vynasobi dva retezce
 * static string Fakt(string input) - rekursivni faktorial
 * static string Faktorial(string input) - iteracni faktorial
 * 
 * Legenda:
 * + = pridano
 * - = zrušeno
 * $ = opraveno
 * 
 * Zmeny:
 * 1.1
 * $ optimalizace procesu soucinu, souctu, rozdilu
 * $ opraveny chyby v rozdilu, souctu i nasobeni, ktere pusobily chybne vysledky
 * + pridan podil retezcu (zatim nefunguje ve vsech pripadech)
 * + pridana mocnina (zatim nefunguje u racionalniho exponentu)
 * 
 * */

namespace MiCHALosoft_CALC
{
    class Math
    {
        const int OmezeniDesetinnychMist = 10;      //zadejte pocet vypisovanych desetinnyc mist
        const string pi = "3.141592653589793238462643383279502884197169399375105820974944592307816406286208998628034825342117067982148086513282306647093844609550582231725359408128481117450284102701938521105559644622948954930381964428810975665933446128475648233786783165271201909145648566923460348610454326648213393607260249141273724587006606315588174881520920962829254091715364367892590360011330530548820466521384146951941511609433057270365759591953092186117381932611793105118548074462379962749567351885752724891227938183011949129833673362440656643086021394946395224737190702179860943702770539217176293176752384674818467669";
        const string e = "2.7182818284590452353602874713526624977572470936999595749669676277240766303535475945713821785251664274274663919320030599218174135966290435729003342952605956307381323286279434907632338298807531952510190115738341879307021540891499348841675092447614606680822648001684774118537423454424371075390777449920695517027618386062613313845830007520449338265602976067371132007093287091274437470472306969772093101416928368190255151086574637721112523897844250569536967707854499699679468644549059879316368892300987931277361782154249992295763514822082698951936680331825288693984964651058209392398294887933203625094431173012381970684161403970198376793206832823764648042953118023287825098194558153017567173613320698112509961818815930416903515988885193458072738667385894228792284998920868058257492796104841984443634632449684875602336248270419786232090021609902353043699418491463140934317381436405462531520961836908887070167683964243781405927145635490613031072085103837505101157477041718986106873969655212671546889570350";

        

        public static string SumString(string s1, string s2)
        {
            if (EqualString(s1, "0") == 0)
                return s2;
            if (EqualString(s2, "0") == 0)
                return s1;

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
                if (mant_res.Length > OmezeniDesetinnychMist)
                    mant_res.Remove(0, mant_res.Length - OmezeniDesetinnychMist);
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
            int res = Int16.Parse(one[index_one].ToString()) * Int16.Parse(two[index_two].ToString());

            res += Int16.Parse(slo_dal);

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
            if (EqualString(s1, "0") == 0)
                return s2 = s2.Insert(0, "-");
            if (EqualString(s2, "0") == 0)
                return s1;

            int TotalLength = 0,
                MinLength = 0;
            string is1 = s1,
                is2 = s2, buff, buff2 = "", go_next = "0", znamenko = "",
                is1_mant = "",
                is2_mant = "";
            int des_tec = -1,
                des_tec2 = -1;

            //des_tec = DeletePoint(ref is1, ref is1_mant);
            //des_tec2 = DeletePoint(ref is2, ref is2_mant);

            /*if (des_tec != -1 || des_tec2 != -1)
                if (des_tec < des_tec2)
                    des_tec = des_tec2;*/

            if (DetectPoint(is1))
            {
                des_tec = DeletePoint(ref is1, ref is1_mant);
                //is1 = is1 + is1_mant;
            }
            if (DetectPoint(is2))
            {
                if ((des_tec2 = DeletePoint(ref is2, ref is2_mant)) > des_tec)
                    des_tec = des_tec2;
                //is2 = is2 + is2_mant;
                //des_tec += des_tec2;
            }


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
                    result.Append(buff2);
                }

                else if (i == 0 && buff.Length > 1)
                {
                    result.Append(buff2);
                    result.Append(go_next);
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
                if ((b_ret.Length - result.Length - 1) > OmezeniDesetinnychMist)
                {
                    b_ret.Remove(OmezeniDesetinnychMist, (b_ret.Length - result.Length - 1) - OmezeniDesetinnychMist);
                }

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
            else ret = ret2 = 0;

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

        private static bool DetectPoint(string input)
        {
            for (int i = 0; i < input.Length; i++)
                if (input[i] == '.')
                    return true;

            return false;
        }

        public static string ProductString(string s1, string s2)
        {
            if (EqualString(s2, "1") == 0)
                return s1;
            if (EqualString(s2, "0") == 0)
                return "0";

            string is1 = Math.EqualString(s1, s2) == 1 ? s1 : s2,
                is2 = Math.EqualString(s1, s2) == 1 ? s2 : s1, 
                buff, buff2 = "", go_next = "0", znamenko = "",
                is1_mant = "",
                is2_mant = "";
            int des_tec = 0,
                des_tec2 = 0;

            

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

            if (DetectPoint(is1))
            {
                des_tec = DeletePoint(ref is1, ref is1_mant);
                is1 = is1 + is1_mant;
            }
            if (DetectPoint(is2))
            {
                des_tec2 = DeletePoint(ref is2, ref is2_mant);
                is2 = is2 + is2_mant;
                des_tec += des_tec2;
            }


            StringBuilder result = new StringBuilder();
            //StringBuilder mant_res = new StringBuilder();

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
            string res_s = "0";

            for (int i = 0; i < soucty.Count; i++)
            {
                res_s = SumString(soucty[i], res_s);
            }


            if (des_tec != 0)
            {
                //return AddPoint(ReverseString(result.ToString()), null, des_tec, 3);
                res_s = res_s.Insert(res_s.Length - des_tec, ".");
            }

            //odstrani nuly ze zacatku u nekterych vysledku
            for (int i = 0; i < res_s.Length; i++)
            {
                if (res_s[i] == '0' && res_s[i + 1] == '.')
                    break;
                else if (res_s[i] != '0')
                    break;
                else
                {
                    res_s = res_s.Remove(0, 1);
                    i = 0;
                }
            }

            if ((res_s.Length - (res_s.Length - des_tec) - 1) > OmezeniDesetinnychMist)
                res_s = res_s.Remove((res_s.Length - des_tec) + OmezeniDesetinnychMist);

            if (znamenko == "-")
            {
                res_s.Insert(0, "-");
            }

            /*for (int i = res_s.Length - 1; i >= 0; i--)
            {
                if (res_s[i] == '0')
                {
                    if (res_s[i - 1] == '.')
                        res_s = res_s.Remove(i - 1, 2);
                    else
                        res_s = res_s.Remove(i, 1);

                    i = res_s.Length;
                    continue;
                }

                break;
            }*/

            return CleanString(res_s.ToString());
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

        public static string DivisionString(string s1, string s2)
        {
            return CleanString(DivisionString(s1, s2, 0));
        }

        private static string DivisionString(string s1, string s2, int n)
        {
            if (n == OmezeniDesetinnychMist)
                return "";

            if (EqualString(s2, "0") == 0)
                return "#err:1";        //1 - pokus o deleni nulou

            if (EqualString(s2, "1") == 0)
                return s1;

            string is1 = s1,//Math.EqualString(s1, s2) == 1 ? s1 : s2,
                is2 = s2,//Math.EqualString(s1, s2) == 1 ? s2 : s1,
                buff, //znamenko = "",
                is1_mant = "",
                is2_mant = "";
            int des_tec = 0,
                des_tec2 = 0;

            bool mensi = false;

            if (EqualString(is1, is2) == -1)
            {
                is1 = s2;
                is2 = s1;

                mensi = true;
            }

            /*if (s1[0] == '-' && s2[0] == '-')
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
            }*/

            if (DetectPoint(is1))
            {
                des_tec = DeletePoint(ref is1, ref is1_mant);
                is1 = is1 + is1_mant;
            }
            if (DetectPoint(is2))
            {
                des_tec2 = DeletePoint(ref is2, ref is2_mant);
                is2 = is2 + is2_mant;
                des_tec += des_tec2;
            }

            buff = "0";
            string main_del = "",
                zbytek = "";
                

            for (int i = 0; ; i++)
            {
                buff = SumString(buff, is2);

                main_del = (i + 1).ToString();

                if (Math.EqualString(buff, is1) == 0)
                {
                    if (mensi)
                    {
                        StringBuilder nasobek_deseti = new StringBuilder("1");
                        StringBuilder nuly = new StringBuilder();
                        for (int ii = 0; ii < main_del.Length; ii++)
                        {
                            nasobek_deseti.Append("0");
                            if (ii > 0)
                                nuly.Append("0");

                        }

                        zbytek += DivisionString(nasobek_deseti.ToString(), (i+1).ToString(), n + 1);

                        return "0" + "." + nuly.ToString() + zbytek;
                    }
                    return main_del;
                }
                else if (Math.EqualString(buff, is1) == 1)
                {
                    main_del = i.ToString();
                    zbytek = main_del;
                    //zbytek = ProductString(DesumString(is1, ProductString(main_del, is2)), "100");
                    //procento = DivisionString(zbytek, is2);
                    //if (n == 0)
                    //    zbytek += DivisionString(ProductString(DesumString(s1, ProductString(main_del, s2)), "100"), s2, n + 1);

                    //for(int s = 0; s < OmezeniDesetinnychMist; s++)
                    zbytek += DivisionString(ProductString(DesumString(s1, ProductString(zbytek, s2)), "100"), s2, n + 1);

                    if (mensi)
                    {
                        StringBuilder nasobek_deseti = new StringBuilder("1");
                        StringBuilder nuly = new StringBuilder();

                        
                        for (int ii = 0; ii < main_del.Length; ii++)
                        {
                            nasobek_deseti.Append("0");
                            if (main_del.Length > 1)
                                nuly.Append("0");
                        }
                        

                        zbytek += DivisionString(nasobek_deseti.ToString(), i.ToString(), n + 1);

                        return "0" + "." + nuly.ToString() + zbytek;
                    }

                    if (n == 0)
                    {
                        zbytek = zbytek.Remove(0, main_del.Length);
                        if (zbytek.Length > OmezeniDesetinnychMist)
                            return main_del + "." + zbytek.Remove(OmezeniDesetinnychMist);
                        else
                            return main_del + "." + zbytek;
                    }
                    else
                        return zbytek;
                }
            }
        }


        public static string Expon(string zaklad, string exponent)
        {
            string ret = zaklad;
            bool zaporny = false;

            if (exponent[0] == '-')
                zaporny = true;

            if (EqualString(exponent, "0") == 0)
                return "1";

            if (EqualString(exponent, "1") == 0)
                return zaklad;

            for (int i = 0; i < Int64.Parse(exponent)-1; i++)
            {
                ret = ProductString(ret, zaklad);
            }

            return ret;
        }

        /// <summary>
        /// Prevede cislo na podobu zlomku
        /// </summary>
        /// <param name="num_up">citatel</param>
        /// <param name="num_down">jmenovatel</param>
        /// <returns></returns>
        public static string ConvertToRational(string num_up, string num_down)
        {
            string buf;            

            if (EqualString(num_up, num_down) == 1)
            {
                string buf2;

                buf = DivisionString(num_up, num_down, 0);
                //buf = DivisionStringBETA(num_up, num_down);

                buf2 = DesumString(num_up, ProductString(ReturnInteger(buf), num_down));

                if (EqualString(buf2, "0") == 0)
                {
                    return buf;
                }

                return ReturnInteger(buf) + "/" + buf2 + "/" + num_down;

            }

            buf = DivisionString(num_down, num_up, 0);
            //buf = DivisionStringBETA(num_down, num_up);

            if (IsInteger(buf))
                return "1/" + buf;
            else
                return num_up + "/" + num_down;

        }

        public static string ConvertToRational(string num)
        {
            string dec = ReturnDecimal(num);
            string del = ReturnDec(num.Length);
            string jmen = DivisionString(del, dec);

            

            return ConvertToRational(ReturnInteger(num), jmen) + "Calc Error";
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
                return ret.ToString();
            else
                return "";
        }

        private static string ReturnDec(int pocet)
        {
            StringBuilder ret = new StringBuilder("1");

            for (int i = 0; i < pocet; i++)
                ret.Append("0");

            return ret.ToString();

        }



        private static string CleanString(string vstup)
        {
            bool cislo = false,
                tecka = false;
            StringBuilder input = new StringBuilder(vstup);

            for (int i = 0; i < input.Length; i++)
            {
                if (input[i] != '0')
                    cislo = true;

                

                if (input[i] == '.')
                {
                    for (int ii = input.Length-1; ii > 0; ii--)
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

                if (input[i] == '0' && (input.Length == i + 1 ? true : input[i + 1] != '.') && !cislo)
                {
                    input.Remove(i, 1);
                    if (i >= input.Length)
                        break;
                }
            }

            return input.ToString();
        }

        public static string DivisionStringBETA(string s1, string s2)
        {
            if (s2 == "0")
                return "#err:1";
            if (s2 == "1")
                return s1;

            if (EqualString(">", s1, s2))
            {
                string num = "0";
                int i = 0, ii = 0;

                for (i = 0; EqualString("<=", num, s1); i++)
                {
                    num = SumString(num, s2);
                }
                i--;
                num = DesumString(num, s2);

                if (EqualString("=", num, s1))
                {
                    return i.ToString();
                }
                else
                {
                    string zbytek = DesumString(s1, ProductString(i.ToString(), s2));
                    /*string ret = "0";

                    for (ii = 0; EqualString("<=", ret, s2); ii++)
                    {
                        ret = SumString(ret, zbytek);
                    }
                    //ii--;
                    //ret = DesumString(ret, zbytek);*/

                    return i.ToString() + "/" + ConvertToRational(zbytek, s2); //zbytek + "/" + s2; //ii.ToString();
                }
            }

            return "#err: 0";
                

        }




    }
}
                    