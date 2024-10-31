using System;
using System.Collections.Generic;
using System.Linq;
using JJ.Business.CanonicalModel;
using JJ.Business.Synthesizer.EntityWrappers;
using JJ.Business.Synthesizer.Validation;
using JJ.Business.Synthesizer.Validation.Entities;
using JJ.Business.Synthesizer.Warnings;
using JJ.Business.Synthesizer.Warnings.Entities;
using JJ.Framework.Validation;
using JJ.Persistence.Synthesizer;

namespace JJ.Business.Synthesizer.Wishes
{
    // Validation
    
    public partial class FluentOutlet
    {

        public Result Validate(bool recursive = true) => _thisOutlet.Validate(recursive);
        public void Assert(bool recursive = true) => _thisOutlet.Assert(recursive);
        public IList<string> GetWarnings(bool recursive = true) => _thisOutlet.GetWarnings(recursive);
    }

    public static class ValidationExtensionWishes
    {
        // Validation

        public static Result Validate(this Sample sample)
            => new SampleValidator(sample).ToResult();

        public static Result Validate(this AudioFileOutput audioFileOutput)
            => new AudioFileOutputValidator(audioFileOutput).ToResult();

        public static void Assert(this Sample sample)
            => new SampleValidator(sample).Verify();

        public static void Assert(this AudioFileOutput audioFileOutput)
            => new AudioFileOutputValidator(audioFileOutput).Verify();

        public static IList<string> GetWarnings(this Sample sample)
            => new SampleWarningValidator(sample).ValidationMessages.Select(x => x.Text).ToList();

        public static IList<string> GetWarnings(this AudioFileOutput audioFileOutput)
            => new AudioFileOutputWarningValidator(audioFileOutput).ValidationMessages.Select(x => x.Text).ToList();
    
        // Validation

        public static void Assert(this Curve curve)
            => new CurveValidator(curve).Verify();

        public static void Assert(this Node node)
            => new NodeValidator(node).Verify();

        public static Result Validate(this Curve curve)
            => new CurveValidator(curve).ToResult();

        public static Result Validate(this Node node)
            => new NodeValidator(node).ToResult();

        // Validation

        public static Result Validate(this Outlet entity, bool recursive = true)
        {
            if (entity == null) throw new ArgumentNullException(nameof(entity));
            return Validate(entity.Operator, recursive);
        }

        public static Result Validate(this Operator entity, bool recursive = true)
        {
            if (recursive)
            {
                return new RecursiveOperatorValidator(entity).ToResult();
            }
            else
            {
                return new VersatileOperatorValidator(entity).ToResult();
            }
        }

        public static Result Validate(this OperatorWrapperBase wrapper, bool recursive = true)
        {
            if (wrapper == null) throw new ArgumentNullException(nameof(wrapper));
            return Validate( wrapper.Operator, recursive);
        }

        public static Result Validate(this CurveInWrapper wrapper, bool recursive = true)
        {
            if (wrapper == null) throw new ArgumentNullException(nameof(wrapper));
            return Validate(wrapper.Result, recursive);
        }

        public static Result Validate(this SampleOperatorWrapper wrapper, bool recursive = true)
        {
            if (wrapper == null) throw new ArgumentNullException(nameof(wrapper));
            return Validate(wrapper.Result, recursive);
        }

        public static void Assert(this Outlet entity, bool recursive = true)
            => Validate(entity, recursive).Assert();

        public static void Assert(this Operator entity, bool recursive = true)
            => Validate(entity, recursive).Assert();
        
        public static void Assert(this OperatorWrapperBase wrapper, bool recursive = true)
        {
            if (wrapper == null) throw new ArgumentNullException(nameof(wrapper));
            Assert( wrapper.Operator, recursive);
        }

        public static void Assert(this CurveInWrapper wrapper, bool recursive = true)
        {
            if (wrapper == null) throw new ArgumentNullException(nameof(wrapper));
            Assert(wrapper.Result, recursive);
        }

        public static void Assert(this SampleOperatorWrapper wrapper, bool recursive = true)
        {
            if (wrapper == null) throw new ArgumentNullException(nameof(wrapper));
            Assert(wrapper.Result, recursive);
        }

        public static IList<string> GetWarnings(this Operator entity, bool recursive = true)
        {
            IValidator validator;

            if (recursive)
            {
                validator = new RecursiveOperatorWarningValidator(entity);
            }
            else
            {
                validator = new VersatileOperatorWarningValidator(entity);
            }

            return validator.ValidationMessages.Select(x => x.Text).ToList();
        }

        public static IList<string> GetWarnings(this Outlet entity, bool recursive = true)
        {
            if (entity == null) throw new ArgumentNullException(nameof(entity));
            return GetWarnings(entity.Operator, recursive);
        }
                
        public static IList<string> GetWarnings(this OperatorWrapperBase wrapper, bool recursive = true)
        {
            if (wrapper == null) throw new ArgumentNullException(nameof(wrapper));
            return GetWarnings( wrapper.Operator, recursive);
        }

        public static IList<string> GetWarnings(this CurveInWrapper wrapper, bool recursive = true)
        {
            if (wrapper == null) throw new ArgumentNullException(nameof(wrapper));
            return GetWarnings(wrapper.Result, recursive);
        }

        public static IList<string> GetWarnings(this SampleOperatorWrapper wrapper, bool recursive = true)
        {
            if (wrapper == null) throw new ArgumentNullException(nameof(wrapper));
            return GetWarnings(wrapper.Result, recursive);
        }
    }
}
