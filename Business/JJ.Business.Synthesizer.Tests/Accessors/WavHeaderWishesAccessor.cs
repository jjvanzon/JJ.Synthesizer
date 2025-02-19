using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using JJ.Business.Synthesizer.Structs;
using JJ.Business.Synthesizer.Wishes;
using JJ.Business.Synthesizer.Wishes.Config;
using JJ.Framework.Wishes.Reflection;

namespace JJ.Business.Synthesizer.Tests.Accessors
{
    internal static class WavHeaderWishesAccessor
    {
        private static AccessorEx _accessor = new AccessorEx(typeof(WavHeaderWishes));
        
        // With ConfigResolver
        
        internal static AudioInfoWish ToWish(ConfigResolverAccessor entity, SynthWishes synthWishes) 
            => _accessor.InvokeMethod<AudioInfoWish>(entity.Obj, synthWishes);

        internal static void ApplyTo(AudioInfoWish infoWish, ConfigResolverAccessor entity, SynthWishes synthWishes) 
            => _accessor.InvokeMethod(infoWish, entity.Obj, synthWishes);

        internal static void FromWish(ConfigResolverAccessor entity, AudioInfoWish infoWish, SynthWishes synthWishes)
            => _accessor.InvokeMethod(entity.Obj, infoWish, synthWishes);
        
        // With ConfigSection
        
        internal static AudioInfoWish ToWish(ConfigSectionAccessor entity)
            => _accessor.InvokeMethod<AudioInfoWish>(entity.Obj);
        
        public static WavHeaderStruct ToWavHeader(ConfigResolverAccessor entity, SynthWishes synthWishes)
            => _accessor.InvokeMethod<WavHeaderStruct>(entity.Obj, synthWishes);
        internal static WavHeaderStruct ToWavHeader(ConfigSectionAccessor entity)
            => _accessor.InvokeMethod<WavHeaderStruct>(entity.Obj);

        internal static void ApplyWavHeader(ConfigResolverAccessor entity, WavHeaderStruct wav, SynthWishes synthWishes)
            => _accessor.InvokeMethod(entity.Obj, wav, synthWishes);
        internal static void ApplyWavHeader(WavHeaderStruct wav, ConfigResolverAccessor entity, SynthWishes synthWishes)
            => _accessor.InvokeMethod(wav, entity.Obj, synthWishes);
        
        internal static void ReadWavHeader(ConfigResolverAccessor entity, string filePath, SynthWishes synthWishes)
            => _accessor.InvokeMethod(entity.Obj, filePath, synthWishes);
        internal static void ReadWavHeader(ConfigResolverAccessor entity, byte[] source, SynthWishes synthWishes)
            => _accessor.InvokeMethod(entity.Obj, source, synthWishes);
        internal static void ReadWavHeader(ConfigResolverAccessor entity, Stream source, SynthWishes synthWishes)
            => _accessor.InvokeMethod(entity.Obj, source, synthWishes);
        internal static void ReadWavHeader(ConfigResolverAccessor entity, BinaryReader source, SynthWishes synthWishes)
            => _accessor.InvokeMethod(entity.Obj, source, synthWishes);
        internal static void ReadWavHeader(string filePath, ConfigResolverAccessor entity, SynthWishes synthWishes)
            => _accessor.InvokeMethod(new[]{ filePath, entity.Obj, synthWishes });
        internal static void ReadWavHeader(byte[] source, ConfigResolverAccessor entity, SynthWishes synthWishes)
            => _accessor.InvokeMethod(source, entity.Obj, synthWishes);
        internal static void ReadWavHeader(Stream source, ConfigResolverAccessor entity, SynthWishes synthWishes)
            => _accessor.InvokeMethod(source, entity.Obj, synthWishes);
        internal static void ReadWavHeader(BinaryReader source, ConfigResolverAccessor entity, SynthWishes synthWishes)
            => _accessor.InvokeMethod(source, entity.Obj, synthWishes);
            
    }
}
