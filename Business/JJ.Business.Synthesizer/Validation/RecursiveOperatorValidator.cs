using JJ.Business.Synthesizer.Names;
using JJ.Framework.Validation;
using JJ.Persistence.Synthesizer;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JJ.Business.Synthesizer.Validation
{
    public class RecursiveOperatorValidator : ValidatorBase<Operator>
    {
        private ISet<object> _alreadyDone;

        public RecursiveOperatorValidator(Operator obj, ISet<object> alreadyDone = null)
            : base(obj, postponeExecute: true)
        { 
            _alreadyDone = alreadyDone ?? new HashSet<object>();

            Execute();
        }

        protected override void Execute()
        {
            if (_alreadyDone.Contains(Object)) return;
            _alreadyDone.Add(Object);

            Execute<BasicOperatorValidator>();

            if (String.Equals(Object.OperatorTypeName, PropertyNames.ValueOperator)) 
            {
                Execute<ValueOperatorValidator>();
            }
            else if (String.Equals(Object.OperatorTypeName, PropertyNames.Add))
            {
                Execute<AddValidator>();
            }
            else if (String.Equals(Object.OperatorTypeName, PropertyNames.Substract))
            {
                Execute<SubstractValidator>();
            }
            else if (String.Equals(Object.OperatorTypeName, PropertyNames.Multiply))
            {
                Execute<MultiplyValidator>();
            }
            else
            {
                throw new Exception(String.Format("OperatorTypeName value '{0}' is not supported.", Object.OperatorTypeName));
            }
            
            foreach (Inlet inlet in Object.Inlets)
            {
                if (inlet.Input != null)
                {
                    Execute(new RecursiveOperatorValidator(inlet.Input.Operator, _alreadyDone));
                }
            }
        }
    }
}
