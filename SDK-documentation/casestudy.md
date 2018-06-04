Galore Case Study
=================

In order to get a better overview of the galore capabilities, we will
explore it in a simplified case study.

Lets assume that we are a small Wind Farm owner and operator and we want
to get better control over our assets. In this case the assets are wind
turbines and related infrastructure. Galore is very general with regards
to what type of assets it can model. We could just as well have chosen
our case study to be about ships, oil rigs, fish farms and so on. In
other words, any asset that lends itself to hierarchical modelling and
that produces data that can be represented as streams of measurements,
events and samplesets is a good match for Galore.

Data Acquisition/Data Ingest
============================

Our small windfarm has these major components that we want to monitor:

-   A number of wind turbines

-   An electrical substation

-   A weather station

Each of these assets have built in controllers and instrumentation that
continuously produces data:

-   Measurements of physical parameters important to the operation of
    the asset and its subcomponents. E.g. current power output, wind
    speed, wind direction, rotation speed etc.

-   State information amount the whole asset or one of its subsystem.
    E.g. Stopped, Starting, Operational, Shutting down

-   Alarms/Events from the controllers when it detects errors in a
    subsystem

In addition, we have external data sources we would like to integrate
into our system e.g.

-   Weather forecasts

-   Price forecasts

-   Actual prices

In order to get all these data connected to our Galore system, we will
use a suitable Kognifai Edge solution \<insert reference\>. Here we will
set up the "Connectors" to gather all the data from the various data
producing systems. The edge solution will automatically set up an asset
model in Galore that might look something like this:

![EdgeModel](images/EdgeModel.png) Edge Model

The asset model can be viewed in and edited the galore configuration
tool. The circular items represent assets and sub-assets. The other
items represents streams of data, often referred to as tags. A real
system can have hundreds or thousands of tags per asset so this is
simplified a great deal.

A few things to note about the tags. The system contains metadata about
each tag that describes the data in the stream e.g.:

-   Power output. Is a stream timestamped single measurements with a
    unit of Watts. The expected update rate is part of the metadata.

-   Wind. Is a stream of vectors (speed and direction) with the units of
    Metres per second and direction in degrees. The expected update rate
    is part of the metadata.

-   State is a stream of states and is represented by the event stream
    type. Each possible state is described by metadata. The system keeps
    track of the duration of each state and when it transitions to
    another state. Each event can have custom attributes with extra
    information related to the event e.g. descriptions and messages.

-   Weather forecasts is a stream of sample sets. A new forecasts is
    expected to arrive every six hours and predicts the "average"
    weather for the next 48 hours. A sampleset is essentially a table
    where each column can represent any physical parameter sampled at
    regular interval along another physical dimension such as time. The
    metadata describes what each column represent. In our simplified
    case, we have 3 columns with temperature, windspeed and wind
    direction sampled every hour for the next 48 hours

Asset modelling
===============

To make our data more accessible, we will create a more detailed model
of our turbine assets. This will give more context to the data and make
it easier for a user to navigate the data and for and application
developer to create context sensitive user interfaces, dashboards and
reports.

\*The asset models are sometimes referred to as logical models or
semantic model.

Our turbine asset model could look something like this:

![AssetModel](TurbineAssetModel.png) Turbine Asset Model

This is a very much simplified model of a wind turbine, but we already
have a representation of some of the main components of a real turbine.
In general, the more data we have and the more context we want to
provide the more detail we will add to our model. It is also possible to
provide multiple models of the same asset with different level of
detail.

When we are happy with the model, we can make it into a "model template"
and then apply the same template to each turbine in our wind farm.

Note that the tags here are indicated with dashed lines. This indicates
that this is a link to another part of the complete asset model. In this
case the nodes representing data tags are the same as we got
automatically from the Edge system. We just point to them in our asset
model. These links are part of the template and as far as each edge
model has the same tags, we can include the complete link in the
template. If the tag varies from one asset to another we can adjust the
links to point to the correct tag after applying the template.

Attributes
==========

Each node in the asset model can store attributes. Some of the
attributes are used by galore itself to store important information
about that particular part of the system. For example:

-   Node name

-   Node Id

-   Stream metadata (for nodes representing streams)

-   TQL \<ref to TQL doc\> queries (for nodes generating new streams
    derived from other streams)

-   Parameters for TQL queries such as constants and lookuptables.

-   Access control attributes. These allow nodes to be hidden or write
    protected so that only certain users can read or modify them.

-   Attributes can also be used to mark nodes so that the can be
    selected by an node selector with attribute matching \<ref to TQL
    doc\>

Other attributes are defined by the application and galore itself does
not care about their content.

Streams
=======

We have already mentioned streams several times without explain what is
meant. The Galore stream is an abstraction of data that has several
attractive properties:

-   It is an ordered sequence of data items, This makes it suitable to
    represent sequences of measurements, in other words timeseries.

-   It lends itself to pipeline processing that is, processing
    operations can be chained to transform streams into new streams. TQL
    is a language for describing such processing pipelines

-   It allows the unification of real-time processing and processing of
    historical data. Many systems with some similar capabilities as
    Galore have strict separation between historical data processing and
    real-time data processing.

-   It allows processing definitions (TQL) to take several types of
    streams as input.

Calculators and derived streams
===============================

While TQL queries can be used to run ad hoc processing from client
application and services, they can also be stored in the galore asset
model.

In our case study we will give to simplified examples of this can be
used. We will skip over the details of the TQL queries. See the TQL
documentation for more details \<ref\>.

1.  Production forecast

Wind turbines have a defined relationship between wind speed and power
output. This is called the power curve. We can add the power curve to
our asset model, either as an Attribute or as A sample set stream. The
latter has the advantage of keeping a history of power curves which can
be useful if the turbine is modified over the lifetime in a way that
changes the power-curve. In both cases the power-curve will be entered
manually using the Galore config tool.

The next step is to add a calculator node and a sample set node to the
asset model. The sample set node will represent the production forecast.

The calculator node will have a query that will take the predicted wind
from the weather forecast and use that to look up the expected power
output of the turbine. Each time a new weather forecast arrives in the
system it will trigger a new production forecast.

The inputs of the query (weather forecast and power-curve streams) will
be specified using relative paths in the asset model. This allow the
calculator to be added to the asset template an as such work on each
turbine it is applied to.

The next step is to produce a farm total production forecast. To do this
we add a new calculator and sample set node to the farm node in the
asset model

Now we specify a query on the calculator that will sum all the forecasts
from each individual turbine. Now the input of the query uses a path
with wildcards to select all the individual forecast streams and sum
them to a farm level forecasts stream.

2.  High bearing temperature warning

Now we add a calculator and event node to the bearing node in the asset
model

We add a query to the calculator that selects the temperature as input
and transforms it using a monitor operation. The monitor operation
allows us to create events when the input signal fulfils the given
condition expressions.

Implicit Derived streams and Drill Down
=======================================

The Galore asset model allows the creation of implicit streams for drill
down. An event stream will be propagated to its parent node and merged
with event streams from other child nodes. This allows implicit streams
of events to be formed on each node comprising all the event on all
subnodes. This allows the client application to easily extract events
related to a particular subsystem. This allows the end user to drill
down into the asset model and see only the relevant events.

For timeseries nodes and sampleset nodes a number time based aggregated
streams are created in addition to the raw data streams. This allows
very fast access to aggregate of large time periods. For events the
aggregate contains the number and type events in the period. For
measurements, the aggregate contains average, minimum, maximum and
number of measurements.

The time series viewer (A general tool used to explore galore
timeseries) relies heavily on this to provide a smooth interactive user
experience with fast interactive zooming.