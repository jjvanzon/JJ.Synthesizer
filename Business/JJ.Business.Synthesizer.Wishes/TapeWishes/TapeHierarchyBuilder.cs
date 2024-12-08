using System;
using System.Collections.Generic;
using System.Linq;

namespace JJ.Business.Synthesizer.Wishes.TapeWishes
{
    internal class TapeHierarchyBuilder
    {
        private readonly TapeCollection _tapes;
        
        public TapeHierarchyBuilder(TapeCollection tapes)
        {
            _tapes = tapes ?? throw new ArgumentNullException(nameof(tapes));
        }
        
        public void BuildTapeHierarchyRecursive(Tape tape)
        {
            BuildTapeHierarchyRecursive(tape.Signal, null);
        }
        
        private void BuildTapeHierarchyRecursive(FlowNode node, Tape parentTape)
        {
            Tape tape = _tapes.TryGet(node);
            if (tape != null)
            {
                if (parentTape != null && tape.ParentTape == null)
                {
                    tape.ParentTape = parentTape;
                    parentTape.ChildTapes.Add(tape);
                }
                
                parentTape = tape;
            }
            
            foreach (FlowNode child in node.Operands)
            {
                if (child == null) continue;
                BuildTapeHierarchyRecursive(child, parentTape);
            }
        }
        
        private static void SetTapeNestingLevelsRecursive(IList<Tape> tapes)
        {
            var roots = tapes.Where(x => x.ParentTape == null).ToArray();
            foreach (Tape root in roots)
            {
                SetTapeNestingLevelsRecursive(root);
            }
        }

        private static void SetTapeNestingLevelsRecursive(Tape tape, int level = 1)
        {
            // Don't overwrite in case of multiple usage.
            if (tape.NestingLevel == default) tape.NestingLevel = level++;
            
            foreach (Tape child in tape.ChildTapes)
            {
                if (child == null) continue;
                SetTapeNestingLevelsRecursive(child, level);
            }
        }

    }
}
