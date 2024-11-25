using JJ.Business.Synthesizer.Tests.Helpers;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using static JJ.Framework.Testing.AssertHelper;

namespace JJ.Business.Synthesizer.Tests.Technical
{
    [TestClass]
    public class ConfigWishesTests : MySynthWishes
    {
        [TestMethod]
        public void Fluent_NoteLength_Test()
        {
            // TODO: Can't easily apply an envelope consistently to the note length?
            // TODO: Note enough overloads in the note arrangement indexer.
            //_[time, A4, instrument, volume]

            WithAudioLength(2);

            var time       = _[0];
            var volume     = 0.8;
            var instrument = A4.Sine();
            
            // Config file (0.5)
            {
                var noteLength = GetNoteLength;
                AreEqual(0.5, () => noteLength.Value);
                Play(() => StrikeNote(instrument, time, volume));
            }
            
            // WithNoteLength (0.8)
            {
                WithNoteLength(_[0.8]);
                var noteLength = GetNoteLength;
                AreEqual(0.8, () => noteLength.Value);
                Play(() => StrikeNote(instrument, time, volume));
            }

            // WithNoteLength(default) => Config file (0.5)
            {
                WithNoteLength(default);
                
                var noteLength = GetNoteLength;
                AreEqual(0.5, () => noteLength.Value);
                Play(() => StrikeNote(instrument, time, volume));
            }
            
            // StrikeNote parameter (1.5)
            {
                Play(() => StrikeNote(instrument, time, volume, duration: _[1.5]));
            }
            
            // Just the instrument for reference
            Play(() => instrument);
        }
    }
}
