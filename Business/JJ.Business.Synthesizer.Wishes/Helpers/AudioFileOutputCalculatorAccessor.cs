using System;
using System.IO;
using System.Reflection;
using JJ.Business.Synthesizer.Calculation.AudioFileOutputs;
using JJ.Framework.Reflection;

namespace JJ.Business.Synthesizer.Wishes.Helpers
{
    internal class AudioFileOutputCalculatorAccessor
    {
        private readonly Accessor _accessor;
        
        public AudioFileOutputCalculatorAccessor(IAudioFileOutputCalculator obj)
        {
            Assembly assembly = typeof(IAudioFileOutputCalculator).Assembly;
            string typeName = "JJ.Business.Synthesizer.Calculation.AudioFileOutputs.AudioFileOutputCalculatorBase";
            Type type = assembly.GetType(typeName, true);
            _accessor = new Accessor(obj, type);
        }

        public Stream _stream
        {
            get => (Stream)_accessor.GetFieldValue(nameof(_stream));
            set => _accessor.SetFieldValue(nameof(_stream), value);
        }
    }
}
