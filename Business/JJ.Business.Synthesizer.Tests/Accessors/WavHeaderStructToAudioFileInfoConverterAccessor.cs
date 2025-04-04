using System;
using System.Reflection;
using JJ.Business.Synthesizer.Infos;
using JJ.Business.Synthesizer.Managers;
using JJ.Business.Synthesizer.Structs;
using JJ.Framework.Reflection;
using JJ.Framework.Reflection.Core;

namespace JJ.Business.Synthesizer.Tests.Accessors
{
    internal static class WavHeaderStructToAudioFileInfoConverterAccessor
    {
        private static readonly AccessorCore _accessor;

        static WavHeaderStructToAudioFileInfoConverterAccessor()
        {
            Assembly assembly = typeof(WavHeaderManager).Assembly;
            string typeName = "JJ.Business.Synthesizer.Converters.WavHeaderStructToAudioFileInfoConverter";
            Type type = assembly.GetType(typeName, true);
            _accessor = new AccessorCore(type);
        }

        public static AudioFileInfo Convert(WavHeaderStruct wavHeaderStruct)
            => (AudioFileInfo)_accessor.InvokeMethod(nameof(Convert), wavHeaderStruct);
    }
}