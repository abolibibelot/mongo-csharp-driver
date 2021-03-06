﻿/* Copyright 2010 10gen Inc.
*
* Licensed under the Apache License, Version 2.0 (the "License");
* you may not use this file except in compliance with the License.
* You may obtain a copy of the License at
*
* http://www.apache.org/licenses/LICENSE-2.0
*
* Unless required by applicable law or agreed to in writing, software
* distributed under the License is distributed on an "AS IS" BASIS,
* WITHOUT WARRANTIES OR CONDITIONS OF ANY KIND, either express or implied.
* See the License for the specific language governing permissions and
* limitations under the License.
*/

using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;

using MongoDB.BsonLibrary.IO;

namespace MongoDB.BsonLibrary.Serialization {
    public static class BsonSerializer {
        #region private static fields
        private static object staticLock = new object();
        private static Dictionary<Type, IBsonSerializer> registry = new Dictionary<Type, IBsonSerializer>();
        private static Dictionary<Type, IBsonSerializer> cache = new Dictionary<Type, IBsonSerializer>();
        private static IBsonSerializer defaultSerializer = BsonClassMapSerializer.Singleton;
        #endregion

        #region public static properties
        public static IBsonSerializer DefaultSerializer {
            get { return defaultSerializer; }
            set { defaultSerializer = value; }
        }
        #endregion

        #region public static methods
        public static T Deserialize<T>(
            BsonReader bsonReader
        ) {
            var obj = Deserialize(bsonReader, typeof(T));
            return (T) obj;
        }

        public static T Deserialize<T>(
            byte[] bytes
        ) {
            var obj = Deserialize(bytes, typeof(T));
            return (T) obj;
        }

        public static T Deserialize<T>(
            Stream stream
        ) {
            var obj = Deserialize(stream, typeof(T));
            return (T) obj;
        }

        public static object Deserialize(
            BsonReader bsonReader,
            Type type
        ) {
            // optimize for the most common case
            if (type == typeof(BsonDocument)) {
                return BsonDocument.ReadFrom(bsonReader);
            }

            var serializer = LookupSerializer(type);
            return serializer.Deserialize(bsonReader, type);
        }

        public static object Deserialize(
            byte[] bytes,
            Type type
        ) {
            using (var memoryStream = new MemoryStream(bytes)) {
                return Deserialize(memoryStream, type);
            }
        }

        public static object Deserialize(
            Stream stream,
            Type type
        ) {
            using (var bsonReader = BsonReader.Create(stream)) {
                return Deserialize(bsonReader, type);
            }
        }

        public static IBsonSerializer LookupSerializer(
            Type type
        ) {
            lock (staticLock) {
                IBsonSerializer serializer;
                if (cache.TryGetValue(type, out serializer)) {
                    return serializer;
                }

                if (type.GetInterface(typeof(IBsonSerializable).FullName) != null) {
                    serializer = BsonSerializableSerializer.Singleton;
                } else {
                    Type ancestorType = type;
                    while (ancestorType != null) {
                        if (registry.TryGetValue(ancestorType, out serializer)) {
                            break;
                        }
                        ancestorType = ancestorType.BaseType;
                    }

                    if (serializer == null) {
                        serializer = defaultSerializer;
                    }
                }

                cache[type] = serializer;
                return serializer;
            }
        }

        public static void RegisterSerializer(
            Type type,
            IBsonSerializer serializer
        ) {
            lock (staticLock) {
                registry[type] = serializer;
                cache.Clear();
            }
        }

        public static void Serialize<T>(
            BsonWriter bsonWriter,
            T obj
        ) {
            Serialize(bsonWriter, obj, false);
        }

        public static void Serialize<T>(
            BsonWriter bsonWriter,
            T obj,
            bool serializeIdFirst
        ) {
            var serializeDiscriminator = BsonPropertyMap.IsPolymorphicType(typeof(T)) || obj.GetType() != typeof(T);
            Serialize(bsonWriter, obj, serializeIdFirst, serializeDiscriminator);
        }

        public static void Serialize(
            BsonWriter bsonWriter,
            object obj,
            bool serializeIdFirst,
            bool serializeDiscriminator
        ) {
            // optimize for the most common case
            var bsonSerializable = obj as IBsonSerializable;
            if (bsonSerializable != null) {
                bsonSerializable.Serialize(bsonWriter, serializeIdFirst, serializeDiscriminator);
                return;
            }

            var serializer = LookupSerializer(obj.GetType());
            serializer.Serialize(bsonWriter, obj, serializeIdFirst, serializeDiscriminator);
        }

        public static void UnregisterSerializer(
            Type type
        ) {
            lock (staticLock) {
                registry.Remove(type);
                cache.Clear();
            }
        }
        #endregion
    }
}
