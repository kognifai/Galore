 
Welcome to Galore Documentation! 

Galore and its components are described in this section. You can read about each one of the Galore components that knowledge can help you to develop your own applications on Galore.

See Also

  - [Galore Asset Model](#Galore-Documentation)

  - [Streams](streams.md)

  - [TQL functional query language](TQL%20Syntax.md)

  - [Node Selector](Node%20Selector.md)
 
  - [Pipeline Operations](Pipeline%20Operations.md)


# Galore Asset Model [![Gitter Join the chat](https://badges.gitter.im/Join%20Chat.svg)](https://gitter.im/kognifai/Lobby)


The Galore asset model is a Directed Acyclic Graph (DAG), in other words, each node (except the root node) has one or more parents and zero or more children. Each node in the DAG has a tag (except the root node). Tag is a string that identifies the node in the context of its parent node. The tag must not be confused with the node's display node. Each node has a path that uniquely identifies it. 

Path describes how to traverse DAG from root to node. Path is a list of tags of each node visited during this traversal and separated by the Slash character `/`.

Example: `/Farm/Turbine/Nacelle/WindSpeed.

# Streams of Events

The fundamental data structure of Galore is a stream of events. Most streams in Galore persist history (but not all). An event in Galore is a time stamp data. [More on events here](#event-types). In Galore, streams are ordered by timestamp.

A stream in Galore is similar to a list or a table with some important differences:

-   An event, in a sequence, does not have an index, thus, it is not possible to
    refer an event by its absolute position in the stream.
-   A stream can extend in the future that can be an indeterminate size.

Other important properties of Galore streams:

-   A stream in Galore is chronologically ordered i.e. order by event timestamp.

Examples of streams in Galore:

-  **Measurement time series** is a sequence of timestamp values or vectors. Value is a vector with a single element.

-  **Alarm log** is a sequence of alarms on a system or a subsystem

- **Active alarms** is a sequence of lists where each list contains active alarms. Each system and sub system has an active alarms sequence, whenever the state of an active alarm changes, it reflects in the active alarms sequence.

-  **Sample set logs** are streams of complex data structures such as weather forecast, production forecast, power curves,           amplitude spectrum, and much more.

# Event types 

The following event types are supported by TQL:

-   **Vector -** This event has a vector of floating point values with one or many elements. Stored streams of vector events have an associated time series node in the Galore asset model. The node contains **element descriptors** which describes the vector element properties such as storage unit and display unit. The element descriptors is also referred as **metadata** in vector and time series.

-   **Sample set -** This event is a matrix or a table. Each column in the matrix is a **channel**. 
      For example,  for a weather forecast one channel can be a wind speed and another a temperature. 
    Stored streams of sample set events have an associated sample set node in the Galore asset model. The node contains **channel descriptors** which describes the channel properties such as storage unit display unit, sample rate and sample dimension. The channel descriptors is also referred as **metadata** in the sample set sequence.

-   **Alarm-** An alarm event has the following properties:

    -   Level: (Info/light blue, Warning/yellow, Alarm/red,
        Emergency/magenta)

    -   Active: (true, false/green)

    -   Acknowledged: (true or false). A user or system(auto ack.) has
        acknowledged the alarm

    -   Passed: (true or false). Triggering condition has passed but
        alarm is not acknowledged

    -   Message

Most TQL operations work on subset types. Some operations have different parameters for different input stream types. Boolean
operations work on alarm streams active property.

## Next Topic
[Streams](streams.md) is the next topic to explore. 
