﻿using Google.Cloud.Firestore;
using System.Reflection;
using FirestoreLINQ.Internals;

namespace FirestoreLINQ
{
    public static class Extensions
    {
        public static IQueryable<T> AsQuerable<T>(this CollectionReference collection)
        {
            IQueryable<T> source = new Queryable<T>(collection);
            return source;
        }

        public static CollectionReference Collection<T>(this FirestoreDb db) where T : class
        {
            TypeInfo typeInfo = typeof(T).GetTypeInfo();
            FirestoreCollectionAttribute collectionAttribute = FirestoreCollectionAttribute.GetAttributes(typeInfo);
            string collectionName = collectionAttribute?.CollectionName ?? typeInfo.Name.ToLower();
            return db.Collection(collectionName);
        }

        public static IQueryable<T> AsQuerable<T>(this FirestoreDb db) where T : class
        {
            return db.Collection<T>().AsQuerable<T>();
        }
    }
}