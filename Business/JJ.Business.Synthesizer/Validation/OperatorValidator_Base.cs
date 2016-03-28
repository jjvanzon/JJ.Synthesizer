using JJ.Business.Synthesizer.Resources;
using JJ.Framework.Presentation.Resources;
using JJ.Framework.Reflection.Exceptions;
using JJ.Framework.Validation;
using JJ.Data.Synthesizer;
using System;
using System.Collections.Generic;
using System.Linq;
using JJ.Business.Synthesizer.Enums;
using JJ.Business.Synthesizer.Extensions;
using JJ.Framework.Common;
using JJ.Business.Synthesizer.Configuration;
using JJ.Business.Synthesizer.Helpers;

namespace JJ.Business.Synthesizer.Validation
{
    /// <summary> Validates the inlet and outlet ListIndexes and that the inlet names are NOT filled in. </summary>
    public abstract class OperatorValidator_Base : FluentValidator<Operator>
    {
        private readonly static int? _dataMaxLength = GetDataMaxLength();

        private OperatorTypeEnum _expectedOperatorTypeEnum;
        private int _expectedInletCount;
        private int _expectedOutletCount;

        /// <summary> HashSet for unicity and value comparisons. </summary>
        private HashSet<string> _expectedDataKeysHashSet;

        // This overload for syntactic sugar only promotes errors.
        //public OperatorValidator_Base(
        //    Operator obj,
        //    OperatorTypeEnum expectedOperatorTypeEnum,
        //    int expectedInletCount,
        //    int expectedOutletCount,
        //    params string[] expectedDataKeys)
        //    : this(
        //          obj,
        //          expectedOperatorTypeEnum,
        //          expectedInletCount,
        //          expectedOutletCount,
        //          (IList<string>)expectedDataKeys)
        //{ }

        public OperatorValidator_Base(
            Operator obj,
            OperatorTypeEnum expectedOperatorTypeEnum,
            int expectedInletCount,
            int expectedOutletCount,
            IList<string> expectedDataKeys)
            : base(obj, postponeExecute: true)
        {
            if (expectedInletCount < 0) throw new LessThanException(() => expectedInletCount, 0);
            if (expectedOutletCount < 0) throw new LessThanException(() => expectedOutletCount, 0);
            if (expectedDataKeys == null) throw new NullException(() => expectedDataKeys);

            int uniqueExpectedDataPropertyKeyCount = expectedDataKeys.Distinct().Count();
            if (uniqueExpectedDataPropertyKeyCount != expectedDataKeys.Count)
            {
                throw new NotUniqueException(() => expectedDataKeys);
            }

            _expectedOperatorTypeEnum = expectedOperatorTypeEnum;
            _expectedInletCount = expectedInletCount;
            _expectedOutletCount = expectedOutletCount;
            _expectedDataKeysHashSet = expectedDataKeys.ToHashSet();

            Execute();
        }

        protected override void Execute()
        {
            Operator op = Object;

            For(() => op.GetOperatorTypeEnum(), PropertyDisplayNames.OperatorType).Is(_expectedOperatorTypeEnum);

            For(() => op.Inlets.Count, GetPropertyDisplayName_ForInletCount())
                .Is(_expectedInletCount);

            if (op.Inlets.Count == _expectedInletCount)
            {
                IList<Inlet> sortedInlets = op.Inlets.OrderBy(x => x.ListIndex).ToArray();
                for (int i = 0; i < sortedInlets.Count; i++)
                {
                    Inlet inlet = sortedInlets[i];

                    string messagePrefix = ValidationHelper.GetMessagePrefix(inlet, i + 1);
                    Execute(new InletValidator_ForOtherOperator(inlet, i), messagePrefix);
                }
            }

            For(() => op.Outlets.Count, GetPropertyDisplayName_ForOutletCount())
                .Is(_expectedOutletCount);

            if (op.Outlets.Count == _expectedOutletCount)
            {
                IList<Outlet> sortedOutlets = op.Outlets.OrderBy(x => x.ListIndex).ToArray();
                for (int i = 0; i < sortedOutlets.Count; i++)
                {
                    Outlet outlet = sortedOutlets[i];

                    string messagePrefix = ValidationHelper.GetMessagePrefix(outlet, i + 1);
                    Execute(new OutletValidator_ForOtherOperator(outlet, i), messagePrefix);
                }
            }

            // Operator Data
            if (_dataMaxLength.HasValue)
            {
                For(() => op.Data, PropertyDisplayNames.Data).MaxLength(_dataMaxLength.Value);
            }

            // HACK: Temporary (2016-03-28) to make it work for operators that just dump a number in the Data property, instead of encoded key-value pairs.
            bool mustValidateDataKeys = _expectedDataKeysHashSet.Count != 0;
            if (mustValidateDataKeys)
            {
                IList<string> actualDataKeysList = DataPropertyParser.GetKeys(op); // List, not HashSet, so we can do a unicity check.

                int uniqueActualDataKeyCount = actualDataKeysList.Distinct().Count();
                if (uniqueActualDataKeyCount != actualDataKeysList.Count)
                {
                    ValidationMessages.AddNotUniqueMessage(PropertyNames.DataKeys, PropertyDisplayNames.DataKeys);
                }
                else
                {
                    HashSet<string> actualDataKeysHashSet = actualDataKeysList.ToHashSet(); // HashSet, not List, so we can do value comparisons.

                    foreach (string expectedDataKey in _expectedDataKeysHashSet)
                    {
                        bool dataKeyExists = actualDataKeysHashSet.Contains(expectedDataKey);
                        if (!dataKeyExists)
                        {
                            string dataKeyIdentifier = ValidationHelper.GetDataKeyIdentifier(expectedDataKey);
                            ValidationMessages.AddNotExistsMessage(PropertyNames.DataKey, dataKeyIdentifier);
                        }
                    }

                    foreach (string actualDataKey in actualDataKeysHashSet)
                    {
                        bool dataKeyIsAllowed = _expectedDataKeysHashSet.Contains(actualDataKey);
                        if (!dataKeyIsAllowed)
                        {
                            string dataKeyIdentifier = ValidationHelper.GetDataKeyIdentifier(actualDataKey);
                            ValidationMessages.AddNotInListMessage(PropertyNames.DataKey, dataKeyIdentifier);
                        }
                    }
                }
            }
        }

        private string GetPropertyDisplayName_ForInletCount()
        {
            return CommonTitleFormatter.ObjectCount(PropertyDisplayNames.Inlets);
        }

        private string GetPropertyDisplayName_ForOutletCount()
        {
            return CommonTitleFormatter.ObjectCount(PropertyDisplayNames.Outlets);
        }

        private static int? GetDataMaxLength()
        {
            return ConfigurationHelper.GetSection<ConfigurationSection>().OperatorDataMaxLength;
        }
    }
}
