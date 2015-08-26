using System;
using System.Collections.Generic;
using System.Linq;
using JJ.Framework.Reflection.Exceptions;
using JJ.Data.Synthesizer;
using JJ.Presentation.Synthesizer.ViewModels;
using JJ.Presentation.Synthesizer.ViewModels.Entities;
using JJ.Presentation.Synthesizer.ToEntity;
using JJ.Presentation.Synthesizer.Helpers;
using JJ.Business.Synthesizer.Managers;
using JJ.Framework.Common;
using JJ.Business.Synthesizer.Validation;
using JJ.Framework.Validation;
using JJ.Business.Synthesizer.EntityWrappers;
using JJ.Business.Synthesizer.Helpers;
using JJ.Business.Synthesizer.Extensions;
using JJ.Business.Synthesizer.Enums;
using JJ.Business.CanonicalModel;
using JJ.Presentation.Synthesizer.Resources;
using System.IO;

namespace JJ.Presentation.Synthesizer.Presenters
{
    internal class PatchDetailsPresenter
    {
        private PatchRepositories _repositories;
        private PatchManager _patchManager;

        public PatchDetailsViewModel ViewModel { get; set; }

        public PatchDetailsPresenter(PatchRepositories repositories)
        {
            if (repositories == null) throw new NullException(() => repositories);

            _repositories = repositories;

            _patchManager = new PatchManager(_repositories);
        }

        public void Show()
        {
            AssertViewModel();

            ViewModel.Visible = true;
        }

        public void Close()
        {
            AssertViewModel();

            Update();

            ViewModel.Visible = false;
        }

        public void LoseFocus()
        {
            Update();
        }

        private void Update()
        {
            AssertViewModel();

            Patch patch = ViewModel.ToEntityWithRelatedEntities(
                _repositories.PatchRepository,
                _repositories.OperatorRepository,
                _repositories.OperatorTypeRepository,
                _repositories.InletRepository,
                _repositories.OutletRepository,
                _repositories.EntityPositionRepository);

            // TODO: Use PatchManager?
            IValidator validator = new PatchValidator_Recursive(patch, _repositories.CurveRepository, _repositories.SampleRepository, _repositories.DocumentRepository, alreadyDone: new HashSet<object>());
            if (!validator.IsValid)
            {
                ViewModel.ValidationMessages = validator.ValidationMessages.ToCanonical();
                ViewModel.Successful = false;
            }
            else
            {
                ViewModel.ValidationMessages = new List<Message>();
                ViewModel.Successful = true;
            }
        }

        public void MoveOperator(int operatorID, float centerX, float centerY)
        {
            AssertViewModel();

            OperatorViewModel operatorViewModel = ViewModel.Entity.Operators
                                                                  .Where(x => x.ID == operatorID)
                                                                  .Single();
            operatorViewModel.CenterX = centerX;
            operatorViewModel.CenterY = centerY;
        }

        public void ChangeInputOutlet(int inletID, int inputOutletID)
        {
            AssertViewModel();

            InletViewModel inletViewModel = ViewModel.Entity.Operators
                                                            .SelectMany(x => x.Inlets)
                                                            .Where(x => x.ID == inletID)
                                                            .Single();

            OutletViewModel inputOutletViewModel = ViewModel.Entity.Operators
                                                                   .SelectMany(x => x.Outlets)
                                                                   .Where(x => x.ID == inputOutletID)
                                                                   .Single();
            inletViewModel.InputOutlet = inputOutletViewModel;
        }

        public void SelectOperator(int operatorID)
        {
            SetSelectedOperator(operatorID);
        }
        
        /// <summary>
        /// Deletes the selected operator. Does not delete anything if no operator is selected.
        /// </summary>
        public void DeleteOperator()
        {
            AssertViewModel();

            if (ViewModel.SelectedOperator != null)
            {
                int operatorID = ViewModel.SelectedOperator.ID;

                int listIndex = ViewModel.Entity.Operators.IndexOf(x => x.ID == operatorID);

                OperatorViewModel operatorViewModel = ViewModel.Entity.Operators[listIndex];

                // Unlink related operator's inlets to which the input operator is connected.
                IList<InletViewModel> relatedInletViewModels =  GetConnectedInletViewModels(ViewModel.Entity.Operators, operatorViewModel);
                foreach (InletViewModel relatedInletViewModel in relatedInletViewModels)
                {
                    relatedInletViewModel.InputOutlet = null;
                }

                // Unlink op.Inlets.InputOutlet
                operatorViewModel.Inlets.ForEach(x => x.InputOutlet = null);
                // Unlink op.Inlets
                operatorViewModel.Inlets = new List<InletViewModel>();

                // Unlink op.Outlet[..].Operator
                operatorViewModel.Outlets.ForEach(x => x.Operator = null);
                // Unlink op.Outlets
                operatorViewModel.Outlets = new List<OutletViewModel>();

                ViewModel.Entity.Operators.RemoveAt(listIndex);
            }
        }

        /// <summary>
        /// Gets related operator's inlets to which the input operator is connected
        /// (at the ViewModel level).
        /// </summary>
        private IList<InletViewModel> GetConnectedInletViewModels(IList<OperatorViewModel> allOperatorViewModels, OperatorViewModel inputOperatorViewModel)
        {
            // TODO: This makes operating on the view model to execute the delete action quite expensive.
            // Is it possible and less expensive to do a partial ToEntity and operate on the entity model?
            var list = new List<InletViewModel>();

            foreach (OperatorViewModel operatorViewModel in allOperatorViewModels)
            {
                foreach (InletViewModel inletViewModel in operatorViewModel.Inlets)
                {
                    if (inletViewModel.InputOutlet != null)
                    {
                        if (inletViewModel.InputOutlet.Operator.ID == inputOperatorViewModel.ID)
                        {
                            list.Add(inletViewModel);
                        }
                    }
                }
            }

            return list;
        }

        /// <summary>
        /// Returns the output file path.
        /// This action is quite a hack.
        /// TODO: It should not be a hack and also this action is way too dependent on infrastructure.
        /// </summary>
        public string Play(RepositoryWrapper repositoryWrapper)
        {
            AssertViewModel();

            var config = ConfigurationHelper.GetSection<ConfigurationSection>();
            double duration = config.PatchPlayDurationInSeconds;
            string sampleFilePath = config.PatchPlayHackedSampleFilePath;
            string outputFilePath = config.PatchPlayHackedAudioFileOutputFilePath;

            Patch patch = ToEntity(ViewModel);

            VoidResult result = DoPlay(duration, sampleFilePath, outputFilePath, patch, repositoryWrapper);

            ViewModel.Successful = result.Successful;
            ViewModel.ValidationMessages = result.Messages;

            return outputFilePath;
        }

        private VoidResult DoPlay(double duration, string sampleFilePath, string outputFilePath, Patch patch, RepositoryWrapper repositoryWrapper)
        {
            var result = new VoidResult
            {
                Messages = new List<Message>()
            };

            Operator patchOutlet = patch.Operators
                                        .Where(x => x.GetOperatorTypeEnum() == OperatorTypeEnum.PatchOutlet)
                                        .FirstOrDefault();

            Operator sampleOperator = patch.Operators
                                           .Where(x => x.GetOperatorTypeEnum() == OperatorTypeEnum.Sample)
                                           .FirstOrDefault();

            if (patchOutlet == null)
            {
                result.Successful = false;
                result.Messages.Add(new Message
                {
                    PropertyKey = PropertyNames.PatchOutlet,
                    Text = PresentationMessages.AddPatchOutletToPlayASound
                });
                return result;
            }

            Sample_OperatorWrapper sampleOperatorWrapper = null;

            if (sampleOperator != null)
            {
                // TODO: Refactor out dependency on file system.
                if (!File.Exists(sampleFilePath))
                {
                    result.Successful = false;
                    result.Messages.Add(new Message
                    {
                        PropertyKey = PropertyNames.Patch,
                        Text = PresentationMessageFormatter.SampleFileDoesNotExistWithLocation(Path.GetFullPath(sampleFilePath))
                    });
                    return result;
                }

                SampleManager sampleManager = CreateSampleManager(repositoryWrapper);

                using (Stream sampleStream = new FileStream(sampleFilePath, FileMode.Open, FileAccess.Read, FileShare.Read))
                {
                    Sample sample = sampleManager.CreateSample(sampleStream);

                    sampleOperatorWrapper = new Sample_OperatorWrapper(sampleOperator, _repositories.SampleRepository);
                    sampleOperatorWrapper.Sample = sample;
                }
            }

            AudioFileOutputManager audioFileOutputManager = CreateAudioFileOutputManager(repositoryWrapper);

            AudioFileOutput audioFileOutput = audioFileOutputManager.CreateWithRelatedEntities();
            audioFileOutput.FilePath = outputFilePath;
            audioFileOutput.Duration = duration;

            var patchOutletWrapper = new PatchOutlet_OperatorWrapper(patchOutlet);
            Outlet outlet = patchOutletWrapper.Result;
            audioFileOutput.AudioFileOutputChannels[0].Outlet = outlet;

            audioFileOutputManager.Execute(audioFileOutput);

            if (sampleOperatorWrapper != null)
            {
                sampleOperatorWrapper.Sample = null;
            }

            return new VoidResult
            {
                Successful = true,
                Messages = new List<Message>()
            };
        }

        // Helpers

        private Patch ToEntity(PatchDetailsViewModel userInput)
        {
            Patch patch = userInput.ToEntityWithRelatedEntities(
                _repositories.PatchRepository,
                _repositories.OperatorRepository,
                _repositories.OperatorTypeRepository,
                _repositories.InletRepository,
                _repositories.OutletRepository,
                _repositories.EntityPositionRepository);

            return patch;
        }

        /// <summary>
        /// The SelectedOperator is non-persisted data.
        /// This method sets the selected operator in the view model.
        /// It uses the Operator's ID for this.
        /// It goes through all the operators in the view model,
        /// setting IsSelected to false unless it is the selected operator,
        /// and sets the details view model's SelectedOperator property.
        /// </summary>
        private void SetSelectedOperator(int operatorID)
        {
            ViewModel.SelectedOperator = null;

            foreach (OperatorViewModel operatorViewModel in ViewModel.Entity.Operators)
            {
                if (operatorViewModel.ID == operatorID)
                {
                    operatorViewModel.IsSelected = true;
                    ViewModel.SelectedOperator = operatorViewModel;
                }
                else
                {
                    operatorViewModel.IsSelected = false;
                }
            }
        }

        private SampleManager CreateSampleManager(RepositoryWrapper repositoryWrapper)
        {
            var sampleRepositories = new SampleRepositories(repositoryWrapper);
            var manager = new SampleManager(sampleRepositories);
            return manager;
        }

        private AudioFileOutputManager CreateAudioFileOutputManager(RepositoryWrapper repositoryWrapper)
        {
            var manager = new AudioFileOutputManager(
                repositoryWrapper.AudioFileOutputRepository,
                repositoryWrapper.AudioFileOutputChannelRepository,
                repositoryWrapper.SampleDataTypeRepository,
                repositoryWrapper.SpeakerSetupRepository,
                repositoryWrapper.AudioFileFormatRepository,
                repositoryWrapper.CurveRepository,
                repositoryWrapper.SampleRepository,
                repositoryWrapper.DocumentRepository,
                repositoryWrapper.IDRepository);

            return manager;
        }

        // Helpers

        private void AssertViewModel()
        {
            if (ViewModel == null) throw new NullException(() => ViewModel);
        }
    }
}
