using JJ.Business.Synthesizer.Enums;
using System;
using System.Collections.Generic;
using JJ.Framework.Reflection.Exceptions;
using JJ.Framework.Common;
using System.Linq;

namespace JJ.Business.Synthesizer.Resources
{
    public static class MessageFormatter
    {
        public static string CannotChangeInletsBecauseOneIsStillFilledIn(int oneBasedInletNumber)
        {
            return String.Format(Messages.CannotChangeInletCountBecauseOneIsStillFilledIn, oneBasedInletNumber);
        }

        public static string CannotChangeOutletsBecauseOneIsStillFilledIn(int oneBasedOutletNumber)
        {
            return String.Format(Messages.CannotChangeOutletCountBecauseOneIsStillFilledIn, oneBasedOutletNumber);
        }

        public static string CannotDeleteBecauseHasReferences()
        {
            return Messages.CannotDeleteBecauseHasReferences;
        }

        public static string CannotDeleteCurveBecauseHasOperators(string name)
        {
            return String.Format(Messages.CannotDeleteCurveBecauseHasOperators, name);
        }

        public static string CannotDeleteSampleBecauseHasOperators(string name)
        {
            return String.Format(Messages.CannotDeleteSampleBecauseHasOperators, name);
        }

        public static string ChannelCountDoesNotMatchSpeakerSetup()
        {
            return Messages.ChannelCountDoesNotMatchSpeakerSetup;
        }

        public static string ChannelIndexNumberDoesNotMatchSpeakerSetup()
        {
            return Messages.ChannelIndexNumberDoesNotMatchSpeakerSetup;
        }

        public static string DocumentIsDependentOnDocument(string dependentDocumentName, string dependentOnDocumentName)
        {
            return String.Format(Messages.DocumentIsDependentOnDocument, dependentDocumentName, dependentOnDocumentName);
        }

        /// <summary>
        /// Note:
        /// When OperatorTypeEnum equals Undefined it will return a text like:
        /// "Undefined operator named '...' does not have ... filled in."
        /// </summary>
        public static string InletNotSet(OperatorTypeEnum operatorTypeEnum, string operatorName, string operandName)
        {
            return String.Format(Messages.InletNotSet, operatorTypeEnum, operatorName, operandName);
        }

        public static string InletNotSet(string operatorTypeName, string operatorName, string operandName)
        {
            return String.Format(Messages.InletNotSet, operatorTypeName, operatorName, operandName);
        }

        public static string InletPropertyDoesNotMatchWithUnderlyingPatch(string propertyDisplayName, string inletName, string dimensionDisplayName, int? inletListIndex)
        {
            return String.Format(Messages.InletPropertyDoesNotMatchWithUnderlyingPatch, propertyDisplayName, inletName, dimensionDisplayName, inletListIndex);
        }

        public static string MustBePowerOf2(string frequencyCount)
        {
            return String.Format(Messages.MustBePowerOf2, frequencyCount);
        }

        public static string NameOrDimensionMustBeFilledIn()
        {
            return Messages.NameOrDimensionMustBeFilledIn;
        }

        public static string NamesNotUnique_WithEntityTypeNameAndNames(string entityTypeDisplayName, IList<string> duplicateNames)
        {
            if (duplicateNames == null) throw new NullException(() => duplicateNames);

            string formattedDuplicateNames = String.Join(", ", duplicateNames.Select(x => String.Format("'{0}'", x)));

            return String.Format(Messages.NamesNotUnique_WithEntityTypeNameAndNames, entityTypeDisplayName, formattedDuplicateNames);
        }

        public static string NotFound_WithTypeName_AndName(string entityTypeDisplayName, string name)
        {
            return String.Format(Messages.NotFound_WithTypeName_AndName, entityTypeDisplayName, name);
        }

        public static string NotFoundInList_WithItemName_ID_AndListName(string name, int documentID, string listName)
        {
            return String.Format(Messages.NotFoundInList_WithItemName_ID_AndListName, name, documentID, listName);
        }

        public static string NotUnique_WithPropertyName_AndValue(string propertyDisplayName, object value)
        {
            return String.Format(Messages.NotUnique_WithPropertyName_AndValue, propertyDisplayName, value);
        }

        public static string NumberIs0WithName(string name)
        {
            return String.Format(Messages.NumberIs0WithName, name);
        }

        public static string OperatorHasNoInletFilledIn_WithOperatorName(string name)
        {
            return String.Format(Messages.OperatorHasNoInletFilledIn_WithOperatorName, name);
        }

        public static string OperatorHasNoInletsFilledIn_WithOperatorName(string name)
        {
            return String.Format(Messages.OperatorHasNoInletsFilledIn_WithOperatorName, name);
        }

        public static string OperatorHasNoItemsFilledIn_WithOperatorName(string name)
        {
            return String.Format(Messages.OperatorHasNoItemsFilledIn_WithOperatorName, name);
        }

        public static string OperatorIsCircularWithName(string name)
        {
            return String.Format(Messages.OperatorIsCircularWithName, name);
        }

        public static string OperatorIsInGraphButNotInList(string operatorIdentifier)
        {
            return String.Format(Messages.OperatorIsInGraphButNotInList, operatorIdentifier);
        }

        public static string OperatorPatchIsNotTheExpectedPatch(string operatorName, string expectedPatchName)
        {
            return String.Format(Messages.OperatorPatchIsNotTheExpectedPatch, operatorName, expectedPatchName);
        }

        public static string OutletPropertyDoesNotMatchWithUnderlyingPatch(string propertyDisplayName, string inletName, string dimensionDisplayName, int? inletListIndex)
        {
            return String.Format(Messages.OutletPropertyDoesNotMatchWithUnderlyingPatch, propertyDisplayName, inletName, dimensionDisplayName, inletListIndex);
        }

        public static string SampleCount0(string sampleName)
        {
            return String.Format(Messages.SampleCount0, sampleName);
        }

        public static string SampleNotActive(string sampleName)
        {
            return String.Format(Messages.SampleNotActive, sampleName);
        }

        public static string SampleNotLoaded(string sampleName)
        {
            return String.Format(Messages.SampleNotLoaded, sampleName);
        }
    }
}
