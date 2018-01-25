using JJ.Framework.VectorGraphics.Gestures;
using JJ.Framework.VectorGraphics.Helpers;
using JJ.Framework.VectorGraphics.Models.Elements;
using JJ.Presentation.Synthesizer.VectorGraphics.Gestures;
using JJ.Presentation.Synthesizer.VectorGraphics.Helpers;

namespace JJ.Presentation.Synthesizer.VectorGraphics
{
	public class PatchViewModelToDiagramConverterResult
	{
		public Diagram Diagram { get; }
		public DeleteGesture DeleteOperatorGesture { get; }
		public DragLineGesture DragLineGesture { get; }
		public DropLineGesture DropLineGesture { get; }
		public ExpandKeyboardGesture ExpandOperatorKeyboardGesture { get; }
		public ExpandMouseGesture ExpandOperatorMouseGesture { get; }
		public DoubleClickGesture ExpandPatchGesture { get; }
		public ToolTipGesture InletToolTipGesture { get; }
		public MoveGesture MoveGesture { get; }
		public ToolTipGesture OutletToolTipGesture { get; }
		public SelectGesture SelectOperatorGesture { get; }
		public ClickGesture SelectPatchGesture { get; }

		public PatchViewModelToDiagramConverterResult(ITextMeasurer textMeasurer, int doubleClickSpeedInMilliseconds, int doubleClickDeltaInPixels)
		{
			Diagram = new Diagram();
			DeleteOperatorGesture = new DeleteGesture();

			DragLineGesture = new DragLineGesture(
				Diagram,
				StyleHelper.LineStyleDashed,
				StyleHelper.DRAG_DROP_LINE_ZINDEX);

			DropLineGesture = new DropLineGesture(
				Diagram,
				new[] { DragLineGesture },
				StyleHelper.LineStyleDashed,
				StyleHelper.DRAG_DROP_LINE_ZINDEX);

			ExpandOperatorKeyboardGesture = new ExpandKeyboardGesture();

			ExpandOperatorMouseGesture = new ExpandMouseGesture(
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
				textMeasurer,
				zIndex: 2);

			MoveGesture = new MoveGesture();

			OutletToolTipGesture = new ToolTipGesture(
				Diagram,
				StyleHelper.ToolTipBackStyle,
				StyleHelper.ToolTipLineStyle,
				StyleHelper.ToolTipTextStyle,
				textMeasurer,
				preferShowOnBottom: true,
				zIndex: 2);

			SelectOperatorGesture = new SelectGesture();
			SelectPatchGesture = new ClickGesture();
		}
	}
}