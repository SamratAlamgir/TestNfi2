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
        public static void Save<T>(T obj,ApplicationType appType)
        {
            var collection = GetCollection<T>(appType);
            collection.Add(obj);


            using (var file = File.CreateText(DirectoryHelper.GetApplicationDataFilePath(appType)))
            {
                var serializer = new JsonSerializer();
                serializer.Serialize(file, collection);
            }
        }

        public static List<T> GetCollection<T>(ApplicationType appType)
        {
            var collection = new List<T>();
           
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