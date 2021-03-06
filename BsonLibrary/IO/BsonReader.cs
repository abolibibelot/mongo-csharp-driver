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

namespace MongoDB.BsonLibrary.IO {
    public abstract class BsonReader : IDisposable {
        #region constructors
        protected BsonReader() {
        }
        #endregion

        #region public properties
        public abstract BsonReadState ReadState { get; }
        #endregion

        #region public static methods
        public static BsonReader Create(
            BsonBuffer buffer
        ) {
            return Create(buffer, BsonBinaryReaderSettings.Defaults);
        }

        public static BsonReader Create(
            BsonBuffer buffer,
            BsonBinaryReaderSettings settings
        ) {
            return new BsonBinaryReader(buffer, settings);
        }

        public static BsonReader Create(
            Stream stream
        ) {
            return Create(stream, BsonBinaryReaderSettings.Defaults);
        }

        public static BsonReader Create(
            Stream stream,
            BsonBinaryReaderSettings settings
        ) {
            BsonBuffer buffer = new BsonBuffer();
            buffer.LoadFrom(stream);
            return new BsonBinaryReader(buffer, settings);
        }
        #endregion

        #region public methods
        public abstract void Close();
        public abstract void Dispose();
        public abstract string FindString(
            string name
        );
        public abstract bool HasElement(
            out BsonType bsonType
        );
        public abstract bool HasElement(
            out BsonType bsonType,
            out string name
        );
        public abstract BsonType PeekBsonType();
        public abstract void ReadArrayName(
            out string name
        );
        public abstract void ReadArrayName(
            string expectedName
        );
        public abstract void ReadBinaryData(
            out string name,
            out byte[] bytes,
            out BsonBinarySubType subType
        );
        public abstract void ReadBinaryData(
            string expectedName,
            out byte[] bytes,
            out BsonBinarySubType subType
        );
        public abstract bool ReadBoolean(
            out string name
        );
        public abstract bool ReadBoolean(
            string expectedName
        );
        public abstract DateTime ReadDateTime(
            out string name
        );
        public abstract DateTime ReadDateTime(
            string expectedName
        );
        public abstract void ReadDocumentName(
            out string name
        );
        public abstract void ReadDocumentName(
            string expectedName
        );
        public abstract double ReadDouble(
            out string name
        );
        public abstract double ReadDouble(
            string expectedName
        );
        public abstract void ReadEndDocument();
        public abstract int ReadInt32(
            out string name
        );
        public abstract int ReadInt32(
            string expectedName
        );
        public abstract long ReadInt64(
            out string name
        );
        public abstract long ReadInt64(
            string expectedName
        );
        public abstract string ReadJavaScript(
            out string name
        );
        public abstract string ReadJavaScript(
            string expectedName
        );
        public abstract string ReadJavaScriptWithScope(
            out string name
        );
        public abstract string ReadJavaScriptWithScope(
            string expectedName
        );
        public abstract void ReadMaxKey(
            out string name
        );
        public abstract void ReadMaxKey(
            string expectedName
        );
        public abstract void ReadMinKey(
            out string name
        );
        public abstract void ReadMinKey(
            string expectedName
        );
        public abstract void ReadNull(
            out string name
        );
        public abstract void ReadNull(
            string expectedName
        );
        public abstract void ReadObjectId(
            out string name,
            out int timestamp,
            out long machinePidIncrement
        );
        public abstract void ReadObjectId(
            string expectedName,
            out int timestamp,
            out long machinePidIncrement
        );
        public abstract void ReadRegularExpression(
            out string name,
            out string pattern,
            out string options
        );
        public abstract void ReadRegularExpression(
            string expectedName,
            out string pattern,
            out string options
        );
        public abstract void ReadStartDocument();
        public abstract string ReadString(
            out string name
        );
        public abstract string ReadString(
            string expectedName
        );
        public abstract string ReadSymbol(
            out string name
        );
        public abstract string ReadSymbol(
            string expectedName
        );
        public abstract long ReadTimestamp(
            out string name
        );
        public abstract long ReadTimestamp(
            string expectedName
        );
        public abstract void SkipElement();
        public abstract void VerifyString(
            string expectedName,
            string expectedValue
        );
        #endregion
    }
}
