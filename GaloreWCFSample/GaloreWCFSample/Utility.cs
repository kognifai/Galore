using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Kognifai.Galore.Shared.ServiceClients.Core;

namespace Kognifai.Galore.Sample
{
    class Utility
    {
        internal static async Task<string> ResolvePath(string path)
        {
            var client = new NodeSelectorServiceClient();
            var nodes = await client.GetNodesAsync("~" + path, 0);
            return nodes.First().NodeId;
        }

        internal static string PrepareNodePath(params string[] nodes)
        {
            string nodePath = "~/";
            foreach (string node in nodes)
            {
                nodePath += $"{node}/";
            }

            return nodePath;
        }
    }
}
