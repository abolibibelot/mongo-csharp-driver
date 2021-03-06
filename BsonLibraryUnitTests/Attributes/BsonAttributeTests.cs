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
using NUnit.Framework;

using MongoDB.BsonLibrary;
using MongoDB.BsonLibrary.Serialization;

namespace MongoDB.BsonLibrary.UnitTests.ObjectModel {
    [TestFixture]
    public class BsonAttributeTests {
        [BsonDiscriminator("discriminator", Required=true)]
        [BsonIgnoreExtraElements(false)]
        [BsonUseCompactRepresentation]
        public class Test {
            [BsonDefaultValue("default1")]
            public string SerializedDefaultValue1 { get; set; }
            [BsonDefaultValue("default2", SerializeDefaultValue=true)]
            public string SerializedDefaultValue2 { get; set; }
            [BsonDefaultValue("default3", SerializeDefaultValue=false)]
            public string NotSerializedDefaultValue { get; set; }
            public string NoDefaultValue { get; set; }

            [BsonUseCompactRepresentation(false)]
            public string NotCompact { get; set; }
            public string Compact { get; set; }

            [BsonId]
            public ObjectId IsId { get; set; }
            public ObjectId IsNotId { get; set; }

            [BsonIgnore]
            public string Ignored { get; set; }
            public string NotIgnored { get; set; }

            [BsonIgnoreIfNull]
            public string IgnoredIfNull { get; set; }
            public string NotIgnoredIfNull { get; set; }

            [BsonRequired]
            public string Required { get; set; }
            public string NotRequired { get; set; }

            [BsonElement("notordered")]
            public string NotOrdered { get; set; }
            [BsonElement("ordered", Order=1)]
            public string Ordered { get; set; }
            public string NoElement { get; set; }
        }

        [Test]
        public void TestDiscriminator() {
            var classMap = BsonClassMap.LookupClassMap(typeof(Test));
            Assert.AreEqual("discriminator", classMap.Discriminator);
            Assert.AreEqual(true, classMap.DiscriminatorIsRequired);
        }

        [Test]
        public void TestIgnoreExtraElements() {
            var classMap = BsonClassMap.LookupClassMap(typeof(Test));
            Assert.AreEqual(false, classMap.IgnoreExtraElements);
        }

        [Test]
        public void TestDefaultValue() {
            var classMap = BsonClassMap.LookupClassMap(typeof(Test));

            var serializedDefaultValue1 = classMap.GetPropertyMap("SerializedDefaultValue1");
            Assert.AreEqual(true, serializedDefaultValue1.HasDefaultValue);
            Assert.AreEqual(true, serializedDefaultValue1.SerializeDefaultValue);
            Assert.AreEqual("default1", serializedDefaultValue1.DefaultValue);

            var serializedDefaultValue2 = classMap.GetPropertyMap("SerializedDefaultValue2");
            Assert.AreEqual(true, serializedDefaultValue2.HasDefaultValue);
            Assert.AreEqual(true, serializedDefaultValue2.SerializeDefaultValue);
            Assert.AreEqual("default2", serializedDefaultValue2.DefaultValue);

            var notSerializedDefaultValue = classMap.GetPropertyMap("NotSerializedDefaultValue");
            Assert.AreEqual(true, notSerializedDefaultValue.HasDefaultValue);
            Assert.AreEqual(false, notSerializedDefaultValue.SerializeDefaultValue);
            Assert.AreEqual("default3", notSerializedDefaultValue.DefaultValue);

            var noDefaultValue = classMap.GetPropertyMap("NoDefaultValue");
            Assert.AreEqual(false, noDefaultValue.HasDefaultValue);
            Assert.AreEqual(true, noDefaultValue.SerializeDefaultValue);
            Assert.IsNull(noDefaultValue.DefaultValue);
        }

        [Test]
        public void TestUseCompactRepresentation() {
            var classMap = BsonClassMap.LookupClassMap(typeof(Test));
            Assert.AreEqual(true, classMap.UseCompactRepresentation);

            var notCompact = classMap.GetPropertyMap("NotCompact");
            Assert.AreEqual(false, notCompact.UseCompactRepresentation);

            var compact = classMap.GetPropertyMap("Compact");
            Assert.AreEqual(true, compact.UseCompactRepresentation);
        }

        [Test]
        public void TestId() {
            var classMap = BsonClassMap.LookupClassMap(typeof(Test));

            var isId = classMap.GetPropertyMap("IsId");
            Assert.AreEqual("_id", isId.ElementName);
            Assert.AreSame(classMap.IdPropertyMap, isId);

            var isNotId = classMap.GetPropertyMap("IsNotId");
            Assert.AreEqual("IsNotId", isNotId.ElementName);
            Assert.AreNotSame(classMap.IdPropertyMap, isNotId);
        }

        [Test]
        public void TestIgnored() {
            var classMap = BsonClassMap.LookupClassMap(typeof(Test));

            var ignored = classMap.GetPropertyMap("Ignored");
            Assert.IsNull(ignored);

            var notIgnored = classMap.GetPropertyMap("NotIgnored");
            Assert.IsNotNull(notIgnored);
        }

        [Test]
        public void TestIgnoredIfNull() {
            var classMap = BsonClassMap.LookupClassMap(typeof(Test));

            var ignoredIfNull = classMap.GetPropertyMap("IgnoredIfNull");
            Assert.AreEqual(true, ignoredIfNull.IgnoreIfNull);

            var notIgnoredIfNull = classMap.GetPropertyMap("NotIgnoredIfNull");
            Assert.AreEqual(false, notIgnoredIfNull.IgnoreIfNull);
        }

        [Test]
        public void TestRequired() {
            var classMap = BsonClassMap.LookupClassMap(typeof(Test));

            var required = classMap.GetPropertyMap("Required");
            Assert.AreEqual(true, required.IsRequired);

            var notRequired = classMap.GetPropertyMap("NotRequired");
            Assert.AreEqual(false, notRequired.IsRequired);
        }

        [Test]
        public void TestElement() {
            var classMap = BsonClassMap.LookupClassMap(typeof(Test));

            var notOrdered = classMap.GetPropertyMap("NotOrdered");
            Assert.AreEqual("notordered", notOrdered.ElementName);
            Assert.AreEqual(int.MaxValue, notOrdered.Order);

            var ordered = classMap.GetPropertyMap("Ordered");
            Assert.AreEqual("ordered", ordered.ElementName);
            Assert.AreEqual(1, ordered.Order);
            Assert.AreSame(classMap.PropertyMaps.First(), ordered);

            var noElement = classMap.GetPropertyMap("NoElement");
            Assert.AreEqual("NoElement", noElement.ElementName);
            Assert.AreEqual(int.MaxValue, noElement.Order);
        }
    }
}
