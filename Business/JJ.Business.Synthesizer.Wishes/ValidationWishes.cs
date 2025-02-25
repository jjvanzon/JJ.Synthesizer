using System;
using System.Collections.Generic;
using System.Linq;
using JJ.Business.CanonicalModel;
using JJ.Business.Synthesizer.Extensions;
using JJ.Business.Synthesizer.Validation;
using JJ.Business.Synthesizer.Validation.Entities;
using JJ.Business.Synthesizer.Warnings;
using JJ.Business.Synthesizer.Warnings.Entities;
using JJ.Business.Synthesizer.Wishes.TapeWishes;
using JJ.Framework.Common;
using JJ.Framework.Reflection;
using JJ.Framework.Validation;
using JJ.Persistence.Synthesizer;
using static JJ.Business.Synthesizer.Wishes.LogWishes;

// ReSharper disable RedundantNameQualifier
// ReSharper disable CheckNamespace

namespace JJ.Business.Synthesizer.Wishes
{
    // SynthWishes Validation
    
    public partial class SynthWishes
    {
        internal static void Assert(Tape tape) => Assert(tape, default);
        
        internal static void Assert(Tape tape, string message)
        {
            if (tape == null) throw new ArgumentNullException(nameof(tape));
            tape.LogAction("Validate", message);
            tape.Outlets.ForEach(x => x.Assert());
        }
        
        internal static void Assert(FlowNode flowNode)
        {
            if (flowNode == null) throw new ArgumentNullException(nameof(flowNode));
            flowNode.LogAction("Validate");
            flowNode.Assert();
        }
    }
    
    // FlowNode Validation
    
    public partial class FlowNode
    {
        public Result Validate(bool recursive = true) => _underlyingOutlet.Validate(recursive);
        public void Assert(bool recursive = true) => _underlyingOutlet.Assert(recursive);
        public IList<string> GetWarnings(bool recursive = true) => _underlyingOutlet.GetWarnings(recursive);
    }

    public static class ValidationExtensionWishes
    {
        // AudioFileOutput Validation
        
        public static Result Validate(this AudioFileOutput audioFileOutput)
            => new AudioFileOutputValidator(audioFileOutput).ToResult();

        public static void Assert(this AudioFileOutput audioFileOutput)
            => new AudioFileOutputValidator(audioFileOutput).Verify();

        public static IList<string> GetWarnings(this AudioFileOutput audioFileOutput)
            => new AudioFileOutputWarningValidator(audioFileOutput).ValidationMessages.Select(x => x.Text).ToList();

        // Sample Validation

        public static Result Validate(this Sample sample)
            => new SampleValidator(sample).ToResult();

        public static void Assert(this Sample sample)
            => new SampleValidator(sample).Verify();

        public static IList<string> GetWarnings(this Sample sample)
            => new SampleWarningValidator(sample).ValidationMessages.Select(x => x.Text).ToList();

        // Curve Validation

        public static void Assert(this Curve curve)
            => new CurveValidator(curve).Verify();

        public static void Assert(this Node node)
            => new NodeValidator(node).Verify();

        public static Result Validate(this Curve curve)
            => new CurveValidator(curve).ToResult();

        public static Result Validate(this Node node)
            => new NodeValidator(node).ToResult();

        // Operator Validation

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

        public static void Assert(this Outlet entity, bool recursive = true)
            => Validate(entity, recursive).Assert();

        public static void Assert(this Operator entity, bool recursive = true)
            => Validate(entity, recursive).Assert();

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
    }
}

// Result Wrapping

namespace JJ.Business.CanonicalModel
{
    public static class ResultWishes
    {
        // ToResult (with Data)
        
        public static CanonicalModel.Result<TData> ToResult<TData>(
            this IList<Framework.Validation.ValidationMessage> frameworkValidationMessages, 
            TData data, bool successful = true)
        {
            if (frameworkValidationMessages == null) throw new ArgumentNullException(nameof(frameworkValidationMessages));
            var messages = frameworkValidationMessages.Select(x => x.Text).ToArray();
            return ToResult(messages, data, successful);
        }
        
        public static CanonicalModel.Result<TData> ToResult<TData>(
            this IList<string> messages, TData data, bool successful = true)
        {
            if (messages == null) throw new ArgumentNullException(nameof(messages));
            
            return new CanonicalModel.Result<TData>
            {
                Data = data,
                ValidationMessages = ToCanonical(messages),
                Successful = successful
            };
        }

        // ToResult
        
        public static CanonicalModel.Result ToResult(
            this List<Framework.Validation.ValidationMessage> frameworkValidationMessages, 
            bool successful = true)
        {
            if (frameworkValidationMessages == null) throw new ArgumentNullException(nameof(frameworkValidationMessages));
            return new CanonicalModel.Result
            {
                ValidationMessages = frameworkValidationMessages.ToCanonical(),
                Successful = successful
            };
        }

        public static CanonicalModel.Result ToResult(
            this Framework.Validation.ValidationMessages frameworkValidationMessages,
            bool successful = true)
        {
            if (frameworkValidationMessages == null) throw new ArgumentNullException(nameof(frameworkValidationMessages));
            return frameworkValidationMessages.ToList().ToResult(successful);
        }

        public static CanonicalModel.Result ToResult(
            this Framework.Validation.IValidator validator)
        {
            if (validator == null) throw new ArgumentNullException(nameof(validator));
            return validator.ValidationMessages.ToResult(validator.IsValid);
        }

        public static CanonicalModel.Result ToResult(
            this IList<string> messages, bool successful = true)
        {
            if (messages == null) throw new ArgumentNullException(nameof(messages));
            
            return new CanonicalModel.Result
            {
                ValidationMessages = ToCanonical(messages),
                Successful = successful
            };
        }

        // ToCanonical

        public static List<CanonicalModel.ValidationMessage> ToCanonical(this IList<string> messages)
        {
            if (messages == null) throw new ArgumentNullException(nameof(messages));
            return messages.Select(ToCanonical).ToList();

        }

        public static CanonicalModel.ValidationMessage ToCanonical(this string message) 
            => new CanonicalModel.ValidationMessage() { Text = message };

        // Assert
        
        public static void Assert<TData>(this CanonicalModel.Result<TData> result)
        {
            if (result == null) throw new NullException(() => result);
            if (result.Data == null) throw new NullException(() => result.Data);
            if (!result.Successful)
            {
                string formattedMessages = Format(result.ValidationMessages);
                throw new Exception(formattedMessages);
            }
        }
        
        public static void Assert(this CanonicalModel.Result result)
        {
            if (result == null) throw new ArgumentNullException(nameof(result));
            if (!result.Successful)
            {
                string formattedMessages = Format(result.ValidationMessages);
                throw new Exception(formattedMessages);
            }
        }

        public static string Format(this IList<CanonicalModel.ValidationMessage> validationMessages)
        {
            if (validationMessages == null) throw new ArgumentNullException(nameof(validationMessages));
            return Format(validationMessages.Select(x => x.Text).ToArray());
        }

        public static string Format(this IList<string> messages)
        {
            if (messages == null) throw new ArgumentNullException(nameof(messages));
            string formattedMessages = string.Join(Environment.NewLine, messages);
            return formattedMessages;
        }

        // Combine

        public static CanonicalModel.Result<Data> Combine<Data>(
            this CanonicalModel.Result<Data> result1,
            CanonicalModel.Result result2)
        {
            if (result1 == null) throw new ArgumentNullException(nameof(result1));
            if (result2 == null) throw new ArgumentNullException(nameof(result2));
            
            List<ValidationMessage> validationMessages1 = result1.ValidationMessages ?? new List<ValidationMessage>();
            List<ValidationMessage> validationMessages2 = result2.ValidationMessages ?? new List<ValidationMessage>();
            
            var result = new CanonicalModel.Result<Data>
            {
                Successful = result1.Successful && result2.Successful,
                ValidationMessages = validationMessages1.Concat(validationMessages2).ToList(),
                Data = result1.Data
            };

            return result;
        }

        public static CanonicalModel.Result<Data> Combine<Data>(
            this CanonicalModel.Result result1,
            CanonicalModel.Result result2)
        {
            if (result1 == null) throw new ArgumentNullException(nameof(result1));
            if (result2 == null) throw new ArgumentNullException(nameof(result2));

            var result = new CanonicalModel.Result<Data>
            {
                Successful = result1.Successful && result2.Successful,
                ValidationMessages = result1.ValidationMessages.Concat(result2.ValidationMessages).ToList()
            };

            return result;
        }

        public static CanonicalModel.Result<bool> Combine(
            this CanonicalModel.Result<bool> result1,
            CanonicalModel.Result<bool> result2)
        {
            if (result1 == null) throw new ArgumentNullException(nameof(result1));
            if (result2 == null) throw new ArgumentNullException(nameof(result2));

            var result = new CanonicalModel.Result<bool>
            {
                Successful = result1.Successful && result2.Successful,
                ValidationMessages = result1.ValidationMessages.Concat(result2.ValidationMessages).ToList(),
                Data = result1.Data && result2.Data
            };

            return result;
        }

    }
}
