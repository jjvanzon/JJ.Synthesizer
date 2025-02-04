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
using static JJ.Business.Synthesizer.Enums.InterpolationTypeEnum;
using static JJ.Business.Synthesizer.Wishes.Config.ConfigWishes;
using static JJ.Business.Synthesizer.Wishes.Helpers.ServiceFactory;
using static JJ.Business.Synthesizer.Wishes.Obsolete.ObsoleteEnumWishesMessages;

// ReSharper disable UnusedParameter.Global

namespace JJ.Business.Synthesizer.Wishes.Config
{
    // Interpolation: A Primary Audio Attribute
        
    /// <inheritdoc cref="docs._configextensionwishes"/>
    public static class InterpolationExtensionWishes
    {
        // Synth-Bound
        
        public static bool IsLinear(this SynthWishes obj) => GetInterpolation(obj) == Line;
        public static bool IsBlocky(this SynthWishes obj) => GetInterpolation(obj) == Block;
        public static InterpolationTypeEnum Interpolation(this SynthWishes obj) => GetInterpolation(obj);
        public static InterpolationTypeEnum GetInterpolation(this SynthWishes obj)
        {
            if (obj == null) throw new NullException(() => obj);
            return obj.GetInterpolation;
        }
        
        public static SynthWishes Linear(this SynthWishes obj) => SetInterpolation(obj, Line);
        public static SynthWishes Blocky(this SynthWishes obj) => SetInterpolation(obj, Block);
        public static SynthWishes Interpolation(this SynthWishes obj, InterpolationTypeEnum? value) => SetInterpolation(obj, value);
        public static SynthWishes WithInterpolation(this SynthWishes obj, InterpolationTypeEnum? value) => SetInterpolation(obj, value);
        public static SynthWishes SetInterpolation(this SynthWishes obj, InterpolationTypeEnum? value)
        {
            if (obj == null) throw new NullException(() => obj);
            return obj.WithInterpolation(value);
        }
        
        public static bool IsLinear(this FlowNode obj) => GetInterpolation(obj) == Line;
        public static bool IsBlocky(this FlowNode obj) => GetInterpolation(obj) == Block;
        public static InterpolationTypeEnum Interpolation(this FlowNode obj) => GetInterpolation(obj);
        public static InterpolationTypeEnum GetInterpolation(this FlowNode obj)
        {
            if (obj == null) throw new NullException(() => obj);
            return obj.GetInterpolation;
        }
        
        public static FlowNode Linear(this FlowNode obj) => SetInterpolation(obj, Line);
        public static FlowNode Blocky(this FlowNode obj) => SetInterpolation(obj, Block);
        public static FlowNode Interpolation(this FlowNode obj, InterpolationTypeEnum? value) => SetInterpolation(obj, value);
        public static FlowNode WithInterpolation(this FlowNode obj, InterpolationTypeEnum? value) => SetInterpolation(obj, value);
        public static FlowNode SetInterpolation(this FlowNode obj, InterpolationTypeEnum? value)
        {
            if (obj == null) throw new NullException(() => obj);
            return obj.WithInterpolation(value);
        }

        [UsedImplicitly] internal static bool IsLinear(this ConfigResolver obj) => GetInterpolation(obj) == Line;
        [UsedImplicitly] internal static bool IsBlocky(this ConfigResolver obj) => GetInterpolation(obj) == Block;
        [UsedImplicitly] internal static InterpolationTypeEnum Interpolation(this ConfigResolver obj) => GetInterpolation(obj);
        [UsedImplicitly] internal static InterpolationTypeEnum GetInterpolation(this ConfigResolver obj)
        {
            if (obj == null) throw new NullException(() => obj);
            return obj.GetInterpolation;
        }
        
        [UsedImplicitly] internal static ConfigResolver Linear(this ConfigResolver obj) => SetInterpolation(obj, Line);
        [UsedImplicitly] internal static ConfigResolver Blocky(this ConfigResolver obj) => SetInterpolation(obj, Block);
        [UsedImplicitly] internal static ConfigResolver Interpolation(this ConfigResolver obj, InterpolationTypeEnum? value) => SetInterpolation(obj, value);
        [UsedImplicitly] internal static ConfigResolver WithInterpolation(this ConfigResolver obj, InterpolationTypeEnum? value) => SetInterpolation(obj, value);
        [UsedImplicitly] internal static ConfigResolver SetInterpolation(this ConfigResolver obj, InterpolationTypeEnum? value)
        {
            if (obj == null) throw new NullException(() => obj);
            return obj.WithInterpolation(value);
        }

        // Global-Bound

        [UsedImplicitly] internal static bool IsLinear(this ConfigSection obj) => GetInterpolation(obj) == Line;
        [UsedImplicitly] internal static bool IsBlocky(this ConfigSection obj) => GetInterpolation(obj) == Block;
        [UsedImplicitly] internal static InterpolationTypeEnum? Interpolation(this ConfigSection obj) => GetInterpolation(obj);
        [UsedImplicitly] internal static InterpolationTypeEnum? GetInterpolation(this ConfigSection obj)
        {
            if (obj == null) throw new NullException(() => obj);
            return obj.Interpolation;
        }
        
        // Tape-Bound
        
        public static bool IsLinear(this Tape obj) => GetInterpolation(obj) == Line;
        public static bool IsBlocky(this Tape obj) => GetInterpolation(obj) == Block;
        public static InterpolationTypeEnum Interpolation(this Tape obj) => GetInterpolation(obj);
        public static InterpolationTypeEnum GetInterpolation(this Tape obj)
        {
            if (obj == null) throw new NullException(() => obj);
            return obj.Config.Interpolation;
        }
        
        public static Tape Linear(this Tape obj) => SetInterpolation(obj, Line);
        public static Tape Blocky(this Tape obj) => SetInterpolation(obj, Block);
        public static Tape Interpolation(this Tape obj, InterpolationTypeEnum value) => SetInterpolation(obj, value);
        public static Tape WithInterpolation(this Tape obj, InterpolationTypeEnum value) => SetInterpolation(obj, value);
        public static Tape SetInterpolation(this Tape obj, InterpolationTypeEnum value)
        {
            if (obj == null) throw new NullException(() => obj);
            obj.Config.Interpolation = value;
            return obj;
        }
        
        public static bool IsLinear(this TapeConfig obj) => GetInterpolation(obj) == Line;
        public static bool IsBlocky(this TapeConfig obj) => GetInterpolation(obj) == Block;
        public static InterpolationTypeEnum Interpolation(this TapeConfig obj) => GetInterpolation(obj);
        public static InterpolationTypeEnum GetInterpolation(this TapeConfig obj)
        {
            if (obj == null) throw new NullException(() => obj);
            return obj.Interpolation;
        }
        
        public static TapeConfig Linear(this TapeConfig obj) => SetInterpolation(obj, Line);
        public static TapeConfig Blocky(this TapeConfig obj) => SetInterpolation(obj, Block);
        public static TapeConfig Interpolation(this TapeConfig obj, InterpolationTypeEnum value) => SetInterpolation(obj, value);
        public static TapeConfig WithInterpolation(this TapeConfig obj, InterpolationTypeEnum value) => SetInterpolation(obj, value);
        public static TapeConfig SetInterpolation(this TapeConfig obj, InterpolationTypeEnum value)
        {
            if (obj == null) throw new NullException(() => obj);
            obj.Interpolation = value;
            return obj;
        }
        
        public static bool IsLinear(this TapeAction obj) => GetInterpolation(obj) == Line;
        public static bool IsBlocky(this TapeAction obj) => GetInterpolation(obj) == Block;
        public static InterpolationTypeEnum Interpolation(this TapeAction obj) => GetInterpolation(obj);
        public static InterpolationTypeEnum GetInterpolation(this TapeAction obj)
        {
            if (obj == null) throw new NullException(() => obj);
            return obj.Tape.Config.Interpolation;
        }
        
        public static TapeAction Linear(this TapeAction obj) => SetInterpolation(obj, Line);
        public static TapeAction Blocky(this TapeAction obj) => SetInterpolation(obj, Block);
        public static TapeAction Interpolation(this TapeAction obj, InterpolationTypeEnum value) => SetInterpolation(obj, value);
        public static TapeAction WithInterpolation(this TapeAction obj, InterpolationTypeEnum value) => SetInterpolation(obj, value);
        public static TapeAction SetInterpolation(this TapeAction obj, InterpolationTypeEnum value)
        {
            if (obj == null) throw new NullException(() => obj);
            obj.Tape.Config.Interpolation = value;
            return obj;
        }
        
        public static bool IsLinear(this TapeActions obj) => GetInterpolation(obj) == Line;
        public static bool IsBlocky(this TapeActions obj) => GetInterpolation(obj) == Block;
        public static InterpolationTypeEnum Interpolation(this TapeActions obj) => GetInterpolation(obj);
        public static InterpolationTypeEnum GetInterpolation(this TapeActions obj)
        {
            if (obj == null) throw new NullException(() => obj);
            return obj.Tape.Config.Interpolation;
        }
        
        public static TapeActions Linear(this TapeActions obj) => SetInterpolation(obj, Line);
        public static TapeActions Blocky(this TapeActions obj) => SetInterpolation(obj, Block);
        public static TapeActions Interpolation(this TapeActions obj, InterpolationTypeEnum value) => SetInterpolation(obj, value);
        public static TapeActions WithInterpolation(this TapeActions obj, InterpolationTypeEnum value) => SetInterpolation(obj, value);
        public static TapeActions SetInterpolation(this TapeActions obj, InterpolationTypeEnum value)
        {
            if (obj == null) throw new NullException(() => obj);
            obj.Tape.Config.Interpolation = value;
            return obj;
        }
        
        // Independent after Taping
        
        public static bool IsLinear(this Sample obj) => GetInterpolation(obj) == Line;
        public static bool IsBlocky(this Sample obj) => GetInterpolation(obj) == Block;
        public static InterpolationTypeEnum Interpolation(this Sample obj) => GetInterpolation(obj);
        public static InterpolationTypeEnum GetInterpolation(this Sample obj)
        {
            return obj.GetInterpolationTypeEnum();
        }
        
        public static Sample Linear(this Sample obj, IContext context) => SetInterpolation(obj, Line, context);
        public static Sample Blocky(this Sample obj, IContext context) => SetInterpolation(obj, Block, context);
        public static Sample Interpolation(this Sample obj, InterpolationTypeEnum value, IContext context) => SetInterpolation(obj, value, context);
        public static Sample WithInterpolation(this Sample obj, InterpolationTypeEnum value, IContext context) => SetInterpolation(obj, value, context);
        public static Sample SetInterpolation(this Sample obj, InterpolationTypeEnum value, IContext context)
        {
            if (obj == null) throw new NullException(() => obj);
            obj.SetInterpolationTypeEnum(value, context);
            return obj;
        }
        
        // Immutable
        
        public static bool IsLinear(this InterpolationTypeEnum obj) => GetInterpolation(obj) == Line;
        public static bool IsBlocky(this InterpolationTypeEnum obj) => GetInterpolation(obj) == Block;
        public static InterpolationTypeEnum Interpolation(this InterpolationTypeEnum obj) => GetInterpolation(obj);
        public static InterpolationTypeEnum GetInterpolation(this InterpolationTypeEnum obj)
        {
            return AssertInterpolation(obj);
        }
        
        /// <inheritdoc cref="docs._quasisetter" />
        public static InterpolationTypeEnum Linear(this InterpolationTypeEnum oldInterpolation)
            => SetInterpolation(oldInterpolation, Line);
        /// <inheritdoc cref="docs._quasisetter" />
        public static InterpolationTypeEnum Blocky(this InterpolationTypeEnum oldInterpolation)
            => SetInterpolation(oldInterpolation, Block);
        /// <inheritdoc cref="docs._quasisetter" />
        public static InterpolationTypeEnum Interpolation(this InterpolationTypeEnum oldEnumValue, InterpolationTypeEnum newEnumValue)
            => SetInterpolation(oldEnumValue, newEnumValue);
        /// <inheritdoc cref="docs._quasisetter" />
        public static InterpolationTypeEnum WithInterpolation(this InterpolationTypeEnum oldEnumValue, InterpolationTypeEnum newEnumValue)
            => SetInterpolation(oldEnumValue, newEnumValue);
        /// <inheritdoc cref="docs._quasisetter" />
        public static InterpolationTypeEnum AsInterpolation(this InterpolationTypeEnum oldEnumValue, InterpolationTypeEnum newEnumValue)
            => SetInterpolation(oldEnumValue, newEnumValue);
        /// <inheritdoc cref="docs._quasisetter" />
        public static InterpolationTypeEnum ToInterpolation(this InterpolationTypeEnum oldEnumValue, InterpolationTypeEnum newEnumValue)
            => SetInterpolation(oldEnumValue, newEnumValue);
        /// <inheritdoc cref="docs._quasisetter" />
        public static InterpolationTypeEnum SetInterpolation(this InterpolationTypeEnum oldEnumValue, InterpolationTypeEnum newEnumValue)
        {
            return AssertInterpolation(newEnumValue);
        }

        [Obsolete(ObsoleteMessage)] public static bool IsLinear(this InterpolationType obj) => GetInterpolation(obj) == Line;
        [Obsolete(ObsoleteMessage)] public static bool IsBlocky(this InterpolationType obj) => GetInterpolation(obj) == Block;
        [Obsolete(ObsoleteMessage)] public static InterpolationTypeEnum Interpolation(this InterpolationType obj) => GetInterpolation(obj);
        [Obsolete(ObsoleteMessage)] public static InterpolationTypeEnum GetInterpolation(this InterpolationType obj)
        {
            return obj.ToEnum();
        }
        
        /// <inheritdoc cref="docs._quasisetter" />
        [Obsolete(ObsoleteMessage)]
        public static InterpolationType Linear(this InterpolationType oldEnumEntity, IContext context)
            => SetInterpolation(oldEnumEntity, Line, context);
        /// <inheritdoc cref="docs._quasisetter" />
        [Obsolete(ObsoleteMessage)]
        public static InterpolationType Blocky(this InterpolationType oldEnumEntity, IContext context)
            => SetInterpolation(oldEnumEntity, Block, context);
        /// <inheritdoc cref="docs._quasisetter" />
        [Obsolete(ObsoleteMessage)] 
        public static InterpolationType Interpolation(this InterpolationType oldEntity, InterpolationTypeEnum newEnumValue, IContext context)
            => SetInterpolation(oldEntity, newEnumValue, context);
        /// <inheritdoc cref="docs._quasisetter" />
        [Obsolete(ObsoleteMessage)] 
        public static InterpolationType WithInterpolation(this InterpolationType oldEntity, InterpolationTypeEnum newEnumValue, IContext context)
            => SetInterpolation(oldEntity, newEnumValue, context);
        /// <inheritdoc cref="docs._quasisetter" />
        [Obsolete(ObsoleteMessage)] 
        public static InterpolationType AsInterpolation(this InterpolationType oldEntity, InterpolationTypeEnum newEnumValue, IContext context)
            => SetInterpolation(oldEntity, newEnumValue, context);
        /// <inheritdoc cref="docs._quasisetter" />
        [Obsolete(ObsoleteMessage)] 
        public static InterpolationType ToInterpolation(this InterpolationType oldEntity, InterpolationTypeEnum newEnumValue, IContext context)
            => SetInterpolation(oldEntity, newEnumValue, context);
        /// <inheritdoc cref="docs._quasisetter" />
        [Obsolete(ObsoleteMessage)] 
        public static InterpolationType SetInterpolation(this InterpolationType oldEntity, InterpolationTypeEnum newEnumValue, IContext context)
            => newEnumValue.ToEntity(context);

         // Conversion-Style

        [Obsolete(ObsoleteMessage)] 
        public static InterpolationTypeEnum ToEnum(this InterpolationType enumEntity)
            => ConfigWishes.ToEnum(enumEntity);

        [Obsolete(ObsoleteMessage)] 
        public static InterpolationType ToEntity(this InterpolationTypeEnum enumValue, IContext context)
            => ConfigWishes.ToEntity(enumValue, context);
    }
    
    public partial class ConfigWishes
    {
        // Conversion-Style
        
        [Obsolete(ObsoleteMessage)] 
        public static InterpolationTypeEnum ToEnum(InterpolationType enumEntity)
        {
            if (enumEntity == null) throw new NullException(() => enumEntity);
            return (InterpolationTypeEnum)enumEntity.ID;
        }
        
        [Obsolete(ObsoleteMessage)] 
        public static InterpolationType ToEntity(InterpolationTypeEnum enumValue, IContext context)
        {
            if (enumValue == default) return default;
            AssertInterpolation(enumValue);
            var repository = CreateRepository<IInterpolationTypeRepository>(context);
            return repository.Get((int)enumValue);
        }
    }
}