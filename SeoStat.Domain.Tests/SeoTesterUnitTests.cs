using FluentAssertions;
using Moq;

namespace SeoStat.Domain.Tests;

[TestClass()]
public class SeoTesterUnitTests
{
    [TestMethod()]
    public void SearchTest_ShouldReturnExpectedResultAndSaveMeasurement()
    {
        Mock<ISeoStatRepo> _mockRepo = new Mock<ISeoStatRepo>();
        Mock<ISearchService> _mockSearch = new Mock<ISearchService>();

        var hitList = new List<int> { 1, 2, 3 };
        _mockSearch
            .Setup(x => x.TestHitIndexes(It.IsAny<string>(), It.IsAny<string>()))
            .Returns(hitList);
        _mockRepo
            .Setup(x => x.Insert(It.IsAny<SeoStatMeasurement>()));

        SeoTester sut = new SeoTester(_mockRepo.Object, _mockSearch.Object);

        List<int> result = sut.TestSearch(expectedUrl: "https://www.infotrack.com", searchString: "conveyancing uk digital mobile contract");

        result.Should().BeEquivalentTo(hitList, "SeoTester should return the result it received from the SearchService.");
        _mockRepo.Verify(
            y => y.Insert(It.IsAny<SeoStatMeasurement>()),
            Times.Once,
            "test result should be saved in the repo.");
    }
}