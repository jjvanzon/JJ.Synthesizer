using System;
using System.Collections.Generic;
using System.Linq;
using JJ.Business.Synthesizer.Enums;
using JJ.Business.Synthesizer.Tests.Accessors;
using JJ.Business.Synthesizer.Wishes.Configuration;
using JJ.Persistence.Synthesizer;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using static JJ.Business.Synthesizer.Wishes.Configuration.ConfigWishes;
using static JJ.Framework.Testing.AssertHelper;
using static Microsoft.VisualStudio.TestTools.UnitTesting.Assert;

#pragma warning disable CS0618 
#pragma warning disable MSTEST0018 

namespace JJ.Business.Synthesizer.Tests.Technical.Configuration
{
    [TestClass]
    [TestCategory("Technical")]
    public class ChannelWishesTests
    {
        [TestMethod]
        [DynamicData(nameof(TestParametersInit))]
        public void Init_Channel(int? channels, int? channel)
        {
            var x = CreateTestEntities((channels, channel));
            
            var coalescedChannels = CoalesceChannels(channels);
            Assert_All_Getters(x, (coalescedChannels, channel));
        }
        
        [TestMethod]
        [DynamicData(nameof(TestParameters))]
        public void SynthBound_Channel(int? initChannels, int? initChannel, int? channels, int? channel)
        {
            var init = (initChannels, initChannel);
            var initCoalesced = (initChannels.CoalesceChannels(), initChannel);
            var val = (channels, channel);
            var valCoalesced = (channels.CoalesceChannels(), channel);

            void AssertProp(Action<TestEntities> setter)
            {
                var x = CreateTestEntities(init);
                Assert_All_Getters(x, initCoalesced);
                
                setter(x);
                
                Assert_SynthBound_Getters(x, valCoalesced);
                Assert_TapeBound_Getters_Complete(x, initCoalesced);
                Assert_BuffBound_Getters(x, initCoalesced);
                Assert_Immutable_Getters(x, initCoalesced);
                
                x.Record();
                Assert_All_Getters(x, valCoalesced);
            }
            
            AssertProp(x => AreEqual(x.SynthBound.SynthWishes,    x.SynthBound.SynthWishes   .Channels    (val.channels).Channel    (val.channel)));
            AssertProp(x => AreEqual(x.SynthBound.SynthWishes,    x.SynthBound.SynthWishes   .WithChannels(val.channels).WithChannel(val.channel)));
            AssertProp(x => AreEqual(x.SynthBound.FlowNode,       x.SynthBound.FlowNode      .Channels    (val.channels).Channel    (val.channel)));
            AssertProp(x => AreEqual(x.SynthBound.FlowNode,       x.SynthBound.FlowNode      .WithChannels(val.channels).WithChannel(val.channel)));
            AssertProp(x => AreEqual(x.SynthBound.ConfigResolver, x.SynthBound.ConfigResolver.Channels    (val.channels).Channel    (val.channel)));
            AssertProp(x => AreEqual(x.SynthBound.ConfigResolver, x.SynthBound.ConfigResolver.WithChannels(val.channels).WithChannel(val.channel)));
            
            AssertProp(x => {
                if      (val == (1,0))    AreEqual(x.SynthBound.SynthWishes, () => x.SynthBound.SynthWishes.WithCenter());
                else if (val == (2,0))    AreEqual(x.SynthBound.SynthWishes, () => x.SynthBound.SynthWishes.WithLeft());
                else if (val == (2,1))    AreEqual(x.SynthBound.SynthWishes, () => x.SynthBound.SynthWishes.WithRight());
                else if (val == (2,null)) AreEqual(x.SynthBound.SynthWishes, () => x.SynthBound.SynthWishes.WithStereo().WithChannel(null)); 
                else                      AreEqual(x.SynthBound.SynthWishes, () => x.SynthBound.SynthWishes.WithCenter()); });
            
            AssertProp(x => {
                if (val == (1,0))    AreEqual(x.SynthBound.SynthWishes, () => x.SynthBound.SynthWishes.Center());
                if (val == (2,0))    AreEqual(x.SynthBound.SynthWishes, () => x.SynthBound.SynthWishes.Left());
                if (val == (2,1))    AreEqual(x.SynthBound.SynthWishes, () => x.SynthBound.SynthWishes.Right());
                if (val == (2,null)) AreEqual(x.SynthBound.SynthWishes, () => x.SynthBound.SynthWishes.Stereo().NoChannel()); });
            
            AssertProp(x => {
                if (val == (1,0))    AreEqual(x.SynthBound.FlowNode, () => x.SynthBound.FlowNode.WithCenter());
                if (val == (2,0))    AreEqual(x.SynthBound.FlowNode, () => x.SynthBound.FlowNode.WithLeft());
                if (val == (2,1))    AreEqual(x.SynthBound.FlowNode, () => x.SynthBound.FlowNode.WithRight()); 
                if (val == (2,null)) AreEqual(x.SynthBound.FlowNode, () => x.SynthBound.FlowNode.WithStereo().WithChannel(null)); });
            
            AssertProp(x => {
                if (val == (1,0))    AreEqual(x.SynthBound.FlowNode, () => x.SynthBound.FlowNode.Center());
                if (val == (2,0))    AreEqual(x.SynthBound.FlowNode, () => x.SynthBound.FlowNode.Left());
                if (val == (2,1))    AreEqual(x.SynthBound.FlowNode, () => x.SynthBound.FlowNode.Right()); 
                if (val == (2,null)) AreEqual(x.SynthBound.FlowNode, () => x.SynthBound.FlowNode.Stereo().NoChannel()); });
            
            AssertProp(x => {
                if (val == (1,0))    AreEqual(x.SynthBound.ConfigResolver, () => x.SynthBound.ConfigResolver.WithCenter());
                if (val == (2,0))    AreEqual(x.SynthBound.ConfigResolver, () => x.SynthBound.ConfigResolver.WithLeft());
                if (val == (2,1))    AreEqual(x.SynthBound.ConfigResolver, () => x.SynthBound.ConfigResolver.WithRight()); 
                if (val == (2,null)) AreEqual(x.SynthBound.ConfigResolver, () => x.SynthBound.ConfigResolver.WithStereo().WithChannel(null)); });
            
            AssertProp(x => {
                if (val == (1,0))    AreEqual(x.SynthBound.ConfigResolver, () => x.SynthBound.ConfigResolver.Center());
                if (val == (2,0))    AreEqual(x.SynthBound.ConfigResolver, () => x.SynthBound.ConfigResolver.Left());
                if (val == (2,1))    AreEqual(x.SynthBound.ConfigResolver, () => x.SynthBound.ConfigResolver.Right()); 
                if (val == (2,null)) AreEqual(x.SynthBound.ConfigResolver, () => x.SynthBound.ConfigResolver.Stereo().NoChannel()); });
            
            AssertProp(x => {
                if (val.channels == 1) AreEqual(x.SynthBound.SynthWishes, () => x.SynthBound.SynthWishes.WithMono().WithChannel(val.channel));
                if (val.channels == 2) AreEqual(x.SynthBound.SynthWishes, () => x.SynthBound.SynthWishes.WithStereo().WithChannel(val.channel)); });
            
            AssertProp(x => {
                if (val.channels == 1) AreEqual(x.SynthBound.SynthWishes, () => x.SynthBound.SynthWishes.Mono().Channel(val.channel));
                if (val.channels == 2) AreEqual(x.SynthBound.SynthWishes, () => x.SynthBound.SynthWishes.Stereo().Channel(val.channel)); });
            
            AssertProp(x => {
                if (val.channels == 1) AreEqual(x.SynthBound.FlowNode, () => x.SynthBound.FlowNode.WithMono().WithChannel(val.channel));
                if (val.channels == 2) AreEqual(x.SynthBound.FlowNode, () => x.SynthBound.FlowNode.WithStereo().WithChannel(val.channel)); });
            
            AssertProp(x => {
                if (val.channels == 1) AreEqual(x.SynthBound.FlowNode, () => x.SynthBound.FlowNode.Mono().Channel(val.channel));
                if (val.channels == 2) AreEqual(x.SynthBound.FlowNode, () => x.SynthBound.FlowNode.Stereo().Channel(val.channel)); });
            
            AssertProp(x => {
                if (val.channels == 1) AreEqual(x.SynthBound.ConfigResolver, () => x.SynthBound.ConfigResolver.WithMono().WithChannel(val.channel));
                if (val.channels == 2) AreEqual(x.SynthBound.ConfigResolver, () => x.SynthBound.ConfigResolver.WithStereo().WithChannel(val.channel)); });
            
            AssertProp(x => {
                if (val.channels == 1) AreEqual(x.SynthBound.ConfigResolver, () => x.SynthBound.ConfigResolver.Mono().Channel(val.channel));
                if (val.channels == 2) AreEqual(x.SynthBound.ConfigResolver, () => x.SynthBound.ConfigResolver.Stereo().Channel(val.channel)); });
        }
        
        [TestMethod]
        [DynamicData(nameof(TestParameters))]
        public void TapeBound_Channel(int initChannels, int? initChannel, int channels, int? channel)
        {
            var init = (initChannels, initChannel);
            var val = (channels, channel);

            void AssertProp(Action<TestEntities> setter)
            {
                var x = CreateTestEntities(init);
                Assert_All_Getters(x, init);
                
                setter(x);
                
                Assert_SynthBound_Getters(x, init);
                Assert_TapeBound_Getters_SingleTape(x, val);
                Assert_BuffBound_Getters(x, init);
                Assert_Immutable_Getters(x, init);
                
                x.Record();
                Assert_All_Getters(x, init); // By Design: Currently you can't record over the same tape. So you always get a new tape, resetting the values.
            }
            
            AssertProp(x => AreEqual(x.TapeBound.Tape,        x.TapeBound.Tape       .Channels(val.channels).Channel(val.channel)));
            AssertProp(x => AreEqual(x.TapeBound.Tape,        x.TapeBound.Tape       .Channels(val.channels).Channel(val.channel)));
            AssertProp(x => AreEqual(x.TapeBound.TapeConfig,  x.TapeBound.TapeConfig .Channels(val.channels).Channel(val.channel)));
            AssertProp(x => AreEqual(x.TapeBound.TapeActions, x.TapeBound.TapeActions.Channels(val.channels).Channel(val.channel)));
            AssertProp(x => AreEqual(x.TapeBound.TapeAction,  x.TapeBound.TapeAction .Channels(val.channels).Channel(val.channel)));
            AssertProp(x => { x.TapeBound.TapeConfig.Channels = val.channels; x.TapeBound.TapeConfig.Channel = val.channel; });
            
            AssertProp(x => {
                if (val == (1,0))    AreEqual(x.TapeBound.Tape, () => x.TapeBound.Tape.Center());
                if (val == (2,0))    AreEqual(x.TapeBound.Tape, () => x.TapeBound.Tape.Left());
                if (val == (2,1))    AreEqual(x.TapeBound.Tape, () => x.TapeBound.Tape.Right());
                if (val == (2,null)) AreEqual(x.TapeBound.Tape, () => x.TapeBound.Tape.NoChannel().Stereo()); });
            
            AssertProp(x => {
                if (val == (1,0))    AreEqual(x.TapeBound.TapeConfig, () => x.TapeBound.TapeConfig.Center());
                if (val == (2,0))    AreEqual(x.TapeBound.TapeConfig, () => x.TapeBound.TapeConfig.Left());
                if (val == (2,1))    AreEqual(x.TapeBound.TapeConfig, () => x.TapeBound.TapeConfig.Right());
                if (val == (2,null)) AreEqual(x.TapeBound.TapeConfig, () => x.TapeBound.TapeConfig.Stereo().NoChannel()); });
            
            AssertProp(x => {
                if (val == (1,0))    AreEqual(x.TapeBound.TapeActions, () => x.TapeBound.TapeActions.Center());
                if (val == (2,0))    AreEqual(x.TapeBound.TapeActions, () => x.TapeBound.TapeActions.Left());
                if (val == (2,1))    AreEqual(x.TapeBound.TapeActions, () => x.TapeBound.TapeActions.Right());
                if (val == (2,null)) AreEqual(x.TapeBound.TapeActions, () => x.TapeBound.TapeActions.Stereo().NoChannel()); });
            
            AssertProp(x => {
                if (val == (1,0))    AreEqual(x.TapeBound.TapeAction, () => x.TapeBound.TapeAction.Center());
                if (val == (2,0))    AreEqual(x.TapeBound.TapeAction, () => x.TapeBound.TapeAction.Left());
                if (val == (2,1))    AreEqual(x.TapeBound.TapeAction, () => x.TapeBound.TapeAction.Right());
                if (val == (2,null)) AreEqual(x.TapeBound.TapeAction, () => x.TapeBound.TapeAction.Stereo().NoChannel()); });
            
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
        }
                
        [TestMethod]
        [DynamicData(nameof(TestParameters))]
        public void BuffBound_Channel(int initChannels, int? initChannel, int channels, int? channel)
        {
            var init = (initChannels, initChannel);
            var val = (channels, channel);

            void AssertProp(Action<TestEntities> setter)
            {
                var x = CreateTestEntities(init);
                Assert_All_Getters(x, init);
                
                setter(x);
                
                Assert_SynthBound_Getters(x, init);
                Assert_TapeBound_Getters_Complete(x, init);
                Assert_BuffBound_Getters(x, val);
                Assert_Immutable_Getters(x, init);
                
                x.Record();
                Assert_All_Getters(x, init);
            }
                        
            AssertProp(x => AreEqual(x.BuffBound.AudioFileOutput, () => x.BuffBound.AudioFileOutput.Channel (val.channel, x.SynthBound.Context)
                                                                                                   .Channels(val.channels, x.SynthBound.Context)));

            AssertProp(x => AreEqual(x.BuffBound.AudioFileOutput, () => x.BuffBound.AudioFileOutput.Channels(val.channels, x.SynthBound.Context)
                                                                                                   .Channel (val.channel, x.SynthBound.Context)));

            AssertProp(x => AreEqual(x.BuffBound.AudioFileOutput, () => x.BuffBound.AudioFileOutput.Channel (val.channel, x.SynthBound.Context)
                                                                                                   .Channels(val.channels, x.SynthBound.Context)
                                                                                                   .Channel (val.channel, x.SynthBound.Context)));
            
            AssertProp(x => AreEqual(x.BuffBound.AudioFileOutput, () => x.BuffBound.AudioFileOutput.Channels(val.channels, x.SynthBound.Context)
                                                                                                   .Channel (val.channel, x.SynthBound.Context)
                                                                                                   .Channels(val.channels, x.SynthBound.Context)));
            
            AssertProp(x => AreEqual(x.BuffBound.Buff, () => x.BuffBound.Buff.Channel (val.channel, x.SynthBound.Context)
                                                                             .Channels(val.channels, x.SynthBound.Context)));
                        
            AssertProp(x => AreEqual(x.BuffBound.Buff, () => x.BuffBound.Buff.Channels(val.channels, x.SynthBound.Context)
                                                                             .Channel (val.channel, x.SynthBound.Context)));
                        
            AssertProp(x => AreEqual(x.BuffBound.Buff, () => x.BuffBound.Buff.Channel (val.channel, x.SynthBound.Context)
                                                                             .Channels(val.channels, x.SynthBound.Context)
                                                                             .Channel (val.channel, x.SynthBound.Context)));

            AssertProp(x => AreEqual(x.BuffBound.Buff, () => x.BuffBound.Buff.Channels(val.channels, x.SynthBound.Context)
                                                                             .Channel (val.channel, x.SynthBound.Context)
                                                                             .Channels(val.channels, x.SynthBound.Context)));

            AssertProp(x => {
                if (val == (1,0)) AreEqual(x.BuffBound.Buff, () => x.BuffBound.Buff.Center(x.SynthBound.Context));
                if (val == (2,0)) AreEqual(x.BuffBound.Buff, () => x.BuffBound.Buff.Left(x.SynthBound.Context));
                if (val == (2,1)) AreEqual(x.BuffBound.Buff, () => x.BuffBound.Buff.Right(x.SynthBound.Context)); 
                if (val == (2,null)) AreEqual(x.BuffBound.Buff, () => x.BuffBound.Buff.NoChannel(x.SynthBound.Context)); });
            
            AssertProp(x => {
                if (val == (1,0)) AreEqual(x.BuffBound.AudioFileOutput, () => x.BuffBound.AudioFileOutput.Center(x.SynthBound.Context));
                if (val == (2,0)) AreEqual(x.BuffBound.AudioFileOutput, () => x.BuffBound.AudioFileOutput.Left(x.SynthBound.Context));
                if (val == (2,1)) AreEqual(x.BuffBound.AudioFileOutput, () => x.BuffBound.AudioFileOutput.Right(x.SynthBound.Context));
                if (val == (2,null)) AreEqual(x.BuffBound.AudioFileOutput, () => x.BuffBound.AudioFileOutput.NoChannel(x.SynthBound.Context)); });
        }
        
        [TestMethod]
        [DynamicData(nameof(TestParameters))]
        public void Immutables_Channel(int initChannels, int? initChannel, int channels, int? channel)
        {
            var init = (initChannels, initChannel);
            var val = (channels, channel);
            var x = CreateTestEntities(init);
            
            // ChannelEnum
            
            var channelEnums = new List<ChannelEnum>();
            {
                void AssertProp(Func<ChannelEnum> setter)
                {
                    Assert_Immutable_Getters(x.Immutable.ChannelEnum, init);
                    
                    ChannelEnum channelEnum2 = setter();
                    
                    Assert_Immutable_Getters(x.Immutable.ChannelEnum, init);
                    Assert_Immutable_Getters(channelEnum2, val);
                    
                    channelEnums.Add(channelEnum2);
                }

                AssertProp(() => val.channel.ChannelToEnum(val.channels));
                
                AssertProp(() => x.Immutable.ChannelEnum.Channels(val.channels)
                                                        .Channel(val.channel)
                                                        .Channels(val.channels));
                
                AssertProp(() => x.Immutable.ChannelEnum.Channel(val.channel)
                                                        .Channels(val.channels)
                                                        .Channel(val.channel));

                AssertProp(() => {
                    if (val == (1,0))    return x.Immutable.ChannelEnum.Center();
                    if (val == (2,0))    return x.Immutable.ChannelEnum.Left();
                    if (val == (2,1))    return x.Immutable.ChannelEnum.Right();
                    if (val == (2,null)) return x.Immutable.ChannelEnum.NoChannel();
                    return default; });
            
                // TODO: There must be more.
            }
                        
            // Channel Entity
            
            var channelEntities = new List<Channel>();
            {
                void AssertProp(Func<Channel> setter)
                {
                    Assert_Immutable_Getters(x.Immutable.ChannelEntity, init);

                    Channel channelEntity2 = setter();
                    
                    Assert_Immutable_Getters(x.Immutable.ChannelEntity, init);
                    Assert_Immutable_Getters(channelEntity2, val);
                    
                    channelEntities.Add(channelEntity2);
                }

                AssertProp(() => val.channel.ChannelToEntity(val.channels, x.SynthBound.Context));
                
                AssertProp(() => x.Immutable.ChannelEntity
                                            .Channels(val.channels, x.SynthBound.Context)
                                            .Channel (val.channel,  x.SynthBound.Context)
                                            .Channels(val.channels, x.SynthBound.Context));
                
                AssertProp(() => x.Immutable.ChannelEntity
                                            .Channel(val.channel, x.SynthBound.Context)
                                            .Channels(val.channels, x.SynthBound.Context)
                                            .Channel(val.channel, x.SynthBound.Context));
                AssertProp(() => {
                    if (val == (1,0))    return x.Immutable.ChannelEntity.Center(x.SynthBound.Context);
                    if (val == (2,0))    return x.Immutable.ChannelEntity.Left(x.SynthBound.Context);
                    if (val == (2,1))    return x.Immutable.ChannelEntity.Right(x.SynthBound.Context);
                    if (val == (2,null)) return x.Immutable.ChannelEntity.NoChannel();
                    return default; });

                // TODO: There must be more.
            }
            
            // After-Record
            x.Record();
            
            // All is reset
            Assert_All_Getters(x, init);
            
            // Except for our variables
            channelEnums.ForEach(e => Assert_Immutable_Getters(e, val));
            channelEntities.ForEach(e => Assert_Immutable_Getters(e, val));
        }
        
        // Getter Helpers

        private void Assert_All_Getters(TestEntities x, (int, int?) values)
        {
            Assert_SynthBound_Getters(x, values);
            Assert_TapeBound_Getters_Complete(x, values);
            Assert_BuffBound_Getters(x, values);
            Assert_Immutable_Getters(x, values);
        }

        private void Assert_SynthBound_Getters(TestEntities x, (int channels, int? channel) c)
        {
            AreEqual(c.channel, () => x.SynthBound.SynthWishes.Channel());
            AreEqual(c.channel, () => x.SynthBound.SynthWishes.GetChannel);
            AreEqual(c.channel, () => x.SynthBound.FlowNode.Channel());
            AreEqual(c.channel, () => x.SynthBound.FlowNode.GetChannel);
            AreEqual(c.channel, () => x.SynthBound.ConfigResolver.Channel());
            AreEqual(c.channel, () => x.SynthBound.ConfigResolver.GetChannel);

            AreEqual(c.channels, () => x.SynthBound.SynthWishes.Channels());
            AreEqual(c.channels, () => x.SynthBound.SynthWishes.GetChannels);
            AreEqual(c.channels, () => x.SynthBound.FlowNode.Channels());
            AreEqual(c.channels, () => x.SynthBound.FlowNode.GetChannels);
            AreEqual(c.channels, () => x.SynthBound.ConfigResolver.Channels());
            AreEqual(c.channels, () => x.SynthBound.ConfigResolver.GetChannels);

            AreEqual(c == (1,0), () => x.SynthBound.SynthWishes.IsCenter());
            AreEqual(c == (1,0), () => x.SynthBound.SynthWishes.IsCenter);
            AreEqual(c == (1,0), () => x.SynthBound.FlowNode.IsCenter());
            AreEqual(c == (1,0), () => x.SynthBound.FlowNode.IsCenter);
            AreEqual(c == (1,0), () => x.SynthBound.ConfigResolver.IsCenter());
            AreEqual(c == (1,0), () => x.SynthBound.ConfigResolver.IsCenter);
            
            AreEqual(c == (2,0), () => x.SynthBound.SynthWishes.IsLeft());
            AreEqual(c == (2,0), () => x.SynthBound.SynthWishes.IsLeft);
            AreEqual(c == (2,0), () => x.SynthBound.FlowNode.IsLeft());
            AreEqual(c == (2,0), () => x.SynthBound.FlowNode.IsLeft);
            AreEqual(c == (2,0), () => x.SynthBound.ConfigResolver.IsLeft());
            AreEqual(c == (2,0), () => x.SynthBound.ConfigResolver.IsLeft);
            
            AreEqual(c == (2,1), () => x.SynthBound.SynthWishes.IsRight());
            AreEqual(c == (2,1), () => x.SynthBound.SynthWishes.IsRight);
            AreEqual(c == (2,1), () => x.SynthBound.FlowNode.IsRight());
            AreEqual(c == (2,1), () => x.SynthBound.FlowNode.IsRight);
            AreEqual(c == (2,1), () => x.SynthBound.ConfigResolver.IsRight());
            AreEqual(c == (2,1), () => x.SynthBound.ConfigResolver.IsRight);
        
            AreEqual(c.channels == MonoChannels, () => x.SynthBound.SynthWishes.IsMono());
            AreEqual(c.channels == MonoChannels, () => x.SynthBound.SynthWishes.IsMono);
            AreEqual(c.channels == MonoChannels, () => x.SynthBound.FlowNode.IsMono());
            AreEqual(c.channels == MonoChannels, () => x.SynthBound.FlowNode.IsMono);
            AreEqual(c.channels == MonoChannels, () => x.SynthBound.ConfigResolver.IsMono());
            AreEqual(c.channels == MonoChannels, () => x.SynthBound.ConfigResolver.IsMono);

            AreEqual(c.channels == StereoChannels, () => x.SynthBound.SynthWishes.IsStereo());
            AreEqual(c.channels == StereoChannels, () => x.SynthBound.SynthWishes.IsStereo);
            AreEqual(c.channels == StereoChannels, () => x.SynthBound.FlowNode.IsStereo());
            AreEqual(c.channels == StereoChannels, () => x.SynthBound.FlowNode.IsStereo);
            AreEqual(c.channels == StereoChannels, () => x.SynthBound.ConfigResolver.IsStereo());
            AreEqual(c.channels == StereoChannels, () => x.SynthBound.ConfigResolver.IsStereo);
        }
        
        private void Assert_TapeBound_Getters_SingleTape(TestEntities x, (int channels, int? channel) c)
        {
            Assert_Tape_Getters_Base(x);
                    
            AreEqual(c.channels, () => x.TapeBound.Tape.Channels());
            AreEqual(c.channels, () => x.TapeBound.TapeConfig.Channels());
            AreEqual(c.channels, () => x.TapeBound.TapeConfig.Channels);
            AreEqual(c.channels, () => x.TapeBound.TapeActions.Channels());
            AreEqual(c.channels, () => x.TapeBound.TapeAction.Channels());
            
            AreEqual(c.channels == MonoChannels, () => x.TapeBound.Tape.IsMono());
            AreEqual(c.channels == MonoChannels, () => x.TapeBound.TapeConfig.IsMono());
            AreEqual(c.channels == MonoChannels, () => x.TapeBound.TapeConfig.IsMono);
            AreEqual(c.channels == MonoChannels, () => x.TapeBound.TapeActions.IsMono());
            AreEqual(c.channels == MonoChannels, () => x.TapeBound.TapeAction.IsMono());
            
            AreEqual(c.channels == StereoChannels, () => x.TapeBound.Tape.IsStereo());
            AreEqual(c.channels == StereoChannels, () => x.TapeBound.TapeConfig.IsStereo());
            AreEqual(c.channels == StereoChannels, () => x.TapeBound.TapeConfig.IsStereo);
            AreEqual(c.channels == StereoChannels, () => x.TapeBound.TapeActions.IsStereo());
            AreEqual(c.channels == StereoChannels, () => x.TapeBound.TapeAction.IsStereo());

            AreEqual(c.channel, () => x.TapeBound.Tape.Channel());
            AreEqual(c.channel, () => x.TapeBound.TapeConfig.Channel());
            AreEqual(c.channel, () => x.TapeBound.TapeConfig.Channel);
            AreEqual(c.channel, () => x.TapeBound.TapeActions.Channel());
            AreEqual(c.channel, () => x.TapeBound.TapeAction.Channel());

            AreEqual(c == (1,0), () => x.TapeBound.Tape.IsCenter());
            AreEqual(c == (1,0), () => x.TapeBound.TapeConfig.IsCenter());
            AreEqual(c == (1,0), () => x.TapeBound.TapeConfig.IsCenter);
            AreEqual(c == (1,0), () => x.TapeBound.TapeActions.IsCenter());
            AreEqual(c == (1,0), () => x.TapeBound.TapeAction.IsCenter());
                                                                    
            AreEqual(c == (2,0), () => x.TapeBound.Tape.IsLeft());
            AreEqual(c == (2,0), () => x.TapeBound.TapeConfig.IsLeft());
            AreEqual(c == (2,0), () => x.TapeBound.TapeConfig.IsLeft);
            AreEqual(c == (2,0), () => x.TapeBound.TapeActions.IsLeft());
            AreEqual(c == (2,0), () => x.TapeBound.TapeAction.IsLeft());
                                                                    
            AreEqual(c == (2,1), () => x.TapeBound.Tape.IsRight());
            AreEqual(c == (2,1), () => x.TapeBound.TapeConfig.IsRight());
            AreEqual(c == (2,1), () => x.TapeBound.TapeConfig.IsRight);
            AreEqual(c == (2,1), () => x.TapeBound.TapeActions.IsRight());
            AreEqual(c == (2,1), () => x.TapeBound.TapeAction.IsRight());
        }
        
        private void Assert_TapeBound_Getters_Complete(TestEntities x, (int channels, int? channel) c)
        {
            IsNotNull(() => x.ChannelEntities);
            AreEqual(c.channels, () => x.ChannelEntities.Count);
            IsFalse(() => x.ChannelEntities.Contains(null));
            
            if (c.channels == MonoChannels)
            {
                AreSame(x.TapeBound.Tape, () => x.ChannelEntities[0].TapeBound.Tape); 
                Assert_MonoTape_Getters(x);
                Assert_MonoTape_Getters(x.ChannelEntities[0]);
            }
            if (c.channels == StereoChannels)
            {
                Assert_StereoTape_Getters(x);
                Assert_LeftTape_Getters(x.ChannelEntities[0]);
                Assert_RightTape_Getters(x.ChannelEntities[1]);
            }
        }

        private void Assert_MonoTape_Getters(TapeEntities x)
        {
            Assert_Tape_Getters_Base(x);
            
            AreEqual(MonoChannels, () => x.TapeBound.Tape.Channels());
            AreEqual(MonoChannels, () => x.TapeBound.TapeConfig.Channels());
            AreEqual(MonoChannels, () => x.TapeBound.TapeConfig.Channels);
            AreEqual(MonoChannels, () => x.TapeBound.TapeActions.Channels());
            AreEqual(MonoChannels, () => x.TapeBound.TapeAction.Channels());
                                        
            IsTrue(() => x.TapeBound.Tape.IsMono());
            IsTrue(() => x.TapeBound.TapeConfig.IsMono());
            IsTrue(() => x.TapeBound.TapeConfig.IsMono);
            IsTrue(() => x.TapeBound.TapeActions.IsMono());
            IsTrue(() => x.TapeBound.TapeAction.IsMono());
                    
            IsFalse(() => x.TapeBound.Tape.IsStereo());
            IsFalse(() => x.TapeBound.TapeConfig.IsStereo());
            IsFalse(() => x.TapeBound.TapeConfig.IsStereo);
            IsFalse(() => x.TapeBound.TapeActions.IsStereo());
            IsFalse(() => x.TapeBound.TapeAction.IsStereo());

            AreEqual(CenterChannel, () => x.TapeBound.Tape.Channel());
            AreEqual(CenterChannel, () => x.TapeBound.TapeConfig.Channel());
            AreEqual(CenterChannel, () => x.TapeBound.TapeConfig.Channel);
            AreEqual(CenterChannel, () => x.TapeBound.TapeActions.Channel());
            AreEqual(CenterChannel, () => x.TapeBound.TapeAction.Channel());
                                                                    
            IsTrue(() => x.TapeBound.Tape.IsCenter());
            IsTrue(() => x.TapeBound.TapeConfig.IsCenter());
            IsTrue(() => x.TapeBound.TapeConfig.IsCenter);
            IsTrue(() => x.TapeBound.TapeActions.IsCenter());
            IsTrue(() => x.TapeBound.TapeAction.IsCenter());
                                                                    
            IsFalse(() => x.TapeBound.Tape.IsLeft());
            IsFalse(() => x.TapeBound.TapeConfig.IsLeft());
            IsFalse(() => x.TapeBound.TapeConfig.IsLeft);
            IsFalse(() => x.TapeBound.TapeActions.IsLeft());
            IsFalse(() => x.TapeBound.TapeAction.IsLeft());
                                                                    
            IsFalse(() => x.TapeBound.Tape.IsRight());
            IsFalse(() => x.TapeBound.TapeConfig.IsRight());
            IsFalse(() => x.TapeBound.TapeConfig.IsRight);
            IsFalse(() => x.TapeBound.TapeActions.IsRight());
            IsFalse(() => x.TapeBound.TapeAction.IsRight());
        }

        private void Assert_StereoTape_Getters(TapeEntities x)
        {
            Assert_Tape_Getters_Base(x);
            
            AreEqual(StereoChannels, () => x.TapeBound.Tape.Channels());
            AreEqual(StereoChannels, () => x.TapeBound.TapeConfig.Channels());
            AreEqual(StereoChannels, () => x.TapeBound.TapeConfig.Channels);
            AreEqual(StereoChannels, () => x.TapeBound.TapeActions.Channels());
            AreEqual(StereoChannels, () => x.TapeBound.TapeAction.Channels());
                                                    
            IsFalse(() => x.TapeBound.Tape.IsMono());
            IsFalse(() => x.TapeBound.TapeConfig.IsMono());
            IsFalse(() => x.TapeBound.TapeConfig.IsMono);
            IsFalse(() => x.TapeBound.TapeActions.IsMono());
            IsFalse(() => x.TapeBound.TapeAction.IsMono());
            
            IsTrue(() => x.TapeBound.Tape.IsStereo());
            IsTrue(() => x.TapeBound.TapeConfig.IsStereo());
            IsTrue(() => x.TapeBound.TapeConfig.IsStereo);
            IsTrue(() => x.TapeBound.TapeActions.IsStereo());
            IsTrue(() => x.TapeBound.TapeAction.IsStereo());

            AreEqual(ChannelEmpty, () => x.TapeBound.Tape.Channel());
            AreEqual(ChannelEmpty, () => x.TapeBound.TapeConfig.Channel());
            AreEqual(ChannelEmpty, () => x.TapeBound.TapeConfig.Channel);
            AreEqual(ChannelEmpty, () => x.TapeBound.TapeActions.Channel());
            AreEqual(ChannelEmpty, () => x.TapeBound.TapeAction.Channel());

            IsFalse(() => x.TapeBound.Tape.IsCenter());
            IsFalse(() => x.TapeBound.TapeConfig.IsCenter());
            IsFalse(() => x.TapeBound.TapeConfig.IsCenter);
            IsFalse(() => x.TapeBound.TapeActions.IsCenter());
            IsFalse(() => x.TapeBound.TapeAction.IsCenter());
                                                                    
            IsFalse(() => x.TapeBound.Tape.IsLeft());
            IsFalse(() => x.TapeBound.TapeConfig.IsLeft());
            IsFalse(() => x.TapeBound.TapeConfig.IsLeft);
            IsFalse(() => x.TapeBound.TapeActions.IsLeft());
            IsFalse(() => x.TapeBound.TapeAction.IsLeft());
                                                                    
            IsFalse(() => x.TapeBound.Tape.IsRight());
            IsFalse(() => x.TapeBound.TapeConfig.IsRight());
            IsFalse(() => x.TapeBound.TapeConfig.IsRight);
            IsFalse(() => x.TapeBound.TapeActions.IsRight());
            IsFalse(() => x.TapeBound.TapeAction.IsRight());
        }

        private void Assert_LeftTape_Getters(TapeEntities x)
        {
            Assert_Tape_Getters_Base(x);
            
            AreEqual(StereoChannels, () => x.TapeBound.Tape.Channels());
            AreEqual(StereoChannels, () => x.TapeBound.TapeConfig.Channels());
            AreEqual(StereoChannels, () => x.TapeBound.TapeConfig.Channels);
            AreEqual(StereoChannels, () => x.TapeBound.TapeActions.Channels());
            AreEqual(StereoChannels, () => x.TapeBound.TapeAction.Channels());
                                                    
            IsFalse(() => x.TapeBound.Tape.IsMono());
            IsFalse(() => x.TapeBound.TapeConfig.IsMono());
            IsFalse(() => x.TapeBound.TapeConfig.IsMono);
            IsFalse(() => x.TapeBound.TapeActions.IsMono());
            IsFalse(() => x.TapeBound.TapeAction.IsMono());
            
            IsTrue(() => x.TapeBound.Tape.IsStereo());
            IsTrue(() => x.TapeBound.TapeConfig.IsStereo());
            IsTrue(() => x.TapeBound.TapeConfig.IsStereo);
            IsTrue(() => x.TapeBound.TapeActions.IsStereo());
            IsTrue(() => x.TapeBound.TapeAction.IsStereo());

            AreEqual(LeftChannel, () => x.TapeBound.Tape.Channel());
            AreEqual(LeftChannel, () => x.TapeBound.TapeConfig.Channel());
            AreEqual(LeftChannel, () => x.TapeBound.TapeConfig.Channel);
            AreEqual(LeftChannel, () => x.TapeBound.TapeActions.Channel());
            AreEqual(LeftChannel, () => x.TapeBound.TapeAction.Channel());

            IsFalse(() => x.TapeBound.Tape.IsCenter());
            IsFalse(() => x.TapeBound.TapeConfig.IsCenter());
            IsFalse(() => x.TapeBound.TapeConfig.IsCenter);
            IsFalse(() => x.TapeBound.TapeActions.IsCenter());
            IsFalse(() => x.TapeBound.TapeAction.IsCenter());
                                                                    
            IsTrue(() => x.TapeBound.Tape.IsLeft());
            IsTrue(() => x.TapeBound.TapeConfig.IsLeft());
            IsTrue(() => x.TapeBound.TapeConfig.IsLeft);
            IsTrue(() => x.TapeBound.TapeActions.IsLeft());
            IsTrue(() => x.TapeBound.TapeAction.IsLeft());
                                                                    
            IsFalse(() => x.TapeBound.Tape.IsRight());
            IsFalse(() => x.TapeBound.TapeConfig.IsRight());
            IsFalse(() => x.TapeBound.TapeConfig.IsRight);
            IsFalse(() => x.TapeBound.TapeActions.IsRight());
            IsFalse(() => x.TapeBound.TapeAction.IsRight());
        }

        private void Assert_RightTape_Getters(TapeEntities x)
        {
            Assert_Tape_Getters_Base(x);
                    
            AreEqual(StereoChannels, () => x.TapeBound.Tape.Channels());
            AreEqual(StereoChannels, () => x.TapeBound.TapeConfig.Channels());
            AreEqual(StereoChannels, () => x.TapeBound.TapeConfig.Channels);
            AreEqual(StereoChannels, () => x.TapeBound.TapeActions.Channels());
            AreEqual(StereoChannels, () => x.TapeBound.TapeAction.Channels());
                                                    
            IsFalse(() => x.TapeBound.Tape.IsMono());
            IsFalse(() => x.TapeBound.TapeConfig.IsMono());
            IsFalse(() => x.TapeBound.TapeConfig.IsMono);
            IsFalse(() => x.TapeBound.TapeActions.IsMono());
            IsFalse(() => x.TapeBound.TapeAction.IsMono());
            
            IsTrue(() => x.TapeBound.Tape.IsStereo());
            IsTrue(() => x.TapeBound.TapeConfig.IsStereo());
            IsTrue(() => x.TapeBound.TapeConfig.IsStereo);
            IsTrue(() => x.TapeBound.TapeActions.IsStereo());
            IsTrue(() => x.TapeBound.TapeAction.IsStereo());

            AreEqual(RightChannel, () => x.TapeBound.Tape.Channel());
            AreEqual(RightChannel, () => x.TapeBound.TapeConfig.Channel());
            AreEqual(RightChannel, () => x.TapeBound.TapeConfig.Channel);
            AreEqual(RightChannel, () => x.TapeBound.TapeActions.Channel());
            AreEqual(RightChannel, () => x.TapeBound.TapeAction.Channel());

            IsFalse(() => x.TapeBound.Tape.IsCenter());
            IsFalse(() => x.TapeBound.TapeConfig.IsCenter());
            IsFalse(() => x.TapeBound.TapeConfig.IsCenter);
            IsFalse(() => x.TapeBound.TapeActions.IsCenter());
            IsFalse(() => x.TapeBound.TapeAction.IsCenter());
                                                                    
            IsFalse(() => x.TapeBound.Tape.IsLeft());
            IsFalse(() => x.TapeBound.TapeConfig.IsLeft());
            IsFalse(() => x.TapeBound.TapeConfig.IsLeft);
            IsFalse(() => x.TapeBound.TapeActions.IsLeft());
            IsFalse(() => x.TapeBound.TapeAction.IsLeft());
                                                                    
            IsTrue(() => x.TapeBound.Tape.IsRight());
            IsTrue(() => x.TapeBound.TapeConfig.IsRight());
            IsTrue(() => x.TapeBound.TapeConfig.IsRight);
            IsTrue(() => x.TapeBound.TapeActions.IsRight());
            IsTrue(() => x.TapeBound.TapeAction.IsRight());
        }

        private void Assert_Tape_Getters_Base(TapeEntities x)
        {
            IsNotNull(() => x);
            IsNotNull(() => x.TapeBound.Tape);
            IsNotNull(() => x.TapeBound.TapeConfig);
            IsNotNull(() => x.TapeBound.TapeActions);
            IsNotNull(() => x.TapeBound.TapeAction);
        }

        private void Assert_BuffBound_Getters(TestEntities x, (int channels, int? channel) c)
        {
            // TODO: Handle Mono/Stereo gracefully.

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

        private void Assert_Immutable_Getters(TestEntities x, (int, int?) c)
        {
            Assert_Immutable_Getters(x.Immutable.ChannelEnum, c);
            Assert_Immutable_Getters(x.Immutable.ChannelEntity, c);
        }
        
        private void Assert_Immutable_Getters(ChannelEnum channelEnum, (int channels, int? channel) c)
        {
            if (c.channels == MonoChannels)
            {
                IsTrue (() => channelEnum.IsMono  ());
                IsTrue (() => channelEnum.IsCenter());
                
                IsFalse(() => channelEnum.IsStereo());
                IsFalse(() => channelEnum.IsLeft  ());
                IsFalse(() => channelEnum.IsRight ());

                AreEqual(MonoChannels, () => channelEnum.Channels());
                AreEqual(MonoChannels, () => channelEnum.ChannelEnumToChannels());
                
                AreEqual(CenterChannel, () => channelEnum.Channel());
                AreEqual(CenterChannel, () => channelEnum.EnumToChannel());
            }
            else if (c.channels == StereoChannels)
            {
                IsTrue(() => channelEnum.IsStereo());
                AreEqual(c.channel == 0, () => channelEnum.IsLeft());
                AreEqual(c.channel == 1, () => channelEnum.IsRight());
                
                IsFalse(() => channelEnum.IsMono());
                IsFalse(() => channelEnum.IsCenter());

                AreEqual(StereoChannels, () => channelEnum.Channels());
                AreEqual(StereoChannels, () => channelEnum.ChannelEnumToChannels());

                AreEqual(c.channel, () => channelEnum.Channel());
                AreEqual(c.channel, () => channelEnum.EnumToChannel());
            }
            else
            {
                Fail($"Unsupported combination of values: {new{ channelEnum, c.channels, c.channel }}");
            }
        }
            
        private void Assert_Immutable_Getters(Channel channelEntity, (int channels, int? channel) c)
        {
            if (c.channels == MonoChannels)
            {
                IsTrue (() => channelEntity.IsMono  ());
                IsTrue (() => channelEntity.IsCenter());
                
                IsFalse(() => channelEntity.IsStereo());
                IsFalse(() => channelEntity.IsLeft  ());
                IsFalse(() => channelEntity.IsRight ());

                AreEqual(MonoChannels, () => channelEntity.Channels());
                AreEqual(CenterChannel, () => channelEntity.Channel());
            }
            else if (c.channels == StereoChannels)
            {
                IsTrue(() => channelEntity.IsStereo());
                AreEqual(c.channel == 0, () => channelEntity.IsLeft());
                AreEqual(c.channel == 1, () => channelEntity.IsRight());
                
                IsFalse(() => channelEntity.IsMono());
                IsFalse(() => channelEntity.IsCenter());

                AreEqual(StereoChannels, () => channelEntity.Channels());
                AreEqual(StereoChannels, () => channelEntity.ChannelEntityToChannels());

                AreEqual(c.channel, () => channelEntity.Channel());
                AreEqual(c.channel, () => channelEntity.EntityToChannel());
            }
            else
            {
                Fail($"Unsupported combination of values: {channelEntity?.ID} - {channelEntity?.Name}, {new{ c.channels, c.channel }}");
            }
        }
        
        // Test Data Helpers

        private TestEntities CreateTestEntities((int? channels, int? channel) c)
            => new TestEntities(x => x.WithChannels(c.channels)
                                      .WithChannel (c.channel));
        
        // ncrunch: no coverage start
        
        static readonly int? _ = null;

        /// <summary> Channels / Channel combos. </summary>
        static object TestParametersInit => new[]
        {
            // Stereo configurations
            new object[] { StereoChannels ,   LeftChannel },
            new object[] { StereoChannels ,  RightChannel },
            new object[] { StereoChannels ,  EveryChannel },

            // Mono: channel ignored (defaults to CenterChannel)
            new object[] {   MonoChannels ,    AnyChannel },
            new object[] {   MonoChannels , CenterChannel },
            new object[] {   MonoChannels ,  RightChannel },
            
            // All Mono: null / 0 Channels => defaults to Mono => ignores the channel.
            new object[] { _, _ }, 
            new object[] { 0, _ }, 
            new object[] { _, 0 }, 
            new object[] { 0, 0 }, 
            new object[] { _, 1 }, 
            new object[] { 0, 1 }, 
        };

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
        
        // New attempt to define test data
 
        private static readonly TestCase[] TestTuples =
        {
            Case(init: ((1,0), (_,_)), val: ((2,0), (_,_))),
            Case(init: ((1,0), (_,_)), val: ((2,1), (_,_))),
            Case(init: ((2,1), (_,_)), val: ((_,_), (1,0))),
            Case(init: ((_,_), (1,0)), val: ((2,1), (_,_))),
        };

        private static IEnumerable<object[]> TestParametersNew
            => TestDataDictionary.Keys.Select(x => new object[] { x }).ToArray();
        
        private static readonly Dictionary<string, TestCase> TestDataDictionary = CreateTestDataDictionary();
        
        private static Dictionary<string, TestCase> CreateTestDataDictionary()
        {
            var dictionary = new Dictionary<string, TestCase>();
            
            foreach (var x in TestTuples)
            {
                string descriptor = GetDescriptor(x.init, x.val);
                TestCase coalesced = new TestCase(CoalesceExpect(x.init), CoalesceExpect(x.val));
                dictionary.Add(descriptor, coalesced);
            }
            
            return dictionary;
        }
        
        private static string GetDescriptor(
            ( (int? channels, int? channel) input, (int? channels, int? channel) expect ) init,
            ( (int? channels, int? channel) input, (int? channels, int? channel) expect ) val)
        {
            return $"({init.input.channels},{init.input.channel}) => ({val.input.channels},{val.input.channel})";
        }

        private static 
            ((int? channels, int? channel) input, (int  channels, int? channel) expect) CoalesceExpect(
            ((int? channels, int? channel) input, (int? channels, int? channel) expect) x)
        {
            return (x.input, (channels: x.expect.channels ?? x.input.channels ?? 0, channel: x.expect.channel ?? x.input.channel));
        }
                
        private static TestCase Case(
            ((int? channels, int? channel) input, (int? channels, int? channel) expect) init,
            ((int? channels, int? channel) input, (int? channels, int? channel) expect) val )
        {
            return new TestCase(init, val);
        }
        
        private struct TestCase
        {
            public ((int? channels, int? channel) input, (int? channels, int? channel) expect) init;
            public ((int? channels, int? channel) input, (int? channels, int? channel) expect) val;

            public TestCase(
                ((int? channels, int? channel) input, (int? channels, int? channel) expect) init, 
                ((int? channels, int? channel) input, (int? channels, int? channel) expect) val )
            {
                this.init = init;
                this.val = val;
            }
        }
        
        // ncrunch: no coverage end
    } 
}