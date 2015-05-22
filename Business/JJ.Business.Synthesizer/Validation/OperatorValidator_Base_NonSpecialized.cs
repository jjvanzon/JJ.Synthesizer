using JJ.Business.Synthesizer.Resources;
using JJ.Data.Synthesizer;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JJ.Business.Synthesizer.Validation
{
    /// <summary>
    /// Base class for operator validators that do not have additional data.
    /// Verifies that the Data property is null.
    /// </summary>
    public abstract class OperatorValidator_Base_NonSpecialized : OperatorValidator_Base
    {
        public OperatorValidator_Base_NonSpecialized(
            Operator obj,
            string expectedOperatorTypeName,
            int expectedInletCount,
            params string[] expectedInletAndOutletNames)
            : base(obj, expectedOperatorTypeName, expectedInletCount, expectedInletAndOutletNames)
        { }

        protected override void Execute()
        {
            base.Execute();

            For(() => Object.Data, PropertyDisplayNames.Data).IsNull();
        }
    }
}
