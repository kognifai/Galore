
Galore Overview
===============

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


