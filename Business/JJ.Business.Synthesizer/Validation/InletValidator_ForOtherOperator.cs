using System;
using JJ.Business.Synthesizer.Resources;
using JJ.Data.Synthesizer;
using JJ.Framework.Presentation.Resources;
using JJ.Framework.Validation;

namespace JJ.Business.Synthesizer.Validation
{
    /// <summary> For operators other than CustomOperator. </summary>
    internal class InletValidator_ForOtherOperator : FluentValidator<Inlet>
    {
        private readonly int _expectedListIndex;

        /// <summary> For operators other than CustomOperator. </summary>
        public InletValidator_ForOtherOperator(Inlet obj, int expectedListIndex)
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
        }
    }
}
