//using System;
//using JJ.Business.Synthesizer.Enums;
//using JJ.Business.Synthesizer.Extensions;
//using JJ.Business.Synthesizer.Helpers;
//using JJ.Business.Synthesizer.Resources;
//using JJ.Data.Synthesizer;
//using JJ.Framework.Reflection.Exceptions;

//namespace JJ.Business.Synthesizer.Warnings
//{
//    internal class OperatorWarningValidator_Cache : OperatorWarningValidator_Base_AllInletsFilled
//    {
//        public OperatorWarningValidator_Cache(Operator obj)
//            : base(obj)
//        { }

//        protected override void Execute()
//        {
//            base.Execute();

//            foreach (Inlet inlet in Object.Inlets)
//            {
//                double? number = TryGetConstantNumber(inlet);

//                switch (inlet.ListIndex)
//                {
//                    case OperatorConstants.CACHE_SIGNAL_INDEX:

//                        For(number, PropertyNames.Signal, PropertyDisplayNames.Signal)
//                            .NotNull()
//                            .NotInfinity()
//                            .NotNaN();
//                        break;

//                    case OperatorConstants.CACHE_START_TIME_INDEX:
//                        For(number, PropertyNames.StartTime, PropertyDisplayNames.StartTime)
//                            .NotNull()
//                            .NotInfinity()
//                            .NotNaN();
//                        break;

//                    case OperatorConstants.CACHE_END_TIME_INDEX:
//                        For(number, PropertyNames.EndTime, PropertyDisplayNames.EndTime)
//                            .NotNull()
//                            .NotInfinity()
//                            .NotNaN();
//                        break;

//                    case OperatorConstants.CACHE_SAMPLING_RATE_INDEX:
//                        For(number, PropertyNames.SamplingRate, PropertyDisplayNames.SamplingRate)
//                            .NotNull()
//                            .IsInteger();
//                        break;
//                }
//            }
//        }

//        private double? TryGetConstantNumber(Inlet inlet)
//        {
//            if (inlet == null) throw new NullException(() => inlet);

//            // Be tolerant in warning validations.
//            if (inlet.InputOutlet == null)
//            {
//                return null;
//            }

//            if (inlet.InputOutlet.Operator == null)
//            {
//                return null;
//            }

//            if (inlet.InputOutlet.Operator.GetOperatorTypeEnum() != OperatorTypeEnum.Number)
//            {
//                return null;
//            }

//            double number;
//            if (!Double.TryParse(inlet.InputOutlet.Operator.Data, out number)) // TODO: Refactor this (and use DataPropertyParser) when the number is encoded into the data property as a key-value pair.
//            {
//                return null;
//            }

//            return number;
//        }
//    }
//}