using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Remoting.Contexts;
using System.Text;
using JetBrains.Annotations;
using JJ.Business.Synthesizer.Enums;
using JJ.Business.Synthesizer.Wishes.Obsolete;
using JJ.Business.Synthesizer.Wishes.TapeWishes;
using JJ.Framework.Common;
using JJ.Framework.Persistence;
using JJ.Framework.Reflection;
using JJ.Persistence.Synthesizer;
using static JJ.Business.Synthesizer.Wishes.Obsolete.ObsoleteEnumWishesMessages;

// ReSharper disable UnusedParameter.Global

namespace JJ.Business.Synthesizer.Wishes.Config
{
    // Channel: A Primary Audio Attribute

    /// <inheritdoc cref="docs._configextensionwishes"/>
    public static class ChannelExtensionWishes
    {
        // Synth-Bound
        
        public static bool IsNoChannel   (this SynthWishes obj) => ConfigWishes.IsNoChannel(obj);
        public static bool IsAnyChannel  (this SynthWishes obj) => ConfigWishes.IsAnyChannel(obj);
        public static bool IsEveryChannel(this SynthWishes obj) => ConfigWishes.IsEveryChannel(obj);
        public static bool IsCenter      (this SynthWishes obj) => ConfigWishes.IsCenter(obj);
        public static bool IsLeft        (this SynthWishes obj) => ConfigWishes.IsLeft(obj);
        public static bool IsRight       (this SynthWishes obj) => ConfigWishes.IsRight(obj);
        public static int? Channel       (this SynthWishes obj) => ConfigWishes.Channel(obj);
        public static int? GetChannel    (this SynthWishes obj) => ConfigWishes.GetChannel(obj);

        public static SynthWishes AnyChannel      (this SynthWishes obj            ) => ConfigWishes.AnyChannel(obj);
        public static SynthWishes EveryChannel    (this SynthWishes obj            ) => ConfigWishes.EveryChannel(obj);
        public static SynthWishes NoChannel       (this SynthWishes obj            ) => ConfigWishes.NoChannel(obj);
        public static SynthWishes Center          (this SynthWishes obj            ) => ConfigWishes.Center(obj);
        public static SynthWishes Left            (this SynthWishes obj            ) => ConfigWishes.Left(obj);
        public static SynthWishes Right           (this SynthWishes obj            ) => ConfigWishes.Right(obj);
        public static SynthWishes Channel         (this SynthWishes obj, int? value) => ConfigWishes.Channel(obj, value);
        public static SynthWishes WithNoChannel   (this SynthWishes obj            ) => ConfigWishes.WithNoChannel(obj);
        public static SynthWishes WithAnyChannel  (this SynthWishes obj            ) => ConfigWishes.WithAnyChannel(obj);
        public static SynthWishes WithEveryChannel(this SynthWishes obj            ) => ConfigWishes.WithEveryChannel(obj);
        public static SynthWishes WithCenter      (this SynthWishes obj            ) => ConfigWishes.WithCenter(obj);
        public static SynthWishes WithLeft        (this SynthWishes obj            ) => ConfigWishes.WithLeft(obj);
        public static SynthWishes WithRight       (this SynthWishes obj            ) => ConfigWishes.WithRight(obj);
        public static SynthWishes WithChannel     (this SynthWishes obj, int? value) => ConfigWishes.WithChannel(obj, value);
        public static SynthWishes AsAnyChannel    (this SynthWishes obj            ) => ConfigWishes.AsAnyChannel(obj);
        public static SynthWishes AsEveryChannel  (this SynthWishes obj            ) => ConfigWishes.AsEveryChannel(obj);
        public static SynthWishes AsNoChannel     (this SynthWishes obj            ) => ConfigWishes.AsNoChannel(obj);
        public static SynthWishes AsCenter        (this SynthWishes obj            ) => ConfigWishes.AsCenter(obj);
        public static SynthWishes AsLeft          (this SynthWishes obj            ) => ConfigWishes.AsLeft(obj);
        public static SynthWishes AsRight         (this SynthWishes obj            ) => ConfigWishes.AsRight(obj);
        public static SynthWishes AsChannel       (this SynthWishes obj, int? value) => ConfigWishes.AsChannel(obj, value);
        public static SynthWishes SetAnyChannel   (this SynthWishes obj            ) => ConfigWishes.SetAnyChannel(obj);
        public static SynthWishes SetEveryChannel (this SynthWishes obj            ) => ConfigWishes.SetEveryChannel(obj);
        public static SynthWishes SetNoChannel    (this SynthWishes obj            ) => ConfigWishes.SetNoChannel(obj);
        public static SynthWishes SetCenter       (this SynthWishes obj            ) => ConfigWishes.SetCenter(obj);
        public static SynthWishes SetLeft         (this SynthWishes obj            ) => ConfigWishes.SetLeft(obj);
        public static SynthWishes SetRight        (this SynthWishes obj            ) => ConfigWishes.SetRight(obj);
        public static SynthWishes SetChannel      (this SynthWishes obj, int? value) => ConfigWishes.SetChannel(obj, value);

        public static bool IsNoChannel   (this FlowNode obj) => ConfigWishes.IsNoChannel(obj);
        public static bool IsAnyChannel  (this FlowNode obj) => ConfigWishes.IsAnyChannel(obj);
        public static bool IsEveryChannel(this FlowNode obj) => ConfigWishes.IsEveryChannel(obj);
        public static bool IsCenter      (this FlowNode obj) => ConfigWishes.IsCenter(obj);
        public static bool IsLeft        (this FlowNode obj) => ConfigWishes.IsLeft(obj);
        public static bool IsRight       (this FlowNode obj) => ConfigWishes.IsRight(obj);
        public static int? Channel       (this FlowNode obj) => ConfigWishes.Channel(obj);
        public static int? GetChannel    (this FlowNode obj) => ConfigWishes.GetChannel(obj);

        public static FlowNode AnyChannel      (this FlowNode obj            ) => ConfigWishes.AnyChannel(obj);
        public static FlowNode EveryChannel    (this FlowNode obj            ) => ConfigWishes.EveryChannel(obj);
        public static FlowNode NoChannel       (this FlowNode obj            ) => ConfigWishes.NoChannel(obj);
        public static FlowNode Center          (this FlowNode obj            ) => ConfigWishes.Center(obj);
        public static FlowNode Left            (this FlowNode obj            ) => ConfigWishes.Left(obj);
        public static FlowNode Right           (this FlowNode obj            ) => ConfigWishes.Right(obj);
        public static FlowNode Channel         (this FlowNode obj, int? value) => ConfigWishes.Channel(obj, value);
        public static FlowNode WithNoChannel   (this FlowNode obj            ) => ConfigWishes.WithNoChannel(obj);
        public static FlowNode WithAnyChannel  (this FlowNode obj            ) => ConfigWishes.WithAnyChannel(obj);
        public static FlowNode WithEveryChannel(this FlowNode obj            ) => ConfigWishes.WithEveryChannel(obj);
        public static FlowNode WithCenter      (this FlowNode obj            ) => ConfigWishes.WithCenter(obj);
        public static FlowNode WithLeft        (this FlowNode obj            ) => ConfigWishes.WithLeft(obj);
        public static FlowNode WithRight       (this FlowNode obj            ) => ConfigWishes.WithRight(obj);
        public static FlowNode WithChannel     (this FlowNode obj, int? value) => ConfigWishes.WithChannel(obj, value);
        public static FlowNode AsNoChannel     (this FlowNode obj            ) => ConfigWishes.AsNoChannel(obj);
        public static FlowNode AsAnyChannel    (this FlowNode obj            ) => ConfigWishes.AsAnyChannel(obj);
        public static FlowNode AsEveryChannel  (this FlowNode obj            ) => ConfigWishes.AsEveryChannel(obj);
        public static FlowNode AsCenter        (this FlowNode obj            ) => ConfigWishes.AsCenter(obj);
        public static FlowNode AsLeft          (this FlowNode obj            ) => ConfigWishes.AsLeft(obj);
        public static FlowNode AsRight         (this FlowNode obj            ) => ConfigWishes.AsRight(obj);
        public static FlowNode AsChannel       (this FlowNode obj, int? value) => ConfigWishes.AsChannel(obj, value);
        public static FlowNode SetAnyChannel   (this FlowNode obj            ) => ConfigWishes.SetAnyChannel(obj);
        public static FlowNode SetEveryChannel (this FlowNode obj            ) => ConfigWishes.SetEveryChannel(obj);
        public static FlowNode SetNoChannel    (this FlowNode obj            ) => ConfigWishes.SetNoChannel(obj);
        public static FlowNode SetCenter       (this FlowNode obj            ) => ConfigWishes.SetCenter(obj);
        public static FlowNode SetLeft         (this FlowNode obj            ) => ConfigWishes.SetLeft(obj);
        public static FlowNode SetRight        (this FlowNode obj            ) => ConfigWishes.SetRight(obj);
        public static FlowNode SetChannel      (this FlowNode obj, int? value) => ConfigWishes.SetChannel(obj, value);

        [UsedImplicitly] internal static bool IsNoChannel   (this ConfigResolver obj) => ConfigWishes.IsNoChannel(obj);
        [UsedImplicitly] internal static bool IsAnyChannel  (this ConfigResolver obj) => ConfigWishes.IsAnyChannel(obj);
        [UsedImplicitly] internal static bool IsEveryChannel(this ConfigResolver obj) => ConfigWishes.IsEveryChannel(obj);
        [UsedImplicitly] internal static bool IsCenter      (this ConfigResolver obj) => ConfigWishes.IsCenter(obj);
        [UsedImplicitly] internal static bool IsLeft        (this ConfigResolver obj) => ConfigWishes.IsLeft(obj);
        [UsedImplicitly] internal static bool IsRight       (this ConfigResolver obj) => ConfigWishes.IsRight(obj);
        [UsedImplicitly] internal static int? Channel       (this ConfigResolver obj) => ConfigWishes.Channel(obj);
        [UsedImplicitly] internal static int? GetChannel    (this ConfigResolver obj) => ConfigWishes.GetChannel(obj);

        [UsedImplicitly] internal static ConfigResolver AnyChannel      (this ConfigResolver obj            ) => ConfigWishes.AnyChannel(obj);
        [UsedImplicitly] internal static ConfigResolver EveryChannel    (this ConfigResolver obj            ) => ConfigWishes.EveryChannel(obj);
        [UsedImplicitly] internal static ConfigResolver NoChannel       (this ConfigResolver obj            ) => ConfigWishes.NoChannel(obj);
        [UsedImplicitly] internal static ConfigResolver Center          (this ConfigResolver obj            ) => ConfigWishes.Center(obj);
        [UsedImplicitly] internal static ConfigResolver Left            (this ConfigResolver obj            ) => ConfigWishes.Left(obj);
        [UsedImplicitly] internal static ConfigResolver Right           (this ConfigResolver obj            ) => ConfigWishes.Right(obj);
        [UsedImplicitly] internal static ConfigResolver Channel         (this ConfigResolver obj, int? value) => ConfigWishes.Channel(obj, value);
        [UsedImplicitly] internal static ConfigResolver WithNoChannel   (this ConfigResolver obj            ) => ConfigWishes.WithNoChannel(obj);
        [UsedImplicitly] internal static ConfigResolver WithAnyChannel  (this ConfigResolver obj            ) => ConfigWishes.WithAnyChannel(obj);
        [UsedImplicitly] internal static ConfigResolver WithEveryChannel(this ConfigResolver obj            ) => ConfigWishes.WithEveryChannel(obj);
        [UsedImplicitly] internal static ConfigResolver WithCenter      (this ConfigResolver obj            ) => ConfigWishes.WithCenter(obj);
        [UsedImplicitly] internal static ConfigResolver WithLeft        (this ConfigResolver obj            ) => ConfigWishes.WithLeft(obj);
        [UsedImplicitly] internal static ConfigResolver WithRight       (this ConfigResolver obj            ) => ConfigWishes.WithRight(obj);
        [UsedImplicitly] internal static ConfigResolver WithChannel     (this ConfigResolver obj, int? value) => ConfigWishes.WithChannel(obj, value);
        [UsedImplicitly] internal static ConfigResolver AsNoChannel     (this ConfigResolver obj            ) => ConfigWishes.AsNoChannel(obj);
        [UsedImplicitly] internal static ConfigResolver AsAnyChannel    (this ConfigResolver obj            ) => ConfigWishes.AsAnyChannel(obj);
        [UsedImplicitly] internal static ConfigResolver AsEveryChannel  (this ConfigResolver obj            ) => ConfigWishes.AsEveryChannel(obj);
        [UsedImplicitly] internal static ConfigResolver AsCenter        (this ConfigResolver obj            ) => ConfigWishes.AsCenter(obj);
        [UsedImplicitly] internal static ConfigResolver AsLeft          (this ConfigResolver obj            ) => ConfigWishes.AsLeft(obj);
        [UsedImplicitly] internal static ConfigResolver AsRight         (this ConfigResolver obj            ) => ConfigWishes.AsRight(obj);
        [UsedImplicitly] internal static ConfigResolver AsChannel       (this ConfigResolver obj, int? value) => ConfigWishes.AsChannel(obj, value);
        [UsedImplicitly] internal static ConfigResolver SetAnyChannel   (this ConfigResolver obj            ) => ConfigWishes.SetAnyChannel(obj);
        [UsedImplicitly] internal static ConfigResolver SetEveryChannel (this ConfigResolver obj            ) => ConfigWishes.SetEveryChannel(obj);
        [UsedImplicitly] internal static ConfigResolver SetNoChannel    (this ConfigResolver obj            ) => ConfigWishes.SetNoChannel(obj);
        [UsedImplicitly] internal static ConfigResolver SetCenter       (this ConfigResolver obj            ) => ConfigWishes.SetCenter(obj);
        [UsedImplicitly] internal static ConfigResolver SetLeft         (this ConfigResolver obj            ) => ConfigWishes.SetLeft(obj);
        [UsedImplicitly] internal static ConfigResolver SetRight        (this ConfigResolver obj            ) => ConfigWishes.SetRight(obj);
        [UsedImplicitly] internal static ConfigResolver SetChannel      (this ConfigResolver obj, int? value) => ConfigWishes.SetChannel(obj, value);

        // Tape-Bound

        public static bool IsNoChannel   (this Tape obj) => ConfigWishes.IsNoChannel(obj);
        public static bool IsAnyChannel  (this Tape obj) => ConfigWishes.IsAnyChannel(obj);
        public static bool IsEveryChannel(this Tape obj) => ConfigWishes.IsEveryChannel(obj);
        public static bool IsCenter      (this Tape obj) => ConfigWishes.IsCenter(obj);
        public static bool IsLeft        (this Tape obj) => ConfigWishes.IsLeft(obj);
        public static bool IsRight       (this Tape obj) => ConfigWishes.IsRight(obj);
        public static int? Channel       (this Tape obj) => ConfigWishes.Channel(obj);
        public static int? GetChannel    (this Tape obj) => ConfigWishes.GetChannel(obj);

        public static Tape AnyChannel      (this Tape obj            ) => ConfigWishes.AnyChannel(obj);
        public static Tape EveryChannel    (this Tape obj            ) => ConfigWishes.EveryChannel(obj);
        public static Tape NoChannel       (this Tape obj            ) => ConfigWishes.NoChannel(obj);
        public static Tape Center          (this Tape obj            ) => ConfigWishes.Center(obj);
        public static Tape Left            (this Tape obj            ) => ConfigWishes.Left(obj);
        public static Tape Right           (this Tape obj            ) => ConfigWishes.Right(obj);
        public static Tape Channel         (this Tape obj, int? value) => ConfigWishes.Channel(obj, value);
        public static Tape WithNoChannel   (this Tape obj            ) => ConfigWishes.WithNoChannel(obj);
        public static Tape WithAnyChannel  (this Tape obj            ) => ConfigWishes.WithAnyChannel(obj);
        public static Tape WithEveryChannel(this Tape obj            ) => ConfigWishes.WithEveryChannel(obj);
        public static Tape WithCenter      (this Tape obj            ) => ConfigWishes.WithCenter(obj);
        public static Tape WithLeft        (this Tape obj            ) => ConfigWishes.WithLeft(obj);
        public static Tape WithRight       (this Tape obj            ) => ConfigWishes.WithRight(obj);
        public static Tape WithChannel     (this Tape obj, int? value) => ConfigWishes.WithChannel(obj, value);
        public static Tape AsNoChannel     (this Tape obj            ) => ConfigWishes.AsNoChannel(obj);
        public static Tape AsAnyChannel    (this Tape obj            ) => ConfigWishes.AsAnyChannel(obj);
        public static Tape AsEveryChannel  (this Tape obj            ) => ConfigWishes.AsEveryChannel(obj);
        public static Tape AsCenter        (this Tape obj            ) => ConfigWishes.AsCenter(obj);
        public static Tape AsLeft          (this Tape obj            ) => ConfigWishes.AsLeft(obj);
        public static Tape AsRight         (this Tape obj            ) => ConfigWishes.AsRight(obj);
        public static Tape AsChannel       (this Tape obj, int? value) => ConfigWishes.AsChannel(obj, value);
        public static Tape SetAnyChannel   (this Tape obj            ) => ConfigWishes.SetAnyChannel(obj);
        public static Tape SetEveryChannel (this Tape obj            ) => ConfigWishes.SetEveryChannel(obj);
        public static Tape SetNoChannel    (this Tape obj            ) => ConfigWishes.SetNoChannel(obj);
        public static Tape SetCenter       (this Tape obj            ) => ConfigWishes.SetCenter(obj);
        public static Tape SetLeft         (this Tape obj            ) => ConfigWishes.SetLeft(obj);
        public static Tape SetRight        (this Tape obj            ) => ConfigWishes.SetRight(obj);
        public static Tape SetChannel      (this Tape obj, int? value) => ConfigWishes.SetChannel(obj, value);

        public static bool IsNoChannel   (this TapeConfig obj) => ConfigWishes.IsNoChannel   (obj);
        public static bool IsAnyChannel  (this TapeConfig obj) => ConfigWishes.IsAnyChannel  (obj);
        public static bool IsEveryChannel(this TapeConfig obj) => ConfigWishes.IsEveryChannel(obj);
        public static bool IsCenter      (this TapeConfig obj) => ConfigWishes.IsCenter      (obj);
        public static bool IsLeft        (this TapeConfig obj) => ConfigWishes.IsLeft        (obj);
        public static bool IsRight       (this TapeConfig obj) => ConfigWishes.IsRight       (obj);
        public static int? Channel       (this TapeConfig obj) => ConfigWishes.Channel       (obj);
        public static int? GetChannel    (this TapeConfig obj) => ConfigWishes.GetChannel    (obj);
        
        public static TapeConfig AnyChannel      (this TapeConfig obj            ) => ConfigWishes.AnyChannel      (obj);
        public static TapeConfig EveryChannel    (this TapeConfig obj            ) => ConfigWishes.EveryChannel    (obj);
        public static TapeConfig NoChannel       (this TapeConfig obj            ) => ConfigWishes.NoChannel       (obj);
        public static TapeConfig Center          (this TapeConfig obj            ) => ConfigWishes.Center          (obj);
        public static TapeConfig Left            (this TapeConfig obj            ) => ConfigWishes.Left            (obj);
        public static TapeConfig Right           (this TapeConfig obj            ) => ConfigWishes.Right           (obj);
        public static TapeConfig Channel         (this TapeConfig obj, int? value) => ConfigWishes.Channel         (obj, value);
        public static TapeConfig WithNoChannel   (this TapeConfig obj            ) => ConfigWishes.WithNoChannel   (obj);
        public static TapeConfig WithAnyChannel  (this TapeConfig obj            ) => ConfigWishes.WithAnyChannel  (obj);
        public static TapeConfig WithEveryChannel(this TapeConfig obj            ) => ConfigWishes.WithEveryChannel(obj);
        public static TapeConfig WithCenter      (this TapeConfig obj            ) => ConfigWishes.WithCenter      (obj);
        public static TapeConfig WithLeft        (this TapeConfig obj            ) => ConfigWishes.WithLeft        (obj);
        public static TapeConfig WithRight       (this TapeConfig obj            ) => ConfigWishes.WithRight       (obj);
        public static TapeConfig WithChannel     (this TapeConfig obj, int? value) => ConfigWishes.WithChannel     (obj, value);
        public static TapeConfig AsNoChannel     (this TapeConfig obj            ) => ConfigWishes.AsNoChannel     (obj);
        public static TapeConfig AsAnyChannel    (this TapeConfig obj            ) => ConfigWishes.AsAnyChannel    (obj);
        public static TapeConfig AsEveryChannel  (this TapeConfig obj            ) => ConfigWishes.AsEveryChannel  (obj);
        public static TapeConfig AsCenter        (this TapeConfig obj            ) => ConfigWishes.AsCenter        (obj);
        public static TapeConfig AsLeft          (this TapeConfig obj            ) => ConfigWishes.AsLeft          (obj);
        public static TapeConfig AsRight         (this TapeConfig obj            ) => ConfigWishes.AsRight         (obj);
        public static TapeConfig AsChannel       (this TapeConfig obj, int? value) => ConfigWishes.AsChannel       (obj, value);
        public static TapeConfig SetAnyChannel   (this TapeConfig obj            ) => ConfigWishes.SetAnyChannel   (obj);
        public static TapeConfig SetEveryChannel (this TapeConfig obj            ) => ConfigWishes.SetEveryChannel (obj);
        public static TapeConfig SetNoChannel    (this TapeConfig obj            ) => ConfigWishes.SetNoChannel    (obj);
        public static TapeConfig SetCenter       (this TapeConfig obj            ) => ConfigWishes.SetCenter       (obj);
        public static TapeConfig SetLeft         (this TapeConfig obj            ) => ConfigWishes.SetLeft         (obj);
        public static TapeConfig SetRight        (this TapeConfig obj            ) => ConfigWishes.SetRight        (obj);
        public static TapeConfig SetChannel      (this TapeConfig obj, int? value) => ConfigWishes.SetChannel      (obj, value);
        
        public static bool IsNoChannel   (this TapeActions obj) => ConfigWishes.IsNoChannel   (obj);
        public static bool IsAnyChannel  (this TapeActions obj) => ConfigWishes.IsAnyChannel  (obj);
        public static bool IsEveryChannel(this TapeActions obj) => ConfigWishes.IsEveryChannel(obj);
        public static bool IsCenter      (this TapeActions obj) => ConfigWishes.IsCenter      (obj);
        public static bool IsLeft        (this TapeActions obj) => ConfigWishes.IsLeft        (obj);
        public static bool IsRight       (this TapeActions obj) => ConfigWishes.IsRight       (obj);
        public static int? Channel       (this TapeActions obj) => ConfigWishes.Channel       (obj);
        public static int? GetChannel    (this TapeActions obj) => ConfigWishes.GetChannel    (obj);
        
        public static TapeActions AnyChannel      (this TapeActions obj            ) => ConfigWishes.AnyChannel      (obj);
        public static TapeActions EveryChannel    (this TapeActions obj            ) => ConfigWishes.EveryChannel    (obj);
        public static TapeActions NoChannel       (this TapeActions obj            ) => ConfigWishes.NoChannel       (obj);
        public static TapeActions Center          (this TapeActions obj            ) => ConfigWishes.Center          (obj);
        public static TapeActions Left            (this TapeActions obj            ) => ConfigWishes.Left            (obj);
        public static TapeActions Right           (this TapeActions obj            ) => ConfigWishes.Right           (obj);
        public static TapeActions Channel         (this TapeActions obj, int? value) => ConfigWishes.Channel         (obj, value);
        public static TapeActions WithNoChannel   (this TapeActions obj            ) => ConfigWishes.WithNoChannel   (obj);
        public static TapeActions WithAnyChannel  (this TapeActions obj            ) => ConfigWishes.WithAnyChannel  (obj);
        public static TapeActions WithEveryChannel(this TapeActions obj            ) => ConfigWishes.WithEveryChannel(obj);
        public static TapeActions WithCenter      (this TapeActions obj            ) => ConfigWishes.WithCenter      (obj);
        public static TapeActions WithLeft        (this TapeActions obj            ) => ConfigWishes.WithLeft        (obj);
        public static TapeActions WithRight       (this TapeActions obj            ) => ConfigWishes.WithRight       (obj);
        public static TapeActions WithChannel     (this TapeActions obj, int? value) => ConfigWishes.WithChannel     (obj, value);
        public static TapeActions AsNoChannel     (this TapeActions obj            ) => ConfigWishes.AsNoChannel     (obj);
        public static TapeActions AsAnyChannel    (this TapeActions obj            ) => ConfigWishes.AsAnyChannel    (obj);
        public static TapeActions AsEveryChannel  (this TapeActions obj            ) => ConfigWishes.AsEveryChannel  (obj);
        public static TapeActions AsCenter        (this TapeActions obj            ) => ConfigWishes.AsCenter        (obj);
        public static TapeActions AsLeft          (this TapeActions obj            ) => ConfigWishes.AsLeft          (obj);
        public static TapeActions AsRight         (this TapeActions obj            ) => ConfigWishes.AsRight         (obj);
        public static TapeActions AsChannel       (this TapeActions obj, int? value) => ConfigWishes.AsChannel       (obj, value);
        public static TapeActions SetAnyChannel   (this TapeActions obj            ) => ConfigWishes.SetAnyChannel   (obj);
        public static TapeActions SetEveryChannel (this TapeActions obj            ) => ConfigWishes.SetEveryChannel (obj);
        public static TapeActions SetNoChannel    (this TapeActions obj            ) => ConfigWishes.SetNoChannel    (obj);
        public static TapeActions SetCenter       (this TapeActions obj            ) => ConfigWishes.SetCenter       (obj);
        public static TapeActions SetLeft         (this TapeActions obj            ) => ConfigWishes.SetLeft         (obj);
        public static TapeActions SetRight        (this TapeActions obj            ) => ConfigWishes.SetRight        (obj);
        public static TapeActions SetChannel      (this TapeActions obj, int? value) => ConfigWishes.SetChannel      (obj, value);
        
        public static bool IsNoChannel   (this TapeAction obj) => ConfigWishes.IsNoChannel   (obj);
        public static bool IsAnyChannel  (this TapeAction obj) => ConfigWishes.IsAnyChannel  (obj);
        public static bool IsEveryChannel(this TapeAction obj) => ConfigWishes.IsEveryChannel(obj);
        public static bool IsCenter      (this TapeAction obj) => ConfigWishes.IsCenter      (obj);
        public static bool IsLeft        (this TapeAction obj) => ConfigWishes.IsLeft        (obj);
        public static bool IsRight       (this TapeAction obj) => ConfigWishes.IsRight       (obj);
        public static int? Channel       (this TapeAction obj) => ConfigWishes.Channel       (obj);
        public static int? GetChannel    (this TapeAction obj) => ConfigWishes.GetChannel    (obj);
        
        public static TapeAction AnyChannel      (this TapeAction obj            ) => ConfigWishes.AnyChannel      (obj);
        public static TapeAction EveryChannel    (this TapeAction obj            ) => ConfigWishes.EveryChannel    (obj);
        public static TapeAction NoChannel       (this TapeAction obj            ) => ConfigWishes.NoChannel       (obj);
        public static TapeAction Center          (this TapeAction obj            ) => ConfigWishes.Center          (obj);
        public static TapeAction Left            (this TapeAction obj            ) => ConfigWishes.Left            (obj);
        public static TapeAction Right           (this TapeAction obj            ) => ConfigWishes.Right           (obj);
        public static TapeAction Channel         (this TapeAction obj, int? value) => ConfigWishes.Channel         (obj, value);
        public static TapeAction WithNoChannel   (this TapeAction obj            ) => ConfigWishes.WithNoChannel   (obj);
        public static TapeAction WithAnyChannel  (this TapeAction obj            ) => ConfigWishes.WithAnyChannel  (obj);
        public static TapeAction WithEveryChannel(this TapeAction obj            ) => ConfigWishes.WithEveryChannel(obj);
        public static TapeAction WithCenter      (this TapeAction obj            ) => ConfigWishes.WithCenter      (obj);
        public static TapeAction WithLeft        (this TapeAction obj            ) => ConfigWishes.WithLeft        (obj);
        public static TapeAction WithRight       (this TapeAction obj            ) => ConfigWishes.WithRight       (obj);
        public static TapeAction WithChannel     (this TapeAction obj, int? value) => ConfigWishes.WithChannel     (obj, value);
        public static TapeAction AsNoChannel     (this TapeAction obj            ) => ConfigWishes.AsNoChannel     (obj);
        public static TapeAction AsAnyChannel    (this TapeAction obj            ) => ConfigWishes.AsAnyChannel    (obj);
        public static TapeAction AsEveryChannel  (this TapeAction obj            ) => ConfigWishes.AsEveryChannel  (obj);
        public static TapeAction AsCenter        (this TapeAction obj            ) => ConfigWishes.AsCenter        (obj);
        public static TapeAction AsLeft          (this TapeAction obj            ) => ConfigWishes.AsLeft          (obj);
        public static TapeAction AsRight         (this TapeAction obj            ) => ConfigWishes.AsRight         (obj);
        public static TapeAction AsChannel       (this TapeAction obj, int? value) => ConfigWishes.AsChannel       (obj, value);
        public static TapeAction SetAnyChannel   (this TapeAction obj            ) => ConfigWishes.SetAnyChannel   (obj);
        public static TapeAction SetEveryChannel (this TapeAction obj            ) => ConfigWishes.SetEveryChannel (obj);
        public static TapeAction SetNoChannel    (this TapeAction obj            ) => ConfigWishes.SetNoChannel    (obj);
        public static TapeAction SetCenter       (this TapeAction obj            ) => ConfigWishes.SetCenter       (obj);
        public static TapeAction SetLeft         (this TapeAction obj            ) => ConfigWishes.SetLeft         (obj);
        public static TapeAction SetRight        (this TapeAction obj            ) => ConfigWishes.SetRight        (obj);
        public static TapeAction SetChannel      (this TapeAction obj, int? value) => ConfigWishes.SetChannel      (obj, value);
        
        // Buff-Bound
        
        public static bool IsNoChannel   (this Buff obj) => ConfigWishes.IsNoChannel   (obj);
        public static bool IsAnyChannel  (this Buff obj) => ConfigWishes.IsAnyChannel  (obj);
        public static bool IsEveryChannel(this Buff obj) => ConfigWishes.IsEveryChannel(obj);
        public static bool IsCenter      (this Buff obj) => ConfigWishes.IsCenter      (obj);
        public static bool IsLeft        (this Buff obj) => ConfigWishes.IsLeft        (obj);
        public static bool IsRight       (this Buff obj) => ConfigWishes.IsRight       (obj);
        public static int? Channel       (this Buff obj) => ConfigWishes.Channel       (obj);
        public static int? GetChannel    (this Buff obj) => ConfigWishes.GetChannel    (obj);
        
        public static Buff AnyChannel      (this Buff obj,             IContext context) => ConfigWishes.AnyChannel      (obj,        context);
        public static Buff EveryChannel    (this Buff obj,             IContext context) => ConfigWishes.EveryChannel    (obj,        context);
        public static Buff NoChannel       (this Buff obj,             IContext context) => ConfigWishes.NoChannel       (obj,        context);
        public static Buff Center          (this Buff obj,             IContext context) => ConfigWishes.Center          (obj,        context);
        public static Buff Left            (this Buff obj,             IContext context) => ConfigWishes.Left            (obj,        context);
        public static Buff Right           (this Buff obj,             IContext context) => ConfigWishes.Right           (obj,        context);
        public static Buff Channel         (this Buff obj, int? value, IContext context) => ConfigWishes.Channel         (obj, value, context);
        public static Buff WithNoChannel   (this Buff obj,             IContext context) => ConfigWishes.WithNoChannel   (obj,        context);
        public static Buff WithAnyChannel  (this Buff obj,             IContext context) => ConfigWishes.WithAnyChannel  (obj,        context);
        public static Buff WithEveryChannel(this Buff obj,             IContext context) => ConfigWishes.WithEveryChannel(obj,        context);
        public static Buff WithCenter      (this Buff obj,             IContext context) => ConfigWishes.WithCenter      (obj,        context);
        public static Buff WithLeft        (this Buff obj,             IContext context) => ConfigWishes.WithLeft        (obj,        context);
        public static Buff WithRight       (this Buff obj,             IContext context) => ConfigWishes.WithRight       (obj,        context);
        public static Buff WithChannel     (this Buff obj, int? value, IContext context) => ConfigWishes.WithChannel     (obj, value, context);
        public static Buff AsNoChannel     (this Buff obj,             IContext context) => ConfigWishes.AsNoChannel     (obj,        context);
        public static Buff AsAnyChannel    (this Buff obj,             IContext context) => ConfigWishes.AsAnyChannel    (obj,        context);
        public static Buff AsEveryChannel  (this Buff obj,             IContext context) => ConfigWishes.AsEveryChannel  (obj,        context);
        public static Buff AsCenter        (this Buff obj,             IContext context) => ConfigWishes.AsCenter        (obj,        context);
        public static Buff AsLeft          (this Buff obj,             IContext context) => ConfigWishes.AsLeft          (obj,        context);
        public static Buff AsRight         (this Buff obj,             IContext context) => ConfigWishes.AsRight         (obj,        context);
        public static Buff AsChannel       (this Buff obj, int? value, IContext context) => ConfigWishes.AsChannel       (obj, value, context);
        public static Buff SetAnyChannel   (this Buff obj,             IContext context) => ConfigWishes.SetAnyChannel   (obj,        context);
        public static Buff SetEveryChannel (this Buff obj,             IContext context) => ConfigWishes.SetEveryChannel (obj,        context);
        public static Buff SetNoChannel    (this Buff obj,             IContext context) => ConfigWishes.SetNoChannel    (obj,        context);
        public static Buff SetCenter       (this Buff obj,             IContext context) => ConfigWishes.SetCenter       (obj,        context);
        public static Buff SetLeft         (this Buff obj,             IContext context) => ConfigWishes.SetLeft         (obj,        context);
        public static Buff SetRight        (this Buff obj,             IContext context) => ConfigWishes.SetRight        (obj,        context);
        public static Buff SetChannel      (this Buff obj, int? value, IContext context) => ConfigWishes.SetChannel      (obj, value, context);
       
        public static bool IsNoChannel   (this AudioFileOutput obj) => ConfigWishes.IsNoChannel   (obj);
        public static bool IsAnyChannel  (this AudioFileOutput obj) => ConfigWishes.IsAnyChannel  (obj);
        public static bool IsEveryChannel(this AudioFileOutput obj) => ConfigWishes.IsEveryChannel(obj);
        public static bool IsCenter      (this AudioFileOutput obj) => ConfigWishes.IsCenter      (obj);
        public static bool IsLeft        (this AudioFileOutput obj) => ConfigWishes.IsLeft        (obj);
        public static bool IsRight       (this AudioFileOutput obj) => ConfigWishes.IsRight       (obj);
        public static int? Channel       (this AudioFileOutput obj) => ConfigWishes.Channel       (obj);
        public static int? GetChannel    (this AudioFileOutput obj) => ConfigWishes.GetChannel    (obj);

        /// <inheritdoc cref="docs._channeltoaudiofileoutput" />
        public static AudioFileOutput AnyChannel      (this AudioFileOutput obj,             IContext context) => ConfigWishes.NoChannel       (obj, context);
        /// <inheritdoc cref="docs._channeltoaudiofileoutput" />
        public static AudioFileOutput EveryChannel    (this AudioFileOutput obj,             IContext context) => ConfigWishes.AnyChannel      (obj, context);
        /// <inheritdoc cref="docs._channeltoaudiofileoutput" />
        public static AudioFileOutput NoChannel       (this AudioFileOutput obj,             IContext context) => ConfigWishes.EveryChannel    (obj, context);
        /// <inheritdoc cref="docs._channeltoaudiofileoutput" />
        public static AudioFileOutput Center          (this AudioFileOutput obj,             IContext context) => ConfigWishes.Center          (obj, context);
        /// <inheritdoc cref="docs._channeltoaudiofileoutput" />
        public static AudioFileOutput Left            (this AudioFileOutput obj,             IContext context) => ConfigWishes.Left            (obj, context);
        /// <inheritdoc cref="docs._channeltoaudiofileoutput" />
        public static AudioFileOutput Right           (this AudioFileOutput obj,             IContext context) => ConfigWishes.Right           (obj, context);
        /// <inheritdoc cref="docs._channeltoaudiofileoutput" />
        public static AudioFileOutput Channel         (this AudioFileOutput obj, int? value, IContext context) => ConfigWishes.Channel         (obj, value, context);
        /// <inheritdoc cref="docs._channeltoaudiofileoutput" />
        public static AudioFileOutput WithNoChannel   (this AudioFileOutput obj,             IContext context) => ConfigWishes.WithNoChannel   (obj, context);
        /// <inheritdoc cref="docs._channeltoaudiofileoutput" />
        public static AudioFileOutput WithAnyChannel  (this AudioFileOutput obj,             IContext context) => ConfigWishes.WithAnyChannel  (obj, context);
        /// <inheritdoc cref="docs._channeltoaudiofileoutput" />
        public static AudioFileOutput WithEveryChannel(this AudioFileOutput obj,             IContext context) => ConfigWishes.WithEveryChannel(obj, context);
        /// <inheritdoc cref="docs._channeltoaudiofileoutput" />
        public static AudioFileOutput WithCenter      (this AudioFileOutput obj,             IContext context) => ConfigWishes.WithCenter      (obj, context);
        /// <inheritdoc cref="docs._channeltoaudiofileoutput" />
        public static AudioFileOutput WithLeft        (this AudioFileOutput obj,             IContext context) => ConfigWishes.WithLeft        (obj, context);
        /// <inheritdoc cref="docs._channeltoaudiofileoutput" />
        public static AudioFileOutput WithRight       (this AudioFileOutput obj,             IContext context) => ConfigWishes.WithRight       (obj, context);
        /// <inheritdoc cref="docs._channeltoaudiofileoutput" />
        public static AudioFileOutput WithChannel     (this AudioFileOutput obj, int? value, IContext context) => ConfigWishes.WithChannel     (obj, value, context);
        /// <inheritdoc cref="docs._channeltoaudiofileoutput" />
        public static AudioFileOutput AsNoChannel     (this AudioFileOutput obj,             IContext context) => ConfigWishes.AsNoChannel     (obj, context);
        /// <inheritdoc cref="docs._channeltoaudiofileoutput" />
        public static AudioFileOutput AsAnyChannel    (this AudioFileOutput obj,             IContext context) => ConfigWishes.AsAnyChannel    (obj, context);
        /// <inheritdoc cref="docs._channeltoaudiofileoutput" />
        public static AudioFileOutput AsEveryChannel  (this AudioFileOutput obj,             IContext context) => ConfigWishes.AsEveryChannel  (obj, context);
        /// <inheritdoc cref="docs._channeltoaudiofileoutput" />
        public static AudioFileOutput AsCenter        (this AudioFileOutput obj,             IContext context) => ConfigWishes.AsCenter        (obj, context);
        /// <inheritdoc cref="docs._channeltoaudiofileoutput" />
        public static AudioFileOutput AsLeft          (this AudioFileOutput obj,             IContext context) => ConfigWishes.AsLeft          (obj, context);
        /// <inheritdoc cref="docs._channeltoaudiofileoutput" />
        public static AudioFileOutput AsRight         (this AudioFileOutput obj,             IContext context) => ConfigWishes.AsRight         (obj, context);
        /// <inheritdoc cref="docs._channeltoaudiofileoutput" />
        public static AudioFileOutput AsChannel       (this AudioFileOutput obj, int? value, IContext context) => ConfigWishes.AsChannel       (obj, value, context);
        /// <inheritdoc cref="docs._channeltoaudiofileoutput" />
        public static AudioFileOutput SetAnyChannel   (this AudioFileOutput obj,             IContext context) => ConfigWishes.SetAnyChannel   (obj, context);
        /// <inheritdoc cref="docs._channeltoaudiofileoutput" />
        public static AudioFileOutput SetEveryChannel (this AudioFileOutput obj,             IContext context) => ConfigWishes.SetEveryChannel (obj, context);
        /// <inheritdoc cref="docs._channeltoaudiofileoutput" />
        public static AudioFileOutput SetNoChannel    (this AudioFileOutput obj,             IContext context) => ConfigWishes.SetNoChannel    (obj, context);
        /// <inheritdoc cref="docs._channeltoaudiofileoutput" />
        public static AudioFileOutput SetCenter       (this AudioFileOutput obj,             IContext context) => ConfigWishes.SetCenter       (obj, context);
        /// <inheritdoc cref="docs._channeltoaudiofileoutput" />
        public static AudioFileOutput SetLeft         (this AudioFileOutput obj,             IContext context) => ConfigWishes.SetLeft         (obj, context);
        /// <inheritdoc cref="docs._channeltoaudiofileoutput" />
        public static AudioFileOutput SetRight        (this AudioFileOutput obj,             IContext context) => ConfigWishes.SetRight        (obj, context);
        /// <inheritdoc cref="docs._channeltoaudiofileoutput" />
        public static AudioFileOutput SetChannel      (this AudioFileOutput obj, int? value, IContext context) => ConfigWishes.SetChannel      (obj, value, context);

        public static int Channel   (this AudioFileOutputChannel obj) => ConfigWishes.Channel   (obj);
        public static int GetChannel(this AudioFileOutputChannel obj) => ConfigWishes.GetChannel(obj);

        public static AudioFileOutputChannel Channel    (this AudioFileOutputChannel obj, int value) => ConfigWishes.SetChannel(obj, value);
        public static AudioFileOutputChannel WithChannel(this AudioFileOutputChannel obj, int value) => ConfigWishes.SetChannel(obj, value);
        public static AudioFileOutputChannel AsChannel  (this AudioFileOutputChannel obj, int value) => ConfigWishes.SetChannel(obj, value);
        public static AudioFileOutputChannel SetChannel (this AudioFileOutputChannel obj, int value) => ConfigWishes.SetChannel(obj, value);

        // Immutable

        
        [Obsolete(ObsoleteMessage)] public static bool IsNoChannel   (this ChannelEnum enumValue) => ConfigWishes.IsNoChannel   (enumValue);
        [Obsolete(ObsoleteMessage)] public static bool IsAnyChannel  (this ChannelEnum enumValue) => ConfigWishes.IsAnyChannel  (enumValue);
        [Obsolete(ObsoleteMessage)] public static bool IsEveryChannel(this ChannelEnum enumValue) => ConfigWishes.IsEveryChannel(enumValue);
        [Obsolete(ObsoleteMessage)] public static bool IsCenter      (this ChannelEnum enumValue) => ConfigWishes.IsCenter      (enumValue);
        [Obsolete(ObsoleteMessage)] public static bool IsLeft        (this ChannelEnum enumValue) => ConfigWishes.IsLeft        (enumValue);
        [Obsolete(ObsoleteMessage)] public static bool IsRight       (this ChannelEnum enumValue) => ConfigWishes.IsRight       (enumValue);
        [Obsolete(ObsoleteMessage)] public static int? Channel       (this ChannelEnum enumValue) => ConfigWishes.Channel       (enumValue);
        [Obsolete(ObsoleteMessage)] public static int? GetChannel    (this ChannelEnum enumValue) => ConfigWishes.GetChannel    (enumValue);
        [Obsolete(ObsoleteMessage)] public static int? EnumToChannel (this ChannelEnum enumValue) => ConfigWishes.EnumToChannel (enumValue);

        /// <inheritdoc cref="docs._quasisetter" />
        [Obsolete(ObsoleteMessage)] public static ChannelEnum AnyChannel(this ChannelEnum oldChannelEnum) => ConfigWishes.AnyChannel(oldChannelEnum);
        /// <inheritdoc cref="docs._quasisetter" />
        [Obsolete(ObsoleteMessage)] public static ChannelEnum EveryChannel(this ChannelEnum oldChannelEnum) => ConfigWishes.EveryChannel(oldChannelEnum);
        /// <inheritdoc cref="docs._quasisetter" />
        [Obsolete(ObsoleteMessage)] public static ChannelEnum NoChannel(this ChannelEnum oldChannelEnum) => ConfigWishes.NoChannel(oldChannelEnum);
        /// <inheritdoc cref="docs._quasisetter" />
        [Obsolete(ObsoleteMessage)] public static ChannelEnum Center(this ChannelEnum oldChannelEnum) => ConfigWishes.Center(oldChannelEnum);
        /// <inheritdoc cref="docs._quasisetter" />
        [Obsolete(ObsoleteMessage)] public static ChannelEnum Left(this ChannelEnum oldChannelEnum) => ConfigWishes.Left(oldChannelEnum);
        /// <inheritdoc cref="docs._quasisetter" />
        [Obsolete(ObsoleteMessage)] public static ChannelEnum Right(this ChannelEnum oldChannelEnum) => ConfigWishes.Right(oldChannelEnum);
        /// <inheritdoc cref="docs._quasisetter" />
        [Obsolete(ObsoleteMessage)] public static ChannelEnum Channel(this ChannelEnum oldChannelEnum, int? newChannelValue) => ConfigWishes.Channel(oldChannelEnum, newChannelValue);
        /// <inheritdoc cref="docs._quasisetter" />
        [Obsolete(ObsoleteMessage)] public static ChannelEnum WithNoChannel(this ChannelEnum oldChannelEnum) => ConfigWishes.WithNoChannel(oldChannelEnum);
        /// <inheritdoc cref="docs._quasisetter" />
        [Obsolete(ObsoleteMessage)] public static ChannelEnum WithAnyChannel(this ChannelEnum oldChannelEnum) => ConfigWishes.WithAnyChannel(oldChannelEnum);
        /// <inheritdoc cref="docs._quasisetter" />
        [Obsolete(ObsoleteMessage)] public static ChannelEnum WithEveryChannel(this ChannelEnum oldChannelEnum) => ConfigWishes.WithEveryChannel(oldChannelEnum);
        /// <inheritdoc cref="docs._quasisetter" />
        [Obsolete(ObsoleteMessage)] public static ChannelEnum WithCenter(this ChannelEnum oldChannelEnum) => ConfigWishes.WithCenter(oldChannelEnum);
        /// <inheritdoc cref="docs._quasisetter" />
        [Obsolete(ObsoleteMessage)] public static ChannelEnum WithLeft(this ChannelEnum oldChannelEnum) => ConfigWishes.WithLeft(oldChannelEnum);
        /// <inheritdoc cref="docs._quasisetter" />
        [Obsolete(ObsoleteMessage)] public static ChannelEnum WithRight(this ChannelEnum oldChannelEnum) => ConfigWishes.WithRight(oldChannelEnum);
        /// <inheritdoc cref="docs._quasisetter" />
        [Obsolete(ObsoleteMessage)] public static ChannelEnum WithChannel(this ChannelEnum oldChannelEnum, int? newChannelValue) => ConfigWishes.WithChannel(oldChannelEnum, newChannelValue);
        /// <inheritdoc cref="docs._quasisetter" />
        [Obsolete(ObsoleteMessage)] public static ChannelEnum AsNoChannel(this ChannelEnum oldChannelEnum) => ConfigWishes.AsNoChannel(oldChannelEnum);
        /// <inheritdoc cref="docs._quasisetter" />
        [Obsolete(ObsoleteMessage)] public static ChannelEnum AsAnyChannel(this ChannelEnum oldChannelEnum) => ConfigWishes.AsAnyChannel(oldChannelEnum);
        /// <inheritdoc cref="docs._quasisetter" />
        [Obsolete(ObsoleteMessage)] public static ChannelEnum AsEveryChannel(this ChannelEnum oldChannelEnum) => ConfigWishes.AsEveryChannel(oldChannelEnum);
        /// <inheritdoc cref="docs._quasisetter" />
        [Obsolete(ObsoleteMessage)] public static ChannelEnum AsCenter(this ChannelEnum oldChannelEnum) => ConfigWishes.AsCenter(oldChannelEnum);
        /// <inheritdoc cref="docs._quasisetter" />
        [Obsolete(ObsoleteMessage)] public static ChannelEnum AsLeft(this ChannelEnum oldChannelEnum) => ConfigWishes.AsLeft(oldChannelEnum);
        /// <inheritdoc cref="docs._quasisetter" />
        [Obsolete(ObsoleteMessage)] public static ChannelEnum AsRight(this ChannelEnum oldChannelEnum) => ConfigWishes.AsRight(oldChannelEnum);
        /// <inheritdoc cref="docs._quasisetter" />
        [Obsolete(ObsoleteMessage)] public static ChannelEnum AsChannel(this ChannelEnum oldChannelEnum, int? newChannelValue) => ConfigWishes.AsChannel(oldChannelEnum, newChannelValue);
        /// <inheritdoc cref="docs._quasisetter" />
        [Obsolete(ObsoleteMessage)] public static ChannelEnum ToNoChannel(this ChannelEnum oldChannelEnum) => ConfigWishes.ToNoChannel(oldChannelEnum);
        /// <inheritdoc cref="docs._quasisetter" />
        [Obsolete(ObsoleteMessage)] public static ChannelEnum ToAnyChannel(this ChannelEnum oldChannelEnum) => ConfigWishes.ToAnyChannel(oldChannelEnum);
        /// <inheritdoc cref="docs._quasisetter" />
        [Obsolete(ObsoleteMessage)] public static ChannelEnum ToEveryChannel(this ChannelEnum oldChannelEnum) => ConfigWishes.ToEveryChannel(oldChannelEnum);
        /// <inheritdoc cref="docs._quasisetter" />
        [Obsolete(ObsoleteMessage)] public static ChannelEnum ToChannelsEmpty(this ChannelEnum oldChannelEnum) => ConfigWishes.ToChannelsEmpty(oldChannelEnum);
        /// <inheritdoc cref="docs._quasisetter" />
        [Obsolete(ObsoleteMessage)] public static ChannelEnum ToCenter(this ChannelEnum oldChannelEnum) => ConfigWishes.ToCenter(oldChannelEnum);
        /// <inheritdoc cref="docs._quasisetter" />
        [Obsolete(ObsoleteMessage)] public static ChannelEnum ToLeft(this ChannelEnum oldChannelEnum) => ConfigWishes.ToLeft(oldChannelEnum);
        /// <inheritdoc cref="docs._quasisetter" />
        [Obsolete(ObsoleteMessage)] public static ChannelEnum ToRight(this ChannelEnum oldChannelEnum) => ConfigWishes.ToRight(oldChannelEnum);
        /// <inheritdoc cref="docs._quasisetter" />
        [Obsolete(ObsoleteMessage)] public static ChannelEnum ToChannel(this ChannelEnum oldChannelEnum, int? newChannelValue) => ConfigWishes.ToChannel(oldChannelEnum, newChannelValue);
        /// <inheritdoc cref="docs._quasisetter" />
        [Obsolete(ObsoleteMessage)] public static ChannelEnum SetAnyChannel(this ChannelEnum oldChannelEnum) => ConfigWishes.SetAnyChannel(oldChannelEnum);
        /// <inheritdoc cref="docs._quasisetter" />
        [Obsolete(ObsoleteMessage)] public static ChannelEnum SetEveryChannel(this ChannelEnum oldChannelEnum) => ConfigWishes.SetEveryChannel(oldChannelEnum);
        /// <inheritdoc cref="docs._quasisetter" />
        [Obsolete(ObsoleteMessage)] public static ChannelEnum SetNoChannel(this ChannelEnum oldChannelEnum) => ConfigWishes.SetNoChannel(oldChannelEnum);
        /// <inheritdoc cref="docs._quasisetter" />
        [Obsolete(ObsoleteMessage)] public static ChannelEnum SetCenter(this ChannelEnum oldChannelEnum) => ConfigWishes.SetCenter(oldChannelEnum);
        /// <inheritdoc cref="docs._quasisetter" />
        [Obsolete(ObsoleteMessage)] public static ChannelEnum SetLeft(this ChannelEnum oldChannelEnum) => ConfigWishes.SetLeft(oldChannelEnum);
        /// <inheritdoc cref="docs._quasisetter" />
        [Obsolete(ObsoleteMessage)] public static ChannelEnum SetRight(this ChannelEnum oldChannelEnum) => ConfigWishes.SetRight(oldChannelEnum);
        /// <inheritdoc cref="docs._quasisetter" />
        [Obsolete(ObsoleteMessage)] public static ChannelEnum SetChannel(this ChannelEnum oldChannelEnum, int? newChannelValue) => ConfigWishes.SetChannel(oldChannelEnum, newChannelValue);
        /// <inheritdoc cref="docs._quasisetter" />
        [Obsolete(ObsoleteMessage)] public static ChannelEnum ChannelToEnum(this int? thisChannel, int channelsForContext) => ConfigWishes.ChannelToEnum(thisChannel, channelsForContext);

        [Obsolete(ObsoleteMessage)] public static bool IsNoChannel    (this Channel entity) => ConfigWishes.IsNoChannel    (entity);
        [Obsolete(ObsoleteMessage)] public static bool IsAnyChannel   (this Channel entity) => ConfigWishes.IsAnyChannel   (entity);
        [Obsolete(ObsoleteMessage)] public static bool IsEveryChannel (this Channel entity) => ConfigWishes.IsEveryChannel (entity);
        [Obsolete(ObsoleteMessage)] public static bool IsCenter       (this Channel entity) => ConfigWishes.IsCenter       (entity);
        [Obsolete(ObsoleteMessage)] public static bool IsLeft         (this Channel entity) => ConfigWishes.IsLeft         (entity);
        [Obsolete(ObsoleteMessage)] public static bool IsRight        (this Channel entity) => ConfigWishes.IsRight        (entity);
        [Obsolete(ObsoleteMessage)] public static int? Channel        (this Channel entity) => ConfigWishes.Channel        (entity);
        [Obsolete(ObsoleteMessage)] public static int? GetChannel     (this Channel entity) => ConfigWishes.GetChannel     (entity);
        [Obsolete(ObsoleteMessage)] public static int? EntityToChannel(this Channel entity) => ConfigWishes.EntityToChannel(entity);
        
        /// <inheritdoc cref="docs._quasisetter" />
        [Obsolete(ObsoleteMessage)] public static Channel AnyChannel(this Channel oldChannelEntity) => ConfigWishes.AnyChannel(oldChannelEntity);
        /// <inheritdoc cref="docs._quasisetter" />
        [Obsolete(ObsoleteMessage)] public static Channel EveryChannel(this Channel oldChannelEntity) => ConfigWishes.EveryChannel(oldChannelEntity);
        /// <inheritdoc cref="docs._quasisetter" />
        [Obsolete(ObsoleteMessage)] public static Channel NoChannel(this Channel oldChannelEntity) => ConfigWishes.NoChannel(oldChannelEntity);
        /// <inheritdoc cref="docs._quasisetter" />
        [Obsolete(ObsoleteMessage)] public static Channel Center(this Channel oldChannelEntity, IContext context) => ConfigWishes.Center(oldChannelEntity, context);
        /// <inheritdoc cref="docs._quasisetter" />
        [Obsolete(ObsoleteMessage)] public static Channel Left(this Channel oldChannelEntity, IContext context) => ConfigWishes.Left(oldChannelEntity, context);
        /// <inheritdoc cref="docs._quasisetter" />
        [Obsolete(ObsoleteMessage)] public static Channel Right(this Channel oldChannelEntity, IContext context) => ConfigWishes.Right(oldChannelEntity, context);
        /// <inheritdoc cref="docs._quasisetter" />
        [Obsolete(ObsoleteMessage)] public static Channel Channel(this Channel oldChannelEntity, int? newChannelValue, IContext context) => ConfigWishes.Channel(oldChannelEntity, newChannelValue, context);
        /// <inheritdoc cref="docs._quasisetter" />
        [Obsolete(ObsoleteMessage)] public static Channel WithNoChannel(this Channel oldChannelEntity) => ConfigWishes.WithNoChannel(oldChannelEntity);
        /// <inheritdoc cref="docs._quasisetter" />
        [Obsolete(ObsoleteMessage)] public static Channel WithAnyChannel(this Channel oldChannelEntity) => ConfigWishes.WithAnyChannel(oldChannelEntity);
        /// <inheritdoc cref="docs._quasisetter" />
        [Obsolete(ObsoleteMessage)] public static Channel WithEveryChannel(this Channel oldChannelEntity) => ConfigWishes.WithEveryChannel(oldChannelEntity);
        /// <inheritdoc cref="docs._quasisetter" />
        [Obsolete(ObsoleteMessage)] public static Channel WithCenter(this Channel oldChannelEntity, IContext context) => ConfigWishes.WithCenter(oldChannelEntity, context);
        /// <inheritdoc cref="docs._quasisetter" />
        [Obsolete(ObsoleteMessage)] public static Channel WithLeft(this Channel oldChannelEntity, IContext context) => ConfigWishes.WithLeft(oldChannelEntity, context);
        /// <inheritdoc cref="docs._quasisetter" />
        [Obsolete(ObsoleteMessage)] public static Channel WithRight(this Channel oldChannelEntity, IContext context) => ConfigWishes.WithRight(oldChannelEntity, context);
        /// <inheritdoc cref="docs._quasisetter" />
        [Obsolete(ObsoleteMessage)] public static Channel WithChannel(this Channel oldChannelEntity, int? newChannelValue, IContext context) => ConfigWishes.WithChannel(oldChannelEntity, newChannelValue, context);
        /// <inheritdoc cref="docs._quasisetter" />
        [Obsolete(ObsoleteMessage)] public static Channel AsNoChannel(this Channel oldChannelEntity) => ConfigWishes.AsNoChannel(oldChannelEntity);
        /// <inheritdoc cref="docs._quasisetter" />
        [Obsolete(ObsoleteMessage)] public static Channel AsAnyChannel(this Channel oldChannelEntity) => ConfigWishes.AsAnyChannel(oldChannelEntity);
        /// <inheritdoc cref="docs._quasisetter" />
        [Obsolete(ObsoleteMessage)] public static Channel AsEveryChannel(this Channel oldChannelEntity) => ConfigWishes.AsEveryChannel(oldChannelEntity);
        /// <inheritdoc cref="docs._quasisetter" />
        [Obsolete(ObsoleteMessage)] public static Channel AsCenter(this Channel oldChannelEntity, IContext context) => ConfigWishes.AsCenter(oldChannelEntity, context);
        /// <inheritdoc cref="docs._quasisetter" />
        [Obsolete(ObsoleteMessage)] public static Channel AsLeft(this Channel oldChannelEntity, IContext context) => ConfigWishes.AsLeft(oldChannelEntity, context);
        /// <inheritdoc cref="docs._quasisetter" />
        [Obsolete(ObsoleteMessage)] public static Channel AsRight(this Channel oldChannelEntity, IContext context) => ConfigWishes.AsRight(oldChannelEntity, context);
        /// <inheritdoc cref="docs._quasisetter" />
        [Obsolete(ObsoleteMessage)] public static Channel AsChannel(this Channel oldChannelEntity, int? newChannelValue, IContext context) => ConfigWishes.AsChannel(oldChannelEntity, newChannelValue, context);
        /// <inheritdoc cref="docs._quasisetter" />
        [Obsolete(ObsoleteMessage)] public static Channel ToNoChannel(this Channel oldChannelEntity) => ConfigWishes.ToNoChannel(oldChannelEntity);
        /// <inheritdoc cref="docs._quasisetter" />
        [Obsolete(ObsoleteMessage)] public static Channel ToAnyChannel(this Channel oldChannelEntity) => ConfigWishes.ToAnyChannel(oldChannelEntity);
        /// <inheritdoc cref="docs._quasisetter" />
        [Obsolete(ObsoleteMessage)] public static Channel ToEveryChannel(this Channel oldChannelEntity) => ConfigWishes.ToEveryChannel(oldChannelEntity);
        /// <inheritdoc cref="docs._quasisetter" />
        [Obsolete(ObsoleteMessage)] public static Channel ToCenter(this Channel oldChannelEntity, IContext context) => ConfigWishes.ToCenter(oldChannelEntity, context);
        /// <inheritdoc cref="docs._quasisetter" />
        [Obsolete(ObsoleteMessage)] public static Channel ToLeft(this Channel oldChannelEntity, IContext context) => ConfigWishes.ToLeft(oldChannelEntity, context);
        /// <inheritdoc cref="docs._quasisetter" />
        [Obsolete(ObsoleteMessage)] public static Channel ToRight(this Channel oldChannelEntity, IContext context) => ConfigWishes.ToRight(oldChannelEntity, context);
        /// <inheritdoc cref="docs._quasisetter" />
        [Obsolete(ObsoleteMessage)] public static Channel ToChannel(this Channel oldChannelEntity, int? newChannelValue, IContext context) => ConfigWishes.ToChannel(oldChannelEntity, newChannelValue, context);
        /// <inheritdoc cref="docs._quasisetter" />
        [Obsolete(ObsoleteMessage)] public static Channel SetNoChannel(this Channel oldChannelEntity) => ConfigWishes.SetNoChannel(oldChannelEntity);
        /// <inheritdoc cref="docs._quasisetter" />
        [Obsolete(ObsoleteMessage)] public static Channel SetAnyChannel(this Channel oldChannelEntity) => ConfigWishes.SetAnyChannel(oldChannelEntity);
        /// <inheritdoc cref="docs._quasisetter" />
        [Obsolete(ObsoleteMessage)] public static Channel SetEveryChannel(this Channel oldChannelEntity) => ConfigWishes.SetEveryChannel(oldChannelEntity);
        /// <inheritdoc cref="docs._quasisetter" />
        [Obsolete(ObsoleteMessage)] public static Channel SetCenter(this Channel oldChannelEntity, IContext context) => ConfigWishes.SetCenter(oldChannelEntity, context);
        /// <inheritdoc cref="docs._quasisetter" />
        [Obsolete(ObsoleteMessage)] public static Channel SetLeft(this Channel oldChannelEntity, IContext context) => ConfigWishes.SetLeft(oldChannelEntity, context);
        /// <inheritdoc cref="docs._quasisetter" />
        [Obsolete(ObsoleteMessage)] public static Channel SetRight(this Channel oldChannelEntity, IContext context) => ConfigWishes.SetRight(oldChannelEntity, context);
        /// <inheritdoc cref="docs._quasisetter" />
        [Obsolete(ObsoleteMessage)] public static Channel SetChannel(this Channel oldChannelEntity, int? newChannelValue, IContext context) => ConfigWishes.SetChannel(oldChannelEntity, newChannelValue, context);

        [Obsolete(ObsoleteMessage)] public static Channel ChannelToEntity(this int? thisChannel, int channelsForContext, IContext context) => ConfigWishes.ChannelToEntity(thisChannel, channelsForContext, context);
    }
    
    public partial class ConfigWishes
    { 
        // Constants
        
        public const int CenterChannel = 0;
        public const int LeftChannel = 0;
        public const int RightChannel = 1;
        public static readonly int? EmptyChannel = null;
        
        // Synth-Bound
        
        public static bool IsAnyChannel  (SynthWishes obj) => IsNoChannel(obj);
        public static bool IsEveryChannel(SynthWishes obj) => IsNoChannel(obj);
        public static bool IsNoChannel   (SynthWishes obj) => GetChannel(obj) == EmptyChannel  && IsStereo(obj);
        public static bool IsCenter      (SynthWishes obj) => GetChannel(obj) == CenterChannel && IsMono  (obj);
        public static bool IsLeft        (SynthWishes obj) => GetChannel(obj) == LeftChannel   && IsStereo(obj);
        public static bool IsRight       (SynthWishes obj) => GetChannel(obj) == RightChannel  && IsStereo(obj);
        public static int? Channel       (SynthWishes obj) => GetChannel(obj);
        public static int? GetChannel    (SynthWishes obj)
        {
            if (obj == null) throw new NullException(() => obj);
            return obj.GetChannel;
        }
        
        public static SynthWishes AnyChannel      (SynthWishes obj            ) => SetNoChannel(obj);
        public static SynthWishes EveryChannel    (SynthWishes obj            ) => SetNoChannel(obj);
        public static SynthWishes NoChannel       (SynthWishes obj            ) => SetNoChannel(obj);
        public static SynthWishes Center          (SynthWishes obj            ) => SetCenter   (obj);
        public static SynthWishes Left            (SynthWishes obj            ) => SetLeft     (obj);
        public static SynthWishes Right           (SynthWishes obj            ) => SetRight    (obj);
        public static SynthWishes Channel         (SynthWishes obj, int? value) => SetChannel  (obj, value);
        public static SynthWishes WithNoChannel   (SynthWishes obj            ) => SetNoChannel(obj);
        public static SynthWishes WithAnyChannel  (SynthWishes obj            ) => SetNoChannel(obj);
        public static SynthWishes WithEveryChannel(SynthWishes obj            ) => SetNoChannel(obj);
        public static SynthWishes WithCenter      (SynthWishes obj            ) => SetCenter   (obj);
        public static SynthWishes WithLeft        (SynthWishes obj            ) => SetLeft     (obj);
        public static SynthWishes WithRight       (SynthWishes obj            ) => SetRight    (obj);
        public static SynthWishes WithChannel     (SynthWishes obj, int? value) => SetChannel  (obj, value);
        public static SynthWishes AsNoChannel     (SynthWishes obj            ) => SetNoChannel(obj);
        public static SynthWishes AsAnyChannel    (SynthWishes obj            ) => SetNoChannel(obj);
        public static SynthWishes AsEveryChannel  (SynthWishes obj            ) => SetNoChannel(obj);
        public static SynthWishes AsCenter        (SynthWishes obj            ) => SetCenter   (obj);
        public static SynthWishes AsLeft          (SynthWishes obj            ) => SetLeft     (obj);
        public static SynthWishes AsRight         (SynthWishes obj            ) => SetRight    (obj);
        public static SynthWishes AsChannel       (SynthWishes obj, int? value) => SetChannel  (obj, value);
        public static SynthWishes SetAnyChannel   (SynthWishes obj            ) => SetNoChannel(obj);
        public static SynthWishes SetEveryChannel (SynthWishes obj            ) => SetNoChannel(obj);
        public static SynthWishes SetNoChannel    (SynthWishes obj            ) => SetChannel  (obj, EmptyChannel ).Stereo();
        public static SynthWishes SetCenter       (SynthWishes obj            ) => SetChannel  (obj, CenterChannel).Mono  ();
        public static SynthWishes SetLeft         (SynthWishes obj            ) => SetChannel  (obj, LeftChannel  ).Stereo();
        public static SynthWishes SetRight        (SynthWishes obj            ) => SetChannel  (obj, RightChannel ).Stereo();
        public static SynthWishes SetChannel      (SynthWishes obj, int? value)
        {
            if (obj == null) throw new NullException(() => obj);
            return obj.WithChannel(value);
        }

        public static bool IsAnyChannel  (FlowNode obj) => IsNoChannel(obj);
        public static bool IsEveryChannel(FlowNode obj) => IsNoChannel(obj);
        public static bool IsNoChannel   (FlowNode obj) => GetChannel(obj) == EmptyChannel  && IsStereo(obj);
        public static bool IsCenter      (FlowNode obj) => GetChannel(obj) == CenterChannel && IsMono  (obj);
        public static bool IsLeft        (FlowNode obj) => GetChannel(obj) == LeftChannel   && IsStereo(obj);
        public static bool IsRight       (FlowNode obj) => GetChannel(obj) == RightChannel  && IsStereo(obj);
        public static int? Channel       (FlowNode obj) => GetChannel(obj);
        public static int? GetChannel    (FlowNode obj)
        {
            if (obj == null) throw new NullException(() => obj);
            return obj.GetChannel;
        }
        
        public static FlowNode AnyChannel      (FlowNode obj            ) => SetNoChannel(obj);
        public static FlowNode EveryChannel    (FlowNode obj            ) => SetNoChannel(obj);
        public static FlowNode NoChannel       (FlowNode obj            ) => SetNoChannel(obj);
        public static FlowNode Center          (FlowNode obj            ) => SetCenter      (obj);
        public static FlowNode Left            (FlowNode obj            ) => SetLeft        (obj);
        public static FlowNode Right           (FlowNode obj            ) => SetRight       (obj);
        public static FlowNode Channel         (FlowNode obj, int? value) => SetChannel     (obj, value);
        public static FlowNode WithNoChannel   (FlowNode obj            ) => SetNoChannel(obj);
        public static FlowNode WithAnyChannel  (FlowNode obj            ) => SetNoChannel(obj);
        public static FlowNode WithEveryChannel(FlowNode obj            ) => SetNoChannel(obj);
        public static FlowNode WithCenter      (FlowNode obj            ) => SetCenter      (obj);
        public static FlowNode WithLeft        (FlowNode obj            ) => SetLeft        (obj);
        public static FlowNode WithRight       (FlowNode obj            ) => SetRight       (obj);
        public static FlowNode WithChannel     (FlowNode obj, int? value) => SetChannel     (obj, value);
        public static FlowNode AsNoChannel     (FlowNode obj            ) => SetNoChannel(obj);
        public static FlowNode AsAnyChannel    (FlowNode obj            ) => SetNoChannel(obj);
        public static FlowNode AsEveryChannel  (FlowNode obj            ) => SetNoChannel(obj);
        public static FlowNode AsCenter        (FlowNode obj            ) => SetCenter      (obj);
        public static FlowNode AsLeft          (FlowNode obj            ) => SetLeft        (obj);
        public static FlowNode AsRight         (FlowNode obj            ) => SetRight       (obj);
        public static FlowNode AsChannel       (FlowNode obj, int? value) => SetChannel     (obj, value);
        public static FlowNode SetAnyChannel   (FlowNode obj            ) => SetNoChannel(obj);
        public static FlowNode SetEveryChannel (FlowNode obj            ) => SetNoChannel(obj);
        public static FlowNode SetNoChannel    (FlowNode obj            ) => SetChannel     (obj, EmptyChannel ).Stereo();
        public static FlowNode SetCenter       (FlowNode obj            ) => SetChannel     (obj, CenterChannel).Mono  ();
        public static FlowNode SetLeft         (FlowNode obj            ) => SetChannel     (obj, LeftChannel  ).Stereo();
        public static FlowNode SetRight        (FlowNode obj            ) => SetChannel     (obj, RightChannel ).Stereo();
        public static FlowNode SetChannel      (FlowNode obj, int? value)
        {
            if (obj == null) throw new NullException(() => obj);
            return obj.WithChannel(value);
        }
        
        [UsedImplicitly] internal static bool IsAnyChannel  (ConfigResolver obj) => IsNoChannel(obj);
        [UsedImplicitly] internal static bool IsEveryChannel(ConfigResolver obj) => IsNoChannel(obj);
        [UsedImplicitly] internal static bool IsNoChannel   (ConfigResolver obj) => GetChannel(obj) == EmptyChannel  && IsStereo(obj);
        [UsedImplicitly] internal static bool IsCenter      (ConfigResolver obj) => GetChannel(obj) == CenterChannel && IsMono  (obj);
        [UsedImplicitly] internal static bool IsLeft        (ConfigResolver obj) => GetChannel(obj) == LeftChannel   && IsStereo(obj);
        [UsedImplicitly] internal static bool IsRight       (ConfigResolver obj) => GetChannel(obj) == RightChannel  && IsStereo(obj);
        [UsedImplicitly] internal static int? Channel       (ConfigResolver obj) => GetChannel(obj);
        [UsedImplicitly] internal static int? GetChannel    (ConfigResolver obj)
        {
            if (obj == null) throw new NullException(() => obj);
            return obj.GetChannel;
        }
        
        [UsedImplicitly] internal static ConfigResolver AnyChannel      (ConfigResolver obj            ) => SetNoChannel(obj);
        [UsedImplicitly] internal static ConfigResolver EveryChannel    (ConfigResolver obj            ) => SetNoChannel(obj);
        [UsedImplicitly] internal static ConfigResolver NoChannel       (ConfigResolver obj            ) => SetNoChannel(obj);
        [UsedImplicitly] internal static ConfigResolver Center          (ConfigResolver obj            ) => SetCenter      (obj);
        [UsedImplicitly] internal static ConfigResolver Left            (ConfigResolver obj            ) => SetLeft        (obj);
        [UsedImplicitly] internal static ConfigResolver Right           (ConfigResolver obj            ) => SetRight       (obj);
        [UsedImplicitly] internal static ConfigResolver Channel         (ConfigResolver obj, int? value) => SetChannel     (obj, value);
        [UsedImplicitly] internal static ConfigResolver WithNoChannel   (ConfigResolver obj            ) => SetNoChannel(obj);
        [UsedImplicitly] internal static ConfigResolver WithAnyChannel  (ConfigResolver obj            ) => SetNoChannel(obj);
        [UsedImplicitly] internal static ConfigResolver WithEveryChannel(ConfigResolver obj            ) => SetNoChannel(obj);
        [UsedImplicitly] internal static ConfigResolver WithCenter      (ConfigResolver obj            ) => SetCenter      (obj);
        [UsedImplicitly] internal static ConfigResolver WithLeft        (ConfigResolver obj            ) => SetLeft        (obj);
        [UsedImplicitly] internal static ConfigResolver WithRight       (ConfigResolver obj            ) => SetRight       (obj);
        [UsedImplicitly] internal static ConfigResolver WithChannel     (ConfigResolver obj, int? value) => SetChannel     (obj, value);
        [UsedImplicitly] internal static ConfigResolver AsNoChannel     (ConfigResolver obj            ) => SetNoChannel(obj);
        [UsedImplicitly] internal static ConfigResolver AsAnyChannel    (ConfigResolver obj            ) => SetNoChannel(obj);
        [UsedImplicitly] internal static ConfigResolver AsEveryChannel  (ConfigResolver obj            ) => SetNoChannel(obj);
        [UsedImplicitly] internal static ConfigResolver AsCenter        (ConfigResolver obj            ) => SetCenter      (obj);
        [UsedImplicitly] internal static ConfigResolver AsLeft          (ConfigResolver obj            ) => SetLeft        (obj);
        [UsedImplicitly] internal static ConfigResolver AsRight         (ConfigResolver obj            ) => SetRight       (obj);
        [UsedImplicitly] internal static ConfigResolver AsChannel       (ConfigResolver obj, int? value) => SetChannel     (obj, value);
        [UsedImplicitly] internal static ConfigResolver SetAnyChannel   (ConfigResolver obj            ) => SetNoChannel(obj);
        [UsedImplicitly] internal static ConfigResolver SetEveryChannel (ConfigResolver obj            ) => SetNoChannel(obj);
        [UsedImplicitly] internal static ConfigResolver SetNoChannel    (ConfigResolver obj            ) => SetChannel     (obj, EmptyChannel ).Stereo();
        [UsedImplicitly] internal static ConfigResolver SetCenter       (ConfigResolver obj            ) => SetChannel     (obj, CenterChannel).Mono  ();
        [UsedImplicitly] internal static ConfigResolver SetLeft         (ConfigResolver obj            ) => SetChannel     (obj, LeftChannel  ).Stereo();
        [UsedImplicitly] internal static ConfigResolver SetRight        (ConfigResolver obj            ) => SetChannel     (obj, RightChannel ).Stereo();
        [UsedImplicitly] internal static ConfigResolver SetChannel      (ConfigResolver obj, int? value)
        {
            if (obj == null) throw new NullException(() => obj);
            return obj.WithChannel(value);
        }
        
        // Tape-Bound
        
        public static bool IsAnyChannel  (Tape obj) => IsNoChannel(obj);
        public static bool IsEveryChannel(Tape obj) => IsNoChannel(obj);
        public static bool IsNoChannel   (Tape obj) => GetChannel(obj) == EmptyChannel  && IsStereo(obj);
        public static bool IsCenter      (Tape obj) => GetChannel(obj) == CenterChannel && IsMono  (obj);
        public static bool IsLeft        (Tape obj) => GetChannel(obj) == LeftChannel   && IsStereo(obj);
        public static bool IsRight       (Tape obj) => GetChannel(obj) == RightChannel  && IsStereo(obj);
        public static int? Channel       (Tape obj) => GetChannel(obj);
        public static int? GetChannel    (Tape obj)
        {
            if (obj == null) throw new NullException(() => obj);
            return obj.Config.Channel;
        }
        
        public static Tape AnyChannel      (Tape obj            ) => SetNoChannel(obj);
        public static Tape EveryChannel    (Tape obj            ) => SetNoChannel(obj);
        public static Tape NoChannel       (Tape obj            ) => SetNoChannel(obj);
        public static Tape Center          (Tape obj            ) => SetCenter      (obj);
        public static Tape Left            (Tape obj            ) => SetLeft        (obj);
        public static Tape Right           (Tape obj            ) => SetRight       (obj);
        public static Tape Channel         (Tape obj, int? value) => SetChannel     (obj, value);
        public static Tape WithNoChannel   (Tape obj            ) => SetNoChannel(obj);
        public static Tape WithAnyChannel  (Tape obj            ) => SetNoChannel(obj);
        public static Tape WithEveryChannel(Tape obj            ) => SetNoChannel(obj);
        public static Tape WithCenter      (Tape obj            ) => SetCenter      (obj);
        public static Tape WithLeft        (Tape obj            ) => SetLeft        (obj);
        public static Tape WithRight       (Tape obj            ) => SetRight       (obj);
        public static Tape WithChannel     (Tape obj, int? value) => SetChannel     (obj, value);
        public static Tape AsNoChannel     (Tape obj            ) => SetNoChannel(obj);
        public static Tape AsAnyChannel    (Tape obj            ) => SetNoChannel(obj);
        public static Tape AsEveryChannel  (Tape obj            ) => SetNoChannel(obj);
        public static Tape AsCenter        (Tape obj            ) => SetCenter      (obj);
        public static Tape AsLeft          (Tape obj            ) => SetLeft        (obj);
        public static Tape AsRight         (Tape obj            ) => SetRight       (obj);
        public static Tape AsChannel       (Tape obj, int? value) => SetChannel     (obj, value);
        public static Tape SetAnyChannel   (Tape obj            ) => SetNoChannel(obj);
        public static Tape SetEveryChannel (Tape obj            ) => SetNoChannel(obj);
        public static Tape SetNoChannel    (Tape obj            ) => SetChannel     (obj, EmptyChannel ).Stereo();
        public static Tape SetCenter       (Tape obj            ) => SetChannel     (obj, CenterChannel).Mono  ();
        public static Tape SetLeft         (Tape obj            ) => SetChannel     (obj, LeftChannel  ).Stereo();
        public static Tape SetRight        (Tape obj            ) => SetChannel     (obj, RightChannel ).Stereo();
        public static Tape SetChannel      (Tape obj, int? value)
        {
            if (obj == null) throw new NullException(() => obj);
            obj.Config.Channel = value;
            return obj;
        }
        
        public static bool IsAnyChannel  (TapeConfig obj) => IsNoChannel(obj);
        public static bool IsEveryChannel(TapeConfig obj) => IsNoChannel(obj);
        public static bool IsNoChannel   (TapeConfig obj) => GetChannel(obj) == EmptyChannel  && IsStereo(obj);
        public static bool IsCenter      (TapeConfig obj) => GetChannel(obj) == CenterChannel && IsMono  (obj);
        public static bool IsLeft        (TapeConfig obj) => GetChannel(obj) == LeftChannel   && IsStereo(obj);
        public static bool IsRight       (TapeConfig obj) => GetChannel(obj) == RightChannel  && IsStereo(obj);
        public static int? Channel       (TapeConfig obj) => GetChannel(obj);
        public static int? GetChannel    (TapeConfig obj)
        {
            if (obj == null) throw new NullException(() => obj);
            return obj.Channel;
        }
        
        public static TapeConfig AnyChannel      (TapeConfig obj            ) => SetNoChannel(obj);
        public static TapeConfig EveryChannel    (TapeConfig obj            ) => SetNoChannel(obj);
        public static TapeConfig NoChannel       (TapeConfig obj            ) => SetNoChannel(obj);
        public static TapeConfig Center          (TapeConfig obj            ) => SetCenter      (obj);
        public static TapeConfig Left            (TapeConfig obj            ) => SetLeft        (obj);
        public static TapeConfig Right           (TapeConfig obj            ) => SetRight       (obj);
        public static TapeConfig Channel         (TapeConfig obj, int? value) => SetChannel     (obj, value);
        public static TapeConfig WithNoChannel   (TapeConfig obj            ) => SetNoChannel(obj);
        public static TapeConfig WithAnyChannel  (TapeConfig obj            ) => SetNoChannel(obj);
        public static TapeConfig WithEveryChannel(TapeConfig obj            ) => SetNoChannel(obj);
        public static TapeConfig WithCenter      (TapeConfig obj            ) => SetCenter      (obj);
        public static TapeConfig WithLeft        (TapeConfig obj            ) => SetLeft        (obj);
        public static TapeConfig WithRight       (TapeConfig obj            ) => SetRight       (obj);
        public static TapeConfig WithChannel     (TapeConfig obj, int? value) => SetChannel     (obj, value);
        public static TapeConfig AsNoChannel     (TapeConfig obj            ) => SetNoChannel(obj);
        public static TapeConfig AsAnyChannel    (TapeConfig obj            ) => SetNoChannel(obj);
        public static TapeConfig AsEveryChannel  (TapeConfig obj            ) => SetNoChannel(obj);
        public static TapeConfig AsCenter        (TapeConfig obj            ) => SetCenter      (obj);
        public static TapeConfig AsLeft          (TapeConfig obj            ) => SetLeft        (obj);
        public static TapeConfig AsRight         (TapeConfig obj            ) => SetRight       (obj);
        public static TapeConfig AsChannel       (TapeConfig obj, int? value) => SetChannel     (obj, value);
        public static TapeConfig SetAnyChannel   (TapeConfig obj            ) => SetNoChannel(obj);
        public static TapeConfig SetEveryChannel (TapeConfig obj            ) => SetNoChannel(obj);
        public static TapeConfig SetNoChannel    (TapeConfig obj            ) => SetChannel     (obj, EmptyChannel ).Stereo();
        public static TapeConfig SetCenter       (TapeConfig obj            ) => SetChannel     (obj, CenterChannel).Mono  ();
        public static TapeConfig SetLeft         (TapeConfig obj            ) => SetChannel     (obj, LeftChannel  ).Stereo();
        public static TapeConfig SetRight        (TapeConfig obj            ) => SetChannel     (obj, RightChannel ).Stereo();
        public static TapeConfig SetChannel      (TapeConfig obj, int? value)
        {
            if (obj == null) throw new NullException(() => obj);
            obj.Channel = value;
            return obj;
        }
        
        public static bool IsAnyChannel  (TapeActions obj) => IsNoChannel(obj);
        public static bool IsEveryChannel(TapeActions obj) => IsNoChannel(obj);
        public static bool IsNoChannel   (TapeActions obj) => GetChannel(obj) == EmptyChannel  && IsStereo(obj);
        public static bool IsCenter      (TapeActions obj) => GetChannel(obj) == CenterChannel && IsMono  (obj);
        public static bool IsLeft        (TapeActions obj) => GetChannel(obj) == LeftChannel   && IsStereo(obj);
        public static bool IsRight       (TapeActions obj) => GetChannel(obj) == RightChannel  && IsStereo(obj);
        public static int? Channel       (TapeActions obj) => GetChannel(obj);
        public static int? GetChannel    (TapeActions obj)
        {
            if (obj == null) throw new NullException(() => obj);
            return obj.Tape.Config.Channel;
        }
        
        public static TapeActions AnyChannel      (TapeActions obj            ) => SetNoChannel(obj);
        public static TapeActions EveryChannel    (TapeActions obj            ) => SetNoChannel(obj);
        public static TapeActions NoChannel       (TapeActions obj            ) => SetNoChannel(obj);
        public static TapeActions Center          (TapeActions obj            ) => SetCenter      (obj);
        public static TapeActions Left            (TapeActions obj            ) => SetLeft        (obj);
        public static TapeActions Right           (TapeActions obj            ) => SetRight       (obj);
        public static TapeActions Channel         (TapeActions obj, int? value) => SetChannel     (obj, value);
        public static TapeActions WithNoChannel   (TapeActions obj            ) => SetNoChannel(obj);
        public static TapeActions WithAnyChannel  (TapeActions obj            ) => SetNoChannel(obj);
        public static TapeActions WithEveryChannel(TapeActions obj            ) => SetNoChannel(obj);
        public static TapeActions WithCenter      (TapeActions obj            ) => SetCenter      (obj);
        public static TapeActions WithLeft        (TapeActions obj            ) => SetLeft        (obj);
        public static TapeActions WithRight       (TapeActions obj            ) => SetRight       (obj);
        public static TapeActions WithChannel     (TapeActions obj, int? value) => SetChannel     (obj, value);
        public static TapeActions AsNoChannel     (TapeActions obj            ) => SetNoChannel(obj);
        public static TapeActions AsAnyChannel    (TapeActions obj            ) => SetNoChannel(obj);
        public static TapeActions AsEveryChannel  (TapeActions obj            ) => SetNoChannel(obj);
        public static TapeActions AsCenter        (TapeActions obj            ) => SetCenter      (obj);
        public static TapeActions AsLeft          (TapeActions obj            ) => SetLeft        (obj);
        public static TapeActions AsRight         (TapeActions obj            ) => SetRight       (obj);
        public static TapeActions AsChannel       (TapeActions obj, int? value) => SetChannel     (obj, value);
        public static TapeActions SetAnyChannel   (TapeActions obj            ) => SetNoChannel(obj);
        public static TapeActions SetEveryChannel (TapeActions obj            ) => SetNoChannel(obj);
        public static TapeActions SetNoChannel    (TapeActions obj            ) => SetChannel     (obj, EmptyChannel ).Stereo();
        public static TapeActions SetCenter       (TapeActions obj            ) => SetChannel     (obj, CenterChannel).Mono  ();
        public static TapeActions SetLeft         (TapeActions obj            ) => SetChannel     (obj, LeftChannel  ).Stereo();
        public static TapeActions SetRight        (TapeActions obj            ) => SetChannel     (obj, RightChannel ).Stereo();
        public static TapeActions SetChannel      (TapeActions obj, int? value)
        {
            if (obj == null) throw new NullException(() => obj);
            obj.Tape.Config.Channel = value;
            return obj;
        }
        
        public static bool IsAnyChannel  (TapeAction obj) => IsNoChannel(obj);
        public static bool IsEveryChannel(TapeAction obj) => IsNoChannel(obj);
        public static bool IsNoChannel   (TapeAction obj) => GetChannel(obj) == EmptyChannel  && IsStereo(obj);
        public static bool IsCenter      (TapeAction obj) => GetChannel(obj) == CenterChannel && IsMono  (obj);
        public static bool IsLeft        (TapeAction obj) => GetChannel(obj) == LeftChannel   && IsStereo(obj);
        public static bool IsRight       (TapeAction obj) => GetChannel(obj) == RightChannel  && IsStereo(obj);
        public static int? Channel       (TapeAction obj) => GetChannel(obj);
        public static int? GetChannel    (TapeAction obj)
        {
            if (obj == null) throw new NullException(() => obj);
            return obj.Tape.Config.Channel;
        }
        
        public static TapeAction AnyChannel      (TapeAction obj            ) => SetNoChannel(obj);
        public static TapeAction EveryChannel    (TapeAction obj            ) => SetNoChannel(obj);
        public static TapeAction NoChannel       (TapeAction obj            ) => SetNoChannel(obj);
        public static TapeAction Center          (TapeAction obj            ) => SetCenter      (obj);
        public static TapeAction Left            (TapeAction obj            ) => SetLeft        (obj);
        public static TapeAction Right           (TapeAction obj            ) => SetRight       (obj);
        public static TapeAction Channel         (TapeAction obj, int? value) => SetChannel     (obj, value);
        public static TapeAction WithNoChannel   (TapeAction obj            ) => SetNoChannel(obj);
        public static TapeAction WithAnyChannel  (TapeAction obj            ) => SetNoChannel(obj);
        public static TapeAction WithEveryChannel(TapeAction obj            ) => SetNoChannel(obj);
        public static TapeAction WithCenter      (TapeAction obj            ) => SetCenter      (obj);
        public static TapeAction WithLeft        (TapeAction obj            ) => SetLeft        (obj);
        public static TapeAction WithRight       (TapeAction obj            ) => SetRight       (obj);
        public static TapeAction WithChannel     (TapeAction obj, int? value) => SetChannel     (obj, value);
        public static TapeAction AsNoChannel     (TapeAction obj            ) => SetNoChannel(obj);
        public static TapeAction AsAnyChannel    (TapeAction obj            ) => SetNoChannel(obj);
        public static TapeAction AsEveryChannel  (TapeAction obj            ) => SetNoChannel(obj);
        public static TapeAction AsCenter        (TapeAction obj            ) => SetCenter      (obj);
        public static TapeAction AsLeft          (TapeAction obj            ) => SetLeft        (obj);
        public static TapeAction AsRight         (TapeAction obj            ) => SetRight       (obj);
        public static TapeAction AsChannel       (TapeAction obj, int? value) => SetChannel     (obj, value);
        public static TapeAction SetAnyChannel   (TapeAction obj            ) => SetNoChannel(obj);
        public static TapeAction SetEveryChannel (TapeAction obj            ) => SetNoChannel(obj);
        public static TapeAction SetNoChannel    (TapeAction obj            ) => SetChannel     (obj, EmptyChannel ).Stereo();
        public static TapeAction SetCenter       (TapeAction obj            ) => SetChannel     (obj, CenterChannel).Mono  ();
        public static TapeAction SetLeft         (TapeAction obj            ) => SetChannel     (obj, LeftChannel  ).Stereo();
        public static TapeAction SetRight        (TapeAction obj            ) => SetChannel     (obj, RightChannel ).Stereo();
        public static TapeAction SetChannel      (TapeAction obj, int? value)
        {
            if (obj == null) throw new NullException(() => obj);
            obj.Tape.Config.Channel = value;
            return obj;
        }
        
        // Buff-Bound

        // Delegated to AudioFileOutput to ensure the same handling.
        
        /// <inheritdoc cref="docs._channeltoaudiofileoutput" />
        public static bool IsNoChannel   (Buff obj) => IsNoChannel   (obj?.UnderlyingAudioFileOutput);
        /// <inheritdoc cref="docs._channeltoaudiofileoutput" />
        public static bool IsAnyChannel  (Buff obj) => IsAnyChannel  (obj?.UnderlyingAudioFileOutput);
        /// <inheritdoc cref="docs._channeltoaudiofileoutput" />
        public static bool IsEveryChannel(Buff obj) => IsEveryChannel(obj?.UnderlyingAudioFileOutput);
        /// <inheritdoc cref="docs._channeltoaudiofileoutput" />
        public static bool IsCenter      (Buff obj) => IsCenter      (obj?.UnderlyingAudioFileOutput);
        /// <inheritdoc cref="docs._channeltoaudiofileoutput" />
        public static bool IsLeft        (Buff obj) => IsLeft        (obj?.UnderlyingAudioFileOutput);
        /// <inheritdoc cref="docs._channeltoaudiofileoutput" />
        public static bool IsRight       (Buff obj) => IsRight       (obj?.UnderlyingAudioFileOutput);
        /// <inheritdoc cref="docs._channeltoaudiofileoutput" />
        public static int? Channel       (Buff obj) => Channel       (obj?.UnderlyingAudioFileOutput);
        /// <inheritdoc cref="docs._channeltoaudiofileoutput" />
        public static int? GetChannel    (Buff obj) => GetChannel    (obj?.UnderlyingAudioFileOutput);

        /// <inheritdoc cref="docs._channeltoaudiofileoutput" />
        public static Buff AnyChannel      (Buff obj,             IContext context) { NoChannel       (obj?.UnderlyingAudioFileOutput,        context); return obj; }
        /// <inheritdoc cref="docs._channeltoaudiofileoutput" />
        public static Buff EveryChannel    (Buff obj,             IContext context) { NoChannel       (obj?.UnderlyingAudioFileOutput,        context); return obj; }
        /// <inheritdoc cref="docs._channeltoaudiofileoutput" />
        public static Buff NoChannel       (Buff obj,             IContext context) { NoChannel       (obj?.UnderlyingAudioFileOutput,        context); return obj; }
        /// <inheritdoc cref="docs._channeltoaudiofileoutput" />
        public static Buff Center          (Buff obj,             IContext context) { Center          (obj?.UnderlyingAudioFileOutput,        context); return obj; }
        /// <inheritdoc cref="docs._channeltoaudiofileoutput" />
        public static Buff Left            (Buff obj,             IContext context) { Left            (obj?.UnderlyingAudioFileOutput,        context); return obj; }
        /// <inheritdoc cref="docs._channeltoaudiofileoutput" />
        public static Buff Right           (Buff obj,             IContext context) { Right           (obj?.UnderlyingAudioFileOutput,        context); return obj; }
        /// <inheritdoc cref="docs._channeltoaudiofileoutput" />
        public static Buff Channel         (Buff obj, int? value, IContext context) { Channel         (obj?.UnderlyingAudioFileOutput, value, context); return obj; }
        /// <inheritdoc cref="docs._channeltoaudiofileoutput" />
        public static Buff WithNoChannel   (Buff obj,             IContext context) { WithNoChannel   (obj?.UnderlyingAudioFileOutput,        context); return obj; }
        /// <inheritdoc cref="docs._channeltoaudiofileoutput" />
        public static Buff WithAnyChannel  (Buff obj,             IContext context) { WithAnyChannel  (obj?.UnderlyingAudioFileOutput,        context); return obj; }
        /// <inheritdoc cref="docs._channeltoaudiofileoutput" />
        public static Buff WithEveryChannel(Buff obj,             IContext context) { WithEveryChannel(obj?.UnderlyingAudioFileOutput,        context); return obj; }
        /// <inheritdoc cref="docs._channeltoaudiofileoutput" />
        public static Buff WithCenter      (Buff obj,             IContext context) { WithCenter      (obj?.UnderlyingAudioFileOutput,        context); return obj; }
        /// <inheritdoc cref="docs._channeltoaudiofileoutput" />
        public static Buff WithLeft        (Buff obj,             IContext context) { WithLeft        (obj?.UnderlyingAudioFileOutput,        context); return obj; }
        /// <inheritdoc cref="docs._channeltoaudiofileoutput" />
        public static Buff WithRight       (Buff obj,             IContext context) { WithRight       (obj?.UnderlyingAudioFileOutput,        context); return obj; }
        /// <inheritdoc cref="docs._channeltoaudiofileoutput" />
        public static Buff WithChannel     (Buff obj, int? value, IContext context) { WithChannel     (obj?.UnderlyingAudioFileOutput, value, context); return obj; }
        /// <inheritdoc cref="docs._channeltoaudiofileoutput" />
        public static Buff AsNoChannel     (Buff obj,             IContext context) { AsNoChannel     (obj?.UnderlyingAudioFileOutput,        context); return obj; }
        /// <inheritdoc cref="docs._channeltoaudiofileoutput" />
        public static Buff AsAnyChannel    (Buff obj,             IContext context) { AsAnyChannel    (obj?.UnderlyingAudioFileOutput,        context); return obj; }
        /// <inheritdoc cref="docs._channeltoaudiofileoutput" />
        public static Buff AsEveryChannel  (Buff obj,             IContext context) { AsEveryChannel  (obj?.UnderlyingAudioFileOutput,        context); return obj; }
        /// <inheritdoc cref="docs._channeltoaudiofileoutput" />
        public static Buff AsCenter        (Buff obj,             IContext context) { AsCenter        (obj?.UnderlyingAudioFileOutput,        context); return obj; }
        /// <inheritdoc cref="docs._channeltoaudiofileoutput" />
        public static Buff AsLeft          (Buff obj,             IContext context) { AsLeft          (obj?.UnderlyingAudioFileOutput,        context); return obj; }
        /// <inheritdoc cref="docs._channeltoaudiofileoutput" />
        public static Buff AsRight         (Buff obj,             IContext context) { AsRight         (obj?.UnderlyingAudioFileOutput,        context); return obj; }
        /// <inheritdoc cref="docs._channeltoaudiofileoutput" />
        public static Buff AsChannel       (Buff obj, int? value, IContext context) { AsChannel       (obj?.UnderlyingAudioFileOutput, value, context); return obj; }
        /// <inheritdoc cref="docs._channeltoaudiofileoutput" />
        public static Buff SetAnyChannel   (Buff obj,             IContext context) { SetAnyChannel   (obj?.UnderlyingAudioFileOutput,        context); return obj; }
        /// <inheritdoc cref="docs._channeltoaudiofileoutput" />
        public static Buff SetEveryChannel (Buff obj,             IContext context) { SetEveryChannel (obj?.UnderlyingAudioFileOutput,        context); return obj; }
        /// <inheritdoc cref="docs._channeltoaudiofileoutput" />
        public static Buff SetNoChannel    (Buff obj,             IContext context) { SetNoChannel (obj?.UnderlyingAudioFileOutput,        context); return obj; }
        /// <inheritdoc cref="docs._channeltoaudiofileoutput" />
        public static Buff SetCenter       (Buff obj,             IContext context) { SetCenter       (obj?.UnderlyingAudioFileOutput,        context); return obj; }
        /// <inheritdoc cref="docs._channeltoaudiofileoutput" />
        public static Buff SetLeft         (Buff obj,             IContext context) { SetLeft         (obj?.UnderlyingAudioFileOutput,        context); return obj; }
        /// <inheritdoc cref="docs._channeltoaudiofileoutput" />
        public static Buff SetRight        (Buff obj,             IContext context) { SetRight        (obj?.UnderlyingAudioFileOutput,        context); return obj; }
        /// <inheritdoc cref="docs._channeltoaudiofileoutput" />
        public static Buff SetChannel      (Buff obj, int? value, IContext context) { SetChannel      (obj?.UnderlyingAudioFileOutput, value, context); return obj; }
        
        /// <inheritdoc cref="docs._channeltoaudiofileoutput" />
        public static bool IsNoChannel   (AudioFileOutput obj) => ConfigNightmares.IsNoChannel(obj);
        /// <inheritdoc cref="docs._channeltoaudiofileoutput" />
        public static bool IsAnyChannel  (AudioFileOutput obj) => ConfigNightmares.IsNoChannel(obj);
        /// <inheritdoc cref="docs._channeltoaudiofileoutput" />
        public static bool IsEveryChannel(AudioFileOutput obj) => ConfigNightmares.IsNoChannel(obj);
        /// <inheritdoc cref="docs._channeltoaudiofileoutput" />
        public static bool IsCenter      (AudioFileOutput obj) => ConfigNightmares.IsCenter(obj);
        /// <inheritdoc cref="docs._channeltoaudiofileoutput" />
        public static bool IsLeft        (AudioFileOutput obj) => ConfigNightmares.IsLeft(obj);
        /// <inheritdoc cref="docs._channeltoaudiofileoutput" />
        public static bool IsRight       (AudioFileOutput obj) => ConfigNightmares.IsRight(obj);
        /// <inheritdoc cref="docs._channeltoaudiofileoutput" />
        public static int? Channel       (AudioFileOutput obj) => ConfigNightmares.GetChannel(obj);
        /// <inheritdoc cref="docs._channeltoaudiofileoutput" />
        public static int? GetChannel    (AudioFileOutput obj) => ConfigNightmares.GetChannel(obj);
        
        /// <inheritdoc cref="docs._channeltoaudiofileoutput" />
        public static AudioFileOutput AnyChannel      (AudioFileOutput obj,             IContext context) => SetNoChannel   (obj,        context);
        /// <inheritdoc cref="docs._channeltoaudiofileoutput" />
        public static AudioFileOutput EveryChannel    (AudioFileOutput obj,             IContext context) => SetNoChannel   (obj,        context);
        /// <inheritdoc cref="docs._channeltoaudiofileoutput" />
        public static AudioFileOutput NoChannel       (AudioFileOutput obj,             IContext context) => SetNoChannel   (obj,        context);
        /// <inheritdoc cref="docs._channeltoaudiofileoutput" />
        public static AudioFileOutput Center          (AudioFileOutput obj,             IContext context) => SetCenter      (obj,        context);
        /// <inheritdoc cref="docs._channeltoaudiofileoutput" />
        public static AudioFileOutput Left            (AudioFileOutput obj,             IContext context) => SetLeft        (obj,        context);
        /// <inheritdoc cref="docs._channeltoaudiofileoutput" />
        public static AudioFileOutput Right           (AudioFileOutput obj,             IContext context) => SetRight       (obj,        context);
        /// <inheritdoc cref="docs._channeltoaudiofileoutput" />
        public static AudioFileOutput Channel         (AudioFileOutput obj, int? value, IContext context) => SetChannel     (obj, value, context);
        /// <inheritdoc cref="docs._channeltoaudiofileoutput" />
        public static AudioFileOutput WithNoChannel   (AudioFileOutput obj,             IContext context) => SetNoChannel   (obj,        context);
        /// <inheritdoc cref="docs._channeltoaudiofileoutput" />
        public static AudioFileOutput WithAnyChannel  (AudioFileOutput obj,             IContext context) => SetNoChannel   (obj,        context);
        /// <inheritdoc cref="docs._channeltoaudiofileoutput" />
        public static AudioFileOutput WithEveryChannel(AudioFileOutput obj,             IContext context) => SetNoChannel   (obj,        context);
        /// <inheritdoc cref="docs._channeltoaudiofileoutput" />
        public static AudioFileOutput WithCenter      (AudioFileOutput obj,             IContext context) => SetCenter      (obj,        context);
        /// <inheritdoc cref="docs._channeltoaudiofileoutput" />
        public static AudioFileOutput WithLeft        (AudioFileOutput obj,             IContext context) => SetLeft        (obj,        context);
        /// <inheritdoc cref="docs._channeltoaudiofileoutput" />
        public static AudioFileOutput WithRight       (AudioFileOutput obj,             IContext context) => SetRight       (obj,        context);
        /// <inheritdoc cref="docs._channeltoaudiofileoutput" />
        public static AudioFileOutput WithChannel     (AudioFileOutput obj, int? value, IContext context) => SetChannel     (obj, value, context);
        /// <inheritdoc cref="docs._channeltoaudiofileoutput" />
        public static AudioFileOutput AsNoChannel     (AudioFileOutput obj,             IContext context) => SetNoChannel   (obj,        context);
        /// <inheritdoc cref="docs._channeltoaudiofileoutput" />
        public static AudioFileOutput AsAnyChannel    (AudioFileOutput obj,             IContext context) => SetNoChannel   (obj,        context);
        /// <inheritdoc cref="docs._channeltoaudiofileoutput" />
        public static AudioFileOutput AsEveryChannel  (AudioFileOutput obj,             IContext context) => SetNoChannel   (obj,        context);
        /// <inheritdoc cref="docs._channeltoaudiofileoutput" />
        public static AudioFileOutput AsCenter        (AudioFileOutput obj,             IContext context) => SetCenter      (obj,        context);
        /// <inheritdoc cref="docs._channeltoaudiofileoutput" />
        public static AudioFileOutput AsLeft          (AudioFileOutput obj,             IContext context) => SetLeft        (obj,        context);
        /// <inheritdoc cref="docs._channeltoaudiofileoutput" />
        public static AudioFileOutput AsRight         (AudioFileOutput obj,             IContext context) => SetRight       (obj,        context);
        /// <inheritdoc cref="docs._channeltoaudiofileoutput" />
        public static AudioFileOutput AsChannel       (AudioFileOutput obj, int? value, IContext context) => SetChannel     (obj, value, context);
        /// <inheritdoc cref="docs._channeltoaudiofileoutput" />
        public static AudioFileOutput SetAnyChannel   (AudioFileOutput obj,             IContext context) => SetNoChannel   (obj,        context);
        /// <inheritdoc cref="docs._channeltoaudiofileoutput" />
        public static AudioFileOutput SetEveryChannel (AudioFileOutput obj,             IContext context) => SetNoChannel   (obj,        context);
        /// <inheritdoc cref="docs._channeltoaudiofileoutput" />
        public static AudioFileOutput SetNoChannel    (AudioFileOutput obj,             IContext context) => ConfigNightmares.SetNoChannel(obj, context);
        /// <inheritdoc cref="docs._channeltoaudiofileoutput" />
        public static AudioFileOutput SetCenter       (AudioFileOutput obj,             IContext context) => ConfigNightmares.SetCenter(obj, context);
        /// <inheritdoc cref="docs._channeltoaudiofileoutput" />
        public static AudioFileOutput SetLeft         (AudioFileOutput obj,             IContext context) => ConfigNightmares.SetLeft(obj, context);
        /// <inheritdoc cref="docs._channeltoaudiofileoutput" />
        public static AudioFileOutput SetRight        (AudioFileOutput obj,             IContext context) => ConfigNightmares.SetRight(obj, context);
        /// <inheritdoc cref="docs._channeltoaudiofileoutput" />
        public static AudioFileOutput SetChannel      (AudioFileOutput obj, int? value, IContext context) => ConfigNightmares.SetChannel(obj, value, context);
        
        public static int Channel   (AudioFileOutputChannel obj) => GetChannel(obj);
        public static int GetChannel(AudioFileOutputChannel obj)
        {
            if (obj == null) throw new NullException(() => obj);
            return obj.Index;
        }
        
        public static AudioFileOutputChannel Channel    (AudioFileOutputChannel obj, int value) => SetChannel(obj, value);
        public static AudioFileOutputChannel WithChannel(AudioFileOutputChannel obj, int value) => SetChannel(obj, value);
        public static AudioFileOutputChannel AsChannel  (AudioFileOutputChannel obj, int value) => SetChannel(obj, value);
        public static AudioFileOutputChannel SetChannel (AudioFileOutputChannel obj, int value)
        {
            if (obj == null) throw new NullException(() => obj);
            obj.Index = value;
            return obj;
        }

        // Immutable

        [Obsolete(ObsoleteMessage)] public static bool IsAnyChannel  (ChannelEnum enumValue) => IsNoChannel   (enumValue);
        [Obsolete(ObsoleteMessage)] public static bool IsEveryChannel(ChannelEnum enumValue) => IsNoChannel   (enumValue);
        [Obsolete(ObsoleteMessage)] public static bool IsNoChannel   (ChannelEnum enumValue) => EnumToChannel(enumValue) == EmptyChannel  && IsStereo(enumValue);
        [Obsolete(ObsoleteMessage)] public static bool IsCenter      (ChannelEnum enumValue) => EnumToChannel(enumValue) == CenterChannel && IsMono  (enumValue);
        [Obsolete(ObsoleteMessage)] public static bool IsLeft        (ChannelEnum enumValue) => EnumToChannel(enumValue) == LeftChannel   && IsStereo(enumValue);
        [Obsolete(ObsoleteMessage)] public static bool IsRight       (ChannelEnum enumValue) => EnumToChannel(enumValue) == RightChannel  && IsStereo(enumValue);
        [Obsolete(ObsoleteMessage)] public static int? Channel       (ChannelEnum enumValue) => EnumToChannel(enumValue);
        [Obsolete(ObsoleteMessage)] public static int? GetChannel    (ChannelEnum enumValue) => EnumToChannel(enumValue);
        [Obsolete(ObsoleteMessage)] public static int? EnumToChannel (ChannelEnum enumValue)
        {
            switch (enumValue)
            {
                case ChannelEnum.Single: return CenterChannel;
                case ChannelEnum.Left: return LeftChannel;
                case ChannelEnum.Right: return RightChannel;
                case ChannelEnum.Undefined: return EmptyChannel;
                default: throw new ValueNotSupportedException(enumValue);
            }
        }

        /// <inheritdoc cref="docs._quasisetter" />
        [Obsolete(ObsoleteMessage)] public static ChannelEnum AnyChannel(ChannelEnum oldChannelEnum) => SetNoChannel(oldChannelEnum);
        /// <inheritdoc cref="docs._quasisetter" />
        [Obsolete(ObsoleteMessage)] public static ChannelEnum EveryChannel(ChannelEnum oldChannelEnum) => SetNoChannel(oldChannelEnum);
        /// <inheritdoc cref="docs._quasisetter" />
        [Obsolete(ObsoleteMessage)] public static ChannelEnum NoChannel(ChannelEnum oldChannelEnum) => SetNoChannel(oldChannelEnum);
        /// <inheritdoc cref="docs._quasisetter" />
        [Obsolete(ObsoleteMessage)] public static ChannelEnum Center(ChannelEnum oldChannelEnum) => SetCenter(oldChannelEnum);
        /// <inheritdoc cref="docs._quasisetter" />
        [Obsolete(ObsoleteMessage)] public static ChannelEnum Left(ChannelEnum oldChannelEnum) => SetLeft(oldChannelEnum);
        /// <inheritdoc cref="docs._quasisetter" />
        [Obsolete(ObsoleteMessage)] public static ChannelEnum Right(ChannelEnum oldChannelEnum) => SetRight(oldChannelEnum);
        /// <inheritdoc cref="docs._quasisetter" />
        [Obsolete(ObsoleteMessage)] public static ChannelEnum Channel(ChannelEnum oldChannelEnum, int? newChannelValue) => ConfigNightmares.SetChannel(oldChannelEnum, newChannelValue);
        /// <inheritdoc cref="docs._quasisetter" />
        [Obsolete(ObsoleteMessage)] public static ChannelEnum WithNoChannel(ChannelEnum oldChannelEnum) => SetNoChannel(oldChannelEnum);
        /// <inheritdoc cref="docs._quasisetter" />
        [Obsolete(ObsoleteMessage)] public static ChannelEnum WithAnyChannel(ChannelEnum oldChannelEnum) => SetNoChannel(oldChannelEnum);
        /// <inheritdoc cref="docs._quasisetter" />
        [Obsolete(ObsoleteMessage)] public static ChannelEnum WithEveryChannel(ChannelEnum oldChannelEnum) => SetNoChannel(oldChannelEnum);
        /// <inheritdoc cref="docs._quasisetter" />
        [Obsolete(ObsoleteMessage)] public static ChannelEnum WithCenter(ChannelEnum oldChannelEnum) => SetCenter(oldChannelEnum);
        /// <inheritdoc cref="docs._quasisetter" />
        [Obsolete(ObsoleteMessage)] public static ChannelEnum WithLeft(ChannelEnum oldChannelEnum) => SetLeft(oldChannelEnum);
        /// <inheritdoc cref="docs._quasisetter" />
        [Obsolete(ObsoleteMessage)] public static ChannelEnum WithRight(ChannelEnum oldChannelEnum) => SetRight(oldChannelEnum);
        /// <inheritdoc cref="docs._quasisetter" />
        [Obsolete(ObsoleteMessage)] public static ChannelEnum WithChannel(ChannelEnum oldChannelEnum, int? newChannelValue) => ConfigNightmares.SetChannel(oldChannelEnum, newChannelValue);
        /// <inheritdoc cref="docs._quasisetter" />
        [Obsolete(ObsoleteMessage)] public static ChannelEnum AsNoChannel(ChannelEnum oldChannelEnum) => SetNoChannel(oldChannelEnum);
        /// <inheritdoc cref="docs._quasisetter" />
        [Obsolete(ObsoleteMessage)] public static ChannelEnum AsAnyChannel(ChannelEnum oldChannelEnum) => SetNoChannel(oldChannelEnum);
        /// <inheritdoc cref="docs._quasisetter" />
        [Obsolete(ObsoleteMessage)] public static ChannelEnum AsEveryChannel(ChannelEnum oldChannelEnum) => SetNoChannel(oldChannelEnum);
        /// <inheritdoc cref="docs._quasisetter" />
        [Obsolete(ObsoleteMessage)] public static ChannelEnum AsCenter(ChannelEnum oldChannelEnum) => SetCenter(oldChannelEnum);
        /// <inheritdoc cref="docs._quasisetter" />
        [Obsolete(ObsoleteMessage)] public static ChannelEnum AsLeft(ChannelEnum oldChannelEnum) => SetLeft(oldChannelEnum);
        /// <inheritdoc cref="docs._quasisetter" />
        [Obsolete(ObsoleteMessage)] public static ChannelEnum AsRight(ChannelEnum oldChannelEnum) => SetRight(oldChannelEnum);
        /// <inheritdoc cref="docs._quasisetter" />
        [Obsolete(ObsoleteMessage)] public static ChannelEnum AsChannel(ChannelEnum oldChannelEnum, int? newChannelValue) => ConfigNightmares.SetChannel(oldChannelEnum, newChannelValue);
        /// <inheritdoc cref="docs._quasisetter" />
        [Obsolete(ObsoleteMessage)] public static ChannelEnum ToNoChannel(ChannelEnum oldChannelEnum) => SetNoChannel(oldChannelEnum);
        /// <inheritdoc cref="docs._quasisetter" />
        [Obsolete(ObsoleteMessage)] public static ChannelEnum ToAnyChannel(ChannelEnum oldChannelEnum) => SetNoChannel(oldChannelEnum);
        /// <inheritdoc cref="docs._quasisetter" />
        [Obsolete(ObsoleteMessage)] public static ChannelEnum ToEveryChannel(ChannelEnum oldChannelEnum) => SetNoChannel(oldChannelEnum);
        /// <inheritdoc cref="docs._quasisetter" />
        [Obsolete(ObsoleteMessage)] public static ChannelEnum ToChannelsEmpty(ChannelEnum oldChannelEnum) => SetNoChannel(oldChannelEnum);
        /// <inheritdoc cref="docs._quasisetter" />
        [Obsolete(ObsoleteMessage)] public static ChannelEnum ToCenter(ChannelEnum oldChannelEnum) => SetCenter(oldChannelEnum);
        /// <inheritdoc cref="docs._quasisetter" />
        [Obsolete(ObsoleteMessage)] public static ChannelEnum ToLeft(ChannelEnum oldChannelEnum) => SetLeft(oldChannelEnum);
        /// <inheritdoc cref="docs._quasisetter" />
        [Obsolete(ObsoleteMessage)] public static ChannelEnum ToRight(ChannelEnum oldChannelEnum) => SetRight(oldChannelEnum);
        /// <inheritdoc cref="docs._quasisetter" />
        [Obsolete(ObsoleteMessage)] public static ChannelEnum ToChannel(ChannelEnum oldChannelEnum, int? newChannelValue) => ConfigNightmares.SetChannel(oldChannelEnum, newChannelValue);
        /// <inheritdoc cref="docs._quasisetter" />
        [Obsolete(ObsoleteMessage)] public static ChannelEnum SetAnyChannel(ChannelEnum oldChannelEnum) => SetNoChannel(oldChannelEnum);
        /// <inheritdoc cref="docs._quasisetter" />
        [Obsolete(ObsoleteMessage)] public static ChannelEnum SetEveryChannel(ChannelEnum oldChannelEnum) => SetNoChannel(oldChannelEnum);
        /// <inheritdoc cref="docs._quasisetter" />
        [Obsolete(ObsoleteMessage)] public static ChannelEnum SetNoChannel(ChannelEnum oldChannelEnum) => ChannelEnum.Undefined;
        /// <inheritdoc cref="docs._quasisetter" />
        [Obsolete(ObsoleteMessage)] public static ChannelEnum SetCenter(ChannelEnum oldChannelEnum) => ChannelEnum.Single;
        /// <inheritdoc cref="docs._quasisetter" />
        [Obsolete(ObsoleteMessage)] public static ChannelEnum SetLeft(ChannelEnum oldChannelEnum) => ChannelEnum.Left;
        /// <inheritdoc cref="docs._quasisetter" />
        [Obsolete(ObsoleteMessage)] public static ChannelEnum SetRight(ChannelEnum oldChannelEnum) => ChannelEnum.Right;
        /// <inheritdoc cref="docs._quasisetter" />
        [Obsolete(ObsoleteMessage)] public static ChannelEnum SetChannel(ChannelEnum oldChannelEnum, int? newChannelValue) => ConfigNightmares.SetChannel(oldChannelEnum, newChannelValue);
        /// <inheritdoc cref="docs._quasisetter" />
        [Obsolete(ObsoleteMessage)] public static ChannelEnum ChannelToEnum(int? thisChannel, int channelsForContext) => ConfigNightmares.ChannelToEnum(thisChannel, channelsForContext);


        [Obsolete(ObsoleteMessage)] public static bool IsAnyChannel   (Channel entity) => entity == null;
        [Obsolete(ObsoleteMessage)] public static bool IsEveryChannel (Channel entity) => entity == null;
        [Obsolete(ObsoleteMessage)] public static bool IsNoChannel    (Channel entity) => entity == null;
        [Obsolete(ObsoleteMessage)] public static bool IsCenter(       Channel entity) => entity.ToEnum() == ChannelEnum.Single;
        [Obsolete(ObsoleteMessage)] public static bool IsLeft(         Channel entity) => entity.ToEnum() == ChannelEnum.Left;
        [Obsolete(ObsoleteMessage)] public static bool IsRight(        Channel entity) => entity.ToEnum() == ChannelEnum.Right; 
        [Obsolete(ObsoleteMessage)] public static int? Channel(        Channel entity) => EntityToChannel(entity);
        [Obsolete(ObsoleteMessage)] public static int? GetChannel(     Channel entity) => EntityToChannel(entity);
        [Obsolete(ObsoleteMessage)] public static int? EntityToChannel(Channel entity) => entity.ToEnum().EnumToChannel();

        /// <inheritdoc cref="docs._quasisetter" />
        [Obsolete(ObsoleteMessage)] public static Channel AnyChannel(Channel oldChannelEntity) => SetNoChannel(oldChannelEntity);
        /// <inheritdoc cref="docs._quasisetter" />
        [Obsolete(ObsoleteMessage)] public static Channel EveryChannel(Channel oldChannelEntity) => SetNoChannel(oldChannelEntity);
        /// <inheritdoc cref="docs._quasisetter" />
        [Obsolete(ObsoleteMessage)] public static Channel NoChannel(Channel oldChannelEntity) => SetNoChannel(oldChannelEntity);
        /// <inheritdoc cref="docs._quasisetter" />
        [Obsolete(ObsoleteMessage)] public static Channel Center(Channel oldChannelEntity, IContext context) => SetCenter(oldChannelEntity, context);
        /// <inheritdoc cref="docs._quasisetter" />
        [Obsolete(ObsoleteMessage)] public static Channel Left(Channel oldChannelEntity, IContext context) => SetLeft(oldChannelEntity, context);
        /// <inheritdoc cref="docs._quasisetter" />
        [Obsolete(ObsoleteMessage)] public static Channel Right(Channel oldChannelEntity, IContext context) => SetRight(oldChannelEntity, context);
        /// <inheritdoc cref="docs._quasisetter" />
        [Obsolete(ObsoleteMessage)] public static Channel Channel(Channel oldChannelEntity, int? newChannelValue, IContext context) => ConfigNightmares.SetChannel(oldChannelEntity, newChannelValue, context);
        /// <inheritdoc cref="docs._quasisetter" />
        [Obsolete(ObsoleteMessage)] public static Channel WithNoChannel(Channel oldChannelEntity) => SetNoChannel(oldChannelEntity);
        /// <inheritdoc cref="docs._quasisetter" />
        [Obsolete(ObsoleteMessage)] public static Channel WithAnyChannel(Channel oldChannelEntity) => SetNoChannel(oldChannelEntity);
        /// <inheritdoc cref="docs._quasisetter" />
        [Obsolete(ObsoleteMessage)] public static Channel WithEveryChannel(Channel oldChannelEntity) => SetNoChannel(oldChannelEntity);
        /// <inheritdoc cref="docs._quasisetter" />
        [Obsolete(ObsoleteMessage)] public static Channel WithCenter(Channel oldChannelEntity, IContext context) => SetCenter(oldChannelEntity, context);
        /// <inheritdoc cref="docs._quasisetter" />
        [Obsolete(ObsoleteMessage)] public static Channel WithLeft(Channel oldChannelEntity, IContext context) => SetLeft(oldChannelEntity, context);
        /// <inheritdoc cref="docs._quasisetter" />
        [Obsolete(ObsoleteMessage)] public static Channel WithRight(Channel oldChannelEntity, IContext context) => SetRight(oldChannelEntity, context);
        /// <inheritdoc cref="docs._quasisetter" />
        [Obsolete(ObsoleteMessage)] public static Channel WithChannel(Channel oldChannelEntity, int? newChannelValue, IContext context) => ConfigNightmares.SetChannel(oldChannelEntity, newChannelValue, context);
        /// <inheritdoc cref="docs._quasisetter" />
        [Obsolete(ObsoleteMessage)] public static Channel AsNoChannel(Channel oldChannelEntity) => SetNoChannel(oldChannelEntity);
        /// <inheritdoc cref="docs._quasisetter" />
        [Obsolete(ObsoleteMessage)] public static Channel AsAnyChannel(Channel oldChannelEntity) => SetNoChannel(oldChannelEntity);
        /// <inheritdoc cref="docs._quasisetter" />
        [Obsolete(ObsoleteMessage)] public static Channel AsEveryChannel(Channel oldChannelEntity) => SetNoChannel(oldChannelEntity);
        /// <inheritdoc cref="docs._quasisetter" />
        [Obsolete(ObsoleteMessage)] public static Channel AsCenter(Channel oldChannelEntity, IContext context) => SetCenter(oldChannelEntity, context);
        /// <inheritdoc cref="docs._quasisetter" />
        [Obsolete(ObsoleteMessage)] public static Channel AsLeft(Channel oldChannelEntity, IContext context) => SetLeft(oldChannelEntity, context);
        /// <inheritdoc cref="docs._quasisetter" />
        [Obsolete(ObsoleteMessage)] public static Channel AsRight(Channel oldChannelEntity, IContext context) => SetRight(oldChannelEntity, context);
        /// <inheritdoc cref="docs._quasisetter" />
        [Obsolete(ObsoleteMessage)] public static Channel AsChannel(Channel oldChannelEntity, int? newChannelValue, IContext context) => ConfigNightmares.SetChannel(oldChannelEntity, newChannelValue, context);
        /// <inheritdoc cref="docs._quasisetter" />
        [Obsolete(ObsoleteMessage)] public static Channel ToNoChannel(Channel oldChannelEntity) => SetNoChannel(oldChannelEntity);
        /// <inheritdoc cref="docs._quasisetter" />
        [Obsolete(ObsoleteMessage)] public static Channel ToAnyChannel(Channel oldChannelEntity) => SetNoChannel(oldChannelEntity);
        /// <inheritdoc cref="docs._quasisetter" />
        [Obsolete(ObsoleteMessage)] public static Channel ToEveryChannel(Channel oldChannelEntity) => SetNoChannel(oldChannelEntity);
        /// <inheritdoc cref="docs._quasisetter" />
        [Obsolete(ObsoleteMessage)] public static Channel ToCenter(Channel oldChannelEntity, IContext context) => SetCenter(oldChannelEntity, context);
        /// <inheritdoc cref="docs._quasisetter" />
        [Obsolete(ObsoleteMessage)] public static Channel ToLeft(Channel oldChannelEntity, IContext context) => SetLeft(oldChannelEntity, context);
        /// <inheritdoc cref="docs._quasisetter" />
        [Obsolete(ObsoleteMessage)] public static Channel ToRight(Channel oldChannelEntity, IContext context) => SetRight(oldChannelEntity, context);
        /// <inheritdoc cref="docs._quasisetter" />
        [Obsolete(ObsoleteMessage)] public static Channel ToChannel(Channel oldChannelEntity, int? newChannelValue, IContext context) => ConfigNightmares.SetChannel(oldChannelEntity, newChannelValue, context);
        /// <inheritdoc cref="docs._quasisetter" />
        [Obsolete(ObsoleteMessage)] public static Channel SetAnyChannel(Channel oldChannelEntity) => SetNoChannel(oldChannelEntity);
        /// <inheritdoc cref="docs._quasisetter" />
        [Obsolete(ObsoleteMessage)] public static Channel SetEveryChannel(Channel oldChannelEntity) => SetNoChannel(oldChannelEntity);
        /// <inheritdoc cref="docs._quasisetter" />
        [Obsolete(ObsoleteMessage)] public static Channel SetNoChannel(Channel oldChannelEntity) => null;
        /// <inheritdoc cref="docs._quasisetter" />
        [Obsolete(ObsoleteMessage)] public static Channel SetCenter(Channel oldChannelEntity, IContext context) => ChannelEnum.Single.ToEntity(context);
        /// <inheritdoc cref="docs._quasisetter" />
        [Obsolete(ObsoleteMessage)] public static Channel SetLeft(Channel oldChannelEntity, IContext context) => ChannelEnum.Left.ToEntity(context);
        /// <inheritdoc cref="docs._quasisetter" />
        [Obsolete(ObsoleteMessage)] public static Channel SetRight(Channel oldChannelEntity, IContext context) => ChannelEnum.Right.ToEntity(context);
        /// <inheritdoc cref="docs._quasisetter" />
        [Obsolete(ObsoleteMessage)] public static Channel SetChannel(Channel oldChannelEntity, int? newChannelValue, IContext context) => ConfigNightmares.SetChannel(oldChannelEntity, newChannelValue, context);

        [Obsolete(ObsoleteMessage)] public static Channel ChannelToEntity(int? thisChannel, int channelsForContext, IContext context) => thisChannel.ChannelToEnum(channelsForContext).ToEntity(context);
    }
}