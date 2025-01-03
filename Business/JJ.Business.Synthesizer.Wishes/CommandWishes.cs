using System;

namespace JJ.Business.Synthesizer.Wishes
{
    // Command Notation in SynthWishes
    
    public partial class SynthWishes
    {
        // No Parameters

        /// <inheritdoc cref="docs._commandindexer"/>
        public FlowNode this[
            Func<FlowNode> command]
            => command();

        // 1 Parameter

        /// <inheritdoc cref="docs._commandindexer"/>
        public FlowNode this[
            Func<FlowNode, FlowNode> command,
            FlowNode param1 = null]
            => command(param1);

        /// <inheritdoc cref="docs._commandindexer"/>
        public FlowNode this[
            Func<FlowNode, FlowNode> command,
            double param1]
            => command(_[param1]);

        // 2 Parameters

        /// <inheritdoc cref="docs._commandindexer"/>
        public FlowNode this[
            Func<FlowNode, FlowNode, FlowNode> command,
            FlowNode param1 = null, FlowNode param2 = null]
            => command(param1, param2);

        /// <inheritdoc cref="docs._commandindexer"/>
        public FlowNode this[
            Func<FlowNode, FlowNode, FlowNode> command,
            double param1, FlowNode param2 = null]
            => command(_[param1], param2);

        /// <inheritdoc cref="docs._commandindexer"/>
        public FlowNode this[
            Func<FlowNode, FlowNode, FlowNode> command,
            FlowNode param1, double param2]
            => command(param1, _[param2]);

        /// <inheritdoc cref="docs._commandindexer"/>
        public FlowNode this[
            Func<FlowNode, FlowNode, FlowNode> command,
            double param1, double param2]
            => command(_[param1], _[param2]);

        // 3 Parameters

        /// <inheritdoc cref="docs._commandindexer"/>
        public FlowNode this[
            Func<FlowNode, FlowNode, FlowNode, FlowNode> command,
            FlowNode param1 = null, FlowNode param2 = null, FlowNode param3 = null]
            => command(param1, param2, param3);

        /// <inheritdoc cref="docs._commandindexer"/>
        public FlowNode this[
            Func<FlowNode, FlowNode, FlowNode, FlowNode> command,
            double param1, FlowNode param2 = null, FlowNode param3 = null]
            => command(_[param1], param2, param3);

        /// <inheritdoc cref="docs._commandindexer"/>
        public FlowNode this[
            Func<FlowNode, FlowNode, FlowNode, FlowNode> command,
            FlowNode param1, double param2, FlowNode param3 = null]
            => command(param1, _[param2], param3);

        /// <inheritdoc cref="docs._commandindexer"/>
        public FlowNode this[
            Func<FlowNode, FlowNode, FlowNode, FlowNode> command,
            FlowNode param1, FlowNode param2, double param3]
            => command(param1, param2, _[param3]);

        /// <inheritdoc cref="docs._commandindexer"/>
        public FlowNode this[
            Func<FlowNode, FlowNode, FlowNode, FlowNode> command,
            double param1, double param2, FlowNode param3]
            => command(_[param1], _[param2], param3);

        /// <inheritdoc cref="docs._commandindexer"/>
        public FlowNode this[
            Func<FlowNode, FlowNode, FlowNode, FlowNode> command,
            double param1, FlowNode param2, double param3]
            => command(_[param1], param2, _[param3]);

        /// <inheritdoc cref="docs._commandindexer"/>
        public FlowNode this[
            Func<FlowNode, FlowNode, FlowNode, FlowNode> command,
            FlowNode param1, double param2, double param3]
            => command(param1, _[param2], _[param3]);

        /// <inheritdoc cref="docs._commandindexer"/>
        public FlowNode this[
            Func<FlowNode, FlowNode, FlowNode, FlowNode> command,
            double param1, double param2, double param3]
            => command(_[param1], _[param2], _[param3]);

        // 4 Parameters

        /// <inheritdoc cref="docs._commandindexer"/>
        public FlowNode this[
            Func<FlowNode, FlowNode, FlowNode, FlowNode, FlowNode> command,
            FlowNode param1 = null, FlowNode param2 = null, FlowNode param3 = null, FlowNode param4 = null]
            => command(param1, param2, param3, param4);

        // 5 Parameters

        /// <inheritdoc cref="docs._commandindexer"/>
        public FlowNode this[
            Func<FlowNode, FlowNode, FlowNode, FlowNode, FlowNode, FlowNode> command,
            FlowNode param1 = null, FlowNode param2 = null, FlowNode param3 = null, FlowNode param4 = null, FlowNode param5 = null]
            => command(param1, param2, param3, param4, param5);

        // 6 Parameters

        /// <inheritdoc cref="docs._commandindexer"/>
        public FlowNode this[
            Func<FlowNode, FlowNode, FlowNode, FlowNode, FlowNode, FlowNode, FlowNode> command,
            FlowNode param1 = null, FlowNode param2 = null, FlowNode param3 = null, FlowNode param4 = null, FlowNode param5 = null, 
            FlowNode param6 = null]
            => command(param1, param2, param3, param4, param5, param6);

        // 7 Parameters

        /// <inheritdoc cref="docs._commandindexer"/>
        public FlowNode this[
            Func<FlowNode, FlowNode, FlowNode, FlowNode, FlowNode, FlowNode, FlowNode, FlowNode> command,
            FlowNode param1 = null, FlowNode param2 = null, FlowNode param3 = null, FlowNode param4 = null, FlowNode param5 = null, 
            FlowNode param6 = null, FlowNode param7 = null]
            => command(param1, param2, param3, param4, param5, param6, param7);

        // 8 Parameters

        /// <inheritdoc cref="docs._commandindexer"/>
        public FlowNode this[
            Func<FlowNode, FlowNode, FlowNode, FlowNode, FlowNode, FlowNode, FlowNode, FlowNode, FlowNode> command,
            FlowNode param1 = null, FlowNode param2 = null, FlowNode param3 = null, FlowNode param4 = null, FlowNode param5 = null, 
            FlowNode param6 = null, FlowNode param7 = null, FlowNode param8 = null]
            => command(param1, param2, param3, param4, param5, param6, param7, param8);

        // 9 Parameters

        /// <inheritdoc cref="docs._commandindexer"/>
        public FlowNode this[
            Func<FlowNode, FlowNode, FlowNode, FlowNode, FlowNode, FlowNode, FlowNode, FlowNode, FlowNode, FlowNode> command,
            FlowNode param1 = null, FlowNode param2 = null, FlowNode param3 = null, FlowNode param4 = null, FlowNode param5 = null, 
            FlowNode param6 = null, FlowNode param7 = null, FlowNode param8 = null, FlowNode param9 = null]
            => command(param1, param2, param3, param4, param5, param6, param7, param8, param9);

        // 10 Parameters

        /// <inheritdoc cref="docs._commandindexer"/>
        public FlowNode this[
            Func<FlowNode, FlowNode, FlowNode, FlowNode, FlowNode, FlowNode, FlowNode, FlowNode, FlowNode, FlowNode, FlowNode> command,
            FlowNode param1 = null, FlowNode param2 = null, FlowNode param3 = null, FlowNode param4 = null, FlowNode param5 = null, 
            FlowNode param6 = null, FlowNode param7 = null, FlowNode param8 = null, FlowNode param9 = null, FlowNode param10 = null]
            => command(param1, param2, param3, param4, param5, param6, param7, param8, param9, param10);
    }

    // Command Notation with FlowNodes

    public partial class FlowNode
    {
        // No Parameters
        
        /// <inheritdoc cref="docs._commandindexer"/>
        public FlowNode this[Func<FlowNode> command] 
            => _[command];
        
        // 1 Parameter
        
        /// <inheritdoc cref="docs._commandindexer"/>
        public FlowNode this[
            Func<FlowNode, FlowNode> command] 
            => _[command, this];
        
        // 2 Parameters
        
        /// <inheritdoc cref="docs._commandindexer"/>
        public FlowNode this[
            Func<FlowNode, FlowNode, FlowNode> command, 
            FlowNode param2 = null] 
            => _[command, this, param2];

        public FlowNode this[
            Func<FlowNode, FlowNode, FlowNode> command,
            double param2]
            => _[command, this, param2];

        // 3 Parameters

        /// <inheritdoc cref="docs._commandindexer"/>
        public FlowNode this[
            Func<FlowNode, FlowNode, FlowNode, FlowNode> command, 
            FlowNode param2 = null, FlowNode param3 = null] 
            => _[command, this, param2, param3];

        /// <inheritdoc cref="docs._commandindexer"/>
        public FlowNode this[
            Func<FlowNode, FlowNode, FlowNode, FlowNode> command,
            double param2, FlowNode param3 = null]
            => _[command, this, param2, param3];

        /// <inheritdoc cref="docs._commandindexer"/>
        public FlowNode this[
            Func<FlowNode, FlowNode, FlowNode, FlowNode> command,
            FlowNode param2, double param3]
            => _[command, this, param2, param3];

        /// <inheritdoc cref="docs._commandindexer"/>
        public FlowNode this[
            Func<FlowNode, FlowNode, FlowNode, FlowNode> command,
            double param2, double param3]
            => _[command, this, param2, param3];

        // 4 Parameters

        /// <inheritdoc cref="docs._commandindexer"/>
        public FlowNode this[
            Func<FlowNode, FlowNode, FlowNode, FlowNode, FlowNode> command, 
            FlowNode param2 = null, FlowNode param3 = null, FlowNode param4 = null] 
            => _[command, this, param2, param3, param4];

        // 5 Parameters
        
        /// <inheritdoc cref="docs._commandindexer"/>
        public FlowNode this[
            Func<FlowNode, FlowNode, FlowNode, FlowNode, FlowNode, FlowNode> command, 
            FlowNode param2 = null, FlowNode param3 = null, FlowNode param4 = null, FlowNode param5 = null] 
            => _[command, this, param2, param3, param4, param5];

        // 6 Parameters
        
        /// <inheritdoc cref="docs._commandindexer"/>
        public FlowNode this[
            Func<FlowNode, FlowNode, FlowNode, FlowNode, FlowNode, FlowNode, FlowNode> command, 
            FlowNode param2 = null, FlowNode param3 = null, FlowNode param4 = null, FlowNode param5 = null,
            FlowNode param6 = null] 
            => _[command, this, param2, param3, param4, param5, param6];

        // 7 Parameters
        
        /// <inheritdoc cref="docs._commandindexer"/>
        public FlowNode this[
            Func<FlowNode, FlowNode, FlowNode, FlowNode, FlowNode, FlowNode, FlowNode, FlowNode> command, 
            FlowNode param2 = null, FlowNode param3 = null, FlowNode param4 = null, FlowNode param5 = null,
            FlowNode param6 = null, FlowNode param7 = null] 
            => _[command, this, param2, param3, param4, param5, param6, param7];

        // 8 Parameters
        
        /// <inheritdoc cref="docs._commandindexer"/>
        public FlowNode this[
            Func<FlowNode, FlowNode, FlowNode, FlowNode, FlowNode, FlowNode, FlowNode, FlowNode, FlowNode> command, 
            FlowNode param2 = null, FlowNode param3 = null, FlowNode param4 = null, FlowNode param5 = null,
            FlowNode param6 = null, FlowNode param7 = null, FlowNode param8 = null] 
            => _[command, this, param2, param3, param4, param5, param6, param7, param8];

        // 9 Parameters
        
        /// <inheritdoc cref="docs._commandindexer"/>
        public FlowNode this[
            Func<FlowNode, FlowNode, FlowNode, FlowNode, FlowNode, FlowNode, FlowNode, FlowNode, FlowNode, FlowNode> command, 
            FlowNode param2 = null, FlowNode param3 = null, FlowNode param4 = null, FlowNode param5 = null,
            FlowNode param6 = null, FlowNode param7 = null, FlowNode param8 = null, FlowNode param9 = null] 
            => _[command, this, param2, param3, param4, param5, param6, param7, param8, param9];

        // 10 Parameters
        
        /// <inheritdoc cref="docs._commandindexer"/>
        public FlowNode this[
            Func<FlowNode, FlowNode, FlowNode, FlowNode, FlowNode, FlowNode, FlowNode, FlowNode, FlowNode, FlowNode, FlowNode> command, 
            FlowNode param2 = null, FlowNode param3 = null, FlowNode param4 = null, FlowNode param5 = null,
            FlowNode param6 = null, FlowNode param7 = null, FlowNode param8 = null, FlowNode param9 = null, FlowNode param10 = null] 
            => _[command, this, param2, param3, param4, param5, param6, param7, param8, param9, param10];
    }
}
