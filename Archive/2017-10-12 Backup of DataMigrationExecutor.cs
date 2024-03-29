﻿//using System;
//using System.Collections.Generic;
//using System.Linq;
//using System.Reflection;
//using JJ.Framework.Data;
//using JJ.Framework.Exceptions;
//using JJ.Business.Synthesizer.Enums;
//using JJ.Business.Synthesizer.Helpers;
//using JJ.Business.Synthesizer;
//using JJ.Business.Canonical;
//using JJ.Business.Synthesizer.EntityWrappers;
//using JJ.Business.Synthesizer.LinkTo;
//using JJ.Business.Synthesizer.Validation;
//using JJ.Data.Synthesizer.Entities;
//using JJ.Framework.Business;
//using JJ.Framework.Collections;

//namespace JJ.OneOff.Synthesizer.DataMigration
//{
//    internal static class DataMigrationExecutor
//    {
//        // General

//        public static bool MustAssertWarningIncrease { get; set; } = true;

//        public static void AssertAllDocuments(Action<string> progressCallback)
//        {
//            if (progressCallback == null) throw new NullException(() => progressCallback);

//            progressCallback($"Starting {MethodBase.GetCurrentMethod().Name}...");

//            using (IContext context = PersistenceHelper.CreateContext())
//            {
//                RepositoryWrapper repositories = PersistenceHelper.CreateRepositoryWrapper(context);
//                AssertDocuments_AndReapplyUnderlyingPatches(repositories, progressCallback);
//            }

//            progressCallback($"{MethodBase.GetCurrentMethod().Name} finished.");
//        }

//        public static void ShowAllWarnings(Action<string> progressCallback)
//        {
//            if (progressCallback == null) throw new NullException(() => progressCallback);

//            progressCallback($"Starting {MethodBase.GetCurrentMethod().Name}...");

//            using (IContext context = PersistenceHelper.CreateContext())
//            {
//                RepositoryWrapper repositories = PersistenceHelper.CreateRepositoryWrapper(context);
//                var documentManager = new DocumentManager(repositories);

//                IResult totalResult = new VoidResult { Successful = true };

//                IList<Document> documents = repositories.DocumentRepository.GetAll();
//                foreach (Document document in documents)
//                {
//                    IResult result = documentManager.GetWarningsRecursive(document);
//                    string messagePrefix = ValidationHelper.GetMessagePrefix(document);

//                    totalResult.Combine(result, messagePrefix);
//                }

//                totalResult.Assert();
//            }

//            progressCallback($"{MethodBase.GetCurrentMethod().Name} finished.");
//        }

//        public static void ReapplyAllPatches(Action<string> progressCallback)
//        {
//            if (progressCallback == null) throw new NullException(() => progressCallback);

//            progressCallback($"Starting {MethodBase.GetCurrentMethod().Name}...");

//            using (IContext context = PersistenceHelper.CreateContext())
//            {
//                RepositoryWrapper repositories = PersistenceHelper.CreateRepositoryWrapper(context);
//                AssertDocuments_AndReapplyUnderlyingPatches(repositories, progressCallback);

//                context.Commit();
//            }

//            progressCallback($"{MethodBase.GetCurrentMethod().Name} finished.");
//        }

//        public static void DeleteOrphanedEntityPositions(Action<string> progressCallback)
//        {
//            if (progressCallback == null) throw new NullException(() => progressCallback);

//            progressCallback($"Starting {MethodBase.GetCurrentMethod().Name}...");

//            using (IContext context = PersistenceHelper.CreateContext())
//            {
//                RepositoryWrapper repositories = PersistenceHelper.CreateRepositoryWrapper(context);

//                var entityPositionManager = new EntityPositionManager(repositories.EntityPositionRepository, repositories.IDRepository);
//                int rowsAffected = entityPositionManager.DeleteOrphans();

//                AssertDocuments_AndReapplyUnderlyingPatches(repositories, progressCallback);

//                context.Commit();

//                progressCallback($"{MethodBase.GetCurrentMethod().Name} finished. {rowsAffected} rows affected.");
//            }

//        }

//        private static void AssertDocuments_AndReapplyUnderlyingPatches(
//            RepositoryWrapper repositories,
//            Action<string> progressCallback)
//        {
//            IList<Document> rootDocuments = repositories.DocumentRepository.GetAll();

//            AssertDocuments_AndReapplyUnderlyingPatches(rootDocuments, repositories, progressCallback, MustAssertWarningIncrease);
//        }

//        private static void AssertDocuments_AndReapplyUnderlyingPatches(
//            IList<Document> rootDocuments,
//            RepositoryWrapper repositories,
//            Action<string> progressCallback,
//            bool mustAssertWarningIncrease = true)
//        {
//            var documentManager = new DocumentManager(repositories);

//            IResult totalResult = new VoidResult { Successful = true };
//            for (int i = 0; i < rootDocuments.Count; i++)
//            {
//                Document document = rootDocuments[i];

//                string progressMessage = $"Validating document {i + 1}/{rootDocuments.Count}: '{document.Name}'.";
//                progressCallback(progressMessage);

//                // Warnings Before
//                IResult warningResultBefore = null;
//                if (mustAssertWarningIncrease)
//                {
//                    warningResultBefore = documentManager.GetWarningsRecursive(document);
//                }

//                // Reapply UnderlyingPatches

//                // 'Get' through the manager will execute side-effects of reapplying underlying patches.
//                // Not that the migration should not have written fully correct data,
//                // but you can at any time change an underlying patch from a library,
//                // and then the dependent documents will not be immediately updated.
//                // So at any time 'invalid' data (non-updated operators)
//                // can be in the database and that would be no problem.
//                // However if you just asser the documents,
//                // it will just complain about mismatches all over the place.

//                documentManager.Get(document.ID);

//                // Validate
//                IResult saveResult = documentManager.Save(document);

//                // Collect Results
//                totalResult.Combine(saveResult, ValidationHelper.GetMessagePrefix(document));

//                if (mustAssertWarningIncrease)
//                {
//                    // Warnings After
//                    IResult warningResultAfter = documentManager.GetWarningsRecursive(document);

//                    // Compare Warnings
//                    IList<string> additionalWarningTexts = warningResultAfter.Messages
//                                                                             .Except(warningResultBefore.Messages)
//                                                                             .ToArray();
//                    if (additionalWarningTexts.Count != 0)
//                    {
//                        var additionalWarningResult = new VoidResult
//                        {
//                            Successful = false,
//                            Messages = additionalWarningTexts
//                        };

//                        totalResult.Combine(additionalWarningResult, "Warning: " + ValidationHelper.GetMessagePrefix(document));
//                    }
//                }
//            }

//            try
//            {
//                totalResult.Assert();
//            }
//            catch
//            {
//                progressCallback("Exception while validating documents.");
//                throw;
//            }
//        }

//        // Specific Migrations

//        /// <summary>
//        /// There are multiple situations that need to be handled:
//        /// - Sample Operators without a sample.
//        /// - Sample Operators with sole ownership over a sample.
//        /// - Sample Operators with joint ownership of the sample sample.
//        /// - Samples without an operator.
//        /// All those sitations have to translated to unique and required link between an operator and a sample.
//        /// 
//        /// In this, possibly simpler, version of the migration,
//        /// all samples without an operator are deleted.
//        /// Those are not that important and it makes the migration a whole lot simpler.
//        /// </summary>
//        public static void Migrate_FromOperator_Data_ToSample_Operator(Action<string> progressCallback)
//        {
//            if (progressCallback == null) throw new NullException(() => progressCallback);

//            progressCallback($"Starting {MethodBase.GetCurrentMethod().Name}...");

//            using (IContext context = PersistenceHelper.CreateContext())
//            {
//                RepositoryWrapper repositories = PersistenceHelper.CreateRepositoryWrapper(context);
//                var documentManager = new DocumentManager(repositories);
//                var sampleManager = new SampleManager(new SampleRepositories(repositories));

//                Patch systemPatch = documentManager.GetSystemPatch(nameof(SystemPatchNames.Sample));
//                IList<Operator> operators = repositories.OperatorRepository.GetManyByUnderlyingPatchID(systemPatch.ID);

//                // Loop through Sample Operators
//                {
//                    int count = operators.Count;
//                    for (int i = 0; i < count; i++)
//                    {
//                        Operator op = operators[i];

//                        int? sampleID = DataPropertyParser.TryGetInt32(op, nameof(Sample_OperatorWrapper.SampleID));
//                        if (sampleID.HasValue)
//                        {
//                            // Clone Sample
//                            Sample originalSample = repositories.SampleRepository.Get(sampleID.Value);
//                            Sample newSample = CloneSample(originalSample, repositories);

//                            // Set Bytes
//                            byte[] bytes = repositories.SampleRepository.TryGetBytes(sampleID.Value);
//                            bytes = bytes ?? new byte[0]; // Create empty byte array if null.
//                            repositories.SampleRepository.SetBytes(newSample.ID, bytes);

//                            // Link new Sample to Operator.
//                            newSample.Operator = op;
//                            DataPropertyParser.SetValue(op, nameof(Sample_OperatorWrapper.SampleID), newSample.ID);
//                        }
//                        else
//                        {
//                            // Operator is sample-less
//                            // Create a new sample.
//                            Sample newSample = sampleManager.CreateSample(op.Patch.Document);

//                            // Set bytes
//                            repositories.SampleRepository.SetBytes(newSample.ID, new byte[0]);

//                            // Link new Sample to Operator.
//                            newSample.Operator = op;
//                            DataPropertyParser.SetValue(op, nameof(Sample_OperatorWrapper.SampleID), newSample.ID);
//                        }

//                        progressCallback($"Migrated {nameof(Operator)} {i + 1}/{count}.");
//                    }
//                }

//                // Flush so GetAll includes the new samples.
//                context.Flush();

//                // Now delete all orphaned samples,
//                // because we created clones for every sample.
//                // (Yes, this also deletes all samples that were already not in use, but so be it.) 
//                {
//                    IList<Sample> samples = repositories.SampleRepository.GetAll();
//                    // TODO: You probaly can use sample.Operator == null instead of this dictionary.
//                    HashSet<int> sampleIDsOfAllOperators = operators.Select(x => DataPropertyParser.TryGetInt32(x, nameof(Sample_OperatorWrapper.SampleID)))
//                                                                    .Where(x => x.HasValue)
//                                                                    .Select(x => x.Value)
//                                                                    .ToHashSet();
//                    int count = samples.Count;
//                    for (int i = 0; i < count; i++)
//                    {
//                        Sample sample = samples[i];
//                        bool isUsed = sampleIDsOfAllOperators.Contains(sample.ID);
//                        if (!isUsed)
//                        {
//                            VoidResult result = sampleManager.Delete(sample);
//                            result.Assert();
//                        }
//                    }
//                }

//                // I expect validation will try to get samples using LINQ queries, which will result in error.
//                // Actually: That was just a guess. Try without it.
//                //context.Flush();

//                // TODO: I get non-unique names probably due to multiply-used, now cloned samples.
//                // Remove the whole unique check from the validation?
//                AssertDocuments_AndReapplyUnderlyingPatches(repositories, progressCallback);

//                throw new Exception("Temporarily not committing, for debugging.");

//                context.Commit();
//            }

//            progressCallback($"{MethodBase.GetCurrentMethod().Name} finished.");
//        }

//        private static Sample CloneSample(Sample originalSample, RepositoryWrapper repositories)
//        {
//            var clonedSample = new Sample { ID = repositories.IDRepository.GetID() };
//            repositories.SampleRepository.Insert(clonedSample);

//            clonedSample.Amplifier = originalSample.Amplifier;
//            clonedSample.BytesToSkip = originalSample.BytesToSkip;
//            clonedSample.IsActive = originalSample.IsActive;
//            clonedSample.Name = originalSample.Name;
//            clonedSample.OriginalLocation = originalSample.OriginalLocation;
//            clonedSample.SamplingRate = originalSample.SamplingRate;
//            clonedSample.TimeMultiplier = originalSample.TimeMultiplier;
            
//            clonedSample.LinkTo(originalSample.AudioFileFormat);
//            clonedSample.LinkTo(originalSample.Document);
//            clonedSample.LinkTo(originalSample.InterpolationType);
//            clonedSample.LinkTo(originalSample.SampleDataType);
//            clonedSample.LinkTo(originalSample.SpeakerSetup);

//            return clonedSample;
//        }
//    }
//}