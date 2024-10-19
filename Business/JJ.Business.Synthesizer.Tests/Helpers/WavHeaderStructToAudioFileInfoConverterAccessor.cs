using System;
using System.Reflection;
using JJ.Business.Synthesizer.Infos;
using JJ.Business.Synthesizer.Managers;
using JJ.Business.Synthesizer.Structs;
using JJ.Framework.Reflection;

namespace JJ.Business.Synthesizer.Tests.Helpers
{
    internal static class WavHeaderStructToAudioFileInfoConverterAccessor
    {
        private static readonly Accessor _accessor;

        static WavHeaderStructToAudioFileInfoConverterAccessor()
        {
            Assembly assembly = typeof(WavHeaderManager).Assembly;
            string typeName = "JJ.Business.Synthesizer.Converters.WavHeaderStructToAudioFileInfoConverter";
            Type type = assembly.GetType(typeName, true);
            _accessor = new Accessor(type);
        }

        public static AudioFileInfo Convert(WavHeaderStruct wavHeaderStruct)
            => (AudioFileInfo)_accessor.InvokeMethod(nameof(Convert), wavHeaderStruct);
    }
}