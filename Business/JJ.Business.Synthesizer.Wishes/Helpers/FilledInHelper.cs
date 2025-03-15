using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using JJ.Business.Synthesizer.Structs;
using JJ.Business.Synthesizer.Wishes.docs;
using JJ.Framework.Nully.Core;

namespace JJ.Business.Synthesizer.Wishes.Helpers
{
    /// <inheritdoc cref="_filledinhelper" />
    internal class FilledInHelper
    {
        // FlowNode
        
        public static bool IsNully(FlowNode flowNode) => !FilledIn(flowNode);
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
        
        // WavHeader
        
        public static bool FilledIn(WavHeaderStruct wavHeader) => !wavHeader.IsNully();
        public static bool Has(WavHeaderStruct wavHeader) => !wavHeader.IsNully();
        public static bool IsNully(WavHeaderStruct wavHeader) 
            => wavHeader.ChunkID        == 0 &&
               wavHeader.ChunkSize      == 0 &&
               wavHeader.Format         == 0 &&
               wavHeader.SubChunk1ID    == 0 &&
               wavHeader.SubChunk1Size  == 0 &&
               wavHeader.AudioFormat    == 0 &&
               wavHeader.ChannelCount   == 0 &&
               wavHeader.SamplingRate   == 0 &&
               wavHeader.BytesPerSecond == 0 &&
               wavHeader.BytesPerSample == 0 &&
               wavHeader.BitsPerValue   == 0 &&
               wavHeader.SubChunk2ID    == 0 &&
               wavHeader.SubChunk2Size  == 0;
    }
}
