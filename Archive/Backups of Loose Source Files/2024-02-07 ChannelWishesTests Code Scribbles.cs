2024-02-07 ChannelWishesTests:

            AssertProp(x => AreEqual(x.BuffBound.AudioFileOutput, () => x.BuffBound.AudioFileOutput.Channel (val.channel, x.SynthBound.Context)
                                                                                                   .Channels(val.channels, x.SynthBound.Context)
                                                                                                   .Channel (val.channel, x.SynthBound.Context)));
            
            AssertProp(x => AreEqual(x.BuffBound.AudioFileOutput, () => x.BuffBound.AudioFileOutput.Channels(val.channels, x.SynthBound.Context)
                                                                                                   .Channel (val.channel, x.SynthBound.Context)
                                                                                                   .Channels(val.channels, x.SynthBound.Context)));

            AssertProp(x => AreEqual(x.BuffBound.Buff, () => x.BuffBound.Buff.Channel (val.channel, x.SynthBound.Context)
                                                                             .Channels(val.channels, x.SynthBound.Context)
                                                                             .Channel (val.channel, x.SynthBound.Context)));

            AssertProp(x => AreEqual(x.BuffBound.Buff, () => x.BuffBound.Buff.Channels(val.channels, x.SynthBound.Context)
                                                                             .Channel (val.channel, x.SynthBound.Context)
                                                                             .Channels(val.channels, x.SynthBound.Context)));

        
            // TODO: Insistence on matching Channels property with ChannelEntities count,
            // might not be realistic, because yu can change a Buff property, that it passes on to Out,
            // but without re-recording, the channel entities won't change.
            // Why did this even work before with TapeEntities?


        private void Assert_BuffBound_Getters(ConfigTestEntities x, (int channels, int? channel) c)
        {
            // TODO: Handle Mono/Stereo gracefully.
            IsNotNull(() => x);
            IsNotNull(() => x.BuffBound);
            IsNotNull(() => x.BuffBound.Buff);
            IsNotNull(() => x.BuffBound.AudioFileOutput);

            AreEqual(c.channels, () => x.BuffBound.Buff.Channels());
            AreEqual(c.channels, () => x.BuffBound.AudioFileOutput.Channels());
            AreEqual(c.channels == MonoChannels, () => x.BuffBound.Buff.IsMono());
            AreEqual(c.channels == MonoChannels, () => x.BuffBound.AudioFileOutput.IsMono());
            AreEqual(c.channels == StereoChannels, () => x.BuffBound.Buff.IsStereo());
            AreEqual(c.channels == StereoChannels, () => x.BuffBound.AudioFileOutput.IsStereo());
            if (c.channels == MonoChannels)
            { 
                // TODO: More getters!
                AreEqual(CenterChannel, () => x.BuffBound.Buff.Channel());
                AreEqual(CenterChannel, () => x.BuffBound.AudioFileOutput.Channel());
                
                IsTrue(() => x.BuffBound.Buff.IsCenter());
                IsTrue(() => x.BuffBound.AudioFileOutput.IsCenter());
            }
            
            if (c.channels == StereoChannels)
            {
                // TODO: More getters!

                //AreEqual(c.channel, () => x.BuffBound.Buff.Channel());
                //AreEqual(c.channel, () => x.BuffBound.AudioFileOutput.Channel());
                
                //AreEqual(ChannelEmpty, () => x.BuffBound.Buff.Channel());
                //AreEqual(ChannelEmpty, () => x.BuffBound.AudioFileOutput.Channel());


                // TODO: Buffs per tape etc.
            
                //AreEqual(c == (2,0), () => x.BuffBound.Buff.IsLeft());
                //AreEqual(c == (2,0), () => x.BuffBound.AudioFileOutput.IsLeft());
                
                //AreEqual(c == (2,1), () => x.BuffBound.Buff.IsRight());
                //AreEqual(c == (2,1), () => x.BuffBound.AudioFileOutput.IsRight());
            }
        }


- [ ] AudioFileOutput vs ConfigWishes:
    - [ ] Discrepancy between SpeakerSetup and Channel representation:
    - [ ] AudioFileOutput calculation kicked off with SpeakerSetup = Mono, Channel[0].Index = 1
    - [ ] This forces the back-end to compute a Stereo Right-Only signal.
    - [ ] ConfigNightmares tries to resolve to SpeakerSetup = Stereo Channel[0].Index = 1 instead.
    - [ ] Which makes sense, but that's not what the back-end can work with.
    - [ ] The back-end seems to win and the ChannelWishesTest must bow to these ambiguous representations.
    - [ ] Revisit later how these two "world-views" can be consolidated.
- [ ] Test for ambiguous representation:
    - [ ] ChannelWishesTests have to assert for Mono and Stereo Left-Only to be indistinguishable.
    - [ ] May correct later if logic can be made more sound.
- [ ] Update ConfigLog to correctly display AudioFileOutput’s internal state,
      ensuring Left-Only and Right-Only cases are visible, but also Mono-Right-Channel states (hacked in for the back-end's sake)?



        // TODO: Consider using this longer format:
        // Draft of longer version but perhaps better readable?
        //public static bool IsMono(AudioFileOutput obj)
        //{
        //    if (obj.GetChannels() == MonoChannels)
        //    {
        //        // Stereo can be Right. Right not Mono.
        //        return obj.GetChannel() != RightChannel;
        //    }

        //    return false;
        //}

        //public static bool IsStereo(AudioFileOutput obj)
        //{
        //    if (obj.GetChannels() == StereoChannels)
        //    {
        //        return true; // Standard stereo case.
        //    }

        //    // Stereo can be Right. Right not Mono.
        //    if (obj.GetChannel() == RightChannel)
        //    {
        //        return true;
        //    }

        //    return false;
        //}


        //public static Buff SetChannel      (Buff obj, int? value, IContext context)
        //{
        //    if (obj == null) throw new NullException(() => obj);
            
        //    if (obj.UnderlyingAudioFileOutput == null && value == null)
        //    {
        //        // Both null: it's ok to set to null.
        //        return obj;
        //    }
            
        //    // Otherwise, let method throw error upon null UnderlyingAudioFileOutput.
        //    obj.UnderlyingAudioFileOutput.Channel(value, context);
            
        //    return obj;
        //}

        //public static bool IsLeft(Buff            obj) => obj.GetChannel() == LeftChannel ;//&& IsStereo(obj); // No Stereo info: Mono & Left are the same.
        //public static bool IsMono  (Buff            obj) => obj.GetChannels() == MonoChannels   && obj.GetChannel() != RightChannel;
        //public static bool IsStereo(Buff            obj) => obj.GetChannels() == StereoChannels || obj.GetChannel() == RightChannel;

        //public static bool IsRight       (AudioFileOutput obj) => GetChannel(obj) == RightChannel ;//&& IsStereo(obj);
        public static bool IsLeft        (AudioFileOutput obj) => GetChannel(obj) == LeftChannel  ;//&& IsStereo(obj); // No Stereo info: Mono & Left are the same.
        public static bool IsCenter      (AudioFileOutput obj) => GetChannel(obj) == CenterChannel;//&& IsMono  (obj);
        public static bool IsChannelEmpty(AudioFileOutput obj) => GetChannel(obj) == ChannelEmpty ;//&& IsStereo(obj);
        public static AudioFileOutput SetRight        (AudioFileOutput obj, IContext context) => SetChannel(obj, RightChannel , context);//.Stereo(context);
        public static AudioFileOutput SetCenter       (AudioFileOutput obj, IContext context) => SetChannel(obj, CenterChannel, context).Mono  (context);
        public static AudioFileOutput SetChannelEmpty (AudioFileOutput obj, IContext context) => SetChannel(obj, ChannelEmpty , context).Stereo(context);
        // Stereo can be Right. Right can't be Mono.
        public static bool IsStereo(AudioFileOutput obj) => GetChannels(obj) == StereoChannels || obj.GetChannel() == RightChannel;
        public static bool IsMono  (AudioFileOutput obj) => GetChannels(obj) == MonoChannels   && obj.GetChannel() != RightChannel;
        // Non-Nightmare version (does not work)
        //public static bool IsMono  (AudioFileOutput obj) => GetChannels(obj) == MonoChannels;
        //public static bool IsStereo(AudioFileOutput obj) => GetChannels(obj) == StereoChannels;
        //public static bool IsMono  (Buff            obj) => GetChannels(obj) == MonoChannels;
        //public static bool IsStereo(Buff            obj) => GetChannels(obj) == StereoChannels;

        /// <inheritdoc cref="docs._channeltoaudiofileoutput" />
        public static AudioFileOutput SetChannel(AudioFileOutput obj, int? channel, IContext context)
        {
            AssertEntities(obj);
            
            if (channel == CenterChannel && obj.IsMono())
            {
                obj.Channels(MonoChannels, context);
                CreateOrRemoveChannels(obj, signalCount: 1, context);
                obj.AudioFileOutputChannels[0].Index = CenterChannel;
            }
            else if (channel == LeftChannel && obj.IsStereo())
            {
                //obj.SpeakerSetup = GetSubstituteSpeakerSetup(StereoChannels, context);
                obj.SpeakerSetup = GetSubstituteSpeakerSetup(MonoChannels, context); // Quirk of the back-end: Stereo tapes are registered as Mono with 1 channel.
                CreateOrRemoveChannels(obj, signalCount: 1, context);
                obj.AudioFileOutputChannels[0].Index = LeftChannel;
            }
            else if (channel == RightChannel)
            {
                //obj.SpeakerSetup = GetSubstituteSpeakerSetup(StereoChannels, context);
                obj.SpeakerSetup = GetSubstituteSpeakerSetup(MonoChannels, context); // Quirk of the back-end: Stereo tapes are registered as Mono with 1 channel.
                CreateOrRemoveChannels(obj, signalCount: 1, context);
                obj.AudioFileOutputChannels[0].Index = RightChannel;
            }
            else if (channel == EveryChannel)
            {
                obj.SpeakerSetup = GetSubstituteSpeakerSetup(StereoChannels, context);
                CreateOrRemoveChannels(obj, signalCount: 2, context);
                obj.AudioFileOutputChannels[0].Index = 0;
                obj.AudioFileOutputChannels[0].Index = 1;
            }
            else
            {
                throw new Exception($"Invalid combination of values: {new { AudioFileOutput_Channels = obj.Channels(), channel }}");
            }
            
            return obj;
        }

                default  : throw new Exception($"Unsupported {nameof(channel)} value: '{channel}'.");

        
        /// <inheritdoc cref="docs._channeltoaudiofileoutput" />
        public static int? GetChannel_Old(AudioFileOutput obj)
        {
            AssertEntities(obj);
            
            int channels = obj.Channels();
            int signalCount = obj.AudioFileOutputChannels.Count;
            int? firstChannelNumber = obj.AudioFileOutputChannels.ElementAtOrDefault(0)?.Channel();
            
            if (channels == MonoChannels)
            {
                if (firstChannelNumber != null) 
                {
                    // Handles Right-Channel-Only case
                    return firstChannelNumber;
                }
                
                // Mono has channel 0 only.
                return CenterChannel;
            }

            if (channels == StereoChannels)
            {
                if (signalCount == 2)
                {
                    // Handles stereo with 2 channels defined, so not specific channel can be returned,
                    return null;
                }
                if (signalCount == 1)
                {
                    // By returning index, we handle both "Left-only" and "Right-only" (single channel 1) scenarios.
                    if (firstChannelNumber != null)
                    {
                        return firstChannelNumber;
                    }
                }
            }
            
            throw new Exception(
                "Unsupported combination of values: " + Environment.NewLine +
                $"obj.Channels = {channels}, " + Environment.NewLine +
                $"obj.AudioFileOutputChannels.Count = {signalCount} ({nameof(signalCount)})" + Environment.NewLine +
                $"obj.AudioFileOutputChannels[0].Index = {firstChannelNumber} ({nameof(firstChannelNumber)})");
        }

        //// Channels is based on SpeakerSetupEnum, not number of signals.
        //AreEqual(           2, () => x.BuffBound.Buff           .Channels      ()); // By Design: Mono, Left & Right are the same for Buff & AudioFileOutput.
        //AreEqual(           2, () => x.BuffBound.AudioFileOutput.Channels      ());
        //AreEqual(           2, () => x.BuffBound.Buff           .GetChannels   ());
        //AreEqual(           2, () => x.BuffBound.AudioFileOutput.GetChannels   ());

// IsStereo is always True for Buff-Bounds, because each state can represent stereo / 2 channels / single left or right channels.


            AssertProp(x => AreEqual(x.Buff,            () => x.Buff           .Channel     (val.channel, context)/*.Channels    (val.channels, context)*/));
            AssertProp(x => AreEqual(x.AudioFileOutput, () => x.AudioFileOutput.Channel     (val.channel, context)/*.Channels    (val.channels, context)*/));
            AssertProp(x => AreEqual(x.Buff,            () => x.Buff           .WithChannel (val.channel, context)/*.WithChannels(val.channels, context)*/));
            AssertProp(x => AreEqual(x.AudioFileOutput, () => x.AudioFileOutput.WithChannel (val.channel, context)/*.WithChannels(val.channels, context)*/));
            AssertProp(x => AreEqual(x.Buff,            () => x.Buff           .AsChannel   (val.channel, context)/*.SetChannels (val.channels, context)*/));
            AssertProp(x => AreEqual(x.AudioFileOutput, () => x.AudioFileOutput.AsChannel   (val.channel, context)/*.SetChannels (val.channels, context)*/));
            AssertProp(x => AreEqual(x.Buff,            () => x.Buff           .SetChannel  (val.channel, context)/*.SetChannels (val.channels, context)*/));
            AssertProp(x => AreEqual(x.AudioFileOutput, () => x.AudioFileOutput.SetChannel  (val.channel, context)/*.SetChannels (val.channels, context)*/));
            AssertProp(x => AreEqual(x.Buff,            () => Channel    (x.Buff,            val.channel, context)/*.Channels    (val.channels, context)*/));
            AssertProp(x => AreEqual(x.AudioFileOutput, () => Channel    (x.AudioFileOutput, val.channel, context)/*.Channels    (val.channels, context)*/));
            AssertProp(x => AreEqual(x.Buff,            () => WithChannel(x.Buff,            val.channel, context)/*.WithChannels(val.channels, context)*/));
            AssertProp(x => AreEqual(x.AudioFileOutput, () => WithChannel(x.AudioFileOutput, val.channel, context)/*.WithChannels(val.channels, context)*/));
            AssertProp(x => AreEqual(x.Buff,            () => AsChannel  (x.Buff,            val.channel, context)/*.SetChannels (val.channels, context)*/));
            AssertProp(x => AreEqual(x.AudioFileOutput, () => AsChannel  (x.AudioFileOutput, val.channel, context)/*.SetChannels (val.channels, context)*/));
            AssertProp(x => AreEqual(x.Buff,            () => SetChannel (x.Buff,            val.channel, context)/*.SetChannels (val.channels, context)*/));
            AssertProp(x => AreEqual(x.AudioFileOutput, () => SetChannel (x.AudioFileOutput, val.channel, context)/*.SetChannels (val.channels, context)*/));
            AssertProp(x => AreEqual(x.Buff,            () => ConfigWishes.Channel    (x.Buff,            val.channel, context)/*.Channels    (val.channels, context)*/));
            AssertProp(x => AreEqual(x.AudioFileOutput, () => ConfigWishes.Channel    (x.AudioFileOutput, val.channel, context)/*.Channels    (val.channels, context)*/));
            AssertProp(x => AreEqual(x.Buff,            () => ConfigWishes.WithChannel(x.Buff,            val.channel, context)/*.WithChannels(val.channels, context)*/));
            AssertProp(x => AreEqual(x.AudioFileOutput, () => ConfigWishes.WithChannel(x.AudioFileOutput, val.channel, context)/*.WithChannels(val.channels, context)*/));
            AssertProp(x => AreEqual(x.Buff,            () => ConfigWishes.AsChannel  (x.Buff,            val.channel, context)/*.SetChannels (val.channels, context)*/));
            AssertProp(x => AreEqual(x.AudioFileOutput, () => ConfigWishes.AsChannel  (x.AudioFileOutput, val.channel, context)/*.SetChannels (val.channels, context)*/));
            AssertProp(x => AreEqual(x.Buff,            () => ConfigWishes.SetChannel (x.Buff,            val.channel, context)/*.SetChannels (val.channels, context)*/));
            AssertProp(x => AreEqual(x.AudioFileOutput, () => ConfigWishes.SetChannel (x.AudioFileOutput, val.channel, context)/*.SetChannels (val.channels, context)*/));
            AssertProp(x => AreEqual(x.Buff,            () => x.Buff           /*.Channels    (val.channels, context)*/.Channel    (val.channel, context))); // Switched Channel and ChannelS calls
            AssertProp(x => AreEqual(x.AudioFileOutput, () => x.AudioFileOutput/*.Channels    (val.channels, context)*/.Channel    (val.channel, context)));
            AssertProp(x => AreEqual(x.Buff,            () => x.Buff           /*.WithChannels(val.channels, context)*/.WithChannel(val.channel, context)));
            AssertProp(x => AreEqual(x.AudioFileOutput, () => x.AudioFileOutput/*.WithChannels(val.channels, context)*/.WithChannel(val.channel, context)));
            AssertProp(x => AreEqual(x.Buff,            () => x.Buff           /*.SetChannels (val.channels, context)*/.AsChannel  (val.channel, context)));
            AssertProp(x => AreEqual(x.AudioFileOutput, () => x.AudioFileOutput/*.SetChannels (val.channels, context)*/.AsChannel  (val.channel, context)));
            AssertProp(x => AreEqual(x.Buff,            () => x.Buff           /*.SetChannels (val.channels, context)*/.SetChannel (val.channel, context)));
            AssertProp(x => AreEqual(x.AudioFileOutput, () => x.AudioFileOutput/*.SetChannels (val.channels, context)*/.SetChannel (val.channel, context)));

        
        /// <inheritdoc cref="docs._singletapeassertion" />
        private void Assert_BuffBound_Getters_Single_NightmareVersion(ConfigTestEntities x, (int channels, int? channel) c)
        {
            IsNotNull(() => x);
            IsNotNull(() => x.BuffBound.Buff);
            IsNotNull(() => x.BuffBound.AudioFileOutput);
            AreEqual(c.channel,       () => x.BuffBound.Buff           .Channel       ());
            AreEqual(c.channel,       () => x.BuffBound.AudioFileOutput.Channel       ());
            AreEqual(c.channels,      () => x.BuffBound.Buff           .Channels      ());
            AreEqual(c.channels,      () => x.BuffBound.AudioFileOutput.Channels      ());
            AreEqual(c.channel,       () => x.BuffBound.Buff           .GetChannel    ());
            AreEqual(c.channel,       () => x.BuffBound.AudioFileOutput.GetChannel    ());
            AreEqual(c.channels,      () => x.BuffBound.Buff           .GetChannels   ());
            AreEqual(c.channels,      () => x.BuffBound.AudioFileOutput.GetChannels   ());
            AreEqual(c.channels == 1, () => x.BuffBound.Buff           .IsMono        ());
            AreEqual(c.channels == 1, () => x.BuffBound.AudioFileOutput.IsMono        ());
            AreEqual(c.channels == 2, () => x.BuffBound.Buff           .IsStereo      ());
            AreEqual(c.channels == 2, () => x.BuffBound.AudioFileOutput.IsStereo      ());
            AreEqual(c.channel,       () => Channel       (x.BuffBound.Buff            ));
            AreEqual(c.channel,       () => Channel       (x.BuffBound.AudioFileOutput ));
            AreEqual(c.channels,      () => Channels      (x.BuffBound.Buff            ));
            AreEqual(c.channels,      () => Channels      (x.BuffBound.AudioFileOutput ));
            AreEqual(c.channel,       () => GetChannel    (x.BuffBound.Buff            ));
            AreEqual(c.channel,       () => GetChannel    (x.BuffBound.AudioFileOutput ));
            AreEqual(c.channels,      () => GetChannels   (x.BuffBound.Buff            ));
            AreEqual(c.channels,      () => GetChannels   (x.BuffBound.AudioFileOutput ));
            AreEqual(c.channels == 1, () => IsMono        (x.BuffBound.Buff            ));
            AreEqual(c.channels == 1, () => IsMono        (x.BuffBound.AudioFileOutput ));
            AreEqual(c.channels == 2, () => IsStereo      (x.BuffBound.Buff            ));
            AreEqual(c.channels == 2, () => IsStereo      (x.BuffBound.AudioFileOutput ));
            AreEqual(c.channel,       () => ConfigWishes.Channel       (x.BuffBound.Buff           ));
            AreEqual(c.channel,       () => ConfigWishes.Channel       (x.BuffBound.AudioFileOutput));
            AreEqual(c.channels,      () => ConfigWishes.Channels      (x.BuffBound.Buff           ));
            AreEqual(c.channels,      () => ConfigWishes.Channels      (x.BuffBound.AudioFileOutput));
            AreEqual(c.channel,       () => ConfigWishes.GetChannel    (x.BuffBound.Buff           ));
            AreEqual(c.channel,       () => ConfigWishes.GetChannel    (x.BuffBound.AudioFileOutput));
            AreEqual(c.channels,      () => ConfigWishes.GetChannels   (x.BuffBound.Buff           ));
            AreEqual(c.channels,      () => ConfigWishes.GetChannels   (x.BuffBound.AudioFileOutput));
            AreEqual(c.channels == 1, () => ConfigWishes.IsMono        (x.BuffBound.Buff           ));
            AreEqual(c.channels == 1, () => ConfigWishes.IsMono        (x.BuffBound.AudioFileOutput));
            AreEqual(c.channels == 2, () => ConfigWishes.IsStereo      (x.BuffBound.Buff           ));
            AreEqual(c.channels == 2, () => ConfigWishes.IsStereo      (x.BuffBound.AudioFileOutput));

            // AudioFileOutput currently cannot (always) represent stereo-left and mono-center states unambiguously.
            //if (c == (1,0))
            //{
            //    IsTrue (() => x.BuffBound.Buff.IsCenter());
            //    IsTrue (() => x.BuffBound.Buff.IsLeft());
            //    IsFalse(() => x.BuffBound.Buff.IsRight());
            //}
            //else if (c == (2,0))
            //{
            //    IsTrue(() => x.BuffBound.Buff.IsLeft());
            //}
            //else if (c == (2,1))
            //{
            //}
            //else if (c == (2,_))
            //{
            //}
            //else
            //{
            //    throw new Exception("Unsupported combination of values: " + c);
            //}

            AreEqual(c == (1,0),      () => x.BuffBound.Buff           .IsCenter      ());
            AreEqual(c == (1,0),      () => x.BuffBound.AudioFileOutput.IsCenter      ());
            AreEqual(c == (2,0),      () => x.BuffBound.Buff           .IsLeft        ());
            AreEqual(c == (2,0),      () => x.BuffBound.AudioFileOutput.IsLeft        ());
            AreEqual(c == (2,1),      () => x.BuffBound.Buff           .IsRight       ());
            AreEqual(c == (2,1),      () => x.BuffBound.AudioFileOutput.IsRight       ());
            AreEqual(c == (2,_),      () => x.BuffBound.Buff           .IsNoChannel   ());
            AreEqual(c == (2,_),      () => x.BuffBound.AudioFileOutput.IsNoChannel   ());
            AreEqual(c == (2,_),      () => x.BuffBound.Buff           .IsAnyChannel  ());
            AreEqual(c == (2,_),      () => x.BuffBound.AudioFileOutput.IsAnyChannel  ());
            AreEqual(c == (2,_),      () => x.BuffBound.Buff           .IsEveryChannel());
            AreEqual(c == (2,_),      () => x.BuffBound.AudioFileOutput.IsEveryChannel());
            AreEqual(c == (2,_),      () => x.BuffBound.Buff           .IsChannelEmpty());
            AreEqual(c == (2,_),      () => x.BuffBound.AudioFileOutput.IsChannelEmpty());
            AreEqual(c == (1,0),      () => IsCenter      (x.BuffBound.Buff            ));
            AreEqual(c == (1,0),      () => IsCenter      (x.BuffBound.AudioFileOutput ));
            AreEqual(c == (2,0),      () => IsLeft        (x.BuffBound.Buff            ));
            AreEqual(c == (2,0),      () => IsLeft        (x.BuffBound.AudioFileOutput ));
            AreEqual(c == (2,1),      () => IsRight       (x.BuffBound.Buff            ));
            AreEqual(c == (2,1),      () => IsRight       (x.BuffBound.AudioFileOutput ));
            AreEqual(c == (2,_),      () => IsNoChannel   (x.BuffBound.Buff            ));
            AreEqual(c == (2,_),      () => IsNoChannel   (x.BuffBound.AudioFileOutput ));
            AreEqual(c == (2,_),      () => IsAnyChannel  (x.BuffBound.Buff            ));
            AreEqual(c == (2,_),      () => IsAnyChannel  (x.BuffBound.AudioFileOutput ));
            AreEqual(c == (2,_),      () => IsEveryChannel(x.BuffBound.Buff            ));
            AreEqual(c == (2,_),      () => IsEveryChannel(x.BuffBound.AudioFileOutput ));
            AreEqual(c == (2,_),      () => IsChannelEmpty(x.BuffBound.Buff            ));
            AreEqual(c == (2,_),      () => IsChannelEmpty(x.BuffBound.AudioFileOutput ));
            AreEqual(c == (1,0),      () => ConfigWishes.IsCenter      (x.BuffBound.Buff           ));
            AreEqual(c == (1,0),      () => ConfigWishes.IsCenter      (x.BuffBound.AudioFileOutput));
            AreEqual(c == (2,0),      () => ConfigWishes.IsLeft        (x.BuffBound.Buff           ));
            AreEqual(c == (2,0),      () => ConfigWishes.IsLeft        (x.BuffBound.AudioFileOutput));
            AreEqual(c == (2,1),      () => ConfigWishes.IsRight       (x.BuffBound.Buff           ));
            AreEqual(c == (2,1),      () => ConfigWishes.IsRight       (x.BuffBound.AudioFileOutput));
            AreEqual(c == (2,_),      () => ConfigWishes.IsNoChannel   (x.BuffBound.Buff           ));
            AreEqual(c == (2,_),      () => ConfigWishes.IsNoChannel   (x.BuffBound.AudioFileOutput));
            AreEqual(c == (2,_),      () => ConfigWishes.IsAnyChannel  (x.BuffBound.Buff           ));
            AreEqual(c == (2,_),      () => ConfigWishes.IsAnyChannel  (x.BuffBound.AudioFileOutput));
            AreEqual(c == (2,_),      () => ConfigWishes.IsEveryChannel(x.BuffBound.Buff           ));
            AreEqual(c == (2,_),      () => ConfigWishes.IsEveryChannel(x.BuffBound.AudioFileOutput));
            AreEqual(c == (2,_),      () => ConfigWishes.IsChannelEmpty(x.BuffBound.Buff           ));
            AreEqual(c == (2,_),      () => ConfigWishes.IsChannelEmpty(x.BuffBound.AudioFileOutput));
        }

        // By Design: IsStereo always true since every state maps to stereo, 2 channels, or single L/R.


        // By Design: Doesn't set Channels property. Could interfere with Channel state.
        // Same channel number evaluates to the same (ambiguous) state regardless of Mono/Stereo,

            AreEqual (LeftChannel,    () => x.BuffBound.AudioFileOutput.AudioFileOutputChannels[LeftChannel].Index);
            AreEqual (RightChannel,   () => x.BuffBound.AudioFileOutput.AudioFileOutputChannels[RightChannel].Index);
            AreEqual (LeftChannel,    () => x.ChannelEntities[LeftChannel].BuffBound.AudioFileOutput.AudioFileOutputChannels[0].Index);
            AreEqual (RightChannel,   () => x.ChannelEntities[RightChannel].BuffBound.AudioFileOutput.AudioFileOutputChannels[0].Index); // Here's our Right-only Channel.



        //static CaseStruct[] _casesInit =
        //{
        //    // Stereo configurations
        //    new CaseStruct (2,0),
        //    new CaseStruct (2,1),
        //    new CaseStruct (2,_),

        //    // Mono: channel ignored (defaults to CenterChannel)
        //    new CaseStruct ( (1,_), (1,0) ),
        //    new CaseStruct   (1,0),
        //    new CaseStruct ( (1,1), (1,0) ),
            
        //    // All Mono: null / 0 Channels => defaults to Mono => ignores the channel.
        //    new CaseStruct ( (_,_) , (1,0) ),
        //    new CaseStruct ( (0,_) , (1,0) ), 
        //    new CaseStruct ( (_,0) , (1,0) ), 
        //    new CaseStruct ( (0,0) , (1,0) ), 
        //    new CaseStruct ( (_,1) , (1,0) ), 
        //    new CaseStruct ( (0,1) , (1,0) ) 
        //};

        //static object CaseKeysInit        => _casesInit       .Select(x => new object[] { x.Descriptor }).ToArray();

        //[DynamicData(nameof(CaseKeys))]
            //CaseStruct testCase = _caseDictionary[caseKey];
            //var init = testCase.init.coalesce;
            //var val  = testCase.val.coalesce;
        //[DynamicData(nameof(CaseKeys))]
            //CaseStruct testCase = _caseDictionary[caseKey];
            //var init = testCase.init.coalesce;
            //var val  = testCase.val.coalesce;
        static object CaseKeys            => _cases           .Select(x => new object[] { x.Descriptor }).ToArray();


            //public Case((
            //    (int? channels, int? channel)? nully, (int? channels, int? channel) coalesced) from, 
            //    (int? channels, int? channel) to) : base(from, to)
            //{ }

            //public Case(
            //    (int? channels, int? channel) from, 
            //    ((int? channels, int? channel)? nully, (int? channels, int? channel) coalesced) to) : base(from, to)
            //{ }
