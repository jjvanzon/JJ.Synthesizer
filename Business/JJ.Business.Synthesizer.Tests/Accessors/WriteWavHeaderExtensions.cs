using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using JJ.Business.Synthesizer.Structs;
using JJ.Business.Synthesizer.Wishes;
using JJ.Business.Synthesizer.Wishes.Config;
using JJ.Framework.Reflection.Core;

namespace JJ.Business.Synthesizer.Tests.Accessors
{
    internal static partial class WavExtensionWishesAccessor
    {
        private static AccessorCore _accessor = new AccessorCore(typeof(WavExtensionWishes));

        // ToInfo
        
        internal static AudioInfoWish ToInfo(this ConfigResolverAccessor entity, SynthWishes synthWishes)
            => (AudioInfoWish)_accessor.InvokeMethod(entity?.Obj, synthWishes);
        
        internal static AudioInfoWish ToInfo(this ConfigSectionAccessor entity)            
            => (AudioInfoWish)_accessor.InvokeMethod(entity?.Obj);

        // ApplyInfo
        
        internal static ConfigResolverAccessor ApplyInfo(this ConfigResolverAccessor obj, AudioInfoWish infoWish, SynthWishes synthWishes)
            => new ConfigResolverAccessor(_accessor.InvokeMethod(obj.Obj, infoWish, synthWishes));
        
        internal static AudioInfoWish ApplyInfo(this AudioInfoWish infoWish, ConfigResolverAccessor obj, SynthWishes synthWishes)
            => (AudioInfoWish)_accessor.InvokeMethod(infoWish, obj.Obj, synthWishes);

        // ToWavHeader
        
        internal static WavHeaderStruct ToWavHeader(this ConfigResolverAccessor entity, SynthWishes synthWishes)
            => (WavHeaderStruct)_accessor.InvokeMethod(entity?.Obj, synthWishes);
        
        internal static WavHeaderStruct ToWavHeader(this ConfigSectionAccessor entity)            
            => (WavHeaderStruct)_accessor.InvokeMethod(entity?.Obj);

        // ApplyWavHeader
        
        internal static ConfigResolverAccessor ApplyWavHeader(this ConfigResolverAccessor entity, WavHeaderStruct wavHeader, SynthWishes synthWishes)
            => new ConfigResolverAccessor(_accessor.InvokeMethod(entity?.Obj, wavHeader, synthWishes));
        
        internal static WavHeaderStruct ApplyWavHeader(this WavHeaderStruct wavHeader, ConfigResolverAccessor entity, SynthWishes synthWishes)
            => (WavHeaderStruct)_accessor.InvokeMethod(wavHeader, entity?.Obj, synthWishes);

        // ReadWavHeader

        internal static ConfigResolverAccessor ReadWavHeader(this ConfigResolverAccessor entity, string filePath, SynthWishes synthWishes)
            => new ConfigResolverAccessor(_accessor.InvokeMethod(entity?.Obj, filePath, synthWishes));
        internal static ConfigResolverAccessor ReadWavHeader(this ConfigResolverAccessor entity, byte[] source, SynthWishes synthWishes)
            => new ConfigResolverAccessor(_accessor.InvokeMethod(entity?.Obj, source, synthWishes));
        internal static ConfigResolverAccessor ReadWavHeader(this ConfigResolverAccessor entity, Stream source, SynthWishes synthWishes)
            => new ConfigResolverAccessor(_accessor.InvokeMethod(entity?.Obj, source, synthWishes));
        internal static ConfigResolverAccessor ReadWavHeader(this ConfigResolverAccessor entity, BinaryReader source, SynthWishes synthWishes)
            => new ConfigResolverAccessor(_accessor.InvokeMethod(entity?.Obj, source, synthWishes));
        
        internal static string ReadWavHeader(this string filePath, ConfigResolverAccessor entity, SynthWishes synthWishes)
            => (string)_accessor.InvokeMethod(new[]{ filePath, entity?.Obj, synthWishes });
        internal static byte[] ReadWavHeader(this byte[] source, ConfigResolverAccessor entity, SynthWishes synthWishes)
            => (byte[])_accessor.InvokeMethod(source, entity?.Obj, synthWishes);
        internal static Stream ReadWavHeader(this Stream source, ConfigResolverAccessor entity, SynthWishes synthWishes)
            => (Stream)_accessor.InvokeMethod(source, entity?.Obj, synthWishes);
        internal static BinaryReader ReadWavHeader(this BinaryReader source, ConfigResolverAccessor entity, SynthWishes synthWishes)
            => (BinaryReader)_accessor.InvokeMethod(source, entity?.Obj, synthWishes);

        // WriteWavHeader
        
        // With ConfigResolver

        internal static ConfigResolverAccessor WriteWavHeader(this ConfigResolverAccessor entity, string filePath, SynthWishes synthWishes)
            => new ConfigResolverAccessor(_accessor.InvokeMethod(entity?.Obj, filePath, synthWishes));
        
        internal static ConfigResolverAccessor WriteWavHeader(this ConfigResolverAccessor entity, byte[] dest, SynthWishes synthWishes)
            => new ConfigResolverAccessor(_accessor.InvokeMethod(entity?.Obj, dest, synthWishes));
        
        internal static ConfigResolverAccessor WriteWavHeader(this ConfigResolverAccessor entity, BinaryWriter dest, SynthWishes synthWishes)
            => new ConfigResolverAccessor(_accessor.InvokeMethod(entity?.Obj, dest, synthWishes));
        
        internal static ConfigResolverAccessor WriteWavHeader(this ConfigResolverAccessor entity, Stream dest, SynthWishes synthWishes)
            => new ConfigResolverAccessor(_accessor.InvokeMethod(entity?.Obj, dest, synthWishes));
        
        internal static string WriteWavHeader(this string filePath, ConfigResolverAccessor entity, SynthWishes synthWishes)
            => (string)_accessor.InvokeMethod(new[]{ filePath, entity?.Obj, synthWishes });
        
        internal static byte[] WriteWavHeader(this byte[] dest, ConfigResolverAccessor entity, SynthWishes synthWishes)
            => (byte[])_accessor.InvokeMethod(dest, entity?.Obj, synthWishes);
        
        internal static Stream WriteWavHeader(this Stream dest, ConfigResolverAccessor entity, SynthWishes synthWishes)
            => (Stream)_accessor.InvokeMethod(dest, entity?.Obj, synthWishes);
        
        internal static BinaryWriter WriteWavHeader(this BinaryWriter dest, ConfigResolverAccessor entity, SynthWishes synthWishes)
            => (BinaryWriter)_accessor.InvokeMethod(dest, entity?.Obj, synthWishes);

        // With ConfigSection
        
        internal static ConfigSectionAccessor WriteWavHeader(this ConfigSectionAccessor entity, string filePath)
            => new ConfigSectionAccessor(_accessor.InvokeMethod(new [] { entity?.Obj, filePath }));
        
        internal static ConfigSectionAccessor WriteWavHeader(this ConfigSectionAccessor entity, byte[] dest)
            => new ConfigSectionAccessor(_accessor.InvokeMethod(entity?.Obj, dest));
        
        internal static ConfigSectionAccessor WriteWavHeader(this ConfigSectionAccessor entity, BinaryWriter dest)
            => new ConfigSectionAccessor(_accessor.InvokeMethod(entity?.Obj, dest));
        
        internal static ConfigSectionAccessor WriteWavHeader(this ConfigSectionAccessor entity, Stream dest)
            => new ConfigSectionAccessor(_accessor.InvokeMethod(entity?.Obj, dest));
        
        internal static string WriteWavHeader(this string filePath, ConfigSectionAccessor entity)
            => (string)_accessor.InvokeMethod(new[] { filePath, entity?.Obj });
        
        internal static byte[] WriteWavHeader(this byte[] dest, ConfigSectionAccessor entity)
            => (byte[])_accessor.InvokeMethod(dest, entity?.Obj);
        
        internal static Stream WriteWavHeader(this Stream dest, ConfigSectionAccessor entity)
            => (Stream)_accessor.InvokeMethod(dest, entity?.Obj);
        
        internal static BinaryWriter WriteWavHeader(this BinaryWriter dest, ConfigSectionAccessor entity)
            => (BinaryWriter)_accessor.InvokeMethod(dest, entity?.Obj);

    }
}

