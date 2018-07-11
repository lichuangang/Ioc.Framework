using Li.Framework.Entitys;
using MongoDB.Driver;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace Li.Framework.Repositorys
{
    /* ==============================================================================
     * 描述：MongoRepository
     * 创建人：李传刚 2017/8/24 13:58:53
     * ==============================================================================
     */
    public class MongoRepository<T, TKey> where T : IdKey<TKey>
    {
        public virtual string CollectionName
        {
            get { return typeof(T).Name; }
        }

        protected abstract IMongoCollection<T> Collection { get; }

        public virtual void InsertOne(T book)
        {
            Collection.InsertOne(book);
        }

        public virtual void InsertMany(IEnumerable<T> entities)
        {
            Collection.InsertMany(entities);
        }

        public virtual void ReplaceOne(T book)
        {
            Collection.ReplaceOne(o => o.Id.Equals(book.Id), book);
        }

        public virtual IQueryable<T> GetQuery()
        {
            return Collection.AsQueryable();
        }

        public int Delete(Expression<Func<T, bool>> filter)
        {
            return (int)Collection.DeleteMany(filter).DeletedCount;
        }

        public int Update(Expression<Func<T, bool>> filter, UpdateDefinition<T> updateDefinition)
        {
            return (int)Collection.UpdateMany(filter, updateDefinition).ModifiedCount;
        }

        public virtual int GetCount()
        {
            return GetQuery().Count();
        }

        public T Get(TKey key)
        {
            return GetQuery().Single(o => o.Id.Equals(key));
        }

        public TKey MaxId()
        {
            var entity = GetQuery().OrderByDescending(o => o.Id).FirstOrDefault();
            if (entity == null)
            {
                return default(TKey);
            }
            return entity.Id;
        }
    }

    public class MongoDatabase
    {
        public IMongoDatabase Database { get; private set; }

        protected MongoDatabase(string connectionString)
        {
            var mongoUrl = new MongoUrl(connectionString);
            var client = new MongoClient(connectionString);
            Database = client.GetDatabase(mongoUrl.DatabaseName);
        }
    }
}
