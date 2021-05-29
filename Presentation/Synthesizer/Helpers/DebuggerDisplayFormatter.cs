using System;
using System.Text;
using JJ.Framework.Exceptions.Basic;
using JJ.Framework.Logging;
using JJ.Presentation.Synthesizer.ViewModels;
using JJ.Presentation.Synthesizer.ViewModels.Items;

namespace JJ.Presentation.Synthesizer.Helpers
{
    internal static class DebuggerDisplayFormatter
    {
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

        public static string GetDebuggerDisplay(EntityTypeAndIDViewModel viewModel)
        {
            if (viewModel == null) throw new ArgumentNullException(nameof(viewModel));

            string debuggerDisplay = $"{{{viewModel.GetType().Name}}} {viewModel.EntityTypeEnum} {viewModel.EntityID}";
            return debuggerDisplay;
        }

        public static string GetDebuggerDisplay(InletViewModel viewModel)
        {
            if (viewModel == null) throw new NullException(() => viewModel);

            return CommonDebuggerDisplayFormatter.GetDebuggerDisplayWithIDAndName(viewModel.GetType(), viewModel.ID, viewModel.Caption);
        }

        public static string GetDebuggerDisplay(NodePropertiesViewModel viewModel)
        {
            if (viewModel == null) throw new ArgumentNullException(nameof(viewModel));
            string debuggerDisplay = $"{{{viewModel.GetType().Name}}} {GetIdentifier(viewModel.Entity)}";
            return debuggerDisplay;
        }

        public static string GetDebuggerDisplay(NodeViewModel viewModel)
        {
            if (viewModel == null) throw new ArgumentNullException(nameof(viewModel));
            string debuggerDisplay = $"{{{viewModel.GetType().Name}}} {GetIdentifier(viewModel)}";
            return debuggerDisplay;
        }

        private static string GetIdentifier(NodeViewModel viewModel)
        {
            if (viewModel == null) throw new ArgumentNullException(nameof(viewModel));
            string debuggerDisplay = $"{new { viewModel.X, viewModel.Y, Interpolation = viewModel.Interpolation?.Name }}";
            return debuggerDisplay;
        }

        public static string GetDebuggerDisplay(OperatorViewModel viewModel)
        {
            if (viewModel == null) throw new NullException(() => viewModel);

            return CommonDebuggerDisplayFormatter.GetDebuggerDisplayWithIDAndName(viewModel.GetType(), viewModel.ID, viewModel.Caption);
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

            return CommonDebuggerDisplayFormatter.GetDebuggerDisplayWithIDAndName(viewModel.GetType(), viewModel.ID, viewModel.Name);
        }

        public static string GetDebuggerDisplay(UndoItemViewModelBase viewModel) => CommonDebuggerDisplayFormatter.GetDebuggerDisplay(viewModel);

        public static string GetDebuggerDisplay(ScreenViewModelBase obj) => CommonDebuggerDisplayFormatter.GetDebuggerDisplay(obj);
    }
}