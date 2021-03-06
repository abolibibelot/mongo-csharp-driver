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

namespace MongoDB.BsonLibrary.IO {
    [Serializable]
    public class BsonJsonWriterSettings {
        #region private static fields
        private static BsonJsonWriterSettings defaults = new BsonJsonWriterSettings();
        #endregion

        #region private fields
        private bool closeOutput = false;
        private Encoding encoding = Encoding.UTF8;
        private bool indent = false;
        private string indentChars = "  ";
        private string newLineChars = "\r\n";
        private BsonJsonOutputMode outputMode = BsonJsonOutputMode.Strict;
        #endregion

        #region constructors
        public BsonJsonWriterSettings() {
        }
        #endregion

        #region public static properties
        public static BsonJsonWriterSettings Defaults {
            get { return defaults; } // TODO: clone?
        }
        #endregion

        #region public properties
        public bool CloseOutput {
            get { return closeOutput; }
            set { closeOutput = value; }
        }

        public Encoding Encoding {
            get { return encoding; }
            set { encoding = value; }
        }

        public bool Indent {
            get { return indent; }
            set { indent = value; }
        }

        public string IndentChars {
            get { return indentChars; }
            set { indentChars = value; }
        }

        public string NewLineChars {
            get { return newLineChars; }
            set { newLineChars = value; }
        }

        public BsonJsonOutputMode OutputMode {
            get { return outputMode; }
            set { outputMode = value; }
        }
        #endregion
    }
}
