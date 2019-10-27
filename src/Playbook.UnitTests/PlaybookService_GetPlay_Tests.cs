using System;
using Xunit;
using Moq;
using Playbook;
using Microsoft.Extensions.Options;
using AutoFixture;
using System.Collections.Generic;

namespace Playbook.UnitTests
{
    public class PlaybookService_GetPlay_Tests
    {
        private readonly Fixture _fixture;
        private readonly Mock<IOptions<PlaybookConfig>> _mockPlaybookOptions;

        public PlaybookService_GetPlay_Tests()
        {
            _fixture = new Fixture();
            _mockPlaybookOptions = new Mock<IOptions<PlaybookConfig>>();
        }
        private PlaybookService CreateSystemToTest()
        {
            return new PlaybookService(_mockPlaybookOptions.Object);
        }

        [Fact]
        public void GetPlay_WhenPlayFound_ReturnsPlay()
        {
            var playKey = "PLAY_1";
            var play = new Play { Players = { "1" }};
            var playbook = new Dictionary<string, Play>();

            playbook.Add(playKey, play);
            playbook.Add("PLAY_2", new Play { Players = { "1" }});

            var configFixture = _fixture.Build<PlaybookConfig>()
                                        .With(_ => _.Playbook, playbook)
                                        .Create();
            
            _mockPlaybookOptions.Setup(_ => _.Value).Returns(configFixture);

            var sut = CreateSystemToTest();

            var result = sut.GetPlay(playKey);

            Assert.IsType<Play>(result);
            Assert.Equal(play, result);
        }

        [Fact]
        public void GetPlay_WhenPlayNotFound_ReturnsNull()
        {
            var playbook = new Dictionary<string, Play>();

            playbook.Add("PLAY_1", new Play { Players = { "1" }});
            playbook.Add("PLAY_2", new Play { Players = { "1" }});

            var configFixture = _fixture.Build<PlaybookConfig>()
                                        .With(_ => _.Playbook, playbook)
                                        .Create();
            
            _mockPlaybookOptions.Setup(_ => _.Value).Returns(configFixture);

            var sut = CreateSystemToTest();

            var result = sut.GetPlay("PLAY_3");

            Assert.Null(result);
        }
    }
}
