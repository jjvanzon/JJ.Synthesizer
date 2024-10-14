//using System;
//using System.Collections.Generic;
//using System.Linq;
//using JetBrains.Annotations;
//using JJ.Business.CanonicalModel;
//using JJ.Framework.Common;
//using JJ.Framework.Reflection;
//using JJ.Framework.Validation;
//// ReSharper disable CheckNamespace

//namespace JJ.Business.CanonicalModel
//{
//    [PublicAPI]
//    public static class ValidationToCanonicalExtensions
//    {
//        public static Result ToCanonical(this IValidator validator)
//        {
//            if (validator == null) throw new NullException(() => validator);

//            var result = new Result
//            {
//                Successful = validator.IsValid,
//                ValidationMessages = validator.ValidationMessages
//                                              .Select(x => new ValidationMessage
//                                              {
//                                                  PropertyKey = x.PropertyKey,
//                                                  Text        = x.Text
//                                              })
//                                              .ToList()
//            };

//            return result;
//        }

//        /// <summary>
//        /// Mind that destResult.Successful should be set to true,
//        /// if it is ever te be set to true.
//        /// </summary>
//        public static void ToCanonical(this IEnumerable<IValidator> sourceValidators, Result destResult)
//        {
//            // ReSharper disable once JoinNullCheckWithUsage
//            if (sourceValidators == null) throw new NullException(() => sourceValidators);
//            if (destResult == null) throw new ArgumentNullException(nameof(destResult));

//            // Prevent multiple enumeration.
//            sourceValidators = sourceValidators.ToArray();

//            destResult.Successful &= sourceValidators.All(x => x.IsValid);

//            destResult.ValidationMessages = destResult.ValidationMessages ?? new List<ValidationMessage>();

//            destResult.ValidationMessages.AddRange(sourceValidators.SelectMany(x => x.ValidationMessages).Select(x=> x.));
//        }

//        public static Result ToCanonical(this IEnumerable<IValidator> validators)
//        {
//            var result = new Result { Successful = true, ValidationMessages = new List<ValidationMessage>() };

//            ToCanonical(validators, result);

//            return result;
//        }
//    }
//}