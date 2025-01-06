using System;
using JJ.Business.Synthesizer.Enums;
using JJ.Business.Synthesizer.Extensions;
using JJ.Business.Synthesizer.Wishes.Helpers;
using JJ.Business.Synthesizer.Wishes.Obsolete;
using JJ.Business.Synthesizer.Wishes.TapeWishes;
using JJ.Framework.Persistence;
using JJ.Framework.Reflection;
using JJ.Persistence.Synthesizer;
using JJ.Persistence.Synthesizer.DefaultRepositories.Interfaces;

namespace JJ.Business.Synthesizer.Wishes.AttributeWishes
{
    /// <inheritdoc cref="docs._attributewishes"/>
    public static partial class AttributeExtensionWishes
    {
        // A Primary Audio Attribute
        
        public static InterpolationTypeEnum Interpolation(this SynthWishes obj)
        {
            if (obj == null) throw new NullException(() => obj);
            return obj.GetInterpolation;
        }
        
        public static SynthWishes Interpolation(this SynthWishes obj, InterpolationTypeEnum value)
        {
            if (obj == null) throw new NullException(() => obj);
            return obj.WithInterpolation(value);
        }
        
        public static InterpolationTypeEnum Interpolation(this FlowNode obj)
        {
            if (obj == null) throw new NullException(() => obj);
            return obj.GetInterpolation;
        }
        
        public static FlowNode Interpolation(this FlowNode obj, InterpolationTypeEnum value)
        {
            if (obj == null) throw new NullException(() => obj);
            return obj.WithInterpolation(value);
        }
        
        public static InterpolationTypeEnum Interpolation(this ConfigWishes obj)
        {
            if (obj == null) throw new NullException(() => obj);
            return obj.GetInterpolation;
        }
        
        public static ConfigWishes Interpolation(this ConfigWishes obj, InterpolationTypeEnum value)
        {
            if (obj == null) throw new NullException(() => obj);
            return obj.WithInterpolation(value);
        }
        
        internal static InterpolationTypeEnum Interpolation(this ConfigSection obj)
        {
            if (obj == null) throw new NullException(() => obj);
            return obj.Interpolation ?? ConfigWishes.DefaultInterpolation;
        }
        
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
        
        public static InterpolationTypeEnum Interpolation(this Sample obj)
        {
            if (obj == null) throw new NullException(() => obj);
            return obj.GetInterpolationTypeEnum();
        }
        
        public static Sample Interpolation(this Sample obj, InterpolationTypeEnum value, IContext context)
        {
            if (obj == null) throw new NullException(() => obj);
            obj.SetInterpolationTypeEnum(value, context);
            return obj;
        }
        
        public static InterpolationTypeEnum Interpolation(this InterpolationTypeEnum obj) => obj;
        
        /// <inheritdoc cref="docs._quasisetter" />
        // ReSharper disable once UnusedParameter.Global
        public static InterpolationTypeEnum Interpolation(this InterpolationTypeEnum obj, InterpolationTypeEnum value) => value;
        
        [Obsolete(ObsoleteEnumWishesMessages.ObsoleteMessage)] public static InterpolationTypeEnum Interpolation(this InterpolationType obj) => ToEnum(obj);
        
        /// <inheritdoc cref="docs._quasisetter" />
        // ReSharper disable once UnusedParameter.Global
        [Obsolete(ObsoleteEnumWishesMessages.ObsoleteMessage)] public static InterpolationType Interpolation(this InterpolationType obj, InterpolationTypeEnum value, IContext context) => ToEntity(value, context);
        
        // Interpolation, Conversion-Style
        
        [Obsolete(ObsoleteEnumWishesMessages.ObsoleteMessage)] public static InterpolationTypeEnum ToEnum(this InterpolationType enumEntity)
        {
            if (enumEntity == null) throw new ArgumentNullException(nameof(enumEntity));
            return (InterpolationTypeEnum)enumEntity.ID;
        }
        
        [Obsolete(ObsoleteEnumWishesMessages.ObsoleteMessage)] public static InterpolationType ToEntity(this InterpolationTypeEnum enumValue, IContext context)
        {
            if (enumValue == default) return default;
            var repository = ServiceFactory.CreateRepository<IInterpolationTypeRepository>(context);
            return repository.Get((int)enumValue);
        }
        
        // Interpolation Shorthand
        
        public   static bool IsLinear(this SynthWishes           obj) => Interpolation(obj) == InterpolationTypeEnum.Line;
        public   static bool IsLinear(this FlowNode              obj) => Interpolation(obj) == InterpolationTypeEnum.Line;
        public   static bool IsLinear(this ConfigWishes          obj) => Interpolation(obj) == InterpolationTypeEnum.Line;
        internal static bool IsLinear(this ConfigSection         obj) => Interpolation(obj) == InterpolationTypeEnum.Line;
        public   static bool IsLinear(this TapeConfig            obj) => Interpolation(obj) == InterpolationTypeEnum.Line;
        public   static bool IsLinear(this TapeAction            obj) => Interpolation(obj) == InterpolationTypeEnum.Line;
        public   static bool IsLinear(this TapeActions           obj) => Interpolation(obj) == InterpolationTypeEnum.Line;
        public   static bool IsLinear(this Sample                obj) => Interpolation(obj) == InterpolationTypeEnum.Line;
        public   static bool IsLinear(this InterpolationTypeEnum obj) => Interpolation(obj) == InterpolationTypeEnum.Line;
        [Obsolete(ObsoleteEnumWishesMessages.ObsoleteMessage)]
        public   static bool IsLinear(this InterpolationType     obj) => Interpolation(obj) == InterpolationTypeEnum.Line;
        
        public   static bool IsBlocky(this SynthWishes           obj) => Interpolation(obj) == InterpolationTypeEnum.Block;
        public   static bool IsBlocky(this FlowNode              obj) => Interpolation(obj) == InterpolationTypeEnum.Block;
        public   static bool IsBlocky(this ConfigWishes          obj) => Interpolation(obj) == InterpolationTypeEnum.Block;
        internal static bool IsBlocky(this ConfigSection         obj) => Interpolation(obj) == InterpolationTypeEnum.Block;
        public   static bool IsBlocky(this TapeConfig            obj) => Interpolation(obj) == InterpolationTypeEnum.Block;
        public   static bool IsBlocky(this TapeAction            obj) => Interpolation(obj) == InterpolationTypeEnum.Block;
        public   static bool IsBlocky(this TapeActions           obj) => Interpolation(obj) == InterpolationTypeEnum.Block;
        public   static bool IsBlocky(this Sample                obj) => Interpolation(obj) == InterpolationTypeEnum.Block;
        public   static bool IsBlocky(this InterpolationTypeEnum obj) => Interpolation(obj) == InterpolationTypeEnum.Block;
        [Obsolete(ObsoleteEnumWishesMessages.ObsoleteMessage)]
        public   static bool IsBlocky(this InterpolationType  obj) => Interpolation(obj) == InterpolationTypeEnum.Block;
        
        public   static SynthWishes           Linear(this SynthWishes         obj) => Interpolation(obj, InterpolationTypeEnum.Line);
        public   static FlowNode              Linear(this FlowNode            obj) => Interpolation(obj, InterpolationTypeEnum.Line);
        public   static ConfigWishes          Linear(this ConfigWishes        obj) => Interpolation(obj, InterpolationTypeEnum.Line);
        public   static Tape                  Linear(this Tape                obj) => Interpolation(obj, InterpolationTypeEnum.Line);
        public   static TapeConfig            Linear(this TapeConfig          obj) => Interpolation(obj, InterpolationTypeEnum.Line);
        public   static TapeAction            Linear(this TapeAction          obj) => Interpolation(obj, InterpolationTypeEnum.Line);
        public   static TapeActions           Linear(this TapeActions         obj) => Interpolation(obj, InterpolationTypeEnum.Line);
        public   static Sample                Linear(this Sample              obj, IContext context) => Interpolation(obj, InterpolationTypeEnum.Line, context);
        /// <inheritdoc cref="docs._quisetter" />
        public   static InterpolationTypeEnum Linear(this InterpolationTypeEnum obj) => Interpolation(obj, InterpolationTypeEnum.Block);
        /// <inheritdoc cref="docs._quisetter" />
        [Obsolete(ObsoleteEnumWishesMessages.ObsoleteMessage)]
        public   static InterpolationType     Linear(this InterpolationType     obj, IContext context) => Interpolation(obj, InterpolationTypeEnum.Line, context);
        
        public   static SynthWishes           Blocky(this SynthWishes           obj) => Interpolation(obj, InterpolationTypeEnum.Block);
        public   static FlowNode              Blocky(this FlowNode              obj) => Interpolation(obj, InterpolationTypeEnum.Block);
        public   static ConfigWishes          Blocky(this ConfigWishes          obj) => Interpolation(obj, InterpolationTypeEnum.Block);
        public   static Tape                  Blocky(this Tape                  obj) => Interpolation(obj, InterpolationTypeEnum.Block);
        public   static TapeConfig            Blocky(this TapeConfig            obj) => Interpolation(obj, InterpolationTypeEnum.Block);
        public   static TapeAction            Blocky(this TapeAction            obj) => Interpolation(obj, InterpolationTypeEnum.Block);
        public   static TapeActions           Blocky(this TapeActions           obj) => Interpolation(obj, InterpolationTypeEnum.Block);
        public   static Sample                Blocky(this Sample                obj, IContext context) => Interpolation(obj, InterpolationTypeEnum.Block, context);
        /// <inheritdoc cref="docs._quisetter" />
        public   static InterpolationTypeEnum Blocky(this InterpolationTypeEnum obj) => Interpolation(obj, InterpolationTypeEnum.Block);
        /// <inheritdoc cref="docs._quisetter" />
        [Obsolete(ObsoleteEnumWishesMessages.ObsoleteMessage)]
        public   static InterpolationType     Blocky(this InterpolationType     obj, IContext context) => Interpolation(obj, InterpolationTypeEnum.Block, context);
    }
}