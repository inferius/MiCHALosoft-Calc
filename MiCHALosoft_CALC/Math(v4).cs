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
            m._is_positive = number[0] == '-' ? false : true;
            char [] float_separator = { '.' };
            m._float_start_index = number.IndexOf('.');
            m._is_float = m._float_start_index == -1 ? false : true;

            //StringBuilder new_str = new StringBuilder(number);

            // pokud je float je rozdelen na 2 části
            if (!m._is_float)
            {
                //if (number 
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

        //public static IEnumerable<string> Split(string str, int chunkSize)
        //{
        //    return Enumerable.Range(0, str.Length / chunkSize)
        //        .Select(i => str.Substring(i * chunkSize, chunkSize));
        //}

        public static IEnumerable<string> Split(string s, int length)
        {
            var buf = new char[length];
            using (var rdr = new System.IO.StringReader(s))
            {
                int l;
                l = rdr.ReadBlock(buf, 0, length);
                while (l > 0)
                {
                    yield return (new string(buf, 0, l));
                    l = rdr.ReadBlock(buf, 0, length);
                }
            }
        }
        #endregion

        #region Operator
        public static MathNumber operator +(MathNumber m1, MathNumber m2)
        {
            MathNumber m = new MathNumber();

            if (!m1._is_float && !m2._is_float)
            {
                MathNumber mn_buf = m1._segments.Count > m2._segments.Count ? m1 : m2;
                int lng = mn_buf._segments.Count;
                int[] offset = new int[lng];
                UInt64[] res = new UInt64[lng];

                for (int i = 0; i < lng; i++)
                {
                    UInt64 s1 = m1._segments.Count < i ? 0 : m1._segments[i].NumberPart;
                    UInt64 s2 = m2._segments.Count < i ? 0 : m2._segments[i].NumberPart;
                    res[i] = s1 + s2;
                    offset[i] = res[i].ToString().Length - mn_buf._segments[i].ToString().Length;
                }

                for (int i = 0; i < lng; i++)
                {
                    if (i == 0)
                    {
                        continue;
                    }
                    string st1 = res[i].ToString().Substring(0, offset[i]),
                           st2 = res[i - 1].ToString();

                    var st1_olenght = st1.Length;
                    UInt64 st1_ui = 0;

                    if (st1_olenght > 0)
                    {
                        st1_ui = UInt64.Parse(st1);
                    }

                    res[i] = UInt64.Parse(res[i].ToString().Substring(st1_olenght));
                    res[i - 1] = st1_ui + UInt64.Parse(st2);
                    
                }
                StringBuilder str_res = new StringBuilder();
                foreach (var r in res)
                {
                    str_res.Append(r.ToString());
                }

                m = MathNumber.Parse(str_res.ToString());
                
            }
            else if ((!m1._is_float && m2._is_float) || (m1._is_float && !m2._is_float))
            {

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
