using JJ.Framework.Mathematics;
using JJ.Framework.Reflection.Exceptions;
using JJ.Data.Synthesizer;
using JJ.Data.Synthesizer.DefaultRepositories.Interfaces;
using System.Collections.Generic;

namespace JJ.Business.Synthesizer.Managers
{
    public class EntityPositionManager
    {
        private const int MIN_RANDOM_X = 50;
        private const int MAX_RANDOM_X = 300;
        private const int MIN_RANDOM_Y = 50;
        private const int MAX_RANDOM_Y = 400;

        private IEntityPositionRepository _entityPositionRepository;
        private IIDRepository _idRepository;

        private Dictionary<int, EntityPosition> _operatorPositionDictionary = new Dictionary<int, EntityPosition>();

        public EntityPositionManager(IEntityPositionRepository entityPositionRepository, IIDRepository idRepository)
        {
            if (entityPositionRepository == null) throw new NullException(() => entityPositionRepository);
            if (idRepository == null) throw new NullException(() => idRepository);

            _entityPositionRepository = entityPositionRepository;
            _idRepository = idRepository;
        }

        public EntityPosition GetOrCreateOperatorPosition(Operator op)
        {
            if (op == null) throw new NullException(() => op);

            return GetOrCreateOperatorPosition(op.ID);
        }

        public EntityPosition GetOrCreateOperatorPosition(int operatorID)
        {
            EntityPosition entityPosition;
            if (!_operatorPositionDictionary.TryGetValue(operatorID, out entityPosition))
            {
                string entityTypeName = typeof(Operator).Name;
                int entityID = operatorID;

                entityPosition = _entityPositionRepository.TryGetByEntityTypeNameAndEntityID(entityTypeName, entityID);
                if (entityPosition == null)
                {
                    entityPosition = new EntityPosition();
                    entityPosition.ID = _idRepository.GetID();
                    entityPosition.EntityTypeName = entityTypeName;
                    entityPosition.EntityID = entityID;
                    entityPosition.X = Randomizer.GetInt32(MIN_RANDOM_X, MAX_RANDOM_X);
                    entityPosition.Y = Randomizer.GetInt32(MIN_RANDOM_Y, MAX_RANDOM_Y);
                    _entityPositionRepository.Insert(entityPosition);
                }

                _operatorPositionDictionary.Add(entityID, entityPosition);
            }
            
            return entityPosition;
        }

        public EntityPosition SetOrCreateOperatorPosition(int operatorID, float x, float y)
        {
            EntityPosition entityPosition;
            if (!_operatorPositionDictionary.TryGetValue(operatorID, out entityPosition))
            {
                string entityTypeName = typeof(Operator).Name;
                int entityID = operatorID;

                entityPosition = _entityPositionRepository.TryGetByEntityTypeNameAndEntityID(entityTypeName, entityID);
                if (entityPosition == null)
                {
                    entityPosition = new EntityPosition();
                    entityPosition.ID = _idRepository.GetID();
                    entityPosition.EntityTypeName = entityTypeName;
                    entityPosition.EntityID = entityID;
                    _entityPositionRepository.Insert(entityPosition);
                }

                _operatorPositionDictionary.Add(operatorID, entityPosition);
            }

            entityPosition.X = x;
            entityPosition.Y = y;

            return entityPosition;
        }
    }
}
