using System;
using System.Collections.Generic;
using System.Linq;
using JJ.Business.Synthesizer.Calculation;
using JJ.Framework.Reflection.Exceptions;

namespace JJ.Presentation.Synthesizer.NAudio
{
    internal class NoteRecycler
    {
        private const int DEFAULT_MAX_CONCURRENT_NOTES = 4;

        private volatile int _maxConcurrentNotes = DEFAULT_MAX_CONCURRENT_NOTES;
        private readonly List<NoteInfo> _noteInfos = new List<NoteInfo>();

        public int MaxConcurrentNotes
        {
            get { return _maxConcurrentNotes; }
            set { _maxConcurrentNotes = value; }
        }

        /// <summary> Returns null if max concurrent notes was exceeded. </summary>
        public NoteInfo TryGetNoteInfoToStart(int noteNumber, double presentTime)
        {
            NoteInfo noteInfo = _noteInfos.Where(x => x.EndTime < presentTime).FirstOrDefault();
            if (noteInfo == null)
            {
                bool mutCreate = _noteInfos.Count < _maxConcurrentNotes;
                if (mutCreate)
                {
                    noteInfo = new NoteInfo();
                    noteInfo.ListIndex = _noteInfos.Count;
                    _noteInfos.Add(noteInfo);
                }
            }
    
            if (noteInfo != null)
            {
                noteInfo.NoteNumber = noteNumber;
                noteInfo.StartTime = presentTime;
                noteInfo.ReleaseTime = CalculationHelper.VERY_HIGH_VALUE;
                noteInfo.EndTime = CalculationHelper.VERY_HIGH_VALUE;
            }

            return noteInfo;
        }

        /// <summary> Might return null, when note was ignored earlier, due to not enough slots. </summary>
        public NoteInfo TryGetNoteInfoToRelease(int noteNumber, double presentTime)
        {
            NoteInfo noteInfo = _noteInfos.Where(x => x.NoteNumber == noteNumber &&
                                                      x.ReleaseTime > presentTime &&
                                                      x.EndTime > presentTime) // Should never be evaluated, but does not cost anything to keep it in there.
                                          .OrderBy(x => x.StartTime)
                                          .FirstOrDefault();

            return noteInfo;
        }

        public void ReleaseNoteInfo(NoteInfo noteInfo, double releaseTime, double endTime)
        {
            if (noteInfo == null) throw new NullException(() => noteInfo);

            noteInfo.ReleaseTime = releaseTime;
            noteInfo.EndTime = endTime;
        }
    }
}
