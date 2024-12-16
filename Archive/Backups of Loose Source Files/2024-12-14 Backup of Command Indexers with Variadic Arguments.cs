Command indexers with variadic arguments: (didn't work)

        // Variadic Arguments

        //public FlowNode this[Delegate d, params FlowNode[] args]
        //    => d.DynamicInvoke(args.Cast<object>().ToArray()) as FlowNode;

        // Variadic Arguments
        
        //public FlowNode this[Delegate d, params FlowNode[] args]
        //    => _synthWishes[d, new[] { this }.Concat(args).ToArray() ];

        // Variadic Arguments
        
        //public FlowNode this[Delegate d, params FlowNode[] args]
        //    => _synthWishes[d, args];
