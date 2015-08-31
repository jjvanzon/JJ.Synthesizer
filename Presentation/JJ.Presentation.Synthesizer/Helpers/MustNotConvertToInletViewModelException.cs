using JJ.Data.Synthesizer;
using System;
using System.Collections.Generic;
using System.Linq;

namespace JJ.Presentation.Synthesizer.Helpers
{
    public class MustNotConvertToInletViewModelException : Exception
    {
        private Inlet _inlet;

        public MustNotConvertToInletViewModelException(Inlet inlet)
        {
            _inlet = inlet;
        }

        public override string Message
        {
            get
            {
                if (_inlet == null)
                {
                    return "Null Inlet should not be converted to InletViewModel.";
                }
                else if (_inlet.Operator == null)
                {
                    return String.Format("Inlet '{0}' with no Operator should not be converted to InletViewModel.", _inlet.Name);
                }
                else if (!String.IsNullOrEmpty(_inlet.Operator.Name))
                {
                    return String.Format("Inlet '{0}' of Operator '{1}' should not be converted to InletViewModel.", _inlet.Name, _inlet.Operator.Name);

                }
                else if (_inlet.Operator.OperatorType != null)
                {
                    return String.Format("Inlet '{0}' of Operator of type '{1}' should not be converted to InletViewModel.", _inlet.Name, _inlet.Operator.OperatorType.Name);
                }
                else
                {
                    return String.Format("Inlet '{0}' should not be converted to InletViewModel.", _inlet.Name);
                }
            }
        }
    }
}
