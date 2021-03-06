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

namespace MongoDB.BsonLibrary {
    public static class Bson {
        #region private static fields
        private static DateTime unixEpoch = new DateTime(1970, 1, 1, 0, 0, 0, DateTimeKind.Utc);
        #endregion

        #region public static properties
        public static BsonBoolean False { get { return BsonBoolean.False; } }
        public static BsonMaxKey MaxKey { get { return BsonMaxKey.Singleton; } }
        public static BsonMinKey MinKey { get { return BsonMinKey.Singleton; } }
        public static BsonNull Null { get { return BsonNull.Singleton; } }
        public static BsonBoolean True { get { return BsonBoolean.True; } }
        public static DateTime UnixEpoch { get { return unixEpoch; } }
        #endregion
    }
}
