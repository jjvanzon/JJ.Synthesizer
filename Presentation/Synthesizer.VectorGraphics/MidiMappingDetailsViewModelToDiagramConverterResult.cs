using JJ.Framework.Presentation.VectorGraphics.Gestures;
using JJ.Framework.Presentation.VectorGraphics.Models.Elements;
using JJ.Presentation.Synthesizer.VectorGraphics.Gestures;

namespace JJ.Presentation.Synthesizer.VectorGraphics
{
	public class MidiMappingDetailsViewModelToDiagramConverterResult
	{
		public Diagram Diagram { get; }
		public MoveGesture MoveGesture { get; }
		public SelectGesture SelectElementGesture { get; }
		public DeleteGesture DeleteElementGesture { get; }
		public DoubleClickGesture ExpandElementGesture { get; }


		public MidiMappingDetailsViewModelToDiagramConverterResult(int doubleClickSpeedInMilliseconds, int doubleClickDeltaInPixels)
		{
			Diagram = new Diagram();
			MoveGesture = new MoveGesture();
			SelectElementGesture = new SelectGesture();
			DeleteElementGesture = new DeleteGesture();
			ExpandElementGesture = new DoubleClickGesture(doubleClickSpeedInMilliseconds, doubleClickDeltaInPixels);
		}
	}
}
