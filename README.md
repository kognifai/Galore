

# Galore

Welcome to the Galore!

This open source project contains sample codes, documentation for using Galore.

# Overview

Galore is one of the storage systems that can be leveraged by kognifai
applications. Galore is native to Kognifai and is used by many of the
Kognifai applications provided by Kongsberg Digital. The major
components of the Galore system are:

-   A number of services for storing, retrieving and processing data. The most
    important services are:

    -   Asset modelling

    -   Measurement aka vector streams aka timeseries

    -   StateEvent streams aka Alarms&Events

    -   Sample set streams.

    -   We will explore the capabilities of these services in more
        detail later.
-   A query service that allows simple and complex combined historical and realtime queries using the TQL functional query language.

-   A calculation service that allows simple and complex creation of new streams with stored history using the TQL functional query language.

-   The services are accessible by end user applications and other
    services through REST apis, Signal R and as WCF services.

-   Client libraries (C\# and javascript/typescript). These can be
    leveraged

-   General tools for configuring the system

-   General tools for exploring data stored by the system


# Galore Asset Model
To get started developing your first application using Galore read the Galore Asset model content and use the sample codes given in the following link .

[Galore Asset Model](https://github.com/kognifai/Galore/blob/master/SDK-documentation/TQL.md)

# Case Study
In order to get a better overview of the galore capabilities, we will explore it in a simplified case study.

[Case Study](https://github.com/kognifai/Galore/blob/master/SDK-documentation/casestudy.md)

# License
Read the copyright information and terms and conditions for Usage and Development of the software [here](https://github.com/kognifai/Kognifai/blob/master/License.md#copyright--year-kongsberg-digital-as).
