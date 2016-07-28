using System;
using System.Collections.Generic;
using System.Linq;
using JJ.Framework.Presentation.VectorGraphics.Enums;
using JJ.Framework.Presentation.VectorGraphics.EventArg;
using JJ.Framework.Presentation.VectorGraphics.Gestures;
using JJ.Presentation.Synthesizer.VectorGraphics.EventArg;

namespace JJ.Presentation.Synthesizer.VectorGraphics.Gestures
{
    /// <summary>
    /// Keyboard gesture must be separated from the mouse gesture,
    /// because keyword gesture is tied to the whole diagram,
    /// while mouse gesture is tied to a specific element.
    /// </summary>
    public class ShowNodePropertiesKeyboardGesture : GestureBase
    {
        public event EventHandler<IDEventArgs> ShowNodePropertiesRequested;

        public int SelectedNodeID { get; set; }

        protected override void HandleKeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == KeyCodeEnum.Enter)
            {
                ShowNodePropertiesRequested?.Invoke(this, new IDEventArgs(SelectedNodeID));
            }
        }
    }
}
