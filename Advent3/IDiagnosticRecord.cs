using System.Collections;
using System.Collections.Generic;

namespace CodeAdvent.Advent3
{
    public interface IDiagnosticRecord
    {
        int Co2 { get; set; }
        BitArray EpsilonBits { get; set; }
        int EpsilonNumber { get; set; }
        string FilePath { get; set; }
        BitArray GammaBits { get; set; }
        int GammaNumber { get; set; }
        int Oxy { get; set; }
        List<Diagnostic> Record { get; set; }

        void ComputeAtmospherics();
        void ComputeMostCommon();
        List<Diagnostic> FilterOnBit(int index, bool bit, List<Diagnostic> source);
        List<Diagnostic> FilterOnPosition(int pos, List<Diagnostic> source, bool mostCommon);
        int getIntFromBitArray(BitArray bitArray);
        void LoadDiagnostics();
        void LoadFromList(List<string> diagnosticList);
    }
}