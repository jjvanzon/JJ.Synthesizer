using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using JJ.Framework.Wishes.Common;

namespace JJ.Business.Synthesizer.Wishes.Helpers
{
    /// <summary>
    /// Extensions to the FrameworkWishes' FilledIn methods,
    /// specific to Synth objects.
    /// </summary>
    internal static class FilledInHelper
    {
        public static bool Has(FlowNode flowNode) => FilledIn(flowNode);
        public static bool FilledIn(FlowNode flowNode)
        {
            if (flowNode == null) return false;
            if (flowNode.IsConst && flowNode.Value == 0) return false;
            if (flowNode.IsSample)
            {
                var underlyingSample = flowNode.UnderlyingSample();
                return FilledInWishes.Has(underlyingSample.Bytes);
            }

            return true;
        }
    }
}
