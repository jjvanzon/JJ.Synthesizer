using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using JJ.Framework.Data;
using JJ.Framework.Exceptions;
using JJ.Data.Synthesizer;
using JJ.Data.Canonical;
using JJ.Business.Synthesizer.EntityWrappers;
using JJ.Business.Synthesizer.Enums;
using JJ.Business.Synthesizer.Extensions;
using JJ.Business.Synthesizer.Helpers;
using JJ.Business.Synthesizer;
using JJ.Business.Canonical;
using JJ.Business.Synthesizer.LinkTo;
using JJ.Framework.Common;
using JJ.Data.Synthesizer.DefaultRepositories.Interfaces;

namespace JJ.OneOff.Synthesizer.DataMigration
{
    internal class DataMigrationExecutor
    {
        private class InletOrOutletDimensionTuple
        {
            public InletOrOutletDimensionTuple(
                OperatorTypeEnum operatorTypeEnum,
                int listIndex,
                DimensionEnum dimensionEnum)
            {
                OperatorTypeEnum = operatorTypeEnum;
                ListIndex = listIndex;
                DimensionEnum = dimensionEnum;
            }

            public OperatorTypeEnum OperatorTypeEnum { get; }
            public int ListIndex { get; }
            public DimensionEnum DimensionEnum { get; }
        }

        private const int DEFAULT_FREQUENCY = 440;

        public static void AssertAllDocuments(Action<string> progressCallback)
        {
            if (progressCallback == null) throw new NullException(() => progressCallback);

            progressCallback(String.Format("Starting {0}...", MethodBase.GetCurrentMethod().Name));

            using (IContext context = PersistenceHelper.CreateContext())
            {
                RepositoryWrapper repositories = PersistenceHelper.CreateRepositoryWrapper(context);
                AssertDocuments(repositories, progressCallback);
            }

            progressCallback(String.Format("{0} finished.", MethodBase.GetCurrentMethod().Name));
        }

        public static void DeleteOrphanedEntityPositions(Action<string> progressCallback)
        {
            if (progressCallback == null) throw new NullException(() => progressCallback);

            progressCallback(String.Format("Starting {0}...", MethodBase.GetCurrentMethod().Name));

            using (IContext context = PersistenceHelper.CreateContext())
            {
                RepositoryWrapper repositories = PersistenceHelper.CreateRepositoryWrapper(context);

                var entityPositionManager = new EntityPositionManager(repositories.EntityPositionRepository, repositories.IDRepository);
                int rowsAffected = entityPositionManager.DeleteOrphans();

                AssertDocuments(repositories, progressCallback);

                context.Commit();
            }

            progressCallback(String.Format("{0} finished.", MethodBase.GetCurrentMethod().Name));
        }

        //public static void MigrateSineVolumes(Action<string> progressCallback)
        //{
        //    throw new NotSupportedException();

        //    if (progressCallback == null) throw new NullException(() => progressCallback);

        //    progressCallback(String.Format("Starting {0}...", MethodBase.GetCurrentMethod().Name));

        //    using (IContext context = PersistenceHelper.CreateContext())
        //    {
        //        RepositoryWrapper repositories = PersistenceHelper.CreateRepositoryWrapper(context);

        //        var patchManager = new PatchManager(new PatchRepositories(repositories));

        //        IList<Operator> sineOperators = repositories.OperatorRepository
        //                                                    .GetAll()
        //                                                    .Where(x => x.GetOperatorTypeEnum() == OperatorTypeEnum.Sine)
        //                                                    .ToArray();
        //        for (int i = 0; i < sineOperators.Count; i++)
        //        {
        //            Operator sineOperator = sineOperators[i];

        //            patchManager.Patch = sineOperator.Patch;

        //            var sine = new Sine_OperatorWrapper(sineOperator);
        //            throw new Exception("Process cannot be run anymore, since the data has been migrated so that there are no more volumes anymore.");
        //            //if (sine.Volume != null)
        //            {
        //                var multiply = patchManager.Multiply();

        //                foreach (Inlet inlet in sine.Result.ConnectedInlets.ToArray())
        //                {
        //                    inlet.LinkTo(multiply.Result);
        //                }

        //                //multiply.A = sine.Volume;
        //                multiply.B = sine.Result;

        //                //sine.Volume = null;

        //                VoidResult result = patchManager.SavePatch();
        //                ResultHelper.Assert(result);
        //            }

        //            string progressMessage = String.Format("Migrated sine {0}/{1}.", i + 1, sineOperators.Count);
        //            progressCallback(progressMessage);
        //        }

        //        context.Commit();

        //        progressCallback(String.Format("{0} finished.", MethodBase.GetCurrentMethod().Name));
        //    }
        //}

        //public static void AddSampleOperatorFrequencyInlets(Action<string> progressCallback)
        //{
        //    if (progressCallback == null) throw new NullException(() => progressCallback);

        //    progressCallback(String.Format("Starting {0}...", MethodBase.GetCurrentMethod().Name));

        //    using (IContext context = PersistenceHelper.CreateContext())
        //    {
        //        RepositoryWrapper repositories = PersistenceHelper.CreateRepositoryWrapper(context);

        //        var patchManager = new PatchManager(new PatchRepositories(repositories));

        //        IList<Operator> sampleOperators = repositories.OperatorRepository
        //                                                      .GetAll()
        //                                                      .Where(x => x.GetOperatorTypeEnum() == OperatorTypeEnum.Sample)
        //                                                      .ToArray();

        //        for (int i = 0; i < sampleOperators.Count; i++)
        //        {
        //            Operator sampleOperator = sampleOperators[i];

        //            patchManager.Patch = sampleOperator.Patch;

        //            var sampleOperatorWrapper = new Sample_OperatorWrapper(sampleOperator, repositories.SampleRepository);

        //            if (sampleOperator.Inlets.Count == 0)
        //            {
        //                Inlet inlet = patchManager.CreateInlet(sampleOperator);

        //                var numberWrapper = patchManager.Number(DEFAULT_FREQUENCY);

        //                sampleOperatorWrapper.Frequency = numberWrapper;

        //                VoidResult result = patchManager.SavePatch();
        //                ResultHelper.Assert(result);
        //            }

        //            string progressMessage = String.Format("Migrated sample operator {0}/{1}.", i + 1, sampleOperators.Count);
        //            progressCallback(progressMessage);
        //        }

        //        context.Commit();

        //        progressCallback(String.Format("{0} finished.", MethodBase.GetCurrentMethod().Name));
        //    }
        //}

        ///// <summary>
        ///// Renames all the patches, so overwrites all of the names.
        ///// Currently (2015-11-21) this is acceptable since no patch has a specific name.
        ///// They are just called 'Patch 1', 'Patch 2', etc.
        ///// The new names become 'Patch 1', etc. too, but always with the English term for Patch, not Dutch.
        ///// </summary>
        //public static void MakePatchNamesUnique(Action<string> progressCallback)
        //{
        //    if (progressCallback == null) throw new NullException(() => progressCallback);

        //    progressCallback(String.Format("Starting {0}...", MethodBase.GetCurrentMethod().Name));

        //    CultureHelper.SetThreadCultureName("en-US");

        //    using (IContext context = PersistenceHelper.CreateContext())
        //    {
        //        RepositoryWrapper repositories = PersistenceHelper.CreateRepositoryWrapper(context);

        //        IList<Document> rootDocuments = repositories.DocumentRepository.GetAll().Where(x => x.ParentDocument == null).ToArray();

        //        for (int i = 0; i < rootDocuments.Count; i++)
        //        {
        //            Document rootDocument = rootDocuments[i];

        //            int patchNumber = 1;

        //            // Traverse patches in a very specific order, so that the numbers do not seem all mixed up.
        //            //IEnumerable<Document> childDocuments = rootDocument.ChildDocuments.OrderBy(x => x.ChildDocumentType.ID).ThenBy(x => x.Name);
        //            IEnumerable<Document> childDocuments = rootDocument.ChildDocuments.OrderBy(x => x.GroupName).ThenBy(x => x.Name);
        //            foreach (Document childDocument in childDocuments)
        //            {
        //                foreach (Patch patch in childDocument.Patches)
        //                {
        //                    string newName = String.Format("{0} {1}", PropertyDisplayNames.Patch, patchNumber);
        //                    patch.Name = newName;

        //                    patchNumber++;
        //                }
        //            }

        //            foreach (Patch patch in rootDocument.Patches)
        //            {
        //                string newName = String.Format("{0} {1}", PropertyDisplayNames.Patch, patchNumber);
        //                patch.Name = newName;

        //                patchNumber++;
        //            }

        //            string progressMessage = String.Format("Migrated document {0}/{1}.", i + 1, rootDocuments.Count);
        //            progressCallback(progressMessage);
        //        }

        //        context.Commit();
        //    }

        //    progressCallback(String.Format("{0} finished.", MethodBase.GetCurrentMethod().Name));
        //}

        ///// <summary>
        ///// Renames curves and samples so they are unique within a root documant and its child documents.
        ///// Only renames ones with actual duplicate names.
        ///// </summary>
        //public static void MakeCurveNamesAndSampleNamesUnique(Action<string> progressCallback)
        //{
        //    if (progressCallback == null) throw new NullException(() => progressCallback);

        //    progressCallback(String.Format("Starting {0}...", MethodBase.GetCurrentMethod().Name));

        //    using (IContext context = PersistenceHelper.CreateContext())
        //    {
        //        RepositoryWrapper repositories = PersistenceHelper.CreateRepositoryWrapper(context);

        //        var documentManager = new DocumentManager(repositories);

        //        IList<Document> rootDocuments = repositories.DocumentRepository.GetAll().Where(x => x.ParentDocument == null).ToArray();
        //        for (int i = 0; i < rootDocuments.Count; i++)
        //        {
        //            Document rootDocument = rootDocuments[i];

        //            // Rename duplicate Curves
        //            var duplicateCurveGroups = rootDocument.EnumerateSelfAndParentAndTheirChildren()
        //                                                   .SelectMany(x => x.Curves)
        //                                                   .GroupBy(x => x.Name)
        //                                                   .Where(x => x.Count() > 1);

        //            foreach (var duplicateCurveGroup in duplicateCurveGroups)
        //            {
        //                int curveNumber = 1;
        //                foreach (Curve curve in duplicateCurveGroup)
        //                {
        //                    curve.Name = String.Format("{0} ({1})", curve.Name, curveNumber);
        //                    curveNumber++;
        //                }
        //            }

        //            // Rename duplicate Samples
        //            var duplicateSampleGroups = rootDocument.EnumerateSelfAndParentAndTheirChildren()
        //                                                   .SelectMany(x => x.Samples)
        //                                                   .GroupBy(x => x.Name)
        //                                                   .Where(x => x.Count() > 1);

        //            foreach (var duplicateSampleGroup in duplicateSampleGroups)
        //            {
        //                int sampleNumber = 1;
        //                foreach (Sample sample in duplicateSampleGroup)
        //                {
        //                    sample.Name = String.Format("{0} ({1})", sample.Name, sampleNumber);
        //                    sampleNumber++;
        //                }
        //            }

        //            // Validate
        //            VoidResult result = documentManager.ValidateRecursive(rootDocument);
        //            ResultHelper.Assert(result);

        //            string progressMessage = String.Format("Migrated document {0}/{1}.", i + 1, rootDocuments.Count);
        //            progressCallback(progressMessage);
        //        }

        //        context.Commit();
        //    }

        //    progressCallback(String.Format("{0} finished.", MethodBase.GetCurrentMethod().Name));
        //}

        ///// <summary>
        ///// Puts every patch in its own child document and asserts that no child document has more than one patch.
        ///// </summary>
        //public static void PutEachPatchInAChildDocument(Action<string> progressCallback)
        //{
        //    if (progressCallback == null) throw new NullException(() => progressCallback);

        //    progressCallback(String.Format("Starting {0}...", MethodBase.GetCurrentMethod().Name));

        //    CultureHelper.SetThreadCultureName("en-US");

        //    using (IContext context = PersistenceHelper.CreateContext())
        //    {
        //        RepositoryWrapper repositories = PersistenceHelper.CreateRepositoryWrapper(context);

        //        var documentManager = new DocumentManager(repositories);
        //        var patchManager = new PatchManager(new PatchRepositories(repositories));

        //        IList<Document> rootDocuments = repositories.DocumentRepository.GetAll().Where(x => x.ParentDocument == null).ToArray();
        //        for (int i = 0; i < rootDocuments.Count; i++)
        //        {
        //            Document rootDocument = rootDocuments[i];

        //            // Move patches to their own child document.
        //            foreach (Patch patch in rootDocument.Patches.ToArray())
        //            {
        //                //Document newChildDocument = documentManager.CreateChildDocument(rootDocument, ChildDocumentTypeEnum.Instrument, mustGenerateName: true);
        //                Document newChildDocument = documentManager.CreateChildDocument(rootDocument, mustGenerateName: true);
        //                patch.LinkTo(newChildDocument);
        //            }

        //            // Give each child document minimally one patch.
        //            foreach (Document childDocument in rootDocument.ChildDocuments)
        //            {
        //                if (childDocument.Patches.Count == 0)
        //                {
        //                    patchManager.CreatePatch(childDocument, mustGenerateName: true);
        //                    Patch newPatch = patchManager.Patch;
        //                }
        //            }

        //            // Assert no child document has more than 1 patch.
        //            foreach (Document childDocument in rootDocument.ChildDocuments)
        //            {
        //                if (childDocument.Patches.Count > 1)
        //                {
        //                    throw new Exception(String.Format("childDocument with Name '{0}' and ID '{1}' has more than one Patch.", childDocument.Name, childDocument.ID));
        //                }
        //            }

        //            // Validate
        //            VoidResult result = documentManager.ValidateRecursive(rootDocument);
        //            ResultHelper.Assert(result);

        //            string progressMessage = String.Format("Migrated document {0}/{1}.", i + 1, rootDocuments.Count);
        //            progressCallback(progressMessage);
        //        }

        //        context.Commit();
        //    }

        //    progressCallback(String.Format("{0} finished.", MethodBase.GetCurrentMethod().Name));
        //}

        //public static void ConvertUnderlyingDocumentIDsToUnderlyingPatchIDs(Action<string> progressCallback)
        //{
        //    if (progressCallback == null) throw new NullException(() => progressCallback);

        //    progressCallback(String.Format("Starting {0}...", MethodBase.GetCurrentMethod().Name));

        //    using (IContext context = PersistenceHelper.CreateContext())
        //    {
        //        RepositoryWrapper repositories = PersistenceHelper.CreateRepositoryWrapper(context);

        //        IList<Operator> customOperators = repositories.OperatorRepository.GetAll()
        //                                                                         .Where(x => x.GetOperatorTypeEnum() == OperatorTypeEnum.CustomOperator)
        //                                                                         .ToArray();
        //        for (int i = 0; i < customOperators.Count; i++)
        //        {
        //            Operator customOperator = customOperators[i];

        //            var wrapper = new CustomOperator_OperatorWrapper(customOperator, repositories.PatchRepository);

        //            // Try using the UnderlyingPatchID as a DocumentID.
        //            int? underlyingEntityID = wrapper.UnderlyingPatchID;
        //            if (underlyingEntityID != null)
        //            {
        //                Document document = repositories.DocumentRepository.TryGet(underlyingEntityID.Value);
        //                if (document != null)
        //                {
        //                    // It is a DocumentID. Make it the PatchID.
        //                    if (document.Patches.Count != 1)
        //                    {
        //                        throw new NotEqualException(() => document.Patches.Count, 1);
        //                    }

        //                    wrapper.UnderlyingPatch = document.Patches[0];
        //                }
        //            }

        //            string progressMessage = String.Format("Migrated custom operator {0}/{1}.", i + 1, customOperators.Count);
        //            progressCallback(progressMessage);
        //        }

        //        context.Commit();
        //    }

        //    progressCallback(String.Format("{0} finished.", MethodBase.GetCurrentMethod().Name));
        //}

        //public static void GivePatchesSameNameAsTheirDocuments(Action<string> progressCallback)
        //{
        //    if (progressCallback == null) throw new NullException(() => progressCallback);

        //    progressCallback(String.Format("Starting {0}...", MethodBase.GetCurrentMethod().Name));

        //    using (IContext context = PersistenceHelper.CreateContext())
        //    {
        //        RepositoryWrapper repositories = PersistenceHelper.CreateRepositoryWrapper(context);

        //        IList<Patch> patches = repositories.PatchRepository.GetAll().ToArray();

        //        for (int i = 0; i < patches.Count; i++)
        //        {
        //            Patch patch = patches[i];

        //            patch.Name = patch.Document.Name;

        //            string progressMessage = String.Format("Migrated Patch {0}/{1}.", i + 1, patches.Count);
        //            progressCallback(progressMessage);
        //        }

        //        AssertDocuments(repositories, progressCallback);

        //        context.Commit();
        //    }

        //    progressCallback(String.Format("{0} finished.", MethodBase.GetCurrentMethod().Name));
        //}

        //public static void ResaveCustomOperatorsToSet_InletDefaultValue_InletDimension_And_OutletDimension(Action<string> progressCallback)
        //{
        //    if (progressCallback == null) throw new NullException(() => progressCallback);

        //    progressCallback(String.Format("Starting {0}...", MethodBase.GetCurrentMethod().Name));

        //    using (IContext context = PersistenceHelper.CreateContext())
        //    {
        //        RepositoryWrapper repositories = PersistenceHelper.CreateRepositoryWrapper(context);

        //        var patchManager = new PatchManager(new PatchRepositories(repositories));

        //        IList<Operator> customOperators = repositories.OperatorRepository
        //                                                      .GetAll()
        //                                                      .Where(x => x.GetOperatorTypeEnum() == OperatorTypeEnum.CustomOperator)
        //                                                      .ToArray();

        //        for (int i = 0; i < customOperators.Count; i++)
        //        {
        //            Operator customOperator = customOperators[i];
        //            patchManager.Patch = customOperator.Patch;

        //            VoidResult result = patchManager.SaveOperator(customOperator);
        //            ResultHelper.Assert(result);

        //            string progressMessage = String.Format("Migrated custom operator {0}/{1}.", i + 1, customOperators.Count);
        //            progressCallback(progressMessage);
        //        }

        //        AssertDocuments(repositories, progressCallback);

        //        context.Commit();
        //    }

        //    progressCallback(String.Format("{0} finished.", MethodBase.GetCurrentMethod().Name));
        //}

        //public static void ResavePatchInletOperatorsToSet_InletDefaultValue_AndInletDimension(Action<string> progressCallback)
        //{
        //    if (progressCallback == null) throw new NullException(() => progressCallback);

        //    progressCallback(String.Format("Starting {0}...", MethodBase.GetCurrentMethod().Name));

        //    using (IContext context = PersistenceHelper.CreateContext())
        //    {
        //        RepositoryWrapper repositories = PersistenceHelper.CreateRepositoryWrapper(context);

        //        var patchManager = new PatchManager(new PatchRepositories(repositories));

        //        IList<Operator> patchInlets = repositories.OperatorRepository
        //                                                          .GetAll()
        //                                                          .Where(x => x.GetOperatorTypeEnum() == OperatorTypeEnum.PatchInlet)
        //                                                          .ToArray();

        //        for (int i = 0; i < patchInlets.Count; i++)
        //        {
        //            Operator patchInlet = patchInlets[i];
        //            patchManager.Patch = patchInlet.Patch;

        //            VoidResult result = patchManager.SaveOperator(patchInlet);
        //            ResultHelper.Assert(result);

        //            string progressMessage = String.Format("Migrated PatchInlet Operator {0}/{1}.", i + 1, patchInlets.Count);
        //            progressCallback(progressMessage);
        //        }

        //        AssertDocuments(repositories, progressCallback);

        //        context.Commit();
        //    }

        //    progressCallback(String.Format("{0} finished.", MethodBase.GetCurrentMethod().Name));
        //}

        //public static void Remove_PatchInletOperator_DataKeys_DefaultValue_AndDimensionEnum(Action<string> progressCallback)
        //{
        //    if (progressCallback == null) throw new NullException(() => progressCallback);

        //    progressCallback(String.Format("Starting {0}...", MethodBase.GetCurrentMethod().Name));

        //    using (IContext context = PersistenceHelper.CreateContext())
        //    {
        //        RepositoryWrapper repositories = PersistenceHelper.CreateRepositoryWrapper(context);

        //        var patchManager = new PatchManager(new PatchRepositories(repositories));

        //        IList<Operator> patchInlets = repositories.OperatorRepository
        //                                                          .GetAll()
        //                                                          .Where(x => x.GetOperatorTypeEnum() == OperatorTypeEnum.PatchInlet)
        //                                                          .ToArray();
        //        for (int i = 0; i < patchInlets.Count; i++)
        //        {
        //            Operator patchInlet = patchInlets[i];

        //            DataPropertyParser.RemoveKey(patchInlet, "DefaultValue");
        //            DataPropertyParser.RemoveKey(patchInlet, "DimensionEnum");

        //            string progressMessage = String.Format("Migrated PatchInlet Operator {0}/{1}.", i + 1, patchInlets.Count);
        //            progressCallback(progressMessage);
        //        }

        //        AssertDocuments(repositories, progressCallback);

        //        context.Commit();
        //    }

        //    progressCallback(String.Format("{0} finished.", MethodBase.GetCurrentMethod().Name));
        //}

        //public static void Migrate_PatchOutlet_Dimension_FromDataProperty_ToPatchOutletOutlet(Action<string> progressCallback)
        //{
        //    if (progressCallback == null) throw new NullException(() => progressCallback);

        //    progressCallback(String.Format("Starting {0}...", MethodBase.GetCurrentMethod().Name));

        //    using (IContext context = PersistenceHelper.CreateContext())
        //    {
        //        RepositoryWrapper repositories = PersistenceHelper.CreateRepositoryWrapper(context);

        //        var patchManager = new PatchManager(new PatchRepositories(repositories));

        //        IList<Operator> patchOutlets = repositories.OperatorRepository
        //                                                          .GetAll()
        //                                                          .Where(x => x.GetOperatorTypeEnum() == OperatorTypeEnum.PatchOutlet)
        //                                                          .ToArray();
        //        for (int i = 0; i < patchOutlets.Count; i++)
        //        {
        //            Operator patchOutlet = patchOutlets[i];

        //            string progressMessage = String.Format("Migrating PatchOutlet Operator {0}/{1}.", i + 1, patchOutlets.Count);
        //            progressCallback(progressMessage);

        //            var wrapper = new PatchOutlet_OperatorWrapper(patchOutlet);

        //            DimensionEnum dimensionEnum = DataPropertyParser.GetEnum<DimensionEnum>(patchOutlet, "DimensionEnum");

        //            wrapper.Result.SetDimensionEnum(dimensionEnum, repositories.DimensionRepository);

        //            // Make side-effects go off.
        //            patchManager.Patch = patchOutlet.Patch;
        //            VoidResult result = patchManager.SaveOperator(patchOutlet);
        //            try
        //            {
        //                ResultHelper.Assert(result);
        //            }
        //            catch
        //            {
        //                progressCallback("Exception!");
        //            }

        //            DataPropertyParser.RemoveKey(patchOutlet, "DimensionEnum");
        //        }

        //        AssertDocuments(repositories, progressCallback);

        //        context.Commit();
        //    }

        //    progressCallback(String.Format("{0} finished.", MethodBase.GetCurrentMethod().Name));
        //}

        //public static void MigrateResampleOperators_SetToDefaultInterpolationTypeInDataProperty(Action<string> progressCallback)
        //{
        //    if (progressCallback == null) throw new NullException(() => progressCallback);

        //    progressCallback(String.Format("Starting {0}...", MethodBase.GetCurrentMethod().Name));

        //    using (IContext context = PersistenceHelper.CreateContext())
        //    {
        //        RepositoryWrapper repositories = PersistenceHelper.CreateRepositoryWrapper(context);

        //        var patchManager = new PatchManager(new PatchRepositories(repositories));

        //        IList<Operator> operators = repositories.OperatorRepository
        //                                                .GetAll()
        //                                                .Where(x => x.GetOperatorTypeEnum() == OperatorTypeEnum.Resample)
        //                                                .ToArray();

        //        for (int i = 0; i < operators.Count; i++)
        //        {
        //            Operator op = operators[i];
        //            patchManager.Patch = op.Patch;

        //            var wrapper = new Resample_OperatorWrapper(op);
        //            wrapper.InterpolationType = ResampleInterpolationTypeEnum.CubicSmoothSlope;

        //            VoidResult result = patchManager.SaveOperator(op);

        //            ResultHelper.Assert(result);

        //            string progressMessage = String.Format("Migrated operator {0}/{1}.", i + 1, operators.Count);
        //            progressCallback(progressMessage);
        //        }

        //        AssertDocuments(repositories, progressCallback);

        //        context.Commit();
        //    }

        //    progressCallback(String.Format("{0} finished.", MethodBase.GetCurrentMethod().Name));
        //}

        //public static void Migrate_AggregateOperators_ParametersToInlets(Action<string> progressCallback)
        //{
        //    if (progressCallback == null) throw new NullException(() => progressCallback);

        //    progressCallback(String.Format("Starting {0}...", MethodBase.GetCurrentMethod().Name));

        //    using (IContext context = PersistenceHelper.CreateContext())
        //    {
        //        RepositoryWrapper repositories = PersistenceHelper.CreateRepositoryWrapper(context);

        //        var patchManager = new PatchManager(new PatchRepositories(repositories));

        //        IList<Operator> averageOperators = repositories.OperatorRepository
        //                                                       .GetAll()
        //                                                       .Where(x => x.GetOperatorTypeEnum() == OperatorTypeEnum.Average)
        //                                                       .ToArray();
        //        for (int i = 0; i < averageOperators.Count; i++)
        //        {
        //            Operator op = averageOperators[i];

        //            bool mustMigrate = op.Inlets.Count == 1;
        //            if (!mustMigrate)
        //            {
        //                throw new Exception("Operator already migrated!");
        //            }

        //            patchManager.Patch = op.Patch;

        //            // Create extra inlets.
        //            int newInletCount = 3;
        //            for (int inletIndex = 1; inletIndex < newInletCount; inletIndex++)
        //            {
        //                Inlet inlet = patchManager.CreateInlet(op);
        //                inlet.ListIndex = inletIndex;
        //            }

        //            // Convert parameter to inlets.
        //            var averageWrapper = new Average_OperatorWrapper(op);

        //            double timeSliceDurationValue = DataPropertyParser.GetDouble(op, PropertyNames.TimeSliceDuration);
        //            var timeSliceDurationNumberWrapper = patchManager.Number(timeSliceDurationValue);
        //            averageWrapper.TimeSliceDuration = timeSliceDurationNumberWrapper;

        //            int sampleCountValue = DataPropertyParser.GetInt32(op, PropertyNames.SampleCount);
        //            var sampleCountNumberWrapper = patchManager.Number(sampleCountValue);
        //            averageWrapper.SampleCount = sampleCountNumberWrapper;

        //            VoidResult result = patchManager.SaveOperator(op);

        //            ResultHelper.Assert(result);

        //            string progressMessage = String.Format("Step 1/4: Migrated Average {0}/{1}.", i + 1, averageOperators.Count);
        //            progressCallback(progressMessage);
        //        }

        //        IList<Operator> minimumOperators = repositories.OperatorRepository
        //                                                       .GetAll()
        //                                                       .Where(x => x.GetOperatorTypeEnum() == OperatorTypeEnum.Minimum)
        //                                                       .ToArray();
        //        for (int i = 0; i < minimumOperators.Count; i++)
        //        {
        //            Operator op = minimumOperators[i];

        //            bool mustMigrate = op.Inlets.Count == 1;
        //            if (!mustMigrate)
        //            {
        //                throw new Exception("Operator already migrated!");
        //            }

        //            patchManager.Patch = op.Patch;

        //            // Create extra inlets.
        //            int newInletCount = 3;
        //            for (int inletIndex = 1; inletIndex < newInletCount; inletIndex++)
        //            {
        //                Inlet inlet = patchManager.CreateInlet(op);
        //                inlet.ListIndex = inletIndex;
        //            }

        //            // Convert parameter to inlets.
        //            var minimumWrapper = new Minimum_OperatorWrapper(op);

        //            double timeSliceDurationValue = DataPropertyParser.GetDouble(op, PropertyNames.TimeSliceDuration);
        //            var timeSliceDurationNumberWrapper = patchManager.Number(timeSliceDurationValue);
        //            minimumWrapper.TimeSliceDuration = timeSliceDurationNumberWrapper;

        //            int sampleCountValue = DataPropertyParser.GetInt32(op, PropertyNames.SampleCount);
        //            var sampleCountNumberWrapper = patchManager.Number(sampleCountValue);
        //            minimumWrapper.SampleCount = sampleCountNumberWrapper;

        //            VoidResult result = patchManager.SaveOperator(op);

        //            ResultHelper.Assert(result);

        //            string progressMessage = String.Format("Step 2/4: Migrated Minimum Operator {0}/{1}.", i + 1, minimumOperators.Count);
        //            progressCallback(progressMessage);
        //        }

        //        IList<Operator> maximumOperators = repositories.OperatorRepository
        //                                                       .GetAll()
        //                                                       .Where(x => x.GetOperatorTypeEnum() == OperatorTypeEnum.Maximum)
        //                                                       .ToArray();
        //        for (int i = 0; i < maximumOperators.Count; i++)
        //        {
        //            Operator op = maximumOperators[i];

        //            bool mustMigrate = op.Inlets.Count == 1;
        //            if (!mustMigrate)
        //            {
        //                throw new Exception("Operator already migrated!");
        //            }

        //            patchManager.Patch = op.Patch;

        //            // Create extra inlets.
        //            int newInletCount = 3;
        //            for (int inletIndex = 1; inletIndex < newInletCount; inletIndex++)
        //            {
        //                Inlet inlet = patchManager.CreateInlet(op);
        //                inlet.ListIndex = inletIndex;
        //            }

        //            // Convert parameter to inlets.
        //            var maximumWrapper = new Maximum_OperatorWrapper(op);

        //            double timeSliceDurationValue = DataPropertyParser.GetDouble(op, PropertyNames.TimeSliceDuration);
        //            var timeSliceDurationNumberWrapper = patchManager.Number(timeSliceDurationValue);
        //            maximumWrapper.TimeSliceDuration = timeSliceDurationNumberWrapper;

        //            int sampleCountValue = DataPropertyParser.GetInt32(op, PropertyNames.SampleCount);
        //            var sampleCountNumberWrapper = patchManager.Number(sampleCountValue);
        //            maximumWrapper.SampleCount = sampleCountNumberWrapper;

        //            VoidResult result = patchManager.SaveOperator(op);

        //            ResultHelper.Assert(result);

        //            string progressMessage = String.Format("Step 3/4: Migrated Maximum Operator {0}/{1}.", i + 1, maximumOperators.Count);
        //            progressCallback(progressMessage);
        //        }

        //        AssertDocuments(repositories, progressCallback);

        //        context.Commit();
        //    }

        //    progressCallback(String.Format("{0} finished.", MethodBase.GetCurrentMethod().Name));
        //}

        //public static void Migrate_CacheOperators_ParametersToInlets(Action<string> progressCallback)
        //{
        //    if (progressCallback == null) throw new NullException(() => progressCallback);

        //    progressCallback(String.Format("Starting {0}...", MethodBase.GetCurrentMethod().Name));

        //    using (IContext context = PersistenceHelper.CreateContext())
        //    {
        //        RepositoryWrapper repositories = PersistenceHelper.CreateRepositoryWrapper(context);

        //        var patchManager = new PatchManager(new PatchRepositories(repositories));

        //        IList<Operator> operators = repositories.OperatorRepository
        //                                                       .GetAll()
        //                                                       .Where(x => x.GetOperatorTypeEnum() == OperatorTypeEnum.Cache)
        //                                                       .ToArray();
        //        for (int i = 0; i < operators.Count; i++)
        //        {
        //            Operator op = operators[i];

        //            bool mustMigrate = op.Inlets.Count == 1;
        //            if (!mustMigrate)
        //            {
        //                throw new Exception("Operator already migrated!");
        //            }

        //            patchManager.Patch = op.Patch;

        //            // Create extra inlets.
        //            int newInletCount = 4;
        //            for (int inletIndex = 1; inletIndex < newInletCount; inletIndex++)
        //            {
        //                Inlet inlet = patchManager.CreateInlet(op);
        //                inlet.ListIndex = inletIndex;
        //            }

        //            // Convert parameter to inlets.
        //            var wrapper = new Cache_OperatorWrapper(op);

        //            double startTimeValue = DataPropertyParser.GetDouble(op, PropertyNames.StartTime);
        //            var startTimeNumberWrapper = patchManager.Number(startTimeValue);
        //            wrapper.StartTime = startTimeNumberWrapper;

        //            double endTimeValue = DataPropertyParser.GetDouble(op, PropertyNames.EndTime);
        //            var endTimeNumberWrapper = patchManager.Number(endTimeValue);
        //            wrapper.EndTime = endTimeNumberWrapper;

        //            double samplingRateValue = DataPropertyParser.GetDouble(op, PropertyNames.SamplingRate);
        //            var samplingRateNumberWrapper = patchManager.Number(samplingRateValue);
        //            wrapper.SamplingRate = samplingRateNumberWrapper;

        //            DataPropertyParser.RemoveKey(op, PropertyNames.StartTime);
        //            DataPropertyParser.RemoveKey(op, PropertyNames.EndTime);
        //            DataPropertyParser.RemoveKey(op, PropertyNames.SamplingRate);

        //            VoidResult result = patchManager.SaveOperator(op);
        //            ResultHelper.Assert(result);

        //            string progressMessage = String.Format("Migrated Operator {0}/{1}.", i + 1, operators.Count);
        //            progressCallback(progressMessage);
        //        }

        //        AssertDocuments(repositories, progressCallback);

        //        context.Commit();
        //    }

        //    progressCallback(String.Format("{0} finished.", MethodBase.GetCurrentMethod().Name));
        //}

        //public static void Migrate_NumberOperators_FormatDataProperty(Action<string> progressCallback)
        //{
        //    if (progressCallback == null) throw new NullException(() => progressCallback);

        //    progressCallback(String.Format("Starting {0}...", MethodBase.GetCurrentMethod().Name));

        //    using (IContext context = PersistenceHelper.CreateContext())
        //    {
        //        RepositoryWrapper repositories = PersistenceHelper.CreateRepositoryWrapper(context);

        //        var patchManager = new PatchManager(new PatchRepositories(repositories));

        //        IList<Operator> operators = repositories.OperatorRepository
        //                                                .GetAll()
        //                                                .Where(x => x.GetOperatorTypeEnum() == OperatorTypeEnum.Number)
        //                                                .ToArray();
        //        for (int i = 0; i < operators.Count; i++)
        //        {
        //            Operator op = operators[i];

        //            double number;
        //            if (!DoubleHelper.TryParse(op.Data, DataPropertyParser.FormattingCulture, out number))
        //            {
        //                throw new Exception("op.Data cannot be parsed to Double. Operator already migrated?");
        //            }

        //            op.Data = null;

        //            var wrapper = new Number_OperatorWrapper(op);
        //            wrapper.Number = number;

        //            patchManager.Patch = op.Patch;
        //            VoidResult result = patchManager.SaveOperator(op);
        //            ResultHelper.Assert(result);

        //            string progressMessage = String.Format("Migrated Operator {0}/{1}.", i + 1, operators.Count);
        //            progressCallback(progressMessage);
        //        }

        //        AssertDocuments(repositories, progressCallback);

        //        context.Commit();
        //    }

        //    progressCallback(String.Format("{0} finished.", MethodBase.GetCurrentMethod().Name));
        //}

        //public static void Migrate_ReferenceOperators_FormatDataProperty(Action<string> progressCallback)
        //{
        //    if (progressCallback == null) throw new NullException(() => progressCallback);

        //    progressCallback(String.Format("Starting {0}...", MethodBase.GetCurrentMethod().Name));

        //    using (IContext context = PersistenceHelper.CreateContext())
        //    {
        //        RepositoryWrapper repositories = PersistenceHelper.CreateRepositoryWrapper(context);

        //        var patchManager = new PatchManager(new PatchRepositories(repositories));

        //        {
        //            IList<Operator> curveOperators = repositories.OperatorRepository
        //                                                         .GetAll()
        //                                                         .Where(x => x.GetOperatorTypeEnum() == OperatorTypeEnum.Curve)
        //                                                         .ToArray();
        //            for (int i = 0; i < curveOperators.Count; i++)
        //            {
        //                Operator op = curveOperators[i];

        //                int? curveID = null;
        //                if (!String.IsNullOrEmpty(op.Data))
        //                {
        //                    curveID = Int32.Parse(op.Data);
        //                }

        //                op.Data = null;

        //                var wrapper = new Curve_OperatorWrapper(op, repositories.CurveRepository);
        //                wrapper.CurveID = curveID;

        //                patchManager.Patch = op.Patch;
        //                VoidResult result = patchManager.SaveOperator(op);
        //                ResultHelper.Assert(result);

        //                string progressMessage = String.Format("Step 1: Migrated Curve Operator {0}/{1}.", i + 1, curveOperators.Count);
        //                progressCallback(progressMessage);
        //            }
        //        }

        //        {
        //            IList<Operator> sampleOperators = repositories.OperatorRepository
        //                                                          .GetAll()
        //                                                          .Where(x => x.GetOperatorTypeEnum() == OperatorTypeEnum.Sample)
        //                                                          .ToArray();
        //            for (int i = 0; i < sampleOperators.Count; i++)
        //            {
        //                Operator op = sampleOperators[i];

        //                int? sampleID = null;
        //                if (!String.IsNullOrEmpty(op.Data))
        //                {
        //                    sampleID = Int32.Parse(op.Data);
        //                }

        //                op.Data = null;

        //                var wrapper = new Sample_OperatorWrapper(op, repositories.SampleRepository);
        //                wrapper.SampleID = sampleID;

        //                patchManager.Patch = op.Patch;
        //                VoidResult result = patchManager.SaveOperator(op);
        //                ResultHelper.Assert(result);

        //                string progressMessage = String.Format("Step 2: Migrated Sample Operator {0}/{1}.", i + 1, sampleOperators.Count);
        //                progressCallback(progressMessage);
        //            }
        //        }

        //        {
        //            IList<Operator> customOperators = repositories.OperatorRepository
        //                                                           .GetAll()
        //                                                           .Where(x => x.GetOperatorTypeEnum() == OperatorTypeEnum.CustomOperator)
        //                                                           .ToArray();
        //            for (int i = 0; i < customOperators.Count; i++)
        //            {
        //                Operator op = customOperators[i];

        //                int? underlyingPatchID = null;
        //                if (!String.IsNullOrEmpty(op.Data))
        //                {
        //                    underlyingPatchID = Int32.Parse(op.Data);
        //                }

        //                op.Data = null;

        //                var wrapper = new CustomOperator_OperatorWrapper(op, repositories.PatchRepository);
        //                wrapper.UnderlyingPatchID = underlyingPatchID;

        //                patchManager.Patch = op.Patch;
        //                VoidResult result = patchManager.SaveOperator(op);
        //                ResultHelper.Assert(result);

        //                string progressMessage = String.Format("Step 3: Migrated Custom Operator {0}/{1}.", i + 1, customOperators.Count);
        //                progressCallback(progressMessage);
        //            }
        //        }

        //        AssertDocuments(repositories, progressCallback);

        //        context.Commit();
        //    }

        //    progressCallback(String.Format("{0} finished.", MethodBase.GetCurrentMethod().Name));
        //}

        //public static void Migrate_SpectrumOperators_ParametersToInlets(Action<string> progressCallback)
        //{
        //    if (progressCallback == null) throw new NullException(() => progressCallback);

        //    progressCallback(String.Format("Starting {0}...", MethodBase.GetCurrentMethod().Name));

        //    using (IContext context = PersistenceHelper.CreateContext())
        //    {
        //        RepositoryWrapper repositories = PersistenceHelper.CreateRepositoryWrapper(context);

        //        var patchManager = new PatchManager(new PatchRepositories(repositories));

        //        IList<Operator> operators = repositories.OperatorRepository
        //                                                .GetAll()
        //                                                .Where(x => x.GetOperatorTypeEnum() == OperatorTypeEnum.Spectrum)
        //                                                .ToArray();
        //        for (int i = 0; i < operators.Count; i++)
        //        {
        //            Operator op = operators[i];

        //            int newInletCount = 4;

        //            bool mustMigrate = op.Inlets.Count != newInletCount;
        //            if (!mustMigrate)
        //            {
        //                throw new Exception("op.Inlets.Count == 4. Operator already migrated?");
        //            }

        //            patchManager.Patch = op.Patch;

        //            // Create extra inlets.
        //            for (int inletIndex = 1; inletIndex < newInletCount; inletIndex++)
        //            {
        //                Inlet inlet = patchManager.CreateInlet(op);
        //                inlet.ListIndex = inletIndex;
        //            }

        //            // Convert parameter to inlets.
        //            var wrapper = new Spectrum_OperatorWrapper(op);

        //            double startTimeValue = DataPropertyParser.GetDouble(op, PropertyNames.StartTime);
        //            var startTimeNumberWrapper = patchManager.Number(startTimeValue);
        //            wrapper.StartTime = startTimeNumberWrapper;

        //            double endTimeValue = DataPropertyParser.GetDouble(op, PropertyNames.EndTime);
        //            var endTimeNumberWrapper = patchManager.Number(endTimeValue);
        //            wrapper.EndTime = endTimeNumberWrapper;

        //            double frequencyCountValue = DataPropertyParser.GetDouble(op, PropertyNames.FrequencyCount);
        //            var frequencyCountNumberWrapper = patchManager.Number(frequencyCountValue);
        //            wrapper.FrequencyCount = frequencyCountNumberWrapper;

        //            DataPropertyParser.RemoveKey(op, PropertyNames.StartTime);
        //            DataPropertyParser.RemoveKey(op, PropertyNames.EndTime);
        //            DataPropertyParser.RemoveKey(op, PropertyNames.FrequencyCount);

        //            VoidResult result = patchManager.SaveOperator(op);
        //            ResultHelper.Assert(result);

        //            string progressMessage = String.Format("Migrated Operator {0}/{1}.", i + 1, operators.Count);
        //            progressCallback(progressMessage);
        //        }

        //        AssertDocuments(repositories, progressCallback);

        //        //throw new Exception("Not committing yet, for debugging purposes.");
        //        context.Commit();
        //    }

        //    progressCallback(String.Format("{0} finished.", MethodBase.GetCurrentMethod().Name));
        //}

        //public static void Migrate_CurveOperators_SetDimensionParameter(Action<string> progressCallback)
        //{
        //    if (progressCallback == null) throw new NullException(() => progressCallback);

        //    progressCallback(String.Format("Starting {0}...", MethodBase.GetCurrentMethod().Name));

        //    using (IContext context = PersistenceHelper.CreateContext())
        //    {
        //        RepositoryWrapper repositories = PersistenceHelper.CreateRepositoryWrapper(context);

        //        var patchManager = new PatchManager(new PatchRepositories(repositories));

        //        IList<Operator> operators = repositories.OperatorRepository
        //                                                .GetAll()
        //                                                .Where(x => x.GetOperatorTypeEnum() == OperatorTypeEnum.Curve)
        //                                                .ToArray();
        //        for (int i = 0; i < operators.Count; i++)
        //        {
        //            Operator op = operators[i];
        //            var wrapper = new Curve_OperatorWrapper(op, repositories.CurveRepository);

        //            if (wrapper.Dimension != DimensionEnum.Undefined)
        //            {
        //                throw new Exception("wrapper.Dimension == DimensionEnum.Undefined. Operator already migrated?");
        //            }
        //            wrapper.Dimension = DimensionEnum.Time;

        //            patchManager.Patch = op.Patch;
        //            VoidResult result = patchManager.SaveOperator(op);
        //            ResultHelper.Assert(result);

        //            string progressMessage = String.Format("Migrated Operator {0}/{1}.", i + 1, operators.Count);
        //            progressCallback(progressMessage);
        //        }

        //        AssertDocuments(repositories, progressCallback);

        //        context.Commit();
        //    }

        //    progressCallback(String.Format("{0} finished.", MethodBase.GetCurrentMethod().Name));
        //}

        //public static void Migrate_StretchOperators_SetDimensionParameter(Action<string> progressCallback)
        //{
        //    if (progressCallback == null) throw new NullException(() => progressCallback);

        //    progressCallback(String.Format("Starting {0}...", MethodBase.GetCurrentMethod().Name));

        //    using (IContext context = PersistenceHelper.CreateContext())
        //    {
        //        RepositoryWrapper repositories = PersistenceHelper.CreateRepositoryWrapper(context);

        //        var patchManager = new PatchManager(new PatchRepositories(repositories));

        //        IList<Operator> operators = repositories.OperatorRepository
        //                                                .GetAll()
        //                                                .Where(x => x.GetOperatorTypeEnum() == OperatorTypeEnum.Stretch)
        //                                                .ToArray();
        //        for (int i = 0; i < operators.Count; i++)
        //        {
        //            Operator op = operators[i];
        //            var wrapper = new Stretch_OperatorWrapper(op);

        //            if (wrapper.Dimension != DimensionEnum.Undefined)
        //            {
        //                throw new Exception("wrapper.Dimension == DimensionEnum.Undefined. Operator already migrated?");
        //            }
        //            wrapper.Dimension = DimensionEnum.Time;

        //            patchManager.Patch = op.Patch;
        //            VoidResult result = patchManager.SaveOperator(op);
        //            ResultHelper.Assert(result);

        //            string progressMessage = String.Format("Migrated Operator {0}/{1}.", i + 1, operators.Count);
        //            progressCallback(progressMessage);
        //        }

        //        AssertDocuments(repositories, progressCallback);

        //        //throw new Exception("Temporarily not committing, for debugging purposes.");
        //        context.Commit();
        //    }

        //    progressCallback(String.Format("{0} finished.", MethodBase.GetCurrentMethod().Name));
        //}

        //public static void Migrate_SelectOperators_SetDimensionParameter(Action<string> progressCallback)
        //{
        //    if (progressCallback == null) throw new NullException(() => progressCallback);

        //    progressCallback(String.Format("Starting {0}...", MethodBase.GetCurrentMethod().Name));

        //    using (IContext context = PersistenceHelper.CreateContext())
        //    {
        //        RepositoryWrapper repositories = PersistenceHelper.CreateRepositoryWrapper(context);

        //        var patchManager = new PatchManager(new PatchRepositories(repositories));

        //        IList<Operator> operators = repositories.OperatorRepository
        //                                                .GetAll()
        //                                                .Where(x => x.GetOperatorTypeEnum() == OperatorTypeEnum.Select)
        //                                                .ToArray();
        //        for (int i = 0; i < operators.Count; i++)
        //        {
        //            Operator op = operators[i];
        //            var wrapper = new Select_OperatorWrapper(op);

        //            if (wrapper.Dimension != DimensionEnum.Undefined)
        //            {
        //                throw new Exception("wrapper.Dimension == DimensionEnum.Undefined. Operator already migrated?");
        //            }
        //            wrapper.Dimension = DimensionEnum.Time;

        //            patchManager.Patch = op.Patch;
        //            VoidResult result = patchManager.SaveOperator(op);
        //            ResultHelper.Assert(result);

        //            string progressMessage = String.Format("Migrated Operator {0}/{1}.", i + 1, operators.Count);
        //            progressCallback(progressMessage);
        //        }

        //        AssertDocuments(repositories, progressCallback);

        //        //throw new Exception("Temporarily not committing, for debugging purposes.");
        //        context.Commit();
        //    }

        //    progressCallback(String.Format("{0} finished.", MethodBase.GetCurrentMethod().Name));
        //}

        //public static void Migrate_SetDimensionParameter_ForManyOperatorTypes(Action<string> progressCallback)
        //{
        //    if (progressCallback == null) throw new NullException(() => progressCallback);

        //    progressCallback(String.Format("Starting {0}...", MethodBase.GetCurrentMethod().Name));

        //    var operatorTypeEnumHashSet = new HashSet<OperatorTypeEnum>
        //    {
        //        OperatorTypeEnum.Sine,
        //        OperatorTypeEnum.Sample,
        //        OperatorTypeEnum.Noise,
        //        OperatorTypeEnum.SawUp,
        //        OperatorTypeEnum.Square,
        //        OperatorTypeEnum.Triangle,
        //        OperatorTypeEnum.Pulse,
        //        OperatorTypeEnum.SawDown,
        //        OperatorTypeEnum.Delay,
        //        OperatorTypeEnum.SpeedUp,
        //        OperatorTypeEnum.SlowDown,
        //        OperatorTypeEnum.TimePower,
        //        OperatorTypeEnum.Earlier,
        //        OperatorTypeEnum.Squash,
        //        OperatorTypeEnum.Shift,
        //        OperatorTypeEnum.Loop,
        //        OperatorTypeEnum.Reverse,
        //        OperatorTypeEnum.Resample,
        //        OperatorTypeEnum.Random,
        //        OperatorTypeEnum.MinFollower,
        //        OperatorTypeEnum.MaxFollower,
        //        OperatorTypeEnum.AverageFollower,
        //        OperatorTypeEnum.Cache
        //    };

        //    using (IContext context = PersistenceHelper.CreateContext())
        //    {
        //        RepositoryWrapper repositories = PersistenceHelper.CreateRepositoryWrapper(context);

        //        var patchManager = new PatchManager(new PatchRepositories(repositories));

        //        IList<Operator> operators = repositories.OperatorRepository
        //                                                .GetAll().ToArray();

        //        for (int i = 0; i < operators.Count; i++)
        //        {
        //            Operator op = operators[i];
        //            var wrapper = new Select_OperatorWrapper(op);

        //            OperatorTypeEnum operatorTypeEnum = op.GetOperatorTypeEnum();
        //            if (!operatorTypeEnumHashSet.Contains(operatorTypeEnum))
        //            {
        //                continue;
        //            }

        //            if (wrapper.Dimension != DimensionEnum.Undefined)
        //            {
        //                throw new Exception("wrapper.Dimension == DimensionEnum.Undefined. Operator already migrated?");
        //            }
        //            wrapper.Dimension = DimensionEnum.Time;

        //            patchManager.Patch = op.Patch;
        //            VoidResult result = patchManager.SaveOperator(op);
        //            ResultHelper.Assert(result);

        //            string progressMessage = String.Format("Migrated Operator {0}/{1}.", i + 1, operators.Count);
        //            progressCallback(progressMessage);
        //        }

        //        AssertDocuments(repositories, progressCallback);

        //        //throw new Exception("Temporarily not committing, for debugging purposes.");
        //        context.Commit();
        //    }

        //    progressCallback(String.Format("{0} finished.", MethodBase.GetCurrentMethod().Name));
        //}

        //public static void Migrate_Document_CreateAudioOutput(Action<string> progressCallback)
        //{
        //    if (progressCallback == null) throw new NullException(() => progressCallback);

        //    progressCallback(String.Format("Starting {0}...", MethodBase.GetCurrentMethod().Name));

        //    using (IContext context = PersistenceHelper.CreateContext())
        //    {
        //        RepositoryWrapper repositories = PersistenceHelper.CreateRepositoryWrapper(context);
        //        var audioOutputManager = new AudioOutputManager(
        //            repositories.AudioOutputRepository,
        //            repositories.SpeakerSetupRepository,
        //            repositories.IDRepository);

        //        IList<Document> rootDocuments = repositories.DocumentRepository.GetAll().Where(x => x.ParentDocument == null).ToArray();
        //        for (int i = 0; i < rootDocuments.Count; i++)
        //        {
        //            Document rootDocument = rootDocuments[i];

        //            AudioOutput audioOutput = audioOutputManager.Create(rootDocument);

        //            string progressMessage = String.Format("Migrated Document {0}/{1}.", i + 1, rootDocuments.Count);
        //            progressCallback(progressMessage);
        //        }

        //        AssertDocuments(repositories, progressCallback);

        //        //throw new Exception("Temporarily not committing, for debugging purposes.");
        //        context.Commit();
        //    }

        //    progressCallback(String.Format("{0} finished.", MethodBase.GetCurrentMethod().Name));
        //}

        //public static void Migrate_BundleAndUnbundleOperators_SetInletAndOutletListIndexes_AndDimensionParameter(Action<string> progressCallback)
        //{
        //    if (progressCallback == null) throw new NullException(() => progressCallback);

        //    progressCallback(String.Format("Starting {0}...", MethodBase.GetCurrentMethod().Name));

        //    using (IContext context = PersistenceHelper.CreateContext())
        //    {
        //        RepositoryWrapper repositories = PersistenceHelper.CreateRepositoryWrapper(context);

        //        var patchManager = new PatchManager(new PatchRepositories(repositories));

        //        IList<Operator> operators = repositories.OperatorRepository
        //                                                .GetAll()
        //                                                .Where(x => x.GetOperatorTypeEnum() == OperatorTypeEnum.Bundle ||
        //                                                            x.GetOperatorTypeEnum() == OperatorTypeEnum.Unbundle)
        //                                                .ToArray();
        //        for (int i = 0; i < operators.Count; i++)
        //        {
        //            Operator op = operators[i];
        //            for (int j = 0; j < op.Outlets.Count; j++)
        //            {
        //                Outlet outlet = op.Outlets[j];
        //                outlet.ListIndex = j;
        //            }

        //            for (int j = 0; j < op.Inlets.Count; j++)
        //            {
        //                Inlet inlet = op.Inlets[j];
        //                inlet.ListIndex = j;
        //            }

        //            var wrapper = new GetDimension_OperatorWrapper(op);
        //            // This is the strangest if,
        //            // but this adds the Dimension key to the operator's data property,
        //            // without removing the original Dimension property.
        //            if (wrapper.Dimension == DimensionEnum.Undefined)
        //            {
        //                wrapper.Dimension = DimensionEnum.Undefined;
        //            }

        //            patchManager.Patch = op.Patch;
        //            VoidResult result = patchManager.SaveOperator(op);
        //            ResultHelper.Assert(result);

        //            string progressMessage = String.Format("Migrated Operator {0}/{1}.", i + 1, operators.Count);
        //            progressCallback(progressMessage);
        //        }

        //        AssertDocuments(repositories, progressCallback);

        //        //throw new Exception("Temporarily not committing, for debugging purposes.");
        //        context.Commit();
        //    }

        //    progressCallback(String.Format("{0} finished.", MethodBase.GetCurrentMethod().Name));
        //}

        //public static void Migrate_SetRecalculateParameter_ForAggregatesOverDimension(Action<string> progressCallback)
        //{
        //    if (progressCallback == null) throw new NullException(() => progressCallback);

        //    progressCallback(String.Format("Starting {0}...", MethodBase.GetCurrentMethod().Name));

        //    var operatorTypeEnumHashSet = new HashSet<OperatorTypeEnum>
        //    {
        //        OperatorTypeEnum.AverageOverDimension,
        //        OperatorTypeEnum.MaxOverDimension,
        //        OperatorTypeEnum.MinOverDimension,
        //        OperatorTypeEnum.SumOverDimension
        //    };

        //    using (IContext context = PersistenceHelper.CreateContext())
        //    {
        //        RepositoryWrapper repositories = PersistenceHelper.CreateRepositoryWrapper(context);

        //        var patchManager = new PatchManager(new PatchRepositories(repositories));

        //        IList<Operator> operators = repositories.OperatorRepository
        //                                                .GetAll().ToArray();

        //        for (int i = 0; i < operators.Count; i++)
        //        {
        //            Operator op = operators[i];
        //            var wrapper = new MaxOverDimension_OperatorWrapper(op);

        //            OperatorTypeEnum operatorTypeEnum = op.GetOperatorTypeEnum();
        //            if (!operatorTypeEnumHashSet.Contains(operatorTypeEnum))
        //            {
        //                continue;
        //            }

        //            if (wrapper.CollectionRecalculation != CollectionRecalculationEnum.Undefined)
        //            {
        //                throw new Exception("wrapper.Dimension == DimensionEnum.Undefined. Operator already migrated?");
        //            }
        //            wrapper.CollectionRecalculation = CollectionRecalculationEnum.Continuous;

        //            patchManager.Patch = op.Patch;
        //            VoidResult result = patchManager.SaveOperator(op);
        //            ResultHelper.Assert(result);

        //            string progressMessage = String.Format("Migrated Operator {0}/{1}.", i + 1, operators.Count);
        //            progressCallback(progressMessage);
        //        }

        //        AssertDocuments(repositories, progressCallback);

        //        context.Commit();
        //    }

        //    progressCallback(String.Format("{0} finished.", MethodBase.GetCurrentMethod().Name));
        //}

        //public static void Migrate_RenameContinualToContinuous(Action<string> progressCallback)
        //{
        //    if (progressCallback == null) throw new NullException(() => progressCallback);

        //    progressCallback(String.Format("Starting {0}...", MethodBase.GetCurrentMethod().Name));

        //    var operatorTypeEnumHashSet = new HashSet<OperatorTypeEnum>
        //    {
        //        OperatorTypeEnum.AverageOverDimension,
        //        OperatorTypeEnum.MaxOverDimension,
        //        OperatorTypeEnum.MinOverDimension,
        //        OperatorTypeEnum.SumOverDimension
        //    };

        //    using (IContext context = PersistenceHelper.CreateContext())
        //    {
        //        RepositoryWrapper repositories = PersistenceHelper.CreateRepositoryWrapper(context);

        //        var patchManager = new PatchManager(new PatchRepositories(repositories));

        //        IList<Operator> operators = repositories.OperatorRepository
        //                                                .GetAll().ToArray();

        //        for (int i = 0; i < operators.Count; i++)
        //        {
        //            Operator op = operators[i];

        //            OperatorTypeEnum operatorTypeEnum = op.GetOperatorTypeEnum();
        //            if (!operatorTypeEnumHashSet.Contains(operatorTypeEnum))
        //            {
        //                continue;
        //            }

        //            string value = DataPropertyParser.TryGetString(op, PropertyNames.Recalculation);
        //            if (String.Equals(value, PropertyNames.Continual))
        //            {
        //                DataPropertyParser.SetValue(op, PropertyNames.Recalculation, PropertyNames.Continuous);
        //            }

        //            patchManager.Patch = op.Patch;
        //            VoidResult result = patchManager.SaveOperator(op);
        //            ResultHelper.Assert(result);

        //            string progressMessage = String.Format("Migrated Operator {0}/{1}.", i + 1, operators.Count);
        //            progressCallback(progressMessage);
        //        }

        //        AssertDocuments(repositories, progressCallback);

        //        context.Commit();
        //    }

        //    progressCallback(String.Format("{0} finished.", MethodBase.GetCurrentMethod().Name));
        //}

        //public static void Migrate_RenameRecalculation_To_CollectionRecalculation(Action<string> progressCallback)
        //{
        //    if (progressCallback == null) throw new NullException(() => progressCallback);

        //    progressCallback(String.Format("Starting {0}...", MethodBase.GetCurrentMethod().Name));

        //    var operatorTypeEnumHashSet = new HashSet<OperatorTypeEnum>
        //    {
        //        OperatorTypeEnum.AverageOverDimension,
        //        OperatorTypeEnum.ClosestOverDimension,
        //        OperatorTypeEnum.MaxOverDimension,
        //        OperatorTypeEnum.MinOverDimension,
        //        OperatorTypeEnum.SumOverDimension
        //    };

        //    using (IContext context = PersistenceHelper.CreateContext())
        //    {
        //        RepositoryWrapper repositories = PersistenceHelper.CreateRepositoryWrapper(context);

        //        var patchManager = new PatchManager(new PatchRepositories(repositories));

        //        IList<Operator> operators = repositories.OperatorRepository
        //                                                .GetAll().ToArray();

        //        for (int i = 0; i < operators.Count; i++)
        //        {
        //            Operator op = operators[i];

        //            OperatorTypeEnum operatorTypeEnum = op.GetOperatorTypeEnum();
        //            if (!operatorTypeEnumHashSet.Contains(operatorTypeEnum))
        //            {
        //                continue;
        //            }

        //            string value = DataPropertyParser.TryGetString(op, PropertyNames.Recalculation);
        //            DataPropertyParser.SetValue(op, PropertyNames.CollectionRecalculation, value);
        //            DataPropertyParser.RemoveKey(op, PropertyNames.Recalculation);

        //            patchManager.Patch = op.Patch;
        //            VoidResult result = patchManager.SaveOperator(op);
        //            ResultHelper.Assert(result);

        //            string progressMessage = String.Format("Migrated Operator {0}/{1}.", i + 1, operators.Count);
        //            progressCallback(progressMessage);
        //        }

        //        AssertDocuments(repositories, progressCallback);

        //        //throw new Exception("Temporarily do not commit, for debugging.");

        //        context.Commit();
        //    }

        //    progressCallback(String.Format("{0} finished.", MethodBase.GetCurrentMethod().Name));
        //}

        //public static void Migrate_MultiplyWithOrigin_ToMultiply(Action<string> progressCallback)
        //{
        //    if (progressCallback == null) throw new NullException(() => progressCallback);

        //    progressCallback(String.Format("Starting {0}...", MethodBase.GetCurrentMethod().Name));

        //    using (IContext context = PersistenceHelper.CreateContext())
        //    {
        //        RepositoryWrapper repositories = PersistenceHelper.CreateRepositoryWrapper(context);

        //        var patchManager = new PatchManager(new PatchRepositories(repositories));

        //        IList<Operator> operators = repositories.OperatorRepository
        //                                                .GetAll()
        //                                                .Where(x => x.GetOperatorTypeEnum() == OperatorTypeEnum.MultiplyWithOrigin)
        //                                                .ToArray();

        //        for (int i = 0; i < operators.Count; i++)
        //        {
        //            Operator op = operators[i];
        //            var wrapper = new MultiplyWithOrigin_OperatorWrapper(op);

        //            if (wrapper.Origin != null)
        //            {
        //                continue;
        //            }

        //            op.SetOperatorTypeEnum(OperatorTypeEnum.Multiply, repositories.OperatorTypeRepository);

        //            Inlet originInlet = op.Inlets
        //                                  .OrderBy(x => x.ListIndex)
        //                                  .ElementAt(2);

        //            patchManager.DeleteInlet(originInlet);

        //            patchManager.Patch = op.Patch;
        //            VoidResult result = patchManager.SaveOperator(op);
        //            ResultHelper.Assert(result);

        //            string progressMessage = String.Format("Migrated Operator {0}/{1}.", i + 1, operators.Count);
        //            progressCallback(progressMessage);
        //        }

        //        AssertDocuments(repositories, progressCallback);

        //        context.Commit();
        //    }

        //    progressCallback(String.Format("{0} finished.", MethodBase.GetCurrentMethod().Name));
        //}

        //public static void Migrate_ResetOperators_AddListIndexDataKey(Action<string> progressCallback)
        //{
        //    if (progressCallback == null) throw new NullException(() => progressCallback);

        //    progressCallback(String.Format("Starting {0}...", MethodBase.GetCurrentMethod().Name));

        //    using (IContext context = PersistenceHelper.CreateContext())
        //    {
        //        RepositoryWrapper repositories = PersistenceHelper.CreateRepositoryWrapper(context);

        //        var patchManager = new PatchManager(new PatchRepositories(repositories));

        //        IList<Operator> operators = repositories.OperatorRepository.GetManyByOperatorTypeID((int)OperatorTypeEnum.Reset);

        //        for (int i = 0; i < operators.Count; i++)
        //        {
        //            Operator op = operators[i];
        //            var wrapper = new Reset_OperatorWrapper(op);

        //            if (wrapper.ListIndex.HasValue)
        //            {
        //                continue;
        //            }

        //            // Adds data key if it is not present yet.
        //            wrapper.ListIndex = null;

        //            patchManager.Patch = op.Patch;
        //            VoidResult result = patchManager.SaveOperator(op);
        //            ResultHelper.Assert(result);

        //            string progressMessage = String.Format("Migrated Operator {0}/{1}.", i + 1, operators.Count);
        //            progressCallback(progressMessage);
        //        }

        //        AssertDocuments(repositories, progressCallback);

        //        //throw new Exception("Temporarily do not commit, for debugging.");
        //        context.Commit();
        //    }

        //    progressCallback(String.Format("{0} finished.", MethodBase.GetCurrentMethod().Name));
        //}

        //public static void Migrate_LowPassFilter_HighPassFilter_AddBandWidthInlet(Action<string> progressCallback)
        //{
        //    if (progressCallback == null) throw new NullException(() => progressCallback);

        //    progressCallback(String.Format("Starting {0}...", MethodBase.GetCurrentMethod().Name));

        //    using (IContext context = PersistenceHelper.CreateContext())
        //    {
        //        RepositoryWrapper repositories = PersistenceHelper.CreateRepositoryWrapper(context);

        //        var patchManager = new PatchManager(new PatchRepositories(repositories));

        //        IList<Operator> operators1 = repositories.OperatorRepository.GetManyByOperatorTypeID((int)OperatorTypeEnum.HighPassFilter);
        //        IList<Operator> operators2 = repositories.OperatorRepository.GetManyByOperatorTypeID((int)OperatorTypeEnum.LowPassFilter);
        //        IList<Operator> operators = Enumerable.Union(operators1, operators2).ToArray();

        //        for (int i = 0; i < operators.Count; i++)
        //        {
        //            Operator op = operators[i];

        //            switch (op.Inlets.Count)
        //            {
        //                case 3:
        //                    continue;

        //                case 2:
        //                    break;

        //                default:
        //                    throw new EqualException(() => op.Inlets.Count, op.Inlets.Count);
        //            }

        //            Inlet bandWidthInlet = patchManager.CreateInlet(op);
        //            bandWidthInlet.ListIndex = 2;
        //            bandWidthInlet.DefaultValue = 1.0;

        //            patchManager.Patch = op.Patch;
        //            VoidResult result = patchManager.SaveOperator(op);
        //            ResultHelper.Assert(result);

        //            string progressMessage = String.Format("Migrated Operator {0}/{1}.", i + 1, operators.Count);
        //            progressCallback(progressMessage);
        //        }

        //        AssertDocuments(repositories, progressCallback);

        //        context.Commit();
        //    }

        //    progressCallback(String.Format("{0} finished.", MethodBase.GetCurrentMethod().Name));
        //}

        //public static void Migrate_Operator_SpeedUp_ToSquash(Action<string> progressCallback)
        //{
        //    if (progressCallback == null) throw new NullException(() => progressCallback);

        //    progressCallback(String.Format("Starting {0}...", MethodBase.GetCurrentMethod().Name));

        //    using (IContext context = PersistenceHelper.CreateContext())
        //    {
        //        RepositoryWrapper repositories = PersistenceHelper.CreateRepositoryWrapper(context);

        //        var patchManager = new PatchManager(new PatchRepositories(repositories));

        //        IList<Operator> operators = repositories.OperatorRepository
        //                                                .GetAll()
        //                                                .Where(x => x.GetOperatorTypeEnum() == OperatorTypeEnum.SpeedUp)
        //                                                .ToArray();

        //        for (int i = 0; i < operators.Count; i++)
        //        {
        //            Operator op = operators[i];

        //            op.SetOperatorTypeEnum(OperatorTypeEnum.Squash, repositories.OperatorTypeRepository);

        //            Inlet originInlet = patchManager.CreateInlet(op);
        //            originInlet.ListIndex = 2;

        //            patchManager.Patch = op.Patch;
        //            VoidResult result = patchManager.SaveOperator(op);
        //            ResultHelper.Assert(result);

        //            string progressMessage = String.Format("Migrated Operator {0}/{1}.", i + 1, operators.Count);
        //            progressCallback(progressMessage);
        //        }

        //        AssertDocuments(repositories, progressCallback);

        //        //throw new Exception("Temporarily not committing, for debugging.");
        //        context.Commit();
        //    }

        //    progressCallback(String.Format("{0} finished.", MethodBase.GetCurrentMethod().Name));
        //}

        //public static void Migrate_Operator_SlowDown_ToStretch(Action<string> progressCallback)
        //{
        //    if (progressCallback == null) throw new NullException(() => progressCallback);

        //    progressCallback(String.Format("Starting {0}...", MethodBase.GetCurrentMethod().Name));

        //    using (IContext context = PersistenceHelper.CreateContext())
        //    {
        //        RepositoryWrapper repositories = PersistenceHelper.CreateRepositoryWrapper(context);

        //        var patchManager = new PatchManager(new PatchRepositories(repositories));

        //        IList<Operator> operators = repositories.OperatorRepository
        //                                                .GetAll()
        //                                                .Where(x => x.GetOperatorTypeEnum() == OperatorTypeEnum.SlowDown)
        //                                                .ToArray();

        //        for (int i = 0; i < operators.Count; i++)
        //        {
        //            Operator op = operators[i];

        //            op.SetOperatorTypeEnum(OperatorTypeEnum.Stretch, repositories.OperatorTypeRepository);

        //            Inlet originInlet = patchManager.CreateInlet(op);
        //            originInlet.ListIndex = 2;

        //            patchManager.Patch = op.Patch;
        //            VoidResult result = patchManager.SaveOperator(op);
        //            ResultHelper.Assert(result);

        //            string progressMessage = String.Format("Migrated Operator {0}/{1}.", i + 1, operators.Count);
        //            progressCallback(progressMessage);
        //        }

        //        AssertDocuments(repositories, progressCallback);

        //        //throw new Exception("Temporarily not committing, for debugging.");
        //        context.Commit();
        //    }

        //    progressCallback(String.Format("{0} finished.", MethodBase.GetCurrentMethod().Name));
        //}

        //public static void Migrate_Operator_Earlier_ToShiftAndNegative(Action<string> progressCallback)
        //{
        //    if (progressCallback == null) throw new NullException(() => progressCallback);

        //    progressCallback(String.Format("Starting {0}...", MethodBase.GetCurrentMethod().Name));

        //    using (IContext context = PersistenceHelper.CreateContext())
        //    {
        //        RepositoryWrapper repositories = PersistenceHelper.CreateRepositoryWrapper(context);

        //        var x = new PatchManager(new PatchRepositories(repositories));

        //        IList<Operator> operators = repositories.OperatorRepository.GetManyByOperatorTypeID((int)OperatorTypeEnum.Earlier);

        //        for (int i = 0; i < operators.Count; i++)
        //        {
        //            Operator op = operators[i];

        //            x.Patch = op.Patch;

        //            var earlier = new Earlier_OperatorWrapper(op);

        //            // Insert number when TimeDifference not filled in.
        //            if (earlier.TimeDifference == null)
        //            {
        //                double defaultValue = earlier.TimeDifferenceInlet.DefaultValue ?? 0;
        //                earlier.TimeDifference = x.Number(defaultValue);
        //            }

        //            // Negate the time difference
        //            var negative = x.Negative(earlier.TimeDifference);
        //            earlier.TimeDifference = negative;

        //            // Change OperatorType
        //            op.SetOperatorTypeEnum(OperatorTypeEnum.Shift, repositories.OperatorTypeRepository);

        //            VoidResult result = x.SavePatch();
        //            ResultHelper.Assert(result);

        //            string progressMessage = String.Format("Migrated Operator {0}/{1}.", i + 1, operators.Count);
        //            progressCallback(progressMessage);
        //        }

        //        AssertDocuments(repositories, progressCallback);

        //        //throw new Exception("Temporarily not committing, for debugging.");
        //        context.Commit();
        //    }

        //    progressCallback(String.Format("{0} finished.", MethodBase.GetCurrentMethod().Name));
        //}

        //public static void Migrate_Operator_Delay_To_Shift(Action<string> progressCallback)
        //{
        //    if (progressCallback == null) throw new NullException(() => progressCallback);

        //    progressCallback(String.Format("Starting {0}...", MethodBase.GetCurrentMethod().Name));

        //    using (IContext context = PersistenceHelper.CreateContext())
        //    {
        //        RepositoryWrapper repositories = PersistenceHelper.CreateRepositoryWrapper(context);

        //        var patchManager = new PatchManager(new PatchRepositories(repositories));

        //        IList<Operator> operators = repositories.OperatorRepository.GetManyByOperatorTypeID((int)OperatorTypeEnum.Delay);

        //        for (int i = 0; i < operators.Count; i++)
        //        {
        //            Operator op = operators[i];

        //            op.SetOperatorTypeEnum(OperatorTypeEnum.Shift, repositories.OperatorTypeRepository);

        //            patchManager.Patch = op.Patch;
        //            VoidResult result = patchManager.SavePatch();
        //            ResultHelper.Assert(result);

        //            string progressMessage = String.Format("Migrated Operator {0}/{1}.", i + 1, operators.Count);
        //            progressCallback(progressMessage);
        //        }

        //        AssertDocuments(repositories, progressCallback);

        //        //throw new Exception("Temporarily not committing, for debugging.");
        //        context.Commit();
        //    }

        //    progressCallback(String.Format("{0} finished.", MethodBase.GetCurrentMethod().Name));
        //}

        //public static void Migrate_Operator_Spectrum_AddDimensionDataKey(Action<string> progressCallback)
        //{
        //    if (progressCallback == null) throw new NullException(() => progressCallback);

        //    progressCallback(String.Format("Starting {0}...", MethodBase.GetCurrentMethod().Name));

        //    using (IContext context = PersistenceHelper.CreateContext())
        //    {
        //        RepositoryWrapper repositories = PersistenceHelper.CreateRepositoryWrapper(context);

        //        var patchManager = new PatchManager(new PatchRepositories(repositories));

        //        IList<Operator> operators = repositories.OperatorRepository.GetManyByOperatorTypeID((int)OperatorTypeEnum.Spectrum);

        //        for (int i = 0; i < operators.Count; i++)
        //        {
        //            Operator op = operators[i];

        //            if (!String.IsNullOrEmpty(op.Data))
        //            {
        //                continue;
        //            }

        //            var wrapper = new Spectrum_OperatorWrapper(op);
        //            wrapper.Dimension = DimensionEnum.Time;

        //            patchManager.Patch = op.Patch;
        //            VoidResult result = patchManager.SaveOperator(op);
        //            ResultHelper.Assert(result);

        //            string progressMessage = String.Format("Migrated Operator {0}/{1}.", i + 1, operators.Count);
        //            progressCallback(progressMessage);
        //        }

        //        AssertDocuments(repositories, progressCallback);

        //        //throw new Exception("Temporarily not committing, for debugging.");
        //        context.Commit();
        //    }

        //    progressCallback(String.Format("{0} finished.", MethodBase.GetCurrentMethod().Name));
        //}

        //public static void Migrate_OperatorData_RenameDimensionHarmonicNumberToHarmonic(Action<string> progressCallback)
        //{
        //    if (progressCallback == null) throw new NullException(() => progressCallback);

        //    progressCallback(String.Format("Starting {0}...", MethodBase.GetCurrentMethod().Name));

        //    using (IContext context = PersistenceHelper.CreateContext())
        //    {
        //        RepositoryWrapper repositories = PersistenceHelper.CreateRepositoryWrapper(context);

        //        var patchManager = new PatchManager(new PatchRepositories(repositories));

        //        IList<Operator> operators = repositories.OperatorRepository.GetAll();

        //        for (int i = 0; i < operators.Count; i++)
        //        {
        //            Operator op = operators[i];

        //            string dimension = DataPropertyParser.TryGetString(op, PropertyNames.Dimension);

        //            if (String.Equals(dimension, PropertyNames.HarmonicNumber))
        //            {
        //                DataPropertyParser.SetValue(op, PropertyNames.Dimension, DimensionEnum.Harmonic);
        //            }

        //            // Cannot validate the operator, because it will do a recursive validation,
        //            // validating not yet migrated operators.

        //            string progressMessage = String.Format("Migrated Operator {0}/{1}.", i + 1, operators.Count);
        //            progressCallback(progressMessage);
        //        }

        //        //AssertDocuments(repositories, progressCallback);

        //        //throw new Exception("Temporarily not committing, for debugging.");
        //        context.Commit();
        //    }

        //    progressCallback(String.Format("{0} finished.", MethodBase.GetCurrentMethod().Name));
        //}

        //public static void Migrate_OperatorDimension_FromDataProperty_ToEntityReference(Action<string> progressCallback)
        //{
        //    if (progressCallback == null) throw new NullException(() => progressCallback);

        //    progressCallback(String.Format("Starting {0}...", MethodBase.GetCurrentMethod().Name));

        //    using (IContext context = PersistenceHelper.CreateContext())
        //    {
        //        RepositoryWrapper repositories = PersistenceHelper.CreateRepositoryWrapper(context);

        //        var patchManager = new PatchManager(new PatchRepositories(repositories));

        //        IList<Operator> operators = repositories.OperatorRepository.GetAll();

        //        for (int i = 0; i < operators.Count; i++)
        //        {
        //            Operator op = operators[i];

        //            DimensionEnum dimensionEnum = DataPropertyParser.GetEnum<DimensionEnum>(op, PropertyNames.Dimension);
        //            op.SetStandardDimensionEnum(dimensionEnum, repositories.DimensionRepository);
        //            DataPropertyParser.RemoveKey(op, PropertyNames.Dimension);

        //            // Cannot validate the operator, because it will do a recursive validation,
        //            // validating not yet migrated operators.

        //            string progressMessage = String.Format("Migrated Operator {0}/{1}.", i + 1, operators.Count);
        //            progressCallback(progressMessage);
        //        }

        //        AssertDocuments(repositories, progressCallback);

        //        //throw new Exception("Temporarily not committing, for debugging.");
        //        context.Commit();
        //    }

        //    progressCallback(String.Format("{0} finished.", MethodBase.GetCurrentMethod().Name));
        //}

        //public static void Migrate_SetDimension_OfInletsAndOutlets_OfStandardOperators(Action<string> progressCallback)
        //{
        //    if (progressCallback == null) throw new NullException(() => progressCallback);

        //    progressCallback(String.Format("Starting {0}...", MethodBase.GetCurrentMethod().Name));

        //    using (IContext context = PersistenceHelper.CreateContext())
        //    {
        //        var inletTuples = new List<InletOrOutletDimensionTuple>();
        //        var outletTuples = new List<InletOrOutletDimensionTuple>();

        //        inletTuples.Add(new InletOrOutletDimensionTuple(OperatorTypeEnum.AllPassFilter, 0, DimensionEnum.Signal));
        //        outletTuples.Add(new InletOrOutletDimensionTuple(OperatorTypeEnum.AllPassFilter, 0, DimensionEnum.Signal));

        //        inletTuples.Add(new InletOrOutletDimensionTuple(OperatorTypeEnum.AverageFollower, 0, DimensionEnum.Signal));

        //        inletTuples.Add(new InletOrOutletDimensionTuple(OperatorTypeEnum.AverageOverDimension, 0, DimensionEnum.Signal));

        //        inletTuples.Add(new InletOrOutletDimensionTuple(OperatorTypeEnum.BandPassFilterConstantPeakGain, 0, DimensionEnum.Signal));
        //        outletTuples.Add(new InletOrOutletDimensionTuple(OperatorTypeEnum.BandPassFilterConstantPeakGain, 0, DimensionEnum.Signal));

        //        inletTuples.Add(new InletOrOutletDimensionTuple(OperatorTypeEnum.BandPassFilterConstantTransitionGain, 0, DimensionEnum.Signal));
        //        outletTuples.Add(new InletOrOutletDimensionTuple(OperatorTypeEnum.BandPassFilterConstantTransitionGain, 0, DimensionEnum.Signal));

        //        inletTuples.Add(new InletOrOutletDimensionTuple(OperatorTypeEnum.Cache, 0, DimensionEnum.Signal));
        //        outletTuples.Add(new InletOrOutletDimensionTuple(OperatorTypeEnum.Cache, 0, DimensionEnum.Signal));

        //        inletTuples.Add(new InletOrOutletDimensionTuple(OperatorTypeEnum.HighPassFilter, 0, DimensionEnum.Signal));
        //        outletTuples.Add(new InletOrOutletDimensionTuple(OperatorTypeEnum.HighPassFilter, 0, DimensionEnum.Signal));

        //        inletTuples.Add(new InletOrOutletDimensionTuple(OperatorTypeEnum.HighShelfFilter, 0, DimensionEnum.Signal));
        //        outletTuples.Add(new InletOrOutletDimensionTuple(OperatorTypeEnum.HighShelfFilter, 0, DimensionEnum.Signal));

        //        inletTuples.Add(new InletOrOutletDimensionTuple(OperatorTypeEnum.Loop, 0, DimensionEnum.Signal));
        //        outletTuples.Add(new InletOrOutletDimensionTuple(OperatorTypeEnum.Loop, 0, DimensionEnum.Signal));

        //        inletTuples.Add(new InletOrOutletDimensionTuple(OperatorTypeEnum.LowPassFilter, 0, DimensionEnum.Signal));
        //        outletTuples.Add(new InletOrOutletDimensionTuple(OperatorTypeEnum.LowPassFilter, 0, DimensionEnum.Signal));

        //        inletTuples.Add(new InletOrOutletDimensionTuple(OperatorTypeEnum.LowShelfFilter, 0, DimensionEnum.Signal));
        //        outletTuples.Add(new InletOrOutletDimensionTuple(OperatorTypeEnum.LowShelfFilter, 0, DimensionEnum.Signal));

        //        inletTuples.Add(new InletOrOutletDimensionTuple(OperatorTypeEnum.MaxFollower, 0, DimensionEnum.Signal));

        //        inletTuples.Add(new InletOrOutletDimensionTuple(OperatorTypeEnum.MaxOverDimension, 0, DimensionEnum.Signal));

        //        inletTuples.Add(new InletOrOutletDimensionTuple(OperatorTypeEnum.MinFollower, 0, DimensionEnum.Signal));

        //        inletTuples.Add(new InletOrOutletDimensionTuple(OperatorTypeEnum.MinOverDimension, 0, DimensionEnum.Signal));

        //        inletTuples.Add(new InletOrOutletDimensionTuple(OperatorTypeEnum.NotchFilter, 0, DimensionEnum.Signal));
        //        outletTuples.Add(new InletOrOutletDimensionTuple(OperatorTypeEnum.NotchFilter, 0, DimensionEnum.Signal));

        //        inletTuples.Add(new InletOrOutletDimensionTuple(OperatorTypeEnum.PeakingEQFilter, 0, DimensionEnum.Signal));
        //        outletTuples.Add(new InletOrOutletDimensionTuple(OperatorTypeEnum.PeakingEQFilter, 0, DimensionEnum.Signal));

        //        inletTuples.Add(new InletOrOutletDimensionTuple(OperatorTypeEnum.Pulse, 0, DimensionEnum.Frequency));
        //        outletTuples.Add(new InletOrOutletDimensionTuple(OperatorTypeEnum.Pulse, 0, DimensionEnum.Signal));

        //        inletTuples.Add(new InletOrOutletDimensionTuple(OperatorTypeEnum.Interpolate, 0, DimensionEnum.Signal));
        //        outletTuples.Add(new InletOrOutletDimensionTuple(OperatorTypeEnum.Interpolate, 0, DimensionEnum.Signal));

        //        inletTuples.Add(new InletOrOutletDimensionTuple(OperatorTypeEnum.Reverse, 0, DimensionEnum.Signal));
        //        outletTuples.Add(new InletOrOutletDimensionTuple(OperatorTypeEnum.Reverse, 0, DimensionEnum.Signal));

        //        inletTuples.Add(new InletOrOutletDimensionTuple(OperatorTypeEnum.Round, 0, DimensionEnum.Signal));
        //        outletTuples.Add(new InletOrOutletDimensionTuple(OperatorTypeEnum.Round, 0, DimensionEnum.Signal));

        //        inletTuples.Add(new InletOrOutletDimensionTuple(OperatorTypeEnum.Sample, 0, DimensionEnum.Frequency));
        //        outletTuples.Add(new InletOrOutletDimensionTuple(OperatorTypeEnum.Sample, 0, DimensionEnum.Signal));

        //        inletTuples.Add(new InletOrOutletDimensionTuple(OperatorTypeEnum.SawDown, 0, DimensionEnum.Frequency));
        //        outletTuples.Add(new InletOrOutletDimensionTuple(OperatorTypeEnum.SawDown, 0, DimensionEnum.Signal));

        //        inletTuples.Add(new InletOrOutletDimensionTuple(OperatorTypeEnum.SawUp, 0, DimensionEnum.Frequency));
        //        outletTuples.Add(new InletOrOutletDimensionTuple(OperatorTypeEnum.SawUp, 0, DimensionEnum.Signal));

        //        inletTuples.Add(new InletOrOutletDimensionTuple(OperatorTypeEnum.Scaler, 0, DimensionEnum.Signal));
        //        outletTuples.Add(new InletOrOutletDimensionTuple(OperatorTypeEnum.Scaler, 0, DimensionEnum.Signal));

        //        inletTuples.Add(new InletOrOutletDimensionTuple(OperatorTypeEnum.Select, 0, DimensionEnum.Signal));

        //        inletTuples.Add(new InletOrOutletDimensionTuple(OperatorTypeEnum.Shift, 0, DimensionEnum.Signal));
        //        outletTuples.Add(new InletOrOutletDimensionTuple(OperatorTypeEnum.Shift, 0, DimensionEnum.Signal));

        //        inletTuples.Add(new InletOrOutletDimensionTuple(OperatorTypeEnum.Sine, 0, DimensionEnum.Frequency));
        //        outletTuples.Add(new InletOrOutletDimensionTuple(OperatorTypeEnum.Sine, 0, DimensionEnum.Signal));

        //        inletTuples.Add(new InletOrOutletDimensionTuple(OperatorTypeEnum.SortOverDimension, 0, DimensionEnum.Signal));
        //        outletTuples.Add(new InletOrOutletDimensionTuple(OperatorTypeEnum.SortOverDimension, 0, DimensionEnum.Signal));

        //        inletTuples.Add(new InletOrOutletDimensionTuple(OperatorTypeEnum.Spectrum, 0, DimensionEnum.Signal));
        //        outletTuples.Add(new InletOrOutletDimensionTuple(OperatorTypeEnum.Spectrum, 0, DimensionEnum.Volume));

        //        inletTuples.Add(new InletOrOutletDimensionTuple(OperatorTypeEnum.Square, 0, DimensionEnum.Frequency));
        //        outletTuples.Add(new InletOrOutletDimensionTuple(OperatorTypeEnum.Square, 0, DimensionEnum.Signal));

        //        inletTuples.Add(new InletOrOutletDimensionTuple(OperatorTypeEnum.Squash, 0, DimensionEnum.Signal));
        //        outletTuples.Add(new InletOrOutletDimensionTuple(OperatorTypeEnum.Squash, 0, DimensionEnum.Signal));

        //        inletTuples.Add(new InletOrOutletDimensionTuple(OperatorTypeEnum.Stretch, 0, DimensionEnum.Signal));
        //        outletTuples.Add(new InletOrOutletDimensionTuple(OperatorTypeEnum.Stretch, 0, DimensionEnum.Signal));

        //        inletTuples.Add(new InletOrOutletDimensionTuple(OperatorTypeEnum.SumFollower, 0, DimensionEnum.Signal));

        //        inletTuples.Add(new InletOrOutletDimensionTuple(OperatorTypeEnum.SumOverDimension, 0, DimensionEnum.Signal));

        //        inletTuples.Add(new InletOrOutletDimensionTuple(OperatorTypeEnum.TimePower, 0, DimensionEnum.Signal));
        //        outletTuples.Add(new InletOrOutletDimensionTuple(OperatorTypeEnum.TimePower, 0, DimensionEnum.Signal));

        //        inletTuples.Add(new InletOrOutletDimensionTuple(OperatorTypeEnum.Triangle, 0, DimensionEnum.Frequency));
        //        outletTuples.Add(new InletOrOutletDimensionTuple(OperatorTypeEnum.Triangle, 0, DimensionEnum.Signal));

        //        Dictionary<OperatorTypeEnum, IList<InletOrOutletDimensionTuple>> inletTupleDictionary = 
        //            inletTuples.ToNonUniqueDictionary(x => x.OperatorTypeEnum);

        //        Dictionary<OperatorTypeEnum, IList<InletOrOutletDimensionTuple>> outletTupleDictionary = 
        //            outletTuples.ToNonUniqueDictionary(x => x.OperatorTypeEnum);

        //        RepositoryWrapper repositories = PersistenceHelper.CreateRepositoryWrapper(context);

        //        var patchManager = new PatchManager(new PatchRepositories(repositories));

        //        IList<Operator> operators = repositories.OperatorRepository.GetAll();

        //        for (int i = 0; i < operators.Count; i++)
        //        {
        //            Operator op = operators[i];

        //            OperatorTypeEnum operatorTypeEnum = op.GetOperatorTypeEnum();

        //            IList<InletOrOutletDimensionTuple> inletTuples2;
        //            if (inletTupleDictionary.TryGetValue(operatorTypeEnum, out inletTuples2))
        //            {
        //                foreach (InletOrOutletDimensionTuple inletTuple in inletTuples2)
        //                {
        //                    Inlet inlet = op.Inlets.Where(x => x.ListIndex == inletTuple.ListIndex).Single();
        //                    inlet.SetDimensionEnum(inletTuple.DimensionEnum, repositories.DimensionRepository);
        //                }
        //            }

        //            IList<InletOrOutletDimensionTuple> outletTuples2;
        //            if (outletTupleDictionary.TryGetValue(operatorTypeEnum, out outletTuples2))
        //            {
        //                foreach (InletOrOutletDimensionTuple outletTuple in outletTuples2)
        //                {
        //                    Outlet outlet = op.Outlets.Where(x => x.ListIndex == outletTuple.ListIndex).Single();
        //                    outlet.SetDimensionEnum(outletTuple.DimensionEnum, repositories.DimensionRepository);
        //                }
        //            }

        //            // Cannot validate the operator, because it will do a recursive validation,
        //            // validating not yet migrated operators.

        //            string progressMessage = String.Format("Migrated Operator {0}/{1}.", i + 1, operators.Count);
        //            progressCallback(progressMessage);
        //        }

        //        AssertDocuments(repositories, progressCallback);

        //        //throw new Exception("Temporarily not committing, for debugging.");

        //        context.Commit();
        //    }

        //    progressCallback(String.Format("{0} finished.", MethodBase.GetCurrentMethod().Name));
        //}

        //public static void Migrate_DeletePhaseShiftInlets(Action<string> progressCallback)
        //{
        //    if (progressCallback == null) throw new NullException(() => progressCallback);

        //    progressCallback(String.Format("Starting {0}...", MethodBase.GetCurrentMethod().Name));

        //    using (IContext context = PersistenceHelper.CreateContext())
        //    {
        //        RepositoryWrapper repositories = PersistenceHelper.CreateRepositoryWrapper(context);

        //        var patchManager = new PatchManager(new PatchRepositories(repositories));

        //        IList<Operator> operators = repositories.OperatorRepository.GetAll();

        //        for (int i = 0; i < operators.Count; i++)
        //        {
        //            Operator op = operators[i];

        //            OperatorTypeEnum operatorTypeEnum = op.GetOperatorTypeEnum();

        //            switch (operatorTypeEnum)
        //            {
        //                case OperatorTypeEnum.Random:
        //                case OperatorTypeEnum.SawDown:
        //                case OperatorTypeEnum.SawUp:
        //                case OperatorTypeEnum.Sine:
        //                case OperatorTypeEnum.Square:
        //                case OperatorTypeEnum.Triangle:
        //                    {
        //                        Inlet inlet = op.Inlets.Single(x => x.ListIndex == 1);
        //                        patchManager.DeleteInlet(inlet);
        //                        break;
        //                    }

        //                case OperatorTypeEnum.Pulse:
        //                    {
        //                        Inlet inlet = op.Inlets.Single(x => x.ListIndex == 2);
        //                        patchManager.DeleteInlet(inlet);
        //                        break;
        //                    }
        //            }

        //            // Cannot validate the operator, because it will do a recursive validation,
        //            // validating not yet migrated operators.

        //            string progressMessage = String.Format("Migrated Operator {0}/{1}.", i + 1, operators.Count);
        //            progressCallback(progressMessage);
        //        }

        //        AssertDocuments(repositories, progressCallback);

        //        //throw new Exception("Temporarily not committing, for debugging.");

        //        context.Commit();
        //    }

        //    progressCallback(String.Format("{0} finished.", MethodBase.GetCurrentMethod().Name));
        //}

        //public static void Migrate_CustomOperators_ResaveToSetIsObsolete_OfInletsAndOutlets(Action<string> progressCallback)
        //{
        //    if (progressCallback == null) throw new NullException(() => progressCallback);

        //    progressCallback(String.Format("Starting {0}...", MethodBase.GetCurrentMethod().Name));

        //    using (IContext context = PersistenceHelper.CreateContext())
        //    {
        //        RepositoryWrapper repositories = PersistenceHelper.CreateRepositoryWrapper(context);

        //        var patchManager = new PatchManager(new PatchRepositories(repositories));

        //        IList<Operator> operators = repositories.OperatorRepository.GetManyByOperatorTypeID((int)OperatorTypeEnum.CustomOperator);

        //        for (int i = 0; i < operators.Count; i++)
        //        {
        //            Operator op = operators[i];

        //            patchManager.Patch = op.Patch;
        //            patchManager.SaveOperator(op);

        //            // Cannot validate the operator, because it will do a recursive validation,
        //            // validating not yet migrated operators.

        //            string progressMessage = String.Format("Migrated Operator {0}/{1}.", i + 1, operators.Count);
        //            progressCallback(progressMessage);
        //        }

        //        AssertDocuments(repositories, progressCallback);

        //        //throw new Exception("Temporarily not committing, for debugging.");
        //        context.Commit();
        //    }

        //    progressCallback(String.Format("{0} finished.", MethodBase.GetCurrentMethod().Name));
        //}

        //public static void Migrate_Move_CurvesSamplesAndPatch_FromChildDocuments_ToRootDocuments(Action<string> progressCallback)
        //{
        //    if (progressCallback == null) throw new NullException(() => progressCallback);

        //    progressCallback(String.Format("Starting {0}...", MethodBase.GetCurrentMethod().Name));

        //    using (IContext context = PersistenceHelper.CreateContext())
        //    {
        //        RepositoryWrapper repositories = PersistenceHelper.CreateRepositoryWrapper(context);

        //        IList<Document> rootDocuments = repositories.DocumentRepository.OrderByName();

        //        for (int i = 0; i < rootDocuments.Count; i++)
        //        {
        //            Document rootDocument = rootDocuments[i];

        //            var documentManager = new DocumentManager(repositories);

        //            HashSet<string> existingSampleNamesLowerCase = rootDocument.Samples
        //                                                                       .Select(x => x.Name.ToLower())
        //                                                                       .ToHashSet();

        //            foreach (Document childDocument in rootDocument.ChildDocuments.ToArray())
        //            {
        //                foreach (Curve curve in childDocument.Curves.ToArray())
        //                {
        //                    curve.LinkTo(rootDocument);
        //                }

        //                foreach (Sample sample in childDocument.Samples.ToArray())
        //                {
        //                    string newSampleName = sample.Name;
        //                    int number = 2;
        //                    while (existingSampleNamesLowerCase.Contains(newSampleName.ToLower()))
        //                    {
        //                        newSampleName = $"{sample.Name} ({number})";
        //                        number++;
        //                    }
        //                    existingSampleNamesLowerCase.Add(newSampleName.ToLower());

        //                    sample.Name = newSampleName;
        //                    sample.LinkTo(rootDocument);
        //                }

        //                Patch patch = childDocument.Patches.Single();
        //                patch.LinkTo(rootDocument);

        //                //repositories.DocumentRepository.Delete(childDocument);

        //                VoidResult result = documentManager.DeleteWithRelatedEntities(childDocument);
        //                result.Assert();
        //            }

        //            string progressMessage = String.Format("Migrated Document {0}/{1}.", i + 1, rootDocuments.Count);
        //            progressCallback(progressMessage);
        //        }

        //        // Flush to make NHibernate not accidently mistake the deleted child documents for root documents.
        //        // (for some reason that happens).
        //        context.Flush();

        //        AssertDocuments(repositories, progressCallback);

        //        //throw new Exception("Temporarily not committing, for debugging.");

        //        context.Commit();
        //    }

        //    progressCallback(String.Format("{0} finished.", MethodBase.GetCurrentMethod().Name));
        //}

        //public static void Migrate_ResampleInterpolationType_LineRememberT0_To_LineRememberT1(Action<string> progressCallback)
        //{
        //    if (progressCallback == null) throw new NullException(() => progressCallback);

        //    progressCallback(String.Format("Starting {0}...", MethodBase.GetCurrentMethod().Name));

        //    using (IContext context = PersistenceHelper.CreateContext())
        //    {
        //        RepositoryWrapper repositories = PersistenceHelper.CreateRepositoryWrapper(context);

        //        var patchManager = new PatchManager(new PatchRepositories(repositories));

        //        IList<Operator> operators = repositories.OperatorRepository.GetAll();

        //        for (int i = 0; i < operators.Count; i++)
        //        {
        //            Operator op = operators[i];

        //            string interpolationType = DataPropertyParser.TryGetString(op, PropertyNames.InterpolationType);

        //            if (String.Equals(interpolationType, "LineRememberT0"))
        //            {
        //                DataPropertyParser.SetValue(op, PropertyNames.InterpolationType, "LineRememberT1");
        //            }

        //            string progressMessage = String.Format("Migrated Operator {0}/{1}.", i + 1, operators.Count);
        //            progressCallback(progressMessage);
        //        }

        //        AssertDocuments(repositories, progressCallback);

        //        //throw new Exception("Temporarily not committing, for debugging.");
        //        context.Commit();
        //    }

        //    progressCallback(String.Format("{0} finished.", MethodBase.GetCurrentMethod().Name));
        //}

        public static void Migrate_ResampleInterpolationType_Rename_LineRememberT1_To_Line(Action<string> progressCallback)
        {
            if (progressCallback == null) throw new NullException(() => progressCallback);

            progressCallback(String.Format("Starting {0}...", MethodBase.GetCurrentMethod().Name));

            using (IContext context = PersistenceHelper.CreateContext())
            {
                RepositoryWrapper repositories = PersistenceHelper.CreateRepositoryWrapper(context);

                var patchManager = new PatchManager(new PatchRepositories(repositories));

                IList<Operator> operators = repositories.OperatorRepository.GetAll();

                for (int i = 0; i < operators.Count; i++)
                {
                    Operator op = operators[i];

                    string interpolationType = DataPropertyParser.TryGetString(op, PropertyNames.InterpolationType);

                    if (String.Equals(interpolationType, "LineRememberT1"))
                    {
                        DataPropertyParser.SetValue(op, PropertyNames.InterpolationType, "Line");
                    }

                    string progressMessage = String.Format("Migrated Operator {0}/{1}.", i + 1, operators.Count);
                    progressCallback(progressMessage);
                }

                AssertDocuments(repositories, progressCallback);

                //throw new Exception("Temporarily not committing, for debugging.");
                context.Commit();
            }

            progressCallback(String.Format("{0} finished.", MethodBase.GetCurrentMethod().Name));
        }

        public static void Migrate_Bundle_AndUnbundle_ToInletsToDimension_AndDimensionToOutlets(Action<string> progressCallback)
        {
            if (progressCallback == null) throw new NullException(() => progressCallback);

            progressCallback(String.Format("Starting {0}...", MethodBase.GetCurrentMethod().Name));

            using (IContext context = PersistenceHelper.CreateContext())
            {
                RepositoryWrapper repositories = PersistenceHelper.CreateRepositoryWrapper(context);

                var patchManager = new PatchManager(new PatchRepositories(repositories));
                var entityPositionManager = new EntityPositionManager(repositories.EntityPositionRepository, repositories.IDRepository);

                {
                    IList<Operator> source_Bundle_Operators = repositories.OperatorRepository.GetManyByOperatorTypeID((int)OperatorTypeEnum.Bundle);

                    for (int i = 0; i < source_Bundle_Operators.Count; i++)
                    {
                        Operator source_Bundle_Operator = source_Bundle_Operators[i];
                        var source_Bundle_Wrapper = new Bundle_OperatorWrapper(source_Bundle_Operator);

                        patchManager.Patch = source_Bundle_Operator.Patch;

                        InletsToDimension_OperatorWrapper dest_InletsToDimension_Wrapper = patchManager.InletsToDimension(source_Bundle_Wrapper.Operands);
                        dest_InletsToDimension_Wrapper.InterpolationType = ResampleInterpolationTypeEnum.Stripe;

                        Operator dest_InletsToDimension_Operator = dest_InletsToDimension_Wrapper;
                        dest_InletsToDimension_Operator.LinkTo(source_Bundle_Operator.StandardDimension);
                        dest_InletsToDimension_Operator.CustomDimensionName = source_Bundle_Operator.CustomDimensionName;
                        dest_InletsToDimension_Operator.Name = source_Bundle_Operator.Name;

                        Outlet destOutlet = dest_InletsToDimension_Wrapper.Result;

                        IList<Inlet> connectedInlets = source_Bundle_Wrapper.Result.ConnectedInlets.ToArray();
                        foreach (Inlet connectedInlet in connectedInlets)
                        {
                            connectedInlet.LinkTo(destOutlet);
                        }

                        EntityPosition sourceEntityPosition = entityPositionManager.GetOperatorPosition(source_Bundle_Operator.ID);
                        EntityPosition destEntityPosition = entityPositionManager.GetOrCreateOperatorPosition(dest_InletsToDimension_Operator.ID);
                        destEntityPosition.X = sourceEntityPosition.X;
                        destEntityPosition.Y = sourceEntityPosition.Y;

                        patchManager.DeleteOperatorWithRelatedEntities(source_Bundle_Operator);

                        string progressMessage = String.Format("Migrated Bundle Operator {0}/{1}.", i + 1, source_Bundle_Operators.Count);
                        progressCallback(progressMessage);
                    }
                }

                {
                    IList<Operator> source_Unbundle_Operators = repositories.OperatorRepository.GetManyByOperatorTypeID((int)OperatorTypeEnum.Unbundle);

                    for (int sourceOperatorIndex = 0; sourceOperatorIndex < source_Unbundle_Operators.Count; sourceOperatorIndex++)
                    {
                        Operator source_Unbundle_Operator = source_Unbundle_Operators[sourceOperatorIndex];
                        var source_Unbundle_Wrapper = new Unbundle_OperatorWrapper(source_Unbundle_Operator);

                        patchManager.Patch = source_Unbundle_Operator.Patch;

                        int outletCount = source_Unbundle_Operator.Outlets.Count;

                        DimensionToOutlets_OperatorWrapper dest_DimensionToOutlets_Wrapper = patchManager.DimensionToOutlets(
                            source_Unbundle_Wrapper.Operand,
                            source_Unbundle_Operator.GetStandardDimensionEnum(),
                            source_Unbundle_Operator.CustomDimensionName,
                            outletCount);
                        dest_DimensionToOutlets_Wrapper.Operand = source_Unbundle_Wrapper.Operand;

                        Operator dest_DimensionToOutlets_Operator = dest_DimensionToOutlets_Wrapper;
                        dest_DimensionToOutlets_Operator.Name = source_Unbundle_Operator.Name;

                        for (int outletIndex = 0; outletIndex < outletCount; outletIndex++)
                        {
                            Outlet sourceOutlet = source_Unbundle_Operator.Outlets[outletIndex];
                            Outlet destOutlet = dest_DimensionToOutlets_Operator.Outlets[outletIndex];
                        
                            foreach (Inlet connectedInlet in sourceOutlet.ConnectedInlets.ToArray())
                            {
                                connectedInlet.LinkTo(destOutlet);
                            }
                        }

                        EntityPosition sourceEntityPosition = entityPositionManager.GetOperatorPosition(source_Unbundle_Operator.ID);
                        EntityPosition destEntityPosition = entityPositionManager.GetOrCreateOperatorPosition(dest_DimensionToOutlets_Operator.ID);
                        destEntityPosition.X = sourceEntityPosition.X;
                        destEntityPosition.Y = sourceEntityPosition.Y;

                        patchManager.DeleteOperatorWithRelatedEntities(source_Unbundle_Operator);

                        string progressMessage = String.Format("Migrated Unbundle Operator {0}/{1}.", sourceOperatorIndex + 1, source_Unbundle_Operators.Count);
                        progressCallback(progressMessage);
                    }
                }

                AssertDocuments(repositories, progressCallback);

                //throw new Exception("Temporarily not committing, for debugging.");
                context.Commit();
            }

            progressCallback(String.Format("{0} finished.", MethodBase.GetCurrentMethod().Name));
        }

        // Helpers

        private static void AssertDocuments(RepositoryWrapper repositories, Action<string> progressCallback)
        {
            IList<Document> rootDocuments = repositories.DocumentRepository.GetAll();

            AssertDocuments(rootDocuments, repositories, progressCallback);
        }

        private static void AssertDocuments(IList<Document> rootDocuments, RepositoryWrapper repositories, Action<string> progressCallback)
        {
            IResult totalResult = new VoidResult { Successful = true };
            for (int i = 0; i < rootDocuments.Count; i++)
            {
                Document rootDocument = rootDocuments[i];

                string progressMessage = String.Format("Validating document {0}/{1}: '{2}'.", i + 1, rootDocuments.Count, rootDocument.Name);
                progressCallback(progressMessage);

                // Validate
                var documentManager = new DocumentManager(repositories);
                VoidResult result = documentManager.Save(rootDocument);
                totalResult.Combine(result);
            }

            try
            {
                ResultHelper.Assert(totalResult);
            }
            catch
            {
                string progressMessage = String.Format("Exception while validating documents.");
                progressCallback(progressMessage);
                throw;
            }
        }
    }
}