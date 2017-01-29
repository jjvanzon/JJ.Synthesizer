using JJ.Business.Synthesizer.Enums;
using System;
using System.Collections.Generic;
using JJ.Framework.Exceptions;
using System.Linq;

namespace JJ.Business.Synthesizer.Resources
{
    public static class MessageFormatter
    {
        public static string CannotChangeInletsBecauseOneIsStillFilledIn(int oneBasedInletNumber)
        {
            return string.Format(Messages.CannotChangeInletCountBecauseOneIsStillFilledIn, oneBasedInletNumber);
        }

        public static string CannotChangeOutletsBecauseOneIsStillFilledIn(int oneBasedOutletNumber)
        {
            return string.Format(Messages.CannotChangeOutletCountBecauseOneIsStillFilledIn, oneBasedOutletNumber);
        }

        public static string CannotDeleteBecauseHasReferences()
        {
            return Messages.CannotDeleteBecauseHasReferences;
        }

        public static string CannotDeleteCurveBecauseHasOperators(string name)
        {
            return string.Format(Messages.CannotDeleteCurveBecauseHasOperators, name);
        }

        public static string CannotDeleteSampleBecauseHasOperators(string name)
        {
            return string.Format(Messages.CannotDeleteSampleBecauseHasOperators, name);
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
            return string.Format(Messages.DocumentIsDependentOnDocument, dependentDocumentName, dependentOnDocumentName);
        }

        /// <summary>
        /// Note:
        /// When OperatorTypeEnum equals Undefined it will return a text like:
        /// "Undefined operator named '...' does not have ... filled in."
        /// </summary>
        public static string InletNotSet(OperatorTypeEnum operatorTypeEnum, string operatorName, string operandName)
        {
            return string.Format(Messages.InletNotSet, operatorTypeEnum, operatorName, operandName);
        }

        public static string InletNotSet(string operatorTypeName, string operatorName, string operandName)
        {
            return string.Format(Messages.InletNotSet, operatorTypeName, operatorName, operandName);
        }

        public static string InletPropertyDoesNotMatchWithUnderlyingPatch(string propertyDisplayName)
        {
            return string.Format(Messages.InletPropertyDoesNotMatchWithUnderlyingPatch, propertyDisplayName);
        }

        public static string MustBePowerOf2(string frequencyCount)
        {
            return string.Format(Messages.MustBePowerOf2, frequencyCount);
        }

        public static string NameOrDimensionMustBeFilledIn()
        {
            return Messages.NameOrDimensionMustBeFilledIn;
        }

        public static string NamesNotUnique_WithEntityTypeNameAndNames(string entityTypeDisplayName, IList<string> duplicateNames)
        {
            if (duplicateNames == null) throw new NullException(() => duplicateNames);

            string formattedDuplicateNames = string.Join(", ", duplicateNames.Select(x => string.Format("'{0}'", x)));

            return string.Format(Messages.NamesNotUnique_WithEntityTypeNameAndNames, entityTypeDisplayName, formattedDuplicateNames);
        }

        public static string NotFound_WithTypeName_AndName(string entityTypeDisplayName, string name)
        {
            return string.Format(Messages.NotFound_WithTypeName_AndName, entityTypeDisplayName, name);
        }

        public static string NotFoundInList_WithItemName_ID_AndListName(string name, int documentID, string listName)
        {
            return string.Format(Messages.NotFoundInList_WithItemName_ID_AndListName, name, documentID, listName);
        }

        public static string NotUnique_WithPropertyName_AndValue(string propertyDisplayName, object value)
        {
            return string.Format(Messages.NotUnique_WithPropertyName_AndValue, propertyDisplayName, value);
        }

        public static string NumberIs0WithName(string name)
        {
            return string.Format(Messages.NumberIs0WithName, name);
        }

        public static string OperatorHasNoInletFilledIn_WithOperatorName(string name)
        {
            return string.Format(Messages.OperatorHasNoInletFilledIn_WithOperatorName, name);
        }

        public static string OperatorHasNoInletsFilledIn_WithOperatorName(string name)
        {
            return string.Format(Messages.OperatorHasNoInletsFilledIn_WithOperatorName, name);
        }

        public static string OperatorHasNoItemsFilledIn_WithOperatorName(string name)
        {
            return string.Format(Messages.OperatorHasNoItemsFilledIn_WithOperatorName, name);
        }

        public static string OperatorIsCircularWithName(string name)
        {
            return string.Format(Messages.OperatorIsCircularWithName, name);
        }

        public static string OperatorIsInGraphButNotInList(string operatorIdentifier)
        {
            return string.Format(Messages.OperatorIsInGraphButNotInList, operatorIdentifier);
        }

        public static string OperatorPatchIsNotTheExpectedPatch(string operatorName, string expectedPatchName)
        {
            return string.Format(Messages.OperatorPatchIsNotTheExpectedPatch, operatorName, expectedPatchName);
        }

        public static string OutletPropertyDoesNotMatchWithUnderlyingPatch(string propertyDisplayName)
        {
            return string.Format(Messages.OutletPropertyDoesNotMatchWithUnderlyingPatch, propertyDisplayName);
        }

        public static string SampleCount0(string sampleName)
        {
            return string.Format(Messages.SampleCount0, sampleName);
        }

        public static string SampleNotActive(string sampleName)
        {
            return string.Format(Messages.SampleNotActive, sampleName);
        }

        public static string SampleNotLoaded(string sampleName)
        {
            return string.Format(Messages.SampleNotLoaded, sampleName);
        }
    }
}
