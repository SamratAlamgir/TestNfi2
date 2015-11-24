using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using Newtonsoft.Json;
using NFI.Enums;

namespace NFI.Helper
{
    public static class JsonHelper
    {
        public static void Save<T>(T obj, string dataFilePath)
        {
            var collection = GetCollections<T>(dataFilePath);
            collection.Add(obj);


            using (var file = File.CreateText(dataFilePath))
            {
                var serializer = new JsonSerializer();
                serializer.Serialize(file, collection);
            }
        }

        public static void Save<T>(List<T> collection, string dataFilePath)
        {
            using (var file = File.CreateText(dataFilePath))
            {
                var serializer = new JsonSerializer();
                serializer.Serialize(file, collection);
            }
        }

        public static List<T> GetCollections<T>(string dataFilePath)
        {
            var collection = new List<T>();

            if (!File.Exists(dataFilePath))
            {
                return collection;
            }

            // deserialize JSON 
            using (StreamReader file = File.OpenText(dataFilePath))
            {
                var serializer = new JsonSerializer();
                collection = (List<T>)serializer.Deserialize(file, typeof(List<T>));
            }

            return collection ?? new List<T>();
        }
    }
}