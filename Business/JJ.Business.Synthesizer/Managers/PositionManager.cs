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
    public class PositionManager
    {
        private IEntityPointRepository _entityPointRepository;

        private Dictionary<int, EntityPoint> _operatorPointDictionary = new Dictionary<int, EntityPoint>();

        public PositionManager(IEntityPointRepository entityPointRepository)
        {
            if (entityPointRepository == null) throw new NullException(() => entityPointRepository);

            _entityPointRepository = entityPointRepository;
        }

        public EntityPoint GetOperatorPosition(Operator op)
        {
            if (op == null) throw new NullException(() => op);

            EntityPoint entityPoint;
            if (!_operatorPointDictionary.TryGetValue(op.ID, out entityPoint))
            {
                entityPoint = _entityPointRepository.GetByEntityTypeNameAndID(op.GetType().Name, op.ID);
                _operatorPointDictionary.Add(op.ID, entityPoint);
            }
            
            return entityPoint;
        }
    }
}
