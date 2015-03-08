using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JJ.Business.Synthesizer.Names
{
    public static class PropertyNames
    {
        // Operators
        public const string Add = "Add";
        public const string Adder = "Adder";
        public const string Divide = "Divide";
        public const string Multiply = "Multiply";
        public const string PatchInlet = "PatchInlet";
        public const string PatchOutlet = "PatchOutlet";
        public const string Power = "Power";
        public const string Sine = "Sine";
        public const string Substract = "Substract";
        public const string TimeAdd = "TimeAdd";
        public const string TimeDivide = "TimeDivide";
        public const string TimeMultiply = "TimeMultiply";
        public const string TimePower = "TimePower";
        public const string TimeSubstract = "TimeSubstract";
        public const string ValueOperator = "ValueOperator";
        public const string CurveIn = "CurveIn";
        public const string SampleOperator = "SampleOperator";

        // Inlets
        public const string OperandA = "OperandA";
        public const string OperandB = "OperandB";
        public const string Origin = "Origin";
        public const string Numerator = "Numerator";
        public const string Denominator = "Denominator";
        public const string Input = "Input";
        public const string Base = "Base";
        public const string Exponent = "Exponent";
        public const string Volume = "Volume";
        public const string Pitch = "Pitch";
        public const string Level = "Level";
        public const string PhaseStart = "PhaseStart";
        public const string Signal = "Signal";
        public const string TimeDifference = "TimeDifference";
        public const string TimeDivider = "TimeDivider";
        public const string TimeMultiplier = "TimeMultiplier";
        public const string Operand = "Operand";

        // Outlets
        public const string Result = "Result";
        
        // Wav Header Properties
        public const string ChunkID = "ChunkID";
        public const string Format = "Format";
        public const string SubChunkID = "SubChunkID";
        public const string SubChunk1Size = "SubChunk1Size";
        public const string AudioFormat = "AudioFormat";
        public const string SubChunk2ID = "SubChunk2ID";
        public const string ChannelCount = "ChannelCount";
        public const string BitsPerValue = "BitsPerValue";
        public const string SamplingRate = "SamplingRate";
        public const string BytesPerSample = "BytesPerSample";
        public const string BytesPerSecond = "BytesPerSecond";
        public const string ChunkSize = "ChunkSize";
    }
}
