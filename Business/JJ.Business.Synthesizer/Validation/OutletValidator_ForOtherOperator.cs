using System;
using JJ.Business.Synthesizer.Enums;
using JJ.Business.Synthesizer.Extensions;
using JJ.Business.Synthesizer.Resources;
using JJ.Data.Synthesizer;
using JJ.Framework.Presentation.Resources;
using JJ.Framework.Validation;

namespace JJ.Business.Synthesizer.Validation
{
    /// <summary> For operators other than CustomOperator. </summary>
    internal class OutletValidator_ForOtherOperator : FluentValidator<Outlet>
    {
        private readonly int _expectedListIndex;

        /// <summary> For operators other than CustomOperator. </summary>
        public OutletValidator_ForOtherOperator(Outlet obj, int expectedListIndex)
            : base(obj, postponeExecute: true)
        {
            _expectedListIndex = expectedListIndex;

            Execute();
        }

        protected override void Execute()
        {
            For(() => Object.Name, CommonTitles.Name).IsNullOrEmpty();

            // ListIndex check DOES NOT apply to CustomOperators, 
            // because those need to be more flexible or it would become unmanageable for the user.
            For(() => Object.ListIndex, PropertyDisplayNames.ListIndex).Is(_expectedListIndex);

            For(() => Object.GetDimensionEnum(), PropertyDisplayNames.Dimension).IsEnum<DimensionEnum>();
        }
    }
}
