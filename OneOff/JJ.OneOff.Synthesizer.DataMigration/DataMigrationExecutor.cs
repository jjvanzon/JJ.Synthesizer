using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using JJ.Business.CanonicalModel;
using JJ.Business.Synthesizer.EntityWrappers;
using JJ.Business.Synthesizer.Enums;
using JJ.Business.Synthesizer.Extensions;
using JJ.Business.Synthesizer.Helpers;
using JJ.Business.Synthesizer.LinkTo;
using JJ.Business.Synthesizer.Managers;
using JJ.Data.Synthesizer;
using JJ.Framework.Data;
using JJ.Framework.Reflection.Exceptions;

namespace JJ.OneOff.Synthesizer.DataMigration
{
    internal class DataMigrationExecutor
    {
        private const int DEFAULT_FREQUENCY = 440;

        public static void MigrateSineVolumes(Action<string> progressCallback)
        {
            throw new NotSupportedException();

            if (progressCallback == null) throw new NullException(() => progressCallback);

            progressCallback(String.Format("Starting {0}...", MethodBase.GetCurrentMethod().Name));

            using (IContext context = PersistenceHelper.CreateContext())
            {
                var repositories = PersistenceHelper.CreateRepositoryWrapper(context);

                var patchManager = new PatchManager(new PatchRepositories(repositories));

                IList<Operator> sineOperators = repositories.OperatorRepository
                                                            .GetAll()
                                                            .Where(x => x.GetOperatorTypeEnum() == OperatorTypeEnum.Sine)
                                                            .ToArray();
                for (int i = 0; i < sineOperators.Count; i++)
                {
                    Operator sineOperator = sineOperators[i];

                    patchManager.Patch = sineOperator.Patch;

                    var sine = new OperatorWrapper_Sine(sineOperator);
                    throw new Exception("Process cannot be run anymore, since the data has been migrated so that there are no more volumes anymore.");
                    //if (sine.Volume != null)
                    {
                        var multiply = patchManager.Multiply();

                        foreach (Inlet inlet in sine.Result.ConnectedInlets.ToArray())
                        {
                            inlet.LinkTo(multiply.Result);
                        }

                        //multiply.OperandA = sine.Volume;
                        multiply.OperandB = sine.Result;

                        //sine.Volume = null;

                        VoidResult result = patchManager.Save();
                        if (!result.Successful)
                        {
                            string formattedMessages = String.Join(" ", result.Messages.Select(x => x.Text));
                            throw new Exception(formattedMessages);
                        }
                    }

                    string progressMessage = String.Format("Migrated sine {0}/{1}.", i + 1, sineOperators.Count);
                    progressCallback(progressMessage);
                }

                context.Commit();

                progressCallback(String.Format("Done migrating {0} sine operators.", sineOperators.Count));
            }
        }

        public static void AddSampleOperatorFrequencyInlets(Action<string> progressCallback)
        {
            if (progressCallback == null) throw new NullException(() => progressCallback);

            progressCallback(String.Format("Starting {0}...", MethodBase.GetCurrentMethod().Name));

            using (IContext context = PersistenceHelper.CreateContext())
            {
                var repositories = PersistenceHelper.CreateRepositoryWrapper(context);

                var patchManager = new PatchManager(new PatchRepositories(repositories));

                IList<Operator> sampleOperators = repositories.OperatorRepository
                                                              .GetAll()
                                                              .Where(x => x.GetOperatorTypeEnum() == OperatorTypeEnum.Sample)
                                                              .ToArray();

                for (int i = 0; i < sampleOperators.Count; i++)
                {
                    Operator sampleOperator = sampleOperators[i];

                    patchManager.Patch = sampleOperator.Patch;

                    var sampleOperatorWrapper = new OperatorWrapper_Sample(sampleOperator, repositories.SampleRepository);

                    if (sampleOperator.Inlets.Count == 0)
                    {
                        Inlet inlet = patchManager.CreateInlet(sampleOperator);

                        var numberWrapper = patchManager.Number(DEFAULT_FREQUENCY);

                        sampleOperatorWrapper.Frequency = numberWrapper;

                        VoidResult result = patchManager.Save();
                        if (!result.Successful)
                        {
                            string formattedMessages = String.Join(" ", result.Messages.Select(x => x.Text));
                            throw new Exception(formattedMessages);
                        }
                    }

                    string progressMessage = String.Format("Migrated sample operator {0}/{1}.", i + 1, sampleOperators.Count);
                    progressCallback(progressMessage);
                }

                context.Commit();

                progressCallback(String.Format("Done migrating {0} sample operators.", sampleOperators.Count));
            }
        }
    }
}
