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
using MongoDB.CSharpDriver.Internal;

namespace MongoDB.CSharpDriver {
    public class MongoDatabase {
        #region private fields
        private object databaseLock = new object();
        private MongoServer server;
        private string name;
        private MongoCredentials credentials;
        private SafeMode safeMode;
        private Dictionary<string, MongoCollection> collections = new Dictionary<string, MongoCollection>();
        private MongoGridFS gridFS;
        #endregion

        #region constructors
        public MongoDatabase(
            MongoServer server,
            string name
        )
            : this(server, name, null) {
        }

        public MongoDatabase(
            MongoServer server,
            string name,
            MongoCredentials credentials
        ) {
            ValidateDatabaseName(name);
            this.server = server;
            this.name = name;
            this.credentials = credentials;
            this.safeMode = server.SafeMode;
        }
        #endregion

        #region factory methods
        public static MongoDatabase Create(
            MongoConnectionSettings settings
        ) {
            if (settings.DatabaseName == null) {
                throw new ArgumentException("Connection string must have database name");
            }
            MongoServer server = MongoServer.Create(settings);
            return server.GetDatabase(settings.DatabaseName, settings.Credentials);
        }

        public static MongoDatabase Create(
            MongoConnectionStringBuilder builder
        ) {
            return Create(builder.ToConnectionSettings());
        }

        public static MongoDatabase Create(
            MongoUrl url
        ) {
            return Create(url.ToConnectionSettings());
        }

        public static MongoDatabase Create(
            string connectionString
        ) {
            if (connectionString.StartsWith("mongodb://")) {
                MongoUrl url = new MongoUrl(connectionString);
                return Create(url);
            } else {
                MongoConnectionStringBuilder builder = new MongoConnectionStringBuilder(connectionString);
                return Create(builder);
            }
        }

        public static MongoDatabase Create(
            Uri uri
        ) {
            return Create(new MongoUrl(uri.ToString()));
        }
        #endregion

        #region public properties
        public MongoCollection<BsonDocument> CommandCollection {
            get { return GetCollection<BsonDocument>("$cmd"); }
        }

        public MongoCredentials Credentials {
            get { return credentials; }
        }

        public MongoGridFS GridFS {
            get {
                lock (databaseLock) {
                    if (gridFS == null) {
                        gridFS = new MongoGridFS(this, MongoGridFSSettings.Defaults.Clone());
                    }
                    return gridFS;
                }
            }
        }

        public string Name {
            get { return name; }
        }

        public SafeMode SafeMode {
            get { return safeMode; }
            set { safeMode = value; }
        }

        public MongoServer Server {
            get { return server; }
        }
        #endregion

        #region public indexers
        public MongoCollection<BsonDocument> this[
            string collectionName
        ] {
            get { return GetCollection(collectionName); }
        }
        #endregion

        #region public methods
        public void AddUser(
            MongoCredentials credentials
        ) {
            AddUser(credentials, false);
        }

        public void AddUser(
            MongoCredentials credentials,
            bool readOnly
        ) {
            var users = GetCollection("system.users");
            var user = users.FindOne(new BsonDocument("user", credentials.Username));
            if (user == null) {
                user = new BsonDocument("user", credentials.Username);
            }
            user["readOnly"] = readOnly;
            user["pwd"] = MongoUtils.Hash(credentials.Username + ":mongo:" + credentials.Password);
            users.Save(user);
        }

        public bool CollectionExists(
            string collectionName
        ) {
            return GetCollectionNames().Contains(collectionName);
        }

        public BsonDocument CreateCollection(
            string collectionName,
            BsonDocument options
        ) {
            BsonDocument command = new BsonDocument("create", collectionName);
            command.Merge(options);
            return RunCommand(command);
        }

        public BsonDocument CurrentOp() {
            var collection = GetCollection("$cmd.sys.inprog");
            return collection.FindOne();
        }

        public BsonDocument DropCollection(
            string collectionName
        ) {
            BsonDocument command = new BsonDocument("drop", collectionName);
            return RunCommand(command);
        }

        public BsonValue Eval(
            string code,
            params object[] args
        ) {
            BsonDocument command = new BsonDocument {
                { "$eval", code },
                { "args", new BsonArray(args) }
            };
            var result = RunCommand(command);
            return result["retval"];
        }

        public MongoCollection<BsonDocument> GetCollection(
            string collectionName
        ) {
            return GetCollection<BsonDocument>(collectionName);
        }

        public MongoCollection<TDefaultDocument> GetCollection<TDefaultDocument>(
            string collectionName
        ) {
            lock (databaseLock) {
                MongoCollection collection;
                string key = string.Format("{0}<{1}>", collectionName, typeof(TDefaultDocument).FullName);
                if (!collections.TryGetValue(key, out collection)) {
                    collection = new MongoCollection<TDefaultDocument>(this, collectionName);
                    collections.Add(key, collection);
                }
                return (MongoCollection<TDefaultDocument>) collection;
            }
        }

        public IEnumerable<string> GetCollectionNames() {
            List<string> collectionNames = new List<string>();
            var namespaces = GetCollection("system.namespaces");
            var prefix = name + ".";
            foreach (var @namespace in namespaces.FindAll()) {
                string collectionName = @namespace["name"].AsString;
                if (!collectionName.StartsWith(prefix)) { continue; }
                if (collectionName.Contains('$')) { continue; }
                collectionNames.Add(collectionName);
            }
            collectionNames.Sort();
            return collectionNames;
        }

        public BsonDocument GetLastError() {
            var connectionPool = server.GetConnectionPool();
            if (connectionPool.RequestNestingLevel == 0) {
                throw new InvalidOperationException("GetLastError can only be called if RequestStart has been called first");
            }
            return RunCommand("getlasterror"); // use all lowercase for backward compatibility
        }

        // TODO: mongo shell has GetPrevError at the database level?
        // TODO: mongo shell has GetProfilingLevel at the database level?
        // TODO: mongo shell has GetReplicationInfo at the database level?

        public MongoDatabase GetSisterDatabase(
            string databaseName
        ) {
            return server.GetDatabase(databaseName);
        }

        public BsonDocument GetStats() {
            return RunCommand("dbstats");
        }

        // TODO: mongo shell has IsMaster at database level?

        public void RemoveUser(
            string username
        ) {
            var users = GetCollection("system.users");
            users.Remove(new BsonDocument("user", username));
        }

        public void RequestDone() {
            var connectionPool = server.GetConnectionPool();
            connectionPool.RequestDone();
        }

        // the result of RequestStart is IDisposable so you can use RequestStart in a using statment
        // and then RequestDone will be called automatically when leaving the using statement
        public IDisposable RequestStart() {
            var connectionPool = server.GetConnectionPool();
            connectionPool.RequestStart(this);
            return new RequestStartResult(this);
        }

        // TODO: mongo shell has ResetError at the database level

        public BsonDocument RunCommand<TCommand>(
            TCommand command
        ) {
            BsonDocument result = CommandCollection.FindOne(command);
            if (!result.Contains("ok")) {
                throw new MongoCommandException("ok element is missing");
            }
            if (!result["ok"].ToBoolean()) {
                string errmsg = result["errmsg"].AsString;
                string errorMessage = string.Format("Command failed: {1}", errmsg);
                throw new MongoCommandException(errorMessage);
            }
            return result;
        }

        public BsonDocument RunCommand(
            string commandName
        ) {
            BsonDocument command = new BsonDocument(commandName, true);
            return RunCommand(command);
        }

        public override string ToString() {
            return name;
        }
        #endregion

        #region internal methods
        internal MongoConnection GetConnection() {
            MongoConnectionPool connectionPool;
            lock (databaseLock) {
                connectionPool = server.GetConnectionPool();
            }
            return connectionPool.GetConnection(this);
        }

        internal void ReleaseConnection(
            MongoConnection connection
        ) {
            connection.ConnectionPool.ReleaseConnection(connection);
        }
        #endregion

        #region private methods
        private void ValidateDatabaseName(
            string name
        ) {
            if (name == null) {
                throw new ArgumentNullException("name");
            }
            if (name == "") {
                throw new ArgumentException("Database name is empty");
            }
            if (name.IndexOfAny(new char[] { '\0', ' ', '.', '$', '/', '\\' }) != -1) {
                throw new ArgumentException("Database name cannot contain the following special characters: null, space, period, $, / or \\");
            }
            if (Encoding.UTF8.GetBytes(name).Length > 64) {
                throw new ArgumentException("Database name cannot exceed 64 bytes (after encoding to UTF8)");
            }
        }
        #endregion

        #region private nested classes
        private class RequestStartResult : IDisposable {
            #region private fields
            private MongoDatabase database;
            #endregion

            #region constructors
            public RequestStartResult(
                MongoDatabase database
            ) {
                this.database = database;
            }
            #endregion

            #region public methods
            public void Dispose() {
                database.RequestDone();
            }
            #endregion
        }
        #endregion
    }
}
