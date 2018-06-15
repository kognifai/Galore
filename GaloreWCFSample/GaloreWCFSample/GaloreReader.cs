using System;
using System.Threading.Tasks;
using Kognifai.Galore.Shared.ServiceClients.Core;

namespace Kognifai.Galore.Sample
{
    class GaloreReader
    {
        internal async Task ReadValue(params string[] nodes)
        {
            using (QueryServiceClient queryService = new QueryServiceClient("QueryServiceEndpoint"))
            {
                var results = await queryService.ExecuteAsync(new System.Collections.Generic.List<string>()
                {
                    // Following is a TQL query to fetch data which fetches stale data. For more details please refere to "https://github.com/kognifai/Galore"
                    $"input {Utility.PrepareNodePath(nodes)} acceptstale |> takebefore maxtime 10 |> takeafter maxtime 0"
                });

                foreach (var result in results)
                {
                    foreach (var e in result.Events)
                    {
                        Console.WriteLine(e.ToString());
                    }
                }
            }
        }
    }
}
