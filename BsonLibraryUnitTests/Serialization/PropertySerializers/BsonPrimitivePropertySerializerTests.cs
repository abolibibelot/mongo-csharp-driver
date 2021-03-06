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
using NUnit.Framework;

using MongoDB.BsonLibrary.IO;
using MongoDB.BsonLibrary.Serialization;

namespace MongoDB.BsonLibrary.UnitTests.Serialization.PropertySerializers {
    [TestFixture]
    public class BooleanPropertySerializerTests {
        public class TestClass {
            public bool Boolean { get; set; }
        }

        [Test]
        public void TestFalse() {
            var obj = new TestClass {
                Boolean = false
            };
            var json = obj.ToJson();
            var expected = "{ 'Boolean' : false }".Replace("'", "\"");
            Assert.AreEqual(expected, json);

            var bson = obj.ToBson();
            var rehydrated = BsonSerializer.Deserialize<TestClass>(bson);
            Assert.IsTrue(bson.SequenceEqual(rehydrated.ToBson()));
        }

        [Test]
        public void TestTrue() {
            var obj = new TestClass {
                Boolean = true
            };
            var json = obj.ToJson();
            var expected = "{ 'Boolean' : true }".Replace("'", "\"");
            Assert.AreEqual(expected, json);

            var bson = obj.ToBson();
            var rehydrated = BsonSerializer.Deserialize<TestClass>(bson);
            Assert.IsTrue(bson.SequenceEqual(rehydrated.ToBson()));
        }
    }

    [TestFixture]
    public class DateTimePropertySerializerTests {
        public class TestClass {
            public DateTime DateTime { get; set; }
        }

        [Test]
        public void TestMinLocal() {
            var obj = new TestClass {
                DateTime = DateTime.SpecifyKind(DateTime.MinValue, DateTimeKind.Local)
            };
            long milliseconds = (long) (obj.DateTime.ToUniversalTime() - Bson.UnixEpoch).TotalMilliseconds;
            var json = obj.ToJson();
            var expected = ("{ 'DateTime' : { '$date' : " + milliseconds.ToString() + " } }").Replace("'", "\"");
            Assert.AreEqual(expected, json);

            var bson = obj.ToBson();
            var rehydrated = BsonSerializer.Deserialize<TestClass>(bson);
            Assert.IsTrue(bson.SequenceEqual(rehydrated.ToBson()));
        }

        [Test]
        public void TestMinUnspecified() {
            var obj = new TestClass {
                DateTime = DateTime.SpecifyKind(DateTime.MinValue, DateTimeKind.Unspecified)
            };
            long milliseconds = (long) (obj.DateTime.ToUniversalTime() - Bson.UnixEpoch).TotalMilliseconds;
            var json = obj.ToJson();
            var expected = ("{ 'DateTime' : { '$date' : " + milliseconds.ToString() + " } }").Replace("'", "\"");
            Assert.AreEqual(expected, json);

            var bson = obj.ToBson();
            var rehydrated = BsonSerializer.Deserialize<TestClass>(bson);
            Assert.IsTrue(bson.SequenceEqual(rehydrated.ToBson()));
        }

        [Test]
        public void TestMinUtc() {
            var obj = new TestClass {
                DateTime = DateTime.SpecifyKind(DateTime.MinValue, DateTimeKind.Utc)
            };
            long milliseconds = (long) (obj.DateTime - Bson.UnixEpoch).TotalMilliseconds;
            var json = obj.ToJson();
            var expected = ("{ 'DateTime' : { '$date' : " + milliseconds.ToString() + " } }").Replace("'", "\"");
            Assert.AreEqual(expected, json);

            var bson = obj.ToBson();
            var rehydrated = BsonSerializer.Deserialize<TestClass>(bson);
            Assert.IsTrue(bson.SequenceEqual(rehydrated.ToBson()));
        }

        [Test]
        public void TestMaxLocal() {
            var obj = new TestClass {
                DateTime = DateTime.SpecifyKind(DateTime.MaxValue, DateTimeKind.Local)
            };
            long milliseconds = (long) (obj.DateTime.ToUniversalTime() - Bson.UnixEpoch).TotalMilliseconds;
            var json = obj.ToJson();
            var expected = ("{ 'DateTime' : { '$date' : " + milliseconds.ToString() + " } }").Replace("'", "\"");
            Assert.AreEqual(expected, json);

            var bson = obj.ToBson();
            var rehydrated = BsonSerializer.Deserialize<TestClass>(bson);
            Assert.IsTrue(bson.SequenceEqual(rehydrated.ToBson()));
        }

        [Test]
        public void TestMaxUnspecified() {
            var obj = new TestClass {
                DateTime = DateTime.SpecifyKind(DateTime.MaxValue, DateTimeKind.Unspecified)
            };
            long milliseconds = (long) (obj.DateTime.ToUniversalTime() - Bson.UnixEpoch).TotalMilliseconds;
            var json = obj.ToJson();
            var expected = ("{ 'DateTime' : { '$date' : " + milliseconds.ToString() + " } }").Replace("'", "\"");
            Assert.AreEqual(expected, json);

            var bson = obj.ToBson();
            var rehydrated = BsonSerializer.Deserialize<TestClass>(bson);
            Assert.IsTrue(bson.SequenceEqual(rehydrated.ToBson()));
        }

        [Test]
        public void TestMaxUtc() {
            var obj = new TestClass {
                DateTime = DateTime.SpecifyKind(DateTime.MaxValue, DateTimeKind.Utc)
            };
            long milliseconds = (long) (obj.DateTime - Bson.UnixEpoch).TotalMilliseconds;
            var json = obj.ToJson();
            var expected = ("{ 'DateTime' : { '$date' : " + milliseconds.ToString() + " } }").Replace("'", "\"");
            Assert.AreEqual(expected, json);

            var bson = obj.ToBson();
            var rehydrated = BsonSerializer.Deserialize<TestClass>(bson);
            Assert.IsTrue(bson.SequenceEqual(rehydrated.ToBson()));
        }

        [Test]
        public void TestLocal() {
            var obj = new TestClass {
                DateTime = new DateTime(2010, 10, 08, 13, 30, 0, DateTimeKind.Local)
            };
            long milliseconds = (long) (obj.DateTime.ToUniversalTime() - Bson.UnixEpoch).TotalMilliseconds;
            var json = obj.ToJson();
            var expected = ("{ 'DateTime' : { '$date' : " + milliseconds.ToString() + " } }").Replace("'", "\"");
            Assert.AreEqual(expected, json);

            var bson = obj.ToBson();
            var rehydrated = BsonSerializer.Deserialize<TestClass>(bson);
            Assert.IsTrue(bson.SequenceEqual(rehydrated.ToBson()));
        }

        [Test]
        public void TestUnspecified() {
            var obj = new TestClass {
                DateTime = new DateTime(2010, 10, 08, 13, 30, 0, DateTimeKind.Unspecified)
            };
            long milliseconds = (long) (obj.DateTime.ToUniversalTime() - Bson.UnixEpoch).TotalMilliseconds;
            var json = obj.ToJson();
            var expected = ("{ 'DateTime' : { '$date' : " + milliseconds.ToString() + " } }").Replace("'", "\"");
            Assert.AreEqual(expected, json);

            var bson = obj.ToBson();
            var rehydrated = BsonSerializer.Deserialize<TestClass>(bson);
            Assert.IsTrue(bson.SequenceEqual(rehydrated.ToBson()));
        }

        [Test]
        public void TestUtc() {
            var obj = new TestClass {
                DateTime = new DateTime(2010, 10, 08, 13, 30, 0, DateTimeKind.Utc)
            };
            long milliseconds = (long) (obj.DateTime - Bson.UnixEpoch).TotalMilliseconds;
            var json = obj.ToJson();
            var expected = ("{ 'DateTime' : { '$date' : " + milliseconds.ToString() + " } }").Replace("'", "\"");
            Assert.AreEqual(expected, json);

            var bson = obj.ToBson();
            var rehydrated = BsonSerializer.Deserialize<TestClass>(bson);
            Assert.IsTrue(bson.SequenceEqual(rehydrated.ToBson()));
        }
    }

    [TestFixture]
    public class DoublePropertySerializerTests {
        public class TestClass {
            public double Double { get; set; }
        }

        [Test]
        public void TestMin() {
            var obj = new TestClass {
                Double = double.MinValue
            };
            var json = obj.ToJson();
            var expected = ("{ 'Double' : " + double.MinValue.ToString() + " }").Replace("'", "\"");
            Assert.AreEqual(expected, json);

            var bson = obj.ToBson();
            var rehydrated = BsonSerializer.Deserialize<TestClass>(bson);
            Assert.IsTrue(bson.SequenceEqual(rehydrated.ToBson()));
        }

        [Test]
        public void TestMinusOne() {
            var obj = new TestClass {
                Double = -1.0
            };
            var json = obj.ToJson();
            var expected = "{ 'Double' : -1 }".Replace("'", "\"");
            Assert.AreEqual(expected, json);

            var bson = obj.ToBson();
            var rehydrated = BsonSerializer.Deserialize<TestClass>(bson);
            Assert.IsTrue(bson.SequenceEqual(rehydrated.ToBson()));
        }

        [Test]
        public void TestZero() {
            var obj = new TestClass {
                Double = 0.0
            };
            var json = obj.ToJson();
            var expected = "{ 'Double' : 0 }".Replace("'", "\"");
            Assert.AreEqual(expected, json);

            var bson = obj.ToBson();
            var rehydrated = BsonSerializer.Deserialize<TestClass>(bson);
            Assert.IsTrue(bson.SequenceEqual(rehydrated.ToBson()));
        }

        [Test]
        public void TestOne() {
            var obj = new TestClass {
                Double = 1.0
            };
            var json = obj.ToJson();
            var expected = "{ 'Double' : 1 }".Replace("'", "\"");
            Assert.AreEqual(expected, json);

            var bson = obj.ToBson();
            var rehydrated = BsonSerializer.Deserialize<TestClass>(bson);
            Assert.IsTrue(bson.SequenceEqual(rehydrated.ToBson()));
        }

        [Test]
        public void TestMax() {
            var obj = new TestClass {
                Double = double.MaxValue
            };
            var json = obj.ToJson();
            var expected = ("{ 'Double' : " + double.MaxValue.ToString() + " }").Replace("'", "\"");
            Assert.AreEqual(expected, json);

            var bson = obj.ToBson();
            var rehydrated = BsonSerializer.Deserialize<TestClass>(bson);
            Assert.IsTrue(bson.SequenceEqual(rehydrated.ToBson()));
        }

        [Test]
        public void TestNaN() {
            var obj = new TestClass {
                Double = double.NaN
            };
            var json = obj.ToJson();
            var expected = "{ 'Double' : NaN }".Replace("'", "\"");
            Assert.AreEqual(expected, json);

            var bson = obj.ToBson();
            var rehydrated = BsonSerializer.Deserialize<TestClass>(bson);
            Assert.IsTrue(bson.SequenceEqual(rehydrated.ToBson()));
        }

        [Test]
        public void TestNegativeInfinity() {
            var obj = new TestClass {
                Double = double.NegativeInfinity
            };
            var json = obj.ToJson();
            var expected = "{ 'Double' : -Infinity }".Replace("'", "\"");
            Assert.AreEqual(expected, json);

            var bson = obj.ToBson();
            var rehydrated = BsonSerializer.Deserialize<TestClass>(bson);
            Assert.IsTrue(bson.SequenceEqual(rehydrated.ToBson()));
        }

        [Test]
        public void TestPositiveInfinity() {
            var obj = new TestClass {
                Double = double.PositiveInfinity
            };
            var json = obj.ToJson();
            var expected = "{ 'Double' : Infinity }".Replace("'", "\"");
            Assert.AreEqual(expected, json);

            var bson = obj.ToBson();
            var rehydrated = BsonSerializer.Deserialize<TestClass>(bson);
            Assert.IsTrue(bson.SequenceEqual(rehydrated.ToBson()));
        }
    }

    [TestFixture]
    public class GuidPropertySerializerTests {
        public class TestClass {
            public Guid Guid { get; set; }
        }

        [Test]
        public void TestEmpty() {
            var obj = new TestClass {
                Guid = Guid.Empty
            };
            var json = obj.ToJson();
            var expected = ("{ 'Guid' : { '$binary' : 'AAAAAAAAAAAAAAAAAAAAAA==', '$type' : '03' } }").Replace("'", "\"");
            Assert.AreEqual(expected, json);

            var bson = obj.ToBson();
            var rehydrated = BsonSerializer.Deserialize<TestClass>(bson);
            Assert.IsTrue(bson.SequenceEqual(rehydrated.ToBson()));
        }

        [Test]
        public void TestNew() {
            var obj = new TestClass {
                Guid = Guid.NewGuid()
            };
            var json = obj.ToJson();
            var base64 = Convert.ToBase64String(obj.Guid.ToByteArray()).Replace("\\", "\\\\");
            var expected = ("{ 'Guid' : { '$binary' : '" + base64 + "', '$type' : '03' } }").Replace("'", "\"");
            Assert.AreEqual(expected, json);

            var bson = obj.ToBson();
            var rehydrated = BsonSerializer.Deserialize<TestClass>(bson);
            Assert.IsTrue(bson.SequenceEqual(rehydrated.ToBson()));
        }
    }

    [TestFixture]
    public class Int32PropertySerializerTests {
        public class TestClass {
            public int Int32 { get; set; }
        }

        [Test]
        public void TestMin() {
            var obj = new TestClass {
                Int32 = int.MinValue
            };
            var json = obj.ToJson();
            var expected = ("{ 'Int32' : " + int.MinValue.ToString() + " }").Replace("'", "\"");
            Assert.AreEqual(expected, json);

            var bson = obj.ToBson();
            var rehydrated = BsonSerializer.Deserialize<TestClass>(bson);
            Assert.IsTrue(bson.SequenceEqual(rehydrated.ToBson()));
        }

        [Test]
        public void TestMinusOne() {
            var obj = new TestClass {
                Int32 = -1
            };
            var json = obj.ToJson();
            var expected = "{ 'Int32' : -1 }".Replace("'", "\"");
            Assert.AreEqual(expected, json);

            var bson = obj.ToBson();
            var rehydrated = BsonSerializer.Deserialize<TestClass>(bson);
            Assert.IsTrue(bson.SequenceEqual(rehydrated.ToBson()));
        }

        [Test]
        public void TestZero() {
            var obj = new TestClass {
                Int32 = 0
            };
            var json = obj.ToJson();
            var expected = "{ 'Int32' : 0 }".Replace("'", "\"");
            Assert.AreEqual(expected, json);

            var bson = obj.ToBson();
            var rehydrated = BsonSerializer.Deserialize<TestClass>(bson);
            Assert.IsTrue(bson.SequenceEqual(rehydrated.ToBson()));
        }

        [Test]
        public void TestOne() {
            var obj = new TestClass {
                Int32 = 1
            };
            var json = obj.ToJson();
            var expected = "{ 'Int32' : 1 }".Replace("'", "\"");
            Assert.AreEqual(expected, json);

            var bson = obj.ToBson();
            var rehydrated = BsonSerializer.Deserialize<TestClass>(bson);
            Assert.IsTrue(bson.SequenceEqual(rehydrated.ToBson()));
        }

        [Test]
        public void TestMax() {
            var obj = new TestClass {
                Int32 = int.MaxValue
            };
            var json = obj.ToJson();
            var expected = ("{ 'Int32' : " + int.MaxValue.ToString() + " }").Replace("'", "\"");
            Assert.AreEqual(expected, json);

            var bson = obj.ToBson();
            var rehydrated = BsonSerializer.Deserialize<TestClass>(bson);
            Assert.IsTrue(bson.SequenceEqual(rehydrated.ToBson()));
        }
    }

    [TestFixture]
    public class Int64PropertySerializerTests {
        public class TestClass {
            public long Int64 { get; set; }
        }

        [Test]
        public void TestMin() {
            var obj = new TestClass {
                Int64 = long.MinValue
            };
            var json = obj.ToJson();
            var expected = ("{ 'Int64' : " + long.MinValue.ToString() + " }").Replace("'", "\"");
            Assert.AreEqual(expected, json);

            var bson = obj.ToBson();
            var rehydrated = BsonSerializer.Deserialize<TestClass>(bson);
            Assert.IsTrue(bson.SequenceEqual(rehydrated.ToBson()));
        }

        [Test]
        public void TestMinusOne() {
            var obj = new TestClass {
                Int64 = -1
            };
            var json = obj.ToJson();
            var expected = "{ 'Int64' : -1 }".Replace("'", "\"");
            Assert.AreEqual(expected, json);

            var bson = obj.ToBson();
            var rehydrated = BsonSerializer.Deserialize<TestClass>(bson);
            Assert.IsTrue(bson.SequenceEqual(rehydrated.ToBson()));
        }

        [Test]
        public void TestZero() {
            var obj = new TestClass {
                Int64 = 0
            };
            var json = obj.ToJson();
            var expected = "{ 'Int64' : 0 }".Replace("'", "\"");
            Assert.AreEqual(expected, json);

            var bson = obj.ToBson();
            var rehydrated = BsonSerializer.Deserialize<TestClass>(bson);
            Assert.IsTrue(bson.SequenceEqual(rehydrated.ToBson()));
        }

        [Test]
        public void TestOne() {
            var obj = new TestClass {
                Int64 = 1
            };
            var json = obj.ToJson();
            var expected = "{ 'Int64' : 1 }".Replace("'", "\"");
            Assert.AreEqual(expected, json);

            var bson = obj.ToBson();
            var rehydrated = BsonSerializer.Deserialize<TestClass>(bson);
            Assert.IsTrue(bson.SequenceEqual(rehydrated.ToBson()));
        }

        [Test]
        public void TestMax() {
            var obj = new TestClass {
                Int64 = long.MaxValue
            };
            var json = obj.ToJson();
            var expected = ("{ 'Int64' : " + long.MaxValue.ToString() + " }").Replace("'", "\"");
            Assert.AreEqual(expected, json);

            var bson = obj.ToBson();
            var rehydrated = BsonSerializer.Deserialize<TestClass>(bson);
            Assert.IsTrue(bson.SequenceEqual(rehydrated.ToBson()));
        }
    }

    [TestFixture]
    public class ObjectIdPropertySerializerTests {
        public class TestClass {
            public ObjectId ObjectId { get; set; }
        }

        [Test]
        public void TestSerialize() {
            var obj = new TestClass {
                ObjectId = new ObjectId(1, 2)
            };
            var json = obj.ToJson();
            var expected = ("{ 'ObjectId' : { '$oid' : '000000010000000000000002' } }").Replace("'", "\"");
            Assert.AreEqual(expected, json);

            var bson = obj.ToBson();
            var rehydrated = BsonSerializer.Deserialize<TestClass>(bson);
            Assert.IsTrue(bson.SequenceEqual(rehydrated.ToBson()));
        }
    }

    [TestFixture]
    public class StringPropertySerializerTests {
        public class TestClass {
            public String String { get; set; }
        }

        [Test]
        public void TestNull() {
            var obj = new TestClass {
                String = null
            };
            var json = obj.ToJson();
            var expected = ("{ 'String' : null }").Replace("'", "\"");
            Assert.AreEqual(expected, json);

            var bson = obj.ToBson();
            var rehydrated = BsonSerializer.Deserialize<TestClass>(bson);
            Assert.IsTrue(bson.SequenceEqual(rehydrated.ToBson()));
        }

        [Test]
        public void TestEmpty() {
            var obj = new TestClass {
                String = String.Empty
            };
            var json = obj.ToJson();
            var expected = ("{ 'String' : '' }").Replace("'", "\"");
            Assert.AreEqual(expected, json);

            var bson = obj.ToBson();
            var rehydrated = BsonSerializer.Deserialize<TestClass>(bson);
            Assert.IsTrue(bson.SequenceEqual(rehydrated.ToBson()));
        }

        [Test]
        public void TestHelloWorld() {
            var obj = new TestClass {
                String = "Hello World"
            };
            var json = obj.ToJson();
            var expected = ("{ 'String' : 'Hello World' }").Replace("'", "\"");
            Assert.AreEqual(expected, json);

            var bson = obj.ToBson();
            var rehydrated = BsonSerializer.Deserialize<TestClass>(bson);
            Assert.IsTrue(bson.SequenceEqual(rehydrated.ToBson()));
        }
    }
}
