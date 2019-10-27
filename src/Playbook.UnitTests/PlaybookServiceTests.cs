using System;
using Xunit;
using Moq;
using Playbook;
using Microsoft.Extensions.Options;
using AutoFixture;
using System.Collections.Generic;

namespace Playbook.UnitTests
{
    public class PlaybookServiceTests
    {
        private readonly Fixture _fixture;
        private readonly Mock<IOptions<PlaybookConfig>> _mockPlaybookOptions;

        public PlaybookServiceTests()
        {
            _fixture = new Fixture();
            _mockPlaybookOptions = new Mock<IOptions<PlaybookConfig>>();
        }
        private PlaybookService CreateSystemToTest()
        {
            return new PlaybookService(_mockPlaybookOptions.Object);
        }

        [Fact]
        public void PlaybookService_WhenCreated_DoesNotThrowException()
        {
            var result = Record.Exception(() => CreateSystemToTest());
        }

        [Fact]
        public void GetPlaybook_WhenCalled_ReturnsPlaybook()
        {
            var configFixture = _fixture.Create<PlaybookConfig>();

            _mockPlaybookOptions.Setup(_ => _.Value).Returns(configFixture);

            var sut = CreateSystemToTest();

            var result = sut.GetPlaybook();

            Assert.IsType<PlaybookConfig>(result);
            Assert.Equal(configFixture, result);
        }

    }
}
