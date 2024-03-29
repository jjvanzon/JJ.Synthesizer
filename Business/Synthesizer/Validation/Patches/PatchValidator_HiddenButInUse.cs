﻿using System.Collections.Generic;
using JJ.Business.Synthesizer.Configuration;
using JJ.Business.Synthesizer.Extensions;
using JJ.Business.Synthesizer.StringResources;
using JJ.Data.Synthesizer.Entities;
using JJ.Data.Synthesizer.RepositoryInterfaces;
using JJ.Framework.Configuration;
using JJ.Framework.Exceptions.Basic;
using JJ.Framework.Validation;

namespace JJ.Business.Synthesizer.Validation.Patches
{
    internal class PatchValidator_HiddenButInUse : VersatileValidator
    {
        private static readonly bool _hiddenButInUseValidationEnabled = CustomConfigurationManager.GetSection<ConfigurationSection>().HiddenButInUseValidationEnabled;

        public PatchValidator_HiddenButInUse(Patch lowerPatch, ICurveRepository curveRepository)
        {
            if (lowerPatch == null) throw new NullException(() => lowerPatch);

            if (!_hiddenButInUseValidationEnabled)
            {
                return;
            }

            // ReSharper disable once InvertIf
            if (lowerPatch.Hidden)
            {
                string lowerPatchIdentifier = ResourceFormatter.Patch + " " + ValidationHelper.GetUserFriendlyIdentifier(lowerPatch);

                IEnumerable<Operator> customOperators = lowerPatch.EnumerateDerivedOperators();
                foreach (Operator op in customOperators)
                {
                    bool isExternal = op.Patch.Document != lowerPatch.Document;
                    if (!isExternal)
                    {
                        continue;
                    }

                    Patch higherPatch = op.Patch;
                    string higherDocumentPrefix = ValidationHelper.TryGetHigherDocumentPrefix(lowerPatch, higherPatch);
                    string higherPatchPrefix = ValidationHelper.GetMessagePrefix(op.Patch);
                    string higherOperatorIdentifier = ResourceFormatter.Operator + " " + ValidationHelper.GetUserFriendlyIdentifier(op, curveRepository);

                    Messages.Add(
                        ResourceFormatter.CannotHide_WithName_AndDependentItem(lowerPatchIdentifier, higherDocumentPrefix + higherPatchPrefix + higherOperatorIdentifier));
                }
            }
        }
    }
}