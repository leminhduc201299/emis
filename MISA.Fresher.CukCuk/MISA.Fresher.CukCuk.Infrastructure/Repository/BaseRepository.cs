using Dapper;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Options;
using MISA.Fresher.CukCuk.Core.Entities;
using MISA.Fresher.CukCuk.Core.Interfaces.Repository;
using MongoDB.Driver;
using MySqlConnector;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace MISA.Fresher.CukCuk.Infrastructure.Repository
{
    public class BaseRepository<TEntity> : IBaseRepository<TEntity>
    {
        #region Field
        protected readonly IMongoCollection<TEntity> _entityCollection;
        private string _tableName;
        #endregion

        #region Contructor
        public BaseRepository(IOptions<MISAEmisDatabaseSettings> misaEmisDatabaseSettings)
        {
            _tableName = typeof(TEntity).Name;

            var mongoClient = new MongoClient(
                misaEmisDatabaseSettings.Value.ConnectionString);

            var mongoDatabase = mongoClient.GetDatabase(
                misaEmisDatabaseSettings.Value.DatabaseName);

            _entityCollection = mongoDatabase.GetCollection<TEntity>(_tableName);
        }
        #endregion

        #region Method
        public virtual async Task<List<TEntity>> GetAll()
        {
            return await _entityCollection.Find(_ => true).ToListAsync();
        }

        public async Task<TEntity?> GetById(string entityId)
        {
            var where = CreateWhereExpression($"{_tableName}Id", entityId);
            return await _entityCollection.Find(where)
                .FirstOrDefaultAsync();
        }

        public async Task Insert(TEntity entity)
        {
            await _entityCollection.InsertOneAsync(entity);
        }

        public async Task Update(string entityId, TEntity entity)
        {
            var where = CreateWhereExpression($"{_tableName}Id", entityId);
            await _entityCollection.ReplaceOneAsync(where, entity);
        }

        public async Task Delete(string entityId)
        {
            var where = CreateWhereExpression($"{_tableName}Id", entityId);
            await _entityCollection.DeleteOneAsync(where);
        }

        private Expression<Func<TEntity, bool>> CreateWhereExpression(string propertyName, string filterValue)
        {
            var entityType = typeof(TEntity);
            var entity = Expression.Parameter(entityType, "entity");
            var body = Expression.Equal(
                Expression.Property(entity, propertyName),
                Expression.Constant(filterValue));

            return Expression.Lambda<Func<TEntity, bool>>(body, entity);
        }
        #endregion
    }
}
