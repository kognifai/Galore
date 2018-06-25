## Dev Stack SDK [![Gitter Join the chat](https://badges.gitter.im/Join%20Chat.svg)](https://gitter.im/kognifai/Lobby)

Welcome to Galore SDK documentation. In this section, you will learn how to install, upgrade, troubleshoot Galore Dev Stack as well as you can know more about  Stack Endpoint values.

- [dev stack Overview](#Overview)

## See Also
- [Dev stack Installation](Installation.md)
- [Upgrading Dev stack SDK](Upgrading%20Dev%20stack.md)
- [Stack Endpoint Values](Stack%20Endpoint%20Values.md)
- [Troubleshooting](Troubleshooting.md)
- [Uninstallation](Uninstallation.md)

## Overview

The following diagram depicts a basic idea on the dev stack SDK.

![](Images/Dev%20stack%20SDK%20overview.png)

We use the chocolatey windows package manager (https://chocolatey.org/) to distribute the Meta package. The Meta package takes care of installing and configuring various components of the PoseidonNext and Galore.  The following are the Meta package composition:

Kognifai global node modules
-	poseidon-dev-host
-	generator-poseidon
-	Poseidon-app-registration-cli

Kognifai local node modules (installed in PoseidonNext/SDK folder)
- poseidon-home
- poseidon-user-profile
- poseidon-user-administration
- poseidon-test-pages
- galore-configtool

Chocolatey Packages
- galoreapi
- galorecoreservices

## Next Step

Next step is to explore the [Prerequisites](Prerequisites.md) for the installation.
