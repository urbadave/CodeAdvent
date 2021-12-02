using System.Collections.Generic;

namespace CodeAdvent.Advent2
{
    public interface IPositionTracker
    {
        List<MoveInstruction> Course { get; set; }
        int Aim { get; set; }
        int Depth { get; set; }
        string FilePath { get; set; }
        int HPosition { get; set; }

        void FollowCourse();
        int GetFinalValus();
        void LoadCourse();
        void MakeMove(MoveInstruction move);
    }
}