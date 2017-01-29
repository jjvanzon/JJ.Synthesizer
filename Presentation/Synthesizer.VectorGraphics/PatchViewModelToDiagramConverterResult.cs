using JJ.Framework.Presentation.VectorGraphics.Gestures;
using JJ.Framework.Presentation.VectorGraphics.Models.Elements;
using JJ.Presentation.Synthesizer.VectorGraphics.Gestures;
using JJ.Presentation.Synthesizer.VectorGraphics.Helpers;

namespace JJ.Presentation.Synthesizer.VectorGraphics
{
    public class PatchViewModelToDiagramConverterResult
    {
        public Diagram Diagram { get; }
        public MoveGesture MoveGesture { get; }
        public DragLineGesture DragLineGesture { get; }
        public DropLineGesture DropLineGesture { get; }
        public SelectOperatorGesture SelectOperatorGesture { get; }
        public DeleteOperatorGesture DeleteOperatorGesture { get; }
        public ToolTipGesture InletToolTipGesture { get; }
        public ToolTipGesture OutletToolTipGesture { get; }
        public ShowOperatorPropertiesKeyboardGesture ShowOperatorPropertiesKeyboardGesture { get; }
        public ShowOperatorPropertiesMouseGesture ShowOperatorPropertiesMouseGesture { get; }
        public ShowPatchPropertiesGesture ShowPatchPropertiesGesture { get; }

        public PatchViewModelToDiagramConverterResult(int doubleClickSpeedInMilliseconds, int doubleClickDeltaInPixels)
        {
            Diagram = new Diagram();
            MoveGesture = new MoveGesture();
            SelectOperatorGesture = new SelectOperatorGesture();
            DeleteOperatorGesture = new DeleteOperatorGesture();
            ShowOperatorPropertiesKeyboardGesture = new ShowOperatorPropertiesKeyboardGesture();

            ShowOperatorPropertiesMouseGesture = new ShowOperatorPropertiesMouseGesture(
                doubleClickSpeedInMilliseconds,
                doubleClickDeltaInPixels);

            ShowPatchPropertiesGesture = new ShowPatchPropertiesGesture(
                doubleClickSpeedInMilliseconds,
                doubleClickDeltaInPixels);

            DragLineGesture = new DragLineGesture(
                Diagram,
                StyleHelper.LineStyleDashed,
                StyleHelper.DRAG_DROP_LINE_ZINDEX);

            DropLineGesture = new DropLineGesture(
                Diagram,
                new[] { DragLineGesture },
                StyleHelper.LineStyleDashed,
                StyleHelper.DRAG_DROP_LINE_ZINDEX);

            InletToolTipGesture = new ToolTipGesture(
                Diagram,
                StyleHelper.ToolTipBackStyle,
                StyleHelper.ToolTipLineStyle,
                StyleHelper.ToolTipTextStyle,
                zIndex: 2);

            OutletToolTipGesture = new ToolTipGesture(
                Diagram,
                StyleHelper.ToolTipBackStyle,
                StyleHelper.ToolTipLineStyle,
                StyleHelper.ToolTipTextStyle,
                preferShowOnBottom: true,
                zIndex: 2);
        }
    }
}