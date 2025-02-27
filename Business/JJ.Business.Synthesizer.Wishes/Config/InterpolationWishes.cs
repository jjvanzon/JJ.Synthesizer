using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using JetBrains.Annotations;
using JJ.Business.Synthesizer.Enums;
using JJ.Business.Synthesizer.Extensions;
using JJ.Business.Synthesizer.Wishes.TapeWishes;
using JJ.Framework.Persistence;
using JJ.Framework.Reflection;
using JJ.Persistence.Synthesizer;
using JJ.Persistence.Synthesizer.DefaultRepositories.Interfaces;
using JJ.Business.Synthesizer.Wishes.docs;
using static JJ.Business.Synthesizer.Enums.InterpolationTypeEnum;
using static JJ.Business.Synthesizer.Wishes.Helpers.ServiceFactory;
using static JJ.Business.Synthesizer.Wishes.Obsolete.ObsoleteEnumWishesMessages;

// ReSharper disable UnusedParameter.Global

namespace JJ.Business.Synthesizer.Wishes.Config
{
    // Interpolation: A Primary Audio Attribute
        
    /// <inheritdoc cref="_configextensionwishes"/>
    public static class InterpolationExtensionWishes
    {
        // Synth-Bound

        public static bool IsLinear(this SynthWishes obj) => ConfigWishes.IsLinear(obj);
        public static bool IsBlocky(this SynthWishes obj) => ConfigWishes.IsBlocky(obj);
        public static InterpolationTypeEnum Interpolation(this SynthWishes obj) => ConfigWishes.Interpolation(obj);
        public static InterpolationTypeEnum GetInterpolation(this SynthWishes obj) => ConfigWishes.GetInterpolation(obj);

        public static SynthWishes Linear(this SynthWishes obj) => ConfigWishes.Linear(obj);
        public static SynthWishes Blocky(this SynthWishes obj) => ConfigWishes.Blocky(obj);
        public static SynthWishes WithLinear(this SynthWishes obj) => ConfigWishes.WithLinear(obj);
        public static SynthWishes WithBlocky(this SynthWishes obj) => ConfigWishes.WithBlocky(obj);
        public static SynthWishes AsLinear(this SynthWishes obj) => ConfigWishes.AsLinear(obj);
        public static SynthWishes AsBlocky(this SynthWishes obj) => ConfigWishes.AsBlocky(obj);
        public static SynthWishes SetLinear(this SynthWishes obj) => ConfigWishes.SetLinear(obj);
        public static SynthWishes SetBlocky(this SynthWishes obj) => ConfigWishes.SetBlocky(obj);
        public static SynthWishes Interpolation(this SynthWishes obj, InterpolationTypeEnum? value) => ConfigWishes.Interpolation(obj, value);
        public static SynthWishes WithInterpolation(this SynthWishes obj, InterpolationTypeEnum? value) => ConfigWishes.WithInterpolation(obj, value);
        public static SynthWishes AsInterpolation(this SynthWishes obj, InterpolationTypeEnum? value) => ConfigWishes.AsInterpolation(obj, value);
        public static SynthWishes SetInterpolation(this SynthWishes obj, InterpolationTypeEnum? value) => ConfigWishes.SetInterpolation(obj, value);

        public static bool IsLinear(this FlowNode obj) => ConfigWishes.IsLinear(obj);
        public static bool IsBlocky(this FlowNode obj) => ConfigWishes.IsBlocky(obj);
        public static InterpolationTypeEnum Interpolation(this FlowNode obj) => ConfigWishes.Interpolation(obj);
        public static InterpolationTypeEnum GetInterpolation(this FlowNode obj) => ConfigWishes.GetInterpolation(obj);

        public static FlowNode Linear(this FlowNode obj) => ConfigWishes.Linear(obj);
        public static FlowNode Blocky(this FlowNode obj) => ConfigWishes.Blocky(obj);
        public static FlowNode WithLinear(this FlowNode obj) => ConfigWishes.WithLinear(obj);
        public static FlowNode WithBlocky(this FlowNode obj) => ConfigWishes.WithBlocky(obj);
        public static FlowNode AsLinear(this FlowNode obj) => ConfigWishes.AsLinear(obj);
        public static FlowNode AsBlocky(this FlowNode obj) => ConfigWishes.AsBlocky(obj);
        public static FlowNode SetLinear(this FlowNode obj) => ConfigWishes.SetLinear(obj);
        public static FlowNode SetBlocky(this FlowNode obj) => ConfigWishes.SetBlocky(obj);
        public static FlowNode Interpolation(this FlowNode obj, InterpolationTypeEnum? value) => ConfigWishes.Interpolation(obj, value);
        public static FlowNode WithInterpolation(this FlowNode obj, InterpolationTypeEnum? value) => ConfigWishes.WithInterpolation(obj, value);
        public static FlowNode AsInterpolation(this FlowNode obj, InterpolationTypeEnum? value) => ConfigWishes.AsInterpolation(obj, value);
        public static FlowNode SetInterpolation(this FlowNode obj, InterpolationTypeEnum? value) => ConfigWishes.SetInterpolation(obj, value);

        [UsedImplicitly] internal static bool IsLinear(this ConfigResolver obj) => ConfigWishes.IsLinear(obj);
        [UsedImplicitly] internal static bool IsBlocky(this ConfigResolver obj) => ConfigWishes.IsBlocky(obj);
        [UsedImplicitly] internal static InterpolationTypeEnum Interpolation(this ConfigResolver obj) => ConfigWishes.Interpolation(obj);
        [UsedImplicitly] internal static InterpolationTypeEnum GetInterpolation(this ConfigResolver obj) => ConfigWishes.GetInterpolation(obj);

        [UsedImplicitly] internal static ConfigResolver Linear(this ConfigResolver obj) => ConfigWishes.Linear(obj);
        [UsedImplicitly] internal static ConfigResolver Blocky(this ConfigResolver obj) => ConfigWishes.Blocky(obj);
        [UsedImplicitly] internal static ConfigResolver WithLinear(this ConfigResolver obj) => ConfigWishes.WithLinear(obj);
        [UsedImplicitly] internal static ConfigResolver WithBlocky(this ConfigResolver obj) => ConfigWishes.WithBlocky(obj);
        [UsedImplicitly] internal static ConfigResolver AsLinear(this ConfigResolver obj) => ConfigWishes.AsLinear(obj);
        [UsedImplicitly] internal static ConfigResolver AsBlocky(this ConfigResolver obj) => ConfigWishes.AsBlocky(obj);
        [UsedImplicitly] internal static ConfigResolver SetLinear(this ConfigResolver obj) => ConfigWishes.SetLinear(obj);
        [UsedImplicitly] internal static ConfigResolver SetBlocky(this ConfigResolver obj) => ConfigWishes.SetBlocky(obj);
        [UsedImplicitly] internal static ConfigResolver Interpolation(this ConfigResolver obj, InterpolationTypeEnum? value) => ConfigWishes.Interpolation(obj, value);
        [UsedImplicitly] internal static ConfigResolver WithInterpolation(this ConfigResolver obj, InterpolationTypeEnum? value) => ConfigWishes.WithInterpolation(obj, value);
        [UsedImplicitly] internal static ConfigResolver AsInterpolation(this ConfigResolver obj, InterpolationTypeEnum? value) => ConfigWishes.AsInterpolation(obj, value);
        [UsedImplicitly] internal static ConfigResolver SetInterpolation(this ConfigResolver obj, InterpolationTypeEnum? value) => ConfigWishes.SetInterpolation(obj, value);

        // Global-Bound

        [UsedImplicitly] internal static bool IsLinear(this ConfigSection obj) => ConfigWishes.IsLinear(obj);
        [UsedImplicitly] internal static bool IsBlocky(this ConfigSection obj) => ConfigWishes.IsBlocky(obj);
        [UsedImplicitly] internal static InterpolationTypeEnum? Interpolation(this ConfigSection obj) => ConfigWishes.Interpolation(obj);
        [UsedImplicitly] internal static InterpolationTypeEnum? GetInterpolation(this ConfigSection obj) => ConfigWishes.GetInterpolation(obj);

        // Tape-Bound

        public static bool IsLinear(this Tape obj) => ConfigWishes.IsLinear(obj);
        public static bool IsBlocky(this Tape obj) => ConfigWishes.IsBlocky(obj);
        public static InterpolationTypeEnum Interpolation(this Tape obj) => ConfigWishes.Interpolation(obj);
        public static InterpolationTypeEnum GetInterpolation(this Tape obj) => ConfigWishes.GetInterpolation(obj);

        public static Tape Linear(this Tape obj) => ConfigWishes.Linear(obj);
        public static Tape Blocky(this Tape obj) => ConfigWishes.Blocky(obj);
        public static Tape WithLinear(this Tape obj) => ConfigWishes.WithLinear(obj);
        public static Tape WithBlocky(this Tape obj) => ConfigWishes.WithBlocky(obj);
        public static Tape AsLinear(this Tape obj) => ConfigWishes.AsLinear(obj);
        public static Tape AsBlocky(this Tape obj) => ConfigWishes.AsBlocky(obj);
        public static Tape SetLinear(this Tape obj) => ConfigWishes.SetLinear(obj);
        public static Tape SetBlocky(this Tape obj) => ConfigWishes.SetBlocky(obj);
        public static Tape Interpolation(this Tape obj, InterpolationTypeEnum value) => ConfigWishes.Interpolation(obj, value);
        public static Tape WithInterpolation(this Tape obj, InterpolationTypeEnum value) => ConfigWishes.WithInterpolation(obj, value);
        public static Tape AsInterpolation(this Tape obj, InterpolationTypeEnum value) => ConfigWishes.AsInterpolation(obj, value);
        public static Tape SetInterpolation(this Tape obj, InterpolationTypeEnum value) => ConfigWishes.SetInterpolation(obj, value);

        public static bool IsLinear(this TapeConfig obj) => ConfigWishes.IsLinear(obj);
        public static bool IsBlocky(this TapeConfig obj) => ConfigWishes.IsBlocky(obj);
        public static InterpolationTypeEnum Interpolation(this TapeConfig obj) => ConfigWishes.Interpolation(obj);
        public static InterpolationTypeEnum GetInterpolation(this TapeConfig obj) => ConfigWishes.GetInterpolation(obj);

        public static TapeConfig Linear(this TapeConfig obj) => ConfigWishes.Linear(obj);
        public static TapeConfig Blocky(this TapeConfig obj) => ConfigWishes.Blocky(obj);
        public static TapeConfig WithLinear(this TapeConfig obj) => ConfigWishes.WithLinear(obj);
        public static TapeConfig WithBlocky(this TapeConfig obj) => ConfigWishes.WithBlocky(obj);
        public static TapeConfig AsLinear(this TapeConfig obj) => ConfigWishes.AsLinear(obj);
        public static TapeConfig AsBlocky(this TapeConfig obj) => ConfigWishes.AsBlocky(obj);
        public static TapeConfig SetLinear(this TapeConfig obj) => ConfigWishes.SetLinear(obj);
        public static TapeConfig SetBlocky(this TapeConfig obj) => ConfigWishes.SetBlocky(obj);
        public static TapeConfig Interpolation(this TapeConfig obj, InterpolationTypeEnum value) => ConfigWishes.Interpolation(obj, value);
        public static TapeConfig WithInterpolation(this TapeConfig obj, InterpolationTypeEnum value) => ConfigWishes.WithInterpolation(obj, value);
        public static TapeConfig AsInterpolation(this TapeConfig obj, InterpolationTypeEnum value) => ConfigWishes.AsInterpolation(obj, value);
        public static TapeConfig SetInterpolation(this TapeConfig obj, InterpolationTypeEnum value) => ConfigWishes.SetInterpolation(obj, value);

        public static bool IsLinear(this TapeAction obj) => ConfigWishes.IsLinear(obj);
        public static bool IsBlocky(this TapeAction obj) => ConfigWishes.IsBlocky(obj);
        public static InterpolationTypeEnum Interpolation(this TapeAction obj) => ConfigWishes.Interpolation(obj);
        public static InterpolationTypeEnum GetInterpolation(this TapeAction obj) => ConfigWishes.GetInterpolation(obj);

        public static TapeAction Linear(this TapeAction obj) => ConfigWishes.Linear(obj);
        public static TapeAction Blocky(this TapeAction obj) => ConfigWishes.Blocky(obj);
        public static TapeAction WithLinear(this TapeAction obj) => ConfigWishes.WithLinear(obj);
        public static TapeAction WithBlocky(this TapeAction obj) => ConfigWishes.WithBlocky(obj);
        public static TapeAction AsLinear(this TapeAction obj) => ConfigWishes.AsLinear(obj);
        public static TapeAction AsBlocky(this TapeAction obj) => ConfigWishes.AsBlocky(obj);
        public static TapeAction SetLinear(this TapeAction obj) => ConfigWishes.SetLinear(obj);
        public static TapeAction SetBlocky(this TapeAction obj) => ConfigWishes.SetBlocky(obj);
        public static TapeAction Interpolation(this TapeAction obj, InterpolationTypeEnum value) => ConfigWishes.Interpolation(obj, value);
        public static TapeAction WithInterpolation(this TapeAction obj, InterpolationTypeEnum value) => ConfigWishes.WithInterpolation(obj, value);
        public static TapeAction AsInterpolation(this TapeAction obj, InterpolationTypeEnum value) => ConfigWishes.AsInterpolation(obj, value);
        public static TapeAction SetInterpolation(this TapeAction obj, InterpolationTypeEnum value) => ConfigWishes.SetInterpolation(obj, value);

        public static bool IsLinear(this TapeActions obj) => ConfigWishes.IsLinear(obj);
        public static bool IsBlocky(this TapeActions obj) => ConfigWishes.IsBlocky(obj);
        public static InterpolationTypeEnum Interpolation(this TapeActions obj) => ConfigWishes.Interpolation(obj);
        public static InterpolationTypeEnum GetInterpolation(this TapeActions obj) => ConfigWishes.GetInterpolation(obj);

        public static TapeActions Linear(this TapeActions obj) => ConfigWishes.Linear(obj);
        public static TapeActions Blocky(this TapeActions obj) => ConfigWishes.Blocky(obj);
        public static TapeActions WithLinear(this TapeActions obj) => ConfigWishes.WithLinear(obj);
        public static TapeActions WithBlocky(this TapeActions obj) => ConfigWishes.WithBlocky(obj);
        public static TapeActions AsLinear(this TapeActions obj) => ConfigWishes.AsLinear(obj);
        public static TapeActions AsBlocky(this TapeActions obj) => ConfigWishes.AsBlocky(obj);
        public static TapeActions SetLinear(this TapeActions obj) => ConfigWishes.SetLinear(obj);
        public static TapeActions SetBlocky(this TapeActions obj) => ConfigWishes.SetBlocky(obj);
        public static TapeActions Interpolation(this TapeActions obj, InterpolationTypeEnum value) => ConfigWishes.Interpolation(obj, value);
        public static TapeActions WithInterpolation(this TapeActions obj, InterpolationTypeEnum value) => ConfigWishes.WithInterpolation(obj, value);
        public static TapeActions AsInterpolation(this TapeActions obj, InterpolationTypeEnum value) => ConfigWishes.AsInterpolation(obj, value);
        public static TapeActions SetInterpolation(this TapeActions obj, InterpolationTypeEnum value) => ConfigWishes.SetInterpolation(obj, value);

        // Independent after Taping

        public static bool IsLinear(this Sample obj) => ConfigWishes.IsLinear(obj);
        public static bool IsBlocky(this Sample obj) => ConfigWishes.IsBlocky(obj);
        public static InterpolationTypeEnum Interpolation(this Sample obj) => ConfigWishes.Interpolation(obj);
        public static InterpolationTypeEnum GetInterpolation(this Sample obj) => ConfigWishes.GetInterpolation(obj);

        public static Sample Linear(this Sample obj, IContext context) => ConfigWishes.Linear(obj, context);
        public static Sample Blocky(this Sample obj, IContext context) => ConfigWishes.Blocky(obj, context);
        public static Sample WithLinear(this Sample obj, IContext context) => ConfigWishes.WithLinear(obj, context);
        public static Sample WithBlocky(this Sample obj, IContext context) => ConfigWishes.WithBlocky(obj, context);
        public static Sample AsLinear(this Sample obj, IContext context) => ConfigWishes.AsLinear(obj, context);
        public static Sample AsBlocky(this Sample obj, IContext context) => ConfigWishes.AsBlocky(obj, context);
        public static Sample SetLinear(this Sample obj, IContext context) => ConfigWishes.SetLinear(obj, context);
        public static Sample SetBlocky(this Sample obj, IContext context) => ConfigWishes.SetBlocky(obj, context);
        public static Sample Interpolation(this Sample obj, InterpolationTypeEnum value, IContext context) => ConfigWishes.Interpolation(obj, value, context);
        public static Sample WithInterpolation(this Sample obj, InterpolationTypeEnum value, IContext context) => ConfigWishes.WithInterpolation(obj, value, context);
        public static Sample AsInterpolation(this Sample obj, InterpolationTypeEnum value, IContext context) => ConfigWishes.AsInterpolation(obj, value, context);
        public static Sample SetInterpolation(this Sample obj, InterpolationTypeEnum value, IContext context) => ConfigWishes.SetInterpolation(obj, value, context);

        // Immutable

        public static bool IsLinear(this InterpolationTypeEnum obj) => ConfigWishes.IsLinear(obj);
        public static bool IsBlocky(this InterpolationTypeEnum obj) => ConfigWishes.IsBlocky(obj);
        public static InterpolationTypeEnum Interpolation(this InterpolationTypeEnum obj) => ConfigWishes.Interpolation(obj);
        public static InterpolationTypeEnum GetInterpolation(this InterpolationTypeEnum obj) => ConfigWishes.GetInterpolation(obj);

        /// <inheritdoc cref="_quasisetter" />
        public static InterpolationTypeEnum Linear(this InterpolationTypeEnum oldInterpolation) => ConfigWishes.Linear(oldInterpolation);
        /// <inheritdoc cref="_quasisetter" />
        public static InterpolationTypeEnum Blocky(this InterpolationTypeEnum oldInterpolation) => ConfigWishes.Blocky(oldInterpolation);
        /// <inheritdoc cref="_quasisetter" />
        public static InterpolationTypeEnum WithLinear(this InterpolationTypeEnum oldInterpolation) => ConfigWishes.WithLinear(oldInterpolation);
        /// <inheritdoc cref="_quasisetter" />
        public static InterpolationTypeEnum WithBlocky(this InterpolationTypeEnum oldInterpolation) => ConfigWishes.WithBlocky(oldInterpolation);
        /// <inheritdoc cref="_quasisetter" />
        public static InterpolationTypeEnum AsLinear(this InterpolationTypeEnum oldInterpolation) => ConfigWishes.AsLinear(oldInterpolation);
        /// <inheritdoc cref="_quasisetter" />
        public static InterpolationTypeEnum AsBlocky(this InterpolationTypeEnum oldInterpolation) => ConfigWishes.AsBlocky(oldInterpolation);
        /// <inheritdoc cref="_quasisetter" />
        public static InterpolationTypeEnum ToLinear(this InterpolationTypeEnum oldInterpolation) => ConfigWishes.ToLinear(oldInterpolation);
        /// <inheritdoc cref="_quasisetter" />
        public static InterpolationTypeEnum ToBlocky(this InterpolationTypeEnum oldInterpolation) => ConfigWishes.ToBlocky(oldInterpolation);
        /// <inheritdoc cref="_quasisetter" />
        public static InterpolationTypeEnum SetLinear(this InterpolationTypeEnum oldInterpolation) => ConfigWishes.SetLinear(oldInterpolation);
        /// <inheritdoc cref="_quasisetter" />
        public static InterpolationTypeEnum SetBlocky(this InterpolationTypeEnum oldInterpolation) => ConfigWishes.SetBlocky(oldInterpolation);
        /// <inheritdoc cref="_quasisetter" />
        public static InterpolationTypeEnum Interpolation(this InterpolationTypeEnum oldEnumValue, InterpolationTypeEnum newEnumValue) => ConfigWishes.Interpolation(oldEnumValue, newEnumValue);
        /// <inheritdoc cref="_quasisetter" />
        public static InterpolationTypeEnum WithInterpolation(this InterpolationTypeEnum oldEnumValue, InterpolationTypeEnum newEnumValue) => ConfigWishes.WithInterpolation(oldEnumValue, newEnumValue);
        /// <inheritdoc cref="_quasisetter" />
        public static InterpolationTypeEnum AsInterpolation(this InterpolationTypeEnum oldEnumValue, InterpolationTypeEnum newEnumValue) => ConfigWishes.AsInterpolation(oldEnumValue, newEnumValue);
        /// <inheritdoc cref="_quasisetter" />
        public static InterpolationTypeEnum ToInterpolation(this InterpolationTypeEnum oldEnumValue, InterpolationTypeEnum newEnumValue) => ConfigWishes.ToInterpolation(oldEnumValue, newEnumValue);
        /// <inheritdoc cref="_quasisetter" />
        public static InterpolationTypeEnum SetInterpolation(this InterpolationTypeEnum oldEnumValue, InterpolationTypeEnum newEnumValue) => ConfigWishes.SetInterpolation(oldEnumValue, newEnumValue);


        [Obsolete(ObsoleteMessage)]
        public static bool IsLinear(this InterpolationType obj) => ConfigWishes.IsLinear(obj);
        [Obsolete(ObsoleteMessage)]
        public static bool IsBlocky(this InterpolationType obj) => ConfigWishes.IsBlocky(obj);
        [Obsolete(ObsoleteMessage)]
        public static InterpolationTypeEnum Interpolation(this InterpolationType obj) => ConfigWishes.Interpolation(obj);
        [Obsolete(ObsoleteMessage)]
        public static InterpolationTypeEnum GetInterpolation(this InterpolationType obj) => ConfigWishes.GetInterpolation(obj);
        [Obsolete(ObsoleteMessage)]
        public static InterpolationTypeEnum AsInterpolation(this InterpolationType obj) => ConfigWishes.AsInterpolation(obj);
        [Obsolete(ObsoleteMessage)]
        public static InterpolationTypeEnum ToInterpolation(this InterpolationType obj) => ConfigWishes.ToInterpolation(obj);
        [Obsolete(ObsoleteMessage)]
        public static InterpolationTypeEnum InterpolationEntityToEnum(this InterpolationType obj) => ConfigWishes.InterpolationEntityToEnum(obj);
        [Obsolete(ObsoleteMessage)]
        public static InterpolationTypeEnum InterpolationEntityAsEnum(this InterpolationType obj) => ConfigWishes.InterpolationEntityAsEnum(obj);
        [Obsolete(ObsoleteMessage)]
        public static InterpolationTypeEnum ToEnum(this InterpolationType obj) => ConfigWishes.ToEnum(obj);
        [Obsolete(ObsoleteMessage)]
        public static InterpolationTypeEnum AsEnum(this InterpolationType obj) => ConfigWishes.AsEnum(obj);
        [Obsolete(ObsoleteMessage)]
        public static InterpolationTypeEnum EntityAsEnum(this InterpolationType obj) => ConfigWishes.EntityAsEnum(obj);
        [Obsolete(ObsoleteMessage)]
        public static InterpolationTypeEnum EntityToEnum(this InterpolationType obj) => ConfigWishes.EntityToEnum(obj);

        /// <inheritdoc cref="_quasisetter" />
        [Obsolete(ObsoleteMessage)]
        public static InterpolationType Linear(this InterpolationType oldEntity, IContext context) => ConfigWishes.Linear(oldEntity, context);
        /// <inheritdoc cref="_quasisetter" />
        [Obsolete(ObsoleteMessage)]
        public static InterpolationType Blocky(this InterpolationType oldEntity, IContext context) => ConfigWishes.Blocky(oldEntity, context);
        /// <inheritdoc cref="_quasisetter" />
        [Obsolete(ObsoleteMessage)]
        public static InterpolationType WithLinear(this InterpolationType oldEntity, IContext context) => ConfigWishes.WithLinear(oldEntity, context);
        /// <inheritdoc cref="_quasisetter" />
        [Obsolete(ObsoleteMessage)]
        public static InterpolationType WithBlocky(this InterpolationType oldEntity, IContext context) => ConfigWishes.WithBlocky(oldEntity, context);
        /// <inheritdoc cref="_quasisetter" />
        [Obsolete(ObsoleteMessage)]
        public static InterpolationType AsLinear(this InterpolationType oldEntity, IContext context) => ConfigWishes.AsLinear(oldEntity, context);
        /// <inheritdoc cref="_quasisetter" />
        [Obsolete(ObsoleteMessage)]
        public static InterpolationType AsBlocky(this InterpolationType oldEntity, IContext context) => ConfigWishes.AsBlocky(oldEntity, context);
        /// <inheritdoc cref="_quasisetter" />
        [Obsolete(ObsoleteMessage)]
        public static InterpolationType ToLinear(this InterpolationType oldEntity, IContext context) => ConfigWishes.ToLinear(oldEntity, context);
        /// <inheritdoc cref="_quasisetter" />
        [Obsolete(ObsoleteMessage)]
        public static InterpolationType ToBlocky(this InterpolationType oldEntity, IContext context) => ConfigWishes.ToBlocky(oldEntity, context);
        /// <inheritdoc cref="_quasisetter" />
        [Obsolete(ObsoleteMessage)]
        public static InterpolationType SetLinear(this InterpolationType oldEntity, IContext context) => ConfigWishes.SetLinear(oldEntity, context);
        /// <inheritdoc cref="_quasisetter" />
        [Obsolete(ObsoleteMessage)]
        public static InterpolationType SetBlocky(this InterpolationType oldEntity, IContext context) => ConfigWishes.SetBlocky(oldEntity, context);
        /// <inheritdoc cref="_quasisetter" />
        [Obsolete(ObsoleteMessage)]
        public static InterpolationType Interpolation(this InterpolationType oldEntity, InterpolationTypeEnum newEnumValue, IContext context) => ConfigWishes.Interpolation(oldEntity, newEnumValue, context);
        /// <inheritdoc cref="_quasisetter" />
        [Obsolete(ObsoleteMessage)]
        public static InterpolationType WithInterpolation(this InterpolationType oldEntity, InterpolationTypeEnum newEnumValue, IContext context) => ConfigWishes.WithInterpolation(oldEntity, newEnumValue, context);
        /// <inheritdoc cref="_quasisetter" />
        [Obsolete(ObsoleteMessage)]
        public static InterpolationType AsInterpolation(this InterpolationType oldEntity, InterpolationTypeEnum newEnumValue, IContext context) => ConfigWishes.AsInterpolation(oldEntity, newEnumValue, context);
        /// <inheritdoc cref="_quasisetter" />
        [Obsolete(ObsoleteMessage)]
        public static InterpolationType ToInterpolation(this InterpolationType oldEntity, InterpolationTypeEnum newEnumValue, IContext context) => ConfigWishes.ToInterpolation(oldEntity, newEnumValue, context);
        /// <inheritdoc cref="_quasisetter" />
        [Obsolete(ObsoleteMessage)]
        public static InterpolationType SetInterpolation(this InterpolationType oldEntity, InterpolationTypeEnum newEnumValue, IContext context) => ConfigWishes.SetInterpolation(oldEntity, newEnumValue, context);
        [Obsolete(ObsoleteMessage)]
        public static InterpolationType InterpolationEnumToEntity(this InterpolationTypeEnum enumValue, IContext context) => ConfigWishes.InterpolationEnumToEntity(enumValue, context);
        [Obsolete(ObsoleteMessage)]
        public static InterpolationType InterpolationEnumAsEntity(this InterpolationTypeEnum enumValue, IContext context) => ConfigWishes.InterpolationEnumAsEntity(enumValue, context);
        [Obsolete(ObsoleteMessage)]
        public static InterpolationType ToEntity(this InterpolationTypeEnum enumValue, IContext context) => ConfigWishes.ToEntity(enumValue, context);
        [Obsolete(ObsoleteMessage)]
        public static InterpolationType AsEntity(this InterpolationTypeEnum enumValue, IContext context) => ConfigWishes.AsEntity(enumValue, context);
        [Obsolete(ObsoleteMessage)]
        public static InterpolationType EnumAsEntity(this InterpolationTypeEnum enumValue, IContext context) => ConfigWishes.EnumAsEntity(enumValue, context);
        [Obsolete(ObsoleteMessage)]
        public static InterpolationType EnumToEntity(this InterpolationTypeEnum enumValue, IContext context) => ConfigWishes.EnumToEntity(enumValue, context);
    }
    
    public partial class ConfigWishes
    {
        // Synth-Bound
        
        public static bool IsLinear(SynthWishes obj) => GetInterpolation(obj) == Line;
        public static bool IsBlocky(SynthWishes obj) => GetInterpolation(obj) == Block;
        public static InterpolationTypeEnum Interpolation(SynthWishes obj) => GetInterpolation(obj);
        public static InterpolationTypeEnum GetInterpolation(SynthWishes obj)
        {
            if (obj == null) throw new NullException(() => obj);
            return obj.GetInterpolation;
        }
        
        public static SynthWishes Linear(SynthWishes obj) => SetInterpolation(obj, Line);
        public static SynthWishes Blocky(SynthWishes obj) => SetInterpolation(obj, Block);
        public static SynthWishes WithLinear(SynthWishes obj) => SetInterpolation(obj, Line);
        public static SynthWishes WithBlocky(SynthWishes obj) => SetInterpolation(obj, Block);
        public static SynthWishes AsLinear(SynthWishes obj) => SetInterpolation(obj, Line);
        public static SynthWishes AsBlocky(SynthWishes obj) => SetInterpolation(obj, Block);
        public static SynthWishes SetLinear(SynthWishes obj) => SetInterpolation(obj, Line);
        public static SynthWishes SetBlocky(SynthWishes obj) => SetInterpolation(obj, Block);
        public static SynthWishes Interpolation(SynthWishes obj, InterpolationTypeEnum? value) => SetInterpolation(obj, value);
        public static SynthWishes WithInterpolation(SynthWishes obj, InterpolationTypeEnum? value) => SetInterpolation(obj, value);
        public static SynthWishes AsInterpolation(SynthWishes obj, InterpolationTypeEnum? value) => SetInterpolation(obj, value);
        public static SynthWishes SetInterpolation(SynthWishes obj, InterpolationTypeEnum? value)
        {
            if (obj == null) throw new NullException(() => obj);
            return obj.WithInterpolation(value);
        }
        
        public static bool IsLinear(FlowNode obj) => GetInterpolation(obj) == Line;
        public static bool IsBlocky(FlowNode obj) => GetInterpolation(obj) == Block;
        public static InterpolationTypeEnum Interpolation(FlowNode obj) => GetInterpolation(obj);
        public static InterpolationTypeEnum GetInterpolation(FlowNode obj)
        {
            if (obj == null) throw new NullException(() => obj);
            return obj.GetInterpolation;
        }
        
        public static FlowNode Linear(FlowNode obj) => SetInterpolation(obj, Line);
        public static FlowNode Blocky(FlowNode obj) => SetInterpolation(obj, Block);
        public static FlowNode WithLinear(FlowNode obj) => SetInterpolation(obj, Line);
        public static FlowNode WithBlocky(FlowNode obj) => SetInterpolation(obj, Block);
        public static FlowNode AsLinear(FlowNode obj) => SetInterpolation(obj, Line);
        public static FlowNode AsBlocky(FlowNode obj) => SetInterpolation(obj, Block);
        public static FlowNode SetLinear(FlowNode obj) => SetInterpolation(obj, Line);
        public static FlowNode SetBlocky(FlowNode obj) => SetInterpolation(obj, Block);
        public static FlowNode Interpolation(FlowNode obj, InterpolationTypeEnum? value) => SetInterpolation(obj, value);
        public static FlowNode WithInterpolation(FlowNode obj, InterpolationTypeEnum? value) => SetInterpolation(obj, value);
        public static FlowNode AsInterpolation(FlowNode obj, InterpolationTypeEnum? value) => SetInterpolation(obj, value);
        public static FlowNode SetInterpolation(FlowNode obj, InterpolationTypeEnum? value)
        {
            if (obj == null) throw new NullException(() => obj);
            return obj.WithInterpolation(value);
        }

        [UsedImplicitly] internal static bool IsLinear(ConfigResolver obj) => GetInterpolation(obj) == Line;
        [UsedImplicitly] internal static bool IsBlocky(ConfigResolver obj) => GetInterpolation(obj) == Block;
        [UsedImplicitly] internal static InterpolationTypeEnum Interpolation(ConfigResolver obj) => GetInterpolation(obj);
        [UsedImplicitly] internal static InterpolationTypeEnum GetInterpolation(ConfigResolver obj)
        {
            if (obj == null) throw new NullException(() => obj);
            return obj.GetInterpolation;
        }
        
        [UsedImplicitly] internal static ConfigResolver Linear(ConfigResolver obj) => SetInterpolation(obj, Line);
        [UsedImplicitly] internal static ConfigResolver Blocky(ConfigResolver obj) => SetInterpolation(obj, Block);
        [UsedImplicitly] internal static ConfigResolver WithLinear(ConfigResolver obj) => SetInterpolation(obj, Line);
        [UsedImplicitly] internal static ConfigResolver WithBlocky(ConfigResolver obj) => SetInterpolation(obj, Block);
        [UsedImplicitly] internal static ConfigResolver AsLinear(ConfigResolver obj) => SetInterpolation(obj, Line);
        [UsedImplicitly] internal static ConfigResolver AsBlocky(ConfigResolver obj) => SetInterpolation(obj, Block);
        [UsedImplicitly] internal static ConfigResolver SetLinear(ConfigResolver obj) => SetInterpolation(obj, Line);
        [UsedImplicitly] internal static ConfigResolver SetBlocky(ConfigResolver obj) => SetInterpolation(obj, Block);
        [UsedImplicitly] internal static ConfigResolver Interpolation(ConfigResolver obj, InterpolationTypeEnum? value) => SetInterpolation(obj, value);
        [UsedImplicitly] internal static ConfigResolver WithInterpolation(ConfigResolver obj, InterpolationTypeEnum? value) => SetInterpolation(obj, value);
        [UsedImplicitly] internal static ConfigResolver AsInterpolation(ConfigResolver obj, InterpolationTypeEnum? value) => SetInterpolation(obj, value);
        [UsedImplicitly] internal static ConfigResolver SetInterpolation(ConfigResolver obj, InterpolationTypeEnum? value)
        {
            if (obj == null) throw new NullException(() => obj);
            return obj.WithInterpolation(value);
        }

        // Global-Bound

        [UsedImplicitly] internal static bool IsLinear(ConfigSection obj) => GetInterpolation(obj) == Line;
        [UsedImplicitly] internal static bool IsBlocky(ConfigSection obj) => GetInterpolation(obj) == Block;
        [UsedImplicitly] internal static InterpolationTypeEnum? Interpolation(ConfigSection obj) => GetInterpolation(obj);
        [UsedImplicitly] internal static InterpolationTypeEnum? GetInterpolation(ConfigSection obj)
        {
            if (obj == null) throw new NullException(() => obj);
            return obj.Interpolation;
        }
        
        // Tape-Bound
        
        public static bool IsLinear(Tape obj) => GetInterpolation(obj) == Line;
        public static bool IsBlocky(Tape obj) => GetInterpolation(obj) == Block;
        public static InterpolationTypeEnum Interpolation(Tape obj) => GetInterpolation(obj);
        public static InterpolationTypeEnum GetInterpolation(Tape obj)
        {
            if (obj == null) throw new NullException(() => obj);
            return obj.Config.Interpolation;
        }
        
        public static Tape Linear(Tape obj) => SetInterpolation(obj, Line);
        public static Tape Blocky(Tape obj) => SetInterpolation(obj, Block);
        public static Tape WithLinear(Tape obj) => SetInterpolation(obj, Line);
        public static Tape WithBlocky(Tape obj) => SetInterpolation(obj, Block);
        public static Tape AsLinear(Tape obj) => SetInterpolation(obj, Line);
        public static Tape AsBlocky(Tape obj) => SetInterpolation(obj, Block);
        public static Tape SetLinear(Tape obj) => SetInterpolation(obj, Line);
        public static Tape SetBlocky(Tape obj) => SetInterpolation(obj, Block);
        public static Tape Interpolation(Tape obj, InterpolationTypeEnum value) => SetInterpolation(obj, value);
        public static Tape WithInterpolation(Tape obj, InterpolationTypeEnum value) => SetInterpolation(obj, value);
        public static Tape AsInterpolation(Tape obj, InterpolationTypeEnum value) => SetInterpolation(obj, value);
        public static Tape SetInterpolation(Tape obj, InterpolationTypeEnum value)
        {
            if (obj == null) throw new NullException(() => obj);
            obj.Config.Interpolation = value;
            return obj;
        }
        
        public static bool IsLinear(TapeConfig obj) => GetInterpolation(obj) == Line;
        public static bool IsBlocky(TapeConfig obj) => GetInterpolation(obj) == Block;
        public static InterpolationTypeEnum Interpolation(TapeConfig obj) => GetInterpolation(obj);
        public static InterpolationTypeEnum GetInterpolation(TapeConfig obj)
        {
            if (obj == null) throw new NullException(() => obj);
            return obj.Interpolation;
        }
        
        public static TapeConfig Linear(TapeConfig obj) => SetInterpolation(obj, Line);
        public static TapeConfig Blocky(TapeConfig obj) => SetInterpolation(obj, Block);
        public static TapeConfig WithLinear(TapeConfig obj) => SetInterpolation(obj, Line);
        public static TapeConfig WithBlocky(TapeConfig obj) => SetInterpolation(obj, Block);
        public static TapeConfig AsLinear(TapeConfig obj) => SetInterpolation(obj, Line);
        public static TapeConfig AsBlocky(TapeConfig obj) => SetInterpolation(obj, Block);
        public static TapeConfig SetLinear(TapeConfig obj) => SetInterpolation(obj, Line);
        public static TapeConfig SetBlocky(TapeConfig obj) => SetInterpolation(obj, Block);
        public static TapeConfig Interpolation(TapeConfig obj, InterpolationTypeEnum value) => SetInterpolation(obj, value);
        public static TapeConfig WithInterpolation(TapeConfig obj, InterpolationTypeEnum value) => SetInterpolation(obj, value);
        public static TapeConfig AsInterpolation(TapeConfig obj, InterpolationTypeEnum value) => SetInterpolation(obj, value);
        public static TapeConfig SetInterpolation(TapeConfig obj, InterpolationTypeEnum value)
        {
            if (obj == null) throw new NullException(() => obj);
            obj.Interpolation = value;
            return obj;
        }
        
        public static bool IsLinear(TapeAction obj) => GetInterpolation(obj) == Line;
        public static bool IsBlocky(TapeAction obj) => GetInterpolation(obj) == Block;
        public static InterpolationTypeEnum Interpolation(TapeAction obj) => GetInterpolation(obj);
        public static InterpolationTypeEnum GetInterpolation(TapeAction obj)
        {
            if (obj == null) throw new NullException(() => obj);
            return obj.Tape.Config.Interpolation;
        }
        
        public static TapeAction Linear(TapeAction obj) => SetInterpolation(obj, Line);
        public static TapeAction Blocky(TapeAction obj) => SetInterpolation(obj, Block);
        public static TapeAction WithLinear(TapeAction obj) => SetInterpolation(obj, Line);
        public static TapeAction WithBlocky(TapeAction obj) => SetInterpolation(obj, Block);
        public static TapeAction AsLinear(TapeAction obj) => SetInterpolation(obj, Line);
        public static TapeAction AsBlocky(TapeAction obj) => SetInterpolation(obj, Block);
        public static TapeAction SetLinear(TapeAction obj) => SetInterpolation(obj, Line);
        public static TapeAction SetBlocky(TapeAction obj) => SetInterpolation(obj, Block);
        public static TapeAction Interpolation(TapeAction obj, InterpolationTypeEnum value) => SetInterpolation(obj, value);
        public static TapeAction WithInterpolation(TapeAction obj, InterpolationTypeEnum value) => SetInterpolation(obj, value);
        public static TapeAction AsInterpolation(TapeAction obj, InterpolationTypeEnum value) => SetInterpolation(obj, value);
        public static TapeAction SetInterpolation(TapeAction obj, InterpolationTypeEnum value)
        {
            if (obj == null) throw new NullException(() => obj);
            obj.Tape.Config.Interpolation = value;
            return obj;
        }
        
        public static bool IsLinear(TapeActions obj) => GetInterpolation(obj) == Line;
        public static bool IsBlocky(TapeActions obj) => GetInterpolation(obj) == Block;
        public static InterpolationTypeEnum Interpolation(TapeActions obj) => GetInterpolation(obj);
        public static InterpolationTypeEnum GetInterpolation(TapeActions obj)
        {
            if (obj == null) throw new NullException(() => obj);
            return obj.Tape.Config.Interpolation;
        }
        
        public static TapeActions Linear(TapeActions obj) => SetInterpolation(obj, Line);
        public static TapeActions Blocky(TapeActions obj) => SetInterpolation(obj, Block);
        public static TapeActions WithLinear(TapeActions obj) => SetInterpolation(obj, Line);
        public static TapeActions WithBlocky(TapeActions obj) => SetInterpolation(obj, Block);
        public static TapeActions AsLinear(TapeActions obj) => SetInterpolation(obj, Line);
        public static TapeActions AsBlocky(TapeActions obj) => SetInterpolation(obj, Block);
        public static TapeActions SetLinear(TapeActions obj) => SetInterpolation(obj, Line);
        public static TapeActions SetBlocky(TapeActions obj) => SetInterpolation(obj, Block);
        public static TapeActions Interpolation(TapeActions obj, InterpolationTypeEnum value) => SetInterpolation(obj, value);
        public static TapeActions WithInterpolation(TapeActions obj, InterpolationTypeEnum value) => SetInterpolation(obj, value);
        public static TapeActions AsInterpolation(TapeActions obj, InterpolationTypeEnum value) => SetInterpolation(obj, value);
        public static TapeActions SetInterpolation(TapeActions obj, InterpolationTypeEnum value)
        {
            if (obj == null) throw new NullException(() => obj);
            obj.Tape.Config.Interpolation = value;
            return obj;
        }
        
        // Independent after Taping
        
        public static bool IsLinear(Sample obj) => GetInterpolation(obj) == Line;
        public static bool IsBlocky(Sample obj) => GetInterpolation(obj) == Block;
        public static InterpolationTypeEnum Interpolation(Sample obj) => GetInterpolation(obj);
        public static InterpolationTypeEnum GetInterpolation(Sample obj)
        {
            return obj.GetInterpolationTypeEnum();
        }
        
        public static Sample Linear(Sample obj, IContext context) => SetInterpolation(obj, Line, context);
        public static Sample Blocky(Sample obj, IContext context) => SetInterpolation(obj, Block, context);
        public static Sample WithLinear(Sample obj, IContext context) => SetInterpolation(obj, Line, context);
        public static Sample WithBlocky(Sample obj, IContext context) => SetInterpolation(obj, Block, context);
        public static Sample AsLinear(Sample obj, IContext context) => SetInterpolation(obj, Line, context);
        public static Sample AsBlocky(Sample obj, IContext context) => SetInterpolation(obj, Block, context);
        public static Sample SetLinear(Sample obj, IContext context) => SetInterpolation(obj, Line, context);
        public static Sample SetBlocky(Sample obj, IContext context) => SetInterpolation(obj, Block, context);
        public static Sample Interpolation(Sample obj, InterpolationTypeEnum value, IContext context) => SetInterpolation(obj, value, context);
        public static Sample WithInterpolation(Sample obj, InterpolationTypeEnum value, IContext context) => SetInterpolation(obj, value, context);
        public static Sample AsInterpolation(Sample obj, InterpolationTypeEnum value, IContext context) => SetInterpolation(obj, value, context);
        public static Sample SetInterpolation(Sample obj, InterpolationTypeEnum value, IContext context)
        {
            if (obj == null) throw new NullException(() => obj);
            obj.SetInterpolationTypeEnum(value, context);
            return obj;
        }
        
        // Immutable
        
        public static bool IsLinear(InterpolationTypeEnum obj) => GetInterpolation(obj) == Line;
        public static bool IsBlocky(InterpolationTypeEnum obj) => GetInterpolation(obj) == Block;
        public static InterpolationTypeEnum Interpolation(InterpolationTypeEnum obj) => GetInterpolation(obj);
        public static InterpolationTypeEnum GetInterpolation(InterpolationTypeEnum obj)
        {
            return AssertInterpolation(obj);
        }
        
        /// <inheritdoc cref="_quasisetter" />
        public static InterpolationTypeEnum Linear(InterpolationTypeEnum oldInterpolation)
            => SetInterpolation(oldInterpolation, Line);
        /// <inheritdoc cref="_quasisetter" />
        public static InterpolationTypeEnum Blocky(InterpolationTypeEnum oldInterpolation)
            => SetInterpolation(oldInterpolation, Block);
        /// <inheritdoc cref="_quasisetter" />
        public static InterpolationTypeEnum WithLinear(InterpolationTypeEnum oldInterpolation)
            => SetInterpolation(oldInterpolation, Line);
        /// <inheritdoc cref="_quasisetter" />
        public static InterpolationTypeEnum WithBlocky(InterpolationTypeEnum oldInterpolation)
            => SetInterpolation(oldInterpolation, Block);
        /// <inheritdoc cref="_quasisetter" />
        public static InterpolationTypeEnum AsLinear(InterpolationTypeEnum oldInterpolation)
            => SetInterpolation(oldInterpolation, Line);
        /// <inheritdoc cref="_quasisetter" />
        public static InterpolationTypeEnum AsBlocky(InterpolationTypeEnum oldInterpolation)
            => SetInterpolation(oldInterpolation, Block);
        /// <inheritdoc cref="_quasisetter" />
        public static InterpolationTypeEnum ToLinear(InterpolationTypeEnum oldInterpolation)
            => SetInterpolation(oldInterpolation, Line);
        /// <inheritdoc cref="_quasisetter" />
        public static InterpolationTypeEnum ToBlocky(InterpolationTypeEnum oldInterpolation)
            => SetInterpolation(oldInterpolation, Block);
        /// <inheritdoc cref="_quasisetter" />
        public static InterpolationTypeEnum SetLinear(InterpolationTypeEnum oldInterpolation)
            => SetInterpolation(oldInterpolation, Line);
        /// <inheritdoc cref="_quasisetter" />
        public static InterpolationTypeEnum SetBlocky(InterpolationTypeEnum oldInterpolation)
            => SetInterpolation(oldInterpolation, Block);
        /// <inheritdoc cref="_quasisetter" />
        public static InterpolationTypeEnum Interpolation(InterpolationTypeEnum oldEnumValue, InterpolationTypeEnum newEnumValue)
            => SetInterpolation(oldEnumValue, newEnumValue);
        /// <inheritdoc cref="_quasisetter" />
        public static InterpolationTypeEnum WithInterpolation(InterpolationTypeEnum oldEnumValue, InterpolationTypeEnum newEnumValue)
            => SetInterpolation(oldEnumValue, newEnumValue);
        /// <inheritdoc cref="_quasisetter" />
        public static InterpolationTypeEnum AsInterpolation(InterpolationTypeEnum oldEnumValue, InterpolationTypeEnum newEnumValue)
            => SetInterpolation(oldEnumValue, newEnumValue);
        /// <inheritdoc cref="_quasisetter" />
        public static InterpolationTypeEnum ToInterpolation(InterpolationTypeEnum oldEnumValue, InterpolationTypeEnum newEnumValue)
            => SetInterpolation(oldEnumValue, newEnumValue);
        /// <inheritdoc cref="_quasisetter" />
        public static InterpolationTypeEnum SetInterpolation(InterpolationTypeEnum oldEnumValue, InterpolationTypeEnum newEnumValue)
        {
            return AssertInterpolation(newEnumValue);
        }

        [Obsolete(ObsoleteMessage)] public static bool IsLinear(InterpolationType obj) => EntityToEnum(obj) == Line;
        [Obsolete(ObsoleteMessage)] public static bool IsBlocky(InterpolationType obj) => EntityToEnum(obj) == Block;
        [Obsolete(ObsoleteMessage)] public static InterpolationTypeEnum Interpolation(InterpolationType obj) => EntityToEnum(obj);
        [Obsolete(ObsoleteMessage)] public static InterpolationTypeEnum GetInterpolation(InterpolationType obj) => EntityToEnum(obj);
        [Obsolete(ObsoleteMessage)] public static InterpolationTypeEnum AsInterpolation(InterpolationType obj) => EntityToEnum(obj);
        [Obsolete(ObsoleteMessage)] public static InterpolationTypeEnum ToInterpolation(InterpolationType obj) => EntityToEnum(obj);
        [Obsolete(ObsoleteMessage)] public static InterpolationTypeEnum InterpolationEntityToEnum(InterpolationType obj) => EntityToEnum(obj);
        [Obsolete(ObsoleteMessage)] public static InterpolationTypeEnum InterpolationEntityAsEnum(InterpolationType obj) => EntityToEnum(obj);
        [Obsolete(ObsoleteMessage)] public static InterpolationTypeEnum ToEnum(InterpolationType obj) => EntityToEnum(obj);
        [Obsolete(ObsoleteMessage)] public static InterpolationTypeEnum AsEnum(InterpolationType obj) => EntityToEnum(obj);
        [Obsolete(ObsoleteMessage)] public static InterpolationTypeEnum EntityAsEnum(InterpolationType obj) => EntityToEnum(obj);
        [Obsolete(ObsoleteMessage)] public static InterpolationTypeEnum EntityToEnum(InterpolationType obj)
        {
            if (obj == null) throw new NullException(() => obj);
            return (InterpolationTypeEnum)obj.ID;
        }

        /// <inheritdoc cref="_quasisetter" />
        [Obsolete(ObsoleteMessage)]
        public static InterpolationType Linear(InterpolationType oldEntity, IContext context) => EnumToEntity(Line, context);
        /// <inheritdoc cref="_quasisetter" />
        [Obsolete(ObsoleteMessage)]
        public static InterpolationType Blocky(InterpolationType oldEntity, IContext context) => EnumToEntity(Block, context);
        /// <inheritdoc cref="_quasisetter" />
        [Obsolete(ObsoleteMessage)]
        public static InterpolationType WithLinear(InterpolationType oldEntity, IContext context) => EnumToEntity(Line, context);
        /// <inheritdoc cref="_quasisetter" />
        [Obsolete(ObsoleteMessage)]
        public static InterpolationType WithBlocky(InterpolationType oldEntity, IContext context) => EnumToEntity(Block, context);
        /// <inheritdoc cref="_quasisetter" />
        [Obsolete(ObsoleteMessage)]
        public static InterpolationType AsLinear(InterpolationType oldEntity, IContext context) => EnumToEntity(Line, context);
        /// <inheritdoc cref="_quasisetter" />
        [Obsolete(ObsoleteMessage)]
        public static InterpolationType AsBlocky(InterpolationType oldEntity, IContext context) => EnumToEntity(Block, context);
        /// <inheritdoc cref="_quasisetter" />
        [Obsolete(ObsoleteMessage)]
        public static InterpolationType ToLinear(InterpolationType oldEntity, IContext context) => EnumToEntity(Line, context);
        /// <inheritdoc cref="_quasisetter" />
        [Obsolete(ObsoleteMessage)]
        public static InterpolationType ToBlocky(InterpolationType oldEntity, IContext context) => EnumToEntity(Block, context);
        /// <inheritdoc cref="_quasisetter" />
        [Obsolete(ObsoleteMessage)]
        public static InterpolationType SetLinear(InterpolationType oldEntity, IContext context) => EnumToEntity(Line, context);
        /// <inheritdoc cref="_quasisetter" />
        [Obsolete(ObsoleteMessage)]
        public static InterpolationType SetBlocky(InterpolationType oldEntity, IContext context) => EnumToEntity(Block, context);
        /// <inheritdoc cref="_quasisetter" />
        [Obsolete(ObsoleteMessage)] 
        public static InterpolationType Interpolation(InterpolationType oldEntity, InterpolationTypeEnum newEnumValue, IContext context) => EnumToEntity(newEnumValue, context);
        /// <inheritdoc cref="_quasisetter" />
        [Obsolete(ObsoleteMessage)] 
        public static InterpolationType WithInterpolation(InterpolationType oldEntity, InterpolationTypeEnum newEnumValue, IContext context) => EnumToEntity(newEnumValue, context);
        /// <inheritdoc cref="_quasisetter" />
        [Obsolete(ObsoleteMessage)] 
        public static InterpolationType AsInterpolation(InterpolationType oldEntity, InterpolationTypeEnum newEnumValue, IContext context) => EnumToEntity(newEnumValue, context);
        /// <inheritdoc cref="_quasisetter" />
        [Obsolete(ObsoleteMessage)] 
        public static InterpolationType ToInterpolation(InterpolationType oldEntity, InterpolationTypeEnum newEnumValue, IContext context) => EnumToEntity(newEnumValue, context);
        /// <inheritdoc cref="_quasisetter" />
        [Obsolete(ObsoleteMessage)] 
        public static InterpolationType SetInterpolation(InterpolationType oldEntity, InterpolationTypeEnum newEnumValue, IContext context) => EnumToEntity(newEnumValue, context);
        [Obsolete(ObsoleteMessage)] 
        public static InterpolationType InterpolationEnumToEntity(InterpolationTypeEnum enumValue, IContext context) => EnumToEntity(enumValue, context);
        [Obsolete(ObsoleteMessage)] 
        public static InterpolationType InterpolationEnumAsEntity(InterpolationTypeEnum enumValue, IContext context) => EnumToEntity(enumValue, context);
        [Obsolete(ObsoleteMessage)] 
        public static InterpolationType ToEntity(InterpolationTypeEnum enumValue, IContext context) => EnumToEntity(enumValue, context);
        [Obsolete(ObsoleteMessage)] 
        public static InterpolationType AsEntity(InterpolationTypeEnum enumValue, IContext context) => EnumToEntity(enumValue, context);
        [Obsolete(ObsoleteMessage)] 
        public static InterpolationType EnumAsEntity(InterpolationTypeEnum enumValue, IContext context) => EnumToEntity(enumValue, context);
        [Obsolete(ObsoleteMessage)] 
        public static InterpolationType EnumToEntity(InterpolationTypeEnum enumValue, IContext context)
        {
            if (enumValue == default) return default;
            AssertInterpolation(enumValue);
            var repository = CreateRepository<IInterpolationTypeRepository>(context);
            return repository.Get((int)enumValue);
        }
    }
}