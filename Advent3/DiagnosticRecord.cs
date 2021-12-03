using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CodeAdvent.Advent3
{
    public class DiagnosticRecord : IDiagnosticRecord
    {
        public string FilePath { get; set; } = "C:\\CodeAdvent\\3\\3-Diagnostic.txt";
        public List<Diagnostic> Record { get; set; }
        public BitArray GammaBits { get; set; }
        public int GammaNumber { get; set; }
        public BitArray EpsilonBits { get; set; }
        public int EpsilonNumber { get; set; }
        public int Oxy { get; set; }
        public int Co2 { get; set; }

        private List<int> MostCommon { get; set; }

        public DiagnosticRecord() { }

        public void LoadDiagnostics()
        {
            var text = File.ReadAllText(FilePath);
            text = text.Replace("\r", string.Empty);
            var strList = text.Split('\n').ToList();
            LoadFromList(strList);
        }

        public void LoadFromList(List<string> diagnosticList)
        {
            if (diagnosticList != null && diagnosticList.Any())
            {
                Record = new List<Diagnostic>();
                diagnosticList.ForEach(d => Record.Add(new Diagnostic() { DString = d }));
            }
        }

        public void ComputeMostCommon()
        {
            if (Record == null || !Record.Any()) return;

            //setup
            var size = Record[0].DString.Length;
            MostCommon = new List<int>();
            var i = 0;
            while (i++ < size)
            {
                MostCommon.Add(0);
            }

            foreach (var r in Record)
            {
                i = 0;
                while (i < size)
                {
                    var bit = r.DBits[i];
                    if (bit) MostCommon[i] += 1;
                    else MostCommon[i] -= 1;
                    i++;
                }
            }

            i = 0;
            EpsilonBits = new BitArray(size);
            GammaBits = new BitArray(size);
            while (i < size)
            {
                var mostCommonBit = MostCommon[i] > 0 ? true : false;
                GammaBits.Set(i, mostCommonBit);
                EpsilonBits.Set(i, !mostCommonBit);
                i++;
            }

            GammaNumber = getIntFromBitArray(GammaBits);
            EpsilonNumber = getIntFromBitArray(EpsilonBits);
        }

        public void ComputeAtmospherics()
        {
            var pos = 0;
            var filtered = Record;
            while (filtered.Count > 1 && pos < 12)
            {
                filtered = FilterOnPosition(pos, filtered, true);
                pos++;
            }

            var mostCommon = filtered.First();
            Oxy = getIntFromBitArray(mostCommon.DBits);

            pos = 0;
            filtered = Record;
            while (filtered.Count > 1 && pos < 12)
            {
                filtered = FilterOnPosition(pos, filtered, false);
                pos++;
            }
            var leastCommon = filtered.First();
            Co2 = getIntFromBitArray(leastCommon.DBits);

        }

        public List<Diagnostic> FilterOnPosition(int pos, List<Diagnostic> source, bool mostCommon)
        {
            var oneList = new List<Diagnostic>();
            var zeroList = new List<Diagnostic>();

            foreach (var d in source)
            {
                if (d.DBits[pos])
                    oneList.Add(d);
                else
                    zeroList.Add(d);
            }

            List<Diagnostic> mostCommonList = null;
            List<Diagnostic> leastCommonList = null;

            if (zeroList.Count > oneList.Count)
            {
                mostCommonList = zeroList;
                leastCommonList = oneList;
            }
            else
            {
                mostCommonList = oneList;
                leastCommonList = zeroList;
            }

            return mostCommon ? mostCommonList : leastCommonList;
        }

        public List<Diagnostic> FilterOnBit(int index, bool bit, List<Diagnostic> source)
        {
            var retVal = new List<Diagnostic>();

            if (source != null && source.Any())
            {
                foreach (var d in source)
                {
                    var dbit = d.DBits[index];
                    if (bit == dbit) retVal.Add(d);
                }
            }

            return retVal;
        }

        public int getIntFromBitArray(BitArray bitArray)
        {
            int number = 0;
            var size = bitArray.Length;
            var i = 0;
            var power = 11;
            while (i < size)
            {
                var bit = bitArray[i];
                if (bit)
                {
                    number += (int)Math.Pow(2, power);
                }
                power--;
                i++;
            }
            return number;
        }
    }
}
