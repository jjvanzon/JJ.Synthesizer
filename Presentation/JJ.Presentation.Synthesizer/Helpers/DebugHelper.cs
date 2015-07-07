using JJ.Framework.Reflection.Exceptions;
using JJ.Presentation.Synthesizer.ViewModels.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JJ.Presentation.Synthesizer.Helpers
{
    internal static class DebugHelper
    {
        public static string GetDebuggerDisplay(OperatorViewModel viewModel)
        {
            if (viewModel == null) throw new NullException(() => viewModel);

            string formattedID;
            if (viewModel.Keys == null)
            {
                formattedID = "Keys is null";
            }
            else
            {
                formattedID = viewModel.Keys.ID.ToString();
            }

            return String.Format("Operator '{0}' ({1})", viewModel.Name, formattedID);
        }

        public static string GetDebuggerDisplay(InletViewModel viewModel)
        {
            if (viewModel == null) throw new NullException(() => viewModel);

            string formattedID;
            if (viewModel.Keys == null)
            {
                formattedID = "Keys is null";
            }
            else
            {
                formattedID = viewModel.Keys.ID.ToString();
            }

            return String.Format("Inlet '{0}'", viewModel.Name, formattedID);
        }

        public static string GetDebuggerDisplay(OutletViewModel viewModel)
        {
            if (viewModel == null) throw new NullException(() => viewModel);

            string formattedOperatorName;
            if (viewModel.Operator == null)
            {
                formattedOperatorName = "Operator is null";
            }
            else
            {
                formattedOperatorName = viewModel.Operator.Name;
            }

            return String.Format("Outlet '{0}' {1}", formattedOperatorName, viewModel.Name);
        }
    }
}
