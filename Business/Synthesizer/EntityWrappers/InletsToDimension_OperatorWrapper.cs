using JJ.Business.Synthesizer.Helpers;
using System.Collections.Generic;
using JJ.Framework.Exceptions;
using JJ.Business.Synthesizer.Resources;
using JJ.Business.Synthesizer.Enums;
using JJ.Data.Synthesizer.Entities;

namespace JJ.Business.Synthesizer.EntityWrappers
{
    public class InletsToDimension_OperatorWrapper : OperatorWrapperBase_WithSignalOutlet
    {
        public InletsToDimension_OperatorWrapper(Operator op)
            : base(op)
        { }

        /// <summary> Executes a loop, so prevent calling it multiple times. </summary>
        public IList<Outlet> Inputs => InletOutletSelector.GetSortedInputOutlets(WrappedOperator);

        /// <summary> Executes a loop, so prevent calling it multiple times. </summary>
        public IList<Inlet> Inlets => InletOutletSelector.GetSortedInlets(WrappedOperator);

        public ResampleInterpolationTypeEnum InterpolationType
        {
            get => DataPropertyParser.GetEnum<ResampleInterpolationTypeEnum>(WrappedOperator, nameof(InterpolationType));
            set => DataPropertyParser.SetValue(WrappedOperator, nameof(InterpolationType), value);
        }

        public override string GetInletDisplayName(Inlet inlet)
        {
            if (inlet == null) throw new NullException(() => inlet);

            string name = $"{ResourceFormatter.Inlet} {inlet.ListIndex + 1}";
            return name;
        }
    }
}