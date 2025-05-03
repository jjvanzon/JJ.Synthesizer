using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using JetBrains.Annotations;
using JJ.Business.Synthesizer.Structs;
using JJ.Business.Synthesizer.Wishes;
using JJ.Business.Synthesizer.Wishes.Config;
using JJ.Framework.Reflection.Core;

namespace JJ.Business.Synthesizer.Tests.Accessors
{
    internal static class WavWishesAccessor
    {
        private static AccessorCore _accessor = new AccessorCore(typeof(WavWishes));
        
        // With ConfigResolver
        
        internal static AudioInfoWish ToInfo(ConfigResolverAccessor entity, SynthWishes synthWishes) 
            => (AudioInfoWish)_accessor.Call(entity.Obj, synthWishes);

        internal static AudioInfoWish ApplyInfo(AudioInfoWish infoWish, ConfigResolverAccessor entity, SynthWishes synthWishes) 
            => (AudioInfoWish)_accessor.Call(infoWish, entity.Obj, synthWishes);

        internal static ConfigResolverAccessor ApplyInfo(ConfigResolverAccessor entity, AudioInfoWish infoWish, SynthWishes synthWishes)
            => new ConfigResolverAccessor(_accessor.Call(entity.Obj, infoWish, synthWishes));
        
        // With ConfigSection
        
        internal static AudioInfoWish ToInfo(ConfigSectionAccessor entity)
            => (AudioInfoWish)_accessor.Call(entity.Obj);
        
        public static WavHeaderStruct ToWavHeader(ConfigResolverAccessor entity, SynthWishes synthWishes)
            => (WavHeaderStruct)_accessor.Call(entity.Obj, synthWishes);
        internal static WavHeaderStruct ToWavHeader(ConfigSectionAccessor entity)
            => (WavHeaderStruct)_accessor.Call(entity.Obj);

        internal static ConfigResolverAccessor ApplyWavHeader(ConfigResolverAccessor entity, WavHeaderStruct wav, SynthWishes synthWishes)
            => new ConfigResolverAccessor(_accessor.Call(entity.Obj, wav, synthWishes));
        internal static WavHeaderStruct ApplyWavHeader(WavHeaderStruct wav, ConfigResolverAccessor entity, SynthWishes synthWishes)
            => (WavHeaderStruct)_accessor.Call(wav, entity.Obj, synthWishes);
        
        internal static ConfigResolverAccessor ReadWavHeader(ConfigResolverAccessor entity, string filePath, SynthWishes synthWishes)
            => new ConfigResolverAccessor(_accessor.Call(entity.Obj, filePath, synthWishes));
        internal static ConfigResolverAccessor ReadWavHeader(ConfigResolverAccessor entity, byte[] source, SynthWishes synthWishes)
            => new ConfigResolverAccessor(_accessor.Call(entity.Obj, source, synthWishes));
        internal static ConfigResolverAccessor ReadWavHeader(ConfigResolverAccessor entity, Stream source, SynthWishes synthWishes)
            => new ConfigResolverAccessor(_accessor.Call(entity.Obj, source, synthWishes));
        internal static ConfigResolverAccessor ReadWavHeader(ConfigResolverAccessor entity, BinaryReader source, SynthWishes synthWishes)
            => new ConfigResolverAccessor(_accessor.Call(entity.Obj, source, synthWishes));
        internal static string ReadWavHeader(string filePath, ConfigResolverAccessor entity, SynthWishes synthWishes)
            => (string)_accessor.Call(new[]{ filePath, entity.Obj, synthWishes });
        internal static byte[] ReadWavHeader(byte[] source, ConfigResolverAccessor entity, SynthWishes synthWishes)
            => (byte[])_accessor.Call(source, entity.Obj, synthWishes);
        internal static Stream ReadWavHeader(Stream source, ConfigResolverAccessor entity, SynthWishes synthWishes)
            => (Stream)_accessor.Call(source, entity.Obj, synthWishes);
        internal static BinaryReader ReadWavHeader(BinaryReader source, ConfigResolverAccessor entity, SynthWishes synthWishes)
            => (BinaryReader)_accessor.Call(source, entity.Obj, synthWishes);
        
        internal static ConfigResolverAccessor WriteWavHeader(ConfigResolverAccessor entity, string filePath, SynthWishes synthWishes)
            => new ConfigResolverAccessor(_accessor.Call(entity.Obj, filePath, synthWishes));
        internal static ConfigResolverAccessor WriteWavHeader(ConfigResolverAccessor entity, byte[] dest, SynthWishes synthWishes)
            => new ConfigResolverAccessor(_accessor.Call(entity.Obj, dest, synthWishes));
        internal static ConfigResolverAccessor WriteWavHeader(ConfigResolverAccessor entity, BinaryWriter dest, SynthWishes synthWishes)
            => new ConfigResolverAccessor(_accessor.Call(entity.Obj, dest, synthWishes));
        internal static ConfigResolverAccessor WriteWavHeader(ConfigResolverAccessor entity, Stream dest, SynthWishes synthWishes)
            => new ConfigResolverAccessor(_accessor.Call(entity.Obj, dest, synthWishes));
        internal static string WriteWavHeader(string filePath, ConfigResolverAccessor entity, SynthWishes synthWishes)
            => (string)_accessor.Call(new[] { filePath, entity.Obj, synthWishes });
        internal static byte[] WriteWavHeader(byte[] dest, ConfigResolverAccessor entity, SynthWishes synthWishes)
            => (byte[])_accessor.Call(dest, entity.Obj, synthWishes);
        internal static Stream WriteWavHeader(Stream dest, ConfigResolverAccessor entity, SynthWishes synthWishes)
            => (Stream)_accessor.Call(dest, entity.Obj, synthWishes);
        internal static BinaryWriter WriteWavHeader(BinaryWriter dest, ConfigResolverAccessor entity, SynthWishes synthWishes)
            => (BinaryWriter)_accessor.Call(dest, entity.Obj, synthWishes);
        internal static ConfigSectionAccessor WriteWavHeader(ConfigSectionAccessor entity, string filePath)
            => new ConfigSectionAccessor(_accessor.Call(new[]{ entity.Obj, filePath }));
        internal static ConfigSectionAccessor WriteWavHeader(ConfigSectionAccessor entity, byte[] dest)
            => new ConfigSectionAccessor(_accessor.Call(entity.Obj, dest));
        internal static ConfigSectionAccessor WriteWavHeader(ConfigSectionAccessor entity, BinaryWriter dest)
            => new ConfigSectionAccessor(_accessor.Call(entity.Obj, dest));
        internal static ConfigSectionAccessor WriteWavHeader(ConfigSectionAccessor entity, Stream dest)
            => new ConfigSectionAccessor(_accessor.Call(entity.Obj, dest));
        internal static string WriteWavHeader(string filePath, ConfigSectionAccessor entity)
            => (string)_accessor.Call(new[] { filePath, entity.Obj });
        internal static byte[] WriteWavHeader(byte[] dest, ConfigSectionAccessor entity)
            => (byte[])_accessor.Call(dest, entity.Obj);
        internal static Stream WriteWavHeader(Stream dest, ConfigSectionAccessor entity)
            => (Stream)_accessor.Call(dest, entity.Obj);
        internal static BinaryWriter WriteWavHeader(BinaryWriter dest, ConfigSectionAccessor entity)
            => (BinaryWriter)_accessor.Call(dest, entity.Obj);
    }
}
