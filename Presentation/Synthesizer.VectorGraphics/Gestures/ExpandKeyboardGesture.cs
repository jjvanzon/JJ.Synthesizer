using System;
using JJ.Framework.VectorGraphics.Enums;
using JJ.Framework.VectorGraphics.EventArg;
using JJ.Framework.VectorGraphics.Gestures;
using JJ.Presentation.Synthesizer.VectorGraphics.EventArg;
// ReSharper disable MemberCanBePrivate.Global

namespace JJ.Presentation.Synthesizer.VectorGraphics.Gestures
{
    /// <summary>
    /// Keyboard gesture must be separated from the mouse gesture,
    /// because keyboard gesture is tied to the whole diagram,
    /// while mouse gesture is tied to a specific element.
    /// </summary>
    public class ExpandKeyboardGesture : GestureBase
    {
        public event EventHandler<IDEventArgs> ExpandRequested;

        public int? SelectedEntityID { get; set; }

        protected override void HandleKeyDown(object sender, KeyEventArgs e)
        {
            if (!SelectedEntityID.HasValue)
            {
                return;
            }

            if (e.KeyCode == KeyCodeEnum.Enter)
            {
                ExpandRequested?.Invoke(sender, new IDEventArgs(SelectedEntityID.Value));
            }
        }
    }
}
