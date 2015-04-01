using JJ.Framework.Reflection;
using JJ.Persistence.Synthesizer;
using JJ.Persistence.Synthesizer.DefaultRepositories.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JJ.Business.Synthesizer.Managers
{
    public class EntityPositionManager
    {
        private IEntityPositionRepository _entityPositionRepository;

        private Dictionary<int, EntityPosition> _operatorPositionDictionary = new Dictionary<int, EntityPosition>();

        public EntityPositionManager(IEntityPositionRepository entityPositionRepository)
        {
            if (entityPositionRepository == null) throw new NullException(() => entityPositionRepository);

            _entityPositionRepository = entityPositionRepository;
        }

        public EntityPosition GetOperatorPosition(Operator op)
        {
            if (op == null) throw new NullException(() => op);

            EntityPosition entityPosition;
            if (!_operatorPositionDictionary.TryGetValue(op.ID, out entityPosition))
            {
                entityPosition = _entityPositionRepository.GetByEntityTypeNameAndID(op.GetType().Name, op.ID);
                _operatorPositionDictionary.Add(op.ID, entityPosition);
            }
            
            return entityPosition;
        }
    }
}
