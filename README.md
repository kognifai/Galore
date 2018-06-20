  
# Galore    [![Gitter Join the chat](https://badges.gitter.im/Join%20Chat.svg)](https://gitter.im/kognifai/Lobby)

Welcome to Galore documentation!

In this section, we talk about:

 [Galore_Overview](#Overview)

  - [Galore Asset Model](https://github.com/kognifai/Galore/blob/master/SDK-documentation/TQL.md)

  - [Streams](https://github.com/kognifai/Galore/blob/master/SDK-documentation/streams.md)

  - [TQL functional query language](https://github.com/kognifai/Galore/blob/master/SDK-documentation/TQL%20Syntax.md)

  - [Node Selector](https://github.com/kognifai/Galore/blob/master/SDK-documentation/Node%20Selector.md)
 
  - [Pipeline Operations](https://github.com/kognifai/Galore/blob/master/SDK-documentation/Pipeline%20Operations.md)

  - [Case Study](https://github.com/kognifai/Galore/blob/master/SDK-documentation/casestudy.md)
  
[GaloreWCFSample code](https://github.com/kognifai/Galore/tree/master/GaloreWCFSample)

## Overview

Many Kognifai applications leverage Galore capabilities for storage because it is one of the storage system. Here are the major components of the Galore system:

  - [Asset modelling](https://github.com/kognifai/Galore/blob/master/SDK-documentation/TQL.md) - The Galore Asset Model is a Directed Acyclic Graph (DAG).
  
  - [Streams](https://github.com/kognifai/Galore/blob/master/SDK-documentation/streams.md) - Galore uses a stream abstraction to allow access to both real-time and historical data..

    -   StateEvent streams and Alarms&Events.

    -   Sample set streams. See [Sample set event stream](https://github.com/kognifai/Galore/blob/master/SDK-documentation/streams.md)           for more details.
    
-  [TQL functional query language](https://github.com/kognifai/Galore/blob/master/SDK-documentation/TQL%20Syntax.md) - A query service that allows simple and complex (historical and real-time combined) queries using the TQL .TQL defines a functional pipeline. 

   -   A calculation service that allows simple and complex creation of new streams (with  history stored) using the TQL functional query language.

- [Node Selector](https://github.com/kognifai/Galore/blob/master/SDK-documentation/Node%20Selector.md)- The node selector is an expression for selecting one or more nodes from the Galore.

 - [Pipeline Operations](https://github.com/kognifai/Galore/blob/master/SDK-documentation/Pipeline%20Operations.md)- Operations that can be performed in pipeline are described in this topic.
 
- These services are accessible by end user applications and other services through REST APIs, Signal R and WCF services.

-   Client libraries (C# and javascript/typescript) can be leveraged by end user applications.

-   General Tools for configuring the system.

-   General Tools for exploring data, stored by the system.
 
 ### Galore WCF Sample Code
[Here](https://github.com/kognifai/Galore/blob/master/SDK-documentation/GaloreWCFSample.md) is the short description and what to expect from the  WCF Sample codes. 
 

### Case Study
In order to get a better overview of the Galore capabilities in a simplified case study, click [here](https://github.com/kognifai/Galore/blob/master/SDK-documentation/casestudy.md).

### License
Read the copyright information and terms and conditions for Usage and Development of the software [here](https://github.com/kognifai/Kognifai/blob/master/License.md#copyright--year-kongsberg-digital-as).

