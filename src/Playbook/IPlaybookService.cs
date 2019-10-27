using System.Collections.Generic;

namespace Playbook
{
    public interface IPlaybookService
    {
        PlaybookConfig GetPlaybook();
        Play GetPlay(string key);
        IEnumerable<string> GetPlaysForPlayer(string player);
        bool AllowPlayerToRunPlay(string player, string play);
    }
}