﻿using System;
using System.Collections.Generic;
using System.Linq;
using JJ.Business.Synthesizer.Enums;
using JJ.Business.Synthesizer.Helpers;
using JJ.Data.Synthesizer;
using JJ.Framework.Reflection.Exceptions;

namespace JJ.Business.Synthesizer.Extensions
{
    public static class InletExtensions
    {
        public static double? TryGetConstantNumber(this Inlet inlet)
        {
            if (inlet == null) throw new NullException(() => inlet);

            if (inlet.InputOutlet?.Operator?.GetOperatorTypeEnum() != OperatorTypeEnum.Number)
            {
                return null;
            }

            if (DataPropertyParser.DataIsWellFormed(inlet.InputOutlet.Operator.Data))
            {
                double? number = DataPropertyParser.TryParseDouble(inlet.InputOutlet.Operator, PropertyNames.Number);
                return number;
            }

            return null;
        }
    }
}