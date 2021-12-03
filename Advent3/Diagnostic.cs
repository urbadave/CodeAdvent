using System;
using System.Collections.Generic;
using System.Collections;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CodeAdvent.Advent3;

namespace CodeAdvent.Advent3
{
    public class Diagnostic
    {
        private BitArray _dbits;
        public BitArray DBits
        {
            get
            {
                return _dbits;
            }
            set
            {
                _dbits = value;
                LoadString(_dbits);
            }
        }
        private string _dstring;
        public string DString
        { get
            {
                return _dstring;
            }
            set
            {
                _dstring = value;
                LoadDBits(_dstring);
            }
        }

        public Diagnostic() { }

        public Diagnostic(string value)
        {
            DString = value;
        }

        private void LoadDBits(string value)
        {
            _dbits = new BitArray(value.Length);
            var ce = value.GetEnumerator();
            var i = 0;
            while (ce.MoveNext())
            {
                _dbits.Set(i++, ce.Current == '1');
            }
        }

        private void LoadString(BitArray value)
        {
            var strBld = new StringBuilder();
            var i = 0;
            while (i < value.Length)
            {                
                var str = value[i++] ? "1" : "0";
                strBld.Append(str);
            }
            _dstring = strBld.ToString();
        }

    }
}
