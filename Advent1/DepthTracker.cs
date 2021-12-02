using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CodeAdvent.Advent1
{
    public class DepthTracker : IDepthTracker
    {
        public string FilePath { get; set; } = "C:\\CodeAdvent\\1\\1-SonarDepths.txt";
        public List<int> DepthList { get; set; }
        public List<int> MeasureWindow { get; set; }

        public int CountDepthIncrease()
        {
            var retVal = 0;

            LoadDepths();
            retVal = CountIncreases(DepthList);

            return retVal;
        }

        public int CountWindowIncrease(int size)
        {
            var retVal = 0;
            LoadDepths();
            LoadMeasureWindows(size);
            retVal = CountIncreases(MeasureWindow);
            return retVal;
        }

        private void LoadDepths()
        {
            DepthList = new List<int>();
            var text = File.ReadAllText(FilePath);
            text = text.Replace("\r", string.Empty);
            var strList = text.Split('\n').ToList();
            var useList = strList.Where(s => !string.IsNullOrWhiteSpace(s)).Select(s => int.Parse(s));
            DepthList.AddRange(useList);
        }
        
        private void LoadMeasureWindows(int size)
        {
            MeasureWindow = new List<int>();
            var rangeList = new List<int>();
            foreach(var d in DepthList)
            {
                rangeList.Add(d);
                if (rangeList.Count > size) rangeList.RemoveAt(0);
                if (rangeList.Count == size) MeasureWindow.Add(rangeList.Aggregate(0, (total, next) => total += next));
            }
        }

        private int CountIncreases(List<int> input)
        {
            var count = 0;
            int? last = null;
            foreach(var d in input)
            {
                if (last != null)
                    if (d > last) count++;
                last = d;
            }
            return count;
        }
    }
}
