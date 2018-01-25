using JJ.Framework.VectorGraphics.Gestures;
using JJ.Framework.VectorGraphics.Models.Elements;
using JJ.Presentation.Synthesizer.VectorGraphics.Gestures;

namespace JJ.Presentation.Synthesizer.VectorGraphics
{
	public class MidiMappingDetailsViewModelToDiagramConverterResult
	{
		public Diagram Diagram { get; }
		public DeleteGesture DeleteElementGesture { get; }
		public ExpandKeyboardGesture ExpandElementKeyboardGesture { get; }
		public ExpandMouseGesture ExpandElementMouseGesture { get; }
		public GridSnapGesture GridSnapGesture { get; }
		public MoveGesture MoveGesture { get; }
		public SelectGesture SelectElementGesture { get; }

		public MidiMappingDetailsViewModelToDiagramConverterResult(int doubleClickSpeedInMilliseconds, int doubleClickDeltaInPixels)
		{
			Diagram = new Diagram();
			DeleteElementGesture = new DeleteGesture();
			ExpandElementKeyboardGesture = new ExpandKeyboardGesture();
			ExpandElementMouseGesture = new ExpandMouseGesture(doubleClickSpeedInMilliseconds, doubleClickDeltaInPixels);
			MoveGesture = new MoveGesture();
			SelectElementGesture = new SelectGesture();
			GridSnapGesture = new GridSnapGesture(MoveGesture);
		}
	}
}
