﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JJ.Demos.Synthesizer.NanoOptimization.Calculation.Operators.WithStructs
{
    internal interface IOperatorCalculator_Vars : IOperatorCalculator
    {
        void SetVarCalculator(int i, IOperatorCalculator varOperatorCalculator);
    }
}