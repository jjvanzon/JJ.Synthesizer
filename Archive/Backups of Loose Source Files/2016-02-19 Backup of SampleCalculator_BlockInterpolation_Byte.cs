//using JJ.Data.Synthesizer;
//using System.IO;

//namespace JJ.Business.Synthesizer.Calculation.Samples
//{
//    internal class SampleCalculator_BlockInterpolation_Byte : SampleCalculator_BlockInterpolation_Base
//    {
//        private const double VALUE_DIVIDER = 128.0;

//        public SampleCalculator_BlockInterpolation_Byte(Sample sample, byte[] bytes)
//            : base(sample, bytes)
//        { }

//        protected override double ReadValue(BinaryReader binaryReader)
//        {
//            byte b = binaryReader.ReadByte();
//            double value = b - 128.0;
//            value /= VALUE_DIVIDER;
//            return value;
//        }
//    }
//}