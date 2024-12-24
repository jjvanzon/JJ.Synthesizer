using System;
using System.Collections.Generic;
using System.Linq;
using static System.Environment;
using static System.String;
using static JJ.Business.Synthesizer.Wishes.LogWishes;

namespace JJ.Business.Synthesizer.Wishes.TapeWishes
{
    internal class StereoTapeMatcher
    {
        private HashSet<Tape> _unprocessedTapes;
        private IList<(Tape, Tape)> _matchedPairs;
        
        /// <summary>
        /// Heuristically matches the left channel and right channel tapes together,
        /// which is non-trivial because of the rigid separation of the 2 channels in this system,
        /// which is both powerful and tricky all the same.
        /// Only call in case of stereo. In case of mono, the tapes can be played, saved or intercepted
        /// individually.
        /// </summary>
        public IList<(Tape Left, Tape Right)> PairTapes(IList<Tape> tapes)
        {
            if (tapes == null) throw new ArgumentNullException(nameof(tapes));
            if (tapes.Count == 1) throw new Exception("Stereo tapes expected, but only 1 channel tape found.");
            
            _unprocessedTapes = tapes.ToHashSet();
            _matchedPairs = new List<(Tape, Tape)>();
            
            // Return single stereo tape
            {
                if (TryAddPair(tapes))
                {
                    return _matchedPairs;
                }
            }
            
            // First grouping:
            // Group by flags, as they definitely should match.
            var groupedByMetaData = tapes.GroupBy(x => new
            {
                x.LeadingSilence,
                x.TrailingSilence,
                //x.Duration, // One test still fails when this is enabled.
                x.SamplingRate,
                x.Bits,
                x.Channels,
                x.AudioFormat,
                x.Interpolation,
                x.DiskCache,
                x.PlayAllTapes,
                x.CourtesyFrames,
                x.IsTape,
                x.IsPadded,
                PlayOn = x.Play.On,
                PlayDone = x.Play.Done,
                SaveOn = x.Save.On,
                SaveDone = x.Save.Done,
                InterceptOn = x.Intercept.On,
                InterceptDone = x.Intercept.Done,
                PlayChannelOn = x.PlayChannel.On,
                PlayChannelDone = x.PlayChannel.Done,
                SaveChannelOn = x.SaveChannel.On,
                SaveChannelDone = x.SaveChannel.Done,
                InterceptChannelOn = x.InterceptChannel.On,
                InterceptChannelDone = x.InterceptChannel.Done
            });
            
            foreach (var group in groupedByMetaData)
            {
                TryAddPair(group);
                
                // Match by name
                var groupedByName = group.GroupBy(x => new 
                { 
                    Name = x.GetName(),
                    x.FallBackName, 
                    x.FilePathSuggested,    
                    SignalName = x.Signal?.Name, 
                    x.Signal?.UnderlyingOperator?.OperatorTypeName
                });
                
                foreach (var subGroup in groupedByName)
                {
                    TryAddPair(subGroup);
                }
            }
            
            if (_unprocessedTapes.Count > 0)
            {
                throw new Exception(
                    _unprocessedTapes.Count + " channel tapes could not be matched to a stereo tape:" + NewLine +
                    Join(NewLine, _unprocessedTapes.Select(x => "- " + Descriptor(x) + " | " + x.Signal)) + NewLine +
                    "To avoid duplicates, consider passing names to the Play, Save, Tape or Intercept methods.");
            }
            
            return _matchedPairs;
        }
        
        private bool TryAddPair(IEnumerable<Tape> potentialPair)
        {
            (Tape left, Tape right) tapePair = TryGetTapePair(potentialPair);
            if (tapePair != default)
            {
                _matchedPairs.Add(tapePair);
                _unprocessedTapes.Remove(tapePair.left);
                _unprocessedTapes.Remove(tapePair.right);
                return true;
            }
            
            return false;
        }
        
        private static (Tape left, Tape right) TryGetTapePair(IEnumerable<Tape> potentialPair)
        {
            if (potentialPair == null) throw new ArgumentNullException(nameof(potentialPair));
            
            var array = potentialPair as Tape[] ?? potentialPair.ToArray();
            if (array.Length != 2) return default;
            
            var left = array.FirstOrDefault(x => x.Channel == 0);
            if (left == null) throw new Exception("There are 2 channel tapes, but none of them are a Left channel (Channel = 0).");
            
            var right = array.FirstOrDefault(x => x.Channel == 1);
            if (right == null) throw new Exception("There are 2 channel tapes, but none of them are a Right channel (Channel = 1).");
            
            var pair = (left, right);
            
            return pair;
        }
    }
}