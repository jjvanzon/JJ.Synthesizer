using JJ.Business.Synthesizer.Resources;
using JJ.Persistence.Synthesizer;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JJ.Business.Synthesizer.Validation
{
    /// <summary>
    /// Base class for operator validators that
    /// do not have a specialized entity.
    /// Verifies that the properties AsCurveIn, AsSampleOperator and AsValueOperator are null.
    /// </summary>
    public abstract class NonSpecializedOperatorValidatorBase : OperatorValidatorBase
    {
        public NonSpecializedOperatorValidatorBase(
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

            // TODO: Remove outcommented code.
            //For(() => Object.AsCurveIn, PropertyDisplayNames.AsCurveIn).IsNull();
            //For(() => Object.AsSampleOperator, PropertyDisplayNames.AsSampleOperator).IsNull();
            //For(() => Object.AsValueOperator, PropertyDisplayNames.AsValueOperator).IsNull();
        }
    }
}
