using System;

namespace JJ.Business.Synthesizer.Wishes
{
    // Command Notation in SynthWishes
    
    public partial class SynthWishes
    {
        // No Parameters

        public FlowNode this[
            Func<FlowNode> func]
            => func();

        // 1 Parameter

        public FlowNode this[
            Func<FlowNode, FlowNode> func,
            FlowNode arg1 = null]
            => func(arg1);

        public FlowNode this[
            Func<FlowNode, FlowNode> func,
            double arg1]
            => func(_[arg1]);

        // 2 Parameters

        public FlowNode this[
            Func<FlowNode, FlowNode, FlowNode> func,
            FlowNode arg1 = null, FlowNode arg2 = null]
            => func(arg1, arg2);

        public FlowNode this[
            Func<FlowNode, FlowNode, FlowNode> func,
            double arg1, FlowNode arg2 = null]
            => func(_[arg1], arg2);

        public FlowNode this[
            Func<FlowNode, FlowNode, FlowNode> func,
            FlowNode arg1, double arg2]
            => func(arg1, _[arg2]);

        public FlowNode this[
            Func<FlowNode, FlowNode, FlowNode> func,
            double arg1, double arg2]
            => func(_[arg1], _[arg2]);

        // 3 Parameters

        public FlowNode this[
            Func<FlowNode, FlowNode, FlowNode, FlowNode> func,
            FlowNode arg1 = null, FlowNode arg2 = null, FlowNode arg3 = null]
            => func(arg1, arg2, arg3);

        public FlowNode this[
            Func<FlowNode, FlowNode, FlowNode, FlowNode> func,
            double arg1, FlowNode arg2 = null, FlowNode arg3 = null]
            => func(_[arg1], arg2, arg3);

        public FlowNode this[
            Func<FlowNode, FlowNode, FlowNode, FlowNode> func,
            FlowNode arg1, double arg2, FlowNode arg3 = null]
            => func(arg1, _[arg2], arg3);

        public FlowNode this[
            Func<FlowNode, FlowNode, FlowNode, FlowNode> func,
            FlowNode arg1, FlowNode arg2, double arg3]
            => func(arg1, arg2, _[arg3]);

        public FlowNode this[
            Func<FlowNode, FlowNode, FlowNode, FlowNode> func,
            double arg1, double arg2, FlowNode arg3]
            => func(_[arg1], _[arg2], arg3);

        public FlowNode this[
            Func<FlowNode, FlowNode, FlowNode, FlowNode> func,
            double arg1, FlowNode arg2, double arg3]
            => func(_[arg1], arg2, _[arg3]);

        public FlowNode this[
            Func<FlowNode, FlowNode, FlowNode, FlowNode> func,
            FlowNode arg1, double arg2, double arg3]
            => func(arg1, _[arg2], _[arg3]);

        public FlowNode this[
            Func<FlowNode, FlowNode, FlowNode, FlowNode> func,
            double arg1, double arg2, double arg3]
            => func(_[arg1], _[arg2], _[arg3]);

        // 4 Parameters

        public FlowNode this[
            Func<FlowNode, FlowNode, FlowNode, FlowNode, FlowNode> func,
            FlowNode arg1 = null, FlowNode arg2 = null, FlowNode arg3 = null, FlowNode arg4 = null]
            => func(arg1, arg2, arg3, arg4);

        // 5 Parameters

        public FlowNode this[
            Func<FlowNode, FlowNode, FlowNode, FlowNode, FlowNode, FlowNode> func,
            FlowNode arg1 = null, FlowNode arg2 = null, FlowNode arg3 = null, FlowNode arg4 = null, FlowNode arg5 = null]
            => func(arg1, arg2, arg3, arg4, arg5);

        // 6 Parameters

        public FlowNode this[
            Func<FlowNode, FlowNode, FlowNode, FlowNode, FlowNode, FlowNode, FlowNode> func,
            FlowNode arg1 = null, FlowNode arg2 = null, FlowNode arg3 = null, FlowNode arg4 = null, FlowNode arg5 = null, 
            FlowNode arg6 = null]
            => func(arg1, arg2, arg3, arg4, arg5, arg6);

        // 7 Parameters

        public FlowNode this[
            Func<FlowNode, FlowNode, FlowNode, FlowNode, FlowNode, FlowNode, FlowNode, FlowNode> func,
            FlowNode arg1 = null, FlowNode arg2 = null, FlowNode arg3 = null, FlowNode arg4 = null, FlowNode arg5 = null, 
            FlowNode arg6 = null, FlowNode arg7 = null]
            => func(arg1, arg2, arg3, arg4, arg5, arg6, arg7);

        // 8 Parameters

        public FlowNode this[
            Func<FlowNode, FlowNode, FlowNode, FlowNode, FlowNode, FlowNode, FlowNode, FlowNode, FlowNode> func,
            FlowNode arg1 = null, FlowNode arg2 = null, FlowNode arg3 = null, FlowNode arg4 = null, FlowNode arg5 = null, 
            FlowNode arg6 = null, FlowNode arg7 = null, FlowNode arg8 = null]
            => func(arg1, arg2, arg3, arg4, arg5, arg6, arg7, arg8);

        // 9 Parameters

        public FlowNode this[
            Func<FlowNode, FlowNode, FlowNode, FlowNode, FlowNode, FlowNode, FlowNode, FlowNode, FlowNode, FlowNode> func,
            FlowNode arg1 = null, FlowNode arg2 = null, FlowNode arg3 = null, FlowNode arg4 = null, FlowNode arg5 = null, 
            FlowNode arg6 = null, FlowNode arg7 = null, FlowNode arg8 = null, FlowNode arg9 = null]
            => func(arg1, arg2, arg3, arg4, arg5, arg6, arg7, arg8, arg9);

        // 10 Parameters

        public FlowNode this[
            Func<FlowNode, FlowNode, FlowNode, FlowNode, FlowNode, FlowNode, FlowNode, FlowNode, FlowNode, FlowNode, FlowNode> func,
            FlowNode arg1 = null, FlowNode arg2 = null, FlowNode arg3 = null, FlowNode arg4 = null, FlowNode arg5 = null, 
            FlowNode arg6 = null, FlowNode arg7 = null, FlowNode arg8 = null, FlowNode arg9 = null, FlowNode arg10 = null]
            => func(arg1, arg2, arg3, arg4, arg5, arg6, arg7, arg8, arg9, arg10);
    }

    // Command Notation with FlowNodes

    public partial class FlowNode
    {
        // No Parameters
        
        public FlowNode this[Func<FlowNode> func] 
            => _synthWishes[func];
        
        // 1 Parameter
        
        public FlowNode this[
            Func<FlowNode, FlowNode> func] 
            => _synthWishes[func, this];
        
        // 2 Parameters
        
        public FlowNode this[
            Func<FlowNode, FlowNode, FlowNode> func, 
            FlowNode arg2 = null] 
            => _synthWishes[func, this, arg2];

        public FlowNode this[
            Func<FlowNode, FlowNode, FlowNode> func,
            double arg2]
            => _synthWishes[func, this, arg2];

        // 3 Parameters

        public FlowNode this[
            Func<FlowNode, FlowNode, FlowNode, FlowNode> func, 
            FlowNode arg2 = null, FlowNode arg3 = null] 
            => _synthWishes[func, this, arg2, arg3];

        public FlowNode this[
            Func<FlowNode, FlowNode, FlowNode, FlowNode> func,
            double arg2, FlowNode arg3 = null]
            => _synthWishes[func, this, arg2, arg3];

        public FlowNode this[
            Func<FlowNode, FlowNode, FlowNode, FlowNode> func,
            FlowNode arg2, double arg3]
            => _synthWishes[func, this, arg2, arg3];

        public FlowNode this[
            Func<FlowNode, FlowNode, FlowNode, FlowNode> func,
            double arg2, double arg3]
            => _synthWishes[func, this, arg2, arg3];

        // 4 Parameters

        public FlowNode this[
            Func<FlowNode, FlowNode, FlowNode, FlowNode, FlowNode> func, 
            FlowNode arg2 = null, FlowNode arg3 = null, FlowNode arg4 = null] 
            => _synthWishes[func, this, arg2, arg3, arg4];

        // 5 Parameters
        
        public FlowNode this[
            Func<FlowNode, FlowNode, FlowNode, FlowNode, FlowNode, FlowNode> func, 
            FlowNode arg2 = null, FlowNode arg3 = null, FlowNode arg4 = null, FlowNode arg5 = null] 
            => _synthWishes[func, this, arg2, arg3, arg4, arg5];

        // 6 Parameters
        
        public FlowNode this[
            Func<FlowNode, FlowNode, FlowNode, FlowNode, FlowNode, FlowNode, FlowNode> func, 
            FlowNode arg2 = null, FlowNode arg3 = null, FlowNode arg4 = null, FlowNode arg5 = null,
            FlowNode arg6 = null] 
            => _synthWishes[func, this, arg2, arg3, arg4, arg5, arg6];

        // 7 Parameters
        
        public FlowNode this[
            Func<FlowNode, FlowNode, FlowNode, FlowNode, FlowNode, FlowNode, FlowNode, FlowNode> func, 
            FlowNode arg2 = null, FlowNode arg3 = null, FlowNode arg4 = null, FlowNode arg5 = null,
            FlowNode arg6 = null, FlowNode arg7 = null] 
            => _synthWishes[func, this, arg2, arg3, arg4, arg5, arg6, arg7];

        // 8 Parameters
        
        public FlowNode this[
            Func<FlowNode, FlowNode, FlowNode, FlowNode, FlowNode, FlowNode, FlowNode, FlowNode, FlowNode> func, 
            FlowNode arg2 = null, FlowNode arg3 = null, FlowNode arg4 = null, FlowNode arg5 = null,
            FlowNode arg6 = null, FlowNode arg7 = null, FlowNode arg8 = null] 
            => _synthWishes[func, this, arg2, arg3, arg4, arg5, arg6, arg7, arg8];

        // 9 Parameters
        
        public FlowNode this[
            Func<FlowNode, FlowNode, FlowNode, FlowNode, FlowNode, FlowNode, FlowNode, FlowNode, FlowNode, FlowNode> func, 
            FlowNode arg2 = null, FlowNode arg3 = null, FlowNode arg4 = null, FlowNode arg5 = null,
            FlowNode arg6 = null, FlowNode arg7 = null, FlowNode arg8 = null, FlowNode arg9 = null] 
            => _synthWishes[func, this, arg2, arg3, arg4, arg5, arg6, arg7, arg8, arg9];

        // 10 Parameters
        
        public FlowNode this[
            Func<FlowNode, FlowNode, FlowNode, FlowNode, FlowNode, FlowNode, FlowNode, FlowNode, FlowNode, FlowNode, FlowNode> func, 
            FlowNode arg2 = null, FlowNode arg3 = null, FlowNode arg4 = null, FlowNode arg5 = null,
            FlowNode arg6 = null, FlowNode arg7 = null, FlowNode arg8 = null, FlowNode arg9 = null, FlowNode arg10 = null] 
            => _synthWishes[func, this, arg2, arg3, arg4, arg5, arg6, arg7, arg8, arg9, arg10];
    }
    
    // Command Notation for CaptureIndexer
    
    public partial class CaptureIndexer
    {
        // 0 Parameters
        
        public FlowNode this[
            Func<FlowNode> func] 
            => _synthWishes[func];
        
        // 1 Parameter
        
        public FlowNode this[
            Func<FlowNode, FlowNode> func, 
            FlowNode arg1 = null] 
            => _synthWishes[func, arg1];

        public FlowNode this[
            Func<FlowNode, FlowNode> func,
            double arg1]
            => _synthWishes[func, arg1];

        // 2 Parameters

        public FlowNode this[
            Func<FlowNode, FlowNode, FlowNode> func, 
            FlowNode arg1 = null, FlowNode arg2 = null] 
            => _synthWishes[func, arg1, arg2];

        public FlowNode this[
            Func<FlowNode, FlowNode, FlowNode> func,
            double arg1, FlowNode arg2 = null]
            => _synthWishes[func, arg1, arg2];

        public FlowNode this[
            Func<FlowNode, FlowNode, FlowNode> func,
            FlowNode arg1, double arg2]
            => _synthWishes[func, arg1, arg2];

        public FlowNode this[
            Func<FlowNode, FlowNode, FlowNode> func,
            double arg1, double arg2]
            => _synthWishes[func, arg1, arg2];

        // 3 Parameters

        public FlowNode this[
            Func<FlowNode, FlowNode, FlowNode, FlowNode> func, 
            FlowNode arg1 = null, FlowNode arg2 = null, FlowNode arg3 = null] 
            => _synthWishes[func, arg1, arg2, arg3];

        public FlowNode this[
            Func<FlowNode, FlowNode, FlowNode, FlowNode> func,
            double arg1, FlowNode arg2 = null, FlowNode arg3 = null]
            => _synthWishes[func, arg1, arg2, arg3];

        public FlowNode this[
            Func<FlowNode, FlowNode, FlowNode, FlowNode> func,
            FlowNode arg1, double arg2, FlowNode arg3 = null]
            => _synthWishes[func, arg1, arg2, arg3];

        public FlowNode this[
            Func<FlowNode, FlowNode, FlowNode, FlowNode> func,
            FlowNode arg1, FlowNode arg2, double arg3]
            => _synthWishes[func, arg1, arg2, arg3];

        public FlowNode this[
            Func<FlowNode, FlowNode, FlowNode, FlowNode> func,
            double arg1, double arg2, FlowNode arg3]
            => _synthWishes[func, arg1, arg2, arg3];

        public FlowNode this[
            Func<FlowNode, FlowNode, FlowNode, FlowNode> func,
            double arg1, FlowNode arg2, double arg3]
            => _synthWishes[func, arg1, arg2, arg3];

        public FlowNode this[
            Func<FlowNode, FlowNode, FlowNode, FlowNode> func,
            FlowNode arg1, double arg2, double arg3]
            => _synthWishes[func, arg1, arg2, arg3];

        public FlowNode this[
            Func<FlowNode, FlowNode, FlowNode, FlowNode> func,
            double arg1, double arg2, double arg3]
            => _synthWishes[func, arg1, arg2, arg3];

        // 4 Parameters

        public FlowNode this[
            Func<FlowNode, FlowNode, FlowNode, FlowNode, FlowNode> func, 
            FlowNode arg1 = null, FlowNode arg2 = null, FlowNode arg3 = null, FlowNode arg4 = null] 
            => _synthWishes[func, arg1, arg2, arg3, arg4];
        
        // 5 Parameters
        
        public FlowNode this[
            Func<FlowNode, FlowNode, FlowNode, FlowNode, FlowNode, FlowNode> func, 
            FlowNode arg1 = null, FlowNode arg2 = null, FlowNode arg3 = null, FlowNode arg4 = null, FlowNode arg5 = null] 
            => _synthWishes[func, arg1, arg2, arg3, arg4, arg5];
        
        // 6 Parameters
        
        public FlowNode this[
            Func<FlowNode, FlowNode, FlowNode, FlowNode, FlowNode, FlowNode, FlowNode> func, 
            FlowNode arg1 = null, FlowNode arg2 = null, FlowNode arg3 = null, FlowNode arg4 = null, FlowNode arg5 = null, 
            FlowNode arg6 = null] 
            => _synthWishes[func, arg1, arg2, arg3, arg4, arg5, arg6];
        
        // 7 Parameters
        
        public FlowNode this[
            Func<FlowNode, FlowNode, FlowNode, FlowNode, FlowNode, FlowNode, FlowNode, FlowNode> func, 
            FlowNode arg1 = null, FlowNode arg2 = null, FlowNode arg3 = null, FlowNode arg4 = null, FlowNode arg5 = null, 
            FlowNode arg6 = null, FlowNode arg7 = null] 
            => _synthWishes[func, arg1, arg2, arg3, arg4, arg5, arg6, arg7];
        
        // 8 Parameters
        
        public FlowNode this[
            Func<FlowNode, FlowNode, FlowNode, FlowNode, FlowNode, FlowNode, FlowNode, FlowNode, FlowNode> func, 
            FlowNode arg1 = null, FlowNode arg2 = null, FlowNode arg3 = null, FlowNode arg4 = null, FlowNode arg5 = null, 
            FlowNode arg6 = null, FlowNode arg7 = null, FlowNode arg8 = null] 
            => _synthWishes[func, arg1, arg2, arg3, arg4, arg5, arg6, arg7, arg8];
        
        // 9 Parameters
        
        public FlowNode this[
            Func<FlowNode, FlowNode, FlowNode, FlowNode, FlowNode, FlowNode, FlowNode, FlowNode, FlowNode, FlowNode> func, 
            FlowNode arg1 = null, FlowNode arg2 = null, FlowNode arg3 = null, FlowNode arg4 = null, FlowNode arg5 = null, 
            FlowNode arg6 = null, FlowNode arg7 = null, FlowNode arg8 = null, FlowNode arg9 = null] 
            => _synthWishes[func, arg1, arg2, arg3, arg4, arg5, arg6, arg7, arg8, arg9];
        
        // 10 Parameters
        
        public FlowNode this[
            Func<FlowNode, FlowNode, FlowNode, FlowNode, FlowNode, FlowNode, FlowNode, FlowNode, FlowNode, FlowNode, FlowNode> func, 
            FlowNode arg1 = null, FlowNode arg2 = null, FlowNode arg3 = null, FlowNode arg4 = null, FlowNode arg5 = null, 
            FlowNode arg6 = null, FlowNode arg7 = null, FlowNode arg8 = null, FlowNode arg9 = null, FlowNode arg10 = null] 
            => _synthWishes[func, arg1, arg2, arg3, arg4, arg5, arg6, arg7, arg8, arg9, arg10];
    }
}
