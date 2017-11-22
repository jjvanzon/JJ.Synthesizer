using JJ.Framework.Presentation.VectorGraphics.Gestures;
using JJ.Framework.Presentation.VectorGraphics.Models.Elements;
using JJ.Presentation.Synthesizer.VectorGraphics.Gestures;
using JJ.Presentation.Synthesizer.VectorGraphics.Helpers;

namespace JJ.Presentation.Synthesizer.VectorGraphics
{
	public class PatchViewModelToDiagramConverterResult
	{
		public Diagram Diagram { get; }
		public DeleteOperatorGesture DeleteOperatorGesture { get; }
		public DragLineGesture DragLineGesture { get; }
		public DropLineGesture DropLineGesture { get; }
		public ExpandOperatorKeyboardGesture ExpandOperatorKeyboardGesture { get; }
		public ExpandOperatorMouseGesture ExpandOperatorMouseGesture { get; }
		public DoubleClickGesture ExpandPatchGesture { get; }
		public ToolTipGesture InletToolTipGesture { get; }
		public MoveGesture MoveGesture { get; }
		public ToolTipGesture OutletToolTipGesture { get; }
		public SelectOperatorGesture SelectOperatorGesture { get; }
		public ClickGesture SelectPatchGesture { get; }

		public PatchViewModelToDiagramConverterResult(int doubleClickSpeedInMilliseconds, int doubleClickDeltaInPixels)
		{
			Diagram = new Diagram();
			DeleteOperatorGesture = new DeleteOperatorGesture();

			DragLineGesture = new DragLineGesture(
				Diagram,
				StyleHelper.LineStyleDashed,
				StyleHelper.DRAG_DROP_LINE_ZINDEX);

			DropLineGesture = new DropLineGesture(
				Diagram,
				new[] { DragLineGesture },
				StyleHelper.LineStyleDashed,
				StyleHelper.DRAG_DROP_LINE_ZINDEX);

			ExpandOperatorKeyboardGesture = new ExpandOperatorKeyboardGesture();

			ExpandOperatorMouseGesture = new ExpandOperatorMouseGesture(
				doubleClickSpeedInMilliseconds,
				doubleClickDeltaInPixels);

			ExpandPatchGesture = new DoubleClickGesture(
				doubleClickSpeedInMilliseconds,
				doubleClickDeltaInPixels);

			InletToolTipGesture = new ToolTipGesture(
				Diagram,
				StyleHelper.ToolTipBackStyle,
				StyleHelper.ToolTipLineStyle,
				StyleHelper.ToolTipTextStyle,
				zIndex: 2);

			MoveGesture = new MoveGesture();

			OutletToolTipGesture = new ToolTipGesture(
				Diagram,
				StyleHelper.ToolTipBackStyle,
				StyleHelper.ToolTipLineStyle,
				StyleHelper.ToolTipTextStyle,
				preferShowOnBottom: true,
				zIndex: 2);

			SelectOperatorGesture = new SelectOperatorGesture();
			SelectPatchGesture = new ClickGesture();
		}
	}
}