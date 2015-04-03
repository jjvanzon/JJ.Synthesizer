using JJ.Framework.Mathematics;
using JJ.Framework.Reflection.Exceptions;
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
        private const int MIN_RANDOM_X = 50;
        private const int MAX_RANDOM_X = 300;
        private const int MIN_RANDOM_Y = 50;
        private const int MAX_RANDOM_Y = 400;

        private IEntityPositionRepository _entityPositionRepository;

        private Dictionary<int, EntityPosition> _operatorPositionDictionary = new Dictionary<int, EntityPosition>();

        public EntityPositionManager(IEntityPositionRepository entityPositionRepository)
        {
            if (entityPositionRepository == null) throw new NullException(() => entityPositionRepository);

            _entityPositionRepository = entityPositionRepository;
        }

        public EntityPosition GetOrCreateOperatorPosition(Operator op)
        {
            if (op == null) throw new NullException(() => op);

            EntityPosition entityPosition;
            if (!_operatorPositionDictionary.TryGetValue(op.ID, out entityPosition))
            {
                int entityID = op.ID;
                string entityTypeName = op.GetType().Name;

                entityPosition = _entityPositionRepository.TryGetByEntityTypeNameAndID(entityTypeName, entityID);
                if (entityPosition == null)
                {
                    entityPosition = _entityPositionRepository.Create();
                    entityPosition.EntityTypeName = entityTypeName;
                    entityPosition.EntityID = entityID;
                    entityPosition.X = Randomizer.GetInt32(MIN_RANDOM_X, MAX_RANDOM_X);
                    entityPosition.Y = Randomizer.GetInt32(MIN_RANDOM_Y, MAX_RANDOM_Y);
                }

                _operatorPositionDictionary.Add(entityID, entityPosition);
            }
            
            return entityPosition;
        }
    }
}
