using JJ.Framework.Presentation.VectorGraphics.Gestures;
using JJ.Framework.Presentation.VectorGraphics.Models.Elements;
using JJ.Presentation.Synthesizer.VectorGraphics.Gestures;
using JJ.Presentation.Synthesizer.VectorGraphics.Helpers;

namespace JJ.Presentation.Synthesizer.VectorGraphics
{
    public class CurveDetailsViewModelToDiagramConverterResult
    {
        public Diagram Diagram { get; }
        public ClickGesture BackgroundClickGesture { get; set; }
        public DoubleClickGesture BackgroundDoubleClickGesture { get; }
        public ChangeNodeTypeGesture ChangeNodeTypeGesture { get; }
        public KeyDownGesture KeyDownGesture { get; }
        public MoveGesture MoveNodeGesture { get; }
        public ToolTipGesture NodeToolTipGesture { get; }
        public SelectNodeGesture SelectNodeGesture { get; }
        public ClickGesture SelectPatchGesture { get; }
        public ExpandNodeKeyboardGesture ExpandNodeKeyboardGesture { get; }
        public ExpandNodeMouseGesture ExpandNodeMouseGesture { get; }

        public CurveDetailsViewModelToDiagramConverterResult(int doubleClickSpeedInMilliseconds, int doubleClickDeltaInPixels)
        {
            Diagram = new Diagram();

            BackgroundClickGesture = new ClickGesture();
            BackgroundDoubleClickGesture = new DoubleClickGesture(
                doubleClickSpeedInMilliseconds,
                doubleClickDeltaInPixels);
            ChangeNodeTypeGesture = new ChangeNodeTypeGesture();
            KeyDownGesture = new KeyDownGesture();
            MoveNodeGesture = new MoveGesture();
            NodeToolTipGesture = new ToolTipGesture(
                Diagram,
                StyleHelper.ToolTipBackStyle,
                StyleHelper.ToolTipLineStyle,
                StyleHelper.ToolTipTextStyle);
            SelectNodeGesture = new SelectNodeGesture();
            ExpandNodeKeyboardGesture = new ExpandNodeKeyboardGesture();
            ExpandNodeMouseGesture = new ExpandNodeMouseGesture(
                doubleClickSpeedInMilliseconds,
                doubleClickDeltaInPixels);

            Diagram.Gestures.Add(KeyDownGesture);
            Diagram.Background.Gestures.Add(BackgroundDoubleClickGesture);
            //2017-11-02: TODO: Somehow adding BackgroundClickGesture makes BackgroundDoubleClickGesture not work anymore.
            //Diagram.Background.Gestures.Add(BackgroundClickGesture);
            Diagram.Background.Gestures.Add(ChangeNodeTypeGesture);
            Diagram.Background.Gestures.Add(ExpandNodeKeyboardGesture);
        }
    }
}