// Bits Conversion-Style

[TestMethod] public void Test_Bits_ConversionStyle()
{
    foreach (int bits in new[] { 8, 16, 32 })
    {
        var x = new TestEntities(bits);
        
        // Getters
        AreEqual(x.SampleDataTypeEnum, () => bits.BitsToEnum());
        AreEqual(x.SampleDataType,     () => bits.BitsToEntity(x.Context));
        AreEqual(x.Type,               () => bits.BitsToType());
    
        // Setters
        AreEqual(bits, () => x.SampleDataTypeEnum.EnumToBits());
        AreEqual(bits, () => x.SampleDataType    .EntityToBits());
        AreEqual(bits, () => x.Type              .TypeToBits());
    }
    
    // For test coverage
    ThrowsException(() => default(Type).TypeToBits());
}

    if (c.channels == 1)
    {
        IsNotNull(x.Tape);
        IsNotNull(() => x.ChannelEntities);
        AreEqual(1, () => x.ChannelEntities.Count);
        IsNotNull(() => x.ChannelEntities[0]);
        
        AreSame(x.Tape, () => x.ChannelEntities[0].Tape); 
        
        Assert_TapeBound_Getters_MonoTape(x);
        Assert_TapeBound_Getters_MonoTape(x.ChannelEntities[0]);
    }           


    if (c == (2,1))
    {
        IsFalse(() => channelEntity.IsCenter());
        IsFalse(() => channelEntity.IsLeft());
        IsTrue(() => channelEntity.IsRight());
        IsFalse(() => channelEntity.IsMono());
        IsTrue(() => channelEntity.IsStereo());
    }


/// <inheritdoc cref="docs._quasisetter" />
[Obsolete(ObsoleteMessage)]
public static Channel Channel(this Channel obj, int? channel, IContext context)
{
    int channels = obj.Channels();

    // Simple case: Mono; return single channel.
    if (channels == MonoChannels)
    {
        return ChannelEnum.Single.ToEntity(context);
    }
    
    switch (channel)
    {
        case null:
            return ChannelEnum.Undefined.ToEntity(context);
        case 0:
            if (channels == NoChannels) return ChannelEnum.Single.ToEntity(context);
            if (channels == StereoChannels) return ChannelEnum.Left.ToEntity(context);
            break;
        case 1:
            if (channels == NoChannels) return ChannelEnum.Single.ToEntity(context);
            if (channels == StereoChannels) return ChannelEnum.Right.ToEntity(context);
            break;
    }                    
            
    //ChannelEnum channelEnum = obj.ToEnum();
    
    //return channel.ChannelToEntity(obj.Channels(), context);
}

/// <inheritdoc cref="docs._quasisetter" />
[Obsolete(ObsoleteMessage)]
public static Channel Channel(this Channel thisObj, int? channel, IContext context)
{
    // Unspecified
    if (channel == null) return ChannelEnum.Undefined.ToEntity(context);

    int channels = thisObj.Channels();

    // Mono
    if (channels == MonoChannels)
    {
        return ChannelEnum.Single.ToEntity(context);
    }
    
    // Stereo case
    if (channels == StereoChannels)
    {
        if (channel == 0) return ChannelEnum.Left.ToEntity(context);
        if (channel == 1) return ChannelEnum.Right.ToEntity(context);
    }

    // Fallback for inconsistent state for fluent switch between speaker setups.
    if (channels == NoChannels)
    {
        if (channel == 0) return ChannelEnum.Single.ToEntity(context);
        if (channel == 1) return ChannelEnum.Right.ToEntity(context);
    }
    
    throw new Exception($"Unsupported combination of values: {new{ channels, channel }}");
}

public static Channel Channels(this Channel oldChannelEntity, int newChannelsValue, IContext context)
{
    int? channel = oldChannelEntity.Channel();
    ChannelEnum channelEnum = newChannelsValue.ChannelsToChannelEnum(channel);
    Channel channelEntity = channelEnum.ToEntity(context);
    return channelEntity;
}

public static ChannelEnum Channels(this ChannelEnum oldChannelEnum, int newChannelsValue)
{
    int? oldChannelEntity = oldChannelEnum.Channel();
    return newChannelsValue.ChannelsToChannelEnum(oldChannelEntity);
}

[TestMethod]
[DynamicData(nameof(GetTestParameters), DynamicDataSourceType.Method)]
public void Test_Immutables_Parameterized_WithDynamicData(int initChannels, int? initChannel, int valChannels, int? valChannel)
{
    Test_Immutables((initChannels, initChannel), (valChannels, valChannel));
}

[TestMethod]
[DynamicData(nameof(GetTestParameters), DynamicDataSourceType.Method)]
public void Test_Immutables_Parameterized_WithDynamicData(int initChannels, int? initChannel, int valChannels, int? valChannel)
{
    Test_Immutables((initChannels, initChannel), (valChannels, valChannel));
}

[TestMethod]
[DataRow( 1,0,    2,0    )]
[DataRow( 1,0,    2,1    )]
[DataRow( 1,0,    2,null )]
[DataRow( 2,0,    1,0    )]
[DataRow( 2,0,    2,1    )]
[DataRow( 2,0,    2,null )]
[DataRow( 2,1,    1,0    )]
[DataRow( 2,1,    2,0    )]
[DataRow( 2,1,    2,null )]
[DataRow( 2,null, 1,0    )]
[DataRow( 2,null, 2,0    )]
[DataRow( 2,null, 2,1    )]
public void Test_Immutables_Parameterized_WithDataRow(int initChannels, int? initChannel, int channelsValue, int? channelValue)
{
    Test_Immutables((initChannels, initChannel), (channelsValue, channelValue));
}
        
[TestMethod]
[DynamicData(nameof(GetTestTuples), DynamicDataSourceType.Method)]
public void Test_Immutables_Parameterized_WithTuples((int, int?) init, (int, int?) val)
{
    Test_Immutables(init, val);
}

public static IEnumerable<object[]> GetTestParameters()
{
    return new List<object[]>
    {
        new object[] { 1, 0, 2, 0 },
        new object[] { 1, 0, 2, 1 },
        new object[] { 1, 0, 2, null },
        new object[] { 2, 0, 1, 0 },
        new object[] { 2, 0, 2, 1 },
        new object[] { 2, 0, 2, null },
        new object[] { 2, 1, 1, 0 },
        new object[] { 2, 1, 2, 0 },
        new object[] { 2, 1, 2, null },
        new object[] { 2, null, 1, 0 },
        new object[] { 2, null, 2, 0 },
        new object[] { 2, null, 2, 1 }
    };
}

public static IEnumerable<object[]> GetTestTuples()
{
    return new List<object[]>
    {
        new object[] { (1, 0), (2, 0) },
        new object[] { (1, 0), (2, 1) },
        new object[] { (1, 0), (2, null) },
        new object[] { (2, 0), (1, 0) },
        new object[] { (2, 0), (2, 1) },
        new object[] { (2, 0), (2, null) },
        new object[] { (2, 1), (1, 0) },
        new object[] { (2, 1), (2, 0) },
        new object[] { (2, 1), (2, null) },
        new object[] { (2, null), (1, 0) },
        new object[] { (2, null), (2, 0) },
        new object[] { (2, null), (2, 1) }
    };
}        

public static IEnumerable<object[]> GetTestTuples()
{
    return new List<(int, int?)[]>
    {
        new (int, int?)[] { (1,0   ), (2,0   ) },
        new (int, int?)[] { (1,0   ), (2,1   ) },
        new (int, int?)[] { (1,0   ), (2,null) },
        new (int, int?)[] { (2,0   ), (1,0   ) },
        new (int, int?)[] { (2,0   ), (2,1   ) },
        new (int, int?)[] { (2,0   ), (2,null) },
        new (int, int?)[] { (2,1   ), (1,0   ) },
        new (int, int?)[] { (2,1   ), (2,0   ) },
        new (int, int?)[] { (2,1   ), (2,null) },
        new (int, int?)[] { (2,null), (1,0   ) },
        new (int, int?)[] { (2,null), (2,0   ) },
        new (int, int?)[] { (2,null), (2,1   ) }
    }.Cast<object>().ToArray();
}

public static IEnumerable<object[]> TestParameters => new List<object[]>
{
    new object[] { Init(1,0),    Val(2,0)    },
    new object[] { Init(1,0),    Val(2,1)    },
    new object[] { Init(1,0),    Val(2,null) },
    
    new object[] { Init(2,0),    Val(1,0)    },
    new object[] { Init(2,0),    Val(2,1)    },
    new object[] { Init(2,0),    Val(2,null) },
    
    new object[] { Init(2,1),    Val(1,0)    },
    new object[] { Init(2,1),    Val(2,0)    },
    new object[] { Init(2,1),    Val(2,null) },
    
    new object[] { Init(2,null), Val(1,0)    },
    new object[] { Init(2,null), Val(2,0)    },
    new object[] { Init(2,null), Val(2,1)    }
};

private static (int, int?) Init(int channels, int? channel) => (channels, channel);
private static (int, int?) Val (int channels, int? channel) => (channels, channel);

[TestMethod] public void Test_Channel_BuffBound()
{
    Test_Channel_BuffBound(init: (1,0), val: (2,0));
    Test_Channel_BuffBound(init: (1,0), val: (2,1));
    Test_Channel_BuffBound(init: (1,0), val: (2,null));
                                        
    Test_Channel_BuffBound(init: (2,0), val: (1,0));
    Test_Channel_BuffBound(init: (2,0), val: (2,1));
    Test_Channel_BuffBound(init: (2,0), val: (2,null));
                                        
    Test_Channel_BuffBound(init: (2,1), val: (1,0));
    Test_Channel_BuffBound(init: (2,1), val: (2,0));
    Test_Channel_BuffBound(init: (2,1), val: (2,null));

    Test_Channel_BuffBound(init: (2,null), val: (1,0));
    Test_Channel_BuffBound(init: (2,null), val: (2,0));
    Test_Channel_BuffBound(init: (2,null), val: (2,1));
}

        
[TestMethod] public void Test_Channel_SynthBound()
{
    Test_Channel_SynthBound(init: (1,0), val: (2,0));
    Test_Channel_SynthBound(init: (1,0), val: (2,1));
    Test_Channel_SynthBound(init: (1,0), val: (2,null));
    
    Test_Channel_SynthBound(init: (2,0), val: (1,0));
    Test_Channel_SynthBound(init: (2,0), val: (2,1));
    Test_Channel_SynthBound(init: (2,0), val: (2,null));
    
    Test_Channel_SynthBound(init: (2,1), val: (1,0));
    Test_Channel_SynthBound(init: (2,1), val: (2,0));
    Test_Channel_SynthBound(init: (2,1), val: (2,null));
    
    Test_Channel_SynthBound(init: (2,null), val: (1,0));
    Test_Channel_SynthBound(init: (2,null), val: (2,0));
    Test_Channel_SynthBound(init: (2,null), val: (2,1));
}

[TestMethod] public void Test_Channel_TapeBound()
{
    Test_Channel_TapeBound(init: (1,0), val: (2,0));
    Test_Channel_TapeBound(init: (1,0), val: (2,1));
    Test_Channel_TapeBound(init: (1,0), val: (2,null));
                                        
    Test_Channel_TapeBound(init: (2,0), val: (1,0));
    Test_Channel_TapeBound(init: (2,0), val: (2,1));
    Test_Channel_TapeBound(init: (2,0), val: (2,null));
                                        
    Test_Channel_TapeBound(init: (2,1), val: (1,0));
    Test_Channel_TapeBound(init: (2,1), val: (2,0));
    Test_Channel_TapeBound(init: (2,1), val: (2,null));

    Test_Channel_TapeBound(init: (2,null), val: (1,0));
    Test_Channel_TapeBound(init: (2,null), val: (2,0));
    Test_Channel_TapeBound(init: (2,null), val: (2,1));
}

[TestMethod] public void Test_Immutables()
{
    Test_Immutables(init: (1,0), val: (2,0));
    Test_Immutables(init: (1,0), val: (2,1));
    Test_Immutables(init: (1,0), val: (2,null));
                                        
    Test_Immutables(init: (2,0), val: (1,0));
    Test_Immutables(init: (2,0), val: (2,1));
    Test_Immutables(init: (2,0), val: (2,null));
                
    Test_Immutables(init: (2,1), val: (1,0));
    Test_Immutables(init: (2,1), val: (2,0));
    Test_Immutables(init: (2,1), val: (2,null));

    Test_Immutables(init: (2,null), val: (1,0));
    Test_Immutables(init: (2,null), val: (2,0));
    Test_Immutables(init: (2,null), val: (2,1));
}

        
private static void RestoreChannelIndexes(AudioFileOutput obj)
{
    for (int i = 0; i < obj.AudioFileOutputChannels.Count; i++)
    {
        obj.AudioFileOutputChannels[i].Index = i;
    }
}
