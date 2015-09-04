using JJ.Framework.Presentation.Svg.Gestures;
using JJ.Framework.Presentation.Svg.Models.Elements;
using JJ.Presentation.Synthesizer.Svg.Gestures;

namespace JJ.Presentation.Synthesizer.Svg
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
        public ToolTipGesture OperatorToolTipGesture { get; private set; }
        public ToolTipGesture InletToolTipGesture { get; private set; }
        public ToolTipGesture OutletToolTipGesture { get; private set; }
    }
}
