using System;
using System.Collections.Generic;
using System.Linq;
using JJ.Business.Synthesizer.Calculation;
using JJ.Framework.Reflection.Exceptions;

namespace JJ.Presentation.Synthesizer.NAudio
{
    public class NoteRecycler
    {
        private int _maxConcurrentNotes;
        private readonly IList<NoteInfo> _noteInfos;

        public NoteRecycler(int maxConcurrentNotes)
        {
            if (maxConcurrentNotes < 1) throw new LessThanException(() => maxConcurrentNotes, 1);

            _maxConcurrentNotes = maxConcurrentNotes;

            _noteInfos = new NoteInfo[_maxConcurrentNotes];

            for (int i = 0; i < _maxConcurrentNotes; i++)
            {
                _noteInfos[i] = new NoteInfo
                {
                    ListIndex = i,
                    EndTime = CalculationHelper.VERY_LOW_VALUE,
                    ReleaseTime = CalculationHelper.VERY_LOW_VALUE,
                    StartTime = CalculationHelper.VERY_LOW_VALUE
                };
            }
        }

        /// <summary> Returns null if max concurrent notes was exceeded. </summary>
        public NoteInfo TryGetNoteInfoToStart(int noteNumber, double presentTime)
        {
            NoteInfo noteInfo = _noteInfos.Where(x => x.EndTime < presentTime).FirstOrDefault();
    
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

        public bool IsNoteReleased(int noteListIndex, double presentTime)
        {
            if (noteListIndex < 0) throw new LessThanException(() => noteListIndex, 0);
            if (noteListIndex > _noteInfos.Count) throw new GreaterThanException(() => noteListIndex, () => _noteInfos.Count);

            bool isReleased = _noteInfos[noteListIndex].EndTime < presentTime;

            return isReleased;
        }
    }
}
