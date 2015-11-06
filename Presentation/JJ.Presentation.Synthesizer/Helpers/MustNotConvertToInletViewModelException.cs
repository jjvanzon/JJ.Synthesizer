using JJ.Data.Synthesizer;
using System;
using JJ.Presentation.Synthesizer.Validators;

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
                    string inletIdentifier = GetInletIdentifier(_inlet);
                    return String.Format("Inlet '{0}' with no Operator should not be converted to InletViewModel.", inletIdentifier);
                }
                else if (!String.IsNullOrEmpty(_inlet.Operator.Name))
                {
                    string inletIdentifier = GetInletIdentifier(_inlet);
                    return String.Format("Inlet '{0}' of Operator '{1}' should not be converted to InletViewModel.", inletIdentifier, _inlet.Operator.Name);
                }
                else if (_inlet.Operator.OperatorType != null)
                {
                    string inletIdentifier = GetInletIdentifier(_inlet);
                    return String.Format("Inlet '{0}' of Operator of type '{1}' should not be converted to InletViewModel.", inletIdentifier, _inlet.Operator.OperatorType.Name);
                }
                else
                {
                    string inletIdentifier = GetInletIdentifier(_inlet);
                    return String.Format("Inlet '{0}' should not be converted to InletViewModel.", inletIdentifier);
                }
            }
        }

        private string GetInletIdentifier(Inlet inlet)
        {
            if (!String.IsNullOrEmpty(inlet.Name))
            {
                return inlet.Name;
            }

            return inlet.ID.ToString();
        }
    }
}
