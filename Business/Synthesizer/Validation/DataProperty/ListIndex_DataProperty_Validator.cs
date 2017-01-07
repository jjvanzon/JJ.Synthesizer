using JJ.Business.Synthesizer.Helpers;
using JJ.Business.Synthesizer.Resources;
using JJ.Framework.Validation;

namespace JJ.Business.Synthesizer.Validation.DataProperty
{
    internal class ListIndex_DataProperty_Validator : VersatileValidator_WithoutConstructorArgumentNullCheck<string>
    {
        public ListIndex_DataProperty_Validator(string obj) 
            : base(obj)
        { }

        protected override void Execute()
        {
            string data = Object;

            if (DataPropertyParser.DataIsWellFormed(data))
            {
                string listIndexString = DataPropertyParser.TryGetString(Object, PropertyNames.ListIndex);

                For(() => listIndexString, PropertyDisplayNames.ListIndex)
                    .NotNullOrEmpty()
                    .IsInteger()
                    .GreaterThanOrEqual(0);
            }
        }
    }
}
