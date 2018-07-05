

## Dev Stack SDK [![Gitter Join the chat](https://badges.gitter.im/Join%20Chat.svg)](https://gitter.im/kognifai/Lobby)

Welcome to Galore Dev Stack SDK documentation!

In this section, you will learn how to install, upgrade, and troubleshoot Galore Dev Stack. You can also know  more about  Stack Endpoint values.

- [Dev Stack Overview](#Overview)


## Overview

The following diagram depicts a basic idea of the Dev Stack SDK.

![](.%20Images/Dev%20stack%20SDK%20overview.png)

We use the Chocolatey package manager for windows (https://chocolatey.org/) to distribute the meta package. The meta package takes care of installing and configuring various components of the [PoseidonNext](https://github.com/kognifai/PoseidonNext-Framework) and [Galore](https://github.com/kognifai/Galore).  The following are the meta package composition:

Kognifai global node modules
-	poseidon-dev-host - A command line utility that provides hosting, API, and identity provider services for developers. For more information on poseidon-dev-host, refer the [PoseidonNext Developers Getting Started page](https://github.com/kognifai/PoseidonNext-Framework/blob/master/Developers-Getting-Started.md).

-	generator-poseidon -  Yeoman generator for scaffolding PoseidonNext applications or Data Adapters.  For more information on generator-poseidon, refer the [PoseidonNext Developers Getting Started page](https://github.com/kognifai/PoseidonNext-Framework/blob/master/Developers-Getting-Started.md).
-	Poseidon-app-registration-cli - A command line utility that helps you with the registration of the applications and data adapters. For more information on Poseidon-app-registration-cli, refer the [Application and data adapter registration tool](https://github.com/kognifai/PoseidonNext-Framework/blob/master/PoseidonNext-documentation/Guides/CLI-tool-for-registering-apps-and-data-adapters.md).

Kognifai local node modules (installed in ```<INSTALL DIR>```/kognifai/SDK/PoseidonNext)
- poseidon-home - This is the home page for the sample applications.
- poseidon-user-profile - This displays the user profile information which can be updated.
- poseidon-user-administration - This is a user management application for the identity provider. 
- poseidon-test-pages - These are the sample pages which showcase different services and integrations in PoseidonNext.
- galore-configtool - This is an angular application which plays a key role in this eco-system and allows users to view and configure various assets for which data needs to be stored and retrieved. 

Chocolatey Packages
- galoreapi - This package installs a REST API which provides programmatic access to read and write operations of Galore. These includes access to various components such as asset model, streams, Running TQL queries, CRUD operations on Nodes and other pipeline operations.
- galorecoreservices - This package installs a set of Galore WCF windows services. These services act as an interface between various clients and Galore store (Couchbase) and handle operations at a lower level


## See Also
- [Dev stack Installation](Installation.md)
- [Upgrading Dev stack SDK](Upgrading%20Dev%20stack.md)
- [Stack Endpoint Values](Stack%20Endpoint%20Values.md)
- [Troubleshooting](Troubleshooting.md)
- [Uninstallation](Uninstallation.md)
- [PoseidonNext Developers Getting Started page](https://github.com/kognifai/PoseidonNext-Framework#developers-getting-started-page)


## Next Step

Find [Prerequisites](Prerequisites.md) for the Galore installation.

## License
Read the copyright information and terms and conditions for Usage and Development of the software [here](https://github.com/kognifai/Kognifai/blob/master/License.md#copyright--year-kongsberg-digital-as).
