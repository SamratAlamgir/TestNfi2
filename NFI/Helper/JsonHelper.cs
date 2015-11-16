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
        public static void Save<T>(T obj)
        {
            var collection = GetCollection<T>();
            collection.Add(obj);

            var appType = ApplicationType.Application1; // TODO: We should set it dynamically

            using (var file = File.CreateText(DirectoryHelper.GetApplicationDataFilePath(appType)))
            {
                var serializer = new JsonSerializer();
                serializer.Serialize(file, collection);
            }
        }

        public static List<T> GetCollection<T>()
        {
            var collection = new List<T>();
            var appType = ApplicationType.Application1; // TODO: We should set it dynamically

            string fileName = DirectoryHelper.GetApplicationDataFilePath(appType);

            if (!File.Exists(fileName))
            {
                return collection;
            }

            // deserialize JSON 
            using (StreamReader file = File.OpenText(fileName))
            {
                var serializer = new JsonSerializer();
                collection = (List<T>)serializer.Deserialize(file, typeof(List<T>));
            }

            return collection;
        }
    }
}