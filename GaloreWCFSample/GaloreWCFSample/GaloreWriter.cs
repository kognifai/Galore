using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Kognifai.Galore.Shared.DataContracts;
using Kognifai.Galore.Shared.ServiceClients.Core;

namespace Kognifai.Galore.Sample
{
    class GaloreWriter
    {
        internal async Task WriteValue(string path, double value)
        {
            using (var client = new DataServiceClient("DataServiceEndpoint"))
            {
                var events = new List<EventDC>()
                {
                    //Following are other event types available:
                    //SampleSetEventDC, VectorEventDC, UInt32EventDC, AlarmEventDC, StateEventDC, ListEventDC, DescriptionEventDC, UserAckEventDC, 
                    //AggregatedEventsDC, AggregatedStatesEventDC, AggregatedLargeEventsDC, TimelineEventDC, StringEventDC, ConfigurationEventDC, 
                    //MatrixEventDC, MatricesEventDC, TimeRangeEventDC
                    new VectorEventDC(DateTime.UtcNow, value) //VectorEventDC is a representation of Timeseries as we are concerned for this sample application
                };

                var nodeId = await Utility.ResolvePath(path);

                await client.UpsertAsync("admin",
                    new List<LocatedEventsDC>() { new LocatedEventsDC() { NodeId = nodeId, Events = events } }); //This call will write data into the node we created in asset creation step.
            }
        }
    }
}
