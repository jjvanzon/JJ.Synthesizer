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
            var groupedByFlags = tapes.GroupBy(x => new
            {
                x.IsPlay,
                x.IsSave,
                x.IsIntercept,
                x.IsPlayChannel,
                x.IsSaveChannel,
                x.IsInterceptChannel,
                x.IsPadded,
                x.IsTape
            });
            
            foreach (var group in groupedByFlags)
            {
                TryAddPair(group);
                
                // Match by delegate method
                var groupedByMethod = group.Where(x => x.Callback != null).GroupBy(x => x.Callback.Method);
                foreach (var subGroup in groupedByMethod)
                {
                    TryAddPair(subGroup);
                }
                
                // Match by delegate type
                var groupedByClass = group.Where(x => x.Callback != null).GroupBy(x => x.Callback.Method.DeclaringType);
                foreach (var subGroup in groupedByClass)
                {
                    TryAddPair(subGroup);
                }
                
                // Match by name
                var groupedByName = group.GroupBy(x => new 
                { 
                    x.GetName,
                    x.FallBackName, 
                    x.FilePath, 
                    x.Signal?.Name, 
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
                    Join(NewLine, _unprocessedTapes.Select(x => "- " + GetTapeDescriptor(x) + " | " + x.Signal)) + NewLine +
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