﻿#region license
// Copyright (c) 2005 - 2007 Ayende Rahien (ayende@ayende.com)
// All rights reserved.
// 
// Redistribution and use in source and binary forms, with or without modification,
// are permitted provided that the following conditions are met:
// 
//     * Redistributions of source code must retain the above copyright notice,
//     this list of conditions and the following disclaimer.
//     * Redistributions in binary form must reproduce the above copyright notice,
//     this list of conditions and the following disclaimer in the documentation
//     and/or other materials provided with the distribution.
//     * Neither the name of Ayende Rahien nor the names of its
//     contributors may be used to endorse or promote products derived from this
//     software without specific prior written permission.
// 
// THIS SOFTWARE IS PROVIDED BY THE COPYRIGHT HOLDERS AND CONTRIBUTORS "AS IS" AND
// ANY EXPRESS OR IMPLIED WARRANTIES, INCLUDING, BUT NOT LIMITED TO, THE IMPLIED
// WARRANTIES OF MERCHANTABILITY AND FITNESS FOR A PARTICULAR PURPOSE ARE
// DISCLAIMED. IN NO EVENT SHALL THE COPYRIGHT OWNER OR CONTRIBUTORS BE LIABLE
// FOR ANY DIRECT, INDIRECT, INCIDENTAL, SPECIAL, EXEMPLARY, OR CONSEQUENTIAL
// DAMAGES (INCLUDING, BUT NOT LIMITED TO, PROCUREMENT OF SUBSTITUTE GOODS OR
// SERVICES; LOSS OF USE, DATA, OR PROFITS; OR BUSINESS INTERRUPTION) HOWEVER
// CAUSED AND ON ANY THEORY OF LIABILITY, WHETHER IN CONTRACT, STRICT LIABILITY,
// OR TORT (INCLUDING NEGLIGENCE OR OTHERWISE) ARISING IN ANY WAY OUT OF THE USE OF
// THIS SOFTWARE, EVEN IF ADVISED OF THE POSSIBILITY OF SUCH DAMAGE.
#endregion


using MbUnit.Framework;
using Rhino.Mocks.Constraints;

namespace Rhino.Mocks.Tests.Constraints
{
	[TestFixture]
	public class ListConstraintTests
	{
		[Test]
		public void InIs()
		{
			AbstractConstraint list = List.IsIn('a');
			Assert.IsTrue(list.Eval("ayende"));
			Assert.IsFalse(list.Eval("sheep"));
			Assert.AreEqual("list contains [a]", list.Message);
		}

		[Test]
		public void OneOf()
		{
			AbstractConstraint list = List.OneOf(new string[] {"Ayende", "Rahien", "Hello", "World"});
			Assert.IsTrue(list.Eval("Ayende"));
			Assert.IsFalse(list.Eval("sheep"));
			Assert.AreEqual("one of [Ayende, Rahien, Hello, World]", list.Message);
		}

		[Test]
		public void Equal()
		{
			AbstractConstraint list = List.Equal(new string[] {"Ayende", "Rahien", "Hello", "World"});
			Assert.IsTrue(list.Eval(new string[] {"Ayende", "Rahien", "Hello", "World"}));
			Assert.IsFalse(list.Eval(new string[] {"Ayende", "Rahien", "World", "Hello"}));
			Assert.IsFalse(list.Eval(new string[] {"Ayende", "Rahien", "World"}));
			Assert.IsFalse(list.Eval(5));
			Assert.AreEqual("equal to collection [Ayende, Rahien, Hello, World]", list.Message);

		}

        [Test]
        public void Count()
        {
            AbstractConstraint list = List.Count(Is.Equal(4));
            Assert.IsTrue(list.Eval(new string[] { "Ayende", "Rahien", "Hello", "World" }));
            Assert.IsFalse(list.Eval(new string[] { "Ayende", "Rahien", "World" }));
            Assert.IsFalse(list.Eval(5));
            Assert.AreEqual("collection count equal to 4", list.Message);
        }

        [Test]
        public void Element()
        {
            AbstractConstraint list = List.Element(2, Is.Equal("Hello"));
            Assert.IsTrue(list.Eval(new string[] { "Ayende", "Rahien", "Hello", "World" }));
            Assert.IsFalse(list.Eval(new string[] { "Ayende", "Rahien", "World", "Hello" }));
            Assert.IsFalse(list.Eval(new string[] { "Ayende", "Rahien" }));
            Assert.IsFalse(list.Eval(5));
            Assert.AreEqual("element at index 2 equal to Hello", list.Message);
        }
	}
}
