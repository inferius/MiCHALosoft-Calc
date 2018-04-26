using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace MiCHALosoft_CALC
{
    class Math_v4
    {

        public static string SumString(string op1, string op2)
        {
            return (MathNumber.Parse(op1) + MathNumber.Parse(op2)).ToString();
        }

    }

    public class MathNumber
    {
        private bool _isPositive = true;
        private bool _isFloat = false;
        private List<SegmentNumber> _segments = new List<SegmentNumber>();
        private string _original = "";
        private int _float_start_index = -1;
        private static int MaxSegmentCount = UInt64.MaxValue.ToString().Length - 4;

        private List<SegmentNumber> IntegerPart => _segments.Where(item => !item.AfterFloat).ToList();
        private List<SegmentNumber> FloatPart => _segments.Where(item => item.AfterFloat).ToList();

        #region Constructors
        public MathNumber(string number)
        {
            var m = Parse(number);

            _isFloat = m._isFloat;
            _isPositive = m._isPositive;
            _segments = m._segments;
            _original = m._original;
            _float_start_index = m._float_start_index;
        }

        public MathNumber()
        {

        }
        #endregion

        #region Static method
        public static MathNumber Parse(string number)
        {
            var m = new MathNumber();
            m._original = number ?? throw new ArgumentNullException();

            number = number.Trim();

            if (number == String.Empty)
            {
                throw new ArgumentException();
            }
            m._isPositive = number[0] != '-';
            char[] float_separator = { '.' };
            m._float_start_index = number.IndexOf('.');
            m._isFloat = m._float_start_index != -1;

            //StringBuilder new_str = new StringBuilder(number);

            // pokud je float je rozdelen na 2 části
            if (!m._isFloat)
            {
                m._segments.AddRange(Split(number, MaxSegmentCount).Select(seg => new SegmentNumber(UInt64.Parse(seg), getPrependCount(seg), m._isPositive, false)));
            }
            else
            {
                string[] numbers = number.Split(float_separator);
                for (var i = 0; i < numbers.Length; i++)
                {
                    if (i == 0)
                    {
                        m._segments.AddRange(Split(numbers[i], MaxSegmentCount).Select(seg => new SegmentNumber(UInt64.Parse(seg), getPrependCount(seg), m._isPositive, false)));
                    }
                    else
                    {
                        m._float_start_index = m._segments.Count;
                        m._segments.AddRange(Split(numbers[i], MaxSegmentCount).Select(seg => new SegmentNumber(UInt64.Parse(seg), getPrependCount(seg), m._isPositive, true)));
                    }
                }
            }

            return m;
        }

        private static int getPrependCount(string nun)
        {
            var zeroCnt = 0;
            foreach (var t in nun)
            {
                if (t == '0')
                {
                    zeroCnt++;
                }
                else
                {
                    break;
                }
            }

            return zeroCnt;
        }

        public static IEnumerable<string> Split(string str, int chunkSize)
        {
            var lng_a = str.Length / chunkSize;
            var lng_m = str.Length % chunkSize;
            int f_lng = 0;

            if (lng_a <= 0) return new string[] { str };

            f_lng = (int)lng_a;
            if (lng_m != 0) f_lng++;

            var s = new string[f_lng];
            var b = new StringBuilder();
            var i = 0;
            var e_lng = f_lng - 1;
            var c_lng = 0;

            //foreach (var c in str)
            for (var j = str.Length - 1; j >= 0; j--)
            {
                b.Append(str[j]);
                if (++i == chunkSize)
                {
                    var charArray = b.ToString().ToCharArray();
                    Array.Reverse(charArray);


                    s[e_lng--] = new string(charArray);
                    //s[c_lng++] = new string(charArray);
                    //s[c_lng++] = b.ToString();
                    b.Clear();
                    i = 0;
                }
            }

            if (b.Length > 0)
            {
                var charArray = b.ToString().ToCharArray();
                Array.Reverse(charArray);


                s[0] = new string(charArray);
            }

            return s;

        }

        //public static IEnumerable<string> Split(string s, int length)
        //{
        //    var buf = new char[length];
        //    using (var rdr = new System.IO.StringReader(s))
        //    {
        //        int l;
        //        l = rdr.ReadBlock(buf, 0, length);
        //        while (l > 0)
        //        {
        //            yield return (new string(buf, 0, l));
        //            l = rdr.ReadBlock(buf, 0, length);
        //        }
        //    }
        //}
        #endregion

        #region Public Methods

        #endregion

        #region Operator

        private static void AppendZero(StringBuilder sb, int count)
        {
            for (var i = 0; i < count; i++) sb.Append("0");
        }

        private static StringBuilder SumSegments(List<SegmentNumber> m1IntPart, List<SegmentNumber> m2IntPart, ulong additional = 0, bool isFloatPart = false)
        {

            if (isFloatPart)
            {
                var m1 = new StringBuilder(SegmentsToString(m1IntPart));
                var m2 = new StringBuilder(SegmentsToString(m2IntPart));

                if (m1IntPart.Count > 0 && m1IntPart[0].PrependZeroCount > 0)
                {
                    AppendZero(m2, m1IntPart[0].PrependZeroCount);
                }

                if (m2IntPart.Count > 0 && m2IntPart[0].PrependZeroCount > 0)
                {
                    AppendZero(m1, m2IntPart[0].PrependZeroCount);
                }

                if (m1.Length > m2.Length)
                {
                    AppendZero(m2, m1.Length - m2.Length);
                }
                else
                {
                    AppendZero(m1, m2.Length - m1.Length);
                }

                m1IntPart.Clear();
                m1IntPart.AddRange(Split(m1.ToString(), MaxSegmentCount).Select(seg => new SegmentNumber(UInt64.Parse(seg), getPrependCount(seg), true, true)));

                m2IntPart.Clear();
                m2IntPart.AddRange(Split(m2.ToString(), MaxSegmentCount).Select(seg => new SegmentNumber(UInt64.Parse(seg), getPrependCount(seg), true, true)));
            }

            var mnBufGreaterIntPart = m1IntPart.Count > m2IntPart.Count ? m1IntPart : m2IntPart;

            int lng = mnBufGreaterIntPart.Count;
            int[] offset = new int[lng];
            UInt64[] res = new UInt64[lng];
            var prepentZero = new int[lng];

            for (int i = lng - 1, c1 = m1IntPart.Count - 1, c2 = m2IntPart.Count - 1; i >= 0; i--, c1--, c2--)
            {
                UInt64 s1 = c1 < 0 ? 0 : m1IntPart[c1].NumberPart;
                UInt64 s2 = c2 < 0 ? 0 : m2IntPart[c2].NumberPart;

                res[i] = s1 + s2 + additional;
                offset[i] = res[i].ToString().Length - mnBufGreaterIntPart[i].ToString().Length;

                additional = 0;
            }

            for (var i = 0; i < lng; i++)
            {
                if (i == 0)
                {
                    continue;
                }
                string st1 = res[i].ToString();

                var cur_offset = offset[i];

                var sb_st1_part1 = new StringBuilder();
                var sb_st1_part2 = new StringBuilder();

                for (var j = 0; j < st1.Length; j++)
                {
                    if (j < cur_offset) sb_st1_part1.Append(st1[j]);
                    else sb_st1_part2.Append(st1[j]);
                }

                UInt64 st1_ui = 0;

                if (cur_offset > 0)
                {
                    st1_ui = UInt64.Parse(sb_st1_part1.ToString());
                }

                var s_st1_part2 = sb_st1_part2.ToString();
                prepentZero[i] = getPrependCount(s_st1_part2);

                res[i] = UInt64.Parse(s_st1_part2);
                res[i - 1] = st1_ui + res[i - 1];

            }
            StringBuilder str_res = new StringBuilder();
            var ji = 0;
            foreach (var r in res)
            {
                for (var z = 0; z < prepentZero[ji]; z++) str_res.Append('0');

                str_res.Append(r.ToString());
                ji++;
            }

            return str_res;
        }

        public static MathNumber operator +(MathNumber m1, MathNumber m2)
        {
            if (m2._original == "0") return Parse(m1._original);
            if (m1._original == "0") return Parse(m2._original);

            ulong add = 0;

            #region Soucet desetinne casti
            var sb_float_part = new StringBuilder();

            if (m1._isFloat && m2._isFloat)
            {
                if (m1._isPositive && m2._isPositive)
                {
                    var m1_float_part = m1.FloatPart;
                    var m2_float_part = m2.FloatPart;

                    var m1_float_string = SegmentsToString(m1_float_part);
                    var m2_float_string = SegmentsToString(m2_float_part);

                    var larger = m1_float_string.Length > m2_float_string.Length ? m1_float_string : m2_float_string;

                    sb_float_part = SumSegments(m1_float_part.ToList(), m2_float_part.ToList(), 0, true);

                    if (sb_float_part.Length > larger.Length)
                    {
                        add = ulong.Parse(sb_float_part.ToString(0, sb_float_part.Length - larger.Length));
                    }

                    

                }
            }
            else if (m1._isFloat || m2._isFloat)
            {
                if (m1._isFloat) sb_float_part.Append(SegmentsToString(m1.FloatPart));
                if (m2._isFloat) sb_float_part.Append(SegmentsToString(m2.FloatPart));
            }

            #endregion

            #region Soucet celociselne casti
            var sb_res = SumSegments(m1.IntegerPart, m2.IntegerPart, add);
            #endregion

            if (sb_float_part.Length > 0)
            {
                sb_res.Append(".");
                sb_res.Append(sb_float_part);
            }

            return Parse(sb_res.ToString());
        }
        #endregion

        #region Override method
        public override string ToString()
        {
            StringBuilder str = new StringBuilder();
            bool isPointAdded = false;

            foreach (var s in _segments)
            {
                if (!isPointAdded && s.AfterFloat)
                {
                    isPointAdded = true;
                    str.Append(".");
                }

                

                str.Append(s);
            }

            return str.ToString();
        }

        private string ToFloatPartString()
        {
            StringBuilder str = new StringBuilder();

            foreach (var s in FloatPart)
            {
                str.Append(s);
            }

            return str.ToString();
        }

        private string ToIntPartString()
        {
            StringBuilder str = new StringBuilder();

            foreach (var s in IntegerPart)
            {
                str.Append(s);
            }

            return str.ToString();
        }

        private static string SegmentsToString(IEnumerable<SegmentNumber> segments)
        {
            StringBuilder str = new StringBuilder();

            foreach (var s in segments)
            {
                str.Append(s);
            }

            return str.ToString();
        }

        public override bool Equals(object obj)
        {
            return base.Equals(obj);
        }

        public override int GetHashCode()
        {
            return base.GetHashCode();
        }
        #endregion
    }

    public class SegmentNumber
    {
        #region Properties
        public UInt64 NumberPart { set; get; } = 0;

        public int PrependZeroCount { set; get; } = 0;

        public bool IsPositive { set; get; } = true;

        public bool AfterFloat { set; get; } = false;

        #endregion

        public SegmentNumber(UInt64 numberPart = 0, int prependZeroCount = 0, bool isPositive = true, bool afterFloat = false)
        {
            IsPositive = isPositive;
            NumberPart = numberPart;
            PrependZeroCount = prependZeroCount;
            AfterFloat = afterFloat;
        }

        public static SegmentNumber Parse(string number)
        {
            var s = new SegmentNumber();

            return s;
        }

        public override string ToString()
        {
            var strFlt = new StringBuilder();

            if (PrependZeroCount > 0)
            {
                for (var i = 0; i < PrependZeroCount; i++) strFlt.Append('0');
            }

            strFlt.Append(NumberPart.ToString());

            return strFlt.ToString();
        }
    }
}
