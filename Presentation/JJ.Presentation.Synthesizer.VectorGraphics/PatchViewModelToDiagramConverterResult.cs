using JJ.Framework.Presentation.VectorGraphics.Gestures;
using JJ.Framework.Presentation.VectorGraphics.Models.Elements;
using JJ.Presentation.Synthesizer.VectorGraphics.Gestures;
using JJ.Presentation.Synthesizer.VectorGraphics.Helpers;

namespace JJ.Presentation.Synthesizer.VectorGraphics
{
    public class PatchViewModelToDiagramConverterResult
    {
        public PatchViewModelToDiagramConverterResult(int doubleClickSpeedInMilliseconds, int doubleClickDeltaInPixels)
        {
            Diagram = new Diagram();
            MoveGesture = new MoveGesture();
            DragLineGesture = new DragLineGesture(Diagram, StyleHelper.LineStyleDashed, StyleHelper.DRAG_DROP_LINE_ZINDEX);
            DropLineGesture = new DropLineGesture(
                Diagram, new DragLineGesture[] { DragLineGesture }, StyleHelper.LineStyleDashed, StyleHelper.DRAG_DROP_LINE_ZINDEX);
            SelectOperatorGesture = new SelectOperatorGesture();
            DeleteOperatorGesture = new DeleteOperatorGesture();
            DoubleClickOperatorGesture = new DoubleClickGesture(doubleClickSpeedInMilliseconds, doubleClickDeltaInPixels);
            OperatorToolTipGesture = new ToolTipGesture(Diagram, StyleHelper.ToolTipBackStyle, StyleHelper.ToolTipLineStyle, StyleHelper.ToolTipTextStyle, zIndex: 2);
            InletToolTipGesture = new ToolTipGesture(Diagram, StyleHelper.ToolTipBackStyle, StyleHelper.ToolTipLineStyle, StyleHelper.ToolTipTextStyle, zIndex: 2);
            OutletToolTipGesture = new ToolTipGesture(Diagram, StyleHelper.ToolTipBackStyle, StyleHelper.ToolTipLineStyle, StyleHelper.ToolTipTextStyle, zIndex: 2);
            OperatorEnterKeyGesture = new OperatorEnterKeyGesture();
        }

        public Diagram Diagram { get; private set; }
        public MoveGesture MoveGesture { get; private set; }
        public DragLineGesture DragLineGesture { get; private set; }
        public DropLineGesture DropLineGesture { get; private set; }
        public SelectOperatorGesture SelectOperatorGesture { get; private set; }
        public DeleteOperatorGesture DeleteOperatorGesture { get; private set; }
        public DoubleClickGesture DoubleClickOperatorGesture { get; private set; }
        public ToolTipGesture OperatorToolTipGesture { get; private set; }
        public ToolTipGesture InletToolTipGesture { get; private set; }
        public ToolTipGesture OutletToolTipGesture { get; private set; }
        public OperatorEnterKeyGesture OperatorEnterKeyGesture { get; private set; }
    }
}
