using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using MongoDB.Bson;
using MongoDB.Driver;
using MongoDB.Driver.Wrappers;

namespace SND.Lib
{
    public class MongoDacHelper
    {
        private string serverSetting;
        private string databaseName;
        private string collectionName;

        public MongoDacHelper(string collection)
        {
            serverSetting = ConfigurationManager.ConnectionStrings["mongoCon"].ConnectionString;
            databaseName = ConfigurationManager.AppSettings["DataBase"];
            collectionName = collection;
        }

        #region Get Database Instance

        public MongoDatabase GetDatabase()
        {
            MongoServer mongo = MongoServer.Create(serverSetting);
            return mongo.GetDatabase(databaseName);
        }

        #endregion

        #region Get Collection Instance

        public MongoCollection<BsonDocument> GetCollection()
        {
            MongoServer mongo = MongoServer.Create(serverSetting);
            return mongo.GetDatabase(databaseName).GetCollection(collectionName);
        }

        #endregion

        #region Group Function

        public IEnumerable<BsonDocument> GetGroupQueryResults(IMongoQuery query, IMongoGroupBy groupBy, BsonDocument initial, BsonJavaScript reduce, BsonJavaScript finalize)
        {
            MongoServer mongo = MongoServer.Create(serverSetting);
            return mongo.GetDatabase(databaseName).GetCollection(collectionName).Group(query, groupBy, initial, reduce, finalize);
        }
        #endregion

        #region MapReduce Function

        public MapReduceResult GetMapReduceResults(BsonJavaScript map, BsonJavaScript reduce, IMongoMapReduceOptions option)
        {
            MongoServer mongo = MongoServer.Create(serverSetting);
            return mongo.GetDatabase(databaseName).GetCollection(collectionName).MapReduce(map, reduce, option);
        }
        #endregion

        #region MongoDac Admin function List
        public virtual List<string> CollectionList()
        {
            MongoServer mongo = MongoServer.Create(serverSetting);
            return mongo.GetDatabase(databaseName).GetCollectionNames().ToList();
        }

        #endregion

        #region MongoDac Count function List
        // Count
        public virtual long Count(IMongoQuery query)
        {
            MongoServer mongo = MongoServer.Create(serverSetting);

            return mongo.GetDatabase(databaseName).GetCollection(collectionName).Count(query);
        }

        public virtual long Count(object query)
        {
            return Count(new QueryWrapper(query));
        }

        public virtual long Count(ObjectId id)
        {
            return Count(new { Id = id });
        }

        public virtual long Count(string id)
        {
            return Count(new ObjectId(id));
        }

        public virtual long Count()
        {
            return CountAll();
        }

        // Count All
        public virtual long CountAll()
        {
            MongoServer mongo = MongoServer.Create(serverSetting);

            return mongo.GetDatabase(databaseName).GetCollection(collectionName).Count();
        }

        #endregion

        #region MongoDac Update function List

        public virtual void Save<T>(T instance) where T : class
        {
            MongoServer mongo = MongoServer.Create(serverSetting);
            mongo.GetDatabase(databaseName).GetCollection(collectionName).Save(instance);
        }

        public virtual void Save(IMongoQuery query, IMongoUpdate update)
        {
            MongoServer mongo = MongoServer.Create(serverSetting);

            mongo.GetDatabase(databaseName).GetCollection(collectionName).Update(query, update, UpdateFlags.None);
        }


        public virtual void Save(IMongoQuery query, IMongoUpdate update, UpdateFlags updateflag)
        {
            MongoServer mongo = MongoServer.Create(serverSetting);

            mongo.GetDatabase(databaseName).GetCollection(collectionName).Update(query, update, updateflag);
        }

        //SafeMode

        public virtual void Save<T>(T instance, SafeMode safeMode) where T : class
        {
            MongoServer mongo = MongoServer.Create(serverSetting);

            mongo.GetDatabase(databaseName).GetCollection(collectionName).Save(instance, safeMode);
        }

        public virtual void Save(IMongoQuery query, IMongoUpdate update, SafeMode safeMode)
        {
            MongoServer mongo = MongoServer.Create(serverSetting);

            mongo.GetDatabase(databaseName).GetCollection(collectionName).Update(query, update, UpdateFlags.None, safeMode);
        }


        public virtual void Save(IMongoQuery query, IMongoUpdate update, UpdateFlags updateflag, SafeMode safeMode)
        {
            MongoServer mongo = MongoServer.Create(serverSetting);

            mongo.GetDatabase(databaseName).GetCollection(collectionName).Update(query, update, updateflag, safeMode);
        }

        #endregion

        #region MongoDac Insert function List
        public virtual void Insert<T>(T instance) where T : class
        {
            MongoServer mongo = MongoServer.Create(serverSetting);

            mongo.GetDatabase(databaseName).GetCollection(collectionName).Insert(instance);
        }

        public virtual void InsertBatch<T>(List<T> instance) where T : class
        {
            MongoServer mongo = MongoServer.Create(serverSetting);

            mongo.GetDatabase(databaseName).GetCollection(collectionName).InsertBatch(instance);
        }

        //safeMode

        public virtual void Insert<T>(T instance, SafeMode safeMode) where T : class
        {
            MongoServer mongo = MongoServer.Create(serverSetting);

            mongo.GetDatabase(databaseName).GetCollection(collectionName).Insert(instance, safeMode);
        }

        public virtual void InsertBatch<T>(List<T> instance, SafeMode safeMode) where T : class
        {
            MongoServer mongo = MongoServer.Create(serverSetting);

            mongo.GetDatabase(databaseName).GetCollection(collectionName).InsertBatch(instance, safeMode);
        }

        #endregion

        #region MongoDac Select function List

        // Find One
        public virtual T FindOne<T>(IMongoQuery query) where T : class
        {
            MongoServer mongo = MongoServer.Create(serverSetting);

            return mongo.GetDatabase(databaseName).GetCollection(collectionName).FindOneAs<T>(query);
        }

        public virtual T FindOne<T>(object query) where T : class
        {
            return FindOne<T>(new QueryWrapper(query));
        }

        /// <summary>
        /// dynamic collection find return BsonDocument
        /// </summary>
        /// <param name="query">IMongoQuery</param>
        /// <returns>List<BsonDocument></returns>
        public BsonDocument FindOne(IMongoQuery query)
        {
            MongoServer mongo = MongoServer.Create(serverSetting);
            return mongo.GetDatabase(databaseName).GetCollection(collectionName).FindOne(query);
        }

        // Find By Id
        public virtual T FindById<T>(ObjectId id) where T : class
        {
            MongoServer mongo = MongoServer.Create(serverSetting);

            return mongo.GetDatabase(databaseName).GetCollection(collectionName).FindOneByIdAs<T>(id);
        }

        public virtual T FindById<T>(string id) where T : class
        {
            return FindById<T>(new ObjectId(id));
        }

        // Find 
        public virtual IQueryable<T> Find<T>(IMongoQuery query) where T : class
        {
            MongoServer mongo = MongoServer.Create(serverSetting);

            return mongo.GetDatabase(databaseName).GetCollection(collectionName)
              .FindAs<T>(query).AsQueryable();
        }

        public virtual IQueryable<T> Find<T>(object query) where T : class
        {
            return Find<T>(new QueryWrapper(query));
        }

        public virtual IQueryable<T> Find<T>(IMongoQuery query, IMongoSortBy sortBy) where T : class
        {
            MongoServer mongo = MongoServer.Create(serverSetting);

            return mongo.GetDatabase(databaseName).GetCollection(collectionName)
                .FindAs<T>(query).SetSortOrder(sortBy).AsQueryable();
        }

        public virtual IQueryable<T> Find<T>(object query, IMongoSortBy sortBy) where T : class
        {
            return Find<T>(new QueryWrapper(query), sortBy);
        }


        public virtual IQueryable<T> Find<T>(IMongoQuery query, int limit, IMongoSortBy sortBy) where T : class
        {
            MongoServer mongo = MongoServer.Create(serverSetting);
            return mongo.GetDatabase(databaseName).GetCollection(collectionName)
              .FindAs<T>(query).SetSortOrder(sortBy).SetLimit(limit).AsQueryable();
        }

        public virtual IQueryable<T> Find<T>(object query, int limit, IMongoSortBy sortBy) where T : class
        {
            return Find<T>(new QueryWrapper(query), limit, sortBy);
        }

        public virtual IQueryable<T> Find<T>() where T : class
        {
            return FindAll<T>();
        }

        /// <summary>
        /// dynamic collection find return BsonDocument List
        /// </summary>
        /// <param name="query">IMongoQuery</param>
        /// <returns>List<BsonDocument></returns>
        public List<BsonDocument> Find(IMongoQuery query)
        {
            MongoServer mongo = MongoServer.Create(serverSetting);
            return mongo.GetDatabase(databaseName).GetCollection(collectionName).Find(query).ToList();
        }



        // Find All
        public virtual IQueryable<T> FindAll<T>() where T : class
        {
            MongoServer mongo = MongoServer.Create(serverSetting);

            return mongo.GetDatabase(databaseName).GetCollection(collectionName).FindAllAs<T>().AsQueryable();
        }

        public FindAndModifyResult FindAndModify(IMongoQuery query, IMongoSortBy sortBy, IMongoUpdate update)
        {
            MongoServer mongo = MongoServer.Create(serverSetting);
            return mongo.GetDatabase(databaseName).GetCollection(collectionName).FindAndModify(query, sortBy, update);
        }

        #endregion

        #region MongoDac Remove function List

        public virtual void Remove(IMongoQuery query)
        {
            MongoServer mongo = MongoServer.Create(serverSetting);

            mongo.GetDatabase(databaseName).GetCollection(collectionName).Remove(query);
        }

        public virtual void Remove(object query)
        {
            Remove(new QueryWrapper(query));
        }

        public virtual void Remove(ObjectId id)
        {
            Remove(new { Id = id });
        }

        public virtual void Remove(string id)
        {
            Remove(new ObjectId(id));
        }

        //SafeMode

        public virtual void Remove(IMongoQuery query, SafeMode safeMode)
        {
            MongoServer mongo = MongoServer.Create(serverSetting);

            mongo.GetDatabase(databaseName).GetCollection(collectionName).Remove(query, safeMode);
        }

        public virtual void Remove(object query, SafeMode safeMode)
        {
            Remove(new QueryWrapper(query), safeMode);
        }

        public virtual void Remove(ObjectId id, SafeMode safeMode)
        {
            Remove(new { Id = id }, safeMode);
        }

        public virtual void Remove(string id, SafeMode safeMode)
        {
            Remove(new ObjectId(id), safeMode);
        }
        #endregion
    }
}