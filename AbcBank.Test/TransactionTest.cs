﻿using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AbcBank.Test
{
    [TestFixture]
    public class TransactionTest
    {
        [Test]
        public void transaction()
        {
            Transaction t = new Transaction(5, 0);
            Assert.AreEqual(true, t is Transaction);
        }
    }
}
