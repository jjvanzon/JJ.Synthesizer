using System;
using System.Collections.Generic;
using System.Linq;
using JJ.Business.Synthesizer.Enums;
using JJ.Business.Synthesizer.Helpers;
using JJ.Framework.Validation;

namespace JJ.Business.Synthesizer.Validation.OperatorData
{
    internal class CollectionRecalculation_OperatorData_Validator : FluentValidator_WithoutConstructorArgumentNullCheck<string>
    {
        public CollectionRecalculation_OperatorData_Validator(string obj) 
            : base(obj)
        { }

        protected override void Execute()
        {
            string data = Object;

            if (DataPropertyParser.DataIsWellFormed(data))
            {
                string collectionRecalculationString = DataPropertyParser.TryGetString(data, PropertyNames.CollectionRecalculation);
                For(() => collectionRecalculationString, PropertyNames.CollectionRecalculation)
                    .NotNullOrEmpty()
                    .IsEnum<CollectionRecalculationEnum>()
                    .IsNot(CollectionRecalculationEnum.Undefined);
            }
        }
    }
}
