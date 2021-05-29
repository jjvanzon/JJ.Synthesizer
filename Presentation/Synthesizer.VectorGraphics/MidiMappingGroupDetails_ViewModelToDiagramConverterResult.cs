using JJ.Framework.VectorGraphics.Gestures;
using JJ.Framework.VectorGraphics.Models.Elements;
using JJ.Presentation.Synthesizer.VectorGraphics.Gestures;

namespace JJ.Presentation.Synthesizer.VectorGraphics
{
    public class MidiMappingGroupDetails_ViewModelToDiagramConverterResult
    {
        public Diagram Diagram { get; }
        public DeleteGesture DeleteMidiMappingGesture { get; }
        public ExpandKeyboardGesture ExpandMidiMappingKeyboardGesture { get; }
        public ExpandMouseGesture ExpandMidiMappingMouseGesture { get; }
        public GridSnapGesture GridSnapGesture { get; }
        public MoveGesture MoveGesture { get; }
        public SelectGesture SelectMidiMappingGesture { get; }

        public MidiMappingGroupDetails_ViewModelToDiagramConverterResult(int doubleClickSpeedInMilliseconds, int doubleClickDeltaInPixels)
        {
            Diagram = new Diagram();
            DeleteMidiMappingGesture = new DeleteGesture();
            ExpandMidiMappingKeyboardGesture = new ExpandKeyboardGesture();
            ExpandMidiMappingMouseGesture = new ExpandMouseGesture(doubleClickSpeedInMilliseconds, doubleClickDeltaInPixels);
            MoveGesture = new MoveGesture();
            SelectMidiMappingGesture = new SelectGesture();
            GridSnapGesture = new GridSnapGesture(MoveGesture);
        }
    }
}
