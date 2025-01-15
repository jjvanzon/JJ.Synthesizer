using System;
using JetBrains.Annotations;
using JJ.Business.Synthesizer.Enums;
using JJ.Business.Synthesizer.Extensions;
using JJ.Business.Synthesizer.Wishes.TapeWishes;
using JJ.Framework.Persistence;
using JJ.Framework.Reflection;
using JJ.Persistence.Synthesizer;
using JJ.Persistence.Synthesizer.DefaultRepositories.Interfaces;
using static JJ.Business.Synthesizer.Enums.InterpolationTypeEnum;
using static JJ.Business.Synthesizer.Wishes.Configuration.ConfigWishes;
using static JJ.Business.Synthesizer.Wishes.Helpers.ServiceFactory;
using static JJ.Business.Synthesizer.Wishes.Obsolete.ObsoleteEnumWishesMessages;

// ReSharper disable UnusedParameter.Global

namespace JJ.Business.Synthesizer.Wishes.Configuration
{
    /// <inheritdoc cref="docs._configextensionwishes"/>
    public static class InterpolationExtensionWishes
    {
        // A Primary Audio Attribute
        
        
        // Synth-Bound
        
        public static InterpolationTypeEnum Interpolation(this SynthWishes obj)
        {
            if (obj == null) throw new NullException(() => obj);
            return obj.GetInterpolation;
        }
        
        public static SynthWishes Interpolation(this SynthWishes obj, InterpolationTypeEnum? value)
        {
            if (obj == null) throw new NullException(() => obj);
            return obj.WithInterpolation(value);
        }
        
        public static InterpolationTypeEnum Interpolation(this FlowNode obj)
        {
            if (obj == null) throw new NullException(() => obj);
            return obj.GetInterpolation;
        }
        
        public static FlowNode Interpolation(this FlowNode obj, InterpolationTypeEnum? value)
        {
            if (obj == null) throw new NullException(() => obj);
            return obj.WithInterpolation(value);
        }
        
        internal static InterpolationTypeEnum Interpolation(this ConfigResolver obj)
        {
            if (obj == null) throw new NullException(() => obj);
            return obj.GetInterpolation;
        }
        
        internal static ConfigResolver Interpolation(this ConfigResolver obj, InterpolationTypeEnum? value)
        {
            if (obj == null) throw new NullException(() => obj);
            return obj.WithInterpolation(value);
        }
        
        // Global-Bound
        
        internal static InterpolationTypeEnum? Interpolation(this ConfigSection obj)
        {
            if (obj == null) throw new NullException(() => obj);
            return obj.Interpolation;
        }
        
        // Tape-Bound
        
        public static InterpolationTypeEnum Interpolation(this Tape obj)
        {
            if (obj == null) throw new NullException(() => obj);
            return obj.Config.Interpolation;
        }
        
        public static Tape Interpolation(this Tape obj, InterpolationTypeEnum value)
        {
            if (obj == null) throw new NullException(() => obj);
            obj.Config.Interpolation = value;
            return obj;
        }
        
        public static InterpolationTypeEnum Interpolation(this TapeConfig obj)
        {
            if (obj == null) throw new NullException(() => obj);
            return obj.Interpolation;
        }
        
        public static TapeConfig Interpolation(this TapeConfig obj, InterpolationTypeEnum value)
        {
            if (obj == null) throw new NullException(() => obj);
            obj.Interpolation = value;
            return obj;
        }
        
        public static InterpolationTypeEnum Interpolation(this TapeAction obj)
        {
            if (obj == null) throw new NullException(() => obj);
            return obj.Tape.Config.Interpolation;
        }
        
        public static TapeAction Interpolation(this TapeAction obj, InterpolationTypeEnum value)
        {
            if (obj == null) throw new NullException(() => obj);
            obj.Tape.Config.Interpolation = value;
            return obj;
        }
        
        public static InterpolationTypeEnum Interpolation(this TapeActions obj)
        {
            if (obj == null) throw new NullException(() => obj);
            return obj.Tape.Config.Interpolation;
        }
        
        public static TapeActions Interpolation(this TapeActions obj, InterpolationTypeEnum value)
        {
            if (obj == null) throw new NullException(() => obj);
            obj.Tape.Config.Interpolation = value;
            return obj;
        }
        
        // Independent after Taping
        
        public static InterpolationTypeEnum Interpolation(this Sample obj) 
            => obj.GetInterpolationTypeEnum();
        
        public static Sample Interpolation(this Sample obj, InterpolationTypeEnum value, IContext context)
        {
            if (obj == null) throw new NullException(() => obj);
            obj.SetInterpolationTypeEnum(value, context);
            return obj;
        }
        
        // Immutable
        
        public static InterpolationTypeEnum Interpolation(this InterpolationTypeEnum obj) => Assert(obj);
        
        /// <inheritdoc cref="docs._quasisetter" />
        public static InterpolationTypeEnum Interpolation(this InterpolationTypeEnum oldEnumValue, InterpolationTypeEnum newEnumValue) 
            => Assert(newEnumValue);
        
        [Obsolete(ObsoleteMessage)] 
        public static InterpolationTypeEnum Interpolation(this InterpolationType obj) => obj.ToEnum();
        
        /// <inheritdoc cref="docs._quasisetter" />
        [Obsolete(ObsoleteMessage)] // ReSharper disable once UnusedParameter.Global
        public static InterpolationType Interpolation(this InterpolationType oldInterpolationEntity, InterpolationTypeEnum newEnumValue, IContext context)
            => newEnumValue.ToEntity(context);
        
        // Shorthand
        
        public   static bool IsLinear(this SynthWishes           obj) => obj.Interpolation() == Line;
        public   static bool IsLinear(this FlowNode              obj) => obj.Interpolation() == Line;
        [UsedImplicitly]
        internal static bool IsLinear(this ConfigResolver        obj) => obj.Interpolation() == Line;
        [UsedImplicitly]
        internal static bool IsLinear(this ConfigSection         obj) => obj.Interpolation() == Line;
        public   static bool IsLinear(this Tape                  obj) => obj.Interpolation() == Line;
        public   static bool IsLinear(this TapeConfig            obj) => obj.Interpolation() == Line;
        public   static bool IsLinear(this TapeAction            obj) => obj.Interpolation() == Line;
        public   static bool IsLinear(this TapeActions           obj) => obj.Interpolation() == Line;
        public   static bool IsLinear(this Sample                obj) => obj.Interpolation() == Line;
        public   static bool IsLinear(this InterpolationTypeEnum obj) => obj.Interpolation() == Line;
        [Obsolete(ObsoleteMessage)]
        public   static bool IsLinear(this InterpolationType     obj) => obj.Interpolation() == Line;
        
        public   static bool IsBlocky(this SynthWishes           obj) => obj.Interpolation() == Block;
        public   static bool IsBlocky(this FlowNode              obj) => obj.Interpolation() == Block;
        [UsedImplicitly]
        internal static bool IsBlocky(this ConfigResolver        obj) => obj.Interpolation() == Block;
        [UsedImplicitly]
        internal static bool IsBlocky(this ConfigSection         obj) => obj.Interpolation() == Block;
        public   static bool IsBlocky(this Tape                  obj) => obj.Interpolation() == Block;
        public   static bool IsBlocky(this TapeConfig            obj) => obj.Interpolation() == Block;
        public   static bool IsBlocky(this TapeAction            obj) => obj.Interpolation() == Block;
        public   static bool IsBlocky(this TapeActions           obj) => obj.Interpolation() == Block;
        public   static bool IsBlocky(this Sample                obj) => obj.Interpolation() == Block;
        public   static bool IsBlocky(this InterpolationTypeEnum obj) => obj.Interpolation() == Block;
        [Obsolete(ObsoleteMessage)]
        public   static bool IsBlocky(this InterpolationType     obj) => obj.Interpolation() == Block;
        
        public   static SynthWishes    Linear(this SynthWishes    obj) => obj.Interpolation(Line);
        public   static FlowNode       Linear(this FlowNode       obj) => obj.Interpolation(Line);
        [UsedImplicitly]
        internal static ConfigResolver Linear(this ConfigResolver obj) => obj.Interpolation(Line);
        public   static Tape           Linear(this Tape           obj) => obj.Interpolation(Line);
        public   static TapeConfig     Linear(this TapeConfig     obj) => obj.Interpolation(Line);
        public   static TapeAction     Linear(this TapeAction     obj) => obj.Interpolation(Line);
        public   static TapeActions    Linear(this TapeActions    obj) => obj.Interpolation(Line);
        public   static Sample         Linear(this Sample         obj, IContext context) => obj.Interpolation(Line, context);
        
        /// <inheritdoc cref="docs._quasisetter" />
        public   static InterpolationTypeEnum Linear(this InterpolationTypeEnum oldInterpolation) 
            => oldInterpolation.Interpolation(Line);
        
        /// <inheritdoc cref="docs._quasisetter" />
        [Obsolete(ObsoleteMessage)]
        public   static InterpolationType Linear(this InterpolationType oldEnumEntity, IContext context) 
            => oldEnumEntity.Interpolation(Line, context);
        
        public   static SynthWishes    Blocky(this SynthWishes    obj) => obj.Interpolation(Block);
        public   static FlowNode       Blocky(this FlowNode       obj) => obj.Interpolation(Block);
        [UsedImplicitly]
        internal static ConfigResolver Blocky(this ConfigResolver obj) => obj.Interpolation(Block);
        public   static Tape           Blocky(this Tape           obj) => obj.Interpolation(Block);
        public   static TapeConfig     Blocky(this TapeConfig     obj) => obj.Interpolation(Block);
        public   static TapeAction     Blocky(this TapeAction     obj) => obj.Interpolation(Block);
        public   static TapeActions    Blocky(this TapeActions    obj) => obj.Interpolation(Block);
        public   static Sample         Blocky(this Sample         obj, IContext context) => obj.Interpolation(Block, context);
        
        /// <inheritdoc cref="docs._quasisetter" />
        public   static InterpolationTypeEnum Blocky(this InterpolationTypeEnum oldInterpolation) 
            => Interpolation(oldInterpolation, Block);
        
        /// <inheritdoc cref="docs._quasisetter" />
        [Obsolete(ObsoleteMessage)]
        public   static InterpolationType Blocky(this InterpolationType oldEnumEntity, IContext context) 
            => oldEnumEntity.Interpolation(Block, context);
         
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
            Assert(enumValue);
            var repository = CreateRepository<IInterpolationTypeRepository>(context);
            return repository.Get((int)enumValue);
        }
    }
}