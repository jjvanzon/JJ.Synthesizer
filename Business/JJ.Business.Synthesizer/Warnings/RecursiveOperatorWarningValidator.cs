using JJ.Business.Synthesizer.Names;
using JJ.Framework.Validation;
using JJ.Persistence.Synthesizer;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JJ.Business.Synthesizer.Warnings
{
    public class RecursiveOperatorWarningValidator : ValidatorBase<Operator>
    {
        private ISet<object> _alreadyDone;

        public RecursiveOperatorWarningValidator(Operator obj, ISet<object> alreadyDone = null)
            : base(obj, postponeExecute: true)
        {
            _alreadyDone = alreadyDone ?? new HashSet<object>();

            Execute();
        }

        protected override void Execute()
        {
            // Handle circularity
            if (_alreadyDone.Contains(Object)) return;
            _alreadyDone.Add(Object);

            if (String.Equals(Object.OperatorTypeName, PropertyNames.Value)) 
            {
                Execute<ValueWarningValidator>();
            }
            else if (String.Equals(Object.OperatorTypeName, PropertyNames.Add))
            {
                Execute<AddWarningValidator>();
            }
            else if (String.Equals(Object.OperatorTypeName, PropertyNames.Substract))
            {
                Execute<SubstractWarningValidator>();
            }
            else if (String.Equals(Object.OperatorTypeName, PropertyNames.Multiply))
            {
                Execute<MultiplyWarningValidator>();
            }
            else
            {
                throw new Exception(String.Format("OperatorTypeName value '{0}' is not supported.", Object.OperatorTypeName));
            }
            
            foreach (Inlet inlet in Object.Inlets)
            {
                if (inlet.Input != null)
                {
                    Execute(new RecursiveOperatorWarningValidator(inlet.Input.Operator, _alreadyDone));
                }
            }
        }
    }
}
