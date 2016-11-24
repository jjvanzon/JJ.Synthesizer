using JJ.Business.Synthesizer.Resources;
using JJ.Framework.Reflection.Exceptions;
using JJ.Framework.Validation;
using System;
using System.Collections.Generic;
using System.Linq;
using JJ.Framework.Common;
using JJ.Business.Synthesizer.Configuration;
using JJ.Business.Synthesizer.Helpers;

namespace JJ.Business.Synthesizer.Validation.DataProperty
{
    /// <summary> Validates the inlet and outlet ListIndexes and that the inlet names are NOT filled in. </summary>
    internal class DataPropertyValidator : VersatileValidator_WithoutConstructorArgumentNullCheck<string>
    {
        private readonly static int? _dataMaxLength = GetDataMaxLength();

        /// <summary> HashSet for unicity and value comparisons. </summary>
        private readonly HashSet<string> _expectedDataKeysHashSet;

        public DataPropertyValidator(string data, IList<string> expectedDataKeys)
            : base(data, postponeExecute: true)
        {
            if (expectedDataKeys == null) throw new NullException(() => expectedDataKeys);

            int uniqueExpectedDataPropertyKeyCount = expectedDataKeys.Distinct().Count();
            if (uniqueExpectedDataPropertyKeyCount != expectedDataKeys.Count)
            {
                throw new NotUniqueException(() => expectedDataKeys);
            }

            _expectedDataKeysHashSet = expectedDataKeys.ToHashSet();

            Execute();
        }

        protected override void Execute()
        {
            string data = Object;

            // Check length
            if (_dataMaxLength.HasValue)
            {
                For(() => data, PropertyDisplayNames.Data).MaxLength(_dataMaxLength.Value);
            }

            // Check well-formedness
            if (!DataPropertyParser.DataIsWellFormed(Object))
            {
                ValidationMessages.AddIsInvalidMessage(() => data, PropertyDisplayNames.Data);
                return;
            }

            IList<string> actualDataKeysList = DataPropertyParser.GetKeys(data); // List, not HashSet, so we can do a unicity check.

            // Check unicity
            int uniqueActualDataKeyCount = actualDataKeysList.Distinct().Count();
            if (uniqueActualDataKeyCount != actualDataKeysList.Count)
            {
                ValidationMessages.AddNotUniqueMessagePlural(PropertyNames.DataKeys, PropertyDisplayNames.DataKeys);
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
                        PropertyDisplayNames.DataKey,
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
                    ValidationMessages.AddNotExistsMessage(PropertyNames.DataKey, PropertyDisplayNames.DataKey, expectedDataKey);
                }
            }
        }

        private static int? GetDataMaxLength()
        {
            return ConfigurationHelper.GetSection<ConfigurationSection>().OperatorDataMaxLength;
        }
    }
}