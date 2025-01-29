using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using JJ.Business.Synthesizer.Structs;

namespace JJ.Business.Synthesizer.Wishes.Helpers
{
    /// <inheritdoc cref="docs._filledinhelper" />
    internal static class FilledInExtensions
    {
        public static bool IsNully(this  FlowNode flowNode) => FilledInHelper.IsNully(flowNode);
        public static bool FilledIn(this FlowNode flowNode) => FilledInHelper.FilledIn(flowNode);
        
        public static bool FilledIn(this WavHeaderStruct wavHeader) => FilledInHelper.FilledIn(wavHeader);
        public static bool IsNully(this  WavHeaderStruct wavHeader) => FilledInHelper.IsNully(wavHeader);
    }
}