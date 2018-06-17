Pipeline Operations
===================

input
-----

The input operation is used to specify starting point for a pipeline. A single input operation can specify one or more event source nodes from Galore asset model. If multiple nodes are selected, parallel pipelines are created and each consecutive single input operation is applied to each pipeline. The first multi-input operation will merge the pipelines.

The input operation has the form:
```
input <nodeselector> [stream selector] [acceptStale]
```
The stream selector has the form:
```
-[related-|count-]<streamtype> [[>=|<=]pre-aggregate interval]
```
Stream type can be prefixed with:
-   related - Selects events that have propagated to the selected node from different source nodes e.g. from subsystem or as causes of
    other events
-   count - For large event types such as sample sets and matrixes count can be used to indicate where they exist in time e.g. for         navigation without loading the whole event. This also combines with the pre aggregate interval parameter to get number of events       in a period.

The optional `>=` and `<=` operators are used to select a pre aggregate without knowing the exact level. This is used e.g. when calculating a suitable resolution for plotting or event aggregates and allows the client application to not know the exact levels available.

Stream types can be customized but these are predefined by the system:
 
-   timeline- Obsolete but kept for use by Galore timelines. Must move to custom types
-   vec- Timestamp + vector, also known as time series
-   ss- Sample set
-   matrices- List of matrixes
-   matrix- Matrix
-   alarm- Galore built-in alarm type
-   sc- Generic state change, use custom type instead
-   streamedit- Real-time information about changes in history for a stream. Real-time only
-   configedit- Notification about asset model changes
-   calculatorstate- The state of a calculator. Real-time only
-   userack- This option set is used for clients to acknowledge alarms in the calculator or monitor

Parameters:

-   nodeSelector- See [Node Selector](https://github.com/kognifai/Galore/blob/master/SDK-documentation/Node%20Selector.md) selects one     or many nodes in the Galore asset model. If the node is not an event source node, all descendants that are alarm log nodes.
-   streamSelector- See [Streamselector](#Streamselector). Nodes may have multiple streams associated e.g. propagated or relate           events, pre aggregates, change notifications etc. In many cases, the stream selector can be left out because it is inferred from the     node type.
-   interval-  See [Interval](https://github.com/kognifai/Galore/blob/master/SDK-documentation/Node%20Selector.md). Interval is part of     the stream selector. It Determines which pre-aggregate interval is selected. The closest larger pre  aggregate interval will be         selected. Default is 0 i.e. not aggregated data.
-   acceptStale - Forces the underlying index to update before reading alarms. Default is False.

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

Defines the output unit of a query operation. This must be used whenever the system could not find the unit by itself or to override the one found by the system.

Example:
```
input ../W, ../WNAC/WdSpd 
    |> combine
    |> tounit Power:SI:W Speed:SI:m/s
```
This example sets a unit for each input that is in the output of the
combined operation.

jitter
------

The jitter operation is used to remove jitter from time stamps. This is particularly useful before the merge operation to align the time stamps in multiple inputs.

Parameters:

-   interval. See [Interval](https://github.com/kognifai/Galore/blob/master/SDK-documentation/Node%20Selector.md)

resample
--------

This operation resamples the input sequence with in the given sample interval. At present,
the input value must be used right before the resample time with no interpolation.

Parameters:

-   interval- Interval between each sample. See 6.1 Interval.

-   type- Upsample or Downsample. Needed for correct handling of gaps in the input data. Upsample fills gap with the same value.
    Downsample does not.

generate
--------

Generate creates a (recurring) time series from a scalar value. The interval between entries, the number of values for each entry and the number of entries can be controlled.

Usage:
```
generate time period [value] [vectorSize] [realtime [count]]
```
Parameters:

-   time- Starting time for series

-   period- Interval between entries. See [Period](https://github.com/kognifai/Galore/blob/master/SDK-documentation/Node%20Selector.md-

-   value- The value of each entry. Defaults to 0 if missing

-   vectorSize- Number of values produced per entry

-   real time- States if the time series must be recurring. If set, the time parameter is ignored. Time series starts on current           time
-   count- Number of repeating entries. Relevant if real-time only

Examples:
```
generate now 1s 3.1415
generate now 2s 3.1415 5
generate now 5s 3.1415 2 realtime 10
```
aggregate
---------

Aggregates the input sequence into a single value or a value for each period

Parameters:

-   period- If given, the aggregate produces an aggregate for each
    period. See [Period](https://github.com/kognifai/Galore/blob/master/SDK-documentation/Node%20Selector.md)

-   isRunning- A partial aggregate is produced for every input value

-   type

    -   MinMaxAvg, The output is a vector with avg, min and max

    -   Statistics, The output is a vector with avg, min, max, count,
        stddev and median

    -   Integrate, The output is the integral with respect to time

    -   IntegrateInterpolate, The output is the integral with respect to
        time

    -   AvgUnitVector, The output is the average of the input treated as
        an angle

-   output time unit- Only used with integrate. E.g. if the input is "W" (Watt) the default output unit is "Ws" (Watt seconds). Using       an output unit of "h" is "Wh" (Watt hours) instead

combine
-------

Combines multiple inputs into a single output vector by trying to align the input streams on time. If some inputs do not have a value at a particular time, the previous value is used. Most useful for time series inputs.

Example:
```
input ~/Simulator/WTUR* [defaultTurbinePower] |> combine 10s
```

Parameters:

-   Optional, debounce interval (real-time only, default 0). If one or
    many input is delayed, emit a new combined event by reusing the previous value of the delayed inputs. I.e resample with hold. If
    omitted or 0, the combine operation will emit for every input change.

>Note: The force delay must be more than the expected interval of the slowest input. I.e. the forcing must only be applied when an input is actually and significantly delayed as opposed to just having a bit of timing jitter. It must not be used to force a fixed output rate.

>Tip: If the input events are not clustered in time, it makes more sense to not to use the debounce interval option. Instead, follow the operation by resample to sample the inputs at specific time.

>Note: The real-time and historical version of this operation produces slightly different results since the historical emits on every distinct timestamp.

combinewithouthold
------------------

It is as same as **combine** but it does not use the last value if there is no new value in the given interval.

It has two parameters:

-  The interval to wait for the inputs to get a value

-  What to fill for the inputs that have no new value (fillWithNaN,
    fillWithZero, fillWithDefault, fillWithValue)

Examples:
```
combinewithouthold 10m fillWithNaN

combinewithouthold 10m fillWithValue 5
```
merge
-----

Combines multiple inputs into a single output by interleaving the input events based on their timestamp. Most useful for alarm/event inputs.

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

Ignores elements from a sequence which are followed by another value within a computed throttle duration.

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

Applies an expression to each row of a sample set and return a sample set with n channels, where n is the number of expression.

Example:
```
Input ~/path/to/samplerset |> dotmap 'v[0]*v[1]'
```

This example returns a new sample set with one channel; each element of the first row of the input channel is multiply by the elements of the second input channel.

>Note:

-   To use expression on multiple inputs, use **combine** to combine the inputs into a vector.

-   Available variables:

    -   `v` -- vector of values from each row. Accessible by index. Ex:
        `v[0]`

    -   `prev` -- previous output value

    -   `dt` -- time difference between previous and current event in
        milliseconds.

    -   `sampleRate` -- channel sampleRate (in dot map all the channels
        must have the same sampleRate)

    -   `rowIndex` -- index of the current row is being processed

    -   `totalRows` -- total number of rows in the input sample set (all
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

Produces the top N active events (alarms) at each point in time as a list.

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

-   expression. A boolean expression (also known as a predicate) to determine which items of the sequence to discard. See [Expression](https://github.com/kognifai/Galore/blob/master/SDK-documentation/Node%20Selector.md)

Example: 
```
discard 'v[0] \> 1'. 
```
Discards all items where the first vector element is larger than 1

wheretext
---------

Parameters:

-   string- A search text (that may contain wildcards) that is matched against textual properties and attribute values of events.

-   attribute-selector- A [key=value] search to target a specific attribute in the search.

wheretext
---------

Parameters:

-   string- A search text (that may contain wildcards) that is matched against textual properties and attribute values of events.

-   attribute-selector- A [key=value] search to target a specific attribute in the search.

map
---

Parameters:

-   One or many expression(s). See [Expression](https://github.com/kognifai/Galore/blob/master/SDK-documentation/Node%20Selector.md).       The first calculates the first element of the output vector, the second expression calculates the second element of the output         vector.

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

-   To use expression on multiple inputs use **combine** to combine the
    inputs into a vector.

-   Available variables:

    -   `v` -- vector of values from the input. Accessible by index. Ex:
        `v[0]`

    -   `prev` -- previous vector of values

    -   `dt` -- time difference between  previous current event in
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
element. Intended for use in real-time queries only.

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
-   incl | excl (default incl) Specifies how an item exactly at the time should be handled. By default it is included in the result stream before the count is started
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
