## Dev Stack SDK [![Gitter Join the chat](https://badges.gitter.im/Join%20Chat.svg)](https://gitter.im/kognifai/Lobby)

Welcome to Galore SDK documentation!

In this section, you will learn how to install, upgrade, troubleshoot Galore Dev Stack. You can also know  more about  Stack Endpoint values.

- [Dev Stack Overview](#Overview)


## Overview

The following diagram depicts a basic idea of the dev Stack SDK. The following are the modules of Dev Stack SDK:

![](.%20Images/Dev%20stack%20SDK%20overview.png)

We use the Chocolatey package manager for windows (https://chocolatey.org/) to distribute the meta package. The meta package takes care of installing and configuring various components of the [PoseidonNext](https://github.com/kognifai/PoseidonNext-Framework) and [Galore](https://github.com/kognifai/Galore).  The following are the meta package composition:

Kognifai global node modules
-	poseidon-dev-host
-	generator-poseidon
-	Poseidon-app-registration-cli

Kognifai local node modules (installed in <INSTALL DIR>/kognifai/SDK/PoseidonNext)
- poseidon-home
- poseidon-user-profile
- poseidon-user-administration
- poseidon-test-pages
- galore-configtool

Chocolatey Packages
- galoreapi - This package installs a REST API which provides programmatic access to read and write operations of Galore. These includes access to various components such as asset model, streams, 
Running TQL queries, CRUD operations on Nodes and other pipeline operations.
- galorecoreservices - This package installs a set of Galore WCF windows services. These services act as an interface between various clients and Galore store (Couchbase) and handle operations at a lower level


## See Also
- [Dev stack Installation](Installation.md)
- [Upgrading Dev stack SDK](Upgrading%20Dev%20stack.md)
- [Stack Endpoint Values](Stack%20Endpoint%20Values.md)
- [Troubleshooting](Troubleshooting.md)
- [Uninstallation](Uninstallation.md)

## License
Read the copyright information and terms and conditions for Usage and Development of the software [here](https://github.com/kognifai/Kognifai/blob/master/License.md#copyright--year-kongsberg-digital-as).

## Next Step

Find [Prerequisites](Prerequisites.md) for the Galore installation.
