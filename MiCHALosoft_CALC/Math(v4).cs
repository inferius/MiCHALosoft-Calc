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
        private bool _is_positive = true;
        private bool _is_float = false;
        private List<SegmentNumber> _segments = new List<SegmentNumber>();
        private string _original = "";
        private int _float_start_index = -1;

        private List<SegmentNumber> IntegerPart => _segments.Where(item => !item.AfterFloat).ToList();
        private List<SegmentNumber> FloatPart => _segments.Where(item => item.AfterFloat).ToList();

        #region Constructors
        public MathNumber(string number)
        {
            var m = MathNumber.Parse(number);

            this._is_float = m._is_float;
            this._is_positive = m._is_positive;
            this._segments = m._segments;
            this._original = m._original;
            this._float_start_index = m._float_start_index;
        }

        public MathNumber()
        {

        }
        #endregion

        #region Static method
        public static MathNumber Parse(string number)
        {
            if (number == null)
            {
                throw new ArgumentNullException();
            }

            var m = new MathNumber();
            int max_segment_number = UInt64.MaxValue.ToString().Length - 2;
            m._original = number;

            number = number.Trim();

            if (number == String.Empty)
            {
                throw new ArgumentException();
            }
            m._is_positive = number[0] != '-';
            char[] float_separator = { '.' };
            m._float_start_index = number.IndexOf('.');
            m._is_float = m._float_start_index != -1;

            //StringBuilder new_str = new StringBuilder(number);

            // pokud je float je rozdelen na 2 části
            if (!m._is_float)
            {

                var segments = MathNumber.Split(number, max_segment_number);

                foreach (var seg in segments)
                {
                    m._segments.Add(new SegmentNumber(UInt64.Parse(seg), 0, m._is_positive, false));
                }
            }
            else
            {
                string[] numbers = number.Split(float_separator);
                for (var i = 0; i < numbers.Length; i++)
                {
                    if (i == 0)
                    {
                        var segments = MathNumber.Split(numbers[i], max_segment_number);
                        foreach (var seg in segments)
                        {
                            m._segments.Add(new SegmentNumber(UInt64.Parse(seg), 0, m._is_positive, false));
                        }
                    }
                    else
                    {
                        m._float_start_index = m._segments.Count;
                        var segments = MathNumber.Split(numbers[i], max_segment_number);
                        foreach (var seg in segments)
                        {
                            int zero_cnt = 0;
                            for (int j = 0; j < seg.Length; j++)
                            {
                                if (seg[j] == '0')
                                {
                                    zero_cnt++;
                                }
                                else
                                {
                                    break;
                                }
                            }

                            m._segments.Add(new SegmentNumber(UInt64.Parse(seg), zero_cnt, m._is_positive, true));
                        }
                    }
                }
            }

            return m;
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

            foreach (var c in str)
            {
                b.Append(c);
                if (i++ == chunkSize)
                {
                    var charArray = b.ToString().ToCharArray();
                    Array.Reverse(charArray);


                    //s[e_lng--] = new string(charArray);
                    s[c_lng++] = new string(charArray);
                    //s[c_lng++] = b.ToString();
                    b.Clear();
                    i = 0;
                }
            }

            if (b.Length > 0)
            {
                var charArray = b.ToString().ToCharArray();
                Array.Reverse(charArray);


                s[e_lng] = new string(charArray);
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
        public static MathNumber operator +(MathNumber m1, MathNumber m2)
        {
            if (m2._original == "0") return Parse(m1._original);
            if (m1._original == "0") return Parse(m2._original);

            MathNumber m = new MathNumber();

            #region Soucet celociselne casti

            var m1_int_part = m1.IntegerPart;
            var m2_int_part = m2.IntegerPart;

            MathNumber mn_buf_greater = m1_int_part.Count > m2_int_part.Count ? m1 : m2;
            MathNumber mn_buf_less = m1_int_part.Count > m2_int_part.Count ? m2 : m1;

            var mn_buf_greater_int_part = mn_buf_greater.IntegerPart;
            var mn_buf_less_int_part = mn_buf_less.IntegerPart;

            int lng = mn_buf_greater_int_part.Count;
            int[] offset = new int[lng];
            UInt64[] res = new UInt64[lng];

            for (int i = lng - 1, c1 = m1_int_part.Count - 1, c2 = m2_int_part.Count - 1; i >= 0; i--, c1--, c2--)
            {
                UInt64 s1 = c1 < 0 ? 0 : m1_int_part[c1].NumberPart;
                UInt64 s2 = c2 < 0 ? 0 : m2_int_part[c2].NumberPart;
                res[i] = s1 + s2;
                offset[i] = res[i].ToString().Length - mn_buf_greater_int_part[i].ToString().Length;
            }


            //for (int i = lng - 1; i >= 0; i--)
            //{
            //    UInt64 s1 = m1_int_part.Count <= i ? 0 : m1_int_part[i].NumberPart;
            //    UInt64 s2 = m2_int_part.Count <= i ? 0 : m2_int_part[i].NumberPart;
            //    res[i] = s1 + s2;
            //    offset[i] = res[i].ToString().Length - mn_buf_int_part[i].ToString().Length;
            //}

            for (int i = 0; i < lng; i++)
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

                res[i] = UInt64.Parse(sb_st1_part2.ToString());
                res[i - 1] = st1_ui + res[i - 1];

            }
            StringBuilder str_res = new StringBuilder();
            foreach (var r in res)
            {
                str_res.Append(r.ToString());
            }

            m = MathNumber.Parse(str_res.ToString());
            #endregion

            if (m1._is_float || m2._is_float)
            {
                if (m1._is_positive && m1._is_positive)
                {
                    var m1_float_part = m1.FloatPart;
                    var m2_float_part = m2.FloatPart;
                }
            }



            return m;
        }
        #endregion

        #region Override method
        public override string ToString()
        {
            StringBuilder str = new StringBuilder();
            bool is_point_added = false;

            foreach (var s in _segments)
            {
                if (s.AfterFloat)
                {
                    string str_flt = s.NumberPart.ToString();
                    str_flt = str_flt.PadLeft(str_flt.Length + s.PrependZeroCount, '0');

                    if (is_point_added)
                    {
                        str.AppendFormat(str_flt);
                        is_point_added = true;
                    }
                    else
                    {
                        str.AppendFormat(".{0}", str_flt);
                    }
                }
                else
                {
                    str.Append(s);
                }
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
        UInt64 _number_part = 0;
        bool _is_positive = true; // oznacuje zda je segment kladny nebo zaporny
        bool _after_float = false; // true - znamea, ze se nachazi az za desetinou čarkou
        int _prepend_zero_count = 0; // oznacuje pocet nul před číslem, tedy když bude _prepend_number_count=5 a _number_pard = 352 pak vysledny segment bude 00000352

        #region Properties
        public UInt64 NumberPart
        {
            set { this._number_part = value; }
            get { return this._number_part; }
        }

        public int PrependZeroCount
        {
            set { this._prepend_zero_count = value; }
            get { return this._prepend_zero_count; }
        }

        public bool IsPositive
        {
            set { this._is_positive = value; }
            get { return this._is_positive; }
        }

        public bool AfterFloat
        {
            set { this._after_float = value; }
            get { return this._after_float; }
        }
        #endregion

        public SegmentNumber(UInt64 numberPart = 0, int prependZeroCount = 0, bool isPositive = true, bool afterFloat = false)
        {
            this._is_positive = isPositive;
            this._number_part = numberPart;
            this._prepend_zero_count = prependZeroCount;
            this.AfterFloat = afterFloat;
        }

        public static SegmentNumber Parse(string number)
        {
            var s = new SegmentNumber();

            return s;
        }

        public override string ToString()
        {
            string str_flt = this.NumberPart.ToString();
            if (this.AfterFloat)
            {
                str_flt = str_flt.PadLeft(str_flt.Length + this.PrependZeroCount, '0');
            }

            return str_flt;
        }
    }
}
