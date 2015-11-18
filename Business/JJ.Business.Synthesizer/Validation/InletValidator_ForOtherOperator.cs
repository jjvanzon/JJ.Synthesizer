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
        /// <summary> For operators other than CustomOperator. </summary>
        public InletValidator_ForOtherOperator(Inlet obj)
            : base(obj)
        { }

        protected override void Execute()
        {
            For(() => Object.Name, CommonTitles.Name).IsNullOrEmpty();
        }
    }
}
