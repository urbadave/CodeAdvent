using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CodeAdvent.Advent2
{
    public class PositionTracker : IPositionTracker
    {
        public string FilePath { get; set; } = "C:\\CodeAdvent\\2\\2-Moves.txt";
        public int HPosition { get; set; } = 0;
        public int Aim { get; set; } = 0;
        public int Depth { get; set; } = 0;
        public List<MoveInstruction> Course { get; set; }

        public void LoadCourse()
        {
            Course = new List<MoveInstruction>();
            var text = File.ReadAllText(FilePath);
            text = text.Replace("\r", string.Empty);
            var strList = text.Split('\n').ToList();
            strList.ForEach(s =>
            {
                var vals = s.Split(" ");
                var dir = vals[0];
                var size = int.Parse(vals[1]);
                Course.Add(new MoveInstruction() { Direction = dir, Size = size });
            });
        }

        public int GetFinalValus()
        {
            return Depth * HPosition;
        }

        public void FollowCourse()
        {
            if (Course == null) return;
            foreach (var move in Course)
            {
                MakeMove(move);
            }
        }

        public void MakeMove(MoveInstruction move)
        {
            if (move == null) return;

            switch (move.Direction)
            {
                case "forward":
                    GoForward(move.Size);
                    break;
                case "up":
                    GoUp(move.Size);
                    break;
                case "down":
                    GoDown(move.Size);
                    break;
            }
        }

        private void GoUp(int amount)
        {
            Aim -= amount;
        }

        private void GoDown(int amount)
        {
            Aim += amount;
        }

        private void GoForward(int amount)
        {
            HPosition += amount;
            Depth += (Aim * amount);
        }
    }
}
