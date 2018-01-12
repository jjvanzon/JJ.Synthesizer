using JJ.Framework.Presentation.VectorGraphics.Gestures;
using JJ.Framework.Presentation.VectorGraphics.Models.Elements;

namespace JJ.Presentation.Synthesizer.VectorGraphics
{
	public class MidiMappingDetailsViewModelToDiagramConverterResult
	{
		public Diagram Diagram { get; }
		public MoveGesture MoveGesture { get; }

		public MidiMappingDetailsViewModelToDiagramConverterResult(int doubleClickSpeedInMilliseconds, int doubleClickDeltaInPixels)
		{
			Diagram = new Diagram();
			MoveGesture = new MoveGesture();
		}
	}
}
