## dev stack SDK Overview

The following diagram depicts a basic idea on the dev stack SDK.

We use the chocolatey windows package manager (https://chocolatey.org/) to distribute the Meta package. The Meta package takes care of installing and configuring various components of the PoseidonNext and Galore.  The following are the meta-package composition:

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

## See Also
- [Prerequisites](Prerequisites.md)
- [Dev stack Installation](Installation.md)
- [Uninstallation](Uninstallation.md)
- [Upgrading Dev stack SDK](Upgrading Dev stack SDK.md)
- [Stack Endpoint Values](Stack Endpoint Values.md)
- [Troubleshooting](Troubleshooting.md)
