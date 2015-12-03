using System;
using System.Linq.Expressions;

namespace JJ.Business.Synthesizer.Helpers
{
    public static class PropertyNames
    {
        // Operator Names
        public const string Add = "Add";
        public const string Adder = "Adder";
        public const string Divide = "Divide";
        public const string Multiply = "Multiply";
        public const string PatchInlet = "PatchInlet";
        public const string PatchOutlet = "PatchOutlet";
        public const string Power = "Power";
        public const string Sine = "Sine";
        public const string Subtract = "Subtract";
        public const string Delay = "Delay";
        public const string SpeedUp = "SpeedUp";
        public const string SlowDown = "SlowDown";
        public const string TimePower = "TimePower";
        public const string Earlier = "Earlier";
        public const string Number = "Number";
        public const string Sample = "Sample";

        // Inlet Names

        // Do not delete outcommented code: I might need it later again.

        //public const string OperandA = "OperandA";
        //public const string OperandB = "OperandB";
        //public const string Origin = "Origin";
        //public const string Numerator = "Numerator";
        //public const string Denominator = "Denominator";
        public const string Input = "Input";
        //public const string Base = "Base";
        //public const string Exponent = "Exponent";
        //public const string Volume = "Volume";
        //public const string Frequency = "Frequency";
        //public const string PhaseShift = "PhaseShift";
        //public const string Signal = "Signal";
        //public const string TimeDifference = "TimeDifference";
        //public const string TimeDivider = "TimeDivider";
        //public const string TimeMultiplier = "TimeMultiplier";
        //public const string Operand = "Operand";
        //public const string Low = "Low";
        //public const string High = "High";
        //public const string Ratio = "Ratio";
        //public const string Attack = "Attack";
        //public const string Start = "Start";
        //public const string Sustain = "Sustain";
        //public const string End = "End";
        //public const string Release = "Release";
        //public const string Time = "Time";

        // Outlet Names
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

        // Other
        public const string AudioFileOutputs = "AudioFileOutputs";
        public const string Cents = "Cents";
        public const string ChildDocument = "ChildDocument";
        public const string Curve = "Curve";
        public const string Curves = "Curves";
        public const string DefaultValue = "DefaultValue";
        public const string Document = "Document";
        public const string DocumentReference = "DocumentReference";
        public const string Documents = "Documents";
        public const string Exponents = "Exponents";
        public const string Factors = "Factors";
        public const string ID = "ID";
        public const string Inlet = "Inlet";
        public const string InletCount = "InletCount";
        public const string Inlets = "Inlets";
        public const string InletTypeEnum = "InletTypeEnum";
        public const string InputOutlet = "InputOutlet";
        public const string ListIndex = "ListIndex";
        public const string LiteralFrequencies = "LiteralFrequencies";
        public const string Name = "Name";
        public const string Node = "Node";
        public const string Nodes = "Nodes";
        public const string Outlet = "Outlet";
        public const string OutletCount = "OutletCount";
        public const string Outlets = "Outlets";
        public const string OutletTypeEnum = "OutletTypeEnum";
        public const string ParentDocument = "ParentDocument";
        public const string Patch = "Patch";
        public const string Patches = "Patches";
        public const string Samples = "Samples";
        public const string Scales = "Scales";
        public const string SemiTones = "SemiTones";
        public const string UnderlyingPatch = "UnderlyingPatch";
    }
}
