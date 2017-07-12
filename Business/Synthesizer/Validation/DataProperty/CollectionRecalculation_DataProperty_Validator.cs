using JJ.Business.Synthesizer.EntityWrappers;
using JJ.Business.Synthesizer.Enums;
using JJ.Business.Synthesizer.Helpers;
using JJ.Framework.Validation;

namespace JJ.Business.Synthesizer.Validation.DataProperty
{
    internal class CollectionRecalculation_DataProperty_Validator : VersatileValidator
    {
        public CollectionRecalculation_DataProperty_Validator(string data) 
        { 
            // ReSharper disable once InvertIf
            if (DataPropertyParser.DataIsWellFormed(data))
            {
                const string dataKey = nameof(OperatorWrapper_WithCollectionRecalculation.CollectionRecalculation);

                string collectionRecalculationString = DataPropertyParser.TryGetString(data, dataKey);

                For(collectionRecalculationString, dataKey)
                    .NotNullOrEmpty()
                    .IsEnum<CollectionRecalculationEnum>()
                    .IsNot(CollectionRecalculationEnum.Undefined);
            }
        }
    }
}
