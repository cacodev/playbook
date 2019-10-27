using System;
using Xunit;
using Moq;
using Playbook;
using Microsoft.Extensions.Options;
using AutoFixture;
using System.Collections.Generic;

namespace Playbook.UnitTests
{
    public class PlaybookService_AllowPlayerToRunPlay_Tests
    {
        private readonly Fixture _fixture;
        private readonly Mock<IOptions<PlaybookConfig>> _mockPlaybookOptions;

        public PlaybookService_AllowPlayerToRunPlay_Tests()
        {
            _fixture = new Fixture();
            _mockPlaybookOptions = new Mock<IOptions<PlaybookConfig>>();
        }
        private PlaybookService CreateSystemToTest()
        {
            return new PlaybookService(_mockPlaybookOptions.Object);
        }

        [Fact]
        public void AllowPlayerToRunPlay_WhenMatched_ReturnsTrue()
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

            var result = sut.AllowPlayerToRunPlay(player, play1Key);

            Assert.True(result);
        }

        [Fact]
        public void AllowPlayerToRunPlay_WhenPlayNotMatched_ReturnsFalse()
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

            var result = sut.AllowPlayerToRunPlay(player, "PLAY_3");

            Assert.False(result);
        }

        [Fact]
        public void AllowPlayerToRunPlay_WhenPlayerNotMatched_ReturnsFalse()
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

            var result = sut.AllowPlayerToRunPlay("2", play1Key);

            Assert.False(result);
        }
    }
}
