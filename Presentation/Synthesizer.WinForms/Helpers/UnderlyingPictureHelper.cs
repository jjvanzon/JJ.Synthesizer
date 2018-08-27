using JJ.Presentation.Synthesizer.VectorGraphics.Helpers;
using JJ.Presentation.Synthesizer.WinForms.Properties;

namespace JJ.Presentation.Synthesizer.WinForms.Helpers
{
    internal static class UnderlyingPictureHelper
    {
        public static UnderlyingPictureWrapper UnderlyingPictureWrapper { get; } = new UnderlyingPictureWrapper(
            Resources.AddIcon,
            Resources.PianoIcon,
            Resources.FolderIcon,
            Resources.CloseIcon,
            Resources.RemoveIcon,
            Resources.OpenWindowIcon,
            Resources.LessThanIcon,
            Resources.GreaterThanIcon,
            Resources.NewIcon,
            Resources.PlayIcon,
            Resources.RedoIcon,
            Resources.RefreshIcon,
            Resources.TextBoxIcon,
            Resources.SaveIcon,
            Resources.TreeStructureIcon,
            Resources.UndoIcon);
    }
}