//using System.Collections.Generic;
//using JJ.Business.Synthesizer.Extensions;
//using JJ.Data.Synthesizer.Entities;
//using JJ.Data.Synthesizer.RepositoryInterfaces;
//using JJ.Framework.Exceptions;
//using JJ.Framework.Mathematics;

//namespace JJ.Business.Synthesizer
//{
//	public class EntityPositionFacade
//	{
//		private const int MIN_RANDOM_X = 50;
//		private const int MAX_RANDOM_X = 300;
//		private const int MIN_RANDOM_Y = 50;
//		private const int MAX_RANDOM_Y = 400;

//		private readonly IEntityPositionRepository _entityPositionRepository;
//		private readonly IIDRepository _idRepository;

//		private readonly Dictionary<int, EntityPosition> _operatorPositionDictionary = new Dictionary<int, EntityPosition>();

//		public EntityPositionFacade(IEntityPositionRepository entityPositionRepository, IIDRepository idRepository)
//		{
//			_entityPositionRepository = entityPositionRepository ?? throw new NullException(() => entityPositionRepository);
//			_idRepository = idRepository ?? throw new NullException(() => idRepository);
//		}

//		public EntityPosition GetOperatorPosition(int operatorID)
//		{
//			EntityPosition entityPosition = TryGetOperatorPosition(operatorID);
//			if (entityPosition == null)
//			{
//				throw new NotFoundException<EntityPosition>(new { operatorID });
//			}
//			return entityPosition;
//		}

//		public EntityPosition TryGetOperatorPosition(int operatorID)
//		{
//			// ReSharper disable once InvertIf
//			if (!_operatorPositionDictionary.TryGetValue(operatorID, out EntityPosition entityPosition))
//			{
//				string entityTypeName = typeof(Operator).Name;
//				int entityID = operatorID;

//				entityPosition = _entityPositionRepository.TryGetByEntityTypeNameAndEntityID(entityTypeName, entityID);

//				if (entityPosition != null)
//				{
//					_operatorPositionDictionary.Add(entityID, entityPosition);
//				}
//			}

//			return entityPosition;
//		}

//		public EntityPosition GetOrCreateOperatorPosition(int operatorID)
//		{
//			EntityPosition entityPosition = TryGetOperatorPosition(operatorID);

//			// ReSharper disable once InvertIf
//			if (entityPosition == null)
//			{
//				string entityTypeName = typeof(Operator).Name;
//				int entityID = operatorID;

//				entityPosition = new EntityPosition
//				{
//					ID = _idRepository.GetID(),
//					EntityTypeName = entityTypeName,
//					EntityID = entityID,
//					X = Randomizer.GetInt32(MIN_RANDOM_X, MAX_RANDOM_X),
//					Y = Randomizer.GetInt32(MIN_RANDOM_Y, MAX_RANDOM_Y)
//				};
//				_entityPositionRepository.Insert(entityPosition);

//				_operatorPositionDictionary.Add(entityID, entityPosition);
//			}

//			return entityPosition;
//		}

//		public EntityPosition SetOrCreateOperatorPosition(int operatorID, float x, float y)
//		{
//			if (!_operatorPositionDictionary.TryGetValue(operatorID, out EntityPosition entityPosition))
//			{
//				string entityTypeName = typeof(Operator).Name;
//				int entityID = operatorID;

//				entityPosition = _entityPositionRepository.TryGetByEntityTypeNameAndEntityID(entityTypeName, entityID);
//				if (entityPosition == null)
//				{
//					entityPosition = new EntityPosition
//					{
//						ID = _idRepository.GetID(),
//						EntityTypeName = entityTypeName,
//						EntityID = entityID
//					};
//					_entityPositionRepository.Insert(entityPosition);
//				}

//				_operatorPositionDictionary.Add(operatorID, entityPosition);
//			}

//			entityPosition.X = x;
//			entityPosition.Y = y;

//			return entityPosition;
//		}

//		/// <summary> Moves the operator, and along with it, the operators it 'owns'. </summary>
//		public void MoveOperator(Operator op, float x, float y)
//		{
//			if (op == null) throw new NullException(() => op);

//			EntityPosition entityPosition = GetOrCreateOperatorPosition(op.ID);

//			float deltaX = x - entityPosition.X;
//			float deltaY = y - entityPosition.Y;

//			entityPosition.X += deltaX;
//			entityPosition.Y += deltaY;

//			// Move owned operators along with the owner.

//			IEnumerable<Operator> ownedOperators = op.GetOwnedOperators();
//			foreach (Operator ownedOperator in ownedOperators)
//			{
//				EntityPosition entityPosition2 = GetOrCreateOperatorPosition(ownedOperator.ID);
//				entityPosition2.X += deltaX;
//				entityPosition2.Y += deltaY;
//			}
//		}

//		public int DeleteOrphans() => _entityPositionRepository.DeleteOrphans();
//	}
//}