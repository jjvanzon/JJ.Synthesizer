﻿using System.Collections.Generic;

namespace JJ.Business.SynthesizerPrototype.Dto
{
    public abstract class OperatorDtoBase_Vars_1Const : OperatorDtoBase
    {
        public IList<OperatorDtoBase> Vars { get; set; }
        public double ConstValue { get; set; }

        public override IList<OperatorDtoBase> InputOperatorDtos
        {
            get { return Vars; }
            set { Vars = value; }
        }
    }
}