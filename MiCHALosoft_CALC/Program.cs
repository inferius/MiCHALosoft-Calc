using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Diagnostics;
using System.Numerics;

namespace MiCHALosoft_CALC
{
    class Program
    {
        static Process _currentProcess = Process.GetCurrentProcess();
        static void Main(string[] args)
        {       
            Process currentProcess = Process.GetCurrentProcess();


            //Console.Write("{0}\n", Math.SumString("356", "658"));
            //Console.Write("{0}\n", Math.DesumString("356", "658"));
            //Console.Write("{0}\n", Math.DesumString("1100", "1092"));
            //Console.Write("{0}\n", Math.SumString("21.5", "23.18"));
            //Console.Write("{0}\n", Math.DesumString("2.1250", "8"));
            //Console.Write("{0}\n", Math.DesumString("0", "8"));
            //Console.Write("{0}\n", Math.DivisionStringBETA("2", ".5"));
            /*Console.Write("{0}\n\n",Math.ProductString("112000321233212112000321233212112000321233212112000321233212112000321233212112000321233212112000321233212112000321233212112000321233212112000321233212112000321233212112000321233212112000321233212112000321233212112000321233212112000321233212112000321233212112000321233212112000321233212112000321233212112000321233212",
                    "32123321211200032123321211200032123321321233212112000321233212112000321233213212332121120003212332121120003212332132123321211200032123321211200032123321112000321233212112000321233212112000321233212112000321233212112000321233212112000321233212112000321233212112000321233212112000321233212112000321233212112000321233212112000321233212112000321233212112000321233212"));

            Console.Write("{0}\n", Math.ProductStringBETA("112000321233212112000321233212112000321233212112000321233212112000321233212112000321233212112000321233212112000321233212112000321233212112000321233212112000321233212112000321233212112000321233212112000321233212112000321233212112000321233212112000321233212112000321233212112000321233212112000321233212112000321233212",
                    "32123321211200032123321211200032123321321233212112000321233212112000321233213212332121120003212332121120003212332132123321211200032123321211200032123321112000321233212112000321233212112000321233212112000321233212112000321233212112000321233212112000321233212112000321233212112000321233212112000321233212112000321233212112000321233212112000321233212112000321233212"));

            Console.Write("{0}\n", Math.DivisionStringBETA("112000321233212112000321233212112000321233212112000321233212112000321233212112000321233212112000321233212112000321233212112000321233212112000321233212112000321233212112000321233212112000321233212112000321233212112000321233212112000321233212112000321233212112000321233212112000321233212112000321233212112000321233212",
                    "3212332121120003212332121120003212332132123321211200032123321211200032123321321233212112000321233212112000321233213212332121120003212332121120003212332111200032123321211200032123321211200032123321211200032123321211200032123321211200032123321211200032123321211200032123321211200032123321211200"));
             * */
            //Console.Write("{0}\n", Math.EqualString("22.55123135424", "22.5"));
            //Console.Write("{0}\n", Math.ProductString("1.352", "1000"));
            //Console.Write("{0}\n", Math.ProductString("0.2", "0.3"));
            //Console.Write("{0}\n", Math.DivisionString("1", "0.1250"));
            //Console.Write("{0}\n", Math.ConvertToRational("23", "8"));
            //Console.Write("{0}\n", Math.ConvertToRational("2.5"));
            //Console.Write("{0}\n", Math.DivisionStringBETA("1", "3"));
            //Console.Write("{0}\n", Math.ProductString("1236295", "2"));
            //Console.Write("{0}\n", Math.DesumString("2963640", "2472590"));
            //Console.Write("{0}\n", Math.DivisionStringBETA("15.32659", "12.36295")); //1,239719484
            //Console.Write("{0}\n", Math.DivisionStringBETA("12.36295121", "153.2659"));
            //Console.Write("{0}\n", Math.Sqrt("2", "900"));
            //Console.Write("[^\\\\]\".*[^\\\\]\"");
            //Console.Write("{0}\n", Math.DesumString("3.2", "3.155"));
            //Console.Write("{0}\n", Math.DesumString("3.0235294177", "0.023437863506"));
            //Console.Write("{0}\n", Math.Trigonometric.Sinus("50"));
            //Console.Write("{0}\n", Math.DivisionStringBETA(Math.pi, Math.e));
            //Console.Write("{0}\n", Math.GetPi());
            //Console.Write("{0}\n", Math.Expon("2", Math.DivisionString("1", "2", 0)));
            //Console.Write("{0}\n", Math.ProductString("112000321233212112000321233212112000321233212112000321233212112000321233212112000321233212112000321233212", Math.DesumString("11200032123321211200032123321211200032123321211200032123321211200032123321211200032123321211200032123321211200032123321211200032123321222432123166542132321", "1")));
            //Console.Write("{0}\n", Math.Fakt("1000"));
            //Console.Write("{0}\n", Math.Faktorial("1000"));
            //Console.Write("{0}\n", Math.ProductString( Math.ProductString("1000000", Math.DesumString("1000000", "1")),  Math.DesumString("1000000", "1")));
            //Console.Write("{0}\n", Math.Degree("1°63°0°"));          // - doladit
            //Console.Write("{0}\n", Math.EqualString("=", "10", "10"));
            //Console.Write("{0}\n", Math.CleanString("0100"));
            //Console.Write("{0}\n", Math.DivisionStringBETA("0.0875", Math.DivisionStringBETA("1", "100")));
            //Console.Write("{0}\n", Math.ConvertToRational("2.785"));// - doladit
            //Console.Write("{0}\n", Math.SumString("18.5", "20.8333333333333333333333333333"));
            //Console.Write("{0}\n", Math.SumString("18.50", "20.83"));
            //Console.Write("{0}\n", Math.Exponential.e("5"));// - doladit
            //Console.Write("{0}\n", Math.SumString("1.99999999999999999999999999", "0.1111111111111111111111111111"));
            //Console.Write("{0}\n", Math.SumString("1.9", "0.1"));
            //Console.Write("{0}\n", Math.DivisionStringBETA("8965.5868", "651.9855"));
            //Console.Write("{0}\n", Math_v2.SumString("39.333333333333333333333333", "26.04166666666666666666"));
            //Console.Write("{0}\n", Math.ProductString("1236295", "2"));
            //Console.Write("{0}\n", Math_v2.ProductString("1236295", "2"));

            //Console.Write("{0}\n", Math.DesumString("3.0235294177", "0.023437863506"));
            //Console.Write("{0}\n", Math_v2.DesumString("3.0235294177", "0.023437863506"));
            //Console.Write("{0}\n", Math.DivisionStringBETA("12.36295121", "153.2659"));
            //Console.Write("{0}\n", Math_v2.DivisionString("12.36295121", "153.2659"));
            //Console.Write("∞");


            //Console.Write("{0}\n", Math_v2.Trigonometric.Sin("5"));
            //ParserTree.Tree test = new ParserTree.Tree("3+(5*4+3)*5.5*(5+2*(3+3*(4+5))*(3+5))");
            //ParserTree.Tree test2 = new ParserTree.Tree("3+sin 3*(6+7)*6+8+2*6*5+7");
            //Console.WriteLine("{0}", test2.GetResult());
            //Console.WriteLine("{0}", Math_v2.DegreeToNumber("57°30'10\""));
            //Console.WriteLine("{0}", Math_v2.RadianToDegree("1.23"));
            //Console.WriteLine("{0}", Math_v2.NumberToDegree(Math_v2.RadianToDegree("1.23")));
            //Console.WriteLine("{0}", Math_v2.Trigonometric.Sin(Math_v2.DegreeToRad("57°30\"")));
            //Console.WriteLine("{0}", Math_v3.Fakt("1000"));

            MathNumber mn1 = new MathNumber("1254313210");
            MathNumber mn2 = new MathNumber("75128562");
            Console.WriteLine("{0}", mn1 + mn2);

            //Console.Write("{0}\n", test.GetResult());
            //string op1 = "1120003212332121120003212332121120003212332121.12000321233212112000321233212112000321233212112000321233212112000321233212112000112000321233212112000321233212112000321233212112000321233212112000321233212112000321233212112000321233212112000321233212112000112000321233212112000321233212112000321233212112000321233212112000321233212112000321233212112000321233212112000321233212112000112000321233212112000321233212112000321233212112000321233212112000321233212112000321233212112000321233212112000321233212112000112000321233212112000321233212112000321233212112000321233212112000321233212112000321233212112000321233212112000321233212112000112000321233212112000321233212112000321233212112000321233212112000321233212112000321233212112000321233212112000321233212112000112000321233212112000321233212112000321233212112000321233212112000321233212112000321233212112000321233212112000321233212112000112000321233212112000321233212112000321233212112000321233212112000321233212112000321233212112000321233212112000321233212112000112000321233212112000321233212112000321233212112000321233212112000321233212112000321233212112000321233212112000321233212112000112000321233212112000321233212112000321233212112000321233212112000321233212112000321233212112000321233212112000321233212112000112000321233212112000321233212112000321233212112000321233212112000321233212112000321233212112000321233212112000321233212112000112000321233212112000321233212112000321233212112000321233212112000321233212112000321233212112000321233212112000321233212112000112000321233212112000321233212112000321233212112000321233212112000321233212112000321233212112000321233212112000321233212112000112000321233212112000321233212112000321233212112000321233212112000321233212112000321233212112000321233212112000321233212112000112000321233212112000321233212112000321233212112000321233212112000321233212112000321233212112000321233212112000321233212112000112000321233212112000321233212112000321233212112000321233212112000321233212112000321233212112000321233212112000321233212112000",
            //    op2 = "1120003212332121120003212332121120003212332121.12000321233212112000321233212112000321233212112000321233212112000321233212112000112000321233212112000321233212112000321233212112000321233212112000321233212112000321233212112000321233212112000321233212112000112000321233212112000321233212112000321233212112000321233212112000321233212112000321233212112000321233212112000321233212112000112000321233212112000321233212112000321233212112000321233212112000321233212112000321233212112000321233212112000321233212112000112000321233212112000321233212112000321233212112000321233212112000321233212112000321233212112000321233212112000321233212112000112000321233212112000321233212112000321233212112000321233212112000321233212112000321233212112000321233212112000321233212112000112000321233212112000321233212112000321233212112000321233212112000321233212112000321233212112000321233212112000321233212112000112000321233212112000321233212112000321233212112000321233212112000321233212112000321233212112000321233212112000321233212112000112000321233212112000321233212112000321233212112000321233212112000321233212112000321233212112000321233212112000321233212112000112000321233212112000321233212112000321233212112000321233212112000321233212112000321233212112000321233212112000321233212112000112000321233212112000321233212112000321233212112000321233212112000321233212112000321233212112000321233212112000321233212112000112000321233212112000321233212112000321233212112000321233212112000321233212112000321233212112000321233212112000321233212112000112000321233212112000321233212112000321233212112000321233212112000321233212112000321233212112000321233212112000321233212112000112000321233212112000321233212112000321233212112000321233212112000321233212112000321233212112000321233212112000321233212112000112000321233212112000321233212112000321233212112000321233212112000321233212112000321233212112000321233212112000321233212112000112000321233212112000321233212112000321233212112000321233212112000321233212112000321233212112000321233212112000321233212112000";

            /** /string[,] op1 = { { "1", "2", "3" }, { "1", "2", "3" }, { "1", "2", "3"} , { "1", "2", "3" } },
                op2 = { { "1", "2", "3", "4" }, { "1", "2", "3", "4" }, { "1", "2", "3", "4" } };

            string [,] res = Math_v2.Matrix.MatrixProductString(op1, op2);

            for (int i = 0; i < res.GetLength(0); i++)
            {
                for (int j = 0; j < res.GetLength(1); j++)
                {
                    Console.Write("{0}\t", res[i, j]);
                }
                Console.Write("\n");
            }/**/


            //Console.Write("{0}\n", Math_v2.Ackermann("1", "3"));
            /*Console.Write("{0}\n", Math_v2.EqualString("==", Math_v3.ProductString(op1, op2), 
                                                            Math_v2.ProductString(op1, op2)));

            /*Console.Write("{0}\n", Math_v2.EqualString("==", Math_v2.ProductString(op1, op2),
                                                            Math.ProductString(op1, op2)));
            /* */
            int iy = 0;



            /*  //debugovaci merici proces
            int pocet_kroku = 3000;
            currentProcess.Refresh();
            TimeSpan start = currentProcess.TotalProcessorTime;
            for (int i = 0; i < pocet_kroku; i++)
                Math.DesumString("112000321233212112000321233212112000321233212112000321233212112000321233212112000321233212112000321233212112000321233212112000112000321233212112000321233212112000321233212112000321233212112000321233212112000321233212112000321233212112000321233212112000112000321233212112000321233212112000321233212112000321233212112000321233212112000321233212112000321233212112000321233212112000112000321233212112000321233212112000321233212112000321233212112000321233212112000321233212112000321233212112000321233212112000112000321233212112000321233212112000321233212112000321233212112000321233212112000321233212112000321233212112000321233212112000112000321233212112000321233212112000321233212112000321233212112000321233212112000321233212112000321233212112000321233212112000112000321233212112000321233212112000321233212112000321233212112000321233212112000321233212112000321233212112000321233212112000112000321233212112000321233212112000321233212112000321233212112000321233212112000321233212112000321233212112000321233212112000112000321233212112000321233212112000321233212112000321233212112000321233212112000321233212112000321233212112000321233212112000112000321233212112000321233212112000321233212112000321233212112000321233212112000321233212112000321233212112000321233212112000112000321233212112000321233212112000321233212112000321233212112000321233212112000321233212112000321233212112000321233212112000112000321233212112000321233212112000321233212112000321233212112000321233212112000321233212112000321233212112000321233212112000112000321233212112000321233212112000321233212112000321233212112000321233212112000321233212112000321233212112000321233212112000112000321233212112000321233212112000321233212112000321233212112000321233212112000321233212112000321233212112000321233212112000112000321233212112000321233212112000321233212112000321233212112000321233212112000321233212112000321233212112000321233212112000112000321233212112000321233212112000321233212112000321233212112000321233212112000321233212112000321233212112000321233212112000",
                    "112000321233212112000321233212112000321233212112000321233212112000321233212112000321233212112000321233212112000321233212112000112000321233212112000321233212112000321233212112000321233212112000321233212112000321233212112000321233212112000321233212112000112000321233212112000321233212112000321233212112000321233212112000321233212112000321233212112000321233212112000321233212112000112000321233212112000321233212112000321233212112000321233212112000321233212112000321233212112000321233212112000321233212112000112000321233212112000321233212112000321233212112000321233212112000321233212112000321233212112000321233212112000321233212112000112000321233212112000321233212112000321233212112000321233212112000321233212112000321233212112000321233212112000321233212112000112000321233212112000321233212112000321233212112000321233212112000321233212112000321233212112000321233212112000321233212112000112000321233212112000321233212112000321233212112000321233212112000321233212112000321233212112000321233212112000321233212112000112000321233212112000321233212112000321233212112000321233212112000321233212112000321233212112000321233212112000321233212112000112000321233212112000321233212112000321233212112000321233212112000321233212112000321233212112000321233212112000321233212112000112000321233212112000321233212112000321233212112000321233212112000321233212112000321233212112000321233212112000321233212112000112000321233212112000321233212112000321233212112000321233212112000321233212112000321233212112000321233212112000321233212112000112000321233212112000321233212112000321233212112000321233212112000321233212112000321233212112000321233212112000321233212112000112000321233212112000321233212112000321233212112000321233212112000321233212112000321233212112000321233212112000321233212112000112000321233212112000321233212112000321233212112000321233212112000321233212112000321233212112000321233212112000321233212112000112000321233212112000321233212112000321233212112000321233212112000321233212112000321233212112000321233212112000321233212112000");
            currentProcess.Refresh();
            double seconds = (currentProcess.TotalProcessorTime - start).TotalMilliseconds;

            currentProcess.Refresh();
            TimeSpan start2 = currentProcess.TotalProcessorTime;
            for (int i = 0; i < pocet_kroku; i++)
                Math_v2.DesumString("112000321233212112000321233212112000321233212112000321233212112000321233212112000321233212112000321233212112000321233212112000112000321233212112000321233212112000321233212112000321233212112000321233212112000321233212112000321233212112000321233212112000112000321233212112000321233212112000321233212112000321233212112000321233212112000321233212112000321233212112000321233212112000112000321233212112000321233212112000321233212112000321233212112000321233212112000321233212112000321233212112000321233212112000112000321233212112000321233212112000321233212112000321233212112000321233212112000321233212112000321233212112000321233212112000112000321233212112000321233212112000321233212112000321233212112000321233212112000321233212112000321233212112000321233212112000112000321233212112000321233212112000321233212112000321233212112000321233212112000321233212112000321233212112000321233212112000112000321233212112000321233212112000321233212112000321233212112000321233212112000321233212112000321233212112000321233212112000112000321233212112000321233212112000321233212112000321233212112000321233212112000321233212112000321233212112000321233212112000112000321233212112000321233212112000321233212112000321233212112000321233212112000321233212112000321233212112000321233212112000112000321233212112000321233212112000321233212112000321233212112000321233212112000321233212112000321233212112000321233212112000112000321233212112000321233212112000321233212112000321233212112000321233212112000321233212112000321233212112000321233212112000112000321233212112000321233212112000321233212112000321233212112000321233212112000321233212112000321233212112000321233212112000112000321233212112000321233212112000321233212112000321233212112000321233212112000321233212112000321233212112000321233212112000112000321233212112000321233212112000321233212112000321233212112000321233212112000321233212112000321233212112000321233212112000112000321233212112000321233212112000321233212112000321233212112000321233212112000321233212112000321233212112000321233212112000",
                    "112000321233212112000321233212112000321233212112000321233212112000321233212112000321233212112000321233212112000321233212112000112000321233212112000321233212112000321233212112000321233212112000321233212112000321233212112000321233212112000321233212112000112000321233212112000321233212112000321233212112000321233212112000321233212112000321233212112000321233212112000321233212112000112000321233212112000321233212112000321233212112000321233212112000321233212112000321233212112000321233212112000321233212112000112000321233212112000321233212112000321233212112000321233212112000321233212112000321233212112000321233212112000321233212112000112000321233212112000321233212112000321233212112000321233212112000321233212112000321233212112000321233212112000321233212112000112000321233212112000321233212112000321233212112000321233212112000321233212112000321233212112000321233212112000321233212112000112000321233212112000321233212112000321233212112000321233212112000321233212112000321233212112000321233212112000321233212112000112000321233212112000321233212112000321233212112000321233212112000321233212112000321233212112000321233212112000321233212112000112000321233212112000321233212112000321233212112000321233212112000321233212112000321233212112000321233212112000321233212112000112000321233212112000321233212112000321233212112000321233212112000321233212112000321233212112000321233212112000321233212112000112000321233212112000321233212112000321233212112000321233212112000321233212112000321233212112000321233212112000321233212112000112000321233212112000321233212112000321233212112000321233212112000321233212112000321233212112000321233212112000321233212112000112000321233212112000321233212112000321233212112000321233212112000321233212112000321233212112000321233212112000321233212112000112000321233212112000321233212112000321233212112000321233212112000321233212112000321233212112000321233212112000321233212112000112000321233212112000321233212112000321233212112000321233212112000321233212112000321233212112000321233212112000321233212112000");
            currentProcess.Refresh();
            double seconds2 = (currentProcess.TotalProcessorTime - start2).TotalMilliseconds;


            Console.Write("Meření času(rozdil)\nMathV1 : {0} ms.\nMathV2 : {1} ms\n", seconds, seconds2); 
            /* */

            /*//debugovaci merici proces
            int pocet_kroku = 50000;
            currentProcess.Refresh();
            TimeSpan start3 = currentProcess.TotalProcessorTime;
            for (int i = 0; i < pocet_kroku; i++)
                Math.ProductString("1356385",
                    "1356385");
            currentProcess.Refresh();
            double seconds3 = (currentProcess.TotalProcessorTime - start3).TotalMilliseconds;

            currentProcess.Refresh();
            TimeSpan start4 = currentProcess.TotalProcessorTime;
            for (int i = 0; i < pocet_kroku; i++)
                Math_v2.ProductString("1356385",
                    "1356385");
            currentProcess.Refresh();
            double seconds4 = (currentProcess.TotalProcessorTime - start4).TotalMilliseconds;

            currentProcess.Refresh();
            TimeSpan start5 = currentProcess.TotalProcessorTime;
            for (int i = 0; i < pocet_kroku; i++)
                Math_v3.ProductString("1356385",
                    "1356385");
            currentProcess.Refresh();
            double seconds5 = (currentProcess.TotalProcessorTime - start5).TotalMilliseconds;


            Console.Write("Meření času(součet)\nMathV11 : {0} ms.\nMathV2 : {1} ms\nMathV3 : {2}\n", seconds3, seconds4, seconds5);
            /* */

             //*//debugovaci merici proces
            string[] res = new string[6];

            //int pocet_kroku = 50000;
            int pocet_kroku = 10000;

            var num1 = "446744073709551614467440737095516144674407370955161446744073709551614467440737095516144674407370955161.001200003003021000000654";
            var num2 = "46744073709551614467440737095516144674407370955161446744073709551614467440737095516144674407370955161.1200003003021000000654";

            //var num1 = "446744073709551614467440737095516144674407370955161446744073709551614467440737095516144674407370955161";
            //var num2 = "46744073709551614467440737095516144674407370955161446744073709551614467440737095516144674407370955161";

            //num2 = "15";


            double seconds4 = testPefrormance(() =>
            {
                return () => res[0] = Math.SumString(num1, num2);
            });


            double seconds5 = testPefrormance(() =>
            {
                return () => res[1] = Math_v2.SumString(num1, num2);
            });

            double seconds6 = testPefrormance(() =>
            {
                return () => res[2] = Math_v2.SumString(num1, num2, false);
            });


            double seconds7 = testPefrormance(() =>
            {
                return () => res[3] = Math_v4.SumString(num1, num2);
            });


            double seconds8 = testPefrormance(() =>
            {
                MathNumber m1 = MathNumber.Parse(num1);
                MathNumber m2 = MathNumber.Parse(num2);

                return () => res[4] = (m1 + m2).ToString();
            });

            //currentProcess.Refresh();
            //TimeSpan start9 = currentProcess.TotalProcessorTime;
            //BigInteger bi_t1 = BigInteger.Parse("446744073709551614467440737095516144674407370955161446744073709551614467440737095516144674407370955161");
            //BigInteger bi_t2 = BigInteger.Parse("446744073709551614467440737095516144674407370955161446744073709551614467440737095516144674407370955161");

            //for (int i = 0; i < pocet_kroku; i++)
            //    res[5] = (bi_t1 + bi_t2).ToString();
            //currentProcess.Refresh();
            //double seconds9 = (currentProcess.TotalProcessorTime - start9).TotalMilliseconds;

            double seconds9 = 0;

            //double seconds9 = testPefrormance(() =>
            //{
            //    var m1 = BigInteger.Parse(num1);
            //    var m2 = BigInteger.Parse(num2);

            //    return () => res[5] = (m1 + m2).ToString();
            //});

            Console.WriteLine("Meření času(soucin)\nMathV1 : {3} ms\nMathV2 : {0} ms. with convert\nMathV2 : {1} ms\nMathV4 : {2} ms\nMathV4 : {4} ms, MathNumber Class\nBigInt : {5} ms\n", seconds5, seconds6, seconds7, seconds4, seconds8, seconds9);
            for (int i = 0; i < res.Length; i++)
                Console.Write("MathV{0} : {1}\n", i+1, res[i]);
            /* */


            /* //debugovaci merici proces
            int pocet_kroku = 1;
            currentProcess.Refresh();
            TimeSpan start7 = currentProcess.TotalProcessorTime;
            for (int i = 0; i < pocet_kroku; i++)
                Math_v2.Fakt("4000");//,
                    //"4225");
            currentProcess.Refresh();
            double seconds7 = (currentProcess.TotalProcessorTime - start7).TotalMilliseconds;

            currentProcess.Refresh();
            TimeSpan start8 = currentProcess.TotalProcessorTime;
            for (int i = 0; i < pocet_kroku; i++)
                Math_v3.Fakt("4000");//,
                    //"4225");
            currentProcess.Refresh();
            double seconds8 = (currentProcess.TotalProcessorTime - start8).TotalMilliseconds;



            Console.Write("Meření času(faktorial)\nMathV2 : {0} ms.\nMathV3 : {1} ms\n", seconds7, seconds8);
            /* */
            
        }

        public static double testPefrormance(Func<Action> prepareAction, int stepCount = 30000, Process proc = null)
        {
            var curProc = proc ?? _currentProcess;
            curProc.Refresh();
            TimeSpan start = curProc.TotalProcessorTime;
            //try
            {
                var action = prepareAction();

                for (int i = 0; i < stepCount; i++)
                    action();
            }
            //catch
            {
                // Ignored
            }

            curProc.Refresh();
            return (curProc.TotalProcessorTime - start).TotalMilliseconds;
        }
    }
}

            


            

