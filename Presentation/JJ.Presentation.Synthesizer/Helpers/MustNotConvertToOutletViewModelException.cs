using JJ.Data.Synthesizer;
using System;
using System.Collections.Generic;
using System.Linq;

namespace JJ.Presentation.Synthesizer.Helpers
{
    public class MustNotConvertToOutletViewModelException : Exception
    {
        private Outlet _outlet;

        public MustNotConvertToOutletViewModelException(Outlet outlet)
        {
            _outlet = outlet;
        }

        public override string Message
        {
            get
            {
                if (_outlet == null)
                {
                    return "Null Outlet should not be converted to OutletViewModel.";
                }
                else if (_outlet.Operator == null)
                {
                    return String.Format("Outlet '{0}' with no Operator should not be converted to OutletViewModel.", _outlet.Name);
                }
                else if (!String.IsNullOrEmpty(_outlet.Operator.Name))
                {
                    return String.Format("Outlet '{0}' of Operator '{1}' should not be converted to OutletViewModel.", _outlet.Name, _outlet.Operator.Name);

                }
                else if (_outlet.Operator.OperatorType != null)
                {
                    return String.Format("Outlet '{0}' of Operator of type '{1}' should not be converted to OutletViewModel.", _outlet.Name, _outlet.Operator.OperatorType.Name);
                }
                else
                {
                    return String.Format("Outlet '{0}' should not be converted to OutletViewModel.", _outlet.Name);
                }
            }
        }
    }
}
