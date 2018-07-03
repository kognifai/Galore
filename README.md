  
# Galore    [![Gitter Join the chat](https://badges.gitter.im/Join%20Chat.svg)](https://gitter.im/kognifai/Lobby)

Welcome to Galore documentation!
 
The Galore documentation is organized into three sections for you to choose  your reading and  grab what you want from the Galore.


 ### SDK documentation
SDK documentation provides information on how to install, upgrade, troubleshoot the Galore Dev Stack(Chocolatey meta package) as well as provides information on Stack Endpoint values.
 
[Dev stack SDK](SDK-documentation/readme.md )

### Code Samples
WCF sample codes are availble in the following link for you to download and reuse them as per your requirements.

[Galore WCFSample code](GaloreWCFSample)

### Galore Documentation

Galore and its components are described in this  section. You can go over each one of them and understand Galore components that can help you to develop your own applications on Galore. 

  - [Galore Overview](#Overview)

  - [Galore Asset Model](Galore-Documentation/readme.md)

  - [Streams](Galore-Documentation/streams.md)

  - [TQL functional query language](Galore-Documentation/TQL%20Syntax.md)

  - [Node Selector](Galore-Documentation/Node%20Selector.md)
 
  - [Pipeline Operations](Galore-Documentation/Pipeline%20Operations.md)

  - [Case Study](Galore-Documentation/casestudy.md)
 


## Overview

Many Kognifai applications leverage Galore capabilities for storage because it is one of the storage system. Here are the major components of the Galore system:

  - [Asset model](Galore-Documentation/readme.md) - The Galore Asset Model is a Directed Acyclic Graph (DAG).
  
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
In order to get a better overview of the Galore capabilities in a simplified case study, click [here](Galore-Documentation/casestudy.md).

### License
Read the copyright information and terms and conditions for Usage and Development of the software [here](https://github.com/kognifai/Kognifai/blob/master/License.md#copyright--year-kongsberg-digital-as).

