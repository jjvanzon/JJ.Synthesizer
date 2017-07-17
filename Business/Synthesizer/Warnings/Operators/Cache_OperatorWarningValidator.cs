using System;
using JJ.Business.Synthesizer.Enums;
using JJ.Business.Synthesizer.Extensions;
using JJ.Business.Synthesizer.Resources;
using JJ.Data.Synthesizer.Entities;
using JJ.Framework.Validation;

namespace JJ.Business.Synthesizer.Warnings.Operators
{
    internal class Cache_OperatorWarningValidator : VersatileValidator
    {
        public Cache_OperatorWarningValidator(Operator op)
        {
            if (op == null) throw new ArgumentNullException(nameof(op));

            double? signal = null;
            double? start = null;
            double? end = null;
            double? samplingRate = null;

            foreach (Inlet inlet in op.Inlets)
            {
                double? number = inlet.TryGetConstantNumber();

                switch (inlet.GetDimensionEnum())
                {
                    case DimensionEnum.Signal:
                        signal = number;
                        break;

                    case DimensionEnum.Start:
                        start = number;
                        break;

                    case DimensionEnum.End:
                        end = number;
                        break;

                    case DimensionEnum.SamplingRate:
                        samplingRate = number;
                        break;
                }
            }

            For(signal, ResourceFormatter.Signal)
                .NotInfinity()
                .NotNaN();

            For(start, ResourceFormatter.Start)
                .NotInfinity()
                .NotNaN();

            For(end, ResourceFormatter.End)
                .NotInfinity()
                .NotNaN();

            For(samplingRate, ResourceFormatter.SamplingRate)
                .IsInteger()
                .GreaterThan(0.0);

            // ReSharper disable once InvertIf
            if (start.HasValue && end.HasValue)
            {
                if (end.Value < start.Value)
                {
                    Messages.AddLessThanMessage(ResourceFormatter.End, ResourceFormatter.Start);
                }
            }
        }
    }
}