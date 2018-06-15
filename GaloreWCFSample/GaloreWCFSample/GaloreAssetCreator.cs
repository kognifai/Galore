using System.Collections.Generic;
using System.Threading.Tasks;
using Kognifai.Galore.Shared.Assets;
using Kognifai.Galore.Shared.DataContracts;
using Kognifai.Galore.Shared.ServiceClients.Core;
using Newtonsoft.Json.Linq;

namespace Kognifai.Galore.Sample
{
    class GaloreAssetCreator
    {
        internal async Task CreateNode(params string[] nodes)
        {
            using (ConfigurationServiceClient _configService = new ConfigurationServiceClient("ConfigurationServiceEndpoint"))
            {
                string currentcontext = "~/";

                for (int index = 0; index < nodes.Length; index++)
                {
                    //CreateNodeCommand is used for creating a node. Other available commands are as follows:
                    //DeleteNodeCommand, MoveNodesCommand, CopyNodesCommand, SetAttributeCommand, DeleteAttributeCommand, RecalculateCommand, 
                    //RestartRealtimeCommand, CreateNodeCommand, UpdateNodeCommand, CreateCalculationCommand, UpdateNameCommand, 
                    //ReloadExternalMappingsCommand, FlushJobQueueCommand
                    CreateNodeCommand command = new CreateNodeCommand();

                    string nodeName = nodes[index];
                    command.Tag = nodeName;
                    command.Attributes = new Dictionary<string, string>
                    {
                        ["displayName"] = nodeName
                    };

                    string nodeType;
                    if (index == nodes.Length - 1)
                    {
                        //Creating timeseries node type as concerned for this sample application.
                        nodeType = NodeType.TimeSeries.ToString(); //Creating Timeseries node type at the below most level(leaf node) where the timeseries data will be pushed.
                        command.Attributes.Add("VectorElementData", GetDescriptor(nodeName)); //Special attributes needed only for Timeseries node.
                        command.Attributes.Add("DataRate", "0"); //Special attributes needed only for Timeseries node.
                    }
                    else
                    {
                        nodeType = NodeType.Unspecified.ToString(); //Creating unspecified node type for rest of the levels as those are not of concern for this sample application.
                    }

                    command.Attributes.Add("nodeType", nodeType);
                    command.ContextSelector = currentcontext;

                    await _configService.ExecuteAsync(new List<Command>() { command }); //This call will create an asset node in Galore.

                    //Preparing context for next level node
                    currentcontext += $"{nodeName}/";
                }
            }
        }

        private static string GetDescriptor(string name)
        {
            return new JArray(
                new JObject
                {
                    ["name"] = name,
                    ["displayName"] = name
                }
            ).ToString();
        }
    }
}
