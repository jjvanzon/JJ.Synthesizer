using JJ.Business.Synthesizer.Resources;
using JJ.Framework.Exceptions;
using JJ.Framework.Validation;
using System.Collections.Generic;
using System.Linq;
using JJ.Business.Synthesizer.Configuration;
using JJ.Business.Synthesizer.Helpers;
using JJ.Framework.Collections;
using JJ.Framework.Configuration;

namespace JJ.Business.Synthesizer.Validation.DataProperty
{
    internal class DataPropertyValidator : VersatileValidator_WithoutConstructorArgumentNullCheck<string>
    {
        private static readonly int? _dataMaxLength = CustomConfigurationManager.GetSection<ConfigurationSection>().OperatorDataMaxLength;

        /// <summary> HashSet for unicity and value comparisons. </summary>
        private readonly HashSet<string> _expectedDataKeysHashSet;

        // ReSharper disable once SuggestBaseTypeForParameter
        public DataPropertyValidator(string data, IList<string> expectedDataKeys)
            : base(data)
        {
            if (expectedDataKeys == null) throw new NullException(() => expectedDataKeys);

            int uniqueExpectedDataPropertyKeyCount = expectedDataKeys.Distinct().Count();
            if (uniqueExpectedDataPropertyKeyCount != expectedDataKeys.Count)
            {
                throw new NotUniqueException(() => expectedDataKeys);
            }

            _expectedDataKeysHashSet = expectedDataKeys.ToHashSet();

            // Check length
            if (_dataMaxLength.HasValue)
            {
                For(() => data, ResourceFormatter.Data).MaxLength(_dataMaxLength.Value);
            }

            // Check well-formedness
            if (!DataPropertyParser.DataIsWellFormed(data))
            {
                ValidationMessages.AddIsInvalidMessage(() => data, ResourceFormatter.Data);
                return;
            }

            IList<string> actualDataKeysList = DataPropertyParser.GetKeys(data); // List, not HashSet, so we can do a unicity check.

            // Check unicity
            int uniqueActualDataKeyCount = actualDataKeysList.Distinct().Count();
            if (uniqueActualDataKeyCount != actualDataKeysList.Count)
            {
                ValidationMessages.AddNotUniqueMessagePlural(PropertyNames.DataKeys, ResourceFormatter.DataKeys);
                return;
            }

            HashSet<string> actualDataKeysHashSet = actualDataKeysList.ToHashSet(); // HashSet, not List, so we can do value comparisons.

            foreach (string actualDataKey in actualDataKeysHashSet)
            {
                // Check non-existence
                bool dataKeyIsAllowed = _expectedDataKeysHashSet.Contains(actualDataKey);
                if (!dataKeyIsAllowed)
                {
                    ValidationMessages.AddNotInListMessage(
                        PropertyNames.DataKey,
                        ResourceFormatter.DataKey,
                        actualDataKey,
                        _expectedDataKeysHashSet);
                }
            }

            foreach (string expectedDataKey in _expectedDataKeysHashSet)
            {
                // Check existence
                bool dataKeyExists = actualDataKeysHashSet.Contains(expectedDataKey);
                if (!dataKeyExists)
                {
                    ValidationMessages.AddNotExistsMessage(PropertyNames.DataKey, ResourceFormatter.DataKey, expectedDataKey);
                }
            }
        }
    }
}