﻿using MiniWebDeploy.Deployer.Features.Installation;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MiniWebDeploy.Deployer.Tests
{
    [TestFixture]
    public class ProgramTests
    {
        Dictionary<string, string> _dictionary;

        [SetUp]
        public void SetUp()
        {
            string[] args = { "pathToSite",  "--key1", "value1", "--key2",  "value2" };
            var parser = new ArgsParser();
            _dictionary = parser.Parse(args);
        }

        [Test]
        public void RelativeSitePathIsConvertedIntoAbsolutueSitePath()
        {
            Assert.AreEqual(Path.GetFullPath("pathToSite"), _dictionary["__SITEPATH"]);
        }

        [Test]
        public void KeysAreAllInUppercase()
        {
            Assert.IsTrue(_dictionary.Keys.All(x => x.All(y => Char.IsUpper(y) || Char.IsNumber(y) || y == '_')));
        }

        [Test]
        public void SecondArgIsKey1()
        {
            Assert.AreEqual("value1", _dictionary["KEY1"]);
        }

        [Test]
        public void ThirdArgIsKey2()
        {
            Assert.AreEqual("value2", _dictionary["KEY2"]);
        }
    }
}
