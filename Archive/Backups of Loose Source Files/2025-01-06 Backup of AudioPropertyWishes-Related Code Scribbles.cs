AudioPropertyWishes-Related Code Scribbles:

        public static Tape FrameCount(this Tape tape, int frameCount)
        {
            if (tape.IsBuff)
            {
                throw new Exception("Can't set the frame count when Tape it is already Buff.");
            }
            else
            {
                return AudioLength(tape, AudioLength(frameCount, SamplingRate(tape)));
            }
        }

        public static int FileLengthNeeded(this AudioFileOutput entity)
        {
            if (entity == null) throw new NullException(() => entity);
            return HeaderLength(entity) + FrameSize(entity) * (int)(entity.Duration * SamplingRate(entity));
        }
        
        public static int FileLengthNeeded(this AudioFileOutput entity, int courtesyFrames)
        {
            if (entity == null) throw new ArgumentNullException(nameof(entity));
            // CourtesyBytes to accomodate a floating-point imprecision issue in the audio loop.
            // Testing revealed 1 courtesy frame was insufficient, and 2 resolved the issue.
            // Setting it to 4 frames as a safer margin to prevent errors in the future.
            int courtesyBytes = FrameSize(entity) * courtesyFrames; 
            return HeaderLength(entity) + FrameSize(entity) * (int)(entity.Duration * SamplingRate(entity)) + courtesyBytes;
        }

        public static AudioFileOutput BytesNeeded(this AudioFileOutput entity, int byteCount, int courtesyFrames) 
            //=> ByteCount(entity, bytesNeeded - CourtesyBytes(courtesyFrames, FrameSize(entity)));
            => ByteCount(entity, byteCount + CourtesyBytes(courtesyFrames, FrameSize(entity)));

        
        public static int? Channel(this ChannelEnum enumValue)
        {
            switch (enumValue)
            {
                case ChannelEnum.Left: return 0;
                case ChannelEnum.Right: return 1;
                case ChannelEnum.Single: return 0;
                case ChannelEnum.Undefined: return null;
                default: throw new ValueNotSupportedException(enumValue);
            }
        }

        public static ChannelEnum ChannelToEnum(this int? channel, int channels)
        {
            if (channel == null) return ChannelEnum.Undefined;
            if (channels == 1 && channel == 0) return ChannelEnum.Single;
            if (channels == 2 && channel == 0) return ChannelEnum.Left;
            if (channels == 2 && channel == 1) return ChannelEnum.Right;
            throw new Exception($"Unsupported combination of values: {new { channel, channels }}");
        }


        if (signalCount == 2)
        {
            // Not for specific channel, so return null.
            return null;
        }

                                
        [Obsolete(ObsoleteMessage)]
        public static int ToIndex(this ChannelEnum channelEnum)
        {
            switch (channelEnum)
            {
                case ChannelEnum.Single: return 0;
                case ChannelEnum.Left: return 0;
                case ChannelEnum.Right: return 1;
                default: throw new ArgumentOutOfRangeException(nameof(channelEnum), channelEnum, null);
            }
        }

        private static T Assert<T> (T obj, Expression<Func<object>> expression) where T: class
        { 
            if (obj == default) throw new NullException(expression);
            return obj;
        }

        
        public SynthWishes Run(Action action, bool newInstance) => newInstance ? RunOnNew(action) : RunOnThis(action);
        public SynthWishes Run(bool newInstance, Action action) => newInstance ? RunOnNew(action) : RunOnThis(action);

        [TestMethod] public void Test8BitGetters_ConversionStyle() => TestBitGetters_ConversionStyle(8);
        
        [TestMethod] public void Test16BitGetters_ConversionStyle() => TestBitGetters_ConversionStyle(16);
        
        [TestMethod] public void Test32BitGetters_ConversionStyle() => TestBitGetters_ConversionStyle(32);

        void TestBitGetters_ConversionStyle(int bits)
        {
            var x = CreateEntities(bits);
        }
        
        /// <inheritdoc cref="docs._testaudiopropertywishesold"/>
        [TestMethod] 
        public void MonoExtensions_Test()
        {
            Tape tape = null;
            Run(() => WithMono().Sine().AfterRecord(x => tape = x));
            IsNotNull(() => tape);
            AudioFileOutput audioFileOutputMono = tape.UnderlyingAudioFileOutput;
            IsNotNull(() => audioFileOutputMono);
            IsNotNull(() => audioFileOutputMono.SpeakerSetup);
            AreEqual(SpeakerSetupEnum.Mono, () => audioFileOutputMono.SpeakerSetup.ToEnum());
        }

        /// <inheritdoc cref="docs._testaudiopropertywishesold"/>
        [TestMethod]
        public void StereoExtensions_Test()
        {
            Tape tape = null;
            Run(() => WithStereo().Sine().AfterRecord(x => tape = x).Save());
            IsNotNull(() => tape);
            AudioFileOutput audioFileOutputStereo = tape.UnderlyingAudioFileOutput;
            IsNotNull(() => audioFileOutputStereo);
            IsNotNull(() => audioFileOutputStereo.SpeakerSetup);
            AreEqual(SpeakerSetupEnum.Stereo, () => audioFileOutputStereo.SpeakerSetup.ToEnum());
        }

        /// <inheritdoc cref="docs._testaudiopropertywishesold"/>
        [TestMethod]
        public void WavExtensions_Test()
        {
            Tape tape = null;
            Run(() => AsWav().Sine().AfterRecord(x => tape = x).Save());
            IsNotNull(() => tape);
            
            AudioFileOutput audioFileOutputWav = tape.UnderlyingAudioFileOutput;
            IsNotNull(() => audioFileOutputWav);
            IsNotNull(() => audioFileOutputWav.AudioFileFormat);
            AreEqual(".wav", () => audioFileOutputWav.AudioFileFormat.FileExtension());
            AreEqual(".wav", () => audioFileOutputWav.GetAudioFileFormatEnum().FileExtension());
            AreEqual(44,     () => audioFileOutputWav.AudioFileFormat.HeaderLength());
            AreEqual(44,     () => audioFileOutputWav.GetAudioFileFormatEnum().HeaderLength());
        }

        /// <inheritdoc cref="docs._testaudiopropertywishesold"/>
        [TestMethod]
        public void RawExtensions_Test()
        {
            AudioFileOutput audioFileOutputRaw = null;
            Run(() => AsRaw().Sine().Save().AfterRecord(x => audioFileOutputRaw = x.UnderlyingAudioFileOutput));
            IsNotNull(() => audioFileOutputRaw);
            IsNotNull(() => audioFileOutputRaw.AudioFileFormat);
            AreEqual(".raw", () => audioFileOutputRaw.AudioFileFormat.FileExtension());
            AreEqual(".raw", () => audioFileOutputRaw.GetAudioFileFormatEnum().FileExtension());
            AreEqual(0,      () => audioFileOutputRaw.AudioFileFormat.HeaderLength());
            AreEqual(0,      () => audioFileOutputRaw.GetAudioFileFormatEnum().HeaderLength());
        }

        /// <inheritdoc cref="docs._testaudiopropertywishesold"/>
        [TestMethod]
        public void _16BitHelpers_Test()
        {
            AreEqual(SampleDataTypeEnum.Int16, () => 16.BitsToEnum());
        }

        /// <inheritdoc cref="docs._testaudiopropertywishesold"/>
        [TestMethod]
        public void _8BitHelpers_Test()
        {
            AreEqual(SampleDataTypeEnum.Byte, () => 8.BitsToEnum());
        }

ConfigSection setters (evil):

        //internal static ConfigSection Bits(this ConfigSection obj, int value)
        //{
        //    if (obj == null) throw new NullException(() => obj);
        //    obj.Bits = value;
        //    return obj;
        //}

        //internal static ConfigSection ByteCount(this ConfigSection obj, int value) 
        //    => AudioLength(obj, AudioLength(value, FrameSize(obj), SamplingRate(obj), HeaderLength(obj), CourtesyFrames(obj)));

        //internal static ConfigSection      With8Bit (this ConfigSection      obj)                   => Bits(obj,    8);
        //internal static ConfigSection      With16Bit(this ConfigSection      obj)                   => Bits(obj,    16);
        //internal static ConfigSection      With32Bit(this ConfigSection      obj)                   => Bits(obj,    32);

        //internal static ConfigSection Channels(this ConfigSection obj, int value)
        //{
        //    if (obj == null) throw new NullException(() => obj);
        //    obj.Channels = value;
        //    return obj;
        //}

        //internal static ConfigSection    Mono  (this ConfigSection    obj) => Channels(obj, 1);
        //internal static ConfigSection    Stereo(this ConfigSection    obj) => Channels(obj, 2);

        //internal static ConfigSection SamplingRate(this ConfigSection obj, int value)
        //{
        //    if (obj == null) throw new NullException(() => obj);
        //    obj.SamplingRate = value;
        //    return obj;
        //}
        
        //internal static ConfigSection AudioFormat(this ConfigSection obj, AudioFileFormatEnum value)
        //{
        //    if (obj == null) throw new NullException(() => obj);
        //    obj.AudioFormat = value;
        //    return obj;
        //}

        //internal static ConfigSection       AsWav(this ConfigSection       obj) => AudioFormat(obj, Wav);


        //internal static ConfigSection Interpolation(this ConfigSection obj, InterpolationTypeEnum value)
        //{
        //    if (obj == null) throw new NullException(() => obj);
        //    obj.Interpolation = value;
        //    return obj;
        //}

        //internal static ConfigSection         Linear(this ConfigSection       obj) => Interpolation(obj, Line);
        //internal static ConfigSection         Blocky(this ConfigSection         obj) => Interpolation(obj, Block);
        
        //internal static ConfigSection CourtesyFrames(this ConfigSection obj, int value)
        //{
        //    if (obj == null) throw new NullException(() => obj);
        //    obj.CourtesyFrames = value;
        //    return obj;
        //}

        //internal static ConfigSection      SizeOfBitDepth(this ConfigSection      obj, int byteSize) => Bits(obj, byteSize * 8);

        /// <inheritdoc cref="docs._fileextension"/>
        //internal static ConfigSection FileExtension(this ConfigSection obj, string value) => AudioFormat(obj, AudioFormat(value));

        //internal static ConfigSection CourtesyBytes(this ConfigSection obj, int value) 
        //    => CourtesyFrames(obj, CourtesyFrames(value, FrameSize(obj)));

        //internal static ConfigSection AudioLength(this ConfigSection obj, double value)
        //{
        //    if (obj == null) throw new NullException(() => obj);
        //    obj.AudioLength = value;
        //    return obj;
        //}

        //internal static ConfigSection FrameCount(this ConfigSection obj, int value) 
        //    => AudioLength(obj, AudioLength(value, SamplingRate(obj)));

        //set => _accessor.SetPropertyValue(MemberName(), value);

        
        //public static ConfigSectionAccessor Bits(this ConfigSectionAccessor obj, int value) 
        //{
        //    _accessor.InvokeMethod(MemberName(), obj.Obj, value); 
        //    return obj; 
        //}
        
        //public static ConfigSectionAccessor With8Bit(this ConfigSectionAccessor obj)
        //{
        //    _accessor.InvokeMethod(MemberName(), obj.Obj);
        //    return obj;
        //}
        
        //public static ConfigSectionAccessor With16Bit(this ConfigSectionAccessor obj)
        //{
        //    _accessor.InvokeMethod(MemberName(), obj.Obj);
        //    return obj;
        //}
        
        //public static ConfigSectionAccessor With32Bit(this ConfigSectionAccessor obj)
        //{
        //    _accessor.InvokeMethod(MemberName(), obj.Obj);
        //    return obj;
        //}

AudioPropertyWishesTests.cs:

    // Independent after Taping
    x.Sample.Assert_Bits(init);
    x.Sample.With8Bit(x.Context);
    x.Sample.Assert_Bits(value);

    x.AudioInfoWish.Assert_Bits(init);
    x.AudioInfoWish.With8Bit();
    x.AudioInfoWish.Assert_Bits(value);

    x.AudioFileInfo.Assert_Bits(init);
    x.AudioFileInfo.With8Bit();
    x.AudioFileInfo.Assert_Bits(value);

    // Independent after Taping
    x.Sample.Assert_Bits(init);
    x.Sample.With16Bit(x.Context);
    x.Sample.Assert_Bits(value);
    
    x.AudioInfoWish.Assert_Bits(init);
    x.AudioInfoWish.With16Bit();
    x.AudioInfoWish.Assert_Bits(value);
                
    x.AudioFileInfo.Assert_Bits(init);
    x.AudioFileInfo.With16Bit();
    x.AudioFileInfo.Assert_Bits(value);
    // Independent after Taping
    x.Sample.Assert_Bits(init);
    x.Sample.With32Bit(x.Context);
    x.Sample.Assert_Bits(value);
    
    x.AudioInfoWish.Assert_Bits(init);
    x.AudioInfoWish.With32Bit();
    x.AudioInfoWish.Assert_Bits(value);
                
    x.AudioFileInfo.Assert_Bits(init);
    x.AudioFileInfo.With32Bit();
    x.AudioFileInfo.Assert_Bits(value);
