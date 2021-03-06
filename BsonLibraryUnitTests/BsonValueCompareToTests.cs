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

namespace MongoDB.BsonLibrary.UnitTests {
    [TestFixture]
    public class BsonValueCompareToTests {
        [Test]
        public void TestCompareTypeTo() {
            BsonValue[] values = {
                Bson.MinKey,
                Bson.Null,
                BsonInt32.Zero,
                BsonString.Empty,
                new BsonDocument(),
                new BsonArray(),
                new BsonBinaryData(new byte[] { 1 }),
                BsonObjectId.GenerateNewId(),
                BsonBoolean.False,
                new BsonDateTime(DateTime.UtcNow),
                new BsonRegularExpression("pattern")
            };
            for (int i = 0; i < values.Length - 2; i++) {
                Assert.AreEqual(-1, values[i].CompareTypeTo(values[i + 1]));
                Assert.AreEqual(1, values[i + 1].CompareTypeTo(values[i]));
                Assert.IsTrue(values[i] < values[i + 1]);
                Assert.IsTrue(values[i] <= values[i + 1]);
                Assert.IsTrue(values[i] != values[i + 1]);
                Assert.IsFalse(values[i] == values[i + 1]);
                Assert.IsFalse(values[i] > values[i + 1]);
                Assert.IsFalse(values[i] >= values[i + 1]);
                Assert.AreEqual(1, values[i].CompareTypeTo(null));
            }
        }

        [Test]
        public void TestCompareTwoCsharpNulls() {
            BsonValue null1 = null;
            BsonValue null2 = null;
            Assert.IsFalse(null1 < null2);
            Assert.IsTrue(null1 <= null2);
            Assert.IsFalse(null1 != null2);
            Assert.IsTrue(null1 == null2);
            Assert.IsFalse(null1 > null2);
            Assert.IsTrue(null1 >= null2);
        }

        [Test]
        public void TestCompareTwoMaxKeys() {
            Assert.IsFalse(Bson.MaxKey < Bson.MaxKey);
            Assert.IsTrue(Bson.MaxKey <= Bson.MaxKey);
            Assert.IsFalse(Bson.MaxKey != Bson.MaxKey);
            Assert.IsTrue(Bson.MaxKey == Bson.MaxKey);
            Assert.IsFalse(Bson.MaxKey > Bson.MaxKey);
            Assert.IsTrue(Bson.MaxKey >= Bson.MaxKey);
        }

        [Test]
        public void TestCompareTwoMinKeys() {
            Assert.IsFalse(Bson.MinKey < Bson.MinKey);
            Assert.IsTrue(Bson.MinKey <= Bson.MinKey);
            Assert.IsFalse(Bson.MinKey != Bson.MinKey);
            Assert.IsTrue(Bson.MinKey == Bson.MinKey);
            Assert.IsFalse(Bson.MinKey > Bson.MinKey);
            Assert.IsTrue(Bson.MinKey >= Bson.MinKey);
        }

        [Test]
        public void TestCompareTwoBsonNulls() {
            Assert.IsFalse(Bson.Null < Bson.Null);
            Assert.IsTrue(Bson.Null <= Bson.Null);
            Assert.IsFalse(Bson.Null != Bson.Null);
            Assert.IsTrue(Bson.Null == Bson.Null);
            Assert.IsFalse(Bson.Null > Bson.Null);
            Assert.IsTrue(Bson.Null >= Bson.Null);
        }

        [Test]
        public void TestCompareTwoOnes() {
            var n1 = new BsonInt32(1);
            var n2 = new BsonInt32(1);
            Assert.IsFalse(n1 < n2);
            Assert.IsTrue(n1 <= n2);
            Assert.IsFalse(n1 != n2);
            Assert.IsTrue(n1 == n2);
            Assert.IsFalse(n1 > n2);
            Assert.IsTrue(n1 >= n2);
        }

        [Test]
        public void TestCompareOneAndTwo() {
            var n1 = new BsonInt32(1);
            var n2 = new BsonInt32(2);
            Assert.IsTrue(n1 < n2);
            Assert.IsTrue(n1 <= n2);
            Assert.IsTrue(n1 != n2);
            Assert.IsFalse(n1 == n2);
            Assert.IsFalse(n1 > n2);
            Assert.IsFalse(n1 >= n2);
        }

        [Test]
        public void TestCompareDifferentTypeOnes() {
            var n1 = new BsonInt32(1);
            var n2 = new BsonInt64(1);
            var n3 = new BsonDouble(1.0);
            Assert.IsTrue(n1 == n2);
            Assert.IsTrue(n1 == n3);
            Assert.IsTrue(n2 == n1);
            Assert.IsTrue(n2 == n3);
            Assert.IsTrue(n3 == n1);
            Assert.IsTrue(n3 == n2);
        }
    }
}
