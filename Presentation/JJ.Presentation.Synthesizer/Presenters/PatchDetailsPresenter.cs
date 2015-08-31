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
using JJ.Business.Synthesizer.Helpers;
using JJ.Business.Synthesizer.Extensions;
using JJ.Business.CanonicalModel;
using JJ.Presentation.Synthesizer.Resources;

namespace JJ.Presentation.Synthesizer.Presenters
{
    internal class PatchDetailsPresenter
    {
        private static double _patchPlayDuration = GetPatchPlayDuration();
        private static string _patchPlayOutputFilePath = GetPatchPlayOutputFilePath();

        private static double GetPatchPlayDuration()
        {
            var config = ConfigurationHelper.GetSection<ConfigurationSection>();
            return config.PatchPlayDurationInSeconds;
        }

        private static string GetPatchPlayOutputFilePath()
        {
            var config = ConfigurationHelper.GetSection<ConfigurationSection>();
            return config.PatchPlayHackedAudioFileOutputFilePath;
        }

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

            Patch patch = ViewModel.ToEntityWithRelatedEntities(_repositories);

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
        /// Deletes the selected operator. 
        /// Produces a validation message if no operator is selected.
        /// </summary>
        public void DeleteOperator()
        {
            AssertViewModel();

            if (ViewModel.SelectedOperator == null)
            {
                ViewModel.Successful = false;
                ViewModel.ValidationMessages.Add(new Message
                {
                    PropertyKey = PresentationPropertyNames.SelectedOperator,
                    Text = PresentationMessages.SelectAnOperatorFirst
                });

                return;
            }

            int operatorID = ViewModel.SelectedOperator.ID;

            int listIndex = ViewModel.Entity.Operators.IndexOf(x => x.ID == operatorID);

            OperatorViewModel operatorViewModel = ViewModel.Entity.Operators[listIndex];

            // Unlink related operator's inlets to which the input operator is connected.
            IList<InletViewModel> relatedInletViewModels = GetConnectedInletViewModels(ViewModel.Entity.Operators, operatorViewModel);
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

            ViewModel.SelectedOperator = null;

            ViewModel.Successful = true;
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
        /// Writes the output of the currently selected operator to an audio file with a configurable duration.
        /// Returns the output file path if ViewModel.Successful.
        /// TODO: This action is too dependent on infrastructure, because the AudioFileOutput business logic is.
        /// Instead of writing to a file it had better write to a stream.
        /// </summary>
        public string Play(RepositoryWrapper repositoryWrapper)
        {
            AssertViewModel();

            if (ViewModel.SelectedOperator == null)
            {
                ViewModel.Successful = false;
                ViewModel.ValidationMessages.Add(new Message
                {
                    PropertyKey = PresentationPropertyNames.SelectedOperator,
                    Text = PresentationMessages.SelectAnOperatorFirst
                });

                return null;
            }

            Operator selectedOperator = _repositories.OperatorRepository.Get(ViewModel.SelectedOperator.ID);
            if (selectedOperator.Outlets.Count != 1)
            {
                ViewModel.Successful = false;
                ViewModel.ValidationMessages.Add(new Message
                {
                    PropertyKey = PresentationPropertyNames.SelectedOperator,
                    Text = PresentationMessages.SelectAnOperatorWithASingleOutlet
                });

                return null;
            }
            Outlet outlet = selectedOperator.Outlets.Single();

            AudioFileOutputManager audioFileOutputManager = CreateAudioFileOutputManager(repositoryWrapper);
            AudioFileOutput audioFileOutput = audioFileOutputManager.CreateWithRelatedEntities();
            audioFileOutput.FilePath = _patchPlayOutputFilePath;
            audioFileOutput.Duration = _patchPlayDuration;
            audioFileOutput.AudioFileOutputChannels[0].Outlet = outlet;

            audioFileOutputManager.Execute(audioFileOutput);

            ViewModel.Successful = true;

            return _patchPlayOutputFilePath;
        }

        // Helpers

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

        private void AssertViewModel()
        {
            if (ViewModel == null) throw new NullException(() => ViewModel);
        }
    }
}
