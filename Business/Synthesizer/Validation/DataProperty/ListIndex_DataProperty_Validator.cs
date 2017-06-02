using JJ.Business.Synthesizer.EntityWrappers;
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
            string data = Obj;

            // ReSharper disable once InvertIf
            if (DataPropertyParser.DataIsWellFormed(data))
            {
                string listIndexString = DataPropertyParser.TryGetString(Obj, nameof(PatchInlet_OperatorWrapper.ListIndex));

                For(() => listIndexString, ResourceFormatter.ListIndex)
                    .NotNullOrEmpty()
                    .IsInteger()
                    .GreaterThanOrEqual(0);
            }
        }
    }
}
