using System;
using Xunit;
using Moq;
using Playbook;
using Microsoft.Extensions.Options;
using AutoFixture;
using System.Collections.Generic;

namespace Playbook.UnitTests
{
    public class PlaybookService_GetPlaysForPlayer_Tests
    {
        private readonly Fixture _fixture;
        private readonly Mock<IOptions<PlaybookConfig>> _mockPlaybookOptions;

        public PlaybookService_GetPlaysForPlayer_Tests()
        {
            _fixture = new Fixture();
            _mockPlaybookOptions = new Mock<IOptions<PlaybookConfig>>();
        }
        private PlaybookService CreateSystemToTest()
        {
            return new PlaybookService(_mockPlaybookOptions.Object);
        }

        [Fact]
        public void GetPlaysForPlayer_WhenPlayerFound_ReturnsPlayKeys()
        {
            var player = "1";
            var play1Key = "PLAY_1";
            var play2Key = "PLAY_2";
            var playbook = new Dictionary<string, Play>();

            playbook.Add(play1Key, new Play { Players = { player }});
            playbook.Add(play2Key, new Play { Players = { player }});

            var configFixture = _fixture.Build<PlaybookConfig>()
                                        .With(_ => _.Playbook, playbook)
                                        .Create();
            
            _mockPlaybookOptions.Setup(_ => _.Value).Returns(configFixture);

            var sut = CreateSystemToTest();

            var result = sut.GetPlaysForPlayer("1");
            
            Assert.Collection(
                result,
                item => Assert.Equal(play1Key, item),
                item => Assert.Equal(play2Key, item)
            );
        }

        [Fact]
        public void GetPlaysForPlayer_WhenPlayerNotFound_ReturnsEmptyCollection()
        {
            var player = "1";
            var play1Key = "PLAY_1";
            var play2Key = "PLAY_2";
            var playbook = new Dictionary<string, Play>();

            playbook.Add(play1Key, new Play { Players = { player }});
            playbook.Add(play2Key, new Play { Players = { player }});

            var configFixture = _fixture.Build<PlaybookConfig>()
                                        .With(_ => _.Playbook, playbook)
                                        .Create();
            
            _mockPlaybookOptions.Setup(_ => _.Value).Returns(configFixture);

            var sut = CreateSystemToTest();

            var result = sut.GetPlaysForPlayer("2");

            Assert.Empty(result);
        }

    }
}
