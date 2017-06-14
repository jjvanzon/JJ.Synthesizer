using System;
using System.Linq;
using JetBrains.Annotations;
using JJ.Business.Synthesizer.Enums;
using JJ.Business.Synthesizer.Extensions;
using JJ.Data.Synthesizer.Entities;
using JJ.Data.Synthesizer.RepositoryInterfaces;
using JJ.Framework.Exceptions;

namespace JJ.Business.Synthesizer.EntityWrappers
{
    public class OperatorWrapper_WithUnderlyingPatch : OperatorWrapperBase
    {
        private readonly IDimensionRepository _dimensionRepository;

        public OperatorWrapper_WithUnderlyingPatch(Operator op, [NotNull] IDimensionRepository dimensionRepository)
            : base(op)
        {
            _dimensionRepository = dimensionRepository ?? throw new NullException(() => dimensionRepository);

            Inputs = new OperatorWrapper_WithUnderlyingPatch_Inputs(op);
            Inlets = new OperatorWrapper_WithUnderlyingPatch_Inlets(op);
            Outlets = new OperatorWrapper_WithUnderlyingPatch_Outlets(op);
        }

        public string Name
        {
            get => WrappedOperator.Name;
            set => WrappedOperator.Name = value;
        }

        public OperatorWrapper_WithUnderlyingPatch_Inputs Inputs { get; }

        public OperatorWrapper_WithUnderlyingPatch_Inlets Inlets { get; }

        public OperatorWrapper_WithUnderlyingPatch_Outlets Outlets { get; }

        public DimensionEnum StandardDimension
        {
            get => WrappedOperator.GetStandardDimensionEnum();
            set => WrappedOperator.SetStandardDimensionEnum(value, _dimensionRepository);
        }

        public string CustomDimension
        {
            get => WrappedOperator.CustomDimensionName;
            set => WrappedOperator.CustomDimensionName = value;
        }

        /// <summary> Implicit for syntactic sugar. </summary>
        public static implicit operator Outlet(OperatorWrapper_WithUnderlyingPatch wrapper) 
            => wrapper?.WrappedOperator.Outlets.Single();
    }
}
