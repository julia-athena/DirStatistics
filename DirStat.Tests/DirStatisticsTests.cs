using Xunit;
using System.IO;
using System.Collections.Generic;
using System;
using Moq;
using System.Text;
using DirStat.Dao;
using DirStat.Dao.Impl;

namespace DirStat.Tests
{
    public class DirStatisticsTests
    {

        [Fact]
        public void TestMethod_GetTopBigFiles_CheckCountAndSort()
        {
            // Arrange
            var expected = GetTopBigFiles_GetExpected();
            var daoMock = new Mock<IStatItemDao>();
            daoMock.Setup(d => d.GetByDirNameRec("")).Returns(GetTestData());
            var dirStat = new DirStatistics("", daoMock.Object);
            // Act
            var actual = dirStat.GetTopBigFiles(3);
            // Assert
            Assert.Equal(expected.Count, actual.Count);
            for (int i = 0; i < expected.Count; i++)
            {
                Assert.Equal(expected[i].Size, actual[i].Size);
            }
        }

        [Fact]
        public void GetTopOldFiles()
        {
            // Arrange
            // Act
            // Assert
        }
        [Fact]
        public void GetTopExtensions()
        {
            // Arrange
            // Act
            // Assert
        }
        [Fact]
        public void FreshStatItemsInfoTest()
        {
            // Arrange
            // Act
            // Assert
        }

        private List<StatItem> GetTopBigFiles_GetExpected()
        {
            throw new NotImplementedException();
        }
        private List<StatItem> GetTopOldFiles_GetExpected()
        {
            throw new NotImplementedException();
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
                    FullName = "",
                    DirName = "",
                    FileName = "",
                    Size = 100,
                    CreationTime = DateTime.Parse("01.05.2021 11:15:10"),
                    RegTime = DateTime.Parse("06.04.2022 8:39:22")},
                new StatItem {
                    FullName = "",
                    DirName = "",
                    FileName = "",
                    Size = 100,
                    CreationTime = DateTime.Parse("01.05.2021 11:15:10"),
                    RegTime = DateTime.Parse("06.04.2022 8:39:22")},
                new StatItem {
                    FullName = "",
                    DirName = "",
                    FileName = "",
                    Size = 100,
                    CreationTime = DateTime.Parse("01.05.2021 11:15:10"),
                    RegTime = DateTime.Parse("06.04.2022 8:39:22")},
            };
            return result;
        }


    }
}