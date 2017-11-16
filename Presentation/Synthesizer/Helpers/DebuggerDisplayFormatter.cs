using System;
using System.Text;
using JJ.Framework.Exceptions;
using JJ.Framework.Logging;
using JJ.Presentation.Synthesizer.ViewModels;
using JJ.Presentation.Synthesizer.ViewModels.Items;

namespace JJ.Presentation.Synthesizer.Helpers
{
    internal static class DebuggerDisplayFormatter
    {
        public static string GetDebuggerDisplay(OperatorViewModel viewModel)
        {
            if (viewModel == null) throw new NullException(() => viewModel);

            return CommonDebuggerDisplayFormatter.GetDebuggDisplayWithIDAndName(viewModel.GetType(), viewModel.ID, viewModel.Caption);
        }

        public static string GetDebuggerDisplay(InletViewModel viewModel)
        {
            if (viewModel == null) throw new NullException(() => viewModel);

            return CommonDebuggerDisplayFormatter.GetDebuggDisplayWithIDAndName(viewModel.GetType(), viewModel.ID, viewModel.Caption);
        }

        public static string GetDebuggerDisplay(OutletViewModel viewModel)
        {
            if (viewModel == null) throw new NullException(() => viewModel);

            var sb = new StringBuilder();

            if (viewModel.Operator != null)
            {
                string operatorDebuggerDisplay = GetDebuggerDisplay(viewModel.Operator);
                sb.Append(operatorDebuggerDisplay);
                sb.Append(" - ");
            }
            else
            {
                sb.Append("Operator is null - ");
            }

            sb.Append($"{{{viewModel.GetType().Name}}} ");

            if (!string.IsNullOrEmpty(viewModel.Name))
            {
                sb.Append($"'{viewModel.Name}' ");
            }

            sb.Append($"({viewModel.ID})");

            return sb.ToString();
        }

        public static string GetDebuggerDisplay(PatchPropertiesViewModel viewModel)
        {
            if (viewModel == null) throw new ArgumentNullException(nameof(viewModel));

            return CommonDebuggerDisplayFormatter.GetDebuggDisplayWithIDAndName(viewModel.GetType(), viewModel.ID, viewModel.Name);
        }

        public static string GetDebuggerDisplay(AudioOutputPropertiesViewModel viewModel)
        {
            if (viewModel == null) throw new ArgumentNullException(nameof(viewModel));

            var sb = new StringBuilder();

            sb.Append($"{{{viewModel.GetType().Name}}} ");

            AudioOutputViewModel entityViewModel = viewModel.Entity;

            if (entityViewModel == null)
            {
                sb.Append($"{nameof(viewModel.Entity)} is null.");
            }
            else
            {
                var identifier = new
                {
                    entityViewModel.ID,
                    entityViewModel.SamplingRate,
                    SpeakerSetup = entityViewModel.SpeakerSetup.Name,
                    entityViewModel.MaxConcurrentNotes,
                    entityViewModel.DesiredBufferDuration
                };

                sb.Append($"{identifier}");
            }

            return sb.ToString();
        }
    }
}