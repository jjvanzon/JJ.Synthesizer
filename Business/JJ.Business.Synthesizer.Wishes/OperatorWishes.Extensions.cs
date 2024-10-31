using System;
using JJ.Business.Synthesizer.EntityWrappers;
using JJ.Framework.Common;
using JJ.Framework.Reflection;
using JJ.Persistence.Synthesizer;
using static JJ.Business.Synthesizer.Wishes.docs;

// ReSharper disable NotResolvedInText

namespace JJ.Business.Synthesizer.Wishes
{
    /// <inheritdoc cref="_operatorextensionwishes"/>
    public static class OperatorExtensionsWishes
    {
        // Missing
        
        public static Operator Operator(this SampleOperatorWrapper wrapper)
        {
            if (wrapper == null) throw new ArgumentNullException(nameof(wrapper));
            if (wrapper.Result == null) throw new NullException(() => wrapper.Result);
            return wrapper.Result.Operator;
        }
        
        public static Operator Operator(this CurveInWrapper wrapper)
        {
            if (wrapper == null) throw new ArgumentNullException(nameof(wrapper));
            if (wrapper.Result == null) throw new NullException(() => wrapper.Result);
            return wrapper.Result.Operator;
        }

        // String

        /// <inheritdoc cref="_stringify"/>
        public static string Stringify(this Outlet entity, bool singleLine = false)
            => new OperatorStringifier().StringifyRecursive(entity, singleLine);

        /// <inheritdoc cref="_stringify"/>
        public static string Stringify(this Operator entity, bool singleLine = false)
            => new OperatorStringifier().StringifyRecursive(entity, singleLine);

        /// <inheritdoc cref="_stringify"/>
        public static string Stringify(this Inlet entity, bool singleLine = false)
            => new OperatorStringifier().StringifyRecursive(entity, singleLine);

        /// <inheritdoc cref="_stringify"/>
        public static string Stringify(this OperatorWrapperBase wrapper, bool singleLine = false)
        {
            if (wrapper == null) throw new ArgumentNullException(nameof(wrapper));
            return new OperatorStringifier().StringifyRecursive(wrapper.Operator, singleLine);
        }

        /// <inheritdoc cref="_stringify"/>
        public static string Stringify(this SampleOperatorWrapper wrapper, bool singleLine = false)
        {
            if (wrapper == null) throw new ArgumentNullException(nameof(wrapper));
            return new OperatorStringifier().StringifyRecursive(wrapper.Result, singleLine);
        }

        /// <inheritdoc cref="_stringify"/>
        public static string Stringify(this CurveInWrapper wrapper, bool singleLine = false)
        {
            if (wrapper == null) throw new ArgumentNullException(nameof(wrapper));
            return new OperatorStringifier().StringifyRecursive(wrapper.Result, singleLine);
        }
    }
}