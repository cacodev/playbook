using System.Collections.Generic;
using System.Linq;
using Microsoft.Extensions.Options;

namespace Playbook
{
    public class PlaybookService : IPlaybookService
    {
        private readonly PlaybookConfig _playbook;

        public PlaybookService(IOptions<PlaybookConfig> playbookConfig)
        {
            _playbook = playbookConfig.Value;
        }
        public PlaybookConfig GetPlaybook()
        {
            return _playbook;
        }

        public Play GetPlay(string key)
        {
            if (_playbook.Playbook.ContainsKey(key))
            {
                return _playbook.Playbook[key];
            }
            else
            {
                return null;
            }
        }

        public IEnumerable<string> GetPlaysForPlayer(string player)
        {
            return _playbook.Playbook
                            .Where(_ => _.Value.Players.Contains(player))
                            .Select(_ => _.Key);
        }

        public bool AllowPlayerToRunPlay(string player, string play)
        {
            var currentPlay = GetPlay(play);

            if (currentPlay == null)
            {
                return false;
            }
            else
            {
                return currentPlay.Players.Contains(player);
            }
        }
    }
}