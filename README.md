  
# Galore    [![Gitter Join the chat](https://badges.gitter.im/Join%20Chat.svg)](https://gitter.im/kognifai/Lobby)

Welcome to Galore documentation!

In this section, we talk about:

 [Galore Overview](#Overview)

  - [Galore Asset Model](Galore-Documentation/Asset%20%20Model.md)

  - [Streams](Galore-Documentation/streams.md)

  - [TQL functional query language](Galore-Documentation/TQL%20Syntax.md)

  - [Node Selector](Galore-Documentation/Node%20Selector.md)
 
  - [Pipeline Operations](Galore-Documentation/Pipeline%20Operations.md)

  - [Case Study](Galore-Documentation/casestudy.md)
  
[dev stack SDK](SDK-documentation/dev%20stack%20SDK.md)

[Galore WCFSample code](GaloreWCFSample)

## Overview

Many Kognifai applications leverage Galore capabilities for storage because it is one of the storage system. Here are the major components of the Galore system:

  - [Asset modelling](Galore-Documentation/Asset%20%20Model.md) - The Galore Asset Model is a Directed Acyclic Graph (DAG).
  
  - [Streams](Galore-Documentation/streams.md) - Galore uses a stream abstraction to allow access to both real-time and historical data.

    -   StateEvent streams and Alarms&Events.

    -   Sample set streams. See [Sample set event stream](Galore-Documentation/streams.md)    for more details.
    
-  [TQL functional query language](Galore-Documentation/TQL%20Syntax.md) - A query service that allows simple and complex (historical and real-time combined) queries using the TQL .TQL defines a functional pipeline. 

   -   A calculation service that allows simple and complex creation of new streams (with  history stored) using the TQL functional query language.

- [Node Selector](Galore-Documentation/Node%20Selector.md)- The node selector is an expression for selecting one or more nodes from the Galore.

 - [Pipeline Operations](Galore-Documentation/Pipeline%20Operations.md)- Operations that can be performed in pipeline are described in this topic.
 
- These services are accessible by end user applications and other services through REST APIs, Signal R and WCF services.

-   Client libraries (C# and JavaScript/TypeScript) can be leveraged by end user applications.

-   General Tools for configuring the system.

-   General Tools for exploring data, stored by the system.
 

### Case Study
In order to get a better overview of the Galore capabilities in a simplified case study, click [here](SDK-documentation/casestudy.md).

### License
Read the copyright information and terms and conditions for Usage and Development of the software [here](https://github.com/kognifai/Kognifai/blob/master/License.md#copyright--year-kongsberg-digital-as).

