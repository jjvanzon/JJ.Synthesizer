using JJ.Business.Synthesizer.Enums;
using System;

namespace JJ.Business.Synthesizer.Resources
{
    public static class MessageFormatter
    {
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

        public static string NumberIs0WithName(string name)
        {
            return String.Format(Messages.NumberIs0WithName, name);
        }

        public static string UnsupportedOperatorTypeEnumValue(OperatorTypeEnum operatorTypeEnum)
        {
            return String.Format(Messages.UnsupportedOperatorTypeEnumValue, operatorTypeEnum);
        }

        public static string OperatorTypeMustBeOfType(string operatorTypeDisplayName)
        {
            return String.Format(Messages.OperatorTypeMustBeOfType, operatorTypeDisplayName);
        }

        internal static string OperatorTypeMustBeAdderOrBundle()
        {
            return Messages.OperatorTypeMustBeAdderOrBundle;
        }

        public static string SampleNotLoaded(string sampleName)
        {
            return String.Format(Messages.SampleNotLoaded, sampleName);
        }

        public static string ObjectAmplifier0(string objectTypeName, string objectName)
        {
            return String.Format(Messages.ObjectAmplifier0, objectTypeName, objectName);
        }

        public static string CannotChangeInletsBecauseOneIsStillFilledIn(int oneBasedInletNumber)
        {
            return String.Format(Messages.CannotChangeInletCountBecauseOneIsStillFilledIn, oneBasedInletNumber);
        }

        public static string CannotChangeOutletsBecauseOneIsStillFilledIn(int oneBasedOutletNumber)
        {
            return String.Format(Messages.CannotChangeOutletCountBecauseOneIsStillFilledIn, oneBasedOutletNumber);
        }

        public static string SampleNotActive(string sampleName)
        {
            return String.Format(Messages.SampleNotActive, sampleName);
        }

        public static string SampleCount0(string sampleName)
        {
            return String.Format(Messages.SampleCount0, sampleName);
        }

        public static string ChannelCountDoesNotMatchSpeakerSetup()
        {
            return Messages.ChannelCountDoesNotMatchSpeakerSetup;
        }

        public static string CannotDeleteBecauseHasReferences()
        {
            return Messages.CannotDeleteBecauseHasReferences;
        }

        public static string ChannelIndexNumberDoesNotMatchSpeakerSetup()
        {
            return Messages.ChannelIndexNumberDoesNotMatchSpeakerSetup;
        }

        public static string OperatorPatchIsNotTheExpectedPatch(string operatorName, string expectedPatchName)
        {
            return String.Format(Messages.OperatorPatchIsNotTheExpectedPatch, operatorName, expectedPatchName);
        }

        public static string DocumentIsDependentOnDocument(string dependentDocumentName, string dependentOnDocumentName)
        {
            return String.Format(Messages.DocumentIsDependentOnDocument, dependentDocumentName, dependentOnDocumentName);
        }

        public static string CannotDeleteCurveBecauseHasOperators(string name)
        {
            return String.Format(Messages.CannotDeleteCurveBecauseHasOperators, name);
        }

        public static string CannotDeleteSampleBecauseHasOperators(string name)
        {
            return String.Format(Messages.CannotDeleteSampleBecauseHasOperators, name);
        }

        public static string CannotDeletePatchBecauseIsMainPatch(string name)
        {
            return String.Format(Messages.CannotDeletePatchBecauseIsMainPatch, name);
        }

        public static string OperatorIsCircularWithName(string name)
        {
            return String.Format(Messages.OperatorIsCircularWithName, name);
        }

        public static string NotFoundInList_WithItemName_ID_AndListName(string name, int documentID, string listName)
        {
            return String.Format(Messages.NotFoundInList_WithItemName_ID_AndListName, name, documentID, listName);
        }

        public static string NotFoundInList_WithItemName_AndID(string name, int documentID)
        {
            return String.Format(Messages.NotFoundInList_WithItemName_AndID, name, documentID);
        }

        public static string NotFound_WithTypeName_AndName(string entityTypeDisplayName, string name)
        {
            return String.Format(Messages.NotFound_WithTypeName_AndName, entityTypeDisplayName, name);
        }

        public static string CustomOperatorInletWithNameNotFoundInDocumentMainPatch(string name)
        {
            return String.Format(Messages.CustomOperatorInletWithNameNotFoundInDocumentMainPatch, name);
        }

        public static string CustomOperatorOutletWithNameNotFoundInDocumentMainPatch(string name)
        {
            return String.Format(Messages.CustomOperatorOutletWithNameNotFoundInDocumentMainPatch, name);
        }

        public static string OperatorHasNoInletsFilledIn_WithOperatorName(string name)
        {
            return String.Format(Messages.OperatorHasNoInletsFilledIn_WithOperatorName, name);
        }

        public static string OperatorHasNoInletFilledIn_WithOperatorName(string name)
        {
            return String.Format(Messages.OperatorHasNoInletFilledIn_WithOperatorName, name);
        }
    }
}
