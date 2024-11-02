EntityStore (JJ.Framework.Persistence.Memory) (2024-10-25):

    
        public TEntity Create()
        {
            TEntity entity = new TEntity();


            lock (_lock)
            {
                object id = GetNewIdentity();
                SetIDOfEntity(entity, id);

                //if (id == null)
                //{
                //    throw new Exception($"Error adding entity of type {typeof(TEntity).Name} to dictionary: "+
                //                        $"{nameof(id)} is null.");
                //}
                //
                //if (entity == null)
                //{
                //    throw new Exception($"Error adding entity of type {typeof(TEntity).Name} to dictionary: "+
                //                        $"{nameof(entity)} is null.");
                //}
                //
                //if (_dictionary.ContainsKey(id))
                //{
                //    throw new Exception($"Error adding entity of type {typeof(TEntity).Name} to dictionary: "+
                //                        $"key with { new {id} } already present.");
                //}

                _dictionary.Add(id, entity);
            }

            return entity;
        }


ChannelRepository (JJ.Framework.Persistence.Memory) (2024-10-25):

        //private static bool _initialized;
        //private static readonly object _initLock = new object();

        private readonly object _lock = new object();

        public ChannelRepository(IContext context)
            : base(context)
        {
            //lock (_initLock)
            //{
            //    if (_initialized) return;
            //    _initialized = true;
            //}

SpeakerSetupRepository (JJ.Framework.Persistence.Memory) (2024-10-25):

        public override SpeakerSetup GetWithRelatedEntities(int id)
        {
            SpeakerSetup entity = Get(id);

            //ISpeakerSetupChannelRepository childRepository = RepositoryHelper.GetSpeakerSetupChannelRepository(_context);
            ISpeakerSetupChannelRepository childRepository = new SpeakerSetupChannelRepository(_context);

            lock (_lock)
            {
                if (entity.SpeakerSetupChannels.Count == 0)
                {
                    entity.SpeakerSetupChannels = childRepository.GetAll().ToList().Where(x => x.SpeakerSetup.ID == id).Distinct().ToList();

                    if (entity.ID == 1 && // SpeakerSetup with ID 1 = Mono
                        entity.SpeakerSetupChannels.Count != 1)
                    {
                        throw new Exception($"{nameof(SpeakerSetup)} = {entity.Name} and SpeakerSetup.SpeakerSetupChannels.Count = {entity.SpeakerSetupChannels.Count}.");
                    }
                }
            }

            return entity;
        }


        public override Channel GetWithRelatedEntities(int id)
        {
            Channel entity = Get(id);

            //ISpeakerSetupChannelRepository childRepository = RepositoryHelper.GetSpeakerSetupChannelRepository(_context);
            ISpeakerSetupChannelRepository childRepository = new SpeakerSetupChannelRepository(_context);

            lock (_lock)
            {
                if (entity.SpeakerSetupChannels.Count == 0)
                {
                    entity.SpeakerSetupChannels = childRepository.GetAll().ToList().Where(x => x.Channel.ID == id).ToList();

                    if (entity.ID == 1 && // Channel with ID 1 = Single (of Mono)
                        entity.SpeakerSetupChannels.Count != 1)
                    {
                        throw new Exception($"{nameof(Channel)} = {entity.Name} and Channel.SpeakerSetupChannels.Count = {entity.SpeakerSetupChannels.Count}.");
                    }
                }
            }

            return entity;
        }


AudioFileWishes (2025-10-25):


            // Spoiler alert: It was 0.
            //throw new Exception(
            //    "channelInputs.Count.ToSpeakerSetupEnum().ToEntity().SpeakerSetupChannels.Count = " + 
            //    channelInputs.Count.ToSpeakerSetupEnum().ToEntity().SpeakerSetupChannels.Count);
            //if (channelInputs.Count.ToSpeakerSetupEnum().ToEntity().SpeakerSetupChannels.Count > 2)
            //{
            //    throw new Exception("channelInputs.Count.ToSpeakerSetupEnum().ToEntity().SpeakerSetupChannels.Count > 2");
            //}

            if (audioFileOutput.AudioFileOutputChannels.Count > 2)
            {
                throw new Exception("audioFileOutput.AudioFileOutputChannels.Count > 2" + Environment.NewLine +
                                    "audioFileOutput.AudioFileOutputChannels.Count = " + audioFileOutput.AudioFileOutputChannels.Count);
            }
