# TQL Syntax [![Gitter Join the chat](https://badges.gitter.im/Join%20Chat.svg)](https://gitter.im/kognifai/Lobby)

TQL defines a functional pipeline.

The general form of a query is:
```
Operation1 p1a p1b ... |> Operation2 p2a p2b |> Operation p3a p3b
...
```
p1a, and p1b are parameters.

> Note: 
- Parameters are not generally named. The order of the parameters is important unless explicitly stated.
- Complex queries are formed by grouping sub queries, the formation must be: subqueries in square brackets and ends with semicolons. 
 

```
[
    [
        input WTUR1/W |> where 'v[0] > 5';
        input WTUR2/W |> where 'v[0] > 5'
    ] |> combine ;
    [
        input WTUR1/W |> where 'v[0] < 5';
        input WTUR2/W |> where 'v[0] < 5'
    ] |> merge |> map 'v[0]'
]
|> boolop 'b[0] && b[1]'
|> takebefore 2000-1-1T00:00:00Z 1
|> map 'v[0]+v[1]+v[2]+v[3]'
```

# Table of All TQL Commands

 > Note:
  -  *R* = *Real-time*
  -  *H* = *History*                                                
  
| Command | R,H |Signature | Short Description|
|------|------|----------|----------|                                                                                            
aggregate |   R,H |  DoublesEvent -\> DoublesEvent  |  Aggregates the input sequence into a single value or a value for each period.|
aligneventtime | R,H |                                                               
amplitudespectrum| R,H | SampleSetEvent -\> SampleSetEvent| Creates an amplitude spectrum for each channel in the sample set|
amplitudespectrumfft|   R,H | SampleSetEvent(*2) -\> SampleSetEvent | Creates an amplitude spectrum based on output from the fastfouriertransform-operation for each channel in the sample set. Each channel must have real and imaginary values.|
armaxbuilder | R,H | SampleSetEvent -\> SampleSetEvent| Builds a first order armax model based on the given parameters|
boolop| R,H | [ AlarmEvent ] -\> AlarmEvent| Gets multiple (alarums) inputs, calculates, and generates Boolean result.|
combine|R,H| [ SampleSetEvent ] -\> SampleSetEvent\| Combines hold multiple inputs into a single output vector by aligning input streams on time.|
 |       | | [ DoublesEvent ] -\> DoublesEvent\ | 
 |	| | [ IEvent ] -\> ListEvent|                             
combinewithouthold | R,H | [ SampleSetEvent ] -\> SampleSetEvent\ | Combines without hold multiple inputs into a single output vector by aligning input streams on time.|
| | |[ DoublesEvent ] -\> DoublesEvent |                     
|debounce| R,H | SampleSetEvent -\> SampleSetEvent\|Ignores elements from a sequence which are followed by another value within a computed throttle duration.|
|  |  | DoublesEvent -\> DoublesEvent\  |  
| | | AlarmEvent -\> AlarmEvent\    |
| | | ListEvent -\> ListEvent\      | 
| | | StateChangeEvent -\> StateChangeEvent\ | 
| | | AggregatedLargeEvents -\> AggregatedLargeEvents\|  
| | | TimeRangeEvent -\> TimeRangeEvent\  |  
| | | IEvent -\> IEvent  |   
|decimation| R,H | SampleSetEvent -\> SampleSetEvent| Creates new SampleSet by down sampling input SampleSet channels by a decimation factor (which is calculated from parameter Fmax)|
|dotmap| R,H  | SampleSetEvent -\> SampleSetEvent| Applies an expression to each row of a SampleSet. Each new column in the output SampleSet represents an input expression.|
|downscalex| R,H | ListEvent -\> SampleSetEvent\| Divides the SampleRate (or SampleInterval) of a SampleSet by a factor.|
| | |  *ListEvent must be:\    [ DoublesEvent , SampleSetEvent ] or\   [ SampleSetEvent , DoublesEvent ]*   |
|dump | R,H |  SampleSetEvent -\> Void\ | Writes information about the items in a sequence in the system log. Intended for debugging.|
| | | DoublesEvent -\> Void\ |                                  
| | | ListEvent -\> Void\  |                                    
| | | AlarmEvent -\> Void |                                     
|execute | R,H | IEvent -\> SampleSetEvent | Runs a query and returns the result as a SampleSet |
|fastfouriertransform| R.H  | SampleSetEvent -\> SampleSetEvent(*2) | Calculates FFT which is used in various fft-operations for each channel. The output from this operation is two channels -- one for real and one for imaginary numbers.|
|fftavg | R,H  | SampleSetEvent -\> SampleSetEvent | Creates an average FFT for each channel in the sample set.|
|formatlabel |  H | SampleSetEvent -\> SampleSetEvent\ | Allows user to format the descriptor name for each input |
| | | DoublesEvent -\> DoublesEvent\ |                          
| | | AlarmEvent -\> AlarmEvent\ |                              
| | | ListEvent -\> ListEvent\ |                                
| | | StateChangeEvent -\> StateChangeEvent\ |                  
| | | AggregatedLargeEvents -\> AggregatedLargeEvents\ |        
| | | TimeRangeEvent -\> TimeRangeEvent\ |                      
| | | IEvent -\> IEvent  |                                      
|functionlookup|R,H |      SampleSetEvent -\> SampleSetEvent  |                      Lookup for an expression in a table and applies that expression to the input signal|
|gapfill   |              H   |      DoublesEvent -\> DoublesEvent\  |                         Fills in zero or a given value where query fails to produce value(s) for output sequence|
| | | SampleSetEvent -\> SampleSetEvent |                       
|generate |               R,H |      Void -\> DoublesEvent      |                              Creates a (recurring) time series from a scalar value.|
|hanningwindow |          R,H  |     SampleSetEvent -\> SampleSetEvent  |                      Multiplies the values for each channel in the sample set with a hann window.|
|indexesofmax |        |             DoublesEvent -\> DoublesEvent   |                         Sorts the input array from maximum to minimum and returns the indexes.|
indexesofmin |        |             DoublesEvent -\> DoublesEvent  |                          Sorts the input array from minimum to maximum and returns the indexes.|
indexofmax|           |             DoublesEvent -\> StateChangeEvent |                       Returns the index of the maximum value in a double array.|
indexofmin  |          |            DoublesEvent -\> StateChangeEvent  |                      Returns the index of the minimum value in a double array.|
input |                  R,H |      void -\>IEvent     |  Specifies a starting point for a pipeline. A single input operation can specify one or more event source nodes from the Galore asset model.|
integrate|  R,H|       DoublesEvent -\> DoublesEvent |  An alias of Aggregate operation that use Integrate as type.|
jitter |   R,H |      DoublesEvent -\> DoublesEvent |  Removes clock jitter from input signal.|
linearfit |              R,H |      SampleSetEvent -\> SampleSetEvent | Performs a Linear Least Square Estimate Signal and returns the estimate of the parameters and their covariance.|
lookup |                 R,H  |     SampleSetEvent -\> SampleSetEvent\   |                    Lookup for a value in a lookup table by doing interpolation.|
| | |  ListEvent -\> DoublesEvent\|                              
| | | *ListEvent must be: [SampleSetEvent , DoublesEvent]*  | 
|map                     |R,H    |   DoublesEvent -\> DoublesEvent\    |                       Applies the input C# expression(s) between single quote to the input data|
| | |                                    SampleSetEvent -\> SampleSetEvent |                       
mapv  |                  R,H  |     DoublesEvent -\> DoublesEvent |    A map function that returns an array instead of a value. It takes only one expression. The default metadata is as same as the input.|
 maps  |                  R,H   |    SampleSetEvent -\> SampleSetEvent   |  A map function that returns a SampleSet. Each expression in this map can return an array of array that is map to one or multiple channels in the SampleSet.|
maxenabled  |            R,H   |    [ AlarmEVent ] -\> StateChangeEvent   |                 Returns the index of the last new triggered alarm.|
merge  |                 R,H |      [ SampleSetEvent ] -\> SampleSetEvent\   |              Combines multiple inputs into a single output by interleaving the input events based on their timestamp.|
| | |                                  [ DoublesEvent ] -\> DoublesEvent\   |                  
| | |                                    [ AlarmEvent ] -\> AlarmEvent\   |                      
| | |                                    [ ListEvent ] -\> ListEvent\    |                       
| | |                                    [ StateChangeEvent ] -\> StateChangeEvent\  |           
| | |                                    [ AggregatedLargeEvents ] -\> AggregatedLargeEvents\|   
| | |                                    [ TimeRangeEvent ] -\> TimeRangeEvent\  |               
| | |                                    [ IEvent ] -\> IEvent   |                               
mlptrainer |             H  |       DoublesEvent -\> SampleSetEvent   |                       Trains a multilayer perceptron neural network.|
mma   |                  R,H  |     DoublesEvent -\> DoublesEvent     |                       An alias of Aggregate operation that use minmaxaverage as type.|
multilayerperceptron|    R,H |      DoublesEvent -\> DoublesEvent\    |                       Runs the trained neural network on the current input.|
| | |                                    DoublesEvent, SampleSetEvent -\> DoublesEvent |           
|mux |                    R,H |      [ SampleSetEvent ] -\> SampleSetEvent\  |               Selects its inputs (not including the first one) the output will be based on the state of the first input which must be StateChangeEvent type.|
| | |                                   [ DoublesEvent ] -\> DoublesEvent\|                     
 | | |                                   [ AlarmEvent ] -\> AlarmEvent\ |                        
 | | |                                   [ AggregatedLargeEvents ] -\> AggregatedLargeEvents\ |  
 | | |                                   [ TimeLineEvent ] -\> TimeLineEvent |                   
  normalization |          R,H |      DoublesEvent -\> DoublesEvent\  |                         Normalization of the data by the mean and/or standard deviation.|
 | | |                                   SampleSetEvent -\> SampleSetEvent  |                      
onedit |                 H    |     void -\> TimeRangeEvent    |                              Emits an event when the history data referenced by the input selector changed.|
  onconfig  |              R|         void -\> ConfigurationEvent   |                           Emits an event when the selected node configuration is changed.|
orderspectrum |          R,H  |     SampleSetEvent -\> SampleSetEvent  |                      Creates the order spectrum from a sample set with one channel of samples and one channel of rotational positions.|
  pick |                   R,H   |    SampleSetEvent -\> SampleSetEvent  |                      Creates new sample set by selecting channels from the input sampleset.|
  pow |                    R,H  |     DoublesEvent -\> DoublesEvent\    |                       Raises each element in the input array or matrix (for a sample set) to the exponent.|
 | | |                                   SampleSetEvent -\> SampleSetEvent |                       
powerspectrum  |         R,H |      SampleSetEvent -\> SampleSetEvent |                       Creates the power spectrum (normalized) from each channel in the input SampleSet.|
  powerspectrumfft |       R,H  |     SampleSetEvent(*2) -\> SampleSetEvent  |                 Creates a power spectrum based on an output from the fastfouriertransform-operation for each channel in the sample set. Each channel must have real and imaginary values.|
  recursivelinearfit |     R,H  |     SampleSetEvent, SampleSetEvent -\> SampleSetEvent\ |      Performs a Recursive Linear Least Square Estimate Signal and returns current compute values, current estimate of the parameters and their covariance.|
| | |                                    DoublesEvent, SampleSetEvent -\> SampleSetEvent  |        
 resample   |             R,H   |    SampleSetEvent -\> SampleSetEvent\  |                     Resamples the input sequence with the given sample interval.|
| | |                                   DoublesEvent -\> DoublesEvent\ |                          
| | |                                  AlarmEvent -\> AlarmEvent\   |                            
| | |                                 ListEvent -\> ListEvent\ |                                
| | |                                StateChangeEvent -\> StateChangeEvent\  |                 
| | |                               AggregatedLargeEvents -\> AggregatedLargeEvents\ |        
| | |                              TimeRangeEvent -\> TimeRangeEvent\   |                    
| | |                             IEvent -\> IEvent   |                                     
rootmeansquare |         R,H   |    SampleSetEvent -\> DoublesEvent  |                        Computes the root mean square value for a channel in a SampleSet.|
  selectchannels |         R,H |      SampleSetEvent -\> SampleSetEvent    |                    Creates new SampleSet by selecting channels from the input SampleSet (**Use pick instead)**|
  skip |                   R,H  |     SampleSetEvent -\> SampleSetEvent\  |                     Skips the first number of specified elements.|
| | |                                   DoublesEvent -\> DoublesEvent\|                           
| | |                                    AlarmEvent -\> AlarmEvent\|                               
| | |                                    ListEvent -\> ListEvent\ |                                
| | |                                    StateChangeEvent -\> StateChangeEvent\|                   
| | |                                   AggregatedLargeEvents -\> AggregatedLargeEvents\ |        
| | |                                   TimeRangeEvent -\> TimeRangeEvent\ |                      
| | |                                   IEvent -\> IEvent  |                                      
sliceperiod |            R,H  |     SampleSetEvent -\> SampleSetEvent     |                   Returns a slice sample set based on the number of a given period. The sample set must have been sample in time.|
slicesampleset |         R,H  |     SampleSetEvent -\> DoublesEvent   |                       Returns a slice of a sample set as a sequence of double events.|
  slidingwindow |          R,H  |     DoublesEvent -\> SampleSetEvent |                         Slides a window over a sequence of vectors and emits a matrix with the contents of the window.|
 splitperiod |            H    |     SampleSetEvent -\> SampleSetEvent  |                      Splits a sample set that was sample in time in a sequence of n given period.|
startwithlatest|         R,H   |    SampleSetEvent -\> SampleSetEvent\  |                     Modifies the input operation so that it starts with the last known element. Intented for use in real-time queries only.|
| | |                                   DoublesEvent -\> DoublesEvent\   |                        
| | |                                    AlarmEvent -\> AlarmEvent\     |                          
| | |                                    ListEvent -\> ListEvent\    |                             
| | |                                   StateChangeEvent -\> StateChangeEvent  |                  
sync  |                  R,H  |     SampleSetEvent -\> SampleSetEvent    |                    Intersects all the channels of a sample set sample in time and returns the samples having a common time window.|
take |                   R,H  |     SampleSetEvent -\> SampleSetEvent\ |                      Takes only the specified number of elements.|
| | |                                    DoublesEvent -\> DoublesEvent\  |                         
| | |                                    AlarmEvent -\> AlarmEvent\   |                            
| | |                                    ListEvent -\> ListEvent\  |                               
| | |                                    StateChangeEvent -\> StateChangeEvent\ |                  
| | |                                    AggregatedLargeEvents -\> AggregatedLargeEvents\ |        
| | |                                    TimeRangeEvent -\> TimeRangeEvent\ |                      
| | |                                    IEvent -\> IEvent  |                                      
takeafter |              H   |      SampleSetEvent -\> SampleSetEvent\  |                     Modifies the input operation so that it ends with N items after the given time.|
| | |                                    DoublesEvent -\> DoublesEvent\  |                         
| | |                                    AlarmEvent -\> AlarmEvent\   |                            
| | |                                    ListEvent -\> ListEvent\|                                 
| | |                                    StateChangeEvent -\> StateChangeEvent\|                   
| | |                                    AggregatedLargeEvents -\> AggregatedLargeEvents\|          
| | |                                    TimeRangeEvent -\> TimeRangeEvent\ |                      
| | |                                    TimeLineEvent -\> TimeLineEvent |                         
  takebefore  |            H   |      SampleSetEvent -\> SampleSetEvent\   |                    Starts the sequence with the Nth item before the given time.|
 | | |                                   DoublesEvent -\> DoublesEvent\  |                         
| | |                                    AlarmEvent -\> AlarmEvent\   |                            
| | |                                    ListEvent -\> ListEvent\ |                                
| | |                                    StateChangeEvent -\> StateChangeEvent\ |                  
| | |                                    AggregatedLargeEvents -\> AggregatedLargeEvents\ |        
| | |                                    TimeRangeEvent -\> TimeRangeEvent\|                       
| | |                                    TimeLineEvent -\> TimeLineEvent |                         
  takefrom |               H  |       SampleSetEvent -\> SampleSetEvent\ |                      Specifies the start time of the input range.|
| | |                                    DoublesEvent -\> DoublesEvent\|                           
| | |                                    AlarmEvent -\> AlarmEvent\|                               
| | |                                    ListEvent -\> ListEvent\|                                 
| | |                                    StateChangeEvent -\> StateChangeEvent\|                   
| | |                                    AggregatedLargeEvents -\> AggregatedLargeEvents\|         
| | |                                    TimeRangeEvent -\> TimeRangeEvent\ |                      
| | |                                    TimeLineEvent -\> TimeLineEvent|                          
taketo |                 H |        SampleSetEvent -\> SampleSetEvent\ |     Specifies the end time of the input range.|
| | |                                    DoublesEvent -\> DoublesEvent\ |                          
| | |                                    AlarmEvent -\> AlarmEvent\ |                              
| | |                                    ListEvent -\> ListEvent\ |                                
| | |                                    StateChangeEvent -\> StateChangeEvent\|                   
| | |                                    AggregatedLargeEvents -\> AggregatedLargeEvents\ |        
| | |                                    TimeRangeEvent -\> TimeRangeEvent\ |                      
| | |                                    TimeLineEvent -\> TimeLineEvent|                          
timeline |              R,H |     Void -\> TimeLineEvent|                                  
 topevent |              R,H   |   AlarmEvent -\> ListEvent | Produces the top N active events (alarms) at each point in time as a list.|
tosampleset |           H  |      DoublesEvent -\> SampleSetEvent |  Converts a sequence of double events to a sample set.|
totimerange|            R,H|      SampleSetEvent -\> TimeRangeEvent   |                    Convert a sample set with at least one channel sample in time to a time range event.|
  tounit  |               R,H |     DoublesEvent -\> DoublesEvent\  |                        Defines the output unit of a query operation.|
| | |                                    SampleSetEvent -\> SampleSetEvent  |                     
 transpose |             R,H |     SampleSetEvent -\> SampleSetEvent   |                    Transposes a matrix/SampleSet (all the channels must have the same length).|
unscentedkalmanfilter|  R,H |     DoublesEvent -\> DoublesEvent\ |   Computes the output of the current unscented kalman filter.|
| | |DoublesEvent, DoublesEvent -\> DoublesEvent|             
upscalex |              R,H |     ListEvent -\> SampleSetEvent\  | Multiplies the SampleRate (or SampleInterval) of a SampleSet by a factor.|
| | |              *ListEvent must be:\[ DoublesEvent , SampleSetEvent ] or\ [ SampleSetEvent , DoublesEvent ]*  |                  
where  |                R,H   |   SampleSetEvent -\> SampleSetEvent\  |                    Filter input events based on a predicate.|
| | |                                    DoublesEvent -\> DoublesEvent\ |                         
| | |                                    AlarmEvent -\> AlarmEvent\ |                             
| | |                                    StateChangeEvent -\> StateChangeEvent|                   


## TQL Name Constants

### TQL Name Periods

-   hour
-   day
-   week
-   year
-   quarter
-   decade
-   century

### TQL Name Date

-   now
-   mintime
-   today
-   thishour
-   prevhour
-   nexthour
-   yesterday
-   tomorrow
-   thisweek
-   nextweek
-   prevweek
-   thismonth
-   nextmonth
-   prevmonth
-   thisyear
-   nextyear
-   maxtime

### TQL Named Input Element

-   avg
-   min
-   max
-   count

### TQL Named Value

-   minvalue
-   maxvalue
-   NaN
-   inf
-   -inf
