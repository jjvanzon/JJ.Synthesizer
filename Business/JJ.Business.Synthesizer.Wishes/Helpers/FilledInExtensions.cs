using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using JJ.Business.Synthesizer.Structs;
using JJ.Business.Synthesizer.Wishes.docs;

namespace JJ.Business.Synthesizer.Wishes.Helpers
{
    /// <inheritdoc cref="_filledinhelper" />
    internal static class FilledInExtensions
    {
        public static bool IsNully(this  FlowNode flowNode) => FilledInSynthHelper.IsNully(flowNode);
        public static bool FilledIn(this FlowNode flowNode) => FilledInSynthHelper.FilledIn(flowNode);
        
        public static bool FilledIn(this WavHeaderStruct wavHeader) => FilledInSynthHelper.FilledIn(wavHeader);
        public static bool IsNully(this  WavHeaderStruct wavHeader) => FilledInSynthHelper.IsNully(wavHeader);
    }
}