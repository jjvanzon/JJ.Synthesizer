using JJ.Framework.VectorGraphics.Gestures;
using JJ.Framework.VectorGraphics.Helpers;
using JJ.Framework.VectorGraphics.Models.Elements;
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
		public DeleteGesture DeleteGesture { get; }
		public ExpandKeyboardGesture ExpandNodeKeyboardGesture { get; }
		public ExpandMouseGesture ExpandNodeMouseGesture { get; }
		public KeyDownGesture KeyDownGesture { get; }
		public MoveGesture MoveNodeGesture { get; }
		public ToolTipGesture NodeToolTipGesture { get; }
		public SelectGesture SelectNodeGesture { get; }

		public CurveDetailsViewModelToDiagramConverterResult(
			ITextMeasurer textMeasurer,
			int doubleClickSpeedInMilliseconds,
			int doubleClickDeltaInPixels)
		{
			Diagram = new Diagram();

			BackgroundClickGesture = new ClickGesture();
			BackgroundDoubleClickGesture = new DoubleClickGesture(doubleClickSpeedInMilliseconds, doubleClickDeltaInPixels);
			ChangeNodeTypeGesture = new ChangeNodeTypeGesture();
			DeleteGesture = new DeleteGesture();
			ExpandNodeKeyboardGesture = new ExpandKeyboardGesture();
			ExpandNodeMouseGesture = new ExpandMouseGesture(doubleClickSpeedInMilliseconds, doubleClickDeltaInPixels);
			KeyDownGesture = new KeyDownGesture();
			MoveNodeGesture = new MoveGesture();
			NodeToolTipGesture = new ToolTipGesture(
				Diagram,
				StyleHelper.ToolTipBackStyle,
				StyleHelper.ToolTipLineStyle,
				StyleHelper.ToolTipTextStyle,
				textMeasurer);
			SelectNodeGesture = new SelectGesture();

			Diagram.Gestures.Add(DeleteGesture);
			Diagram.Gestures.Add(KeyDownGesture);
			Diagram.Background.Gestures.Add(BackgroundDoubleClickGesture);
			//2017-11-02: TODO: Somehow adding BackgroundClickGesture makes BackgroundDoubleClickGesture not work anymore.
			//Diagram.Background.Gestures.Add(BackgroundClickGesture);
			Diagram.Background.Gestures.Add(ChangeNodeTypeGesture);
			Diagram.Background.Gestures.Add(ExpandNodeKeyboardGesture);
		}
	}
}