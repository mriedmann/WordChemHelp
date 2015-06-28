/*******************************************************************
ChemHelp (Word ADD-IN)
Copyright (C) 2015 Michael Riedmann <michael_riedmann(AT)live.com>

This program is free software: you can redistribute it and/or modify
it under the terms of the GNU General Public License as published by
the Free Software Foundation, either version 3 of the License, or
(at your option) any later version.

This program is distributed in the hope that it will be useful,
but WITHOUT ANY WARRANTY; without even the implied warranty of
MERCHANTABILITY or FITNESS FOR A PARTICULAR PURPOSE.  See the
GNU General Public License for more details.

You should have received a copy of the GNU General Public License
along with this program.  If not, see <http://www.gnu.org/licenses/>.
*******************************************************************/

using System;
using System.Linq;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using WordChemHelp.Core;

namespace WordChemHelp.Tests
{
    [TestClass]
    public class FormatHelperTests
    {
        [TestMethod]
        public void TestFormat_Charge()
        {
            FormatHelper fh = new FormatHelper();

            FormatString actual = fh.FormatInput("PO4-3");
            FormatString expected = new FormatString("PO43-", new int[] { 0, 0, 1, 2, 2 });

            Assert.AreEqual(expected, actual);
        }

        [TestMethod]
        public void TestFormat_ChargeOnly()
        {
            FormatHelper fh = new FormatHelper();

            FormatString actual = fh.FormatInput("H+");
            FormatString expected = new FormatString("H+", new int[] { 0, 2 });

            Assert.AreEqual(expected, actual);
        }

        [TestMethod]
        public void TestFormat_QuantitiesOnly()
        {
            FormatHelper fh = new FormatHelper();

            FormatString actual = fh.FormatInput("H2SO4");
            FormatString expected = new FormatString("H2SO4", new int[] { 0, 1, 0, 0, 1 });

            Assert.AreEqual(expected, actual);
        }

        [TestMethod]
        // (NH4)2SO4
        // 000101001
        public void TestFormat_Groups()
        {
            FormatHelper fh = new FormatHelper();

            FormatString actual = fh.FormatInput("(NH4)2SO4");
            FormatString expected = new FormatString("(NH4)2SO4", new int[] { 0,0,0,1,0,1,0,0,1 });

            Assert.AreEqual(expected, actual);
        }

        [TestMethod]
        public void TestFormat_Groups2()
        {
            FormatHelper fh = new FormatHelper();

            FormatString actual = fh.FormatInput("Co3(Fe(CN)6)2");
            FormatString expected = new FormatString("Co3(Fe(CN)6)2", "0010000000101");

            Assert.AreEqual(expected, actual);
        }
    }
}
