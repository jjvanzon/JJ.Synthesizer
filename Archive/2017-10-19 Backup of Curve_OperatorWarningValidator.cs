﻿//using System;
//using JJ.Business.Synthesizer.EntityWrappers;
//using JJ.Business.Synthesizer.Helpers;
//using JJ.Business.Synthesizer.Resources;
//using JJ.Data.Synthesizer.Entities;
//using JJ.Framework.Validation;

//namespace JJ.Business.Synthesizer.Warnings.Operators
//{
//    internal class Curve_OperatorWarningValidator : VersatileValidator
//    {
//        public Curve_OperatorWarningValidator(Operator op)
//        {
//            if (op == null) throw new ArgumentNullException(nameof(op));

//            // ReSharper disable once InvertIf
//            if (DataPropertyParser.DataIsWellFormed(op))
//            {
//                string curveIDString = DataPropertyParser.TryGetString(op, nameof(Curve_OperatorWrapper.CurveID));

//                For(curveIDString, ResourceFormatter.Curve).NotNullOrEmpty();
//            }
//        }
//    }
//}
