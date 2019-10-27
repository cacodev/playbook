using System.Collections.Generic;

namespace Playbook
{
    public class PlaybookConfig
    {
        public PlaybookConfig()
        {
            this.Playbook = new Dictionary<string, Play>();
        }
        public Dictionary<string, Play> Playbook { get; set; }
    }
}