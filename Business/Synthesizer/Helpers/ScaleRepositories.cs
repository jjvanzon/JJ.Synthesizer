using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using JJ.Data.Synthesizer.DefaultRepositories.Interfaces;
using JJ.Framework.Reflection.Exceptions;

namespace JJ.Business.Synthesizer.Helpers
{
    public class ScaleRepositories
    {
        public IScaleRepository ScaleRepository { get; set; }
        public IScaleTypeRepository ScaleTypeRepository { get; set; }
        public IToneRepository ToneRepository { get; set; }
        public IIDRepository IDRepository { get; set; }

        public ScaleRepositories(RepositoryWrapper repositoryWrapper)
        {
            if (repositoryWrapper == null) throw new NullException(() => repositoryWrapper);

            ScaleRepository = repositoryWrapper.ScaleRepository;
            ToneRepository = repositoryWrapper.ToneRepository;
            ScaleTypeRepository = repositoryWrapper.ScaleTypeRepository;
            IDRepository = repositoryWrapper.IDRepository;
        }

        public ScaleRepositories(
            IScaleRepository scaleRepository, 
            IToneRepository toneRepository, 
            IScaleTypeRepository scaleTypeRepository,
            IIDRepository idRepository)
        {
            if (scaleRepository == null) throw new NullException(() => scaleRepository);
            if (toneRepository == null) throw new NullException(() => toneRepository);
            if (scaleTypeRepository == null) throw new NullException(() => scaleTypeRepository);
            if (idRepository == null) throw new NullException(() => idRepository);

            ScaleRepository = scaleRepository;
            ToneRepository = toneRepository;
            ScaleTypeRepository = scaleTypeRepository;
            IDRepository = idRepository;
        }

        public void Commit()
        {
            ScaleRepository.Commit();
        }

        public void Rollback()
        {
            ScaleRepository.Rollback();
        }

        public void Flush()
        {
            ScaleRepository.Flush();
        }
    }
}
