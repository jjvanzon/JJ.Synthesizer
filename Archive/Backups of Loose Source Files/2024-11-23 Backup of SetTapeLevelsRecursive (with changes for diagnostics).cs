        private void SetTapeLevelsRecursive(FlowNode node, int level)
        {
            // NOTE: The ToArray() did not help the discrepancy in levels between the old and new process.
            foreach (var child in node.Operands.ToArray()) 
            {
                if (child == null) continue;
                SetTapeLevelsRecursive(child, level + 1);
            }
        
            // NOTE: Moved to below the recursive call for the children.
            Tape tape = TryGetTape(node);
            if (tape != null)
            {
                if (tape.Level == default) // Multiple usage of same tape: keep lowest value.
                {
                    tape.Level = level;
                }
                else
                { 
                    // Hyp: Multiple occurence of the same tape.
                    int dummy = 1;
                }
            }
        }