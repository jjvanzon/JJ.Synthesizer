using System;
using System.Collections.Generic;
using JJ.Business.Synthesizer.Enums;
using JJ.Business.Synthesizer.Structs;
using JJ.Business.Synthesizer.Wishes.AttributeWishes;
using JJ.Framework.Reflection;
using JJ.Persistence.Synthesizer;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using static JJ.Framework.Testing.AssertHelper;
using static Microsoft.VisualStudio.TestTools.UnitTesting.Assert;

#pragma warning disable CS0618 // Type or member is obsolete

namespace JJ.Business.Synthesizer.Tests.Technical
{
    [TestClass]
    [TestCategory("Technical")]
    public class ChannelWishesTests
    {
        [TestMethod] public void Test_Channel_InTandem()
        {
            Test_Channel_InTandem((1,0), (2,0));
            Test_Channel_InTandem((1,0), (2,1));
            Test_Channel_InTandem((2,0), (1,0));
            Test_Channel_InTandem((2,0), (2,1));
            Test_Channel_InTandem((2,1), (1,0));
            Test_Channel_InTandem((2,1), (2,0));
        }

        void Test_Channel_InTandem((int, int) init, (int channels, int channel) c)
        {
            // Check Before Change
            { 
                var x = CreateTestEntities(init);
                x.Assert_All_Channel_Getters(init);
            }

            // Synth-Bound Changes
            {
                AssertProp(x => AreEqual(x.SynthWishes,        x.SynthWishes.Channels(c.channels).Channel(c.channel)));
                AssertProp(x => AreEqual(x.SynthWishes,        x.SynthWishes.Channel(c.channel)));
                AssertProp(x => AreEqual(x.SynthWishes,  () => x.SynthWishes.Channels(c.channels)));
                AssertProp(x => AreEqual(x.SynthWishes,        x.SynthWishes.WithChannel(c.channel)));
                AssertProp(x => AreEqual(x.SynthWishes,        x.SynthWishes.WithChannels(c.channels)));
                AssertProp(x => AreEqual(x.FlowNode,     () => x.FlowNode.Channel(c.channel)));
                AssertProp(x => AreEqual(x.FlowNode,     () => x.FlowNode.Channels(c.channels)));
                AssertProp(x => AreEqual(x.FlowNode,           x.FlowNode.WithChannel(c.channel)));
                AssertProp(x => AreEqual(x.FlowNode,           x.FlowNode.WithChannels(c.channels)));
                AssertProp(x => AreEqual(x.ConfigWishes, () => x.ConfigWishes.Channel(c.channel)));
                AssertProp(x => AreEqual(x.ConfigWishes, () => x.ConfigWishes.Channels(c.channels)));
                AssertProp(x => AreEqual(x.ConfigWishes,       x.ConfigWishes.WithChannel(c.channel)));
                AssertProp(x => AreEqual(x.ConfigWishes,       x.ConfigWishes.WithChannels(c.channels)));
                
                AssertProp(x => {
                    if (c == (1,0)) AreEqual(x.SynthWishes, () => x.SynthWishes.WithCenter());
                    if (c == (2,0)) AreEqual(x.SynthWishes, () => x.SynthWishes.WithLeft());
                    if (c == (2,1)) AreEqual(x.SynthWishes, () => x.SynthWishes.WithRight()); });
                
                AssertProp(x => {
                    if (c == (1,0)) AreEqual(x.FlowNode, () => x.FlowNode.WithCenter());
                    if (c == (2,0)) AreEqual(x.FlowNode, () => x.FlowNode.WithLeft());
                    if (c == (2,1)) AreEqual(x.FlowNode, () => x.FlowNode.WithRight()); });
                
                AssertProp(x => {
                    if (c == (1,0)) AreEqual(x.ConfigWishes, () => x.ConfigWishes.WithCenter());
                    if (c == (2,0)) AreEqual(x.ConfigWishes, () => x.ConfigWishes.WithLeft());
                    if (c == (2,1)) AreEqual(x.ConfigWishes, () => x.ConfigWishes.WithRight()); });
                
                AssertProp(x => {
                    if (c.channels == 1) AreEqual(x.SynthWishes, () => x.SynthWishes.WithMono());
                    if (c.channels == 2) AreEqual(x.SynthWishes, () => x.SynthWishes.WithStereo()); });
                
                AssertProp(x => {
                    if (c.channels == 1) AreEqual(x.FlowNode, () => x.FlowNode.WithMono());
                    if (c.channels == 2) AreEqual(x.FlowNode, () => x.FlowNode.WithStereo()); });
                
                AssertProp(x => {
                    if (c.channels == 1) AreEqual(x.ConfigWishes, () => x.ConfigWishes.WithMono());
                    if (c.channels == 2) AreEqual(x.ConfigWishes, () => x.ConfigWishes.WithStereo()); });
                
                void AssertProp(Action<TestEntities> setter)
                {
                    var x = CreateTestEntities(init);
                    x.Assert_All_Channel_Getters(init);
                    
                    setter(x);
                    
                    x.Assert_SynthBound_Channel_Getters(c);
                    x.Assert_TapeBound_Channel_Getters(init);
                    x.Assert_BuffBound_Channel_Getters(init);
                    x.Assert_Independent_Channel_Getters(init);
                    x.Assert_Immutable_Channel_Getters(init);
                    
                    x.Record();
                    
                    x.Assert_All_Channel_Getters(c);
                }
            }

            // Tape-Bound Changes
            {
                AssertProp(x => AreEqual(x.Tape,        () => x.Tape.Channel(c.channel)));
                AssertProp(x => AreEqual(x.Tape,        () => x.Tape.Channels(c.channels)));
                AssertProp(x => AreEqual(x.TapeConfig,  () => x.TapeConfig.Channel(c.channel)));
                AssertProp(x => AreEqual(x.TapeConfig,  () => x.TapeConfig.Channels(c.channels)));
                AssertProp(x =>                               x.TapeConfig.Channel = c.channel);
                AssertProp(x =>                               x.TapeConfig.Channels = c.channels);
                AssertProp(x => AreEqual(x.TapeActions, () => x.TapeActions.Channel(c.channel)));
                AssertProp(x => AreEqual(x.TapeActions, () => x.TapeActions.Channels(c.channels)));
                AssertProp(x => AreEqual(x.TapeAction,  () => x.TapeAction.Channel(c.channel)));
                AssertProp(x => AreEqual(x.TapeAction,  () => x.TapeAction.Channels(c.channels)));
                
                AssertProp(x => {
                    if (c == (1,0)) AreEqual(x.Tape, () => x.Tape.Center());
                    if (c == (2,0)) AreEqual(x.Tape, () => x.Tape.Left());
                    if (c == (2,1)) AreEqual(x.Tape, () => x.Tape.Right()); });
                
                AssertProp(x => {
                    if (c == (1,0)) AreEqual(x.TapeConfig, () => x.TapeConfig.Center());
                    if (c == (2,0)) AreEqual(x.TapeConfig, () => x.TapeConfig.Left());
                    if (c == (2,1)) AreEqual(x.TapeConfig, () => x.TapeConfig.Right()); });
                
                AssertProp(x => {
                    if (c == (1,0)) AreEqual(x.TapeActions, () => x.TapeActions.Center());
                    if (c == (2,0)) AreEqual(x.TapeActions, () => x.TapeActions.Left());
                    if (c == (2,1)) AreEqual(x.TapeActions, () => x.TapeActions.Right()); });
                
                AssertProp(x => {
                    if (c == (1,0)) AreEqual(x.TapeAction, () => x.TapeAction.Center());
                    if (c == (2,0)) AreEqual(x.TapeAction, () => x.TapeAction.Left());
                    if (c == (2,1)) AreEqual(x.TapeAction, () => x.TapeAction.Right()); });
                
                AssertProp(x => {
                    if (c.channels == 1) AreEqual(x.Tape, () => x.Tape.Mono());
                    if (c.channels == 2) AreEqual(x.Tape, () => x.Tape.Stereo()); });
                
                AssertProp(x => {
                    if (c.channels == 1) AreEqual(x.TapeConfig, () => x.TapeConfig.Mono());
                    if (c.channels == 2) AreEqual(x.TapeConfig, () => x.TapeConfig.Stereo()); });
                
                AssertProp(x => {
                    if (c.channels == 1) AreEqual(x.TapeActions, () => x.TapeActions.Mono());
                    if (c.channels == 2) AreEqual(x.TapeActions, () => x.TapeActions.Stereo()); });
                
                AssertProp(x => {
                    if (c.channels == 1) AreEqual(x.TapeAction, () => x.TapeAction.Mono());
                    if (c.channels == 2) AreEqual(x.TapeAction, () => x.TapeAction.Stereo()); });

                void AssertProp(Action<TestEntities> setter)
                {
                    var x = CreateTestEntities(init);
                    x.Assert_All_Channel_Getters(init);
                    
                    setter(x);
                    
                    x.Assert_SynthBound_Channel_Getters(init);
                    x.Assert_TapeBound_Channel_Getters(c);
                    x.Assert_BuffBound_Channel_Getters(init);
                    x.Assert_Independent_Channel_Getters(init);
                    x.Assert_Immutable_Channel_Getters(init);
                    
                    x.Record();
                    
                    x.Assert_All_Channel_Getters(init); // By Design: Currently you can't record over the same tape. So you always get a new tape, resetting the values.
                }
            }

            // Buff-Bound Changes
            {
                AssertProp(x => AreEqual(x.Buff,            () => x.Buff.Channel(c.channel)));
                AssertProp(x => AreEqual(x.AudioFileOutput, () => x.AudioFileOutput.Channel(c.channel)));
                
                AssertProp(x => {
                    if (c == (1,0)) AreEqual(x.Buff, () => x.Buff.Center(x.Context));
                    if (c == (2,0)) AreEqual(x.Buff, () => x.Buff.Left(x.Context));
                    if (c == (2,1)) AreEqual(x.Buff, () => x.Buff.Right(x.Context)); });
                
                AssertProp(x => {
                    if (c == (1,0)) AreEqual(x.AudioFileOutput, () => x.AudioFileOutput.Center(x.Context));
                    if (c == (2,0)) AreEqual(x.AudioFileOutput, () => x.AudioFileOutput.Left(x.Context));
                    if (c == (2,1)) AreEqual(x.AudioFileOutput, () => x.AudioFileOutput.Right(x.Context)); });
                                
                AssertProp(x => {
                    if (c.channels == 1) AreEqual(x.Buff, () => x.Buff.Mono(x.Context));
                    if (c.channels == 2) AreEqual(x.Buff, () => x.Buff.Stereo(x.Context)); });
                                
                AssertProp(x => {
                    if (c.channels == 1) AreEqual(x.AudioFileOutput, () => x.AudioFileOutput.Mono(x.Context));
                    if (c.channels == 2) AreEqual(x.AudioFileOutput, () => x.AudioFileOutput.Stereo(x.Context)); });

                void AssertProp(Action<TestEntities> setter)
                {    
                    var x = CreateTestEntities(init);
                    x.Assert_All_Channel_Getters(init);
                    
                    setter(x);

                    x.Assert_SynthBound_Channel_Getters(init);
                    x.Assert_TapeBound_Channel_Getters(init);
                    x.Assert_BuffBound_Channel_Getters(c);
                    x.Assert_Independent_Channel_Getters(init);
                    x.Assert_Immutable_Channel_Getters(init);
                    
                    x.Record();
                    x.Assert_All_Channel_Getters(init);
                }
            }
        }
        
        // Channel for Independently Changeable
        
        [TestMethod] public void Test_Channel_IndependentAfterTaping()
        {
            Test_Channel_IndependentAfterTaping(init: 32, value: 8);
            Test_Channel_IndependentAfterTaping(init: 32, value: 16);
            Test_Channel_IndependentAfterTaping(init: 16, value: 32);
        }
        
        // ReSharper disable UnusedParameter.Local
        void Test_Channel_IndependentAfterTaping(int init, int value)
        {
            // Independent after Taping
            // ... ?
        }
        // ReSharper enable UnusedParameter.Local
        
        // Channel for Immutables

        [TestMethod] public void Test_Channel_Immutable()
        {
            Test_Channel_Immutable(init: (1,0), c: (2,0));
            Test_Channel_Immutable(init: (1,0), c: (2,1));
            Test_Channel_Immutable(init: (2,0), c: (1,0));
            Test_Channel_Immutable(init: (2,0), c: (2,1));
            Test_Channel_Immutable(init: (2,1), c: (1,0));
            Test_Channel_Immutable(init: (2,1), c: (2,0));
        }
        
        void Test_Channel_Immutable((int, int) init, (int channels, int channel) c)
        {
            var x = CreateTestEntities(init);
            
            var channelEnums = new List<ChannelEnum>();
            {
                AssertProp(() => x.ChannelEnum.Channels(c.channels).Channel(c.channel));
                //AssertProp(() => c.channel.ChannelToEnum(c.channels)); // ???

                AssertProp(() => 
                {
                    if (c == (1,0)) return x.ChannelEnum.Center();
                    if (c == (2,0)) return x.ChannelEnum.Left();
                    if (c == (2,1)) return x.ChannelEnum.Right();
                    return default; // ncrunch: no coverage
                });
                
                //AssertProp(() => 
                //{
                //    if (c.channels == 1) return x.ChannelEnum.Mono();
                //    if (c.channels == 2) return x.ChannelEnum.Stereo();
                //    return default; // ncrunch: no coverage
                //});
                
                void AssertProp(Func<ChannelEnum> setter)
                {
                    x.ChannelEnum.Assert_Channel_Getters(init);
                    
                    var channelEnum2 = setter();
                    
                    x.ChannelEnum.Assert_Channel_Getters(init);
                    channelEnum2.Assert_Channel_Getters(c);
                    
                    channelEnums.Add(channelEnum2);
                }
            }
                        
            var channelEntities = new List<Channel>();
            {
                AssertProp(() => x.ChannelEntity.Channels(c.channels, x.Context).Channel(c.channel, x.Context));
                // AssertProp(() => c.channel.ChannelToEntity(c.channels, x.Context)); // ???
                
                AssertProp(() => 
                {
                    if (c == (1,0)) return x.ChannelEntity.Center(x.Context);
                    if (c == (2,0)) return x.ChannelEntity.Left(x.Context);
                    if (c == (2,1)) return x.ChannelEntity.Right(x.Context);
                    return default; // ncrunch: no coverage
                });
                
                void AssertProp(Func<Channel> setter)
                {
                    x.ChannelEntity.Assert_Channel_Getters(init);

                    var channelEntity2 = setter();
                    
                    x.ChannelEntity.Assert_Channel_Getters(init);
                    channelEntity2.Assert_Channel_Getters(c);
                    
                    channelEntities.Add(channelEntity2);
                }
            }
            
            // After-Record
            x.Record();
            
            // All is reset
            x.Assert_All_Channel_Getters(init);
            
            // Except for our variables
            channelEnums.ForEach(e => e.Assert_Channel_Getters(c));
            channelEntities.ForEach(e => e.Assert_Channel_Getters(c));
        }
        
        // Helpers
                
        private TestEntities CreateTestEntities(int channels, int channel)
            => new TestEntities(x => x.WithChannels(channels)
                                      .WithChannel(channel));

        private TestEntities CreateTestEntities((int channels, int channel) values)
            => new TestEntities(x => x.WithChannels(values.channels)
                                      .WithChannel(values.channel));
    }

    
    internal static class ChannelWishesTestExtensions
    {
        public static void Assert_All_Channel_Getters(this TestEntities x, (int, int) values)
        {
            x.Assert_SynthBound_Channel_Getters(values);
            x.Assert_TapeBound_Channel_Getters(values);
            x.Assert_BuffBound_Channel_Getters(values);
            x.Assert_Independent_Channel_Getters(values);
            x.Assert_Immutable_Channel_Getters(values);
        }

        public static void Assert_Bound_Channel_Getters(this TestEntities x, (int, int) values)
        {
            x.Assert_SynthBound_Channel_Getters(values);
            x.Assert_TapeBound_Channel_Getters(values);
            x.Assert_BuffBound_Channel_Getters(values);
        }

        public static void Assert_SynthBound_Channel_Getters(this TestEntities x, (int channels, int channel) c)
        {
            AreEqual(c.channel, () => x.SynthWishes.Channel());
            AreEqual(c.channel, () => x.SynthWishes.GetChannel);
            AreEqual(c.channel, () => x.FlowNode.Channel());
            AreEqual(c.channel, () => x.FlowNode.GetChannel);
            AreEqual(c.channel, () => x.ConfigWishes.Channel());
            AreEqual(c.channel, () => x.ConfigWishes.GetChannel);

            AreEqual(c.channels, () => x.SynthWishes.Channels());
            AreEqual(c.channels, () => x.SynthWishes.GetChannels);
            AreEqual(c.channels, () => x.FlowNode.Channels());
            AreEqual(c.channels, () => x.FlowNode.GetChannels);
            AreEqual(c.channels, () => x.ConfigWishes.Channels());
            AreEqual(c.channels, () => x.ConfigWishes.GetChannels);

            AreEqual(c == (1,0), () => x.SynthWishes.IsCenter());
            AreEqual(c == (1,0), () => x.SynthWishes.IsCenter);
            AreEqual(c == (1,0), () => x.FlowNode.IsCenter());
            AreEqual(c == (1,0), () => x.FlowNode.IsCenter);
            AreEqual(c == (1,0), () => x.ConfigWishes.IsCenter());
            AreEqual(c == (1,0), () => x.ConfigWishes.IsCenter);
            
            AreEqual(c == (2,0), () => x.SynthWishes.IsLeft());
            AreEqual(c == (2,0), () => x.SynthWishes.IsLeft);
            AreEqual(c == (2,0), () => x.FlowNode.IsLeft());
            AreEqual(c == (2,0), () => x.FlowNode.IsLeft);
            AreEqual(c == (2,0), () => x.ConfigWishes.IsLeft());
            AreEqual(c == (2,0), () => x.ConfigWishes.IsLeft);
            
            AreEqual(c == (2,1), () => x.SynthWishes.IsRight());
            AreEqual(c == (2,1), () => x.SynthWishes.IsRight);
            AreEqual(c == (2,1), () => x.FlowNode.IsRight());
            AreEqual(c == (2,1), () => x.FlowNode.IsRight);
            AreEqual(c == (2,1), () => x.ConfigWishes.IsRight());
            AreEqual(c == (2,1), () => x.ConfigWishes.IsRight);
        
            AreEqual(c.channels == 1, () => x.SynthWishes.IsMono());
            AreEqual(c.channels == 1, () => x.SynthWishes.IsMono);
            AreEqual(c.channels == 1, () => x.FlowNode.IsMono());
            AreEqual(c.channels == 1, () => x.FlowNode.IsMono);
            AreEqual(c.channels == 1, () => x.ConfigWishes.IsMono());
            AreEqual(c.channels == 1, () => x.ConfigWishes.IsMono);

            AreEqual(c.channels == 2, () => x.SynthWishes.IsStereo());
            AreEqual(c.channels == 2, () => x.SynthWishes.IsStereo);
            AreEqual(c.channels == 2, () => x.FlowNode.IsStereo());
            AreEqual(c.channels == 2, () => x.FlowNode.IsStereo);
            AreEqual(c.channels == 2, () => x.ConfigWishes.IsStereo());
            AreEqual(c.channels == 2, () => x.ConfigWishes.IsStereo);
        }
        
        public static void Assert_TapeBound_Channel_Getters(this TestEntities x, (int channels, int channel) c)
        {
            if (c.channels == 1)
            {
                // Both Tape and Channel tape are same and have the same channel.
                AreEqual(c.channel, () => x.Tape.Channel());
                AreEqual(c.channel, () => x.TapeConfig.Channel());
                AreEqual(c.channel, () => x.TapeConfig.Channel);
                AreEqual(c.channel, () => x.TapeActions.Channel());
                AreEqual(c.channel, () => x.TapeAction.Channel());

                AreEqual(c.channels, () => x.Tape.Channels());
                AreEqual(c.channels, () => x.TapeConfig.Channels());
                AreEqual(c.channels, () => x.TapeConfig.Channels);
                AreEqual(c.channels, () => x.TapeActions.Channels());
                AreEqual(c.channels, () => x.TapeAction.Channels());
            }           
            if (c.channels == 2)
            {
                // Tape has Channel = null and there are 2 ChannelTapes with Channel = 0 and Channel = 1 respectively.
            }
            
            AreEqual(c == (1,0), () => x.Tape.IsCenter());
            AreEqual(c == (1,0), () => x.TapeConfig.IsCenter());
            AreEqual(c == (1,0), () => x.TapeConfig.IsCenter);
            AreEqual(c == (1,0), () => x.TapeActions.IsCenter());
            AreEqual(c == (1,0), () => x.TapeAction.IsCenter());
        
            AreEqual(c == (2,0), () => x.Tape.IsLeft());
            AreEqual(c == (2,0), () => x.TapeConfig.IsLeft());
            AreEqual(c == (2,0), () => x.TapeConfig.IsLeft);
            AreEqual(c == (2,0), () => x.TapeActions.IsLeft());
            AreEqual(c == (2,0), () => x.TapeAction.IsLeft());
        
            AreEqual(c == (2,1), () => x.Tape.IsRight());
            AreEqual(c == (2,1), () => x.TapeConfig.IsRight());
            AreEqual(c == (2,1), () => x.TapeConfig.IsRight);
            AreEqual(c == (2,1), () => x.TapeActions.IsRight());
            AreEqual(c == (2,1), () => x.TapeAction.IsRight());
            
            AreEqual(c.channels == 1, () => x.Tape.IsMono());
            AreEqual(c.channels == 1, () => x.TapeConfig.IsMono());
            AreEqual(c.channels == 1, () => x.TapeConfig.IsMono);
            AreEqual(c.channels == 1, () => x.TapeActions.IsMono());
            AreEqual(c.channels == 1, () => x.TapeAction.IsMono());
            
            AreEqual(c.channels == 2, () => x.Tape.IsStereo());
            AreEqual(c.channels == 2, () => x.TapeConfig.IsStereo());
            AreEqual(c.channels == 2, () => x.TapeConfig.IsStereo);
            AreEqual(c.channels == 2, () => x.TapeActions.IsStereo());
            AreEqual(c.channels == 2, () => x.TapeAction.IsStereo());
        }
        
        public static void Assert_BuffBound_Channel_Getters(this TestEntities x, (int channels, int channel) c)
        {
            AreEqual(c.channel, () => x.Buff.Channel());
            AreEqual(c.channel, () => x.AudioFileOutput.Channel());
            
            AreEqual(c.channels, () => x.Buff.Channels());
            AreEqual(c.channels, () => x.AudioFileOutput.Channels());
            
            AreEqual(c == (1,0), () => x.Buff.IsCenter());
            AreEqual(c == (1,0), () => x.AudioFileOutput.IsCenter());
            
            AreEqual(c == (2,0), () => x.Buff.IsLeft());
            AreEqual(c == (2,0), () => x.AudioFileOutput.IsLeft());
            
            AreEqual(c == (2,1), () => x.Buff.IsRight());
            AreEqual(c == (2,1), () => x.AudioFileOutput.IsRight());
            
            AreEqual(c.channels == 1, () => x.Buff.IsMono());
            AreEqual(c.channels == 1, () => x.AudioFileOutput.IsMono());
            
            AreEqual(c.channels == 2, () => x.Buff.IsStereo());
            AreEqual(c.channels == 2, () => x.AudioFileOutput.IsStereo());
            
        }
        
        // ReSharper disable UnusedParameter.Global
        public static void Assert_Independent_Channel_Getters(this TestEntities x, (int, int) values)
        {
            // Independent after Taping
            // ... ?
        }
        // ReSharper enable UnusedParameter.Global

        public static void Assert_Immutable_Channel_Getters(this TestEntities x, (int, int) values)
        {
            x.ChannelEnum.Assert_Channel_Getters(values);
            x.ChannelEntity.Assert_Channel_Getters(values);
        }
        
        public static void Assert_Channel_Getters(this ChannelEnum channelEnum, (int channels, int channel) c)
        {
            AreEqual(c.channel,  () => channelEnum.Channel());
            AreEqual(c.channel,  () => channelEnum.EnumToChannel());
            AreEqual(c.channels, () => channelEnum.Channels());
            AreEqual(c.channels, () => channelEnum.ChannelEnumToChannels());
            AreEqual(c == (1,0), () => channelEnum.IsCenter());
            AreEqual(c == (2,0), () => channelEnum.IsLeft());
            AreEqual(c == (2,1), () => channelEnum.IsRight());
            AreEqual(c.channels == 1, () => channelEnum.IsMono());
            AreEqual(c.channels == 2, () => channelEnum.IsStereo());
        }
        
        public static void Assert_Channel_Getters(this Channel channel, (int channels, int channel) c)
        {
            if (channel == null) throw new NullException(() => channel);
            AreEqual(c.channel,  () => channel.Channel());
            AreEqual(c.channel,  () => channel.EntityToChannel());
            AreEqual(c.channels, () => channel.Channels());
            AreEqual(c.channels, () => channel.ChannelEntityToChannels());
            AreEqual(c == (1,0), () => channel.IsCenter());
            AreEqual(c == (2,0), () => channel.IsLeft());
            AreEqual(c == (2,1), () => channel.IsRight());
            AreEqual(c.channels == 1, () => channel.IsMono());
            AreEqual(c.channels == 2, () => channel.IsStereo());
        }
    } 
}