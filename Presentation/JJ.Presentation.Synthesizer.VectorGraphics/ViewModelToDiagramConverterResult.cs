using JJ.Framework.Presentation.VectorGraphics.Gestures;
using JJ.Framework.Presentation.VectorGraphics.Models.Elements;
using JJ.Presentation.Synthesizer.VectorGraphics.Gestures;

namespace JJ.Presentation.Synthesizer.VectorGraphics
{
    public class ViewModelToDiagramConverterResult
    {
        public ViewModelToDiagramConverterResult(
            Diagram diagram,
            MoveGesture moveGesture,
            DragGesture dragGesture,
            DropGesture dropGesture,
            LineGesture lineGesture,
            SelectOperatorGesture selectOperatorGesture,
            DeleteOperatorGesture deleteOperatorGesture,
            DoubleClickGesture doubleClickOperatorGesture,
            ToolTipGesture operatorToolTipGesture,
            ToolTipGesture inletToolTipGesture,
            ToolTipGesture outletToolTipGesture)
        {
            Diagram = diagram;
            MoveGesture = moveGesture;
            DragGesture = dragGesture;
            DropGesture = dropGesture;
            LineGesture = lineGesture;
            SelectOperatorGesture = selectOperatorGesture;
            DeleteOperatorGesture = deleteOperatorGesture;
            DoubleClickOperatorGesture = doubleClickOperatorGesture;
            OperatorToolTipGesture = operatorToolTipGesture;
            InletToolTipGesture = inletToolTipGesture;
            OutletToolTipGesture = outletToolTipGesture;
        }

        public Diagram Diagram { get; private set; }
        public MoveGesture MoveGesture { get; private set; }
        public DragGesture DragGesture { get; private set; }
        public DropGesture DropGesture { get; private set; }
        public LineGesture LineGesture { get; private set; }
        public SelectOperatorGesture SelectOperatorGesture { get; private set; }
        public DeleteOperatorGesture DeleteOperatorGesture { get; private set; }
        public DoubleClickGesture DoubleClickOperatorGesture { get; private set; }
        public ToolTipGesture OperatorToolTipGesture { get; private set; }
        public ToolTipGesture InletToolTipGesture { get; private set; }
        public ToolTipGesture OutletToolTipGesture { get; private set; }
    }
}
