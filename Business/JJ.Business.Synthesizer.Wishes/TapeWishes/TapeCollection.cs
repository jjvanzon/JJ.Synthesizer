using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using JJ.Business.Synthesizer.Wishes.Logging;
using JJ.Framework.Reflection;
using JJ.Persistence.Synthesizer;
using static System.Math;
using static JJ.Business.Synthesizer.Wishes.Helpers.CloneWishes;
using static JJ.Framework.Wishes.Common.FilledInWishes;
using static JJ.Business.Synthesizer.Wishes.NameWishes;

namespace JJ.Business.Synthesizer.Wishes.TapeWishes
{
    internal class TapeCollection : IEnumerable<Tape>
    {
        private SynthWishes SynthWishes { get; }

        private readonly Dictionary<Outlet, Tape> _tapes = new Dictionary<Outlet, Tape>();
        
        public TapeCollection(SynthWishes synthWishes)
        {
            SynthWishes = synthWishes ?? throw new ArgumentNullException(nameof(synthWishes));
        }
        
        public int Count => _tapes.Count;

        public Tape Upsert(
            ActionEnum actionType, FlowNode signal, FlowNode duration,
            string name,
            string filePath = null,
            Action<Tape> callback = default,
            [CallerMemberName] string callerMemberName = null)
        {
            if (signal == null) throw new ArgumentNullException(nameof(signal));
            if (!Has(actionType)) throw new Exception("actionType not specified.");

            bool isNew = false;
            if (!_tapes.TryGetValue(signal, out Tape tape))
            {
                isNew = true;
                _tapes[signal] = tape = new Tape(SynthWishes);
                tape.Outlet = signal;
            }
            
            CloneTape(SynthWishes, tape);
            
            if (Has(filePath)) tape.FilePathSuggested = filePath;
            if (Has(callerMemberName)) tape.FallbackName = ResolveName(name, callerMemberName);
            
            double newDuration = (duration ?? SynthWishes.GetAudioLength).Value;
            tape.Duration = Max(tape.Duration, newDuration);
            
            TapeAction action = tape.Actions.TryGet(actionType);
            if (action != null)
            {
                action.On = true;
                action.FilePathSuggested = filePath;
                action.Callback = action.Callback ?? callback;
                AssertCallback(action, callback);
            }
            
            tape.IsTape = actionType == ActionEnum.Tape;

            tape.LogAction(isNew ? "Create" : "Update");
            
            return tape;
        }
        
        /// <summary>
        /// Detect conflicting callback
        /// </summary>
        private void AssertCallback(TapeAction action, Action<Tape> callback)
        {
            if (action == null) throw new ArgumentNullException(nameof(action));
            
            if (callback != null &&
                action.Callback != null &&
                action.Callback != callback)
            {
                throw new Exception($"Different {action.Type} {nameof(callback)} passed than already assigned to the {nameof(Tape)}!");
            }
        }
        
        public bool IsTape(Outlet outlet)
        {
            if (outlet == null) throw new ArgumentNullException(nameof(outlet));
            return _tapes.ContainsKey(outlet);
        }
        
        public Tape TryGet(Outlet outlet)
        {
            if (outlet == null) throw new ArgumentNullException(nameof(outlet));
            _tapes.TryGetValue(outlet, out Tape tape);
            return tape;
        }
        
        public void Remove(Tape tape)
        {
            if (tape == null) throw new NullException(() => tape);
            if (tape.Outlet == null) throw new NullException(() => tape.Outlet);
            
            _tapes.Remove(tape.Outlet);
            
            tape.LogAction("Delete", "Replaced by padded");
        }
        
        public void Clear() => _tapes.Clear();

        public Tape[] ToArray() => _tapes.Values.ToArray(); // For availability in debugger.

        public IEnumerator<Tape> GetEnumerator()
        {
            foreach (Tape tape in _tapes.Values)
            {
                yield return tape;
            }
        }

        IEnumerator IEnumerable.GetEnumerator() => GetEnumerator();
    }
}