using JJ.Data.Synthesizer;
using JJ.Business.Synthesizer.Enums;
using JJ.Business.Synthesizer.Helpers;
using System.Collections.Generic;
using System.Linq;
using JJ.Framework.Common;
using JJ.Business.Synthesizer.Validation.OperatorData;

namespace JJ.Business.Synthesizer.Validation.Operators
{
    internal class OperatorValidator_Base_WithDimension : OperatorValidator_Base
    {
        /// <param name="allowedDataKeys"> 
        /// Validator will allow Dimension data key whether it is in this list or not. 
        /// </param>
        public OperatorValidator_Base_WithDimension(
            Operator obj,
            OperatorTypeEnum expectedOperatorTypeEnum,
            int expectedInletCount,
            int expectedOutletCount,
            IList<string> allowedDataKeys = null)
            : base(
                  obj,
                  expectedOperatorTypeEnum,
                  expectedInletCount,
                  expectedOutletCount,
                  expectedDataKeys: PropertyNames.Dimension.Union(allowedDataKeys ?? new string[0]).ToArray())
        { }

        protected override void Execute()
        {
            base.Execute();

            Execute(new Dimension_OperatorData_Validator(Object.Data));
        }
    }
}
