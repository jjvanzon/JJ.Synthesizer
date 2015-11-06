using System;
using System.Collections.Generic;
using System.Linq;
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
        public static void MigrateSineVolumes(Action<string> progressCallback)
        {
            if (progressCallback == null) throw new NullException(() => progressCallback);

            progressCallback("Starting Migrate Sine Volumes...");

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

                progressCallback("Done.");
            }
        }
    }
}
