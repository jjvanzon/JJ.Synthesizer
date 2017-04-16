using JJ.Framework.Mathematics;
using JJ.Framework.Exceptions;
using System.Collections.Generic;
using JJ.Business.Synthesizer.Extensions;
using JJ.Business.Synthesizer.Enums;
using System.Linq;
using JJ.Data.Synthesizer.Entities;
using JJ.Data.Synthesizer.RepositoryInterfaces;

namespace JJ.Business.Synthesizer
{
    public class EntityPositionManager
    {
        private const int MIN_RANDOM_X = 50;
        private const int MAX_RANDOM_X = 300;
        private const int MIN_RANDOM_Y = 50;
        private const int MAX_RANDOM_Y = 400;

        private readonly IEntityPositionRepository _entityPositionRepository;
        private readonly IIDRepository _idRepository;

        private readonly Dictionary<int, EntityPosition> _operatorPositionDictionary = new Dictionary<int, EntityPosition>();

        public EntityPositionManager(IEntityPositionRepository entityPositionRepository, IIDRepository idRepository)
        {
            _entityPositionRepository = entityPositionRepository ?? throw new NullException(() => entityPositionRepository);
            _idRepository = idRepository ?? throw new NullException(() => idRepository);
        }

        public EntityPosition GetOperatorPosition(int operatorID)
        {
            EntityPosition entityPosition = TryGetOperatorPosition(operatorID);
            if (entityPosition == null)
            {
                throw new NotFoundException<EntityPosition>(new { operatorID });
            }
            return entityPosition;
        }

        public EntityPosition TryGetOperatorPosition(int operatorID)
        {
            EntityPosition entityPosition;
            // ReSharper disable once InvertIf
            if (!_operatorPositionDictionary.TryGetValue(operatorID, out entityPosition))
            {
                string entityTypeName = typeof(Operator).Name;
                int entityID = operatorID;

                entityPosition = _entityPositionRepository.TryGetByEntityTypeNameAndEntityID(entityTypeName, entityID);

                if (entityPosition != null)
                {
                    _operatorPositionDictionary.Add(entityID, entityPosition);
                }
            }

            return entityPosition;
        }

        public EntityPosition GetOrCreateOperatorPosition(int operatorID)
        {
            EntityPosition entityPosition = TryGetOperatorPosition(operatorID);

            // ReSharper disable once InvertIf
            if (entityPosition == null)
            {
                string entityTypeName = typeof(Operator).Name;
                int entityID = operatorID;

                entityPosition = new EntityPosition
                {
                    ID = _idRepository.GetID(),
                    EntityTypeName = entityTypeName,
                    EntityID = entityID,
                    X = Randomizer.GetInt32(MIN_RANDOM_X, MAX_RANDOM_X),
                    Y = Randomizer.GetInt32(MIN_RANDOM_Y, MAX_RANDOM_Y)
                };
                _entityPositionRepository.Insert(entityPosition);

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
                    entityPosition = new EntityPosition
                    {
                        ID = _idRepository.GetID(),
                        EntityTypeName = entityTypeName,
                        EntityID = entityID
                    };
                    _entityPositionRepository.Insert(entityPosition);
                }

                _operatorPositionDictionary.Add(operatorID, entityPosition);
            }

            entityPosition.X = x;
            entityPosition.Y = y;

            return entityPosition;
        }

        /// <summary>
        /// Moves the operator, and along with it, the operators it 'owns'.
        /// </summary>
        public void MoveOperator(Operator op, float x, float y)
        {
            if (op == null) throw new NullException(() => op);

            EntityPosition entityPosition = GetOrCreateOperatorPosition(op.ID);

            float deltaX = x - entityPosition.X;
            float deltaY = y - entityPosition.Y;

            entityPosition.X += deltaX;
            entityPosition.Y += deltaY;

            // Move owned operators along with the owner.

            // Note that the owned operator can be connected to the same owner operator twice (in two different inlets),
            // but make sure that this does not result in applying the move twice (with the Distinct operator below).

            IEnumerable<Operator> ownedOperators = op.Inlets
                                                     .Where(o => o.InputOutlet != null)
                                                     .Where(o => GetOperatorIsOwned(o.InputOutlet.Operator))
                                                     .Select(o => o.InputOutlet.Operator)
                                                     .Distinct()
                                                     .ToArray();

            foreach (Operator ownedOperator in ownedOperators)
            {
                EntityPosition entityPosition2 = GetOrCreateOperatorPosition(ownedOperator.ID);
                entityPosition2.X += deltaX;
                entityPosition2.Y += deltaY;
            }
        }

        /// <summary>
        /// A Number Operator can be considered 'owned' by another operator if
        /// it is the only operator it is connected to.
        /// In that case it is convenient that the Number Operator moves along
        /// with the operator it is connected to.
        /// TODO: Remove the following line of comment, that is about to become irrelevant (2016-02-15):
        /// In the Vector Graphics we accomplish this by making the Number Operator Rectangle a child of the owning Operator's Rectangle. 
        /// But also in the MoveOperator action we must move the owned operators along with their owner.
        /// </summary>
        public static bool GetOperatorIsOwned(Operator entity)
        {
            if (entity == null) throw new NullException(() => entity);

            // ReSharper disable once InvertIf
            if (entity.Outlets.Count > 0)
            {
                bool isOwned = entity.GetOperatorTypeEnum() == OperatorTypeEnum.Number &&
                               // Make sure the connected inlets are all of the same operator.
                               entity.Outlets[0].ConnectedInlets.Select(x => x.Operator).Distinct().Count() == 1;

                return isOwned;
            }

            return false;
        }

        public int DeleteOrphans()
        {
            return _entityPositionRepository.DeleteOrphans();
        }
    }
}