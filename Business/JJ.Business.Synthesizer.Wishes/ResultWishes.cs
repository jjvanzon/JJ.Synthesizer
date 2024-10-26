using System;
using System.Collections.Generic;
using System.Linq;
using JJ.Business.Synthesizer.Extensions;

// ReSharper disable RedundantNameQualifier

namespace JJ.Business.Synthesizer.Wishes
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
        
        public static void Assert(this CanonicalModel.Result result)
        {
            if (result == null) throw new ArgumentNullException(nameof(result));
            Assert(result.ValidationMessages);
        }

        public static void Assert(this IList<CanonicalModel.ValidationMessage> validationMessages)
        {
            if (validationMessages == null) throw new ArgumentNullException(nameof(validationMessages));
            Assert(validationMessages.Select(x => x.Text).ToArray());
        }

        public static void Assert(this IList<string> messages)
        {
            if (messages == null) throw new ArgumentNullException(nameof(messages));

            if (messages.Count != 0)
            {
                string formattedMessages = string.Join(Environment.NewLine, messages);
                throw new Exception(formattedMessages);
            }
        }
        
        // Combine

        public static CanonicalModel.Result<Data> Combine<Data>(
            this CanonicalModel.Result<Data> result1,
            CanonicalModel.Result result2)
        {
            if (result1 == null) throw new ArgumentNullException(nameof(result1));
            if (result2 == null) throw new ArgumentNullException(nameof(result2));

            var result = new CanonicalModel.Result<Data>
            {
                Successful = result1.Successful && result2.Successful,
                ValidationMessages = result1.ValidationMessages.Concat(result2.ValidationMessages).ToList(),
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