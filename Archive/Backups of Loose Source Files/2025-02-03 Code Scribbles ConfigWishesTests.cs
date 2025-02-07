2025-02-03 Code Scribbles FileExtensionWishes:

        ///// <inheritdoc cref="docs._fileextension"/>
        //public static AudioFileFormatEnum AsAudioFormat(this string fileExtension)
        //    => FileExtensionToAudioFormat(fileExtension);
        ///// <inheritdoc cref="docs._fileextension"/>
        //public static AudioFileFormatEnum ToAudioFormat(this string fileExtension)
        //    => FileExtensionToAudioFormat(fileExtension);

2025-02-05 Code Scribbles Accessors:

        private static ConfigResolverAccessor ConfigResolverSetter(this ConfigResolverAccessor obj, int? value, [CallerMemberName] string callerMemberName = null)
            => new ConfigResolverAccessor(_accessor.InvokeMethod(
                callerMemberName,
                new[] { obj.Obj, value },
                new[] { null, typeof(int?) }));


        //private static ConfigResolverAccessor ConfigResolverSetter<TValue>(Accessor_Adapted accessor, ConfigResolverAccessor obj, TValue value, [CallerMemberName] string callerMemberName = null)
        //    => new ConfigResolverAccessor(accessor.InvokeMethod(
        //        callerMemberName,
        //        new[] { obj.Obj, value },
        //        new[] { null, typeof(TValue) }));


2024-02-07 BitWishesTests:

        //AreEqual(bits,       () => x.SynthBound.Derived       .GetBits_Call);
        //AreEqual(bits ==  8, () => x.SynthBound.Derived       .Is8Bit_Call );
        //AreEqual(bits == 16, () => x.SynthBound.Derived       .Is16Bit_Call);
        //AreEqual(bits == 32, () => x.SynthBound.Derived       .Is32Bit_Call);


2024-02-07 ByteCountWishesTests:

            if (testCase.AudioLength.Changed)
            {
                AssertProp(x => AreEqual(x.SynthWishes,    x.SynthWishes   .SetAudioLength(testCase.AudioLength)));
                //AssertProp(x => AreEqual(x.FlowNode,       x.FlowNode      .SetAudioLength(testCase.AudioLength)));
                //AssertProp(x => AreEqual(x.ConfigResolver, x.ConfigResolver.SetAudioLength(testCase.AudioLength, x.SynthWishes)));
            }
                        
            if (testCase.FrameCount.Changed)
            {
                AssertProp(x => AreEqual(x.SynthWishes,    x.SynthWishes   .SetFrameCount(testCase.FrameCount)));
                //AssertProp(x => AreEqual(x.FlowNode,       x.FlowNode      .SetFrameCount(testCase.FrameCount)));
                //AssertProp(x => AreEqual(x.ConfigResolver, x.ConfigResolver.SetFrameCount(testCase.FrameCount, x.SynthWishes)));
            }

            if (testCase.SamplingRate.Changed)
            {
                AssertProp(x => AreEqual(x.SynthWishes,    x.SynthWishes   .SetSamplingRate(testCase.SamplingRate)));
                //AssertProp(x => AreEqual(x.FlowNode,       x.FlowNode      .SetSamplingRate(testCase.SamplingRate)));
                //AssertProp(x => AreEqual(x.ConfigResolver, x.ConfigResolver.SetSamplingRate(testCase.SamplingRate)));
            }
            
            if (testCase.Bits.Changed)
            {
                AssertProp(x => AreEqual(x.SynthWishes,    x.SynthWishes   .SetBits(testCase.Bits)));
                //AssertProp(x => AreEqual(x.FlowNode,       x.FlowNode      .SetBits(testCase.Bits)));
                //AssertProp(x => AreEqual(x.ConfigResolver, x.ConfigResolver.SetBits(testCase.Bits)));
            }
            
            if (testCase.Channels.Changed)
            {
                AssertProp(x => AreEqual(x.SynthWishes,    x.SynthWishes   .SetChannels(testCase.Channels)));
                //AssertProp(x => AreEqual(x.FlowNode,       x.FlowNode      .SetChannels(testCase.Channels)));
                //AssertProp(x => AreEqual(x.ConfigResolver, x.ConfigResolver.SetChannels(testCase.Channels)));
            }
            
            if (testCase.HeaderLength.Changed)
            {
                AssertProp(x => AreEqual(x.SynthWishes,    x.SynthWishes   .SetHeaderLength(testCase.HeaderLength)));
                //AssertProp(x => AreEqual(x.FlowNode,       x.FlowNode      .SetHeaderLength(testCase.HeaderLength)));
                //AssertProp(x => AreEqual(x.ConfigResolver, x.ConfigResolver.SetHeaderLength(testCase.HeaderLength)));
            }

            if (testCase.CourtesyFrames.Changed)
            {
                AssertProp(x => AreEqual(x.SynthWishes,    x.SynthWishes   .SetCourtesyFrames(testCase.CourtesyFrames)));
                //AssertProp(x => AreEqual(x.FlowNode,       x.FlowNode      .SetCourtesyFrames(testCase.CourtesyFrames)));
                //AssertProp(x => AreEqual(x.ConfigResolver, x.ConfigResolver.SetCourtesyFrames(testCase.CourtesyFrames)));
            }


            if (testCase.AudioLength.Changed)
            {
                AssertProp(x =>                                   x.TapeBound.Tape       .Duration     = testCase.AudioLength);
                //AssertProp(x => AreEqual(x.TapeBound.Tape,        x.TapeBound.Tape       .SetAudioLength(testCase.AudioLength.To)));
                //AssertProp(x => AreEqual(x.TapeBound.TapeConfig,  x.TapeBound.TapeConfig .SetAudioLength(testCase.AudioLength.To)));
                //AssertProp(x => AreEqual(x.TapeBound.TapeActions, x.TapeBound.TapeActions.SetAudioLength(testCase.AudioLength.To)));
                //AssertProp(x => AreEqual(x.TapeBound.TapeAction,  x.TapeBound.TapeAction .SetAudioLength(testCase.AudioLength.To)));
            }
                        
            if (testCase.FrameCount.Changed)
            {
                AssertProp(x => AreEqual(x.TapeBound.Tape,        x.TapeBound.Tape       .SetFrameCount(testCase.FrameCount)));
                //AssertProp(x => AreEqual(x.TapeBound.TapeConfig,  x.TapeBound.TapeConfig .SetFrameCount(testCase.FrameCount.To)));
                //AssertProp(x => AreEqual(x.TapeBound.TapeActions, x.TapeBound.TapeActions.SetFrameCount(testCase.FrameCount.To)));
                //AssertProp(x => AreEqual(x.TapeBound.TapeAction,  x.TapeBound.TapeAction .SetFrameCount(testCase.FrameCount.To)));
            }

            if (testCase.SamplingRate.Changed)
            {
                AssertProp(x =>                                   x.TapeBound.TapeConfig .SamplingRate  = testCase.SamplingRate);
                //AssertProp(x => AreEqual(x.TapeBound.Tape,        x.TapeBound.Tape       .SetSamplingRate(testCase.SamplingRate.To)));
                //AssertProp(x => AreEqual(x.TapeBound.TapeConfig,  x.TapeBound.TapeConfig .SetSamplingRate(testCase.SamplingRate.To)));
                //AssertProp(x => AreEqual(x.TapeBound.TapeActions, x.TapeBound.TapeActions.SetSamplingRate(testCase.SamplingRate.To)));
                //AssertProp(x => AreEqual(x.TapeBound.TapeAction,  x.TapeBound.TapeAction .SetSamplingRate(testCase.SamplingRate.To)));
            }
            
            if (testCase.Bits.Changed)
            {
                AssertProp(x =>                                   x.TapeBound.TapeConfig .Bits  = testCase.Bits);
                //AssertProp(x => AreEqual(x.TapeBound.Tape,        x.TapeBound.Tape       .SetBits(testCase.Bits.To)));
                //AssertProp(x => AreEqual(x.TapeBound.TapeConfig,  x.TapeBound.TapeConfig .SetBits(testCase.Bits.To)));
                //AssertProp(x => AreEqual(x.TapeBound.TapeActions, x.TapeBound.TapeActions.SetBits(testCase.Bits.To)));
                //AssertProp(x => AreEqual(x.TapeBound.TapeAction,  x.TapeBound.TapeAction .SetBits(testCase.Bits.To)));
            }
            
            if (testCase.Channels.Changed)
            {
                AssertProp(x =>                                   x.TapeBound.TapeConfig .Channels = testCase.Channels);
                //AssertProp(x => AreEqual(x.TapeBound.Tape,        x.TapeBound.Tape       .SetChannels  (testCase.Channels.To)));
                //AssertProp(x => AreEqual(x.TapeBound.TapeConfig,  x.TapeBound.TapeConfig .SetChannels  (testCase.Channels.To)));
                //AssertProp(x => AreEqual(x.TapeBound.TapeActions, x.TapeBound.TapeActions.SetChannels  (testCase.Channels.To)));
                //AssertProp(x => AreEqual(x.TapeBound.TapeAction,  x.TapeBound.TapeAction .SetChannels  (testCase.Channels.To)));
            }
            
            if (testCase.HeaderLength.Changed)
            {
                //AssertProp(x => AreEqual(x.TapeBound.Tape,        x.TapeBound.Tape       .SetHeaderLength(testCase.HeaderLength.To)));
                AssertProp(x => AreEqual(x.TapeBound.TapeConfig,  x.TapeBound.TapeConfig .SetHeaderLength(testCase.HeaderLength)));
                //AssertProp(x => AreEqual(x.TapeBound.TapeActions, x.TapeBound.TapeActions.SetHeaderLength(testCase.HeaderLength.To)));
                //AssertProp(x => AreEqual(x.TapeBound.TapeAction,  x.TapeBound.TapeAction .SetHeaderLength(testCase.HeaderLength.To)));
            }

            if (testCase.CourtesyFrames.Changed)
            {
                AssertProp(x => x.TapeBound.TapeConfig.CourtesyFrames =  testCase.CourtesyFrames);
                //AssertProp(x => AreEqual(x.TapeBound.Tape,        x.TapeBound.Tape       .SetCourtesyFrames(testCase.CourtesyFrames.To)));
                //AssertProp(x => AreEqual(x.TapeBound.TapeConfig,  x.TapeBound.TapeConfig .SetCourtesyFrames(testCase.CourtesyFrames.To)));
                //AssertProp(x => AreEqual(x.TapeBound.TapeActions, x.TapeBound.TapeActions.SetCourtesyFrames(testCase.CourtesyFrames.To)));
                //AssertProp(x => AreEqual(x.TapeBound.TapeAction,  x.TapeBound.TapeAction .SetCourtesyFrames(testCase.CourtesyFrames.To)));
            }

            
            if (testCase.AudioLength.Changed)
            {
                AssertProp(x =>                                       x.BuffBound.AudioFileOutput.Duration     = testCase.AudioLength);
                AssertProp(x => AreEqual(x.BuffBound.Buff,            x.BuffBound.Buff           .SetAudioLength(testCase.AudioLength)));
                AssertProp(x => AreEqual(x.BuffBound.AudioFileOutput, x.BuffBound.AudioFileOutput.SetAudioLength(testCase.AudioLength)));
            }
            
            if (testCase.FrameCount.Changed)
            {
                AssertProp(x => AreEqual(x.BuffBound.Buff,            x.BuffBound.Buff           .SetFrameCount(testCase.FrameCount, courtesyFrames.Nully)));
                AssertProp(x => AreEqual(x.BuffBound.AudioFileOutput, x.BuffBound.AudioFileOutput.SetFrameCount(testCase.FrameCount, courtesyFrames.Nully)));
            }

            if (testCase.SamplingRate.Changed)
            {
                AssertProp(x =>                                       x.BuffBound.AudioFileOutput.SamplingRate  = testCase.SamplingRate);
                AssertProp(x => AreEqual(x.BuffBound.Buff,            x.BuffBound.Buff           .SetSamplingRate(testCase.SamplingRate)));
                AssertProp(x => AreEqual(x.BuffBound.AudioFileOutput, x.BuffBound.AudioFileOutput.SetSamplingRate(testCase.SamplingRate)));
            }
            
            if (testCase.Bits.Changed)
            {
                AssertProp(x => AreEqual(x.BuffBound.Buff,            x.BuffBound.Buff           .Bits(testCase.Bits, x.SynthBound.Context)));
                AssertProp(x => AreEqual(x.BuffBound.AudioFileOutput, x.BuffBound.AudioFileOutput.Bits(testCase.Bits, x.SynthBound.Context)));
            }
            
            if (testCase.Channels.Changed)
            {
                AssertProp(x => AreEqual(x.BuffBound.Buff,            x.BuffBound.Buff           .Channels(testCase.Channels, x.SynthBound.Context)));
                AssertProp(x => AreEqual(x.BuffBound.AudioFileOutput, x.BuffBound.AudioFileOutput.Channels(testCase.Channels, x.SynthBound.Context)));
            }
            
            if (testCase.HeaderLength.Changed)
            {
                AssertProp(x => AreEqual(x.BuffBound.Buff,            x.BuffBound.Buff           .HeaderLength(testCase.HeaderLength, x.SynthBound.Context)));
                AssertProp(x => AreEqual(x.BuffBound.AudioFileOutput, x.BuffBound.AudioFileOutput.HeaderLength(testCase.HeaderLength, x.SynthBound.Context)));
            }

2025-02-07 ChannelWishesTests:

            // NOTE: Might not need these as they seem to test a different property (Channels).
            AssertProp(x => { if      (val.channels.nully    == 1) AreEqual(x.SynthWishes,    () => x.SynthWishes.WithMono().WithChannel(val.channel.nully));
                              else if (val.channels.nully    == 2) AreEqual(x.SynthWishes,    () => x.SynthWishes.WithStereo().WithChannel(val.channel.nully)); 
                              else if (val.channels.coalesce == 1) AreEqual(x.SynthWishes,    () => x.SynthWishes.WithChannels(val.channels.nully).WithChannel(val.channel.nully)); });
            AssertProp(x => { if      (val.channels.nully    == 1) AreEqual(x.SynthWishes,    () => x.SynthWishes.Mono  ().Channel(val.channel.nully));
                              else if (val.channels.nully    == 2) AreEqual(x.SynthWishes,    () => x.SynthWishes.Stereo().Channel(val.channel.nully)); 
                              else if (val.channels.coalesce == 1) AreEqual(x.SynthWishes,    () => x.SynthWishes.Channels(val.channels.nully).Channel(val.channel.nully)); });
            AssertProp(x => { if      (val.channels.nully    == 1) AreEqual(x.FlowNode,       () => x.FlowNode.WithMono  ().WithChannel(val.channel.nully));
                              else if (val.channels.nully    == 2) AreEqual(x.FlowNode,       () => x.FlowNode.WithStereo().WithChannel(val.channel.nully)); 
                              else if (val.channels.coalesce == 1) AreEqual(x.FlowNode,       () => x.FlowNode.WithChannels(val.channels.nully).WithChannel(val.channel.nully)); });
            AssertProp(x => { if      (val.channels.nully    == 1) AreEqual(x.FlowNode,       () => x.FlowNode.Mono  ().Channel(val.channel.nully));
                              else if (val.channels.nully    == 2) AreEqual(x.FlowNode,       () => x.FlowNode.Stereo().Channel(val.channel.nully)); 
                              else if (val.channels.coalesce == 1) AreEqual(x.FlowNode,       () => x.FlowNode.Channels(val.channels.nully).Channel(val.channel.nully)); });
            AssertProp(x => { if      (val.channels.nully    == 1) AreEqual(x.ConfigResolver, () => x.ConfigResolver.WithMono  ().WithChannel(val.channel.nully));
                              else if (val.channels.nully    == 2) AreEqual(x.ConfigResolver, () => x.ConfigResolver.WithStereo().WithChannel(val.channel.nully)); 
                              else if (val.channels.coalesce == 1) AreEqual(x.ConfigResolver, () => x.ConfigResolver.WithChannels(val.channels.nully).WithChannel(val.channel.nully)); });
            AssertProp(x => { if      (val.channels.nully    == 1) AreEqual(x.ConfigResolver, () => x.ConfigResolver.Mono  ().Channel(val.channel.nully));
                              else if (val.channels.nully    == 2) AreEqual(x.ConfigResolver, () => x.ConfigResolver.Stereo().Channel(val.channel.nully)); 
                              else if (val.channels.coalesce == 1) AreEqual(x.ConfigResolver, () => x.ConfigResolver.Channels(val.channels.nully).Channel(val.channel.nully)); });

            
            AssertProp(x => {
                if (val.channels == MonoChannels  ) AreEqual(x.TapeBound.Tape, () => x.TapeBound.Tape.Mono().  Channel(val.channel));
                if (val.channels == StereoChannels) AreEqual(x.TapeBound.Tape, () => x.TapeBound.Tape.Stereo().Channel(val.channel)); });
            
            AssertProp(x => {
                if (val.channels == MonoChannels  ) AreEqual(x.TapeBound.TapeConfig, () => x.TapeBound.TapeConfig.Mono().  Channel(val.channel));
                if (val.channels == StereoChannels) AreEqual(x.TapeBound.TapeConfig, () => x.TapeBound.TapeConfig.Stereo().Channel(val.channel)); });
            
            AssertProp(x => {
                if (val.channels == MonoChannels  ) AreEqual(x.TapeBound.TapeActions, () => x.TapeBound.TapeActions.Mono()  .Channel(val.channel));
                if (val.channels == StereoChannels) AreEqual(x.TapeBound.TapeActions, () => x.TapeBound.TapeActions.Stereo().Channel(val.channel)); });
            
            AssertProp(x => {
                if (val.channels == MonoChannels  ) AreEqual(x.TapeBound.TapeAction, () => x.TapeBound.TapeAction.Mono().Channel(val.channel));
                if (val.channels == StereoChannels) AreEqual(x.TapeBound.TapeAction, () => x.TapeBound.TapeAction.Stereo().Channel(val.channel)); });
