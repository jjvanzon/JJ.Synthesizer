Code Scribbles

    [TestMethod]
    public void Init_SamplingRate(int init)
    { 
        var x = CreateTestEntities(init);
        Assert_All_Getters(x, init);
    }
    
    [DataRow(new string?[]? { "96000", "88200", "48000", "44100", "22050", "11025", "1", "8", "16", "32", "64", "100", "1000", "12345", "1234567" } )]
    static object SamplingRates => new object[] { 96000, 88200, 48000, 44100, 22050, 11025, 1, 8, 16, 32, 64, 100, 1000, 12345, 1234567 };

    static IEnumerable<object[]> TestParametersInit
    {
        get
        {
            foreach (int bits in _bitsValues)
            foreach (int channels in _channelsValues)
            {
                yield return new object[] { bits, channels };
            }
        }
    }

    return new[]
    {
        new object[] { 8, 1, 1 },
        new object[] { 16, 1, 2 },
        new object[] { 32, 1, 4 },
        new object[] { 8, 2, 2 },
        new object[] { 16, 2, 4 },
        new object[] { 32, 2, 8 }
    };

    private Type TypeFromBits(int bits)
    {
        switch (bits)
        {
            case 8:  return typeof(byte);
            case 16: return typeof(short);
            case 32: return typeof(float);
            default: throw new Exception($"{new { bits }} not supported.");
        }
    }

    [TestMethod]
    [DynamicData(nameof(TestParameters2))]
    public void Immutable_CourtesyBytes(
        string descriptor,
        int initCourtesyBytes, int initCourtesyFrames, int initBits, int initChannels,
        int courtesyBytes, int courtesyFrames, int bits, int channels)
    {
        // Wrap parameters
        var init = (courtesyBytes: initCourtesyBytes, courtesyFrames: initCourtesyFrames, 
            bits: initBits, channels: initChannels);
        var val  = (courtesyBytes, courtesyFrames, bits, channels);
        
        // Create init entities
        var x = CreateTestEntities(init);
    
        // Check initial state
        Assert_Immutable_Getters(init.courtesyFrames, x.Immutable.FrameSize, init.courtesyBytes);

        // Quasi-setter
        int courtesyBytes2 = CourtesyBytes(val.courtesyFrames, val.bits / 8 * val.channels);
        
        // Confirm init entities unchanged
        Assert_Immutable_Getters(init.courtesyFrames, x.Immutable.FrameSize, init.courtesyBytes);
        
        // Confirm changed value
        Assert_Immutable_Getters(val.courtesyFrames, x.Immutable.FrameSize, courtesyBytes2);
    }

    if (!Has(value)) AreEqual(x.TapeBound.Tape, () => x.TapeBound.Tape.Bits(value));
    if (!Has(value)) AreEqual(x.TapeBound.TapeConfig, () => x.TapeBound.TapeConfig.Bits(0));
    if (!Has(value)) AreEqual(x.TapeBound.TapeActions, () => x.TapeBound.TapeActions.Bits(default))
    if (!Has(value)) AreEqual(x.TapeBound.TapeAction, () => x.TapeBound.TapeAction.Bits(value));

    
    public static ConfigResolverAccessor Bits(this ConfigResolverAccessor obj, int? value) 
        => new ConfigResolverAccessor(_accessor.InvokeMethod(MemberName(), obj.UnderlyingObject, value));
    
    public static ConfigResolverAccessor Bits(this ConfigResolverAccessor obj, int? value)
    {
        Type[]     paramTypes = { obj?.UnderlyingObj.GetType(), typeof(int?) };
        MethodInfo method     = GetMethod(_underlyingType, MemberName(), paramTypes);
        object     ret        = method.Invoke(obj, new[] { obj.UnderlyingObj, value });
        var        accessor   = new ConfigResolverAccessor(ret);
        return     accessor;
    }

                
    if (value == Undefined) AreEqual(x.SynthBound.SynthWishes,       x.SynthBound.SynthWishes.WithInterpolation(Undefined));
    if (value == 0        ) AreEqual(x.SynthBound.SynthWishes,       x.SynthBound.SynthWishes.WithInterpolation(0)); 
    if (value == null     ) AreEqual(x.SynthBound.SynthWishes,       x.SynthBound.SynthWishes.WithInterpolation(null)); });


    [TestMethod]
    [DynamicData(nameof(TestParametersInitOld))]
    public void Init_CourtesyBytesOld(int init)
    { 
        var x = CreateTestEntities(init);
        Assert_All_Getters(x, init);
    }

        
    [TestMethod]
    [DynamicData(nameof(TestParameters_Old))]
    public void SynthBound_CourtesyBytes_Old(int init, int val)
    {
        void AssertProp(Action<TestEntities> setter)
        {
            var x = CreateTestEntities(init);
            Assert_All_Getters(x, init);
            
            setter(x);
            
            Assert_SynthBound_Getters(x, val);
            Assert_TapeBound_Getters(x, init);
            
            x.Record();
            Assert_All_Getters(x, val);
        }

        AssertProp(x => AreEqual(x.SynthBound.SynthWishes,   x.SynthBound.SynthWishes.CourtesyBytes(val)));
        AssertProp(x => AreEqual(x.SynthBound.FlowNode,      x.SynthBound.FlowNode.CourtesyBytes(val)));
        AssertProp(x => AreEqual(x.SynthBound.ConfigResolver, x.SynthBound.ConfigResolver.CourtesyBytes(val)));
    }

    [TestMethod]
    [DynamicData(nameof(TestParameters_Old))]
    public void TapeBound_CourtesyBytes_Old(int init, int value)
    {
        void AssertProp(Action<TestEntities> setter)
        {
            var x = CreateTestEntities(init);
            Assert_All_Getters(x, init);
            
            setter(x);
            
            Assert_SynthBound_Getters(x, init);
            Assert_TapeBound_Getters(x, value);
            
            x.Record();
            
            Assert_All_Getters(x, init); // By Design: Currently you can't record over the same tape. So you always get a new tape, resetting the values.
        }

        AssertProp(x => AreEqual(x.TapeBound.Tape,        () => x.TapeBound.Tape.CourtesyBytes(value)));
        AssertProp(x => AreEqual(x.TapeBound.TapeConfig,  () => x.TapeBound.TapeConfig.CourtesyBytes(value)));
        AssertProp(x => AreEqual(x.TapeBound.TapeActions, () => x.TapeBound.TapeActions.CourtesyBytes(value)));
        AssertProp(x => AreEqual(x.TapeBound.TapeAction,  () => x.TapeBound.TapeAction.CourtesyBytes(value)));
    }

    static IEnumerable<object[]> TestParametersInitOld => _courtesyBytesValues.Select(value => new object[] { value });
    
    static IEnumerable<object[]> TestParameters_Old
    {
        get
        {
            foreach (int init in _courtesyBytesValues)
            foreach (int value in _courtesyBytesValues)
            {
                yield return new object[] { init, value };
            }
        }
    }
        
    private TestEntities CreateTestEntities(int courtesyBytes) 
        => new TestEntities(x => x.CourtesyBytes(courtesyBytes));

    [Obsolete(ObsoleteMessage)]
    public static SpeakerSetupEnum ChannelsToEnum(this int? channels) => channels?.ChannelsToEnum() ?? default;
    
    [Obsolete(ObsoleteMessage)] 
    public static SpeakerSetupEnum ChannelsToEnum(this int channels)
    {
        if (channels == ChannelsEmpty ) return SpeakerSetupEnum.Undefined;
        if (channels == MonoChannels  ) return SpeakerSetupEnum.Mono;
        if (channels == StereoChannels) return SpeakerSetupEnum.Stereo;
      
        AssertChannels(channels);
      
        return default; // ncrunch: no coverage
    }

    [Obsolete(ObsoleteMessage)]
    public static SpeakerSetupEnum ChannelsToEnum(this int? channels) => ConfigWishes.ChannelsToEnum(channels);

    [Obsolete(ObsoleteMessage)] 
    public static SpeakerSetupEnum ChannelsToEnum(int channels) => ChannelsToEnum((int?)channels);

    new object[] {
        Descriptor   (_courtesyFrames1, 16, MonoChannels,
                        _courtesyFrames2, 32, StereoChannels),
        CourtesyBytes(_courtesyFrames1, 16, MonoChannels), 
                        _courtesyFrames1, 16, MonoChannels,
        CourtesyBytes(_courtesyFrames2, 32, StereoChannels), 
                        _courtesyFrames2, 32, StereoChannels }

    CourtesyBytes(courtesyFrames1, bits1, channels1), 
    CourtesyBytes(courtesyFrames2, bits2, channels2), 


    static string FormatNullable(int? value) => FormatNullable($"{value}");
    static string FormatNullable(string value) => !Has(value) ? "default" : value;



// Attempt at a more complete test matrix for channelEnum, but that idea got parked.
    
    if (channelEnum == ChannelEnum.Undefined)
    {
        if (c == (null, null))
        {
            IsFalse(() => channelEnum.IsCenter());
            IsFalse(() => channelEnum.IsLeft());
            IsFalse(() => channelEnum.IsRight());
            IsFalse(() => channelEnum.IsMono());
            IsFalse(() => channelEnum.IsStereo());
            IsNull(() => channelEnum.Channel());
            IsNull(() => channelEnum.Channels());
        }
        if (c == (null,    0)) ;
        if (c == (null,    1)) ;
        if (c == (   0, null)) ;
        if (c == (   0,    0)) ;
        if (c == (   0,    1)) ;
        if (c == (   1, null)) ;
        if (c == (   1,    0)) ;
        if (c == (   1,    1)) ;
        if (c == (   2, null)) ;
        if (c == (   2,    0)) ;
        if (c == (   2,    1)) ;
    }
    if (channelEnum == ChannelEnum.Single)
    {
        if (c == (null, null)) ;
        if (c == (null,    0)) ;
        if (c == (null,    1)) ;
        if (c == (   0, null)) ;
        if (c == (   0,    0)) ;
        if (c == (   0,    1)) ;
        if (c == (   1, null)) ;
        if (c == (   1,    0)) ;
        if (c == (   1,    1)) ;
        if (c == (   2, null)) ;
        if (c == (   2,    0)) ;
        if (c == (   2,    1)) ;
    }
    if (channelEnum == ChannelEnum.Left)
    {
        if (c == (null, null)) ;
        if (c == (null,    0)) ;
        if (c == (null,    1)) ;
        if (c == (   0, null)) ;
        if (c == (   0,    0)) ;
        if (c == (   0,    1)) ;
        if (c == (   1, null)) ;
        if (c == (   1,    0)) ;
        if (c == (   1,    1)) ;
        if (c == (   2, null)) ;
        if (c == (   2,    0)) ;
        if (c == (   2,    1)) ;
    }
    if (channelEnum == ChannelEnum.Right)
    {
        if (c == (null, null)) ;
        if (c == (null,    0)) ;
        if (c == (null,    1)) ;
        if (c == (   0, null)) ;
        if (c == (   0,    0)) ;
        if (c == (   0,    1)) ;
        if (c == (   1, null)) ;
        if (c == (   1,    0)) ;
        if (c == (   1,    1)) ;
        if (c == (   2, null)) ;
        if (c == (   2,    0)) ;
        if (c == (   2,    1)) ;
    }
        
    
    // Try a do over.
    return;


    switch (c.channel)
    {
        case null:
            IsTrue(() => channelEnum.IsCenter());
            IsFalse(() => channelEnum.IsLeft());
            IsFalse(() => channelEnum.IsRight());
            break;
            
        case 0:
            IsTrue(() => channelEnum.IsCenter());
            IsFalse(() => channelEnum.IsLeft());
            IsFalse(() => channelEnum.IsRight());
            break;
            
        case 1:
            IsTrue(() => channelEnum.IsCenter());
            IsFalse(() => channelEnum.IsLeft());
            IsFalse(() => channelEnum.IsRight());
            break;
    }


    // For Stereo / No Channel you cannot use the channel to derive stereo/mono from.
    if (channelEnum != ChannelEnum.Undefined) 
    {
        AreEqual(c.channels == StereoChannels, () => channelEnum.IsStereo());

        AreEqual(c.channels, () => channelEnum.Channels());
        AreEqual(c.channels, () => channelEnum.ChannelEnumToChannels());
    }

    else if (c.channels == NoChannels)
    {
        IsFalse(() => channelEnum.IsMono  ());
        IsFalse(() => channelEnum.IsCenter());
      
        IsFalse(() => channelEnum.IsStereo());
        IsFalse(() => channelEnum.IsLeft  ());
        IsFalse(() => channelEnum.IsRight ());

        AreEqual(NoChannels, () => channelEnum.Channels());
        AreEqual(NoChannels, () => channelEnum.ChannelEnumToChannels());
      
        AreEqual(ChannelEmpty, () => channelEnum.Channel());
        AreEqual(ChannelEmpty, () => channelEnum.EnumToChannel());
    }
        
    //if (channelEnum == ChannelEnum.Undefined) IsNull(c.channel);

    if (channelEntity == null) IsNull(c.channel);


    var initCoalesced = (initChannels.CoalesceChannels(), initChannel);
    var valCoalesced = (channels.CoalesceChannels(), channel);

    struct TestData
    {
    
        public int? InitChannelsInput { get; set; }
        public int? InitChannelInput  { get; set; }
        public int? ChannelsInput { get; set; }
        public int? ChannelInput  { get; set; }

        public int InitChannelsOutput { get; set; }
        public int? InitChannelOutput { get; set; }
        public int ChannelsOutput { get; set; }
        public int? ChannelCOutput  { get; set; }
    }


    static IList<TestData> testParameters = new []
    {
        new TestData { init = { input = (1, 0), expected = (1, 0) }, val = { input = (2, 0), expected = (2, 0) } },
        new TestData
        {
            init =
            {
                input = (StereoChannels, RightChannel),
                expected = (MonoChannels, CenterChannel)
            },
            val = 
            {
                input = (1,0),
                expected = (2,1)
            }
        },
        new TestData
        {
            init =
            {
                input = (StereoChannels, EveryChannel),
                expected = (MonoChannels, CenterChannel)
            },
            val = 
            {
                input = (1,0),
                expected = (2,null)
            }
        },
        new TestData
        {
            init =
            {
                input = (MonoChannels, AnyChannel),
                expected = (MonoChannels, CenterChannel)
            },
            val = 
            {
                input = (1,0),
                expected = (1,0)
            }
        },
        new TestData
        {
            init = 
            {
                input = (MonoChannels, CenterChannel),
                expected = (MonoChannels, CenterChannel)
            },
            val = 
            {
                input = (1,0),
                expected = (1,0)
            }
        },
        new TestData
        {
            init = 
            {
                input = (MonoChannels, RightChannel),
                expected = (MonoChannels, CenterChannel)
            },
            val = 
            {
                input = (1,0),
                expected = (1,0)
            }
        },
        new TestData
        {
            init = 
            {
                input = (null, null),
                expected = (MonoChannels, CenterChannel)
            },
            val = 
            {
                input = (1,0),
                expected = (1,0)
            }
        },
    };


    struct TestData
    {
        public Init init;
        public Val val;
    }
    
    struct Init
    {
        public (int? channels, int? channel) input;
        public (int? channels, int? channel) expected;
    }

    public struct Val
    {
        public (int channels, int? channel) input;
        public (int channels, int? channel) expected;
    }



    static ( ( (int? channels, int? channel) input, (int channels, int? channel) expect ) init ,
             ( (int? channels, int? channel) input, (int channels, int? channel) expect ) val ) [] TestTuples2 =
    {
        (init: (1,0), val: (2,0)),
        (init: (1,0), val: (2,1)),
        (init: (2,1), val: (_,_), expect: (0,0)),
        (init: (_,_), val: (2,1)),
    };


    static ( ( (int? channels, int? channel) input, (int channels, int? channel) expect ) init ,
                ( (int? channels, int? channel) input, (int channels, int? channel) expect ) val ) [] TestTuples =
    {
        (init: (input: (1,0), expect: (1,0)), val: (input: (2,0), expect: (2,0))),
        (init: (input: (1,0), expect: (1,0)), val: (input: (2,1), expect: (2,1))),
        (init: (input: (2,1), expect: (2,1)), val: (input: (_,_), expect: (0,0))),
        (init: (input: (_,_), expect: (0,0)), val: (input: (2,1), expect: (2,1))),
    };
        
    static ( ( (int? channels, int? channel) input, (int? channels, int? channel) expect ) init ,
                ( (int? channels, int? channel) input, (int? channels, int? channel) expect ) val ) [] TestTuples3 =
    {
        (init: (input: (1,0), expect: (_,_)), val: (input: (2,0), expect: (_,_))),
        (init: (input: (1,0), expect: (_,_)), val: (input: (2,1), expect: (_,_))),
        (init: (input: (2,1), expect: (_,_)), val: (input: (_,_), expect: (1,0))),
        (init: (input: (_,_), expect: (1,0)), val: (input: (2,1), expect: (_,_))),
    };


    private static TestCase CoalesceExpect(TestCase x)
    {
        return new TestCase
        {
            init = (input: x.init.input, expect: (x.init.expect.channels, x.init.expect.channel), 
            val = (x.val.input, x.val.expect) 
        };
    ...


    static object TestParameters => new[]
    {
        new object[] { 1,0, 2,0 },
        new object[] { 1,0, 2,1 },
        new object[] { 1,0, 2,_ },
        
        new object[] { 2,0, 1,0 },
        new object[] { 2,0, 2,1 },
        new object[] { 2,0, 2,_ },
        
        new object[] { 2,1, 1,0 },
        new object[] { 2,1, 2,0 },
        new object[] { 2,1, 2,_ },
        
        new object[] { 2,_, 1,0 },
        new object[] { 2,_, 2,0 },
        new object[] { 2,_, 2,1 },
    };

    static object TestParametersWithEmpties => new[]
    {
        new object[] { 1,0, 2,0 },
        new object[] { 1,0, 2,1 },
        new object[] { 1,0, 2,_ },
        
        new object[] { 2,0, 1,0 },
        new object[] { 2,0, 2,1 },
        new object[] { 2,0, 2,_ },
        
        new object[] { 2,1, 1,0 },
        new object[] { 2,1, 2,0 },
        new object[] { 2,1, 2,_ },
        
        new object[] { 2,_, 1,0 },
        new object[] { 2,_, 2,0 },
        new object[] { 2,_, 2,1 },
        
        // The 2nd pairs should all coalesce to Mono: null / 0 / 1 channels => defaults to Mono => ignores the channel.
        new object[] { 2,1, _,_ },
        new object[] { 2,0, 0,_ },
        new object[] { 2,_, 1,1 },
    };

    static string Descriptor((int? channels, int? channel) c) => $"({c.channels},{c.channel}) => ({c.channels},{c.channel})";

            
    public static implicit operator (int channels, int? channel) (Values values) 
        => (channels: values.channels.coalesce, channel: values.channel.nully);


    throw new Exception(nameof(fromChannelsNully) + " and " + nameof(fromChannelsCoalesced) + " can't both be null.");
    throw new Exception(nameof(toChannelsNully) + " and " + nameof(toChannelsCoalesced) + " can't both be null.");

    /// <summary> Channels / Channel combos. </summary>
    static object TestParametersInit => new[]
    {
        // Stereo configurations
        new object[] { 2, 0    },
        new object[] { 2, 1    },
        new object[] { 2, null },

        // Mono: channel ignored (defaults to CenterChannel)
        new object[] { 1, null },
        new object[] { 1, 0    },
        new object[] { 1, 1    },
        
        // All Mono: null / 0 Channels => defaults to Mono => ignores the channel.
        new object[] { _, _ }, 
        new object[] { 0, _ }, 
        new object[] { _, 0 }, 
        new object[] { 0, 0 }, 
        new object[] { _, 1 }, 
        new object[] { 0, 1 }, 
    };

    static Dictionary<string, Case> _caseDictionaryInit = _casesInit.ToDictionary(x => x.Descriptor);
    static Dictionary<string, Case> _caseDictionaryWithEmpties = _casesWithEmpties.ToDictionary(x => x.Descriptor);

    public int? GetChannel => CoalesceChannelsChannelCombo(_channels, _channel, Has(_section.Channels) ? _section.Channels.Value : DefaultChannels).channel;

    public static (int channels, int? channel) CoalesceChannelsChannelCombo((int? channels, int? channel) tuple)
        => CoalesceChannelsChannelCombo(tuple.channels, tuple.channel);
