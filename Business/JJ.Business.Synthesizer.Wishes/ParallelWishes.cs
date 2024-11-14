using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using JJ.Business.Synthesizer.LinkTo;
using JJ.Framework.Common;
using JJ.Persistence.Synthesizer;
using static System.Threading.Tasks.Task;
using static JJ.Business.Synthesizer.Wishes.Helpers.FrameworkStringWishes;

// ReSharper disable ParameterHidesMember
// ReSharper disable ConditionIsAlwaysTrueOrFalse
// ReSharper disable ForCanBeConvertedToForeach

namespace JJ.Business.Synthesizer.Wishes
{
    // SynthWishes Parallelization
    
    public partial class SynthWishes
    {
        /// <inheritdoc cref="docs._paralleladd" />
        public FluentOutlet ParallelAdd(params FluentOutlet[] termFuncs)
            => ParallelAdd((IList<FluentOutlet>)termFuncs);

        /// <inheritdoc cref="docs._paralleladd" />
        public FluentOutlet ParallelAdd(IList<FluentOutlet> terms)
        {
            if (terms == null) throw new ArgumentNullException(nameof(terms));
            
            var add = Add(terms);
            
            if (GetParallelEnabled)
            {
                foreach (var term in add.Operands)
                {
                    Tape(term);
                }
            }
            
            return add;
        }

        internal void RunParallelsRecursive(IList<FluentOutlet> channels) 
        {
            if (channels == null) throw new ArgumentNullException(nameof(channels));
            if (channels.Contains(null)) throw new Exception("channels.Contains(null)");
            if (!GetParallelEnabled) return;

            var tasks = new Task[channels.Count];
            for (int i = 0; i < channels.Count; i++)
            {
                int channelIndex = i;
                tasks[channelIndex] = Run(() => RunParallelsRecursive(channels[channelIndex]));
            }
            
            WaitAll(tasks);
        }

        internal void RunParallelsRecursive(FluentOutlet op)
        {
            if (!GetParallelEnabled) return;

            // Gather all tasks with levels
            var tasks = GetParallelTasksRecursive(op, level: 1);

            // Group tasks by nesting level
            var levelGroups = tasks.OrderByDescending(x => x.Level).GroupBy(x => x.Level);
            foreach (var levelGroup in levelGroups)
            {
                // Execute each nesting level's task simultaneously.
                Task[] tasksInLevel = levelGroup.Select(x => x.Task).ToArray();
                tasksInLevel.ForEach(x => x.Start());
                WaitAll(tasksInLevel); // Ensure each level completes before moving up
            }
        }

        private IList<(Task Task, int Level)> GetParallelTasksRecursive(FluentOutlet op, int level)
        {
            if (op == null) throw new ArgumentNullException(nameof(op));

            var tasks = new List<(Task, int)>();
            var operands = op.Operands.Where(x => x != null).ToArray();
            
            // Recursively gather tasks from child nodes
            foreach (var operand in operands)
            {
                tasks.AddRange(GetParallelTasksRecursive(operand, level + 1));
            }
            
            for (var unsafeI = 0; unsafeI < operands.Length; unsafeI++)
            {
                int i = unsafeI;
                var operand = operands[i];
                
                // Are we being parallel?
                if (IsTape(operand))
                {
                    RemoveTape(operand);
                    
                    var task = new Task(() =>
                    {
                        Console.WriteLine($"{PrettyTime()} Start Task: {operand.Name} (Level {level})");
                        
                        var cacheResult = Cache(operand, operand.Name);
                        var newOperand = Sample(cacheResult, name: operand.Name);
                        
                        op.Operands[i] = newOperand;

                        // Replace all references to tape
                        //IList<Operator> connectedOperators = operand.UnderlyingOutlet.ConnectedInlets.Select(x => x.Operator).ToArray();
                        //foreach (Operator connectedOperator in connectedOperators)
                        //{
                        //    OperandList operands2 = connectedOperator.Operands();
                        //    int j = operands2.IndexOf(operand);
                        //    operands2[j] = newOperand;
                        //}
                        
                        IList<Inlet> connectedInlets = operand.UnderlyingOutlet.ConnectedInlets.ToArray();
                        foreach (Inlet inlet in connectedInlets)
                        {
                            inlet.LinkTo(newOperand);
                        }

                        Console.WriteLine($"{PrettyTime()} End Task: {operand.Name} (Level {level})");
                    });
                    
                    tasks.Add((task, level));
                }
            }

            return tasks;
        }

        // Helpers
        
        private readonly HashSet<Outlet> _tapes = new HashSet<Outlet>();
        private void AddTape(Outlet outlet) => _tapes.Add(outlet);
        private bool IsTape(Outlet outlet) => _tapes.Contains(outlet);
        private void RemoveTape(Outlet outlet) => _tapes.Remove(outlet);
    }
}
