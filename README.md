
# Galore

Welcome to the Galore!

This open source project contains sample codes, documentation for you to get started with Galore.

# Overview

Galore is one of the storage systems that can be leveraged by Kognifai
applications. Galore is native to Kognifai and is used by many of the
Kognifai applications provided by Kongsberg Digital. The major
components of the Galore system are:

-   A number of services for storing, retrieving and processing data. The most
    important services are:

    -   Asset modelling

    -   Measurement aka vector streams aka timeseries

    -   StateEvent streams aka Alarms&Events

    -   Sample set streams.

    -   We will explore the capabilities of these services in more detail later.
    
-   A query service that allows simple and complex combined historical and realtime queries using the TQL functional query language.

-   A calculation service that allows simple and complex creation of new streams with stored history using the TQL functional query language.

-   The services are accessible by end user applications and other
    services through REST APIs, Signal R and as WCF services.

-   Client libraries (C\# and javascript/typescript). These can be leveraged

-   General tools for configuring the system

-   General tools for exploring data stored by the system


# Galore Asset Model
To start developing your application using Galore, read the topics in the  following table:

| Topic | Link | Description | 
|------|----------|----------|
 Galore Asset Model | [Galore Asset Model](https://github.com/kognifai/Galore/blob/master/SDK-documentation/TQL.md)|The Galore asset model is a Directed Acyclic Graph (DAG).|
Streams|[Streams](https://github.com/kognifai/Galore/blob/master/SDK-documentation/streams.md)|Galore uses a stream abstraction to allow access to both realtime and historical data in the same way.|
 TQL Syntax| [TQL Syntax](https://github.com/kognifai/Galore/blob/master/SDK-documentation/TQL%20Syntax.md)|TQL defines a functional pipeline. |
  Node Selector | [Node Selector](https://github.com/kognifai/Galore/blob/master/SDK-documentation/Node%20Selector.md)|The node selector is an expression for selecting one or more nodes from the Galore. |
 Pipeline Operations | [Pipeline Operations](https://github.com/kognifai/Galore/blob/master/SDK-documentation/Pipeline%20Operations.md)| Operations that can be performed in pipeline are described in this topic.


# Case Study
In order to get a better overview of the Galore capabilities in a simplified case study, click [here](https://github.com/kognifai/Galore/blob/master/SDK-documentation/casestudy.md).


# License
Read the copyright information and terms and conditions for Usage and Development of the software [here](https://github.com/kognifai/Kognifai/blob/master/License.md#copyright--year-kongsberg-digital-as).
