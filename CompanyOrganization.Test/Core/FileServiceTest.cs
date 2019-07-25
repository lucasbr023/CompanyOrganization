﻿using System;
using System.Linq;
using CompanyEmployeesBalancing.Resources;
using CompanyEmployeesBalancing.Services.Implementation;
using CompanyEmployeesBalancing.Test.Configuration;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace CompanyEmployeesBalancing.Test.Core
{
    [TestClass]
    public class FileServiceTest
    {
        private readonly LoadService _loadService;
        public FileServiceTest()
        {
            _loadService = DependencyResolver.Get<LoadService>();
        }

        [TestMethod]
        public void TestGetLinesCSVFileWithValuesSuccess()
        {
            var filePath = AppDomain.CurrentDomain.BaseDirectory + @"Files\team.csv";
            var lines = _loadService.GetLinesCsvFile(filePath);
            Assert.IsTrue(lines.Any());
        }

        [TestMethod]
        public void TestGetLinesCSVFileEmpty()
        {
            var filePath = AppDomain.CurrentDomain.BaseDirectory + @"Files\empty.csv";
            var lines = _loadService.GetLinesCsvFile(filePath);
            Assert.IsTrue(!lines.Any());
        }

        [TestMethod]
        public void TestGetLinesCSVFileFileNotFound()
        {
            var filePath = AppDomain.CurrentDomain.BaseDirectory + @"Files\team_not_found.csv";
            var exception = Assert.ThrowsException<Exception>(() => _loadService.GetLinesCsvFile(filePath));
            Assert.AreEqual(Messages.FileNotFound, exception.Message);
        }

        [TestMethod]
        public void TestGetLinesCSVFileInvalidExtension()
        {
            var filePath = AppDomain.CurrentDomain.BaseDirectory + @"Files\team_invalid_extension.txt";
            var exception = Assert.ThrowsException<Exception>(() => _loadService.GetLinesCsvFile(filePath));
            Assert.AreEqual(Messages.FileInvalidExtenssion, exception.Message);
        }
    }
}
