using JJ.Presentation.Synthesizer.Helpers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JJ.Presentation.Synthesizer.WinForms.EventArg
{
    internal class CreateOperatorEventArgs : EventArgs
    {
        public int OperatorTypeID { get; private set; }

        public CreateOperatorEventArgs(int operatorTypeID)
        {
            OperatorTypeID = operatorTypeID;
        }
    }
}