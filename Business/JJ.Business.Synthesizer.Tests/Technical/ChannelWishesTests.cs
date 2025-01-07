using System;
using System.Collections.Generic;
using JJ.Business.Synthesizer.Enums;
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
            Test_Channel_InTandem((1,0), (2,default));
            Test_Channel_InTandem((1,0), (2,0));
            Test_Channel_InTandem((1,0), (2,1));
            
            Test_Channel_InTandem((2,0), (1,0));
            Test_Channel_InTandem((2,0), (2,default));
            Test_Channel_InTandem((2,0), (2,1));
            
            Test_Channel_InTandem((2,1), (1,0));
            Test_Channel_InTandem((2,1), (2,default));
            Test_Channel_InTandem((2,1), (2,0));
        }

        void Test_Channel_InTandem((int, int?) init, (int channels, int? channel) c)
        {
            // Check Before Change
            { 
                var x = CreateTestEntities(init);
                x.Assert_All_Getters(init);
            }

            // Synth-Bound Changes
            {
                AssertProp(x => AreEqual(x.SynthWishes,  x.SynthWishes .Channels    (c.channels).Channel    (c.channel)));
                AssertProp(x => AreEqual(x.SynthWishes,  x.SynthWishes .WithChannels(c.channels).WithChannel(c.channel)));
                AssertProp(x => AreEqual(x.FlowNode,     x.FlowNode    .Channels    (c.channels).Channel    (c.channel)));
                AssertProp(x => AreEqual(x.FlowNode,     x.FlowNode    .WithChannels(c.channels).WithChannel(c.channel)));
                AssertProp(x => AreEqual(x.ConfigWishes, x.ConfigWishes.Channels    (c.channels).Channel    (c.channel)));
                AssertProp(x => AreEqual(x.ConfigWishes, x.ConfigWishes.WithChannels(c.channels).WithChannel(c.channel)));
                
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
                    x.Assert_All_Getters(init);
                    
                    setter(x);
                    
                    x.Assert_SynthBound_Getters(c);
                    x.Assert_TapeBound_Getters(init);
                    x.Assert_BuffBound_Getters(init);
                    x.Assert_Immutable_Getters(init);
                    x.Assert_ChannelTape_Getters(init);
                    
                    x.Record();
                    
                    x.Assert_All_Getters(c);
                    //x.Assert_SynthBound_Getters(c);
                    //x.Assert_TapeBound_Getters(c);
                    //x.Assert_ChannelTapeBound_Getters(c);
                    //x.Assert_BuffBound_Getters(c);
                    //x.Assert_Immutable_Getters(c);
                }
            }

            // Tape-Bound Changes
            {
                AssertProp(x => { x.TapeConfig.Channels = c.channels; x.TapeConfig.Channel = c.channel; });
                AssertProp(x => AreEqual(x.Tape,        x.Tape       .Channels(c.channels).Channel(c.channel)));
                AssertProp(x => AreEqual(x.TapeConfig,  x.TapeConfig .Channels(c.channels).Channel(c.channel)));
                AssertProp(x => AreEqual(x.TapeActions, x.TapeActions.Channels(c.channels).Channel(c.channel)));
                AssertProp(x => AreEqual(x.TapeAction,  x.TapeAction .Channels(c.channels).Channel(c.channel)));
                
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
                    x.Assert_All_Getters(init);
                    
                    setter(x);
                    
                    x.Assert_SynthBound_Getters(init);
                    x.Assert_TapeBound_Getters(c);
                    x.Assert_BuffBound_Getters(init);
                    x.Assert_Immutable_Getters(init);
                    x.Assert_ChannelTape_Getters(init);
                    
                    x.Record();
                    
                    x.Assert_All_Getters(init); // By Design: Currently you can't record over the same tape. So you always get a new tape, resetting the values.
                }
            }

            // Buff-Bound Changes
            {
                AssertProp(x => AreEqual(x.Buff,            () => x.Buff.Channels(c.channels, x.Context).Channel(c.channel)));
                AssertProp(x => AreEqual(x.AudioFileOutput, () => x.AudioFileOutput.Channels(c.channels, x.Context).Channel(c.channel)));
                
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
                    x.Assert_All_Getters(init);
                    
                    setter(x);

                    x.Assert_SynthBound_Getters(init);
                    x.Assert_TapeBound_Getters(init);
                    x.Assert_BuffBound_Getters(c);
                    x.Assert_Immutable_Getters(init);
                    x.Assert_ChannelTape_Getters(init);
                    
                    x.Record();
                    x.Assert_All_Getters(init);
                }
            }
        }
        
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
                    x.ChannelEnum.Assert_Getters(init);
                    
                    var channelEnum2 = setter();
                    
                    x.ChannelEnum.Assert_Getters(init);
                    channelEnum2.Assert_Getters(c);
                    
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
                    x.ChannelEntity.Assert_Getters(init);

                    var channelEntity2 = setter();
                    
                    x.ChannelEntity.Assert_Getters(init);
                    channelEntity2.Assert_Getters(c);
                    
                    channelEntities.Add(channelEntity2);
                }
            }
            
            // After-Record
            x.Record();
            
            // All is reset
            x.Assert_All_Getters(init);
            
            // Except for our variables
            channelEnums.ForEach(e => e.Assert_Getters(c));
            channelEntities.ForEach(e => e.Assert_Getters(c));
        }
        
        // Helpers

        private TestEntities CreateTestEntities((int channels, int? channel) c)
            => new TestEntities(x => x.WithChannels(c.channels)
                                      .WithChannel (c.channel));
    }

    internal static class ChannelWishesTestExtensions
    {
        public static void Assert_All_Getters(this TestEntities x, (int, int?) values)
        {
            x.Assert_SynthBound_Getters(values);
            x.Assert_TapeBound_Getters(values);
            x.Assert_ChannelTape_Getters(values);
            x.Assert_BuffBound_Getters(values);
            x.Assert_Immutable_Getters(values);
        }

        public static void Assert_SynthBound_Getters(this TestEntities x, (int channels, int? channel) c)
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
        
        public static void Assert_TapeBound_Getters(this TestEntities x, (int channels, int? channel) c)
        {
            if (c.channels == 1)
            {
                Assert_MonoTape_Getters(x);
            }           
            if (c.channels == 2)
            {
                Assert_TapeBound_Getters_StereoTape(x);
            }
        }
        
        public static void Assert_ChannelTape_Getters(this TestEntities x, (int channels, int? channel) c)
        {
            IsNotNull(() => x.ChannelEntities);
            AreEqual(c.channels, () => x.ChannelEntities.Count);
            IsFalse(() => x.ChannelEntities.Contains(null));
            
            if (c.channels == 1)
            {
                AreSame(x.Tape, () => x.ChannelEntities[0].Tape); 
                Assert_MonoTape_Getters(x.ChannelEntities[0]);
            }
            if (c.channels == 2)
            {
                Assert_LeftTape_Getters(x.ChannelEntities[0]);
                Assert_RightTape_Getters(x.ChannelEntities[1]);
            }
        }

        public static void Assert_MonoTape_Getters(this TapeEntities x)
        {
            Assert_TapeBound_Getters_Base(x);
            
            AreEqual(1, () => x.Tape.Channels());
            AreEqual(1, () => x.TapeConfig.Channels());
            AreEqual(1, () => x.TapeConfig.Channels);
            AreEqual(1, () => x.TapeActions.Channels());
            AreEqual(1, () => x.TapeAction.Channels());
                                        
            IsTrue(() => x.Tape.IsMono());
            IsTrue(() => x.TapeConfig.IsMono());
            IsTrue(() => x.TapeConfig.IsMono);
            IsTrue(() => x.TapeActions.IsMono());
            IsTrue(() => x.TapeAction.IsMono());
                    
            IsFalse(() => x.Tape.IsStereo());
            IsFalse(() => x.TapeConfig.IsStereo());
            IsFalse(() => x.TapeConfig.IsStereo);
            IsFalse(() => x.TapeActions.IsStereo());
            IsFalse(() => x.TapeAction.IsStereo());

            AreEqual(0, () => x.Tape.Channel());
            AreEqual(0, () => x.TapeConfig.Channel());
            AreEqual(0, () => x.TapeConfig.Channel);
            AreEqual(0, () => x.TapeActions.Channel());
            AreEqual(0, () => x.TapeAction.Channel());
                                                                    
            IsTrue(() => x.Tape.IsCenter());
            IsTrue(() => x.TapeConfig.IsCenter());
            IsTrue(() => x.TapeConfig.IsCenter);
            IsTrue(() => x.TapeActions.IsCenter());
            IsTrue(() => x.TapeAction.IsCenter());
                                                                    
            IsFalse(() => x.Tape.IsLeft());
            IsFalse(() => x.TapeConfig.IsLeft());
            IsFalse(() => x.TapeConfig.IsLeft);
            IsFalse(() => x.TapeActions.IsLeft());
            IsFalse(() => x.TapeAction.IsLeft());
                                                                    
            IsFalse(() => x.Tape.IsRight());
            IsFalse(() => x.TapeConfig.IsRight());
            IsFalse(() => x.TapeConfig.IsRight);
            IsFalse(() => x.TapeActions.IsRight());
            IsFalse(() => x.TapeAction.IsRight());
        }

        public static void Assert_TapeBound_Getters_StereoTape(this TapeEntities x)
        {
            Assert_TapeBound_Getters_Base(x);
            
            AreEqual(2, () => x.Tape.Channels());
            AreEqual(2, () => x.TapeConfig.Channels());
            AreEqual(2, () => x.TapeConfig.Channels);
            AreEqual(2, () => x.TapeActions.Channels());
            AreEqual(2, () => x.TapeAction.Channels());
                                                    
            IsFalse(() => x.Tape.IsMono());
            IsFalse(() => x.TapeConfig.IsMono());
            IsFalse(() => x.TapeConfig.IsMono);
            IsFalse(() => x.TapeActions.IsMono());
            IsFalse(() => x.TapeAction.IsMono());
            
            IsTrue(() => x.Tape.IsStereo());
            IsTrue(() => x.TapeConfig.IsStereo());
            IsTrue(() => x.TapeConfig.IsStereo);
            IsTrue(() => x.TapeActions.IsStereo());
            IsTrue(() => x.TapeAction.IsStereo());

            AreEqual(null, () => x.Tape.Channel());
            AreEqual(null, () => x.TapeConfig.Channel());
            AreEqual(null, () => x.TapeConfig.Channel);
            AreEqual(null, () => x.TapeActions.Channel());
            AreEqual(null, () => x.TapeAction.Channel());

            IsFalse(() => x.Tape.IsCenter());
            IsFalse(() => x.TapeConfig.IsCenter());
            IsFalse(() => x.TapeConfig.IsCenter);
            IsFalse(() => x.TapeActions.IsCenter());
            IsFalse(() => x.TapeAction.IsCenter());
                                                                    
            IsFalse(() => x.Tape.IsLeft());
            IsFalse(() => x.TapeConfig.IsLeft());
            IsFalse(() => x.TapeConfig.IsLeft);
            IsFalse(() => x.TapeActions.IsLeft());
            IsFalse(() => x.TapeAction.IsLeft());
                                                                    
            IsFalse(() => x.Tape.IsRight());
            IsFalse(() => x.TapeConfig.IsRight());
            IsFalse(() => x.TapeConfig.IsRight);
            IsFalse(() => x.TapeActions.IsRight());
            IsFalse(() => x.TapeAction.IsRight());
        }

        public static void Assert_LeftTape_Getters(this TapeEntities x)
        {
            Assert_TapeBound_Getters_Base(x);
            
            AreEqual(2, () => x.Tape.Channels());
            AreEqual(2, () => x.TapeConfig.Channels());
            AreEqual(2, () => x.TapeConfig.Channels);
            AreEqual(2, () => x.TapeActions.Channels());
            AreEqual(2, () => x.TapeAction.Channels());
                                                    
            IsFalse(() => x.Tape.IsMono());
            IsFalse(() => x.TapeConfig.IsMono());
            IsFalse(() => x.TapeConfig.IsMono);
            IsFalse(() => x.TapeActions.IsMono());
            IsFalse(() => x.TapeAction.IsMono());
            
            IsTrue(() => x.Tape.IsStereo());
            IsTrue(() => x.TapeConfig.IsStereo());
            IsTrue(() => x.TapeConfig.IsStereo);
            IsTrue(() => x.TapeActions.IsStereo());
            IsTrue(() => x.TapeAction.IsStereo());

            AreEqual(0, () => x.Tape.Channel());
            AreEqual(0, () => x.TapeConfig.Channel());
            AreEqual(0, () => x.TapeConfig.Channel);
            AreEqual(0, () => x.TapeActions.Channel());
            AreEqual(0, () => x.TapeAction.Channel());

            IsFalse(() => x.Tape.IsCenter());
            IsFalse(() => x.TapeConfig.IsCenter());
            IsFalse(() => x.TapeConfig.IsCenter);
            IsFalse(() => x.TapeActions.IsCenter());
            IsFalse(() => x.TapeAction.IsCenter());
                                                                    
            IsTrue(() => x.Tape.IsLeft());
            IsTrue(() => x.TapeConfig.IsLeft());
            IsTrue(() => x.TapeConfig.IsLeft);
            IsTrue(() => x.TapeActions.IsLeft());
            IsTrue(() => x.TapeAction.IsLeft());
                                                                    
            IsFalse(() => x.Tape.IsRight());
            IsFalse(() => x.TapeConfig.IsRight());
            IsFalse(() => x.TapeConfig.IsRight);
            IsFalse(() => x.TapeActions.IsRight());
            IsFalse(() => x.TapeAction.IsRight());
        }

        public static void Assert_RightTape_Getters(this TapeEntities x)
        {
            Assert_TapeBound_Getters_Base(x);
                    
            AreEqual(2, () => x.Tape.Channels());
            AreEqual(2, () => x.TapeConfig.Channels());
            AreEqual(2, () => x.TapeConfig.Channels);
            AreEqual(2, () => x.TapeActions.Channels());
            AreEqual(2, () => x.TapeAction.Channels());
                                                    
            IsFalse(() => x.Tape.IsMono());
            IsFalse(() => x.TapeConfig.IsMono());
            IsFalse(() => x.TapeConfig.IsMono);
            IsFalse(() => x.TapeActions.IsMono());
            IsFalse(() => x.TapeAction.IsMono());
            
            IsTrue(() => x.Tape.IsStereo());
            IsTrue(() => x.TapeConfig.IsStereo());
            IsTrue(() => x.TapeConfig.IsStereo);
            IsTrue(() => x.TapeActions.IsStereo());
            IsTrue(() => x.TapeAction.IsStereo());

            AreEqual(1, () => x.Tape.Channel());
            AreEqual(1, () => x.TapeConfig.Channel());
            AreEqual(1, () => x.TapeConfig.Channel);
            AreEqual(1, () => x.TapeActions.Channel());
            AreEqual(1, () => x.TapeAction.Channel());

            IsFalse(() => x.Tape.IsCenter());
            IsFalse(() => x.TapeConfig.IsCenter());
            IsFalse(() => x.TapeConfig.IsCenter);
            IsFalse(() => x.TapeActions.IsCenter());
            IsFalse(() => x.TapeAction.IsCenter());
                                                                    
            IsFalse(() => x.Tape.IsLeft());
            IsFalse(() => x.TapeConfig.IsLeft());
            IsFalse(() => x.TapeConfig.IsLeft);
            IsFalse(() => x.TapeActions.IsLeft());
            IsFalse(() => x.TapeAction.IsLeft());
                                                                    
            IsTrue(() => x.Tape.IsRight());
            IsTrue(() => x.TapeConfig.IsRight());
            IsTrue(() => x.TapeConfig.IsRight);
            IsTrue(() => x.TapeActions.IsRight());
            IsTrue(() => x.TapeAction.IsRight());
        }

        public static void Assert_TapeBound_Getters_Base(this TapeEntities x)
        {
            IsNotNull(() => x);
            IsNotNull(() => x.Tape);
            IsNotNull(() => x.TapeConfig);
            IsNotNull(() => x.TapeActions);
            IsNotNull(() => x.TapeAction);
        }

        public static void Assert_BuffBound_Getters(this TestEntities x, (int channels, int? channel) c)
        {
            // TODO: Handle Mono/Stereo gracefully.
            
            if (c.channels == 1)
            { 
                IsTrue(() => x.Buff.IsMono());
                IsTrue(() => x.AudioFileOutput.IsMono());
                
                AreEqual(0, () => x.Buff.Channel());
                AreEqual(0, () => x.AudioFileOutput.Channel());
                
                IsTrue(() => x.Buff.IsCenter());
                IsTrue(() => x.AudioFileOutput.IsCenter());
            }
            
            if (c.channels == 2)
            {
                IsTrue(() => x.Buff.IsStereo());
                IsTrue(() => x.AudioFileOutput.IsStereo());
                
                AreEqual(null, () => x.Buff.Channel());
                AreEqual(null, () => x.AudioFileOutput.Channel());

                AreEqual(2, () => x.Buff.Channels());
                AreEqual(2, () => x.AudioFileOutput.Channels());

                // TODO: Buffs per tape etc.
            
                //AreEqual(c == (2,0), () => x.Buff.IsLeft());
                //AreEqual(c == (2,0), () => x.AudioFileOutput.IsLeft());
                
                //AreEqual(c == (2,1), () => x.Buff.IsRight());
                //AreEqual(c == (2,1), () => x.AudioFileOutput.IsRight());
            }
        }

        public static void Assert_Immutable_Getters(this TestEntities x, (int, int?) c)
        {
            x.ChannelEnum.Assert_Getters(c);
            x.ChannelEntity.Assert_Getters(c);
        }
        
        public static void Assert_Getters(this ChannelEnum channelEnum, (int channels, int? channel) c)
        {
            AreEqual(c.channel,  () => channelEnum.Channel());
            AreEqual(c.channel,  () => channelEnum.EnumToChannel());
            // TODO: Solve Errors
            //AreEqual(c.channels, () => channelEnum.Channels());
            //AreEqual(c.channels, () => channelEnum.ChannelEnumToChannels());
            //AreEqual(c == (1,0), () => channelEnum.IsCenter());
            //AreEqual(c == (2,0), () => channelEnum.IsLeft());
            //AreEqual(c == (2,1), () => channelEnum.IsRight());
            //AreEqual(c.channels == 1, () => channelEnum.IsMono());
            //AreEqual(c.channels == 2, () => channelEnum.IsStereo());
        }
        
        public static void Assert_Getters(this Channel channelEntity, (int channels, int? channel) c)
        {
            if (channelEntity == null) throw new NullException(() => channelEntity);
            AreEqual(c.channel,  () => channelEntity.Channel());
            AreEqual(c.channel,  () => channelEntity.EntityToChannel());
            // TODO: Solve Errors
            //AreEqual(c.channels, () => channelEntity.Channels());
            //AreEqual(c.channels, () => channelEntity.ChannelEntityToChannels());
            //AreEqual(c == (1,0), () => channelEntity.IsCenter());
            //AreEqual(c == (2,0), () => channelEntity.IsLeft());
            AreEqual(c == (2,1), () => channelEntity.IsRight());
            //AreEqual(c.channels == 1, () => channelEntity.IsMono());
            //AreEqual(c.channels == 2, () => channelEntity.IsStereo());
        }
    } 
}