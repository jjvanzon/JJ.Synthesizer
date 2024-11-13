            
            // Are we being parallel?
            if (IsParallelAdd(op))
            {
                RemoveParallelAddTag(op);
                
                // Loop through operands
                for (var i = 0; i < operands.Length; i++)
                {
                    int operandIndex = i;
                    var operand = operands[operandIndex];
                    
                    // Make a task per operand
                    var task = new Task(() =>
                    {
                        string name = GetParallelTaskName(op.Name, operandIndex, operand.Name);
                        string displayName = GetDisplayName(name);
                        
                        Console.WriteLine($"{PrettyTime()} Start Task: {displayName} (Level {level})", nameof(SynthWishes));
                        
                        var cacheResult = Cache(operand, name);
                        var sample = Sample(cacheResult, name: displayName);
                        
                        op.Operands[operandIndex] = sample;
                        
                        Console.WriteLine($"{PrettyTime()} End Task: {displayName} (Level {level})", nameof(SynthWishes));
                    });
                    
                    tasks.Add((task, level));
                }
            }


        private const string ParallelAddTag = " 678976b885a04c79 Parallel Add 8882a57583e82813";
        
        private static bool IsParallelAdd(FluentOutlet fluentOutlet) 
            => fluentOutlet.Name != null && fluentOutlet.Name.Contains(ParallelAddTag);
        
        private static void RemoveParallelAddTag(FluentOutlet fluentOutlet) 
            => fluentOutlet.Name = fluentOutlet.Name?.Replace(ParallelAddTag, " Parallel Add");
        
        private string GetParallelTaskName(string addOperatorName, int termIndex, string operandName)
            => $"{addOperatorName} (Term {termIndex + 1} - {operandName}).wav";


            //private const string TapeTag = " [Tape eba106eee7b600ca]";
            //=> fluentOutlet.Name != null && fluentOutlet.Name.Contains(TapeTag);
            //=> fluentOutlet.Name = fluentOutlet.Name?.Replace(TapeTag, " Tape");

        
        private string GetParallelTaskName(string operatorName) 
            => $"{operatorName}.wav";
