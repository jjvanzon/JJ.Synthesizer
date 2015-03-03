using JJ.Business.Synthesizer.Resources;
using JJ.Framework.Validation;
using JJ.Persistence.Synthesizer;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JJ.Business.Synthesizer.Warnings
{
    public class AddOperatorWarningValidator : FluentValidator<Operator>
    {
        public AddOperatorWarningValidator(Operator obj)
            : base(obj)
        { }

        protected override void Execute()
        {
            foreach (Inlet inlet in Object.Inlets)
            {
                if (inlet.Input == null)
                {
                    ValidationMessages.Add("OperandNotSet", MessagesFormatter.OperandNotSet(Object.OperatorTypeName, Object.Name, inlet.Name)); // TODO: Use a better propertyKey. with an expression?
                }
            }
        }
    }
}
