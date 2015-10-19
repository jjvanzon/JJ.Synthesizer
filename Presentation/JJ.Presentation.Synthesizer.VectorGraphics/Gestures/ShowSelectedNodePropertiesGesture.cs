using System;
using System.Collections.Generic;
using System.Linq;
using JJ.Framework.Presentation.VectorGraphics.Enums;
using JJ.Framework.Presentation.VectorGraphics.EventArg;
using JJ.Framework.Presentation.VectorGraphics.Gestures;

namespace JJ.Presentation.Synthesizer.VectorGraphics.Gestures
{
    public class ShowSelectedNodePropertiesGesture : GestureBase
    {
        public event EventHandler ShowSelectedNodePropertiesRequested;

        protected override void HandleKeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == KeyCodeEnum.Enter)
            {
                if (ShowSelectedNodePropertiesRequested != null)
                {
                    ShowSelectedNodePropertiesRequested(this, EventArgs.Empty);
                }
            }
        }
    }
}
