using Xunit;
using System.IO;
using System.Collections.Generic;
using System;
using Moq;
using System.Text;
using DirStat.Dao;
using DirStat.Dao.Impl;
using System.Linq;

namespace DirStat.Tests
{
    public class DirStatisticsTests
    {

        [Fact]
        public void TestMethod_GetTopBigFiles_CheckSort()
        {
            // Arrange
            var expected = GetTopBigFiles_GetExpected();
            var daoMock = new Mock<IStatItemDao>();
            daoMock.Setup(d => d.GetByDirNameRec("")).Returns(GetTestData());
            var dirStat = new DirStatistics("", daoMock.Object);
            // Act
            var actual = dirStat.GetTopBigFiles(3);
            // Assert
            Assert.True(actual.SequenceEqual(expected));
        }

        [Fact]
        public void GetTopOldFiles()
        {
            // Arrange
            var expected = GetTopOldFiles_GetExpected();
            var daoMock = new Mock<IStatItemDao>();
            daoMock.Setup(d => d.GetByDirNameRec("")).Returns(GetTestData());
            var dirStat = new DirStatistics("", daoMock.Object);
            // Act
            var actual = dirStat.GetTopOldFiles(3);
            // Assert
            Assert.True(actual.SequenceEqual(expected));
        }
        [Fact]
        public void GetTopExtensions()
        {
            // Arrange
            // Act
            // Assert
        }

        private List<StatItem> GetTopBigFiles_GetExpected()
        {
            var result = new List<StatItem>()
            {
                new StatItem {
                    FullName = "",
                    DirName = "",
                    FileName = "",
                    Size = 300,
                    CreationTime = DateTime.Parse("03.05.2021 11:15:10"),
                    RegTime = DateTime.Parse("06.04.2022 8:39:22")},
                new StatItem {
                    FullName = @"C:\TestDir\test2.txt",
                    DirName = @"C:\TestDir\",
                    FileName = "test2.txt",
                    Size = 200,
                    CreationTime = DateTime.Parse("01.05.2021 11:15:10"),
                    RegTime = DateTime.Parse("06.04.2022 8:39:22")},
                new StatItem {
                    FullName = @"C:\TestDir\SubDir\test1.txt",
                    DirName = @"C:\TestDir\SubDir\",
                    FileName = "test1.txt",
                    Size = 100,
                    CreationTime = DateTime.Parse("02.05.2021 11:15:10"),
                    RegTime = DateTime.Parse("06.04.2022 8:39:22")},
            };
            return result;
        }
        private List<StatItem> GetTopOldFiles_GetExpected()
        {
            var result = new List<StatItem>()
            {
                new StatItem {
                    FullName = @"C:\TestDir\test3.txt",
                    DirName = @"C:\TestDir\",
                    FileName = "test3.xls",
                    Size = 300,
                    CreationTime = DateTime.Parse("03.05.2021 11:15:10"),
                    RegTime = DateTime.Parse("06.04.2022 8:39:22")},
                new StatItem {
                    FullName = @"C:\TestDir\SubDir\test1.txt",
                    DirName = @"C:\TestDir\SubDir\",
                    FileName = "test1.txt",
                    Size = 100,
                    CreationTime = DateTime.Parse("02.05.2021 11:15:10"),
                    RegTime = DateTime.Parse("06.04.2022 8:39:22")},
                new StatItem {
                    FullName = @"C:\TestDir\test2.txt",
                    DirName = @"C:\TestDir\",
                    FileName = "test2.txt",
                    Size = 200,
                    CreationTime = DateTime.Parse("01.05.2021 11:15:10"),
                    RegTime = DateTime.Parse("06.04.2022 8:39:22")},
            };
            return result;
        }
        private List<StatItem> GetTopExtensions_GetExpected()
        {
            throw new NotImplementedException();
        }
        private List<StatItem> GetTestData()
        {
            var result = new List<StatItem>()
            {
                new StatItem {
                    FullName = @"C:\TestDir\SubDir\test1.txt",
                    DirName = @"C:\TestDir\SubDir\",
                    FileName = "test1.txt",
                    Size = 100,
                    CreationTime = DateTime.Parse("02.05.2021 11:15:10"),
                    RegTime = DateTime.Parse("06.04.2022 8:39:22")},
                new StatItem {
                    FullName = @"C:\TestDir\test2.txt",
                    DirName = @"C:\TestDir\",
                    FileName = "test2.txt",
                    Size = 200,
                    CreationTime = DateTime.Parse("01.05.2021 11:15:10"),
                    RegTime = DateTime.Parse("06.04.2022 8:39:22")},
                new StatItem {
                    FullName = @"C:\TestDir\test3.txt",
                    DirName = @"C:\TestDir\",
                    FileName = "test3.xls",
                    Size = 300,
                    CreationTime = DateTime.Parse("03.05.2021 11:15:10"),
                    RegTime = DateTime.Parse("06.04.2022 8:39:22")},
            };
            return result;
        }


    }
}