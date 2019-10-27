using System.Collections.Generic;

namespace Playbook
{
    public class Play
    {
        public Play()
        {
            this.Players = new List<string>();
        }
        public List<string> Players { get; set; }
    }
}