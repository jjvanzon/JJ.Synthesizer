using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using JJ.Framework.Data;
using JJ.Framework.Exceptions;
using JJ.Business.Synthesizer.Enums;
using JJ.Business.Synthesizer.Helpers;
using JJ.Business.Synthesizer;
using JJ.Business.Canonical;
using JJ.Business.Synthesizer.Extensions;
using JJ.Business.Synthesizer.LinkTo;
using JJ.Business.Synthesizer.Validation;
using JJ.Data.Synthesizer.Entities;
using JJ.Framework.Business;
using JJ.Framework.Collections;

namespace JJ.OneOff.Synthesizer.DataMigration
{
    internal class DataMigrationExecutor
    {
        private class InletOrOutletTuple
        {
            public InletOrOutletTuple(
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

        public static void AssertAllDocuments(Action<string> progressCallback)
        {
            if (progressCallback == null) throw new NullException(() => progressCallback);

            progressCallback($"Starting {MethodBase.GetCurrentMethod().Name}...");

            using (IContext context = PersistenceHelper.CreateContext())
            {
                RepositoryWrapper repositories = PersistenceHelper.CreateRepositoryWrapper(context);
                AssertDocuments(repositories, progressCallback);
            }

            progressCallback($"{MethodBase.GetCurrentMethod().Name} finished.");
        }

        public static void DeleteOrphanedEntityPositions(Action<string> progressCallback)
        {
            if (progressCallback == null) throw new NullException(() => progressCallback);

            progressCallback($"Starting {MethodBase.GetCurrentMethod().Name}...");

            using (IContext context = PersistenceHelper.CreateContext())
            {
                RepositoryWrapper repositories = PersistenceHelper.CreateRepositoryWrapper(context);

                var entityPositionManager = new EntityPositionManager(repositories.EntityPositionRepository, repositories.IDRepository);
                int rowsAffected = entityPositionManager.DeleteOrphans();

                AssertDocuments(repositories, progressCallback);

                context.Commit();

                progressCallback($"{MethodBase.GetCurrentMethod().Name} finished. {rowsAffected} rows affected.");
            }

        }

        public static void Migrate_AndOperator_OperatorType_ToUnderlyingPatch(Action<string> progressCallback)
        {
            if (progressCallback == null) throw new NullException(() => progressCallback);

            progressCallback($"Starting {MethodBase.GetCurrentMethod().Name}...");

            using (IContext context = PersistenceHelper.CreateContext())
            {
                RepositoryWrapper repositories = PersistenceHelper.CreateRepositoryWrapper(context);
                DocumentManager documentManager = new DocumentManager(repositories);
                Patch systemPatch = documentManager.GetSystemPatch(OperatorTypeEnum.And);

                IList<Operator> operators = repositories.OperatorRepository.GetManyByOperatorTypeID((int)OperatorTypeEnum.And);

                for (int i = 0; i < operators.Count; i++)
                {
                    Operator op = operators[i];
                    op.LinkToUnderlyingPatch(systemPatch);
                    op.UnlinkOperatorType();

                    foreach (Inlet inlet in op.Inlets)
                    {
                        inlet.WarnIfEmpty = true;
                    }

                    string progressMessage = $"Migrated Operator {i + 1}/{operators.Count}.";
                    progressCallback(progressMessage);
                }

                AssertDocuments(repositories, progressCallback);

                //throw new Exception("Temporarily not committing, for debugging.");

                context.Commit();
            }

            progressCallback($"{MethodBase.GetCurrentMethod().Name} finished.");
        }

        public static void Migrate_SineOperator_OperatorType_ToUnderlyingPatch(Action<string> progressCallback)
        {
            if (progressCallback == null) throw new NullException(() => progressCallback);

            progressCallback($"Starting {MethodBase.GetCurrentMethod().Name}...");

            using (IContext context = PersistenceHelper.CreateContext())
            {
                RepositoryWrapper repositories = PersistenceHelper.CreateRepositoryWrapper(context);
                DocumentManager documentManager = new DocumentManager(repositories);

                const OperatorTypeEnum operatorTypeEnum = OperatorTypeEnum.Sine;

                Patch systemPatch = documentManager.GetSystemPatch(operatorTypeEnum);

                IList<Operator> operators = repositories.OperatorRepository.GetManyByOperatorTypeID((int)operatorTypeEnum);

                for (int i = 0; i < operators.Count; i++)
                {
                    Operator op = operators[i];
                    op.LinkToUnderlyingPatch(systemPatch);
                    op.UnlinkOperatorType();

                    foreach (Inlet inlet in op.Inlets)
                    {
                        inlet.WarnIfEmpty = true;
                    }

                    foreach (Outlet outlet in op.Outlets)
                    {
                        outlet.NameOrDimensionHidden = true;
                    }

                    string progressMessage = $"Migrated Operator {i + 1}/{operators.Count}.";
                    progressCallback(progressMessage);
                }

                AssertDocuments(repositories, progressCallback);

                throw new Exception("Temporarily not committing, for debugging.");

                context.Commit();
            }

            progressCallback($"{MethodBase.GetCurrentMethod().Name} finished.");
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
                Document document = rootDocuments[i];

                string progressMessage = $"Validating document {i + 1}/{rootDocuments.Count}: '{document.Name}'.";
                progressCallback(progressMessage);

                // Validate
                var documentManager = new DocumentManager(repositories);

                // 'Get' will execute side-effects
                documentManager.Get(document.ID);
                IResult result = documentManager.Save(document);
                string messagePrefix = ValidationHelper.GetMessagePrefix(document);
                totalResult.Combine(result, messagePrefix);
            }

            try
            {
                totalResult.Assert();
            }
            catch
            {
                progressCallback("Exception while validating documents.");
                throw;
            }
        }
    }
}