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
using MongoDB.BsonLibrary.Serialization;

namespace MongoDB.BsonLibrary {
    public static class BsonExtensionMethods {
        public static byte[] ToBson<T>(
            this T obj
        ) {
            return ToBson(obj, BsonBinaryWriterSettings.Defaults);
        }

        public static byte[] ToBson<T>(
            this T obj,
            bool serializeIdFirst
        ) {
            return ToBson(obj, serializeIdFirst, BsonBinaryWriterSettings.Defaults);
        }

        public static byte[] ToBson<T>(
            this T obj,
            bool serializeIdFirst,
            BsonBinaryWriterSettings settings
        ) {
            using (var buffer = new BsonBuffer()) {
                using (var bsonWriter = BsonWriter.Create(buffer, settings)) {
                    BsonSerializer.Serialize<T>(bsonWriter, obj, serializeIdFirst);
                }
                return buffer.ToByteArray();
            }
        }

        public static byte[] ToBson<T>(
            this T obj,
            BsonBinaryWriterSettings settings
        ) {
            return ToBson(obj, false, settings);
        }

        public static BsonDocument ToBsonDocument(
            this object obj
        ) {
            if (obj == null) {
                return null;
            }

            var bsonDocument = obj as BsonDocument;
            if (bsonDocument != null) {
                return bsonDocument; // it's already a BsonDocument
            }

            var convertibleToBsonDocument = obj as IConvertibleToBsonDocument;
            if (convertibleToBsonDocument != null) {
                return convertibleToBsonDocument.ToBsonDocument(); // use the provided ToBsonDocument method
            }

            // otherwise serialize it and then deserialize it into a new BsonDocument
            using (var buffer = new BsonBuffer()) {
                using (var bsonWriter = BsonWriter.Create(buffer)) {
                    BsonSerializer.Serialize(bsonWriter, obj, false, false);
                }
                buffer.Position = 0;
                using (var bsonReader = BsonReader.Create(buffer)) {
                    var document = BsonSerializer.Deserialize<BsonDocument>(bsonReader);
                    return document;
                }
            }
        }

        public static string ToJson<T>(
            this T obj
        ) {
            return ToJson(obj, BsonJsonWriterSettings.Defaults);
        }

        public static string ToJson<T>(
            this T obj,
            bool serializeIdFirst
        ) {
            return ToJson(obj, serializeIdFirst, BsonJsonWriterSettings.Defaults);
        }

        public static string ToJson<T>(
            this T obj,
            bool serializeIdFirst,
            BsonJsonWriterSettings settings
        ) {
            using (var stringWriter = new StringWriter()) {
                using (var bsonWriter = BsonWriter.Create(stringWriter, settings)) {
                    BsonSerializer.Serialize<T>(bsonWriter, obj, serializeIdFirst);
                }
                return stringWriter.ToString();
            }
        }

        public static string ToJson<T>(
            this T obj,
            BsonJsonWriterSettings settings
        ) {
            return ToJson(obj, false, settings);
        }
    }
}
