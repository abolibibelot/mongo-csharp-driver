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
using System.Linq;
using System.Text;

using MongoDB.BsonLibrary;
using MongoDB.BsonLibrary.IO;
using MongoDB.BsonLibrary.Serialization;
using MongoDB.CSharpDriver;

namespace MongoDB.CSharpDriver.Builders {
    public static class MapReduceOptions {
        #region public static properties
        public static MapReduceOptionsBuilder None {
            get { return null; }
        }
        #endregion

        #region public static methods
        public static MapReduceOptionsBuilder SetFinalize(
            BsonJavaScript finalize
        ) {
            return new MapReduceOptionsBuilder().SetFinalize(finalize);
        }

        public static MapReduceOptionsBuilder SetKeepTemp(
            bool value
        ) {
            return new MapReduceOptionsBuilder().SetKeepTemp(value);
        }

        public static MapReduceOptionsBuilder SetLimit(
            int value
        ) {
            return new MapReduceOptionsBuilder().SetLimit(value);
        }

        public static MapReduceOptionsBuilder SetOutput(
            string collectionName
        ) {
            return new MapReduceOptionsBuilder().SetOutput(collectionName);
        }

        public static MapReduceOptionsBuilder SetQuery<TQuery>(
            TQuery query
        ) {
            return new MapReduceOptionsBuilder().SetQuery(query);
        }

        public static MapReduceOptionsBuilder SetScope<TScope>(
            TScope scope
        ) {
            return new MapReduceOptionsBuilder().SetScope(scope);
        }

        public static MapReduceOptionsBuilder SetSortOrder<TSortBy>(
            TSortBy sortBy
        ) {
            return new MapReduceOptionsBuilder().SetSortOrder(sortBy);
        }

        public static MapReduceOptionsBuilder SetSortOrder(
            params string[] keys
        ) {
            return new MapReduceOptionsBuilder().SetSortOrder(SortBy.Ascending(keys));
        }

        public static MapReduceOptionsBuilder SetVerbose(
            bool value
        ) {
            return new MapReduceOptionsBuilder().SetVerbose(value);
        }
        #endregion
    }

    [Serializable]
    public class MapReduceOptionsBuilder : BuilderBase, IConvertibleToBsonDocument, IBsonSerializable {
        #region private fields
        private BsonDocument document;
        #endregion

        #region constructors
        public MapReduceOptionsBuilder() {
            document = new BsonDocument();
        }
        #endregion

        #region public methods
        public MapReduceOptionsBuilder SetFinalize(
            BsonJavaScript finalize
        ) {
            document["finalize"] = finalize;
            return this;
        }

        public MapReduceOptionsBuilder SetKeepTemp(
            bool value
        ) {
            document["keeptemp"] = value;
            return this;
        }

        public MapReduceOptionsBuilder SetLimit(
            int value
        ) {
            document["limit"] = value;
            return this;
        }

        public MapReduceOptionsBuilder SetOutput(
            string collectionName
        ) {
            document["out"] = collectionName;
            return this;
        }

        public MapReduceOptionsBuilder SetQuery<TQuery>(
            TQuery query
        ) {
            document["query"] = BsonDocumentWrapper.Create(query);
            return this;
        }

        public MapReduceOptionsBuilder SetScope<TScope>(
            TScope scope
        ) {
            document["scope"] = BsonDocumentWrapper.Create(scope);
            return this;
        }

        public MapReduceOptionsBuilder SetSortOrder<TSortBy>(
            TSortBy sortBy
        ) {
            document["sort"] = BsonDocumentWrapper.Create(sortBy);
            return this;
        }

        public MapReduceOptionsBuilder SetSortOrder(
            params string[] keys
        ) {
            return SetSortOrder(SortBy.Ascending(keys));
        }

        public BsonDocument ToBsonDocument() {
            return document;
        }

        public MapReduceOptionsBuilder SetVerbose(
            bool value
        ) {
            document["verbose"] = value;
            return this;
        }
        #endregion

        #region internal methods
        internal MapReduceOptionsBuilder AddOptions(
            BsonDocument options
        ) {
            document.Add(options);
            return this;
        }
        #endregion

        #region explicit interface implementations
        void IBsonSerializable.Deserialize(
            BsonReader bsonReader
        ) {
            throw new InvalidOperationException("Deserialize is not supported for MapReduceOptionsBuilder");
        }

        void IBsonSerializable.Serialize(
            BsonWriter bsonWriter,
            bool serializeIdFirst,
            bool serializeDiscriminator
        ) {
            document.Serialize(bsonWriter, serializeIdFirst, serializeDiscriminator);
        }
        #endregion
    }
}
