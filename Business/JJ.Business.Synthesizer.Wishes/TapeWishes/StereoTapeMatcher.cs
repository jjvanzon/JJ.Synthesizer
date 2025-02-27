using System;
using System.Collections.Generic;
using System.Linq;
using JJ.Business.Synthesizer.Wishes.Logging;
using static System.Environment;
using static System.String;
using static JJ.Business.Synthesizer.Wishes.Logging.LogWishes;

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
                x.Config.SamplingRate,
                x.Config.Bits,
                x.Config.Channels,
                x.Config.AudioFormat,
                x.Config.Interpolation,
                DiskCache = x.Actions.DiskCache.On,
                PlayAllTapes = x.Actions.PlayAllTapes.On,
                x.Config.CourtesyFrames,
                x.IsTape,
                x.IsPadded,
                PlayOn = x.Actions.Play.On,
                PlayDone = x.Actions.Play.Done,
                SaveOn = x.Actions.Save.On,
                SaveDone = x.Actions.Save.Done,
                BeforeRecordOn = x.Actions.BeforeRecord.On,
                BeforeRecordDone = x.Actions.BeforeRecord.Done,
                AfterRecordOn = x.Actions.AfterRecord.On,
                AfterRecordDone = x.Actions.AfterRecord.Done,
                PlayChannelsOn = x.Actions.PlayChannels.On,
                PlayChannelsDone = x.Actions.PlayChannels.Done,
                SaveChannelsOn = x.Actions.SaveChannels.On,
                SaveChannelsDone = x.Actions.SaveChannels.Done,
                BeforeRecordChannelOn = x.Actions.BeforeRecordChannel.On,
                BeforeRecordChannelDone = x.Actions.BeforeRecordChannel.Done,
                AfterRecordChannelOn = x.Actions.AfterRecordChannel.On,
                AfterRecordChannelDone = x.Actions.AfterRecordChannel.Done
            });
            
            foreach (var group in groupedByMetaData)
            {
                TryAddPair(group);
                
                // Match by name
                var groupedByName = group.GroupBy(x => new 
                {
                    x.FallbackName, 
                    x.FilePathSuggested,    
                    SignalName = x.Outlet?.GetName(), 
                    x.Outlet?.Operator?.OperatorTypeName
                });
                
                foreach (var subGroup in groupedByName)
                {
                    TryAddPair(subGroup);
                }
            }
            
            if (_unprocessedTapes.Count > 0)
            {
                string unprocessedTapesString = Join(NewLine, _unprocessedTapes.Select(x => "- " + x.Descriptor() + " | " + x.Outlet));
                
                string message = 
                    "Warning: " + NewLine +
                    _unprocessedTapes.Count + " channel tapes could not be matched to a stereo tape:" + NewLine +
                    unprocessedTapesString + NewLine +
                    "Unmatched tapes will be treated as single Mono tapes." + NewLine +
                    "To avoid duplicates, consider passing names to the Play, Save, Tape or Intercept methods.";
                
                throw new Exception(message);
                
                SynthWishes synthWishes = _unprocessedTapes.First().SynthWishes;
                synthWishes.LogSpaced(message);
                
                foreach (Tape tape in _unprocessedTapes.ToArray())
                {
                    _matchedPairs.Add((tape, tape));
                    _unprocessedTapes.Remove(tape);
                }
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
            
            var left = array.FirstOrDefault(x => x.Config.Channel == 0);
            if (left == null) throw new Exception("There are 2 channel tapes, but none of them are a Left channel (Channel = 0).");
            
            var right = array.FirstOrDefault(x => x.Config.Channel == 1);
            if (right == null) throw new Exception("There are 2 channel tapes, but none of them are a Right channel (Channel = 1).");
            
            var pair = (left, right);
            
            return pair;
        }
    }
}