
# Galore Asset Model

The Galore asset model is a Directed Acyclic Graph (DAG), which is to say that each
node (except the root node) has a one or more parents and zero or more
children. Each node in the DAG has a tag (except the root node). The
tag is a string that identifies the node in the context of its parent
node. The tag must not be confused with the nodes display node. Each
node has a path that uniquely identifies it. The path describes how to
traverse the DAG from the root to reach the node. The path is
effectively a list of the tags of each node visited during this
traversal separated by the slash character `/`.

Example: `/Farm/Turbine/Nacelle/WindSpeed`

# Streams of Events

The fundamental data structure of Galore is a stream of events Most streams in Galore persists history (but not all). An event in Galore is a time stamped piece of data. [More on events here](#event-types). In Galore, streams are ordered on timestamp.

A stream in Galore is similar to a list or table but with some important differences:

-   An event in a sequence does not have an index. It is not possible to
    refer to an event by its absolute position in the stream.

-   A stream can extend into the future and as such be of
    indeterminate size.

Other important properties of Galore streams:

-   A stream in Galore is chronologically ordered i.e. order by the
    event timestamp.

Examples of streams in Galore:

-   Measurement time series is a sequence of time stamped values or
    vectors. A value is a vector with a single element.

-   Alarm log is a sequence of alarms on a system or subsystem

-   "Active alarms" is a sequence of lists where each list contains the
    active alarms at that time. Whenever an alarms active state changes,
    the change is reflected in the by active alarms sequence. All
    systems and sub systems have an active alarms sequence.

-   Sample set logs are Streams of more complex data structures such
    as weather forecasts, production forecasts, power curves, amplitude
    spectrum and much more.

# Event types 

The following event types are supported by TQL

-   Vector. The event has a vector of floating point values with one or
    more elements. Stored Streams of vector events have an associated
    time series node in the Galore asset model. The node
    contains "element descriptors" describing the vector elements
    properties such as storage unit and display unit. The elements
    descriptors are also referred to as "meta data" for the vector or
    time series.

-   Sample set. The event is a matrix or table. Each column in the
    matrix is a "channel" e.g. for a weather forecast one channel can be
    wind speed and another temperature. Stored Streams of sample set
    events have an associated sample set node in the Galore
    asset model. The node contains "channel descriptors"
    describing the channel properties such as storage unit display unit,
    sample rate and sample dimension. The channel descriptors are also
    referred to as "meta data" for the sample set sequence.

-   Alarm. An alarm event has the following properties:

    -   Level: (Info/light blue, Warning/yellow, Alarm/red,
        Emergency/magenta)

    -   Active: (true, false/green)

    -   Acknowledged (true or false). User or system(auto ack.) has
        acknowledge the alarm

    -   Passed (true or false). Triggering condition has passed but
        alarm is not acknowledged

    -   message

Most TQL operations work on a subset of types. Some operations have
different parameters for different types of input Streams. Boolean
operations works on alarm Streams active property.

# TQL Syntax

TQL defines a functional pipeline.

The general form of a query is
```
Operation1 p1a p1b ... |> Operation2 p2a p2b |> Operation p3a p3b
...
```
p1a, p1b etc are parameters

Note that parameters are not usually named. The order of the parameters is 
important unless explicitly stated.

More complex queries can be formed by grouping subqueries using square
brackets and semicolon:

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

 > Legend:
  -  *R* = *Real time*
  -  *H* = *History*                                                
-
| Command | R,H |Signature | Short Description|
|------|------|----------|----------|                                                                                            
aggregate |   R,H |  DoublesEvent -\> DoublesEvent  |  Aggregates the input sequence into a single value or a value for each period|
aligneventtime | R,H |                                                               
amplitudespectrum| R,H | SampleSetEvent -\> SampleSetEvent| Creates an amplitude spectrum for each channel in the sample set|
amplitudespectrumfft|   R,H | SampleSetEvent(*2) -\> SampleSetEvent | Creates an amplitude spectrum based on output from the fastfouriertransform-operation for each channel in the sample set. Each channel must have real and imaginary values.|
armaxbuilder | R,H | SampleSetEvent -\> SampleSetEvent| Build a first order armax model based on the given parameters|
boolop| R,H | [ AlarmEvent ] -\> AlarmEvent| Takes multiple (alarm) inputs and calculates a Boolean result.|
combine|R,H| [ SampleSetEvent ] -\> SampleSetEvent\| Combines with hold multiple inputs into a single output vector by trying to align the input Streams on time.|
 |       | | [ DoublesEvent ] -\> DoublesEvent\ | 
 |	| | [ IEvent ] -\> ListEvent|                             
combinewithouthold | R,H | [ SampleSetEvent ] -\> SampleSetEvent\ | Combines without hold multiple inputs into a single output vector by trying to align the input Streams on time.|
| | |[ DoublesEvent ] -\> DoublesEvent |                     
|debounce| R,H | SampleSetEvent -\> SampleSetEvent\|Ignores elements from a sequence which are followed by another value within a computed throttle duration.|
|  |  | DoublesEvent -\> DoublesEvent\  |  
| | | AlarmEvent -\> AlarmEvent\    |
| | | ListEvent -\> ListEvent\      | 
| | | StateChangeEvent -\> StateChangeEvent\ | 
| | | AggregatedLargeEvents -\> AggregatedLargeEvents\|  
| | | TimeRangeEvent -\> TimeRangeEvent\  |  
| | | IEvent -\> IEvent  |   
|decimation| R,H | SampleSetEvent -\> SampleSetEvent| Creates new sampleset by downsampling input sampleset channels by a decimation factor (which is calculated from parameter Fmax)|
|dotmap| R,H  | SampleSetEvent -\> SampleSetEvent| Apply an expression to each row of a SampleSet. Each new column in the output SampleSet represent an input expression.|
|downscalex| R,H | ListEvent -\> SampleSetEvent\| Divide the SampleRate (or SampleInterval) of a SampleSet by a factor.|
| | |  *ListEvent must be:\    [ DoublesEvent , SampleSetEvent ] or\   [ SampleSetEvent , DoublesEvent ]*   |
|dump | R,H |  SampleSetEvent -\> Void\ | Writes information about the items in the sequence to the system log. Intended for debugging.|
| | | DoublesEvent -\> Void\ |                                  
| | | ListEvent -\> Void\  |                                    
| | | AlarmEvent -\> Void |                                     
|execute | R,H | IEvent -\> SampleSetEvent | Run a query and return it's result as a SampleSet |
|fastfouriertransform| R.H  | SampleSetEvent -\> SampleSetEvent(*2) | Calculates the FFT which is to be used in various fft-operations for each channel. The output from this operation is two channels -- one for real and one for imaginary numbers.|
|fftavg | R,H  | SampleSetEvent -\> SampleSetEvent | Creates an averaged FFT for each channel in the sample set.|
|formatlabel |  H | SampleSetEvent -\> SampleSetEvent\ | Allow the user to format the descriptor name for each input |
| | | DoublesEvent -\> DoublesEvent\ |                          
| | | AlarmEvent -\> AlarmEvent\ |                              
| | | ListEvent -\> ListEvent\ |                                
| | | StateChangeEvent -\> StateChangeEvent\ |                  
| | | AggregatedLargeEvents -\> AggregatedLargeEvents\ |        
| | | TimeRangeEvent -\> TimeRangeEvent\ |                      
| | | IEvent -\> IEvent  |                                      
|functionlookup|R,H |      SampleSetEvent -\> SampleSetEvent  |                      Lookup for an expression in a table and apply that expression to the input signal|
|gapfill   |              H   |      DoublesEvent -\> DoublesEvent\  |                         Fills in zero or a given values where query fails to produce values for output sequence|
| | | SampleSetEvent -\> SampleSetEvent |                       
|generate |               R,H |      Void -\> DoublesEvent      |                              creates a (recurring) time series from a scalar value.|
|hanningwindow |          R,H  |     SampleSetEvent -\> SampleSetEvent  |                      Multiplies the values for each channel in the sample set with a hanning window.|
|indexesofmax |        |             DoublesEvent -\> DoublesEvent   |                         Sort the input array from maximum to minimum and return the indexes.|
indexesofmin |        |             DoublesEvent -\> DoublesEvent  |                          Sort the input array from minimum to maximum and return the indexes.|
indexofmax|           |             DoublesEvent -\> StateChangeEvent |                       Return the index of the maximum value in a double array.|
indexofmin  |          |            DoublesEvent -\> StateChangeEvent  |                      Return the index of the minimum value in a double array.|
input |                  R,H |      void -\>IEvent     |  specify a starting point for a pipeline. A single input operation can specify one or more event source nodes from the Galore asset model.|
integrate|  R,H|       DoublesEvent -\> DoublesEvent |  An alias of Aggregate operation that use Integrate as type.|
jitter |   R,H |      DoublesEvent -\> DoublesEvent |                           remove clock jitter from input signal.|
linearfit |              R,H |      SampleSetEvent -\> SampleSetEvent | Perform a Linear Least Square Estimate Signal and return the estimate of the parameters and their covariance.|
lookup |                 R,H  |     SampleSetEvent -\> SampleSetEvent\   |                    Lookup for a value in a lookup table by doing interpolation.|
| | |  ListEvent -\> DoublesEvent\|                              
| | | *ListEvent must be: [SampleSetEvent , DoublesEvent]*  | 
|map                     |R,H    |   DoublesEvent -\> DoublesEvent\    |                       Apply the input C# expression(s) between signle quote to the input data|
| | |                                    SampleSetEvent -\> SampleSetEvent |                       
mapv  |                  R,H  |     DoublesEvent -\> DoublesEvent |                           a map function that return an array instead of a value. It takes only one expression. The default metadata is the same as the input.|
 maps  |                  R,H   |    SampleSetEvent -\> SampleSetEvent   |                     a map function that return a sampleset. Each expression in this map can return an araay of array that is map to one or multiple channels in the sampleset.|
maxenabled  |            R,H   |    [ AlarmEVent ] -\> StateChangeEvent   |                 Return the index of the last new triggered alarm.|
merge  |                 R,H |      [ SampleSetEvent ] -\> SampleSetEvent\   |              Combines multiple inputs into a single output by interleaving the input events based on their timestamp.|
| | |                                  [ DoublesEvent ] -\> DoublesEvent\   |                  
| | |                                    [ AlarmEvent ] -\> AlarmEvent\   |                      
| | |                                    [ ListEvent ] -\> ListEvent\    |                       
| | |                                    [ StateChangeEvent ] -\> StateChangeEvent\  |           
| | |                                    [ AggregatedLargeEvents ] -\> AggregatedLargeEvents\|   
| | |                                    [ TimeRangeEvent ] -\> TimeRangeEvent\  |               
| | |                                    [ IEvent ] -\> IEvent   |                               
mlptrainer |             H  |       DoublesEvent -\> SampleSetEvent   |                       Train a multilayer perceptron neural network.|
mma   |                  R,H  |     DoublesEvent -\> DoublesEvent     |                       An alias of Aggregate operation that use minmaxaverage as type.|
multilayerperceptron|    R,H |      DoublesEvent -\> DoublesEvent\    |                       Run the trained neural network on the current input.|
| | |                                    DoublesEvent, SampleSetEvent -\> DoublesEvent |           
|mux |                    R,H |      [ SampleSetEvent ] -\> SampleSetEvent\  |               Select whihc of its inputs (not including the first one) will be output base on the state of the first input which must be of StateChangeEvent type.|
| | |                                   [ DoublesEvent ] -\> DoublesEvent\|                     
 | | |                                   [ AlarmEvent ] -\> AlarmEvent\ |                        
 | | |                                   [ AggregatedLargeEvents ] -\> AggregatedLargeEvents\ |  
 | | |                                   [ TimeLineEvent ] -\> TimeLineEvent |                   
  normalization |          R,H |      DoublesEvent -\> DoublesEvent\  |                         Nomalization of the data by the mean and/or standard deviation.|
 | | |                                   SampleSetEvent -\> SampleSetEvent  |                      
onedit |                 H    |     void -\> TimeRangeEvent    |                              Emits an event when the history data referenced by the input selector changed.|
  onconfig  |              R|         void -\> ConfigurationEvent   |                           Emits an event when the selected node configuration change.|
orderspectrum |          R,H  |     SampleSetEvent -\> SampleSetEvent  |                      Creates the order spectrum from a sampleset with one channel of samples and one channel of rotational positions.|
  pick |                   R,H   |    SampleSetEvent -\> SampleSetEvent  |                      Create new sampleset by selecting channels from the input sampleset.|
  pow |                    R,H  |     DoublesEvent -\> DoublesEvent\    |                       Raise the each element in the input array or matrix (for a sampleset) to the exponent.|
 | | |                                   SampleSetEvent -\> SampleSetEvent |                       
powerspectrum  |         R,H |      SampleSetEvent -\> SampleSetEvent |                       Creates the power spectrum (normalized) from each channel in the input sampleset.|
  powerspectrumfft |       R,H  |     SampleSetEvent(*2) -\> SampleSetEvent  |                 Creates a power spectrum based on output from the fastfouriertransform-operation for each channel in the sample set. Each channel must have real and imaginary values.|
  recursivelinearfit |     R,H  |     SampleSetEvent, SampleSetEvent -\> SampleSetEvent\ |      Perform a Recursive Linear Least Square Estimate Signal and return current compute values, current estimate of the parameters and their covariance.|
| | |                                    DoublesEvent, SampleSetEvent -\> SampleSetEvent  |        
 resample   |             R,H   |    SampleSetEvent -\> SampleSetEvent\  |                     Resamples the input sequence with the given sample interval.|
| | |                                   DoublesEvent -\> DoublesEvent\ |                          
| | |                                  AlarmEvent -\> AlarmEvent\   |                            
| | |                                 ListEvent -\> ListEvent\ |                                
| | |                                StateChangeEvent -\> StateChangeEvent\  |                 
| | |                               AggregatedLargeEvents -\> AggregatedLargeEvents\ |        
| | |                              TimeRangeEvent -\> TimeRangeEvent\   |                    
| | |                             IEvent -\> IEvent   |                                     
rootmeansquare |         R,H   |    SampleSetEvent -\> DoublesEvent  |                        Compute the root mean square value for a channel in a sampleset.|
  selectchannels |         R,H |      SampleSetEvent -\> SampleSetEvent    |                    Create new sampleset by selecting channels from the input sampleset (**Use pick instead)**|
  skip |                   R,H  |     SampleSetEvent -\> SampleSetEvent\  |                     Skip the first number of specified elements.|
| | |                                   DoublesEvent -\> DoublesEvent\|                           
| | |                                    AlarmEvent -\> AlarmEvent\|                               
| | |                                    ListEvent -\> ListEvent\ |                                
| | |                                    StateChangeEvent -\> StateChangeEvent\|                   
| | |                                   AggregatedLargeEvents -\> AggregatedLargeEvents\ |        
| | |                                   TimeRangeEvent -\> TimeRangeEvent\ |                      
| | |                                   IEvent -\> IEvent  |                                      
sliceperiod |            R,H  |     SampleSetEvent -\> SampleSetEvent     |                   return a slice the sampleset based on a number of a given period. The sampleset must have been sample in time.|
slicesampleset |         R,H  |     SampleSetEvent -\> DoublesEvent   |                       Return a slice of a sampleset as a sequence of double events.|
  slidingwindow |          R,H  |     DoublesEvent -\> SampleSetEvent |                         Slides a window over a sequence of vectors and emits a matrix with the contents of the window.|
 splitperiod |            H    |     SampleSetEvent -\> SampleSetEvent  |                      Split a sampleset that was sample in time in a sequence of n given period.|
startwithlatest|         R,H   |    SampleSetEvent -\> SampleSetEvent\  |                     Modifies the input operation so that it starts with the last known element. Intented for use in realtime queries only.|
| | |                                   DoublesEvent -\> DoublesEvent\   |                        
| | |                                    AlarmEvent -\> AlarmEvent\     |                          
| | |                                    ListEvent -\> ListEvent\    |                             
| | |                                   StateChangeEvent -\> StateChangeEvent  |                  
sync  |                  R,H  |     SampleSetEvent -\> SampleSetEvent    |                    Intersect all the channels of a sampleset sample in time. Wil return the samples having a common time window.|
take |                   R,H  |     SampleSetEvent -\> SampleSetEvent\ |                      Take only the specified number of elements.|
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
tosampleset |           H  |      DoublesEvent -\> SampleSetEvent |  convert a squence of double events to a sampleset.|
totimerange|            R,H|      SampleSetEvent -\> TimeRangeEvent   |                    Convert a sampleset with at least one channel sample in time to a time range event.|
  tounit  |               R,H |     DoublesEvent -\> DoublesEvent\  |                        Define the output unit of a query operation.|
| | |                                    SampleSetEvent -\> SampleSetEvent  |                     
 transpose |             R,H |     SampleSetEvent -\> SampleSetEvent   |                    Transpose a matrix/sampleset (all the channels must have the same length).|
unscentedkalmanfilter|  R,H |     DoublesEvent -\> DoublesEvent\ |   Compute the output of the current unscented kalman filter.|
| | |DoublesEvent, DoublesEvent -\> DoublesEvent|             
upscalex |              R,H |     ListEvent -\> SampleSetEvent\  | Multiply the SampleRate (or SampleInterval) of a SampleSet by a factor.|
| | |              *ListEvent must be:\[ DoublesEvent , SampleSetEvent ] or\ [ SampleSetEvent , DoublesEvent ]*  |                  
where  |                R,H   |   SampleSetEvent -\> SampleSetEvent\  |                    Filter input events based on a predicate.|
| | |                                    DoublesEvent -\> DoublesEvent\ |                         
| | |                                    AlarmEvent -\> AlarmEvent\ |                             
| | |                                    StateChangeEvent -\> StateChangeEvent|                   


# TQL Name Constants

## TQL Name Periods

-   hour
-   day
-   week
-   year
-   quarter
-   decade
-   century

## TQL Name Date

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

## TQL Named Input Element

-   avg
-   min
-   max
-   count

## TQL Named Value

-   minvalue
-   maxvalue
-   NaN
-   inf
-   -inf

Node Selector
=============

The node selector is an expression for selecting one or more nodes from
the Galore. A node can be selected in several ways:

-   Path
-   Id
-   Attribute name
-   Attribute value
-   Template reference (planned obsolete)

Node selectors can also be "chained" to select nodes in multiple steps,
as described below. Nodes selected by the previous part of a chain
becomes the context in which the current part of a chain is applied.
This makes it possible to use relative paths.

path
----

A path selector is a path prefixed by a tilde `~` character. Paths are
similar to file system paths. Wildcards "*" will match any character in
a node tag. "." matches the current context node. ".." matches the
parent node. The context node depends on the execution context of the
selector, normally this is the root node or the previous part of a
chained selector. If the selector is part of a TQL query configured on a
calculator or a monitor node, that node is the context node. Examples:

- `/` Select the root node
- `/Farm/Turbine1`: Select the "Turbine1" node under the "Farm" node
-   `/Farm/Turbine*` Selects all nodes with tag starting with
    "Turbine" under the "Farm" node
-   `../Turbine1` Selects the sibling "Turbine1" of the current context node

id
--

An id selector is a numeric id prefixed by the hash/pound character
"#". The id selector is normally used in dynamic commands/or queries
where the id has been resolved and not in configured/static queries.
Selectors with ids cannot be reused in other instances of Galore
because the "same" node will have a different id.

attribute name
--------------

Attribute selectors appear in between square brackets "[" and "]"

Example: 
```
[maxPower]
```
Select all nodes with the attribute "maxPower"

attribute value
---------------

Attribute selectors appear in between square brackets "[" and "]"

Example: 
```
[maxPower=2300]
```
Select all nodes with the attribute "maxPower" of value 2300

Chained selectors
-----------------

Multiple selectors can be chained to select nodes in a context. Chained
selectors are separated with a space character.

Examples:
```
/Farm1 [maxPower=2300]
```
Select all nodes under "Farm1" with the attribute "maxPower" of value 2300

And selectors
-------------

Multiple criteria can be specified for a node to match for selection.
Multiple criteria are not separated with any characters.

Example:
```
/Farm1 [nodeType=Turbine][maxPower=2300]
```
Select all nodes under "Farm1" with the attribute "nodeType" of value "Turbine" AND the attribute "maxPower" of value 2300.

Or Selectors
------------

Multiple criteria can be specified for nodes to be included in a
selection. Multiple criteria are separated with comma "," characters.

Example:
```
/Farm1 [nodeType=Turbine], /Farm1 [maxPower=2300]`
```
Select all nodes under "Farm1" with the attribute "nodeType" of value "Turbine" OR the attribute "maxPower" of value 2300.

Note: There is presently no way to group parts of the selector, e.g. to
write the above example as

Not allowed: `/Farm1 ([nodeType=Turbine], [maxPower=2300])`

Parameters
==========

Some parameters have special syntax and are used in many operations.

Interval
--------

Interval can be a floating point number with a time unit postfix "s",
"m", "h", "d", "w", "M", "y". Examples:

-   `25s`: 25 seconds

-   `2m`: 2 minutes

-   `1.5h`: 1 hour 30 minutes

The interval can also be written as mm:ss where mm is the number of
minutes and ss is the number of seconds.

Period
------

A period is similar to an interval and can have all the same values. In
addition a period can have the values "week", "month", "year",
"quarter", "decade" and "century".

The main difference between a period and an interval is that periods are
aligned to the local time. An interval of 24h is just an amount of time.
A period of 24h is aligned to the local time. A period parameter is
usually used to "periodize" a sequence into a new sequence of events
grouped by the period they occurred in.

Expression
----------

Some operations take one or more expressions as their parameter.

Expression are have the same syntax as C# expressions and are between
single quotes: `'expression'`

The variable "v" refers to the input vector of the expression if the
input is a time series type. v[0] refers to the first element of the
vector, v[1] to the second and so forth. For alarm input the variable
b is used. m

Expressions are optionally followed by a result unit identifier. The
system is not yet capable of inferring the result unit from the
expression. If the unit is omitted, the result is assumed to have the
same unit as the first input.

Examples:

-   'v[0] + v[1]'

-   'v[0] * v[1]' Power:SI:MW

-   'v[0] > 5' (bool output)

-   'v.Sum()'

-   'sin(v[0])'

-   'b[0] && b[1]' (bool input and output)

Time
----

Format is close to ISO8601

Examples:

-   `2015-03-15`: 15<sup>th</sup> of mars 2015

-   `2015-03-01T01:13`: 13 past one, 1<sup>st</sup> of mars 2015
-   `2015-03-15T12:47:33.12345Z 12:47:33`: 15<sup>th</sup> of mars 2015

Note the "T" separating date and time

Note the Z designating the time zone "Zulu" = UTC/GMT

Pipeline Operations
===================

input
-----

The input operation is used to specify a starting point for a pipeline.
A single input operation can specify one or more event source nodes from
the Galore asset model. If multiple nodes are selected, parallel
pipelines are created and each consecutive single input operation is
applied to each pipeline. The first multi input operation will merge the
pipelines.

The input operation has the form:
```
input <nodeselector> [stream selector] [acceptStale]
```
The stream selector has the form
```
-[related-|count-]<streamtype> [[>=|<=]pre-aggregate interval]
```
Streamtype can be prefixed with:
-   related. Selects events that have propagated to the selected node
    from different source nodes e.g. from subsystem or as causes of
    other events
-   count. For large event types such as samplesets and matrixes count
    can be used to indicate where they exist in time e.g. for navigation
    without loading the whole event. This also combines with the
    preaggregate interval parameter to get number of events in a period.

The optional `>=` and `<=` operators are used to select a preaggregate without
knowing the exact level This is used e.g. when calculating a suitable
resolution for plotting or event aggregates and allows the client
application to not know the exact levels available.

Stream types can be customized but these are predefined by the system:

-   timeline: Obsolete but kept for use by Galore timelines. Should
    move to custom types.
-   vec: timestamp + vector, also known as timeseries
-   ss: sampleset
-   matrices: lists of matrix
-   matrix: matrix
-   alarm: galore built in alarm type
-   sc: generic state change: use custom type in stead
-   streamedit: real time only information about changes in history for a
    stream. real time only.
-   configedit: notification about asset model changes
-   calculatorstate: the state of a calculator. Real time only.
-   userack: for clients to acknowledge alarms with this options set in
    the calculator or monitor

Parameters:

-   nodeSelector. See chapter 5 Node Selector. Selects one or more nodes
    in the Galore asset model. If the node is not an event
    source node, all descendants that are alarm log nodes is
-   streamSelector. See X. Stream selector. Nodes may have multiple
    streams associated e.g. propagated or related events, preaggregates,
    change notifications etc. In many cases the stream selector can be
    left out because it is inferred from the node type.
-   interval. Is a part of the stream selector. Determines which
    pre-aggregate interval is selected. The closest larger preaggregate
    interval will be selected. Default is 0 i.e. not aggregated data.
    See 6.1 Interval.
-   acceptStale. Forces the underlying index to update before reading
    alarms. Default is false.

    Examples:
```
    Input #5 -related-alarms 10s: selects the alarms propagated to node
    10 as 10 second aggregates

    Input ~/Ships/myship/Speed -vec: selects the vector/timeseries
    stream on node speed under ships/myship

    Input #15 -count-ss: selects the time of samplesets on sampleset
    stream 15
```
tounit
------

Define the output unit of a query operation. This should be use whenever
the system could not find the unit by itself or to override the one
found by the system.
Example:

```
input ../W, ../WNAC/WdSpd 
    |> combine
    |> tounit Power:SI:W Speed:SI:m/s
```
This example set the unit for each input that is in the output of the
combine operation.

jitter
------

The jitter operation is used to remove jitter from timestamps. This is
particularly useful before the merge operation to align timestamps in
multiple inputs.

Parameters:

-   interval. See 6.1 Interval.

resample
--------

Resamples the input sequence with the given sample interval. At present,
the input value right before the resample time will be used with no
interpolation.

Parameters:

-   interval. Interval between each sample. See 6.1 Interval.

-   type. Upsample or Downsample. Needed for correct handling of gaps in
    the input data. Upsample will fill gaps with the same value.
    Downsample will not.

generate
--------

Generate creates a (recurring) time series from a scalar value. The
interval between entries, the number of values for each entry and the
number of entries can be controlled.

Usage:
```
generate time period [value] [vectorSize] [realtime [count]]
```
Parameters:

-   time. Starting time for series

-   period. Interval between entries. See 6.2 Period

-   value. The value of each entry. Defaults to 0 if missing

-   vectorSize. Number of values produced per entry

-   real time. States if the time series should be recurring. If set the
    time parameter is ignored. Time series will start on current time

-   count. Number of repeating entries. Only relevant if real time

Examples:
```
generate now 1s 3.1415
generate now 2s 3.1415 5
generate now 5s 3.1415 2 realtime 10
```
aggregate
---------

Aggregates the input sequence into a single value or a value for each
period

Parameters:

-   period. If given the aggregate produces an aggregate for each
    period. See 6.2 Period

-   isRunning. A partial aggregate is produced for every input value

-   type

    -   MinMaxAvg, The output is a vector with avg, min and max

    -   Statistics, The output is a vector with avg, min, max, count,
        stddev and median

    -   Integrate, The output is the integral with respect to time

    -   IntegrateInterpolate, The output is the integral with respect to
        time

    -   AvgUnitVector, The output is the average of the input treated as
        an angle

-   output time unit. Only used with integrate. E.q. if the input is "W" (Watt)
    the default output unit is "Ws" (Watt seconds). Using an output unit of "h" will give
    "Wh" (Watt hours) in stead

combine
-------

Combines multiple inputs into a single output vector by trying to align
the input Streams on time. If some inputs do not have a value at a
particular time, the previous value is used. Most useful for time series
inputs.

Example:
```
input ~/Simulator/WTUR* [defaultTurbinePower] |> combine 10s
```

Parameters:

-   optional debounce interval (real time only, default 0). If one or
    more of the input is delayed, emit a new combined event by reusing
    the previous value of the delayed inputs. I.e resample with hold. If
    omitted or 0, the combine operation will emit for every input
    change.

>Note: The force delay should be more than the expected interval of the
slowest input. I.e. the forcing should only be applied when an input is
actually and significantly delayed as opposed to just having a bit of
timing jitter. It should not be used to force a fixed output rate.

>Tip: If the input events are not clustered in time, it makes more sense
to not use the debounce interval option. Instead, follow the operation
by resample to sample the inputs at specific times.

>Note: The real time and historical version of this operation produces
slightly different results since the historical will emit o n every
distinct timestamp.

combinewithouthold
------------------

It's the same as combine but it doesn't use the last value if there is
no new value in the given interval

It has 2 parameters:

-   The interval to wait for the inputs to get a value

-   What to fill for the inputs which have no new value (fillWithNaN,
    fillWithZero, fillWithDefault, fillWithValue)

Examples:
```
combinewithouthold 10m fillWithNaN

combinewithouthold 10m fillWithValue 5
```
merge
-----

Combines multiple inputs into a single output by interleaving the input
events based on their timestamp. Most useful for alarm/event inputs.

Example:
```
[
    input ~/Simulator/WTUR01 [defaultTurbinePower];
    input ~/Simulator/WTUR01 [defaultTurbineWindSpeed]
] 
|> merge
```

debounce
--------

Ignores elements from a sequence which are followed by another value
within a computed throttle duration.

Parameters: Time interval to wait new data

Example:
```
[
    input ~/Simulator/WTUR02 [defaultTurbinePower] 5s element:avg;
    input ~/Simulator/WTUR02 [defaultTurbineWindSpeed] 5s element:avg;
    input ~/Simulator/WTUR03 [defaultTurbinePower] 5s element:avg;
    input ~/Simulator/WTUR03 [defaultTurbineWindSpeed] 5s element:avg;
    input ~/Simulator/WTUR04 [defaultTurbinePower] 5s element:avg;
    input ~/Simulator/WTUR04 [defaultTurbineWindSpeed] 5s element:avg;
    input ~/Simulator/WTUR05 [defaultTurbinePower] 5s element:avg;
    input ~/Simulator/WTUR05 [defaultTurbineWindSpeed] 5s element:avg
]
|> merge
|> debounce 1s
```

dotmap
------

apply an expression to each row of a sampleset and return a sampleset
with n channels, where n is the number of expression.

Example:
```
Input ~/path/to/samplerset |> dotmap 'v[0]*v[1]'
```

This example will return a new sampleset with one channel each element
of the first row of the input channel is multiply by the elements of the
second input channel

>Note:

-   To use expression on multiple inputs use combine to combine the
    inputs into a vector.

-   Available variables:

    -   `v` -- vector of values from each row. Accessible by index. Ex:
        `v[0]`

    -   `prev` -- previous output value

    -   `dt` -- time difference between current event and previous one in
        milliseconds.

    -   `sampleRate` -- channel sampleRate (in dot map all the channels
        must have the same sampleRate)

    -   `rowIndex` -- index of the current row being processed

    -   `totalRows` -- total number of rows in the input sampleset (all
        channels have the same amount of rows)

    -   `c` -- vector of constant values bound from attributes in nodes.
        Accessible by key. Ex: `c["MP"]`

    -   `cv` -- vector constant of vectors values bound from attributes in
        nodes. Accessible by key and index. Ex: `cv["v1"][0]`

    -   `cm` -- matrix (array of array) constant of matrix values bound
        from attributes in nodes. Accessible by key and index. Ex:
        `cm["m1"]`

topevent
--------

Produces the top N active events (alarms) at each point in time as a
list.

Parameters:

Max Number of events in the list

activeevent
-----------

Produces the active events (alarms) at each point in time as a list.

activeeventcount
----------------

Produces a vector of 4 counts; one for each alarm level.

1.  Info

2.  Warning

3.  Alarm

4.  Emergency

where
-----

Parameters:

-   expression. A boolean expression (also known as a predicate) to
    determine which items of the sequence to discard. See 6.3 Expression

Example: 
```
discard 'v[0] \> 1'. 
```
Discards all items where the first
vector element is larger than 1

wheretext
---------

Parameters:

-   string. A search text (that may contain wildcards) that is matched
    against textual properties and attribute values of events.

-   attribute-selector. A [key=value] search to target a specific
    attribute in the search.

wheretext
---------

Parameters:

-   string. A search text (that may contain wildcards) that is matched
    against textual properties and attribute values of events.

-   attribute-selector. A [key=value] search to target a specific
    attribute in the search.

map
---

Parameters:

-   One or more expression(s). See 6.3 Expression. The first calculates
    the first element of the output vector, the second expression
    calculates the second element of the output vector.

-   Column name

Examples:
```
[
    input ~/Simulator/WTUR01 [defaultTurbinePower];
    input ~/Simulator/WTUR01 [defaultTurbineWindSpeed]
]
|> combine
|> map 'v[0]*1000' In\_MW 'v[0]/v[1]' Power\_per\_speed

map 'c["MP"]' Havoygavlen\_Max\_effekt bindings {
MP=farmMaxPowerInMW[~/HAVGAV [farmMaxPowerInMW]]
```

>Note:

-   To use expression on multiple inputs use combine to combine the
    inputs into a vector.

-   Available variables:

    -   `v` -- vector of values from the input. Accessible by index. Ex:
        `v[0]`

    -   `prev` -- previous vector of values

    -   `dt` -- time difference between current event and previous one in
        milliseconds.

    -   `sampleRates[]` -- sampleRate array that contains the sampleRate
        for each channel (for SampleSetEvent input only)

    -   `c` -- vector of constant values bound from attributes in nodes.
        Accessible by key. Ex: `c["MP"]`

    -   `cv` -- vector constant of vectors values bound from attributes in
        nodes. Accessible by key and index. Ex: `cv["v1"][0]`

    -   `cm` -- matrix (array of array) constant of matrix values bound
        from attributes in nodes. Accessible by key and index. Ex:
        `cm["m1"]`

-   Mathematical expressions are available from .Net Math
    class](https://msdn.microsoft.com/en-us/library/system.math(v=vs.110).aspx)

-   The following functions are also available:
```csharp
    bool HasPassedLimit(double lowThreshold, double highThreshold,
    double value, bool previousResult)

    double Median(double[] value)

    double Maximum(double[] value)

    double Minimum(double[] value)

    double[] Diff(double[] value)

    double[] NonZero(double[] value, double epsilon=1e-10)

    double AngleMedian(double[] values)

    double AngleMedian2(double[] values)

    double DotProduct(this double[] v1, double[] v2)

    double[] SubVector(this double[] v, int startIndex, int count)

    double[] SubVector(this double[] v, int startIndex, int count)

    double[] SubVector(this double[] v, params double[] indexes)

    double[] ToVector(params double[] v)

    double[] SetSubVector(this double[] v, int startIndex,
    double[] subVector)

    double[] SetSubVector(this double[] v, double[] indexes,
    double[] values)

    double[] SetAll(this double[] v, double value)

    double DotProduct(this double[] v1, int offset1, double[] v2,
    int offset2)

    double MeanDeviation(double[][] v)

    double MeanDeviation(double[] v1, double[] v2)

    double MeanAbsoluteDeviation(double[][] v)

    double MeanAbsoluteDeviation(double[] v1, double[] v2)

    double MeanSquareDeviation(double[][] v)

    double MeanSquareDeviation(double[] v1, double[] v2)

    double[][] FilterRowsWithNaN(double[][]values)

    bool IsNaNColumn(double[] values)

    bool HasNaNColumn(double[][] values)

    double WeightedAverage(double[] values1, double[] weights)

    double[] Col (this double[][] values, int col)

    double[] Row (this double[][] values, int row)

    double[] Add (this double[] v1, double[] v2)

    double[] Add (this double[] v1, double v)

    double[] Minus (this double[] v1, double[] v2)

    double[] Multiply (this double[] v1, double[] v2)

    double[] Multiply (this double[] v1, double v)

    double[] Divide (this double[] v1, double[] v2)

    double[][] AscendingSort (this double[] v1)

    double[][] DescendingSort (this double[] v1)

    //return array
    //from min to max separated by step
    double[] Range(double min, double max, double step) 
    
    //return [ maximas\_indexes[], maximas\_values[],
    //minimas\_indexes[], minimamas\_values[] ]
    double[][] PeakDet(double[] v, double delta, double[]
    indices=null)
```
boolop
------

Takes multiple (alarm) inputs and calculates a Boolean result.

Parameters:

-   Expression

Example: 
```
... |> boolop 'b[0] && b[1]'
```


startWithLatest
---------------

Modifies the input operation so that it starts with the last known
element. Intended for use in realtime queries only.

Example:
```
input ~/Simulator/WTUR01 [defaultTurbinePower]
    |> startwithlatest
```
takebefore
----------

Starts the sequence with the Nth item before the given time.

Parameters:

-   Time. See Time
-   incl | excl (default incl) Specifies how an item exacly at the time should be handled. By default it is included in the result stream before the count is started
-   Number of elements

Example: 

```
takebefore 09.02.2016 5
takebefore now 5. takes 5 items before the given date not counting an item at time now
takebefore now excl 5. takes 5 items before the given date counting an item at time now
```
Starts the input sequence with 5 items before the given time

Note: the operation is intended for use more or less directly after the
input operation.

Operations that depend on the normal chronological order of the events
in the sequence may lead to undefined behaviour. This is because the
input sequence is run backwards until the desired count of event is
reached.

![The marble diagram should be here](https://github.com/kognifai/Galore/blob/master/.attachments/takebefore-marble.png)

takeafter
---------

Modifies the input operation so that it ends with N items after the
given time.

Parameters:

-   Time. See Time
-   incl | excl (default incl) Specifies how an item exacly at the time should be handled. By default it is included in the result stream before the count is started
-   Number of elements

Example:
```
takeafter 09.02.2016 5. takes 5 items after the given date
takeafter now 5. takes 5 items after the given date not counting an itme at time now
takeafter now excl 5. takes 5 items after the given date counting an itme at time now
```

Note: The operation only works on historical data. This is expected to
change.

![The marble diagram should be here](https://github.com/kognifai/Galore/blob/master/.attachments/takeafter-marble.png)

takefrom
--------

Specifies the start time of the input range

Parameters:

-   Time. See Time
-   incl | excl (default incl) Specifies how an item exacly at the time should be handled. By default it is included in the result stream

Examples:
```
takefrom 2016-01-01T00:00:00Z
takefrom now excl
```
```
input ~/Simulator/WTUR01 [defaultTurbinePower] 1d
    |> takefrom 2016-01-01T00:00:00Z
    |> taketo 2016-01-01T04:00:00Z
```

![The marble diagram should be here](https://github.com/kognifai/Galore/blob/master/.attachments/takefrom-marble.png)

taketo
------

Specifies the end time of the input range

Parameters:

-   Time. See Time
-   incl | excl (default incl) Specifies how an item exacly at the time should be handled. By default it is included in the result stream before the count is started

Examples:
```
taketo 2016-01-31T23:59:59Z
taketo now. takes items up to the the given date including an item at time now
taketo now excl. takes items up to the the given date excluding an item at time now
```
```
input ~/Simulator/WTUR01 [defaultTurbinePower] 1d
    |> takefrom 2016-01-01T00:00:00Z
    |> taketo 2016-01-01T04:00:00Z
```
![The marble diagram should be here](https://github.com/kognifai/Galore/blob/master/.attachments/taketo-marble.png)

gapfill
-------

Fills in zero (null?) values where query fails to produce values for
output sequence

Parameters:

-   Starttime. See Time

-   Endtime. See Time

Example: 
```
gapfill 2016-01-01T00:00:00Z 2016-01-31T23:59:59Z
```

dump
----

Writes information about the items in the sequence to the system log.
Intended for debugging.

amplitudespectrum
-----------------

Writes information about the items in the sequence to the system log.
Intended for debugging.

powerspectrum
-------------

Creates the power spectrum (normalized) from each channel in the input
sampleset

orderspectrum
-------------

Creates the order spectrum from a sampleset with one channel of samples
and one channel of rotational positions.

Parameters

-   Index of sample channel

-   Index of position channel

evenangleresampling
-------------------

Resamples a sampleset channel sampled with a sample interval of delta t
to a new sampleset with a fixed delta pos/angle

Parameters

-   Index of sample channel

-   Index of position channel

transpose
---------

Transpose a matrix/sampleset

rootmeansquare
--------------

Compute rms value for a channel in a sampleset.

sliding window
--------------

Slides a window over a sequence of vectors and emits a matrix with the
contents of the window. This is useful for implementing signal
processing filters.

Parameters:

-   interval. Size of window as interval

-   count. Size of window as count.

    -   emitOnlyOnFullCount. Option for dealing with gaps in data

Interval only: Emits all events in window for every new input

Count only: Emits all events in window for every new input. Optionally
emit only on full count.

Both interval and count. Emit the last N=count events in the window.
Optionally emit only on full count.

timeseries to matrix
--------------------

Convert a time series to a sampleset. Useful for doing frequency
spectrum analysis on time series data.

lookup, where etc.
------------------

fastfouriertransform
--------------------

Calculates the FFT which is to be used in various fft-operations for
each channel. The output from this operation is two channels -- one for
real and one for imaginary numbers.

Example:
```
input ~/Test/somedata |> fastfouriertransform
```

amplitudespectrumfft
--------------------

Creates an amplitude spectrum based on output from the
fastfouriertransform-operation for each channel in the sample set. Each
channel must have real and imaginary values.

Example:
```
input ~/Test/somedata |> fastfouriertransform |>
amplitudespectrumfft
```

powerspectrumfft
----------------

Creates a power spectrum based on output from the
fastfouriertransform-operation for each channel in the sample set. Each
channel must have real and imaginary values.

Example:
```
input ~/Test/somedata
    |> fastfouriertransform
    |> powerspectrumfft
```

hanningwindow
-------------

Multiplies the values in each channel in the sample set with a hanning
window.

Example:
```
input ~/Test/somedata |> hanningwindow
```

fftavg
------

Creates an averaged FFT with overlap for each channel in the sample set.
Each part is multiplied with a hanning window and the product is an
amplitude spectrum.

Parameters

-   Percentage overlap (must be between 0.0 and 1.0)

-   Number of averages

Example:
```
input ~/Test/somedata |> fftavg 0.67 4
```

firfilter
---------

Create filter functions for lowpass- highpass- and bandpass filtering of
a signal. The filter type used is finite impulse response (FIR) filter.

Parameters

-   Low cutoff limit, for lowpass and pandpass filtering

-   High cutoff limit, for bandpass and highpass filtering

-   Filter type, integer value where 0=lowpass, 1=highpass and
    2=bandpass

-   Window type, integer value where 0=hanning, 1=hamming, 2=blackman,
    3=flattop and 4=barlett

-   Window lengt, the smoothness of the filtering. Recommended values
    30-45.

Example:
```
input ~/Test/somedata |> firfilter 2000.0 20000.0 2 2 32
```

fftzeroing
----------

Set signal values to zero(0) for given frequencies after FFT. All values
below the highest value of Fcutoff and Fmin are set to zero. All values
above Fmax is set to zero,

Parameters

-   Low frequency cutoff

-   Minimum frequency

-   Maximum frequency

Example:
```
input ~/Test/somedata |> fftzeroing 5000 4000 15000
```

decimation
----------

Shortens a timeseries by selecting values spaced by a decimation factor.
The decimation factor is calculated from parameter Fmax.

Valid Fmax values: 100, 200, 400, 800, 1500, 3000, 6000, 12500, 25000,
50000, 100000

Parameter

-   Maximum frequency (Fmax)

Example:
```
input ~/Test/somedata |> decimation 1000
```

fftcrossphase
-------------

Creates a cross phase spectrum with overlap based on two channels input.
Divided into a number of subsamples, each subsample is multiplied with a
hanning window. Each complient subset of the two input signals are
multiplied together. The average of these subsets are the resulting
cross phase spectrum.

Parameters

-   Percentage overlap (must be between 0.0 and 1.0)

-   Number of subsample

Example:
```
input ~/Test/somedata |> fftcrossphase 0.75 4
```

orbitplot
-------------

Creates an orbit plot with two output signals, distributed in separate
channels. The output sample rate is changed in relation to the input
sample rate. Three input channels are required. Channel 1 shall be the
horizontal vibration sensor signal. Channel2 shall be the vertical
vibration sensor signal. Channel 3 shall be the induction sensor signal.
All three signal shall have equal length.

Parameters

-   Threshold (must be between 0.0 and 1.0)

-   Resampling method, string value where resample = Resample
    and linint = Linear interpolation

-   Edge type, string value where rising = Rising and falling = Falling

Example:
```
input ~/Test/somedata |> orbitplot 0.7 resample falling
```

rollplot
-------------

Creates a roll plot with two output signals, distributed in separate
channels. The output sample rate is changed in relation to the input
sample rate. Two input channels are required. Channel 1 shall be the
vibration sensor signal. Channel 2 shall be the rpm sensor signal.
Both signal shall have equal length.

Parameters

-   Threshold (must be between 0.0 and 1.0)

-   Resampling method, string value where resample = Resample
    and linint = Linear interpolation

-   Edge type, string value where rising = Rising and falling = Falling

Example:
```
input ~/Test/somedata |> rollplot 0.7 linint rising
```

pick
-------------

pick channels from a sampleset

Parameters

- list of channels. The _ is a place holder, put an empty channels at that position
- expression for setting the channel index to pick. If negative, it will be ignore.
- fillwithempty, when using expression this will insert an empty channel whenever the index is negative.

Example:

````
input ~/Test/somedata |> pick 0 1 2 
# pick the first 3 channels
    
input ~/Test/somedata |> pick 0 0 _ _ 2 
# pick channels 0 twice and add 2 empty channels and then pick channels 2
    
input ~/Test/somedata |> pick 'index%2 == 0 ? index : -1' 
# pick all channels with an even index

input ~/Test/somedata |> pick 'index>2 ? index : -1' 
# pick all channels having an index greater than 2

input ~/Test/somedata |> pick 'index>2 ? index : -1' 'index<=2 ? index : -1' 
# pick first all the channels having an index greater than 2 and then the first 3 channels. The first 3 channels will become the last 3 channels.
````


slicesampleset
-------------

slice a sampleset into doubles events.

Parameters

-  Interval 

-  start index

-  slice size

-  allrows

-  time selector:
		* maxoffset | minoffset | firstoffset | lastoffset | default | eventTime | samplesettime | offsetindex #idx

Example:
```
input #1 |> slicesampleset 1h allrows offsetindex 1

input #1 |> slicesampleset 1h allrows lastoffset

input #1 |> slicesampleset 1h 0 4
```

tosampleset
-------------

convert a list of doublesevents to a sampleset. The offset of the sampleset will always be the unix time of the first event in the list. The samplerate will be the inverse of the total hours in the output interval (of the input). For example day data, the samplerate will be 1.0/24.

Parameters

-   uselasttime, if set this flag will use the last event time to set the time of the output sampleset, 
	otherwise use the first one.

Example:
```
input ~/Test/somedata |> takefrom starttime |> taketo endtime |> tosampleset

input ~/Test/somedata |> takefrom starttime |> taketo endtime |> tosampleset uselasttime
```

timeout
-------------

Emits timeout state change events after a given interval since last input.

Parameters

-   Interval between last input and emitting timeout event

-   Optional 'useinputtime' to use the timestamp of the last input event as
    opposed to real time now. The effect of this is to "backdate" the timeout event to the time of the previous input.

Examples:
```
input ~/Test/somedata |> timeout 1m
input ~/Test/somedata |> timeout 1m useinputtime

```
![The marble diagram should be here](https://github.com/kognifai/Galore/blob/master/.attachments/timeout-marble.png)

monitor
-------------

Defines a state machine with conditions for going from one state to another. Emits state change events when a state transition happens. The event contains information that
- identifies the current and previous state
- the time it entered the current and previous state
- a description of the transition

Parameters
-   Optional start state, the default is state 0
-   A list of state transition definitions separated by semicolon
A state transition defines:
    - The from and to state
    - An index reference to the input for the condition
    - A boolean condition expression
    - An optional message that will be added to the output events description field

Examples:
```
input ~/Test/somedata |> monitor start in 1
   0 -> 1 i0 'v[0] > 50' 'triggered greater than 50';
   1 -> 0 i0 'v[0] < 45' 'back to normal';

[input ~/Test/somedata; input ~AlarmWithAck/Calculator -userack]
    |> monitor
        0 -> 3 i0 'v[0] > 50' 'greater than 50';
        3 -> 2 i0 'v[0] < 45' 'below 45, waiting for ack';
        3 -> 1 i1 'true'
        1 -> 0 i0 'v[0] < 45' 'back to normal from acked';
        2 -> 0 i1 'true';
        2 -> 3 i0 'v[0] > 50' 'retriggered greater than 50';",


```
![The marble diagram should be here](https://github.com/kognifai/Galore/blob/master/.attachments/monitor-marble.png)

groupdata
---------

Multidimensional data grouping for Model Output Statistics (MOS) and some other machine learning algorithm. The input data must be a sampleset with at least one channel
This function will group the input data base on the number of subdivision for each channel. The number of subdivision and the min max for each column are parameters that the user can enter. If no min/max are given, the operation will pick the min/max automatically for each channel.

The output of this operation will be a sampleset containing the following channels:

* channel containing the lower bounds of the first input channel (**length** = #grp_0 * #grp_1* #grp_n)
* channel containing the upper bounds of the first input channel (**length** = #grp_0 * #grp_1* #grp_n)
* ...
* channel containing the lower bounds of the last input channel (**length** = #grp_0 * #grp_1* #grp_n)
* channel containing the upper bounds of the last input channel (**length** = #grp_0 * #grp_1* #grp_n)
* channel containing the row indexes of points that have fallen into group **0** 
* channel containing the row indexes of points that have fallen into group **1**
* ...
* channel containing the row indexes of points that have fallen into group  **#grp_0 * #grp_1* #grp_n**



So base on the above, one can see that the output sampleset set will have a number of channels that depends on the number of input channels and the number of groups per channel.

For example, for a  sampleset with 2 channels, the following tql command:

````
input ~sampleset |> groupdata 2 4
````

will produce an output sampleset of 2x2 + 2x4 = 12 channels

In the example above, **#grp_n** represent the number of subdivision in group n.


Parameters

-   number of subdivision vectors: #grp_0 #grp_1 ... #grp_n
-   min/max border values: pair of float, of the form (min0,max0),(min1,max1),...

The length of the groups vector must match the length of the min/max pair if the last last is given.

Example:
```
input ~/Test/somedata |> groupdata 2 # divide the input data into 2 groups and using min/max from the first data

input ~/Test/somedata |> groupdata 2 5 # divide the input data into 2 groups forst the first channel and each group will be divided into 5 groups base on the second channels data. Min/max are taken from the input data.

input ~/Test/somedata |> groupdata 2 5 (0,10),(0,100) # divide the input data into 2 groups forst the first channel and each group will be divided into 5 groups base on the second channels data. Min/max are take from the input arguments
```

maps
----

same as map (See 9.18) but the return value of the expressions can be a single value, an array or an array of array. The output of this operation 
will be a sampleset containing all the channels comming from the expressions. It is also possible to specify the samplerate and the offset in each expression. 
In that case, it will affect all the channels in that expression.

Example:

The expression below produce a sampleset with one channel which is the first channel of the input. The output sampleset will have a samplerate 1 and offset 0.
```
input ~/Test/somedata |> maps 's[0]'

The expression below produce a sampleset with one channel having a samplerate twice the original sampleset
```
input ~/Test/somedata |> maps 's[0], sampleRates[0]*2'

The expression below produce a sampleset with one channel having a samplerate twice the original sampleset and the opriginal offset+100
```
input ~/Test/somedata |> maps 's[0], sampleRates[0]*2, offsets[0]+100'


The Histogram function return an array of array containing : the bins count, the lower bounds and the upper bounds for each bin.
The 2 expressions below will produce a sampleset with 6 channels, 3 for the first histogram and 3 for the second one.
```
input ~/Test/somedata |> maps 'Histogram(s[0],10)' 'Histogram(s[0],10,0.0,100.0)'
```

The expression below will produce a sampleset with 4 chanels, 3 comming from the Histogram and 1 which is the first channel of the input
```
input ~/Test/somedata |> maps 'Histogram(s[0],10)' 's[0]'
```

The followinf functions are also available and also all those define in map:
​```csharp
		double[][] Rows(this double[][] values, params int[][] rows)
		double[] Col(this double[][] values, int col)
		double[][] Cols(this double[][] values, params int[] cols)
		double[][] Cols(this double[][] values, params double[] cols)
		double[][] Cols(this double[][] values, Func<double,double> columnDelegate)
		double[][] Cols(this double[][] values, Func<int,int> columnDelegate)
		double[][] Cols(this double[][] values, Func<int,bool> columnDelegate)
		double[] Row(this double[][] values, int row)
		double[][] Rows(this double[][] values, params int[] rows)
		double[][] Rows(this double[][] values, params int[][] rows)
		double[][] Rows(this double[][] values, params double[] rows)
		double[][] Rows(this double[][] values, params double[][] rows)
		double[][] Rows(this double[][] values, Func<double,double> rowDelegate)
		double[][] Rows(this double[][] values, Func<int,int> rowDelegate)
		double[][] Rows(this double[][] values, Func<int,bool> rowDelegate)
		double[] Append(this double[] vec1, double[] vec2)
		double[] Prepend(this double[] vec1, double[] vec2)
		double[][] RemoveColumn(this double[][] array, int index)
		double Polynomial(this double[] coefficients, double value)
		double[] PolynomialValues(double value, int coefficient, int startCoefficient = 0)
		double[] Repeat(this double[] values, int times)

		double[][] Histogram(this double[] values, double[] upperLimits)
        double[][] Histogram(this double[] values, int nbins, double min, double max)
        double[][] Histogram(this double[] values, int nbins)
        double[][] Histogram(this double[] values, int nbins, double min)        
```
