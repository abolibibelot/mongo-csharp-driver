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
    public class BytePropertySerializerTests {
        public class TestClass {
            [BsonUseCompactRepresentation]
            public byte C { get; set; }
            public byte F { get; set; }
        }

        [Test]
        public void TestMin() {
            var obj = new TestClass {
                C = byte.MinValue,
                F = byte.MinValue
            };
            var json = obj.ToJson();
            var expected = "{ 'C' : 0, 'F' : { '_t' : 'System.Byte', 'v' : 0 } }".Replace("'", "\"");
            Assert.AreEqual(expected, json);

            var bson = obj.ToBson();
            var rehydrated = BsonSerializer.Deserialize<TestClass>(bson);
            Assert.AreEqual(rehydrated.C, rehydrated.F);
            Assert.IsTrue(bson.SequenceEqual(rehydrated.ToBson()));
        }

        [Test]
        public void TestZero() {
            var obj = new TestClass {
                C = 0,
                F = 0
            };
            var json = obj.ToJson();
            var expected = "{ 'C' : 0, 'F' : { '_t' : 'System.Byte', 'v' : 0 } }".Replace("'", "\"");
            Assert.AreEqual(expected, json);

            var bson = obj.ToBson();
            var rehydrated = BsonSerializer.Deserialize<TestClass>(bson);
            Assert.AreEqual(rehydrated.C, rehydrated.F);
            Assert.IsTrue(bson.SequenceEqual(rehydrated.ToBson()));
        }

        [Test]
        public void TestOne() {
            var obj = new TestClass {
                C = 1,
                F = 1
            };
            var json = obj.ToJson();
            var expected = "{ 'C' : 1, 'F' : { '_t' : 'System.Byte', 'v' : 1 } }".Replace("'", "\"");
            Assert.AreEqual(expected, json);

            var bson = obj.ToBson();
            var rehydrated = BsonSerializer.Deserialize<TestClass>(bson);
            Assert.AreEqual(rehydrated.C, rehydrated.F);
            Assert.IsTrue(bson.SequenceEqual(rehydrated.ToBson()));
        }

        [Test]
        public void TestMax() {
            var obj = new TestClass {
                C = byte.MaxValue,
                F = byte.MaxValue
            };
            var json = obj.ToJson();
            var expected = "{ 'C' : 255, 'F' : { '_t' : 'System.Byte', 'v' : 255 } }".Replace("'", "\"");
            Assert.AreEqual(expected, json);

            var bson = obj.ToBson();
            var rehydrated = BsonSerializer.Deserialize<TestClass>(bson);
            Assert.AreEqual(rehydrated.C, rehydrated.F);
            Assert.IsTrue(bson.SequenceEqual(rehydrated.ToBson()));
        }
    }

    [TestFixture]
    public class CharPropertySerializerTests {
        public class TestClass {
            [BsonUseCompactRepresentation]
            public char C { get; set; }
            public char F { get; set; }
        }

        [Test]
        public void TestMin() {
            var obj = new TestClass {
                C = char.MinValue,
                F = char.MinValue
            };
            var json = obj.ToJson();
            var expected = "{ 'C' : '\\u0000', 'F' : { '_t' : 'System.Char', 'v' : '\\u0000' } }".Replace("'", "\"");
            Assert.AreEqual(expected, json);

            var bson = obj.ToBson();
            var rehydrated = BsonSerializer.Deserialize<TestClass>(bson);
            Assert.IsTrue(bson.SequenceEqual(rehydrated.ToBson()));
        }

        public void TestZero() {
            var obj = new TestClass {
                C = (char) 0,
                F = (char) 0
            };
            var json = obj.ToJson();
            var expected = "{ 'C' : '\\u0000', 'F' : { '_t' : 'System.Char', 'v' : '\\u0000' } }".Replace("'", "\"");
            Assert.AreEqual(expected, json);

            var bson = obj.ToBson();
            var rehydrated = BsonSerializer.Deserialize<TestClass>(bson);
            Assert.IsTrue(bson.SequenceEqual(rehydrated.ToBson()));
        }

        [Test]
        public void TestOne() {
            var obj = new TestClass {
                C = (char) 1,
                F = (char) 1
            };
            var json = obj.ToJson();
            var expected = "{ 'C' : '\\u0001', 'F' : { '_t' : 'System.Char', 'v' : '\\u0001' } }".Replace("'", "\"");
            Assert.AreEqual(expected, json);

            var bson = obj.ToBson();
            var rehydrated = BsonSerializer.Deserialize<TestClass>(bson);
            Assert.IsTrue(bson.SequenceEqual(rehydrated.ToBson()));
        }

        [Test]
        public void TestA() {
            var obj = new TestClass {
                C = 'A',
                F = 'A'
            };
            var json = obj.ToJson();
            var expected = "{ 'C' : 'A', 'F' : { '_t' : 'System.Char', 'v' : 'A' } }".Replace("'", "\"");
            Assert.AreEqual(expected, json);

            var bson = obj.ToBson();
            var rehydrated = BsonSerializer.Deserialize<TestClass>(bson);
            Assert.IsTrue(bson.SequenceEqual(rehydrated.ToBson()));
        }

        [Test]
        public void TestMax() {
            var obj = new TestClass {
                C = char.MaxValue,
                F = char.MaxValue
            };
            var json = obj.ToJson();
            var expected = "{ 'C' : '\\uffff', 'F' : { '_t' : 'System.Char', 'v' : '\\uffff' } }".Replace("'", "\"");
            Assert.AreEqual(expected, json);

            var bson = obj.ToBson();
            var rehydrated = BsonSerializer.Deserialize<TestClass>(bson);
            Assert.IsTrue(bson.SequenceEqual(rehydrated.ToBson()));
        }
    }

    // TODO: CultureInfoPropertySerializeTests

    [TestFixture]
    public class DateTimeOffsetPropertySerializerTests {
        public class TestClass {
            [BsonUseCompactRepresentation]
            public DateTimeOffset C { get; set; }
            public DateTimeOffset F { get; set; }
        }

        // TODO: more DateTimeOffset tests

        [Test]
        public void TestSerializeDateTimeOffset() {
            var value = new DateTimeOffset(new DateTime(2010, 10, 8, 11, 29, 0), TimeSpan.FromHours(-4));
            var obj = new TestClass {
                C = value,
                F = value
            };
            var json = obj.ToJson();
            var expected = "{ 'C' : #C, 'F' : #F }";
            expected = expected.Replace("#C", string.Format("[{0}, {1}]", value.DateTime.Ticks, value.Offset.Ticks));
            expected = expected.Replace("#F", "{ '_t' : 'System.DateTimeOffset', 'dt' : '2010-10-08T11:29:00', 'o' : '-04:00' }");
            expected = expected.Replace("'", "\"");
            Assert.AreEqual(expected, json);

            var bson = obj.ToBson();
            var rehydrated = BsonSerializer.Deserialize<TestClass>(bson);
            Assert.IsTrue(bson.SequenceEqual(rehydrated.ToBson()));
        }
    }

    // TODO: DecimalPropertySerializerTests

    [TestFixture]
    public class Int16PropertySerializerTests {
        public class TestClass {
            [BsonUseCompactRepresentation]
            public short C { get; set; }
            public short F { get; set; }
        }

        [Test]
        public void TestMin() {
            var obj = new TestClass {
                C = short.MinValue,
                F = short.MinValue
            };
            var json = obj.ToJson();
            var expected = ("{ 'C' : -32768, 'F' : { '_t' : 'System.Int16', 'v' : -32768 } }").Replace("'", "\"");
            Assert.AreEqual(expected, json);

            var bson = obj.ToBson();
            var rehydrated = BsonSerializer.Deserialize<TestClass>(bson);
            Assert.IsTrue(bson.SequenceEqual(rehydrated.ToBson()));
        }

        [Test]
        public void TestMinusOne() {
            var obj = new TestClass {
                C = -1,
                F = -1
            };
            var json = obj.ToJson();
            var expected = ("{ 'C' : -1, 'F' : { '_t' : 'System.Int16', 'v' : -1 } }").Replace("'", "\"");
            Assert.AreEqual(expected, json);

            var bson = obj.ToBson();
            var rehydrated = BsonSerializer.Deserialize<TestClass>(bson);
            Assert.IsTrue(bson.SequenceEqual(rehydrated.ToBson()));
        }

        [Test]
        public void TestZero() {
            var obj = new TestClass {
                C = 0,
                F = 0
            };
            var json = obj.ToJson();
            var expected = ("{ 'C' : 0, 'F' : { '_t' : 'System.Int16', 'v' : 0 } }").Replace("'", "\"");
            Assert.AreEqual(expected, json);

            var bson = obj.ToBson();
            var rehydrated = BsonSerializer.Deserialize<TestClass>(bson);
            Assert.IsTrue(bson.SequenceEqual(rehydrated.ToBson()));
        }

        [Test]
        public void TestOne() {
            var obj = new TestClass {
                C = 1,
                F = 1
            };
            var json = obj.ToJson();
            var expected = ("{ 'C' : 1, 'F' : { '_t' : 'System.Int16', 'v' : 1 } }").Replace("'", "\"");
            Assert.AreEqual(expected, json);

            var bson = obj.ToBson();
            var rehydrated = BsonSerializer.Deserialize<TestClass>(bson);
            Assert.IsTrue(bson.SequenceEqual(rehydrated.ToBson()));
        }

        [Test]
        public void TestMax() {
            var obj = new TestClass {
                C = short.MaxValue,
                F = short.MaxValue
            };
            var json = obj.ToJson();
            var expected = ("{ 'C' : 32767, 'F' : { '_t' : 'System.Int16', 'v' : 32767 } }").Replace("'", "\"");
            Assert.AreEqual(expected, json);

            var bson = obj.ToBson();
            var rehydrated = BsonSerializer.Deserialize<TestClass>(bson);
            Assert.IsTrue(bson.SequenceEqual(rehydrated.ToBson()));
        }
    }

    [TestFixture]
    public class SBytePropertySerializerTests {
        public class TestClass {
            [BsonUseCompactRepresentation]
            public sbyte C { get; set; }
            public sbyte F { get; set; }
        }

        [Test]
        public void TestMin() {
            var obj = new TestClass {
                C = sbyte.MinValue,
                F = sbyte.MinValue
            };
            var json = obj.ToJson();
            var expected = ("{ 'C' : -128, 'F' : { '_t' : 'System.SByte', 'v' : -128 } }").Replace("'", "\"");
            Assert.AreEqual(expected, json);

            var bson = obj.ToBson();
            var rehydrated = BsonSerializer.Deserialize<TestClass>(bson);
            Assert.IsTrue(bson.SequenceEqual(rehydrated.ToBson()));
        }

        [Test]
        public void TestMinusOne() {
            var obj = new TestClass {
                C = -1,
                F = -1
            };
            var json = obj.ToJson();
            var expected = ("{ 'C' : -1, 'F' : { '_t' : 'System.SByte', 'v' : -1 } }").Replace("'", "\"");
            Assert.AreEqual(expected, json);

            var bson = obj.ToBson();
            var rehydrated = BsonSerializer.Deserialize<TestClass>(bson);
            Assert.IsTrue(bson.SequenceEqual(rehydrated.ToBson()));
        }

        [Test]
        public void TestZero() {
            var obj = new TestClass {
                C = 0,
                F = 0
            };
            var json = obj.ToJson();
            var expected = ("{ 'C' : 0, 'F' : { '_t' : 'System.SByte', 'v' : 0 } }").Replace("'", "\"");
            Assert.AreEqual(expected, json);

            var bson = obj.ToBson();
            var rehydrated = BsonSerializer.Deserialize<TestClass>(bson);
            Assert.IsTrue(bson.SequenceEqual(rehydrated.ToBson()));
        }

        [Test]
        public void TestOne() {
            var obj = new TestClass {
                C = 1,
                F = 1
            };
            var json = obj.ToJson();
            var expected = ("{ 'C' : 1, 'F' : { '_t' : 'System.SByte', 'v' : 1 } }").Replace("'", "\"");
            Assert.AreEqual(expected, json);

            var bson = obj.ToBson();
            var rehydrated = BsonSerializer.Deserialize<TestClass>(bson);
            Assert.IsTrue(bson.SequenceEqual(rehydrated.ToBson()));
        }

        [Test]
        public void TestMax() {
            var obj = new TestClass {
                C = sbyte.MaxValue,
                F = sbyte.MaxValue
            };
            var json = obj.ToJson();
            var expected = ("{ 'C' : 127, 'F' : { '_t' : 'System.SByte', 'v' : 127 } }").Replace("'", "\"");
            Assert.AreEqual(expected, json);

            var bson = obj.ToBson();
            var rehydrated = BsonSerializer.Deserialize<TestClass>(bson);
            Assert.IsTrue(bson.SequenceEqual(rehydrated.ToBson()));
        }
    }

    [TestFixture]
    public class SinglePropertySerializerTests {
        public class TestClass {
            [BsonUseCompactRepresentation]
            public float C { get; set; }
            public float F { get; set; }
        }

        [Test]
        public void TestMin() {
            var obj = new TestClass {
                C = float.MinValue,
                F = float.MinValue
            };
            var json = obj.ToJson();
            var expected = ("{ 'C' : #, 'F' : { '_t' : 'System.Single', 'v' : # } }").Replace("#", double.MinValue.ToString()).Replace("'", "\"");
            Assert.AreEqual(expected, json);

            var bson = obj.ToBson();
            var rehydrated = BsonSerializer.Deserialize<TestClass>(bson);
            Assert.IsTrue(bson.SequenceEqual(rehydrated.ToBson()));
        }

        [Test]
        public void TestMinusOne() {
            var obj = new TestClass {
                C = -1.0F,
                F = -1.0F
            };
            var json = obj.ToJson();
            var expected = ("{ 'C' : -1, 'F' : { '_t' : 'System.Single', 'v' : -1 } }").Replace("'", "\"");
            Assert.AreEqual(expected, json);

            var bson = obj.ToBson();
            var rehydrated = BsonSerializer.Deserialize<TestClass>(bson);
            Assert.IsTrue(bson.SequenceEqual(rehydrated.ToBson()));
        }

        [Test]
        public void TestZero() {
            var obj = new TestClass {
                C = 0.0F,
                F = 0.0F
            };
            var json = obj.ToJson();
            var expected = ("{ 'C' : 0, 'F' : { '_t' : 'System.Single', 'v' : 0 } }").Replace("'", "\"");
            Assert.AreEqual(expected, json);

            var bson = obj.ToBson();
            var rehydrated = BsonSerializer.Deserialize<TestClass>(bson);
            Assert.IsTrue(bson.SequenceEqual(rehydrated.ToBson()));
        }

        [Test]
        public void TestOne() {
            var obj = new TestClass {
                C = 1.0F,
                F = 1.0F
            };
            var json = obj.ToJson();
            var expected = ("{ 'C' : 1, 'F' : { '_t' : 'System.Single', 'v' : 1 } }").Replace("'", "\"");
            Assert.AreEqual(expected, json);

            var bson = obj.ToBson();
            var rehydrated = BsonSerializer.Deserialize<TestClass>(bson);
            Assert.IsTrue(bson.SequenceEqual(rehydrated.ToBson()));
        }

        [Test]
        public void TestMax() {
            var obj = new TestClass {
                C = float.MaxValue,
                F = float.MaxValue
            };
            var json = obj.ToJson();
            var expected = ("{ 'C' : #, 'F' : { '_t' : 'System.Single', 'v' : # } }").Replace("#", double.MaxValue.ToString()).Replace("'", "\"");
            Assert.AreEqual(expected, json);

            var bson = obj.ToBson();
            var rehydrated = BsonSerializer.Deserialize<TestClass>(bson);
            Assert.IsTrue(bson.SequenceEqual(rehydrated.ToBson()));
        }

        [Test]
        public void TestNaN() {
            var obj = new TestClass {
                C = float.NaN,
                F = float.NaN
            };
            var json = obj.ToJson();
            var expected = ("{ 'C' : NaN, 'F' : { '_t' : 'System.Single', 'v' : NaN } }").Replace("'", "\"");
            Assert.AreEqual(expected, json);

            var bson = obj.ToBson();
            var rehydrated = BsonSerializer.Deserialize<TestClass>(bson);
            Assert.IsTrue(bson.SequenceEqual(rehydrated.ToBson()));
        }

        [Test]
        public void TestNegativeInfinity() {
            var obj = new TestClass {
                C = float.NegativeInfinity,
                F = float.NegativeInfinity
            };
            var json = obj.ToJson();
            var expected = ("{ 'C' : -Infinity, 'F' : { '_t' : 'System.Single', 'v' : -Infinity } }").Replace("'", "\"");
            Assert.AreEqual(expected, json);

            var bson = obj.ToBson();
            var rehydrated = BsonSerializer.Deserialize<TestClass>(bson);
            Assert.IsTrue(bson.SequenceEqual(rehydrated.ToBson()));
        }

        [Test]
        public void TestPositiveInfinity() {
            var obj = new TestClass {
                C = float.PositiveInfinity,
                F = float.PositiveInfinity
            };
            var json = obj.ToJson();
            var expected = ("{ 'C' : Infinity, 'F' : { '_t' : 'System.Single', 'v' : Infinity } }").Replace("'", "\"");
            Assert.AreEqual(expected, json);

            var bson = obj.ToBson();
            var rehydrated = BsonSerializer.Deserialize<TestClass>(bson);
            Assert.IsTrue(bson.SequenceEqual(rehydrated.ToBson()));
        }
    }

    [TestFixture]
    public class TimeSpanPropertySerializerTests {
        public class TestClass {
            [BsonUseCompactRepresentation]
            public TimeSpan C { get; set; }
            public TimeSpan F { get; set; }
        }

        [Test]
        public void TestMinValue() {
            var obj = new TestClass {
                C = TimeSpan.MinValue,
                F = TimeSpan.MinValue
            };
            var json = obj.ToJson();
            var expected = "{ 'C' : #C, 'F' : { '_t' : 'System.TimeSpan', 'v' : '#F' } }";
            expected = expected.Replace("#C", TimeSpan.MinValue.Ticks.ToString());
            expected = expected.Replace("#F", TimeSpan.MinValue.ToString());
            expected = expected.Replace("'", "\"");
            Assert.AreEqual(expected, json);

            var bson = obj.ToBson();
            var rehydrated = BsonSerializer.Deserialize<TestClass>(bson);
            Assert.IsTrue(bson.SequenceEqual(rehydrated.ToBson()));
        }

        [Test]
        public void TestMinusOneMinute() {
            var obj = new TestClass {
                C = TimeSpan.FromMinutes(-1),
                F = TimeSpan.FromMinutes(-1)
            };
            var json = obj.ToJson();
            var expected = "{ 'C' : #C, 'F' : { '_t' : 'System.TimeSpan', 'v' : '#F' } }";
            expected = expected.Replace("#C", TimeSpan.FromMinutes(-1).Ticks.ToString());
            expected = expected.Replace("#F", TimeSpan.FromMinutes(-1).ToString());
            expected = expected.Replace("'", "\"");
            Assert.AreEqual(expected, json);

            var bson = obj.ToBson();
            var rehydrated = BsonSerializer.Deserialize<TestClass>(bson);
            Assert.IsTrue(bson.SequenceEqual(rehydrated.ToBson()));
        }

        [Test]
        public void TestMinusOneSecond() {
            var obj = new TestClass {
                C = TimeSpan.FromSeconds(-1),
                F = TimeSpan.FromSeconds(-1)
            };
            var json = obj.ToJson();
            var expected = "{ 'C' : #C, 'F' : { '_t' : 'System.TimeSpan', 'v' : '#F' } }";
            expected = expected.Replace("#C", TimeSpan.FromSeconds(-1).Ticks.ToString());
            expected = expected.Replace("#F", TimeSpan.FromSeconds(-1).ToString());
            expected = expected.Replace("'", "\"");
            Assert.AreEqual(expected, json);

            var bson = obj.ToBson();
            var rehydrated = BsonSerializer.Deserialize<TestClass>(bson);
            Assert.IsTrue(bson.SequenceEqual(rehydrated.ToBson()));
        }

        [Test]
        public void TestZero() {
            var obj = new TestClass {
                C = TimeSpan.Zero,
                F = TimeSpan.Zero
            };
            var json = obj.ToJson();
            var expected = "{ 'C' : #C, 'F' : { '_t' : 'System.TimeSpan', 'v' : '#F' } }";
            expected = expected.Replace("#C", TimeSpan.Zero.Ticks.ToString());
            expected = expected.Replace("#F", TimeSpan.Zero.ToString());
            expected = expected.Replace("'", "\"");
            Assert.AreEqual(expected, json);

            var bson = obj.ToBson();
            var rehydrated = BsonSerializer.Deserialize<TestClass>(bson);
            Assert.IsTrue(bson.SequenceEqual(rehydrated.ToBson()));
        }

        [Test]
        public void TestOneSecond() {
            var obj = new TestClass {
                C = TimeSpan.FromSeconds(1),
                F = TimeSpan.FromSeconds(1)
            };
            var json = obj.ToJson();
            var expected = "{ 'C' : #C, 'F' : { '_t' : 'System.TimeSpan', 'v' : '#F' } }";
            expected = expected.Replace("#C", TimeSpan.FromSeconds(1).Ticks.ToString());
            expected = expected.Replace("#F", TimeSpan.FromSeconds(1).ToString());
            expected = expected.Replace("'", "\"");
            Assert.AreEqual(expected, json);

            var bson = obj.ToBson();
            var rehydrated = BsonSerializer.Deserialize<TestClass>(bson);
            Assert.IsTrue(bson.SequenceEqual(rehydrated.ToBson()));
        }

        [Test]
        public void TestOneMinute() {
            var obj = new TestClass {
                C = TimeSpan.FromMinutes(1),
                F = TimeSpan.FromMinutes(1)
            };
            var json = obj.ToJson();
            var expected = "{ 'C' : #C, 'F' : { '_t' : 'System.TimeSpan', 'v' : '#F' } }";
            expected = expected.Replace("#C", TimeSpan.FromMinutes(1).Ticks.ToString());
            expected = expected.Replace("#F", TimeSpan.FromMinutes(1).ToString());
            expected = expected.Replace("'", "\"");
            Assert.AreEqual(expected, json);

            var bson = obj.ToBson();
            var rehydrated = BsonSerializer.Deserialize<TestClass>(bson);
            Assert.IsTrue(bson.SequenceEqual(rehydrated.ToBson()));
        }

        [Test]
        public void TestMaxValue() {
            var obj = new TestClass {
                C = TimeSpan.MaxValue,
                F = TimeSpan.MaxValue
            };
            var json = obj.ToJson();
            var expected = "{ 'C' : #C, 'F' : { '_t' : 'System.TimeSpan', 'v' : '#F' } }";
            expected = expected.Replace("#C", TimeSpan.MaxValue.Ticks.ToString());
            expected = expected.Replace("#F", TimeSpan.MaxValue.ToString());
            expected = expected.Replace("'", "\"");
            Assert.AreEqual(expected, json);

            var bson = obj.ToBson();
            var rehydrated = BsonSerializer.Deserialize<TestClass>(bson);
            Assert.IsTrue(bson.SequenceEqual(rehydrated.ToBson()));
        }
    }

    [TestFixture]
    public class UInt16PropertySerializerTests {
        public class TestClass {
            [BsonUseCompactRepresentation]
            public ushort C { get; set; }
            public ushort F { get; set; }
        }

        [Test]
        public void TestMin() {
            var obj = new TestClass {
                C = ushort.MinValue,
                F = ushort.MinValue
            };
            var json = obj.ToJson();
            var expected = "{ 'C' : 0, 'F' : { '_t' : 'System.UInt16', 'v' : 0 } }".Replace("'", "\"");
            Assert.AreEqual(expected, json);

            var bson = obj.ToBson();
            var rehydrated = BsonSerializer.Deserialize<TestClass>(bson);
            Assert.IsTrue(bson.SequenceEqual(rehydrated.ToBson()));
        }

        [Test]
        public void TestZero() {
            var obj = new TestClass {
                C = 0,
                F = 0
            };
            var json = obj.ToJson();
            var expected = "{ 'C' : 0, 'F' : { '_t' : 'System.UInt16', 'v' : 0 } }".Replace("'", "\"");
            Assert.AreEqual(expected, json);

            var bson = obj.ToBson();
            var rehydrated = BsonSerializer.Deserialize<TestClass>(bson);
            Assert.IsTrue(bson.SequenceEqual(rehydrated.ToBson()));
        }

        [Test]
        public void TestOne() {
            var obj = new TestClass {
                C = 1,
                F = 1
            };
            var json = obj.ToJson();
            var expected = "{ 'C' : 1, 'F' : { '_t' : 'System.UInt16', 'v' : 1 } }".Replace("'", "\"");
            Assert.AreEqual(expected, json);

            var bson = obj.ToBson();
            var rehydrated = BsonSerializer.Deserialize<TestClass>(bson);
            Assert.IsTrue(bson.SequenceEqual(rehydrated.ToBson()));
        }

        [Test]
        public void TestMax() {
            var obj = new TestClass {
                C = ushort.MaxValue,
                F = ushort.MaxValue
            };
            var json = obj.ToJson();
            var expected = "{ 'C' : 65535, 'F' : { '_t' : 'System.UInt16', 'v' : 65535 } }".Replace("'", "\"");
            Assert.AreEqual(expected, json);

            var bson = obj.ToBson();
            var rehydrated = BsonSerializer.Deserialize<TestClass>(bson);
            Assert.IsTrue(bson.SequenceEqual(rehydrated.ToBson()));
        }
    }

    [TestFixture]
    public class UInt32PropertySerializerTests {
        public class TestClass {
            [BsonUseCompactRepresentation]
            public uint C { get; set; }
            public uint F { get; set; }
        }

        [Test]
        public void TestMin() {
            var obj = new TestClass {
                C = uint.MinValue,
                F = uint.MinValue
            };
            var json = obj.ToJson();
            var expected = "{ 'C' : 0, 'F' : { '_t' : 'System.UInt32', 'v' : 0 } }".Replace("'", "\"");
            Assert.AreEqual(expected, json);

            var bson = obj.ToBson();
            var rehydrated = BsonSerializer.Deserialize<TestClass>(bson);
            Assert.IsTrue(bson.SequenceEqual(rehydrated.ToBson()));
        }

        [Test]
        public void TestZero() {
            var obj = new TestClass {
                C = 0,
                F = 0
            };
            var json = obj.ToJson();
            var expected = "{ 'C' : 0, 'F' : { '_t' : 'System.UInt32', 'v' : 0 } }".Replace("'", "\"");
            Assert.AreEqual(expected, json);

            var bson = obj.ToBson();
            var rehydrated = BsonSerializer.Deserialize<TestClass>(bson);
            Assert.IsTrue(bson.SequenceEqual(rehydrated.ToBson()));
        }

        [Test]
        public void TestOne() {
            var obj = new TestClass {
                C = 1,
                F = 1
            };
            var json = obj.ToJson();
            var expected = "{ 'C' : 1, 'F' : { '_t' : 'System.UInt32', 'v' : 1 } }".Replace("'", "\"");
            Assert.AreEqual(expected, json);

            var bson = obj.ToBson();
            var rehydrated = BsonSerializer.Deserialize<TestClass>(bson);
            Assert.IsTrue(bson.SequenceEqual(rehydrated.ToBson()));
        }

        [Test]
        public void TestOverflow() {
            var obj = new TestClass {
                C = 4000000000,
                F = 4000000000
            };
            var json = obj.ToJson();
            var expected = "{ 'C' : -294967296, 'F' : { '_t' : 'System.UInt32', 'v' : -294967296 } }".Replace("'", "\"");
            Assert.AreEqual(expected, json);

            var bson = obj.ToBson();
            var rehydrated = BsonSerializer.Deserialize<TestClass>(bson);
            Assert.AreEqual(4000000000, rehydrated.C);
            Assert.AreEqual(4000000000, rehydrated.F);
            Assert.IsTrue(bson.SequenceEqual(rehydrated.ToBson()));
        }

        [Test]
        public void TestMax() {
            var obj = new TestClass {
                C = uint.MaxValue,
                F = uint.MaxValue
            };
            var json = obj.ToJson();
            var expected = "{ 'C' : -1, 'F' : { '_t' : 'System.UInt32', 'v' : -1 } }".Replace("'", "\"");
            Assert.AreEqual(expected, json);

            var bson = obj.ToBson();
            var rehydrated = BsonSerializer.Deserialize<TestClass>(bson);
            Assert.AreEqual(obj.C, rehydrated.C);
            Assert.AreEqual(obj.F, rehydrated.F);
            Assert.IsTrue(bson.SequenceEqual(rehydrated.ToBson()));
        }
    }

    [TestFixture]
    public class UInt64PropertySerializerTests {
        public class TestClass {
            [BsonUseCompactRepresentation]
            public ulong C { get; set; }
            public ulong F { get; set; }
        }

        [Test]
        public void TestMin() {
            var obj = new TestClass {
                C = ulong.MinValue,
                F = ulong.MinValue
            };
            var json = obj.ToJson();
            var expected = "{ 'C' : 0, 'F' : { '_t' : 'System.UInt64', 'v' : 0 } }".Replace("'", "\"");
            Assert.AreEqual(expected, json);

            var bson = obj.ToBson();
            var rehydrated = BsonSerializer.Deserialize<TestClass>(bson);
            Assert.IsTrue(bson.SequenceEqual(rehydrated.ToBson()));
        }

        [Test]
        public void TestZero() {
            var obj = new TestClass {
                C = 0,
                F = 0
            };
            var json = obj.ToJson();
            var expected = "{ 'C' : 0, 'F' : { '_t' : 'System.UInt64', 'v' : 0 } }".Replace("'", "\"");
            Assert.AreEqual(expected, json);

            var bson = obj.ToBson();
            var rehydrated = BsonSerializer.Deserialize<TestClass>(bson);
            Assert.IsTrue(bson.SequenceEqual(rehydrated.ToBson()));
        }

        [Test]
        public void TestOne() {
            var obj = new TestClass {
                C = 1,
                F = 1
            };
            var json = obj.ToJson();
            var expected = "{ 'C' : 1, 'F' : { '_t' : 'System.UInt64', 'v' : 1 } }".Replace("'", "\"");
            Assert.AreEqual(expected, json);

            var bson = obj.ToBson();
            var rehydrated = BsonSerializer.Deserialize<TestClass>(bson);
            Assert.IsTrue(bson.SequenceEqual(rehydrated.ToBson()));
        }

        [Test]
        public void TestMax() {
            var obj = new TestClass {
                C = ulong.MaxValue,
                F = ulong.MaxValue
            };
            var json = obj.ToJson();
            var expected = "{ 'C' : -1, 'F' : { '_t' : 'System.UInt64', 'v' : -1 } }".Replace("'", "\"");
            Assert.AreEqual(expected, json);

            var bson = obj.ToBson();
            var rehydrated = BsonSerializer.Deserialize<TestClass>(bson);
            Assert.IsTrue(bson.SequenceEqual(rehydrated.ToBson()));
        }
    }
}
