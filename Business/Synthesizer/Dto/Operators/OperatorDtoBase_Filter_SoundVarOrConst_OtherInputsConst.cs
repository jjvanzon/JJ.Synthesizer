﻿namespace JJ.Business.Synthesizer.Dto.Operators
{
    internal abstract class OperatorDtoBase_Filter_SoundVarOrConst_OtherInputsConst : OperatorDtoBase_Filter_VarSound
    {
        public abstract InputDto Frequency { get; }

        public double A0 { get; set; }
        public double A1 { get; set; }
        public double A2 { get; set; }
        public double A3 { get; set; }
        public double A4 { get; set; }
    }
}
