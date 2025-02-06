using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using JJ.Business.Synthesizer.Enums;
using JJ.Business.Synthesizer.Structs;
using JJ.Business.Synthesizer.Tests.Accessors;
using JJ.Business.Synthesizer.Wishes.Config;
using JJ.Framework.Reflection;
using JJ.Persistence.Synthesizer;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using static JJ.Business.Synthesizer.Enums.AudioFileFormatEnum;
using static JJ.Business.Synthesizer.Tests.Accessors.ConfigWishesAccessor;
using static JJ.Business.Synthesizer.Wishes.Config.ConfigWishes;
using static JJ.Framework.Testing.AssertHelper;
using static JJ.Framework.Wishes.Common.FilledInWishes;
using static Microsoft.VisualStudio.TestTools.UnitTesting.Assert;
// ReSharper disable ArrangeStaticMemberQualifier

#pragma warning disable CS0611
#pragma warning disable MSTEST0018
#pragma warning disable IDE0002

namespace JJ.Business.Synthesizer.Tests.ConfigTests
{
    [TestClass]
    [TestCategory("Config")]
    public class AudioFormatAttributeTests
    {
        [TestMethod, DataRow(Raw), DataRow(Wav), DataRow(Undefined), DataRow(null)]
        public void Init_AudioFormat(AudioFileFormatEnum? init)
        { 
            var x = CreateTestEntities(init);
            Assert_All_Getters(x, CoalesceAudioFormat(init));
        }

        [TestMethod] 
        [DynamicData(nameof(TestParametersWithEmpty))]
        public void SynthBound_AudioFormat(int? initAsInt, int? valueAsInt)
        {            
            var init  = (AudioFileFormatEnum?)initAsInt;
            var value = (AudioFileFormatEnum?)valueAsInt;
            
            void AssertProp(Action<ConfigTestEntities> setter)
            {
                var x = CreateTestEntities(init);
                Assert_All_Getters(x, CoalesceAudioFormat(init));
                
                setter(x);
                
                Assert_SynthBound_Getters (x, CoalesceAudioFormat(value));
                Assert_TapeBound_Getters  (x, CoalesceAudioFormat(init));
                Assert_BuffBound_Getters  (x, CoalesceAudioFormat(init));
                Assert_Independent_Getters(x, CoalesceAudioFormat(init));
                Assert_Immutable_Getters  (x, CoalesceAudioFormat(init));
                
                x.Record();
                Assert_All_Getters(x, CoalesceAudioFormat(value));
            }
            
            AssertProp(x => AreEqual(x.SynthBound.SynthWishes,    x.SynthBound.SynthWishes   .AudioFormat(value)));
            AssertProp(x => AreEqual(x.SynthBound.FlowNode,       x.SynthBound.FlowNode      .AudioFormat(value)));
            AssertProp(x => AreEqual(x.SynthBound.ConfigResolver, x.SynthBound.ConfigResolver.AudioFormat(value)));
            AssertProp(x => AreEqual(x.SynthBound.SynthWishes,    x.SynthBound.SynthWishes   .WithAudioFormat(value)));
            AssertProp(x => AreEqual(x.SynthBound.FlowNode,       x.SynthBound.FlowNode      .WithAudioFormat(value)));
            AssertProp(x => AreEqual(x.SynthBound.ConfigResolver, x.SynthBound.ConfigResolver.WithAudioFormat(value)));
            AssertProp(x => AreEqual(x.SynthBound.SynthWishes,    x.SynthBound.SynthWishes   .AsAudioFormat(value)));
            AssertProp(x => AreEqual(x.SynthBound.FlowNode,       x.SynthBound.FlowNode      .AsAudioFormat(value)));
            AssertProp(x => AreEqual(x.SynthBound.ConfigResolver, x.SynthBound.ConfigResolver.AsAudioFormat(value)));
            AssertProp(x => AreEqual(x.SynthBound.SynthWishes,    x.SynthBound.SynthWishes   .FromAudioFormat(value)));
            AssertProp(x => AreEqual(x.SynthBound.FlowNode,       x.SynthBound.FlowNode      .FromAudioFormat(value)));
            AssertProp(x => AreEqual(x.SynthBound.ConfigResolver, x.SynthBound.ConfigResolver.FromAudioFormat(value)));
            AssertProp(x => AreEqual(x.SynthBound.SynthWishes,    x.SynthBound.SynthWishes   .ToAudioFormat(value)));
            AssertProp(x => AreEqual(x.SynthBound.FlowNode,       x.SynthBound.FlowNode      .ToAudioFormat(value)));
            AssertProp(x => AreEqual(x.SynthBound.ConfigResolver, x.SynthBound.ConfigResolver.ToAudioFormat(value)));
            AssertProp(x => AreEqual(x.SynthBound.SynthWishes,    x.SynthBound.SynthWishes   .SetAudioFormat(value)));
            AssertProp(x => AreEqual(x.SynthBound.FlowNode,       x.SynthBound.FlowNode      .SetAudioFormat(value)));
            AssertProp(x => AreEqual(x.SynthBound.ConfigResolver, x.SynthBound.ConfigResolver.SetAudioFormat(value)));
            AssertProp(x => AreEqual(x.SynthBound.SynthWishes,    AudioFormat    (x.SynthBound.SynthWishes   , value)));
            AssertProp(x => AreEqual(x.SynthBound.FlowNode,       AudioFormat    (x.SynthBound.FlowNode      , value)));
            AssertProp(x => AreEqual(x.SynthBound.ConfigResolver, AudioFormat    (x.SynthBound.ConfigResolver, value)));
            AssertProp(x => AreEqual(x.SynthBound.SynthWishes,    WithAudioFormat(x.SynthBound.SynthWishes   , value)));
            AssertProp(x => AreEqual(x.SynthBound.FlowNode,       WithAudioFormat(x.SynthBound.FlowNode      , value)));
            AssertProp(x => AreEqual(x.SynthBound.ConfigResolver, WithAudioFormat(x.SynthBound.ConfigResolver, value)));
            AssertProp(x => AreEqual(x.SynthBound.SynthWishes,    AsAudioFormat  (x.SynthBound.SynthWishes   , value)));
            AssertProp(x => AreEqual(x.SynthBound.FlowNode,       AsAudioFormat  (x.SynthBound.FlowNode      , value)));
            AssertProp(x => AreEqual(x.SynthBound.ConfigResolver, AsAudioFormat  (x.SynthBound.ConfigResolver, value)));
            AssertProp(x => AreEqual(x.SynthBound.SynthWishes,    FromAudioFormat(x.SynthBound.SynthWishes   , value)));
            AssertProp(x => AreEqual(x.SynthBound.FlowNode,       FromAudioFormat(x.SynthBound.FlowNode      , value)));
            AssertProp(x => AreEqual(x.SynthBound.ConfigResolver, FromAudioFormat(x.SynthBound.ConfigResolver, value)));
            AssertProp(x => AreEqual(x.SynthBound.SynthWishes,    ToAudioFormat  (x.SynthBound.SynthWishes   , value)));
            AssertProp(x => AreEqual(x.SynthBound.FlowNode,       ToAudioFormat  (x.SynthBound.FlowNode      , value)));
            AssertProp(x => AreEqual(x.SynthBound.ConfigResolver, ToAudioFormat  (x.SynthBound.ConfigResolver, value)));
            AssertProp(x => AreEqual(x.SynthBound.SynthWishes,    SetAudioFormat (x.SynthBound.SynthWishes   , value)));
            AssertProp(x => AreEqual(x.SynthBound.FlowNode,       SetAudioFormat (x.SynthBound.FlowNode      , value)));
            AssertProp(x => AreEqual(x.SynthBound.ConfigResolver, SetAudioFormat (x.SynthBound.ConfigResolver, value)));
            AssertProp(x => AreEqual(x.SynthBound.SynthWishes,    ConfigWishes        .AudioFormat    (x.SynthBound.SynthWishes   , value)));
            AssertProp(x => AreEqual(x.SynthBound.FlowNode,       ConfigWishes        .AudioFormat    (x.SynthBound.FlowNode      , value)));
            AssertProp(x => AreEqual(x.SynthBound.ConfigResolver, ConfigWishesAccessor.AudioFormat    (x.SynthBound.ConfigResolver, value)));
            AssertProp(x => AreEqual(x.SynthBound.SynthWishes,    ConfigWishes        .WithAudioFormat(x.SynthBound.SynthWishes   , value)));
            AssertProp(x => AreEqual(x.SynthBound.FlowNode,       ConfigWishes        .WithAudioFormat(x.SynthBound.FlowNode      , value)));
            AssertProp(x => AreEqual(x.SynthBound.ConfigResolver, ConfigWishesAccessor.WithAudioFormat(x.SynthBound.ConfigResolver, value)));
            AssertProp(x => AreEqual(x.SynthBound.SynthWishes,    ConfigWishes        .AsAudioFormat  (x.SynthBound.SynthWishes   , value)));
            AssertProp(x => AreEqual(x.SynthBound.FlowNode,       ConfigWishes        .AsAudioFormat  (x.SynthBound.FlowNode      , value)));
            AssertProp(x => AreEqual(x.SynthBound.ConfigResolver, ConfigWishesAccessor.AsAudioFormat  (x.SynthBound.ConfigResolver, value)));
            AssertProp(x => AreEqual(x.SynthBound.SynthWishes,    ConfigWishes        .FromAudioFormat(x.SynthBound.SynthWishes   , value)));
            AssertProp(x => AreEqual(x.SynthBound.FlowNode,       ConfigWishes        .FromAudioFormat(x.SynthBound.FlowNode      , value)));
            AssertProp(x => AreEqual(x.SynthBound.ConfigResolver, ConfigWishesAccessor.FromAudioFormat(x.SynthBound.ConfigResolver, value)));
            AssertProp(x => AreEqual(x.SynthBound.SynthWishes,    ConfigWishes        .ToAudioFormat  (x.SynthBound.SynthWishes   , value)));
            AssertProp(x => AreEqual(x.SynthBound.FlowNode,       ConfigWishes        .ToAudioFormat  (x.SynthBound.FlowNode      , value)));
            AssertProp(x => AreEqual(x.SynthBound.ConfigResolver, ConfigWishesAccessor.ToAudioFormat  (x.SynthBound.ConfigResolver, value)));
            AssertProp(x => AreEqual(x.SynthBound.SynthWishes,    ConfigWishes        .SetAudioFormat (x.SynthBound.SynthWishes   , value)));
            AssertProp(x => AreEqual(x.SynthBound.FlowNode,       ConfigWishes        .SetAudioFormat (x.SynthBound.FlowNode      , value)));
            AssertProp(x => AreEqual(x.SynthBound.ConfigResolver, ConfigWishesAccessor.SetAudioFormat (x.SynthBound.ConfigResolver, value)));
            AssertProp(x => { if (value == Raw) AreEqual(x.SynthBound.SynthWishes,    () => x.SynthBound.SynthWishes   .WithRaw());
                              if (value == Wav) AreEqual(x.SynthBound.SynthWishes,    () => x.SynthBound.SynthWishes   .WithWav()); 
                              if (!Has(value))  AreEqual(x.SynthBound.SynthWishes,          x.SynthBound.SynthWishes   .WithAudioFormat(value)); });
            AssertProp(x => { if (value == Raw) AreEqual(x.SynthBound.FlowNode,       () => x.SynthBound.FlowNode      .WithRaw());
                              if (value == Wav) AreEqual(x.SynthBound.FlowNode,       () => x.SynthBound.FlowNode      .WithWav()); 
                              if (!Has(value))  AreEqual(x.SynthBound.FlowNode,             x.SynthBound.FlowNode      .WithAudioFormat(value)); });
            AssertProp(x => { if (value == Raw) AreEqual(x.SynthBound.ConfigResolver, () => x.SynthBound.ConfigResolver.WithRaw());
                              if (value == Wav) AreEqual(x.SynthBound.ConfigResolver, () => x.SynthBound.ConfigResolver.WithWav());
                              if (!Has(value))  AreEqual(x.SynthBound.ConfigResolver,       x.SynthBound.ConfigResolver.WithAudioFormat(value)); });
            AssertProp(x => { if (value == Raw) AreEqual(x.SynthBound.SynthWishes,    () => x.SynthBound.SynthWishes   .AsRaw());
                              if (value == Wav) AreEqual(x.SynthBound.SynthWishes,    () => x.SynthBound.SynthWishes   .AsWav()); 
                              if (!Has(value))  AreEqual(x.SynthBound.SynthWishes,          x.SynthBound.SynthWishes   .AsAudioFormat(value)); });
            AssertProp(x => { if (value == Raw) AreEqual(x.SynthBound.FlowNode,       () => x.SynthBound.FlowNode      .AsRaw());
                              if (value == Wav) AreEqual(x.SynthBound.FlowNode,       () => x.SynthBound.FlowNode      .AsWav()); 
                              if (!Has(value))  AreEqual(x.SynthBound.FlowNode,             x.SynthBound.FlowNode      .AsAudioFormat(value)); });
            AssertProp(x => { if (value == Raw) AreEqual(x.SynthBound.ConfigResolver, () => x.SynthBound.ConfigResolver.AsRaw());
                              if (value == Wav) AreEqual(x.SynthBound.ConfigResolver, () => x.SynthBound.ConfigResolver.AsWav());
                              if (!Has(value))  AreEqual(x.SynthBound.ConfigResolver,       x.SynthBound.ConfigResolver.AsAudioFormat(value)); });
            AssertProp(x => { if (value == Raw) AreEqual(x.SynthBound.SynthWishes,    () => x.SynthBound.SynthWishes   .FromRaw());
                              if (value == Wav) AreEqual(x.SynthBound.SynthWishes,    () => x.SynthBound.SynthWishes   .FromWav()); 
                              if (!Has(value))  AreEqual(x.SynthBound.SynthWishes,          x.SynthBound.SynthWishes   .FromAudioFormat(value)); });
            AssertProp(x => { if (value == Raw) AreEqual(x.SynthBound.FlowNode,       () => x.SynthBound.FlowNode      .FromRaw());
                              if (value == Wav) AreEqual(x.SynthBound.FlowNode,       () => x.SynthBound.FlowNode      .FromWav()); 
                              if (!Has(value))  AreEqual(x.SynthBound.FlowNode,             x.SynthBound.FlowNode      .FromAudioFormat(value)); });
            AssertProp(x => { if (value == Raw) AreEqual(x.SynthBound.ConfigResolver, () => x.SynthBound.ConfigResolver.FromRaw());
                              if (value == Wav) AreEqual(x.SynthBound.ConfigResolver, () => x.SynthBound.ConfigResolver.FromWav());
                              if (!Has(value))  AreEqual(x.SynthBound.ConfigResolver,       x.SynthBound.ConfigResolver.FromAudioFormat(value)); });
            AssertProp(x => { if (value == Raw) AreEqual(x.SynthBound.SynthWishes,    () => x.SynthBound.SynthWishes   .ToRaw());
                              if (value == Wav) AreEqual(x.SynthBound.SynthWishes,    () => x.SynthBound.SynthWishes   .ToWav()); 
                              if (!Has(value))  AreEqual(x.SynthBound.SynthWishes,          x.SynthBound.SynthWishes   .ToAudioFormat(value)); });
            AssertProp(x => { if (value == Raw) AreEqual(x.SynthBound.FlowNode,       () => x.SynthBound.FlowNode      .ToRaw());
                              if (value == Wav) AreEqual(x.SynthBound.FlowNode,       () => x.SynthBound.FlowNode      .ToWav()); 
                              if (!Has(value))  AreEqual(x.SynthBound.FlowNode,             x.SynthBound.FlowNode      .ToAudioFormat(value)); });
            AssertProp(x => { if (value == Raw) AreEqual(x.SynthBound.ConfigResolver, () => x.SynthBound.ConfigResolver.ToRaw());
                              if (value == Wav) AreEqual(x.SynthBound.ConfigResolver, () => x.SynthBound.ConfigResolver.ToWav());
                              if (!Has(value))  AreEqual(x.SynthBound.ConfigResolver,       x.SynthBound.ConfigResolver.ToAudioFormat(value)); });
            AssertProp(x => { if (value == Raw) AreEqual(x.SynthBound.SynthWishes,    () => x.SynthBound.SynthWishes   .SetRaw());
                              if (value == Wav) AreEqual(x.SynthBound.SynthWishes,    () => x.SynthBound.SynthWishes   .SetWav()); 
                              if (!Has(value))  AreEqual(x.SynthBound.SynthWishes,          x.SynthBound.SynthWishes   .SetAudioFormat(value)); });
            AssertProp(x => { if (value == Raw) AreEqual(x.SynthBound.FlowNode,       () => x.SynthBound.FlowNode      .SetRaw());
                              if (value == Wav) AreEqual(x.SynthBound.FlowNode,       () => x.SynthBound.FlowNode      .SetWav()); 
                              if (!Has(value))  AreEqual(x.SynthBound.FlowNode,             x.SynthBound.FlowNode      .SetAudioFormat(value)); });
            AssertProp(x => { if (value == Raw) AreEqual(x.SynthBound.ConfigResolver, () => x.SynthBound.ConfigResolver.SetRaw());
                              if (value == Wav) AreEqual(x.SynthBound.ConfigResolver, () => x.SynthBound.ConfigResolver.SetWav());
                              if (!Has(value))  AreEqual(x.SynthBound.ConfigResolver,       x.SynthBound.ConfigResolver.SetAudioFormat(value)); });
            AssertProp(x => { if (value == Raw) AreEqual(x.SynthBound.SynthWishes,    () => WithRaw        (x.SynthBound.SynthWishes));
                              if (value == Wav) AreEqual(x.SynthBound.SynthWishes,    () => WithWav        (x.SynthBound.SynthWishes)); 
                              if (!Has(value))  AreEqual(x.SynthBound.SynthWishes,          WithAudioFormat(x.SynthBound.SynthWishes, value)); });
            AssertProp(x => { if (value == Raw) AreEqual(x.SynthBound.FlowNode,       () => WithRaw        (x.SynthBound.FlowNode));
                              if (value == Wav) AreEqual(x.SynthBound.FlowNode,       () => WithWav        (x.SynthBound.FlowNode)); 
                              if (!Has(value))  AreEqual(x.SynthBound.FlowNode,             WithAudioFormat(x.SynthBound.FlowNode, value)); });
            AssertProp(x => { if (value == Raw) AreEqual(x.SynthBound.ConfigResolver, () => WithRaw        (x.SynthBound.ConfigResolver));
                              if (value == Wav) AreEqual(x.SynthBound.ConfigResolver, () => WithWav        (x.SynthBound.ConfigResolver));
                              if (!Has(value))  AreEqual(x.SynthBound.ConfigResolver,       WithAudioFormat(x.SynthBound.ConfigResolver, value)); });
            AssertProp(x => { if (value == Raw) AreEqual(x.SynthBound.SynthWishes,    () => AsRaw          (x.SynthBound.SynthWishes));
                              if (value == Wav) AreEqual(x.SynthBound.SynthWishes,    () => AsWav          (x.SynthBound.SynthWishes)); 
                              if (!Has(value))  AreEqual(x.SynthBound.SynthWishes,          AsAudioFormat  (x.SynthBound.SynthWishes, value)); });
            AssertProp(x => { if (value == Raw) AreEqual(x.SynthBound.FlowNode,       () => AsRaw          (x.SynthBound.FlowNode));
                              if (value == Wav) AreEqual(x.SynthBound.FlowNode,       () => AsWav          (x.SynthBound.FlowNode)); 
                              if (!Has(value))  AreEqual(x.SynthBound.FlowNode,             AsAudioFormat  (x.SynthBound.FlowNode, value)); });
            AssertProp(x => { if (value == Raw) AreEqual(x.SynthBound.ConfigResolver, () => AsRaw          (x.SynthBound.ConfigResolver));
                              if (value == Wav) AreEqual(x.SynthBound.ConfigResolver, () => AsWav          (x.SynthBound.ConfigResolver));
                              if (!Has(value))  AreEqual(x.SynthBound.ConfigResolver,       AsAudioFormat  (x.SynthBound.ConfigResolver, value)); });
            AssertProp(x => { if (value == Raw) AreEqual(x.SynthBound.SynthWishes,    () => FromRaw        (x.SynthBound.SynthWishes));
                              if (value == Wav) AreEqual(x.SynthBound.SynthWishes,    () => FromWav        (x.SynthBound.SynthWishes)); 
                              if (!Has(value))  AreEqual(x.SynthBound.SynthWishes,          FromAudioFormat(x.SynthBound.SynthWishes, value)); });
            AssertProp(x => { if (value == Raw) AreEqual(x.SynthBound.FlowNode,       () => FromRaw        (x.SynthBound.FlowNode));
                              if (value == Wav) AreEqual(x.SynthBound.FlowNode,       () => FromWav        (x.SynthBound.FlowNode)); 
                              if (!Has(value))  AreEqual(x.SynthBound.FlowNode,             FromAudioFormat(x.SynthBound.FlowNode, value)); });
            AssertProp(x => { if (value == Raw) AreEqual(x.SynthBound.ConfigResolver, () => FromRaw        (x.SynthBound.ConfigResolver));
                              if (value == Wav) AreEqual(x.SynthBound.ConfigResolver, () => FromWav        (x.SynthBound.ConfigResolver));
                              if (!Has(value))  AreEqual(x.SynthBound.ConfigResolver,       FromAudioFormat(x.SynthBound.ConfigResolver,value)); });
            AssertProp(x => { if (value == Raw) AreEqual(x.SynthBound.SynthWishes,    () => ToRaw          (x.SynthBound.SynthWishes));
                              if (value == Wav) AreEqual(x.SynthBound.SynthWishes,    () => ToWav          (x.SynthBound.SynthWishes)); 
                              if (!Has(value))  AreEqual(x.SynthBound.SynthWishes,          ToAudioFormat  (x.SynthBound.SynthWishes, value)); });
            AssertProp(x => { if (value == Raw) AreEqual(x.SynthBound.FlowNode,       () => ToRaw          (x.SynthBound.FlowNode));
                              if (value == Wav) AreEqual(x.SynthBound.FlowNode,       () => ToWav          (x.SynthBound.FlowNode)); 
                              if (!Has(value))  AreEqual(x.SynthBound.FlowNode,             ToAudioFormat  (x.SynthBound.FlowNode, value)); });
            AssertProp(x => { if (value == Raw) AreEqual(x.SynthBound.ConfigResolver, () => ToRaw          (x.SynthBound.ConfigResolver));
                              if (value == Wav) AreEqual(x.SynthBound.ConfigResolver, () => ToWav          (x.SynthBound.ConfigResolver));
                              if (!Has(value))  AreEqual(x.SynthBound.ConfigResolver,       ToAudioFormat  (x.SynthBound.ConfigResolver, value)); });
            AssertProp(x => { if (value == Raw) AreEqual(x.SynthBound.SynthWishes,    () => SetRaw         (x.SynthBound.SynthWishes));
                              if (value == Wav) AreEqual(x.SynthBound.SynthWishes,    () => SetWav         (x.SynthBound.SynthWishes)); 
                              if (!Has(value))  AreEqual(x.SynthBound.SynthWishes,          SetAudioFormat (x.SynthBound.SynthWishes, value)); });
            AssertProp(x => { if (value == Raw) AreEqual(x.SynthBound.FlowNode,       () => SetRaw         (x.SynthBound.FlowNode));
                              if (value == Wav) AreEqual(x.SynthBound.FlowNode,       () => SetWav         (x.SynthBound.FlowNode)); 
                              if (!Has(value))  AreEqual(x.SynthBound.FlowNode,             SetAudioFormat (x.SynthBound.FlowNode, value)); });
            AssertProp(x => { if (value == Raw) AreEqual(x.SynthBound.ConfigResolver, () => SetRaw         (x.SynthBound.ConfigResolver));
                              if (value == Wav) AreEqual(x.SynthBound.ConfigResolver, () => SetWav         (x.SynthBound.ConfigResolver));
                              if (!Has(value))  AreEqual(x.SynthBound.ConfigResolver,       SetAudioFormat (x.SynthBound.ConfigResolver, value)); });
            AssertProp(x => { if (value == Raw) AreEqual(x.SynthBound.SynthWishes,    () => ConfigWishes        .WithRaw        (x.SynthBound.SynthWishes));
                              if (value == Wav) AreEqual(x.SynthBound.SynthWishes,    () => ConfigWishes        .WithWav        (x.SynthBound.SynthWishes)); 
                              if (!Has(value))  AreEqual(x.SynthBound.SynthWishes,          ConfigWishes        .WithAudioFormat(x.SynthBound.SynthWishes, value)); });
            AssertProp(x => { if (value == Raw) AreEqual(x.SynthBound.FlowNode,       () => ConfigWishes        .WithRaw        (x.SynthBound.FlowNode));
                              if (value == Wav) AreEqual(x.SynthBound.FlowNode,       () => ConfigWishes        .WithWav        (x.SynthBound.FlowNode)); 
                              if (!Has(value))  AreEqual(x.SynthBound.FlowNode,             ConfigWishes        .WithAudioFormat(x.SynthBound.FlowNode, value)); });
            AssertProp(x => { if (value == Raw) AreEqual(x.SynthBound.ConfigResolver, () => ConfigWishesAccessor.WithRaw        (x.SynthBound.ConfigResolver));
                              if (value == Wav) AreEqual(x.SynthBound.ConfigResolver, () => ConfigWishesAccessor.WithWav        (x.SynthBound.ConfigResolver));
                              if (!Has(value))  AreEqual(x.SynthBound.ConfigResolver,       ConfigWishesAccessor.WithAudioFormat(x.SynthBound.ConfigResolver, value)); });
            AssertProp(x => { if (value == Raw) AreEqual(x.SynthBound.SynthWishes,    () => ConfigWishes        .AsRaw          (x.SynthBound.SynthWishes));
                              if (value == Wav) AreEqual(x.SynthBound.SynthWishes,    () => ConfigWishes        .AsWav          (x.SynthBound.SynthWishes)); 
                              if (!Has(value))  AreEqual(x.SynthBound.SynthWishes,          ConfigWishes        .AsAudioFormat  (x.SynthBound.SynthWishes, value)); });
            AssertProp(x => { if (value == Raw) AreEqual(x.SynthBound.FlowNode,       () => ConfigWishes        .AsRaw          (x.SynthBound.FlowNode));
                              if (value == Wav) AreEqual(x.SynthBound.FlowNode,       () => ConfigWishes        .AsWav          (x.SynthBound.FlowNode)); 
                              if (!Has(value))  AreEqual(x.SynthBound.FlowNode,             ConfigWishes        .AsAudioFormat  (x.SynthBound.FlowNode, value)); });
            AssertProp(x => { if (value == Raw) AreEqual(x.SynthBound.ConfigResolver, () => ConfigWishesAccessor.AsRaw          (x.SynthBound.ConfigResolver));
                              if (value == Wav) AreEqual(x.SynthBound.ConfigResolver, () => ConfigWishesAccessor.AsWav          (x.SynthBound.ConfigResolver));
                              if (!Has(value))  AreEqual(x.SynthBound.ConfigResolver,       ConfigWishesAccessor.AsAudioFormat  (x.SynthBound.ConfigResolver, value)); });
            AssertProp(x => { if (value == Raw) AreEqual(x.SynthBound.SynthWishes,    () => ConfigWishes        .FromRaw        (x.SynthBound.SynthWishes));
                              if (value == Wav) AreEqual(x.SynthBound.SynthWishes,    () => ConfigWishes        .FromWav        (x.SynthBound.SynthWishes)); 
                              if (!Has(value))  AreEqual(x.SynthBound.SynthWishes,          ConfigWishes        .FromAudioFormat(x.SynthBound.SynthWishes, value)); });
            AssertProp(x => { if (value == Raw) AreEqual(x.SynthBound.FlowNode,       () => ConfigWishes        .FromRaw        (x.SynthBound.FlowNode));
                              if (value == Wav) AreEqual(x.SynthBound.FlowNode,       () => ConfigWishes        .FromWav        (x.SynthBound.FlowNode)); 
                              if (!Has(value))  AreEqual(x.SynthBound.FlowNode,             ConfigWishes        .FromAudioFormat(x.SynthBound.FlowNode, value)); });
            AssertProp(x => { if (value == Raw) AreEqual(x.SynthBound.ConfigResolver, () => ConfigWishesAccessor.FromRaw        (x.SynthBound.ConfigResolver));
                              if (value == Wav) AreEqual(x.SynthBound.ConfigResolver, () => ConfigWishesAccessor.FromWav        (x.SynthBound.ConfigResolver));
                              if (!Has(value))  AreEqual(x.SynthBound.ConfigResolver,       ConfigWishesAccessor.FromAudioFormat(x.SynthBound.ConfigResolver,value)); });
            AssertProp(x => { if (value == Raw) AreEqual(x.SynthBound.SynthWishes,    () => ConfigWishes        .ToRaw          (x.SynthBound.SynthWishes));
                              if (value == Wav) AreEqual(x.SynthBound.SynthWishes,    () => ConfigWishes        .ToWav          (x.SynthBound.SynthWishes)); 
                              if (!Has(value))  AreEqual(x.SynthBound.SynthWishes,          ConfigWishes        .ToAudioFormat  (x.SynthBound.SynthWishes, value)); });
            AssertProp(x => { if (value == Raw) AreEqual(x.SynthBound.FlowNode,       () => ConfigWishes        .ToRaw          (x.SynthBound.FlowNode));
                              if (value == Wav) AreEqual(x.SynthBound.FlowNode,       () => ConfigWishes        .ToWav          (x.SynthBound.FlowNode)); 
                              if (!Has(value))  AreEqual(x.SynthBound.FlowNode,             ConfigWishes        .ToAudioFormat  (x.SynthBound.FlowNode, value)); });
            AssertProp(x => { if (value == Raw) AreEqual(x.SynthBound.ConfigResolver, () => ConfigWishesAccessor.ToRaw          (x.SynthBound.ConfigResolver));
                              if (value == Wav) AreEqual(x.SynthBound.ConfigResolver, () => ConfigWishesAccessor.ToWav          (x.SynthBound.ConfigResolver));
                              if (!Has(value))  AreEqual(x.SynthBound.ConfigResolver,       ConfigWishesAccessor.ToAudioFormat  (x.SynthBound.ConfigResolver, value)); });
            AssertProp(x => { if (value == Raw) AreEqual(x.SynthBound.SynthWishes,    () => ConfigWishes        .SetRaw         (x.SynthBound.SynthWishes));
                              if (value == Wav) AreEqual(x.SynthBound.SynthWishes,    () => ConfigWishes        .SetWav         (x.SynthBound.SynthWishes)); 
                              if (!Has(value))  AreEqual(x.SynthBound.SynthWishes,          ConfigWishes        .SetAudioFormat (x.SynthBound.SynthWishes, value)); });
            AssertProp(x => { if (value == Raw) AreEqual(x.SynthBound.FlowNode,       () => ConfigWishes        .SetRaw         (x.SynthBound.FlowNode));
                              if (value == Wav) AreEqual(x.SynthBound.FlowNode,       () => ConfigWishes        .SetWav         (x.SynthBound.FlowNode)); 
                              if (!Has(value))  AreEqual(x.SynthBound.FlowNode,             ConfigWishes        .SetAudioFormat (x.SynthBound.FlowNode, value)); });
            AssertProp(x => { if (value == Raw) AreEqual(x.SynthBound.ConfigResolver, () => ConfigWishesAccessor.SetRaw         (x.SynthBound.ConfigResolver));
                              if (value == Wav) AreEqual(x.SynthBound.ConfigResolver, () => ConfigWishesAccessor.SetWav         (x.SynthBound.ConfigResolver));
                              if (!Has(value))  AreEqual(x.SynthBound.ConfigResolver,       ConfigWishesAccessor.SetAudioFormat (x.SynthBound.ConfigResolver, value)); });
        }

        [TestMethod] 
        [DynamicData(nameof(TestParameters))]
        public void TapeBound_AudioFormat(int initAsInt, int valueAsInt)
        {
            var init  = (AudioFileFormatEnum)initAsInt;
            var value = (AudioFileFormatEnum)valueAsInt;

            void AssertProp(Action<ConfigTestEntities> setter)
            {
                var x = CreateTestEntities(init);
                Assert_All_Getters(x, init);
                
                setter(x);
                
                Assert_SynthBound_Getters(x, init);
                Assert_TapeBound_Getters(x, value);
                Assert_BuffBound_Getters(x, init);
                Assert_Independent_Getters(x, init);
                Assert_Immutable_Getters(x, init);
                
                x.Record();
                Assert_All_Getters(x, init); // By Design: Currently you can't record over the same tape. So you always get a new tape, resetting the values.
            }

            AssertProp(x =>                                         x.TapeBound.TapeConfig .AudioFormat = value);
            AssertProp(x => AreEqual(x.TapeBound.Tape,        () => x.TapeBound.Tape       .AudioFormat(value)));
            AssertProp(x => AreEqual(x.TapeBound.TapeConfig,  () => x.TapeBound.TapeConfig .AudioFormat(value)));
            AssertProp(x => AreEqual(x.TapeBound.TapeActions, () => x.TapeBound.TapeActions.AudioFormat(value)));
            AssertProp(x => AreEqual(x.TapeBound.TapeAction,  () => x.TapeBound.TapeAction .AudioFormat(value)));
            AssertProp(x => AreEqual(x.TapeBound.Tape,        () => x.TapeBound.Tape       .WithAudioFormat(value)));
            AssertProp(x => AreEqual(x.TapeBound.TapeConfig,  () => x.TapeBound.TapeConfig .WithAudioFormat(value)));
            AssertProp(x => AreEqual(x.TapeBound.TapeActions, () => x.TapeBound.TapeActions.WithAudioFormat(value)));
            AssertProp(x => AreEqual(x.TapeBound.TapeAction,  () => x.TapeBound.TapeAction .WithAudioFormat(value)));
            AssertProp(x => AreEqual(x.TapeBound.Tape,        () => x.TapeBound.Tape       .AsAudioFormat(value)));
            AssertProp(x => AreEqual(x.TapeBound.TapeConfig,  () => x.TapeBound.TapeConfig .AsAudioFormat(value)));
            AssertProp(x => AreEqual(x.TapeBound.TapeActions, () => x.TapeBound.TapeActions.AsAudioFormat(value)));
            AssertProp(x => AreEqual(x.TapeBound.TapeAction,  () => x.TapeBound.TapeAction .AsAudioFormat(value)));
            AssertProp(x => AreEqual(x.TapeBound.Tape,        () => x.TapeBound.Tape       .FromAudioFormat(value)));
            AssertProp(x => AreEqual(x.TapeBound.TapeConfig,  () => x.TapeBound.TapeConfig .FromAudioFormat(value)));
            AssertProp(x => AreEqual(x.TapeBound.TapeActions, () => x.TapeBound.TapeActions.FromAudioFormat(value)));
            AssertProp(x => AreEqual(x.TapeBound.TapeAction,  () => x.TapeBound.TapeAction .FromAudioFormat(value)));
            AssertProp(x => AreEqual(x.TapeBound.Tape,        () => x.TapeBound.Tape       .ToAudioFormat(value)));
            AssertProp(x => AreEqual(x.TapeBound.TapeConfig,  () => x.TapeBound.TapeConfig .ToAudioFormat(value)));
            AssertProp(x => AreEqual(x.TapeBound.TapeActions, () => x.TapeBound.TapeActions.ToAudioFormat(value)));
            AssertProp(x => AreEqual(x.TapeBound.TapeAction,  () => x.TapeBound.TapeAction .ToAudioFormat(value)));
            AssertProp(x => AreEqual(x.TapeBound.Tape,        () => x.TapeBound.Tape       .SetAudioFormat(value)));
            AssertProp(x => AreEqual(x.TapeBound.TapeConfig,  () => x.TapeBound.TapeConfig .SetAudioFormat(value)));
            AssertProp(x => AreEqual(x.TapeBound.TapeActions, () => x.TapeBound.TapeActions.SetAudioFormat(value)));
            AssertProp(x => AreEqual(x.TapeBound.TapeAction,  () => x.TapeBound.TapeAction .SetAudioFormat(value)));
            AssertProp(x => AreEqual(x.TapeBound.Tape,        () => AudioFormat    (x.TapeBound.Tape       , value)));
            AssertProp(x => AreEqual(x.TapeBound.TapeConfig,  () => AudioFormat    (x.TapeBound.TapeConfig , value)));
            AssertProp(x => AreEqual(x.TapeBound.TapeActions, () => AudioFormat    (x.TapeBound.TapeActions, value)));
            AssertProp(x => AreEqual(x.TapeBound.TapeAction,  () => AudioFormat    (x.TapeBound.TapeAction , value)));
            AssertProp(x => AreEqual(x.TapeBound.Tape,        () => WithAudioFormat(x.TapeBound.Tape       , value)));
            AssertProp(x => AreEqual(x.TapeBound.TapeConfig,  () => WithAudioFormat(x.TapeBound.TapeConfig , value)));
            AssertProp(x => AreEqual(x.TapeBound.TapeActions, () => WithAudioFormat(x.TapeBound.TapeActions, value)));
            AssertProp(x => AreEqual(x.TapeBound.TapeAction,  () => WithAudioFormat(x.TapeBound.TapeAction , value)));
            AssertProp(x => AreEqual(x.TapeBound.Tape,        () => AsAudioFormat  (x.TapeBound.Tape       , value)));
            AssertProp(x => AreEqual(x.TapeBound.TapeConfig,  () => AsAudioFormat  (x.TapeBound.TapeConfig , value)));
            AssertProp(x => AreEqual(x.TapeBound.TapeActions, () => AsAudioFormat  (x.TapeBound.TapeActions, value)));
            AssertProp(x => AreEqual(x.TapeBound.TapeAction,  () => AsAudioFormat  (x.TapeBound.TapeAction , value)));
            AssertProp(x => AreEqual(x.TapeBound.Tape,        () => FromAudioFormat(x.TapeBound.Tape       , value)));
            AssertProp(x => AreEqual(x.TapeBound.TapeConfig,  () => FromAudioFormat(x.TapeBound.TapeConfig , value)));
            AssertProp(x => AreEqual(x.TapeBound.TapeActions, () => FromAudioFormat(x.TapeBound.TapeActions, value)));
            AssertProp(x => AreEqual(x.TapeBound.TapeAction,  () => FromAudioFormat(x.TapeBound.TapeAction , value)));
            AssertProp(x => AreEqual(x.TapeBound.Tape,        () => ToAudioFormat  (x.TapeBound.Tape       , value)));
            AssertProp(x => AreEqual(x.TapeBound.TapeConfig,  () => ToAudioFormat  (x.TapeBound.TapeConfig , value)));
            AssertProp(x => AreEqual(x.TapeBound.TapeActions, () => ToAudioFormat  (x.TapeBound.TapeActions, value)));
            AssertProp(x => AreEqual(x.TapeBound.TapeAction,  () => ToAudioFormat  (x.TapeBound.TapeAction , value)));
            AssertProp(x => AreEqual(x.TapeBound.Tape,        () => SetAudioFormat (x.TapeBound.Tape       , value)));
            AssertProp(x => AreEqual(x.TapeBound.TapeConfig,  () => SetAudioFormat (x.TapeBound.TapeConfig , value)));
            AssertProp(x => AreEqual(x.TapeBound.TapeActions, () => SetAudioFormat (x.TapeBound.TapeActions, value)));
            AssertProp(x => AreEqual(x.TapeBound.TapeAction,  () => SetAudioFormat (x.TapeBound.TapeAction , value)));
            AssertProp(x => AreEqual(x.TapeBound.Tape,        () => ConfigWishes.AudioFormat    (x.TapeBound.Tape       , value)));
            AssertProp(x => AreEqual(x.TapeBound.TapeConfig,  () => ConfigWishes.AudioFormat    (x.TapeBound.TapeConfig , value)));
            AssertProp(x => AreEqual(x.TapeBound.TapeActions, () => ConfigWishes.AudioFormat    (x.TapeBound.TapeActions, value)));
            AssertProp(x => AreEqual(x.TapeBound.TapeAction,  () => ConfigWishes.AudioFormat    (x.TapeBound.TapeAction , value)));
            AssertProp(x => AreEqual(x.TapeBound.Tape,        () => ConfigWishes.WithAudioFormat(x.TapeBound.Tape       , value)));
            AssertProp(x => AreEqual(x.TapeBound.TapeConfig,  () => ConfigWishes.WithAudioFormat(x.TapeBound.TapeConfig , value)));
            AssertProp(x => AreEqual(x.TapeBound.TapeActions, () => ConfigWishes.WithAudioFormat(x.TapeBound.TapeActions, value)));
            AssertProp(x => AreEqual(x.TapeBound.TapeAction,  () => ConfigWishes.WithAudioFormat(x.TapeBound.TapeAction , value)));
            AssertProp(x => AreEqual(x.TapeBound.Tape,        () => ConfigWishes.AsAudioFormat  (x.TapeBound.Tape       , value)));
            AssertProp(x => AreEqual(x.TapeBound.TapeConfig,  () => ConfigWishes.AsAudioFormat  (x.TapeBound.TapeConfig , value)));
            AssertProp(x => AreEqual(x.TapeBound.TapeActions, () => ConfigWishes.AsAudioFormat  (x.TapeBound.TapeActions, value)));
            AssertProp(x => AreEqual(x.TapeBound.TapeAction,  () => ConfigWishes.AsAudioFormat  (x.TapeBound.TapeAction , value)));
            AssertProp(x => AreEqual(x.TapeBound.Tape,        () => ConfigWishes.FromAudioFormat(x.TapeBound.Tape       , value)));
            AssertProp(x => AreEqual(x.TapeBound.TapeConfig,  () => ConfigWishes.FromAudioFormat(x.TapeBound.TapeConfig , value)));
            AssertProp(x => AreEqual(x.TapeBound.TapeActions, () => ConfigWishes.FromAudioFormat(x.TapeBound.TapeActions, value)));
            AssertProp(x => AreEqual(x.TapeBound.TapeAction,  () => ConfigWishes.FromAudioFormat(x.TapeBound.TapeAction , value)));
            AssertProp(x => AreEqual(x.TapeBound.Tape,        () => ConfigWishes.ToAudioFormat  (x.TapeBound.Tape       , value)));
            AssertProp(x => AreEqual(x.TapeBound.TapeConfig,  () => ConfigWishes.ToAudioFormat  (x.TapeBound.TapeConfig , value)));
            AssertProp(x => AreEqual(x.TapeBound.TapeActions, () => ConfigWishes.ToAudioFormat  (x.TapeBound.TapeActions, value)));
            AssertProp(x => AreEqual(x.TapeBound.TapeAction,  () => ConfigWishes.ToAudioFormat  (x.TapeBound.TapeAction , value)));
            AssertProp(x => AreEqual(x.TapeBound.Tape,        () => ConfigWishes.SetAudioFormat (x.TapeBound.Tape       , value)));
            AssertProp(x => AreEqual(x.TapeBound.TapeConfig,  () => ConfigWishes.SetAudioFormat (x.TapeBound.TapeConfig , value)));
            AssertProp(x => AreEqual(x.TapeBound.TapeActions, () => ConfigWishes.SetAudioFormat (x.TapeBound.TapeActions, value)));
            AssertProp(x => AreEqual(x.TapeBound.TapeAction,  () => ConfigWishes.SetAudioFormat (x.TapeBound.TapeAction , value)));
            AssertProp(x => { if (value == Raw) AreEqual(x.TapeBound.Tape,        () => x.TapeBound.Tape       .WithRaw());
                              if (value == Wav) AreEqual(x.TapeBound.Tape,        () => x.TapeBound.Tape       .WithWav()); });
            AssertProp(x => { if (value == Raw) AreEqual(x.TapeBound.TapeConfig,  () => x.TapeBound.TapeConfig .WithRaw());
                              if (value == Wav) AreEqual(x.TapeBound.TapeConfig,  () => x.TapeBound.TapeConfig .WithWav()); });
            AssertProp(x => { if (value == Raw) AreEqual(x.TapeBound.TapeActions, () => x.TapeBound.TapeActions.WithRaw());
                              if (value == Wav) AreEqual(x.TapeBound.TapeActions, () => x.TapeBound.TapeActions.WithWav()); });
            AssertProp(x => { if (value == Raw) AreEqual(x.TapeBound.TapeAction,  () => x.TapeBound.TapeAction .WithRaw());
                              if (value == Wav) AreEqual(x.TapeBound.TapeAction,  () => x.TapeBound.TapeAction .WithWav()); });
            AssertProp(x => { if (value == Raw) AreEqual(x.TapeBound.Tape,        () => x.TapeBound.Tape       .AsRaw());
                              if (value == Wav) AreEqual(x.TapeBound.Tape,        () => x.TapeBound.Tape       .AsWav()); });
            AssertProp(x => { if (value == Raw) AreEqual(x.TapeBound.TapeConfig,  () => x.TapeBound.TapeConfig .AsRaw());
                              if (value == Wav) AreEqual(x.TapeBound.TapeConfig,  () => x.TapeBound.TapeConfig .AsWav()); });
            AssertProp(x => { if (value == Raw) AreEqual(x.TapeBound.TapeActions, () => x.TapeBound.TapeActions.AsRaw());
                              if (value == Wav) AreEqual(x.TapeBound.TapeActions, () => x.TapeBound.TapeActions.AsWav()); });
            AssertProp(x => { if (value == Raw) AreEqual(x.TapeBound.TapeAction,  () => x.TapeBound.TapeAction .AsRaw());
                              if (value == Wav) AreEqual(x.TapeBound.TapeAction,  () => x.TapeBound.TapeAction .AsWav()); });
            AssertProp(x => { if (value == Raw) AreEqual(x.TapeBound.Tape,        () => x.TapeBound.Tape       .FromRaw());
                              if (value == Wav) AreEqual(x.TapeBound.Tape,        () => x.TapeBound.Tape       .FromWav()); });
            AssertProp(x => { if (value == Raw) AreEqual(x.TapeBound.TapeConfig,  () => x.TapeBound.TapeConfig .FromRaw());
                              if (value == Wav) AreEqual(x.TapeBound.TapeConfig,  () => x.TapeBound.TapeConfig .FromWav()); });
            AssertProp(x => { if (value == Raw) AreEqual(x.TapeBound.TapeActions, () => x.TapeBound.TapeActions.FromRaw());
                              if (value == Wav) AreEqual(x.TapeBound.TapeActions, () => x.TapeBound.TapeActions.FromWav()); });
            AssertProp(x => { if (value == Raw) AreEqual(x.TapeBound.TapeAction,  () => x.TapeBound.TapeAction .FromRaw());
                              if (value == Wav) AreEqual(x.TapeBound.TapeAction,  () => x.TapeBound.TapeAction .FromWav()); });
            AssertProp(x => { if (value == Raw) AreEqual(x.TapeBound.Tape,        () => x.TapeBound.Tape       .ToRaw());
                              if (value == Wav) AreEqual(x.TapeBound.Tape,        () => x.TapeBound.Tape       .ToWav()); });
            AssertProp(x => { if (value == Raw) AreEqual(x.TapeBound.TapeConfig,  () => x.TapeBound.TapeConfig .ToRaw());
                              if (value == Wav) AreEqual(x.TapeBound.TapeConfig,  () => x.TapeBound.TapeConfig .ToWav()); });
            AssertProp(x => { if (value == Raw) AreEqual(x.TapeBound.TapeActions, () => x.TapeBound.TapeActions.ToRaw());
                              if (value == Wav) AreEqual(x.TapeBound.TapeActions, () => x.TapeBound.TapeActions.ToWav()); });
            AssertProp(x => { if (value == Raw) AreEqual(x.TapeBound.TapeAction,  () => x.TapeBound.TapeAction .ToRaw());
                              if (value == Wav) AreEqual(x.TapeBound.TapeAction,  () => x.TapeBound.TapeAction .ToWav()); });
            AssertProp(x => { if (value == Raw) AreEqual(x.TapeBound.Tape,        () => x.TapeBound.Tape       .SetRaw());
                              if (value == Wav) AreEqual(x.TapeBound.Tape,        () => x.TapeBound.Tape       .SetWav()); });
            AssertProp(x => { if (value == Raw) AreEqual(x.TapeBound.TapeConfig,  () => x.TapeBound.TapeConfig .SetRaw());
                              if (value == Wav) AreEqual(x.TapeBound.TapeConfig,  () => x.TapeBound.TapeConfig .SetWav()); });
            AssertProp(x => { if (value == Raw) AreEqual(x.TapeBound.TapeActions, () => x.TapeBound.TapeActions.SetRaw());
                              if (value == Wav) AreEqual(x.TapeBound.TapeActions, () => x.TapeBound.TapeActions.SetWav()); });
            AssertProp(x => { if (value == Raw) AreEqual(x.TapeBound.TapeAction,  () => x.TapeBound.TapeAction .SetRaw());
                              if (value == Wav) AreEqual(x.TapeBound.TapeAction,  () => x.TapeBound.TapeAction .SetWav()); });
            AssertProp(x => { if (value == Raw) AreEqual(x.TapeBound.Tape,        () => WithRaw(x.TapeBound.Tape       ));
                              if (value == Wav) AreEqual(x.TapeBound.Tape,        () => WithWav(x.TapeBound.Tape       )); });
            AssertProp(x => { if (value == Raw) AreEqual(x.TapeBound.TapeConfig,  () => WithRaw(x.TapeBound.TapeConfig ));
                              if (value == Wav) AreEqual(x.TapeBound.TapeConfig,  () => WithWav(x.TapeBound.TapeConfig )); });
            AssertProp(x => { if (value == Raw) AreEqual(x.TapeBound.TapeActions, () => WithRaw(x.TapeBound.TapeActions));
                              if (value == Wav) AreEqual(x.TapeBound.TapeActions, () => WithWav(x.TapeBound.TapeActions)); });
            AssertProp(x => { if (value == Raw) AreEqual(x.TapeBound.TapeAction,  () => WithRaw(x.TapeBound.TapeAction ));
                              if (value == Wav) AreEqual(x.TapeBound.TapeAction,  () => WithWav(x.TapeBound.TapeAction )); });
            AssertProp(x => { if (value == Raw) AreEqual(x.TapeBound.Tape,        () => AsRaw  (x.TapeBound.Tape       ));
                              if (value == Wav) AreEqual(x.TapeBound.Tape,        () => AsWav  (x.TapeBound.Tape       )); });
            AssertProp(x => { if (value == Raw) AreEqual(x.TapeBound.TapeConfig,  () => AsRaw  (x.TapeBound.TapeConfig ));
                              if (value == Wav) AreEqual(x.TapeBound.TapeConfig,  () => AsWav  (x.TapeBound.TapeConfig )); });
            AssertProp(x => { if (value == Raw) AreEqual(x.TapeBound.TapeActions, () => AsRaw  (x.TapeBound.TapeActions));
                              if (value == Wav) AreEqual(x.TapeBound.TapeActions, () => AsWav  (x.TapeBound.TapeActions)); });
            AssertProp(x => { if (value == Raw) AreEqual(x.TapeBound.TapeAction,  () => AsRaw  (x.TapeBound.TapeAction ));
                              if (value == Wav) AreEqual(x.TapeBound.TapeAction,  () => AsWav  (x.TapeBound.TapeAction )); });
            AssertProp(x => { if (value == Raw) AreEqual(x.TapeBound.Tape,        () => FromRaw(x.TapeBound.Tape       ));
                              if (value == Wav) AreEqual(x.TapeBound.Tape,        () => FromWav(x.TapeBound.Tape       )); });
            AssertProp(x => { if (value == Raw) AreEqual(x.TapeBound.TapeConfig,  () => FromRaw(x.TapeBound.TapeConfig ));
                              if (value == Wav) AreEqual(x.TapeBound.TapeConfig,  () => FromWav(x.TapeBound.TapeConfig )); });
            AssertProp(x => { if (value == Raw) AreEqual(x.TapeBound.TapeActions, () => FromRaw(x.TapeBound.TapeActions));
                              if (value == Wav) AreEqual(x.TapeBound.TapeActions, () => FromWav(x.TapeBound.TapeActions)); });
            AssertProp(x => { if (value == Raw) AreEqual(x.TapeBound.TapeAction,  () => FromRaw(x.TapeBound.TapeAction ));
                              if (value == Wav) AreEqual(x.TapeBound.TapeAction,  () => FromWav(x.TapeBound.TapeAction )); });
            AssertProp(x => { if (value == Raw) AreEqual(x.TapeBound.Tape,        () => ToRaw  (x.TapeBound.Tape       ));
                              if (value == Wav) AreEqual(x.TapeBound.Tape,        () => ToWav  (x.TapeBound.Tape       )); });
            AssertProp(x => { if (value == Raw) AreEqual(x.TapeBound.TapeConfig,  () => ToRaw  (x.TapeBound.TapeConfig ));
                              if (value == Wav) AreEqual(x.TapeBound.TapeConfig,  () => ToWav  (x.TapeBound.TapeConfig )); });
            AssertProp(x => { if (value == Raw) AreEqual(x.TapeBound.TapeActions, () => ToRaw  (x.TapeBound.TapeActions));
                              if (value == Wav) AreEqual(x.TapeBound.TapeActions, () => ToWav  (x.TapeBound.TapeActions)); });
            AssertProp(x => { if (value == Raw) AreEqual(x.TapeBound.TapeAction,  () => ToRaw  (x.TapeBound.TapeAction ));
                              if (value == Wav) AreEqual(x.TapeBound.TapeAction,  () => ToWav  (x.TapeBound.TapeAction )); });
            AssertProp(x => { if (value == Raw) AreEqual(x.TapeBound.Tape,        () => SetRaw (x.TapeBound.Tape       ));
                              if (value == Wav) AreEqual(x.TapeBound.Tape,        () => SetWav (x.TapeBound.Tape       )); });
            AssertProp(x => { if (value == Raw) AreEqual(x.TapeBound.TapeConfig,  () => SetRaw (x.TapeBound.TapeConfig ));
                              if (value == Wav) AreEqual(x.TapeBound.TapeConfig,  () => SetWav (x.TapeBound.TapeConfig )); });
            AssertProp(x => { if (value == Raw) AreEqual(x.TapeBound.TapeActions, () => SetRaw (x.TapeBound.TapeActions));
                              if (value == Wav) AreEqual(x.TapeBound.TapeActions, () => SetWav (x.TapeBound.TapeActions)); });
            AssertProp(x => { if (value == Raw) AreEqual(x.TapeBound.TapeAction,  () => SetRaw (x.TapeBound.TapeAction ));
                              if (value == Wav) AreEqual(x.TapeBound.TapeAction,  () => SetWav (x.TapeBound.TapeAction )); });
            AssertProp(x => { if (value == Raw) AreEqual(x.TapeBound.Tape,        () => ConfigWishes.WithRaw(x.TapeBound.Tape       ));
                              if (value == Wav) AreEqual(x.TapeBound.Tape,        () => ConfigWishes.WithWav(x.TapeBound.Tape       )); });
            AssertProp(x => { if (value == Raw) AreEqual(x.TapeBound.TapeConfig,  () => ConfigWishes.WithRaw(x.TapeBound.TapeConfig ));
                              if (value == Wav) AreEqual(x.TapeBound.TapeConfig,  () => ConfigWishes.WithWav(x.TapeBound.TapeConfig )); });
            AssertProp(x => { if (value == Raw) AreEqual(x.TapeBound.TapeActions, () => ConfigWishes.WithRaw(x.TapeBound.TapeActions));
                              if (value == Wav) AreEqual(x.TapeBound.TapeActions, () => ConfigWishes.WithWav(x.TapeBound.TapeActions)); });
            AssertProp(x => { if (value == Raw) AreEqual(x.TapeBound.TapeAction,  () => ConfigWishes.WithRaw(x.TapeBound.TapeAction ));
                              if (value == Wav) AreEqual(x.TapeBound.TapeAction,  () => ConfigWishes.WithWav(x.TapeBound.TapeAction )); });
            AssertProp(x => { if (value == Raw) AreEqual(x.TapeBound.Tape,        () => ConfigWishes.AsRaw  (x.TapeBound.Tape       ));
                              if (value == Wav) AreEqual(x.TapeBound.Tape,        () => ConfigWishes.AsWav  (x.TapeBound.Tape       )); });
            AssertProp(x => { if (value == Raw) AreEqual(x.TapeBound.TapeConfig,  () => ConfigWishes.AsRaw  (x.TapeBound.TapeConfig ));
                              if (value == Wav) AreEqual(x.TapeBound.TapeConfig,  () => ConfigWishes.AsWav  (x.TapeBound.TapeConfig )); });
            AssertProp(x => { if (value == Raw) AreEqual(x.TapeBound.TapeActions, () => ConfigWishes.AsRaw  (x.TapeBound.TapeActions));
                              if (value == Wav) AreEqual(x.TapeBound.TapeActions, () => ConfigWishes.AsWav  (x.TapeBound.TapeActions)); });
            AssertProp(x => { if (value == Raw) AreEqual(x.TapeBound.TapeAction,  () => ConfigWishes.AsRaw  (x.TapeBound.TapeAction ));
                              if (value == Wav) AreEqual(x.TapeBound.TapeAction,  () => ConfigWishes.AsWav  (x.TapeBound.TapeAction )); });
            AssertProp(x => { if (value == Raw) AreEqual(x.TapeBound.Tape,        () => ConfigWishes.FromRaw(x.TapeBound.Tape       ));
                              if (value == Wav) AreEqual(x.TapeBound.Tape,        () => ConfigWishes.FromWav(x.TapeBound.Tape       )); });
            AssertProp(x => { if (value == Raw) AreEqual(x.TapeBound.TapeConfig,  () => ConfigWishes.FromRaw(x.TapeBound.TapeConfig ));
                              if (value == Wav) AreEqual(x.TapeBound.TapeConfig,  () => ConfigWishes.FromWav(x.TapeBound.TapeConfig )); });
            AssertProp(x => { if (value == Raw) AreEqual(x.TapeBound.TapeActions, () => ConfigWishes.FromRaw(x.TapeBound.TapeActions));
                              if (value == Wav) AreEqual(x.TapeBound.TapeActions, () => ConfigWishes.FromWav(x.TapeBound.TapeActions)); });
            AssertProp(x => { if (value == Raw) AreEqual(x.TapeBound.TapeAction,  () => ConfigWishes.FromRaw(x.TapeBound.TapeAction ));
                              if (value == Wav) AreEqual(x.TapeBound.TapeAction,  () => ConfigWishes.FromWav(x.TapeBound.TapeAction )); });
            AssertProp(x => { if (value == Raw) AreEqual(x.TapeBound.Tape,        () => ConfigWishes.ToRaw  (x.TapeBound.Tape       ));
                              if (value == Wav) AreEqual(x.TapeBound.Tape,        () => ConfigWishes.ToWav  (x.TapeBound.Tape       )); });
            AssertProp(x => { if (value == Raw) AreEqual(x.TapeBound.TapeConfig,  () => ConfigWishes.ToRaw  (x.TapeBound.TapeConfig ));
                              if (value == Wav) AreEqual(x.TapeBound.TapeConfig,  () => ConfigWishes.ToWav  (x.TapeBound.TapeConfig )); });
            AssertProp(x => { if (value == Raw) AreEqual(x.TapeBound.TapeActions, () => ConfigWishes.ToRaw  (x.TapeBound.TapeActions));
                              if (value == Wav) AreEqual(x.TapeBound.TapeActions, () => ConfigWishes.ToWav  (x.TapeBound.TapeActions)); });
            AssertProp(x => { if (value == Raw) AreEqual(x.TapeBound.TapeAction,  () => ConfigWishes.ToRaw  (x.TapeBound.TapeAction ));
                              if (value == Wav) AreEqual(x.TapeBound.TapeAction,  () => ConfigWishes.ToWav  (x.TapeBound.TapeAction )); });
            AssertProp(x => { if (value == Raw) AreEqual(x.TapeBound.Tape,        () => ConfigWishes.SetRaw (x.TapeBound.Tape       ));
                              if (value == Wav) AreEqual(x.TapeBound.Tape,        () => ConfigWishes.SetWav (x.TapeBound.Tape       )); });
            AssertProp(x => { if (value == Raw) AreEqual(x.TapeBound.TapeConfig,  () => ConfigWishes.SetRaw (x.TapeBound.TapeConfig ));
                              if (value == Wav) AreEqual(x.TapeBound.TapeConfig,  () => ConfigWishes.SetWav (x.TapeBound.TapeConfig )); });
            AssertProp(x => { if (value == Raw) AreEqual(x.TapeBound.TapeActions, () => ConfigWishes.SetRaw (x.TapeBound.TapeActions));
                              if (value == Wav) AreEqual(x.TapeBound.TapeActions, () => ConfigWishes.SetWav (x.TapeBound.TapeActions)); });
            AssertProp(x => { if (value == Raw) AreEqual(x.TapeBound.TapeAction,  () => ConfigWishes.SetRaw (x.TapeBound.TapeAction ));
                              if (value == Wav) AreEqual(x.TapeBound.TapeAction,  () => ConfigWishes.SetWav (x.TapeBound.TapeAction )); });
        }
        
        [TestMethod]
        [DynamicData(nameof(TestParameters))]
        public void BuffBound_AudioFormat(int initAsInt, int valueAsInt)
        {
            var init  = (AudioFileFormatEnum)initAsInt;
            var value = (AudioFileFormatEnum)valueAsInt;
            
            void AssertProp(Action<ConfigTestEntities> setter)
            {
                var x = CreateTestEntities(init);
                Assert_All_Getters(x, init);
                
                setter(x);
                
                Assert_SynthBound_Getters(x, init);
                Assert_TapeBound_Getters(x, init);
                Assert_BuffBound_Getters(x, value);
                Assert_Independent_Getters(x, init);
                Assert_Immutable_Getters(x, init);
                
                x.Record();
                Assert_All_Getters(x, init);
            }

            AssertProp(x => AreEqual(x.BuffBound.Buff,            () => x.BuffBound.Buff           .AudioFormat(value, x.SynthBound.Context)));
            AssertProp(x => AreEqual(x.BuffBound.AudioFileOutput, () => x.BuffBound.AudioFileOutput.AudioFormat(value, x.SynthBound.Context)));
            AssertProp(x => AreEqual(x.BuffBound.Buff,            () => x.BuffBound.Buff           .WithAudioFormat(value, x.SynthBound.Context)));
            AssertProp(x => AreEqual(x.BuffBound.AudioFileOutput, () => x.BuffBound.AudioFileOutput.WithAudioFormat(value, x.SynthBound.Context)));
            AssertProp(x => AreEqual(x.BuffBound.Buff,            () => x.BuffBound.Buff           .AsAudioFormat(value, x.SynthBound.Context)));
            AssertProp(x => AreEqual(x.BuffBound.AudioFileOutput, () => x.BuffBound.AudioFileOutput.AsAudioFormat(value, x.SynthBound.Context)));
            AssertProp(x => AreEqual(x.BuffBound.Buff,            () => x.BuffBound.Buff           .FromAudioFormat(value, x.SynthBound.Context))); // By Design: AudioFileOutput has no From prefix.
            AssertProp(x => AreEqual(x.BuffBound.Buff,            () => x.BuffBound.Buff           .ToAudioFormat(value, x.SynthBound.Context)));
            AssertProp(x => AreEqual(x.BuffBound.AudioFileOutput, () => x.BuffBound.AudioFileOutput.ToAudioFormat(value, x.SynthBound.Context)));
            AssertProp(x => AreEqual(x.BuffBound.Buff,            () => x.BuffBound.Buff           .SetAudioFormat(value, x.SynthBound.Context)));
            AssertProp(x => AreEqual(x.BuffBound.AudioFileOutput, () => x.BuffBound.AudioFileOutput.SetAudioFormat(value, x.SynthBound.Context)));
            AssertProp(x => AreEqual(x.BuffBound.Buff,            () => AudioFormat    (x.BuffBound.Buff,            value, x.SynthBound.Context)));
            AssertProp(x => AreEqual(x.BuffBound.AudioFileOutput, () => AudioFormat    (x.BuffBound.AudioFileOutput, value, x.SynthBound.Context)));
            AssertProp(x => AreEqual(x.BuffBound.Buff,            () => WithAudioFormat(x.BuffBound.Buff,            value, x.SynthBound.Context)));
            AssertProp(x => AreEqual(x.BuffBound.AudioFileOutput, () => WithAudioFormat(x.BuffBound.AudioFileOutput, value, x.SynthBound.Context)));
            AssertProp(x => AreEqual(x.BuffBound.Buff,            () => AsAudioFormat  (x.BuffBound.Buff,            value, x.SynthBound.Context)));
            AssertProp(x => AreEqual(x.BuffBound.AudioFileOutput, () => AsAudioFormat  (x.BuffBound.AudioFileOutput, value, x.SynthBound.Context)));
            AssertProp(x => AreEqual(x.BuffBound.Buff,            () => FromAudioFormat(x.BuffBound.Buff,            value, x.SynthBound.Context))); // By Design: AudioFileOutput has no From prefix.
            AssertProp(x => AreEqual(x.BuffBound.Buff,            () => ToAudioFormat  (x.BuffBound.Buff,            value, x.SynthBound.Context)));
            AssertProp(x => AreEqual(x.BuffBound.AudioFileOutput, () => ToAudioFormat  (x.BuffBound.AudioFileOutput, value, x.SynthBound.Context)));
            AssertProp(x => AreEqual(x.BuffBound.Buff,            () => SetAudioFormat (x.BuffBound.Buff,            value, x.SynthBound.Context)));
            AssertProp(x => AreEqual(x.BuffBound.AudioFileOutput, () => SetAudioFormat (x.BuffBound.AudioFileOutput, value, x.SynthBound.Context)));
            AssertProp(x => AreEqual(x.BuffBound.Buff,            () => ConfigWishes.AudioFormat    (x.BuffBound.Buff,            value, x.SynthBound.Context)));
            AssertProp(x => AreEqual(x.BuffBound.AudioFileOutput, () => ConfigWishes.AudioFormat    (x.BuffBound.AudioFileOutput, value, x.SynthBound.Context)));
            AssertProp(x => AreEqual(x.BuffBound.Buff,            () => ConfigWishes.WithAudioFormat(x.BuffBound.Buff,            value, x.SynthBound.Context)));
            AssertProp(x => AreEqual(x.BuffBound.AudioFileOutput, () => ConfigWishes.WithAudioFormat(x.BuffBound.AudioFileOutput, value, x.SynthBound.Context)));
            AssertProp(x => AreEqual(x.BuffBound.Buff,            () => ConfigWishes.AsAudioFormat  (x.BuffBound.Buff,            value, x.SynthBound.Context)));
            AssertProp(x => AreEqual(x.BuffBound.AudioFileOutput, () => ConfigWishes.AsAudioFormat  (x.BuffBound.AudioFileOutput, value, x.SynthBound.Context)));
            AssertProp(x => AreEqual(x.BuffBound.Buff,            () => ConfigWishes.FromAudioFormat(x.BuffBound.Buff,            value, x.SynthBound.Context))); // By Design: AudioFileOutput has no From prefix.
            AssertProp(x => AreEqual(x.BuffBound.Buff,            () => ConfigWishes.ToAudioFormat  (x.BuffBound.Buff,            value, x.SynthBound.Context)));
            AssertProp(x => AreEqual(x.BuffBound.AudioFileOutput, () => ConfigWishes.ToAudioFormat  (x.BuffBound.AudioFileOutput, value, x.SynthBound.Context)));
            AssertProp(x => AreEqual(x.BuffBound.Buff,            () => ConfigWishes.SetAudioFormat (x.BuffBound.Buff,            value, x.SynthBound.Context)));
            AssertProp(x => AreEqual(x.BuffBound.AudioFileOutput, () => ConfigWishes.SetAudioFormat (x.BuffBound.AudioFileOutput, value, x.SynthBound.Context)));
            AssertProp(x => { if (value == Raw) AreEqual(x.BuffBound.Buff,            () => x.BuffBound.Buff           .WithRaw(x.SynthBound.Context));
                              if (value == Wav) AreEqual(x.BuffBound.Buff,            () => x.BuffBound.Buff           .WithWav(x.SynthBound.Context)); });
            AssertProp(x => { if (value == Raw) AreEqual(x.BuffBound.AudioFileOutput, () => x.BuffBound.AudioFileOutput.WithRaw(x.SynthBound.Context));
                              if (value == Wav) AreEqual(x.BuffBound.AudioFileOutput, () => x.BuffBound.AudioFileOutput.WithWav(x.SynthBound.Context)); });
            AssertProp(x => { if (value == Raw) AreEqual(x.BuffBound.Buff,            () => x.BuffBound.Buff           .AsRaw  (x.SynthBound.Context));
                              if (value == Wav) AreEqual(x.BuffBound.Buff,            () => x.BuffBound.Buff           .AsWav  (x.SynthBound.Context)); });
            AssertProp(x => { if (value == Raw) AreEqual(x.BuffBound.AudioFileOutput, () => x.BuffBound.AudioFileOutput.AsRaw  (x.SynthBound.Context));
                              if (value == Wav) AreEqual(x.BuffBound.AudioFileOutput, () => x.BuffBound.AudioFileOutput.AsWav  (x.SynthBound.Context)); });
            AssertProp(x => { if (value == Raw) AreEqual(x.BuffBound.Buff,            () => x.BuffBound.Buff           .FromRaw(x.SynthBound.Context));     // By Design: AudioFileOutput has no From prefix.
                              if (value == Wav) AreEqual(x.BuffBound.Buff,            () => x.BuffBound.Buff           .FromWav(x.SynthBound.Context)); }); // By Design: AudioFileOutput has no From prefix.
            AssertProp(x => { if (value == Raw) AreEqual(x.BuffBound.Buff,            () => x.BuffBound.Buff           .ToRaw  (x.SynthBound.Context));
                              if (value == Wav) AreEqual(x.BuffBound.Buff,            () => x.BuffBound.Buff           .ToWav  (x.SynthBound.Context)); });
            AssertProp(x => { if (value == Raw) AreEqual(x.BuffBound.AudioFileOutput, () => x.BuffBound.AudioFileOutput.ToRaw  (x.SynthBound.Context));
                              if (value == Wav) AreEqual(x.BuffBound.AudioFileOutput, () => x.BuffBound.AudioFileOutput.ToWav  (x.SynthBound.Context)); });
            AssertProp(x => { if (value == Raw) AreEqual(x.BuffBound.Buff,            () => x.BuffBound.Buff           .SetRaw (x.SynthBound.Context));
                              if (value == Wav) AreEqual(x.BuffBound.Buff,            () => x.BuffBound.Buff           .SetWav (x.SynthBound.Context)); });
            AssertProp(x => { if (value == Raw) AreEqual(x.BuffBound.AudioFileOutput, () => x.BuffBound.AudioFileOutput.SetRaw (x.SynthBound.Context));
                              if (value == Wav) AreEqual(x.BuffBound.AudioFileOutput, () => x.BuffBound.AudioFileOutput.SetWav (x.SynthBound.Context)); });
            AssertProp(x => { if (value == Raw) AreEqual(x.BuffBound.Buff,            () => WithRaw(x.BuffBound.Buff           , x.SynthBound.Context));
                              if (value == Wav) AreEqual(x.BuffBound.Buff,            () => WithWav(x.BuffBound.Buff           , x.SynthBound.Context)); });
            AssertProp(x => { if (value == Raw) AreEqual(x.BuffBound.AudioFileOutput, () => WithRaw(x.BuffBound.AudioFileOutput, x.SynthBound.Context));
                              if (value == Wav) AreEqual(x.BuffBound.AudioFileOutput, () => WithWav(x.BuffBound.AudioFileOutput, x.SynthBound.Context)); });
            AssertProp(x => { if (value == Raw) AreEqual(x.BuffBound.Buff,            () => AsRaw  (x.BuffBound.Buff           , x.SynthBound.Context));
                              if (value == Wav) AreEqual(x.BuffBound.Buff,            () => AsWav  (x.BuffBound.Buff           , x.SynthBound.Context)); });
            AssertProp(x => { if (value == Raw) AreEqual(x.BuffBound.AudioFileOutput, () => AsRaw  (x.BuffBound.AudioFileOutput, x.SynthBound.Context));
                              if (value == Wav) AreEqual(x.BuffBound.AudioFileOutput, () => AsWav  (x.BuffBound.AudioFileOutput, x.SynthBound.Context)); });
            AssertProp(x => { if (value == Raw) AreEqual(x.BuffBound.Buff,            () => FromRaw(x.BuffBound.Buff           , x.SynthBound.Context));     // By Design: AudioFileOutput has no From prefix.
                              if (value == Wav) AreEqual(x.BuffBound.Buff,            () => FromWav(x.BuffBound.Buff           , x.SynthBound.Context)); }); // By Design: AudioFileOutput has no From prefix.
            AssertProp(x => { if (value == Raw) AreEqual(x.BuffBound.Buff,            () => ToRaw  (x.BuffBound.Buff           , x.SynthBound.Context));
                              if (value == Wav) AreEqual(x.BuffBound.Buff,            () => ToWav  (x.BuffBound.Buff           , x.SynthBound.Context)); });
            AssertProp(x => { if (value == Raw) AreEqual(x.BuffBound.AudioFileOutput, () => ToRaw  (x.BuffBound.AudioFileOutput, x.SynthBound.Context));
                              if (value == Wav) AreEqual(x.BuffBound.AudioFileOutput, () => ToWav  (x.BuffBound.AudioFileOutput, x.SynthBound.Context)); });
            AssertProp(x => { if (value == Raw) AreEqual(x.BuffBound.Buff,            () => SetRaw (x.BuffBound.Buff           , x.SynthBound.Context));
                              if (value == Wav) AreEqual(x.BuffBound.Buff,            () => SetWav (x.BuffBound.Buff           , x.SynthBound.Context)); });
            AssertProp(x => { if (value == Raw) AreEqual(x.BuffBound.AudioFileOutput, () => SetRaw (x.BuffBound.AudioFileOutput, x.SynthBound.Context));
                              if (value == Wav) AreEqual(x.BuffBound.AudioFileOutput, () => SetWav (x.BuffBound.AudioFileOutput, x.SynthBound.Context)); });
            AssertProp(x => { if (value == Raw) AreEqual(x.BuffBound.Buff,            () => ConfigWishes.WithRaw(x.BuffBound.Buff           , x.SynthBound.Context));
                              if (value == Wav) AreEqual(x.BuffBound.Buff,            () => ConfigWishes.WithWav(x.BuffBound.Buff           , x.SynthBound.Context)); });
            AssertProp(x => { if (value == Raw) AreEqual(x.BuffBound.AudioFileOutput, () => ConfigWishes.WithRaw(x.BuffBound.AudioFileOutput, x.SynthBound.Context));
                              if (value == Wav) AreEqual(x.BuffBound.AudioFileOutput, () => ConfigWishes.WithWav(x.BuffBound.AudioFileOutput, x.SynthBound.Context)); });
            AssertProp(x => { if (value == Raw) AreEqual(x.BuffBound.Buff,            () => ConfigWishes.AsRaw  (x.BuffBound.Buff           , x.SynthBound.Context));
                              if (value == Wav) AreEqual(x.BuffBound.Buff,            () => ConfigWishes.AsWav  (x.BuffBound.Buff           , x.SynthBound.Context)); });
            AssertProp(x => { if (value == Raw) AreEqual(x.BuffBound.AudioFileOutput, () => ConfigWishes.AsRaw  (x.BuffBound.AudioFileOutput, x.SynthBound.Context));
                              if (value == Wav) AreEqual(x.BuffBound.AudioFileOutput, () => ConfigWishes.AsWav  (x.BuffBound.AudioFileOutput, x.SynthBound.Context)); });
            AssertProp(x => { if (value == Raw) AreEqual(x.BuffBound.Buff,            () => ConfigWishes.FromRaw(x.BuffBound.Buff           , x.SynthBound.Context));     // By Design: AudioFileOutput has no From prefix.
                              if (value == Wav) AreEqual(x.BuffBound.Buff,            () => ConfigWishes.FromWav(x.BuffBound.Buff           , x.SynthBound.Context)); }); // By Design: AudioFileOutput has no From prefix.
            AssertProp(x => { if (value == Raw) AreEqual(x.BuffBound.Buff,            () => ConfigWishes.ToRaw  (x.BuffBound.Buff           , x.SynthBound.Context));
                              if (value == Wav) AreEqual(x.BuffBound.Buff,            () => ConfigWishes.ToWav  (x.BuffBound.Buff           , x.SynthBound.Context)); });
            AssertProp(x => { if (value == Raw) AreEqual(x.BuffBound.AudioFileOutput, () => ConfigWishes.ToRaw  (x.BuffBound.AudioFileOutput, x.SynthBound.Context));
                              if (value == Wav) AreEqual(x.BuffBound.AudioFileOutput, () => ConfigWishes.ToWav  (x.BuffBound.AudioFileOutput, x.SynthBound.Context)); });
            AssertProp(x => { if (value == Raw) AreEqual(x.BuffBound.Buff,            () => ConfigWishes.SetRaw (x.BuffBound.Buff           , x.SynthBound.Context));
                              if (value == Wav) AreEqual(x.BuffBound.Buff,            () => ConfigWishes.SetWav (x.BuffBound.Buff           , x.SynthBound.Context)); });
            AssertProp(x => { if (value == Raw) AreEqual(x.BuffBound.AudioFileOutput, () => ConfigWishes.SetRaw (x.BuffBound.AudioFileOutput, x.SynthBound.Context));
                              if (value == Wav) AreEqual(x.BuffBound.AudioFileOutput, () => ConfigWishes.SetWav (x.BuffBound.AudioFileOutput, x.SynthBound.Context)); });
        }

        [TestMethod]
        [DynamicData(nameof(TestParameters))]
        public void Independent_AudioFormat(int initAsInt, int valueAsInt)
        {
            // Independent after Taping
            
            var init  = (AudioFileFormatEnum)initAsInt;
            var value = (AudioFileFormatEnum)valueAsInt;

            // Sample
            {
                ConfigTestEntities x = default;

                void AssertProp(Action setter)
                {
                    x = CreateTestEntities(init);
                    Assert_All_Getters(x, init);
                    
                    setter();
                    
                    Assert_Bound_Getters(x, init);
                    Assert_Independent_Getters(x.Independent.Sample, value);
                    Assert_Immutable_Getters(x, init);

                    x.Record();
                    Assert_All_Getters(x, init);
                }

                AssertProp(() => AreEqual(x.Independent.Sample, () => x.Independent.Sample.AudioFormat    (value, x.SynthBound.Context)));
                AssertProp(() => AreEqual(x.Independent.Sample, () => x.Independent.Sample.WithAudioFormat(value, x.SynthBound.Context)));
                AssertProp(() => AreEqual(x.Independent.Sample, () => x.Independent.Sample.AsAudioFormat  (value, x.SynthBound.Context)));
                AssertProp(() => AreEqual(x.Independent.Sample, () => x.Independent.Sample.FromAudioFormat(value, x.SynthBound.Context)));
                AssertProp(() => AreEqual(x.Independent.Sample, () => x.Independent.Sample.ToAudioFormat  (value, x.SynthBound.Context)));
                AssertProp(() => AreEqual(x.Independent.Sample, () => x.Independent.Sample.SetAudioFormat (value, x.SynthBound.Context)));
                AssertProp(() => AreEqual(x.Independent.Sample, () => AudioFormat    (x.Independent.Sample, value, x.SynthBound.Context)));
                AssertProp(() => AreEqual(x.Independent.Sample, () => WithAudioFormat(x.Independent.Sample, value, x.SynthBound.Context)));
                AssertProp(() => AreEqual(x.Independent.Sample, () => AsAudioFormat  (x.Independent.Sample, value, x.SynthBound.Context)));
                AssertProp(() => AreEqual(x.Independent.Sample, () => FromAudioFormat(x.Independent.Sample, value, x.SynthBound.Context)));
                AssertProp(() => AreEqual(x.Independent.Sample, () => ToAudioFormat  (x.Independent.Sample, value, x.SynthBound.Context)));
                AssertProp(() => AreEqual(x.Independent.Sample, () => SetAudioFormat (x.Independent.Sample, value, x.SynthBound.Context)));
                AssertProp(() => AreEqual(x.Independent.Sample, () => ConfigWishes.AudioFormat    (x.Independent.Sample, value, x.SynthBound.Context)));
                AssertProp(() => AreEqual(x.Independent.Sample, () => ConfigWishes.WithAudioFormat(x.Independent.Sample, value, x.SynthBound.Context)));
                AssertProp(() => AreEqual(x.Independent.Sample, () => ConfigWishes.AsAudioFormat  (x.Independent.Sample, value, x.SynthBound.Context)));
                AssertProp(() => AreEqual(x.Independent.Sample, () => ConfigWishes.FromAudioFormat(x.Independent.Sample, value, x.SynthBound.Context)));
                AssertProp(() => AreEqual(x.Independent.Sample, () => ConfigWishes.ToAudioFormat  (x.Independent.Sample, value, x.SynthBound.Context)));
                AssertProp(() => AreEqual(x.Independent.Sample, () => ConfigWishes.SetAudioFormat (x.Independent.Sample, value, x.SynthBound.Context)));
                AssertProp(() => { if (value == Raw) AreEqual(x.Independent.Sample, () => x.Independent.Sample.WithRaw(x.SynthBound.Context));
                                   if (value == Wav) AreEqual(x.Independent.Sample, () => x.Independent.Sample.WithWav(x.SynthBound.Context)); });
                AssertProp(() => { if (value == Raw) AreEqual(x.Independent.Sample, () => x.Independent.Sample.AsRaw  (x.SynthBound.Context));
                                   if (value == Wav) AreEqual(x.Independent.Sample, () => x.Independent.Sample.AsWav  (x.SynthBound.Context)); });
                AssertProp(() => { if (value == Raw) AreEqual(x.Independent.Sample, () => x.Independent.Sample.FromRaw(x.SynthBound.Context));
                                   if (value == Wav) AreEqual(x.Independent.Sample, () => x.Independent.Sample.FromWav(x.SynthBound.Context)); });
                AssertProp(() => { if (value == Raw) AreEqual(x.Independent.Sample, () => x.Independent.Sample.ToRaw  (x.SynthBound.Context));
                                   if (value == Wav) AreEqual(x.Independent.Sample, () => x.Independent.Sample.ToWav  (x.SynthBound.Context)); });
                AssertProp(() => { if (value == Raw) AreEqual(x.Independent.Sample, () => x.Independent.Sample.SetRaw (x.SynthBound.Context));
                                   if (value == Wav) AreEqual(x.Independent.Sample, () => x.Independent.Sample.SetWav (x.SynthBound.Context)); });
                AssertProp(() => { if (value == Raw) AreEqual(x.Independent.Sample, () => WithRaw(x.Independent.Sample, x.SynthBound.Context));
                                   if (value == Wav) AreEqual(x.Independent.Sample, () => WithWav(x.Independent.Sample, x.SynthBound.Context)); });
                AssertProp(() => { if (value == Raw) AreEqual(x.Independent.Sample, () => AsRaw  (x.Independent.Sample, x.SynthBound.Context));
                                   if (value == Wav) AreEqual(x.Independent.Sample, () => AsWav  (x.Independent.Sample, x.SynthBound.Context)); });
                AssertProp(() => { if (value == Raw) AreEqual(x.Independent.Sample, () => FromRaw(x.Independent.Sample, x.SynthBound.Context));
                                   if (value == Wav) AreEqual(x.Independent.Sample, () => FromWav(x.Independent.Sample, x.SynthBound.Context)); });
                AssertProp(() => { if (value == Raw) AreEqual(x.Independent.Sample, () => ToRaw  (x.Independent.Sample, x.SynthBound.Context));
                                   if (value == Wav) AreEqual(x.Independent.Sample, () => ToWav  (x.Independent.Sample, x.SynthBound.Context)); });
                AssertProp(() => { if (value == Raw) AreEqual(x.Independent.Sample, () => SetRaw (x.Independent.Sample, x.SynthBound.Context));
                                   if (value == Wav) AreEqual(x.Independent.Sample, () => SetWav (x.Independent.Sample, x.SynthBound.Context)); });
                AssertProp(() => { if (value == Raw) AreEqual(x.Independent.Sample, () => ConfigWishes.WithRaw(x.Independent.Sample, x.SynthBound.Context));
                                   if (value == Wav) AreEqual(x.Independent.Sample, () => ConfigWishes.WithWav(x.Independent.Sample, x.SynthBound.Context)); });
                AssertProp(() => { if (value == Raw) AreEqual(x.Independent.Sample, () => ConfigWishes.AsRaw  (x.Independent.Sample, x.SynthBound.Context));
                                   if (value == Wav) AreEqual(x.Independent.Sample, () => ConfigWishes.AsWav  (x.Independent.Sample, x.SynthBound.Context)); });
                AssertProp(() => { if (value == Raw) AreEqual(x.Independent.Sample, () => ConfigWishes.FromRaw(x.Independent.Sample, x.SynthBound.Context));
                                   if (value == Wav) AreEqual(x.Independent.Sample, () => ConfigWishes.FromWav(x.Independent.Sample, x.SynthBound.Context)); });
                AssertProp(() => { if (value == Raw) AreEqual(x.Independent.Sample, () => ConfigWishes.ToRaw  (x.Independent.Sample, x.SynthBound.Context));
                                   if (value == Wav) AreEqual(x.Independent.Sample, () => ConfigWishes.ToWav  (x.Independent.Sample, x.SynthBound.Context)); });
                AssertProp(() => { if (value == Raw) AreEqual(x.Independent.Sample, () => ConfigWishes.SetRaw (x.Independent.Sample, x.SynthBound.Context));
                                   if (value == Wav) AreEqual(x.Independent.Sample, () => ConfigWishes.SetWav (x.Independent.Sample, x.SynthBound.Context)); });
            }
        }
        
        [TestMethod] 
        [DynamicData(nameof(TestParameters))]
        public void Immutable_AudioFormat(int intAsInit, int intAsValue)
        {
            var init  = (AudioFileFormatEnum)intAsInit;
            var value = (AudioFileFormatEnum)intAsValue;
            var x = CreateTestEntities(init);

            // AudioFileFormatEnum
            
            var audioFormats = new List<AudioFileFormatEnum>();
            {
                void AssertProp(Func<AudioFileFormatEnum> setter)
                {
                    Assert_Immutable_Getters(x.Immutable.AudioFormat, init);
                    
                    AudioFileFormatEnum audioFormat2 = setter();
                    
                    Assert_Immutable_Getters(x.Immutable.AudioFormat, init);
                    Assert_Immutable_Getters(audioFormat2, value);
                    
                    audioFormats.Add(audioFormat2);
                }

                AssertProp(() => x.Immutable.AudioFormat.AudioFormat(value));
                AssertProp(() => value.AudioFormat());
                AssertProp(() => value == Raw ? x.Immutable.AudioFormat.AsRaw() : x.Immutable.AudioFormat.AsWav());
            }

            // AudioFormat Entity
            
            var audioFormatEntities = new List<AudioFileFormat>();
            {
                void AssertProp(Func<AudioFileFormat> setter)
                {
                    Assert_Immutable_Getters(x.Immutable.AudioFormatEntity, init);

                    AudioFileFormat audioFormatEntity2 = setter();
                    
                    Assert_Immutable_Getters(x.Immutable.AudioFormatEntity, init);
                    Assert_Immutable_Getters(audioFormatEntity2, value);
                    
                    audioFormatEntities.Add(audioFormatEntity2);
                }
                
                AssertProp(() => x.Immutable.AudioFormatEntity.AudioFormat(value, x.SynthBound.Context));
                AssertProp(() => value.ToEntity(x.SynthBound.Context));
                AssertProp(() => value == Raw ? x.Immutable.AudioFormatEntity.AsRaw(x.SynthBound.Context) : x.Immutable.AudioFormatEntity.AsWav(x.SynthBound.Context));
            }
            
            // FileExtension
            
            var fileExtensions = new List<string>();
            {
                void AssertProp(Func<string> setter)
                {
                    Assert_Immutable_Getters(x.Immutable.AudioFormatEntity, init);

                    string fileExtension2 = setter();
                    
                    Assert_Immutable_Getters(x.Immutable.FileExtension, init);
                    Assert_Immutable_Getters(fileExtension2, value);
                    
                    fileExtensions.Add(fileExtension2);
                }
                
                AssertProp(() => x.Immutable.FileExtension.AudioFormat(value));
                AssertProp(() => value == Raw ? x.Immutable.FileExtension.AsRaw() : x.Immutable.FileExtension.AsWav());
            }

            // After-Record
            x.Record();
            
            // All is reset
            Assert_All_Getters(x, init);
            
            // Except for our variables
            fileExtensions.ForEach(s => Assert_Immutable_Getters(s, value));
            audioFormats.ForEach(e => Assert_Immutable_Getters(e, value));
            audioFormatEntities.ForEach(s => Assert_Immutable_Getters(s, value));
        }

        [TestMethod]
        public void ConfigSection_AudioFormat()
        {
            var x = CreateTestEntities(default);
            var configSection = x.SynthBound.ConfigSection;
            
            // Extension Method Syntax
            AreEqual(DefaultAudioFormat == Raw, () => configSection.IsRaw());
            AreEqual(DefaultAudioFormat == Wav, () => configSection.IsWav());
            AreEqual(DefaultAudioFormat,        () => configSection.AudioFormat);
            AreEqual(DefaultAudioFormat,        () => configSection.AudioFormat());
            AreEqual(DefaultAudioFormat,        () => configSection.GetAudioFormat());

            // Using Static Syntax
            AreEqual(DefaultAudioFormat == Raw, () => IsRaw(configSection));
            AreEqual(DefaultAudioFormat == Wav, () => IsWav(configSection));
            AreEqual(DefaultAudioFormat,        () => AudioFormat(configSection));
            AreEqual(DefaultAudioFormat,        () => GetAudioFormat(configSection));

            // Static Syntax
            AreEqual(DefaultAudioFormat == Raw, () => ConfigWishesAccessor.IsRaw(configSection));
            AreEqual(DefaultAudioFormat == Wav, () => ConfigWishesAccessor.IsWav(configSection));
            AreEqual(DefaultAudioFormat,        () => ConfigWishesAccessor.AudioFormat(configSection));
            AreEqual(DefaultAudioFormat,        () => ConfigWishesAccessor.GetAudioFormat(configSection));
        }

        // Getter Helpers
        
        private void Assert_All_Getters(ConfigTestEntities x, AudioFileFormatEnum audioFormat)
        {
            Assert_Bound_Getters(x, audioFormat);
            Assert_Independent_Getters(x, audioFormat);
            Assert_Immutable_Getters(x, audioFormat);
        }

        private void Assert_Bound_Getters(ConfigTestEntities x, AudioFileFormatEnum audioFormat)
        {
            Assert_SynthBound_Getters(x, audioFormat);
            Assert_TapeBound_Getters(x, audioFormat);
            Assert_BuffBound_Getters(x, audioFormat);
        }
        
        private void Assert_Independent_Getters(ConfigTestEntities x, AudioFileFormatEnum audioFormat)
        {
            Assert_Independent_Getters(x.Independent.Sample, audioFormat);
        }

        private void Assert_Immutable_Getters(ConfigTestEntities x, AudioFileFormatEnum audioFormat)
        {
            Assert_Immutable_Getters(x.Immutable.WavHeader, audioFormat);
            Assert_Immutable_Getters(x.Immutable.FileExtension, audioFormat);
            Assert_Immutable_Getters(x.Immutable.AudioFormat, audioFormat);
            Assert_Immutable_Getters(x.Immutable.AudioFormatEntity, audioFormat);
        }

        private void Assert_SynthBound_Getters(ConfigTestEntities x, AudioFileFormatEnum audioFormat)
        {
            // Property Syntax
            AreEqual(audioFormat == Wav, () => x.SynthBound.SynthWishes.IsWav);
            AreEqual(audioFormat == Wav, () => x.SynthBound.FlowNode.IsWav);
            AreEqual(audioFormat == Raw, () => x.SynthBound.SynthWishes.IsRaw);
            AreEqual(audioFormat == Raw, () => x.SynthBound.FlowNode.IsRaw);
            AreEqual(audioFormat,        () => x.SynthBound.SynthWishes.GetAudioFormat);
            AreEqual(audioFormat,        () => x.SynthBound.FlowNode.GetAudioFormat);
            AreEqual(audioFormat,        () => x.SynthBound.ConfigResolver.GetAudioFormat);
            
            // Extension Method Syntax
            AreEqual(audioFormat == Wav, () => x.SynthBound.SynthWishes.IsWav());
            AreEqual(audioFormat == Wav, () => x.SynthBound.FlowNode.IsWav());
            AreEqual(audioFormat == Wav, () => x.SynthBound.ConfigResolver.IsWav);
            AreEqual(audioFormat == Wav, () => x.SynthBound.ConfigResolver.IsWav());
            AreEqual(audioFormat == Raw, () => x.SynthBound.SynthWishes.IsRaw());
            AreEqual(audioFormat == Raw, () => x.SynthBound.FlowNode.IsRaw());
            AreEqual(audioFormat == Raw, () => x.SynthBound.ConfigResolver.IsRaw);
            AreEqual(audioFormat == Raw, () => x.SynthBound.ConfigResolver.IsRaw());
            AreEqual(audioFormat,        () => x.SynthBound.SynthWishes.AudioFormat());
            AreEqual(audioFormat,        () => x.SynthBound.FlowNode.AudioFormat());
            AreEqual(audioFormat,        () => x.SynthBound.ConfigResolver.AudioFormat());
            AreEqual(audioFormat,        () => x.SynthBound.SynthWishes.GetAudioFormat());
            AreEqual(audioFormat,        () => x.SynthBound.FlowNode.GetAudioFormat());
            AreEqual(audioFormat,        () => x.SynthBound.ConfigResolver.GetAudioFormat());

            // Using Static Syntax
            AreEqual(audioFormat == Wav, () => IsWav(x.SynthBound.SynthWishes));
            AreEqual(audioFormat == Wav, () => IsWav(x.SynthBound.FlowNode));
            AreEqual(audioFormat == Wav, () => IsWav(x.SynthBound.ConfigResolver));
            AreEqual(audioFormat == Raw, () => IsRaw(x.SynthBound.SynthWishes));
            AreEqual(audioFormat == Raw, () => IsRaw(x.SynthBound.FlowNode));
            AreEqual(audioFormat == Raw, () => IsRaw(x.SynthBound.ConfigResolver));
            AreEqual(audioFormat,        () => AudioFormat(x.SynthBound.SynthWishes));
            AreEqual(audioFormat,        () => AudioFormat(x.SynthBound.FlowNode));
            AreEqual(audioFormat,        () => AudioFormat(x.SynthBound.ConfigResolver));
            AreEqual(audioFormat,        () => GetAudioFormat(x.SynthBound.SynthWishes));
            AreEqual(audioFormat,        () => GetAudioFormat(x.SynthBound.FlowNode));
            AreEqual(audioFormat,        () => GetAudioFormat(x.SynthBound.ConfigResolver));

            // Static Syntax
            AreEqual(audioFormat == Wav, () => ConfigWishes        .IsWav(x.SynthBound.SynthWishes));
            AreEqual(audioFormat == Wav, () => ConfigWishes        .IsWav(x.SynthBound.FlowNode));
            AreEqual(audioFormat == Wav, () => ConfigWishesAccessor.IsWav(x.SynthBound.ConfigResolver));
            AreEqual(audioFormat == Raw, () => ConfigWishes        .IsRaw(x.SynthBound.SynthWishes));
            AreEqual(audioFormat == Raw, () => ConfigWishes        .IsRaw(x.SynthBound.FlowNode));
            AreEqual(audioFormat == Raw, () => ConfigWishesAccessor.IsRaw(x.SynthBound.ConfigResolver));
            AreEqual(audioFormat,        () => ConfigWishes        .AudioFormat(x.SynthBound.SynthWishes));
            AreEqual(audioFormat,        () => ConfigWishes        .AudioFormat(x.SynthBound.FlowNode));
            AreEqual(audioFormat,        () => ConfigWishesAccessor.AudioFormat(x.SynthBound.ConfigResolver));
            AreEqual(audioFormat,        () => ConfigWishes        .GetAudioFormat(x.SynthBound.SynthWishes));
            AreEqual(audioFormat,        () => ConfigWishes        .GetAudioFormat(x.SynthBound.FlowNode));
            AreEqual(audioFormat,        () => ConfigWishesAccessor.GetAudioFormat(x.SynthBound.ConfigResolver));
        }

        private void Assert_TapeBound_Getters(ConfigTestEntities x, AudioFileFormatEnum audioFormat)
        {
            // Property Syntax
            AreEqual(audioFormat, () => x.TapeBound.TapeConfig.AudioFormat);

            // Extension Method Syntax
            AreEqual(audioFormat == Wav, () => x.TapeBound.Tape.IsWav());
            AreEqual(audioFormat == Wav, () => x.TapeBound.TapeConfig.IsWav());
            AreEqual(audioFormat == Wav, () => x.TapeBound.TapeActions.IsWav());
            AreEqual(audioFormat == Wav, () => x.TapeBound.TapeAction.IsWav());
            AreEqual(audioFormat == Raw, () => x.TapeBound.Tape.IsRaw());
            AreEqual(audioFormat == Raw, () => x.TapeBound.TapeConfig.IsRaw());
            AreEqual(audioFormat == Raw, () => x.TapeBound.TapeActions.IsRaw());
            AreEqual(audioFormat == Raw, () => x.TapeBound.TapeAction.IsRaw());
            AreEqual(audioFormat,        () => x.TapeBound.Tape.AudioFormat());
            AreEqual(audioFormat,        () => x.TapeBound.TapeConfig.AudioFormat());
            AreEqual(audioFormat,        () => x.TapeBound.TapeActions.AudioFormat());
            AreEqual(audioFormat,        () => x.TapeBound.TapeAction.AudioFormat());
            AreEqual(audioFormat,        () => x.TapeBound.Tape.GetAudioFormat());
            AreEqual(audioFormat,        () => x.TapeBound.TapeConfig.GetAudioFormat());
            AreEqual(audioFormat,        () => x.TapeBound.TapeActions.GetAudioFormat());
            AreEqual(audioFormat,        () => x.TapeBound.TapeAction.GetAudioFormat());
            
            // Using Static Syntax
            AreEqual(audioFormat == Wav, () => IsWav(x.TapeBound.Tape));
            AreEqual(audioFormat == Wav, () => IsWav(x.TapeBound.TapeConfig));
            AreEqual(audioFormat == Wav, () => IsWav(x.TapeBound.TapeActions));
            AreEqual(audioFormat == Wav, () => IsWav(x.TapeBound.TapeAction));
            AreEqual(audioFormat == Raw, () => IsRaw(x.TapeBound.Tape));
            AreEqual(audioFormat == Raw, () => IsRaw(x.TapeBound.TapeConfig));
            AreEqual(audioFormat == Raw, () => IsRaw(x.TapeBound.TapeActions));
            AreEqual(audioFormat == Raw, () => IsRaw(x.TapeBound.TapeAction));
            AreEqual(audioFormat,        () => AudioFormat(x.TapeBound.Tape));
            AreEqual(audioFormat,        () => AudioFormat(x.TapeBound.TapeConfig));
            AreEqual(audioFormat,        () => AudioFormat(x.TapeBound.TapeActions));
            AreEqual(audioFormat,        () => AudioFormat(x.TapeBound.TapeAction));
            AreEqual(audioFormat,        () => GetAudioFormat(x.TapeBound.Tape));
            AreEqual(audioFormat,        () => GetAudioFormat(x.TapeBound.TapeConfig));
            AreEqual(audioFormat,        () => GetAudioFormat(x.TapeBound.TapeActions));
            AreEqual(audioFormat,        () => GetAudioFormat(x.TapeBound.TapeAction));
            
            // Static Syntax
            AreEqual(audioFormat == Wav, () => ConfigWishes.IsWav(x.TapeBound.Tape));
            AreEqual(audioFormat == Wav, () => ConfigWishes.IsWav(x.TapeBound.TapeConfig));
            AreEqual(audioFormat == Wav, () => ConfigWishes.IsWav(x.TapeBound.TapeActions));
            AreEqual(audioFormat == Wav, () => ConfigWishes.IsWav(x.TapeBound.TapeAction));
            AreEqual(audioFormat == Raw, () => ConfigWishes.IsRaw(x.TapeBound.Tape));
            AreEqual(audioFormat == Raw, () => ConfigWishes.IsRaw(x.TapeBound.TapeConfig));
            AreEqual(audioFormat == Raw, () => ConfigWishes.IsRaw(x.TapeBound.TapeActions));
            AreEqual(audioFormat == Raw, () => ConfigWishes.IsRaw(x.TapeBound.TapeAction));
            AreEqual(audioFormat,        () => ConfigWishes.AudioFormat(x.TapeBound.Tape));
            AreEqual(audioFormat,        () => ConfigWishes.AudioFormat(x.TapeBound.TapeConfig));
            AreEqual(audioFormat,        () => ConfigWishes.AudioFormat(x.TapeBound.TapeActions));
            AreEqual(audioFormat,        () => ConfigWishes.AudioFormat(x.TapeBound.TapeAction));
            AreEqual(audioFormat,        () => ConfigWishes.GetAudioFormat(x.TapeBound.Tape));
            AreEqual(audioFormat,        () => ConfigWishes.GetAudioFormat(x.TapeBound.TapeConfig));
            AreEqual(audioFormat,        () => ConfigWishes.GetAudioFormat(x.TapeBound.TapeActions));
            AreEqual(audioFormat,        () => ConfigWishes.GetAudioFormat(x.TapeBound.TapeAction));
        }
                        
        private void Assert_BuffBound_Getters(ConfigTestEntities x, AudioFileFormatEnum audioFormat)
        {
            // Extension Method Syntax
            AreEqual(audioFormat == Wav, () => x.BuffBound.Buff.IsWav());
            AreEqual(audioFormat == Wav, () => x.BuffBound.AudioFileOutput.IsWav());
            AreEqual(audioFormat == Raw, () => x.BuffBound.Buff.IsRaw());
            AreEqual(audioFormat == Raw, () => x.BuffBound.AudioFileOutput.IsRaw());
            AreEqual(audioFormat,        () => x.BuffBound.Buff.AudioFormat());
            AreEqual(audioFormat,        () => x.BuffBound.AudioFileOutput.AudioFormat());
            AreEqual(audioFormat,        () => x.BuffBound.Buff.GetAudioFormat());
            AreEqual(audioFormat,        () => x.BuffBound.AudioFileOutput.GetAudioFormat());
            
            // Using Static Syntax
            AreEqual(audioFormat == Wav, () => IsWav(x.BuffBound.Buff));
            AreEqual(audioFormat == Wav, () => IsWav(x.BuffBound.AudioFileOutput));
            AreEqual(audioFormat == Raw, () => IsRaw(x.BuffBound.Buff));
            AreEqual(audioFormat == Raw, () => IsRaw(x.BuffBound.AudioFileOutput));
            AreEqual(audioFormat,        () => AudioFormat(x.BuffBound.Buff));
            AreEqual(audioFormat,        () => AudioFormat(x.BuffBound.AudioFileOutput));
            AreEqual(audioFormat,        () => AudioFormat(x.BuffBound.Buff));
            AreEqual(audioFormat,        () => AudioFormat(x.BuffBound.AudioFileOutput));
            
            // Static Syntax
            
            AreEqual(audioFormat == Wav, () => ConfigWishes.IsWav(x.BuffBound.Buff));
            AreEqual(audioFormat == Wav, () => ConfigWishes.IsWav(x.BuffBound.AudioFileOutput));
            AreEqual(audioFormat == Raw, () => ConfigWishes.IsRaw(x.BuffBound.Buff));
            AreEqual(audioFormat == Raw, () => ConfigWishes.IsRaw(x.BuffBound.AudioFileOutput));
            AreEqual(audioFormat,        () => ConfigWishes.AudioFormat(x.BuffBound.Buff));
            AreEqual(audioFormat,        () => ConfigWishes.AudioFormat(x.BuffBound.AudioFileOutput));
            AreEqual(audioFormat,        () => ConfigWishes.AudioFormat(x.BuffBound.Buff));
            AreEqual(audioFormat,        () => ConfigWishes.AudioFormat(x.BuffBound.AudioFileOutput));
        }

        private void Assert_Independent_Getters(Sample sample, AudioFileFormatEnum audioFormat)
        {
            // Extension Method Syntax
            AreEqual(audioFormat == Wav, () => sample.IsWav());
            AreEqual(audioFormat == Raw, () => sample.IsRaw());
            AreEqual(audioFormat,        () => sample.AudioFormat());
            AreEqual(audioFormat,        () => sample.GetAudioFormat());
            
            // Using Static Syntax
            AreEqual(audioFormat == Wav, () => IsWav(sample));
            AreEqual(audioFormat == Raw, () => IsRaw(sample));
            AreEqual(audioFormat,        () => AudioFormat(sample));
            AreEqual(audioFormat,        () => GetAudioFormat(sample));
            
            // Static Syntax
            AreEqual(audioFormat == Wav, () => ConfigWishes.IsWav(sample));
            AreEqual(audioFormat == Raw, () => ConfigWishes.IsRaw(sample));
            AreEqual(audioFormat,        () => ConfigWishes.AudioFormat(sample));
            AreEqual(audioFormat,        () => ConfigWishes.GetAudioFormat(sample));
        }
        
        private void Assert_Immutable_Getters(WavHeaderStruct wavHeader, AudioFileFormatEnum audioFormat)
        {
            if (audioFormat == Wav)
            {
                NotEqual(default, () => wavHeader);
                
                // Extension Method Syntax 
                IsTrue(               () => wavHeader.IsWav());
                AreEqual(audioFormat, () => wavHeader.AudioFormat());
                AreEqual(audioFormat, () => wavHeader.GetAudioFormat());

                // Using Static Syntax
                IsTrue(               () => IsWav(wavHeader));
                AreEqual(audioFormat, () => AudioFormat(wavHeader));
                AreEqual(audioFormat, () => GetAudioFormat(wavHeader));
                
                // Static Syntax
                IsTrue(               () => ConfigWishes.IsWav(wavHeader));
                AreEqual(audioFormat, () => ConfigWishes.AudioFormat(wavHeader));
                AreEqual(audioFormat, () => ConfigWishes.GetAudioFormat(wavHeader));
            }
            else
            {
                AreEqual(default, () => wavHeader);
                
                // Extension Method Syntax
                IsFalse(              () => wavHeader.IsRaw());
                NotEqual(audioFormat, () => wavHeader.AudioFormat());
                NotEqual(audioFormat, () => wavHeader.GetAudioFormat());

                // Using Static Syntax
                IsFalse(              () => IsRaw(wavHeader));
                NotEqual(audioFormat, () => AudioFormat(wavHeader));
                NotEqual(audioFormat, () => GetAudioFormat(wavHeader));
                
                // Static Syntax
                IsFalse(              () => ConfigWishes.IsRaw(wavHeader));
                NotEqual(audioFormat, () => ConfigWishes.AudioFormat(wavHeader));
                NotEqual(audioFormat, () => ConfigWishes.GetAudioFormat(wavHeader));
            }
        }
                 
        private void Assert_Immutable_Getters(string fileExtension, AudioFileFormatEnum audioFormat)
        {
            // Extension Method Syntax
            AreEqual(audioFormat == Wav, () => fileExtension.IsWav());
            AreEqual(audioFormat == Raw, () => fileExtension.IsRaw());
            AreEqual(audioFormat,        () => fileExtension.AudioFormat());
            AreEqual(audioFormat,        () => fileExtension.AsAudioFormat());
            AreEqual(audioFormat,        () => fileExtension.ToAudioFormat());
            AreEqual(audioFormat,        () => fileExtension.GetAudioFormat());

            // Using Static Syntax
            AreEqual(audioFormat == Wav, () => IsWav(fileExtension));
            AreEqual(audioFormat == Raw, () => IsRaw(fileExtension));
            AreEqual(audioFormat,        () => AudioFormat(fileExtension));
            AreEqual(audioFormat,        () => AsAudioFormat(fileExtension));
            AreEqual(audioFormat,        () => ToAudioFormat(fileExtension));
            AreEqual(audioFormat,        () => GetAudioFormat(fileExtension));

            // Static Syntax
            AreEqual(audioFormat == Wav, () => ConfigWishes.IsWav(fileExtension));
            AreEqual(audioFormat == Raw, () => ConfigWishes.IsRaw(fileExtension));
            AreEqual(audioFormat,        () => ConfigWishes.AudioFormat(fileExtension));
            AreEqual(audioFormat,        () => ConfigWishes.AsAudioFormat(fileExtension));
            AreEqual(audioFormat,        () => ConfigWishes.ToAudioFormat(fileExtension));
            AreEqual(audioFormat,        () => ConfigWishes.GetAudioFormat(fileExtension));
        }

        private void Assert_Immutable_Getters(AudioFileFormatEnum audioFileFormatEnum, AudioFileFormatEnum audioFormat)
        {
            // Extension Method Syntax
            AreEqual(audioFormat == Wav, () => audioFileFormatEnum.IsWav());
            AreEqual(audioFormat == Raw, () => audioFileFormatEnum.IsRaw());
            AreEqual(audioFormat,        () => audioFileFormatEnum.AudioFormat());
            AreEqual(audioFormat,        () => audioFileFormatEnum.ToAudioFormat());
            AreEqual(audioFormat,        () => audioFileFormatEnum.AsAudioFormat());
            AreEqual(audioFormat,        () => audioFileFormatEnum.GetAudioFormat());
            
            // Using Static Syntax
            AreEqual(audioFormat == Wav, () => IsWav(audioFileFormatEnum));
            AreEqual(audioFormat == Raw, () => IsRaw(audioFileFormatEnum));
            AreEqual(audioFormat,        () => AudioFormat(audioFileFormatEnum));
            AreEqual(audioFormat,        () => ToAudioFormat(audioFileFormatEnum));
            AreEqual(audioFormat,        () => AsAudioFormat(audioFileFormatEnum));
            AreEqual(audioFormat,        () => GetAudioFormat(audioFileFormatEnum));
            
            // Static Syntax
            AreEqual(audioFormat == Wav, () => ConfigWishes.IsWav(audioFileFormatEnum));
            AreEqual(audioFormat == Raw, () => ConfigWishes.IsRaw(audioFileFormatEnum));
            AreEqual(audioFormat,        () => ConfigWishes.AudioFormat(audioFileFormatEnum));
            AreEqual(audioFormat,        () => ConfigWishes.ToAudioFormat(audioFileFormatEnum));
            AreEqual(audioFormat,        () => ConfigWishes.AsAudioFormat(audioFileFormatEnum));
            AreEqual(audioFormat,        () => ConfigWishes.GetAudioFormat(audioFileFormatEnum));
        }
        
        private void Assert_Immutable_Getters(AudioFileFormat audioFormatEntity, AudioFileFormatEnum audioFormat)
        {
            if (audioFormatEntity == null) throw new NullException(() => audioFormatEntity);

            // Extension Method Syntax
            AreEqual(audioFormat == Wav, () => audioFormatEntity.IsWav());
            AreEqual(audioFormat == Raw, () => audioFormatEntity.IsRaw());
            AreEqual(audioFormat,        () => audioFormatEntity.AudioFormat());
            AreEqual(audioFormat,        () => audioFormatEntity.AsAudioFormat());
            AreEqual(audioFormat,        () => audioFormatEntity.ToAudioFormat());
            AreEqual(audioFormat,        () => audioFormatEntity.GetAudioFormat());
            AreEqual(audioFormat,        () => audioFormatEntity.AsEnum());
            AreEqual(audioFormat,        () => audioFormatEntity.ToEnum());
            AreEqual(audioFormat,        () => audioFormatEntity.GetEnum());
            AreEqual(audioFormat,        () => audioFormatEntity.AsAudioFormatEnum());
            AreEqual(audioFormat,        () => audioFormatEntity.ToAudioFormatEnum());
            AreEqual(audioFormat,        () => audioFormatEntity.GetAudioFormatEnum());
            AreEqual(audioFormat,        () => audioFormatEntity.EntityToEnum());
            AreEqual(audioFormat,        () => audioFormatEntity.EntityToAudioFormat());
            AreEqual(audioFormat,        () => audioFormatEntity.EntityToAudioFormatEnum());
            AreEqual(audioFormat,        () => audioFormatEntity.AudioFormatEntityToEnum());

            // Using Static Syntax
            AreEqual(audioFormat == Wav, () => IsWav(audioFormatEntity));
            AreEqual(audioFormat == Raw, () => IsRaw(audioFormatEntity));
            AreEqual(audioFormat,        () => AudioFormat(audioFormatEntity));
            AreEqual(audioFormat,        () => AsAudioFormat(audioFormatEntity));
            AreEqual(audioFormat,        () => ToAudioFormat(audioFormatEntity));
            AreEqual(audioFormat,        () => GetAudioFormat(audioFormatEntity));
            AreEqual(audioFormat,        () => AsEnum(audioFormatEntity));
            AreEqual(audioFormat,        () => ToEnum(audioFormatEntity));
            AreEqual(audioFormat,        () => GetEnum(audioFormatEntity));
            AreEqual(audioFormat,        () => AsAudioFormatEnum(audioFormatEntity));
            AreEqual(audioFormat,        () => ToAudioFormatEnum(audioFormatEntity));
            AreEqual(audioFormat,        () => GetAudioFormatEnum(audioFormatEntity));
            AreEqual(audioFormat,        () => EntityToEnum(audioFormatEntity));
            AreEqual(audioFormat,        () => EntityToAudioFormat(audioFormatEntity));
            AreEqual(audioFormat,        () => EntityToAudioFormatEnum(audioFormatEntity));
            AreEqual(audioFormat,        () => AudioFormatEntityToEnum(audioFormatEntity));

            // Static Syntax
            AreEqual(audioFormat == Wav, () => ConfigWishes.IsWav(audioFormatEntity));
            AreEqual(audioFormat == Raw, () => ConfigWishes.IsRaw(audioFormatEntity));
            AreEqual(audioFormat,        () => ConfigWishes.AudioFormat(audioFormatEntity));
            AreEqual(audioFormat,        () => ConfigWishes.AsAudioFormat(audioFormatEntity));
            AreEqual(audioFormat,        () => ConfigWishes.ToAudioFormat(audioFormatEntity));
            AreEqual(audioFormat,        () => ConfigWishes.GetAudioFormat(audioFormatEntity));
            AreEqual(audioFormat,        () => ConfigWishes.AsEnum(audioFormatEntity));
            AreEqual(audioFormat,        () => ConfigWishes.ToEnum(audioFormatEntity));
            AreEqual(audioFormat,        () => ConfigWishes.GetEnum(audioFormatEntity));
            AreEqual(audioFormat,        () => ConfigWishes.AsAudioFormatEnum(audioFormatEntity));
            AreEqual(audioFormat,        () => ConfigWishes.ToAudioFormatEnum(audioFormatEntity));
            AreEqual(audioFormat,        () => ConfigWishes.GetAudioFormatEnum(audioFormatEntity));
            AreEqual(audioFormat,        () => ConfigWishes.EntityToEnum(audioFormatEntity));
            AreEqual(audioFormat,        () => ConfigWishes.EntityToAudioFormat(audioFormatEntity));
            AreEqual(audioFormat,        () => ConfigWishes.EntityToAudioFormatEnum(audioFormatEntity));
            AreEqual(audioFormat,        () => ConfigWishes.AudioFormatEntityToEnum(audioFormatEntity));
        }

        // Test Data Helpers
        
        private ConfigTestEntities CreateTestEntities(AudioFileFormatEnum? audioFormat) 
            => new ConfigTestEntities(x => x.WithAudioFormat(audioFormat));

        static object TestParameters => new[] // ncrunch: no coverage
        {
            new object[] { (int)Raw, (int)Wav },
            new object[] { (int)Wav, (int)Raw }
        };

        static object TestParametersWithEmpty => new[] // ncrunch: no coverage
        {
            new object[] { (int)Raw, (int)Wav       },
            new object[] { (int)Wav, (int)Raw       },
            new object[] { (int)Raw, (int)Undefined },
            new object[] { null    , (int)Raw       },
            new object[] { (int)Raw, null           }
        };
    } 
}