
# Galore Asset Model

The Galore asset model is a Directed Acyclic Graph (DAG), in other words, each node (except the root node) has one or more parents and zero or more children. Each node in the DAG has a tag (except the root node). Tag is a string that identifies the node in the context of its parent node. The tag must not be confused with the nodes' display node. Each node has a path that uniquely identifies it. 

The path describes how to traverse the DAG from the root to reach the node. The path is a list of tags of each node visited during this traversal and separated by the Slash character `/`.

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


