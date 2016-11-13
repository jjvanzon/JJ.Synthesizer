using JJ.Data.Synthesizer;
using JJ.Business.Synthesizer.LinkTo;
using JJ.Business.Synthesizer.Helpers;
using JJ.Business.Synthesizer.Resources;
using JJ.Framework.Reflection.Exceptions;
using System;

namespace JJ.Business.Synthesizer.EntityWrappers
{
    public class Stretch_OperatorWrapper : StretchOrSquash_OperatorWrapperBase
    {
        public Stretch_OperatorWrapper(Operator op) 
            : base(op)
        { }
    }
}
