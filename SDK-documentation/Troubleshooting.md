## Known issues and Workarounds [![Gitter Join the chat](https://badges.gitter.im/Join%20Chat.svg)](https://gitter.im/kognifai/Lobby)


1)	Uninstallation of a Galore package fails, when Galore API or PoseidonNext hosting is in progress and you want to uninstall the Galore package. 
Please ensure to stop these hostings before running the uninstallation or upgrade command.

2)	PoseidonNext uses a dedicated 8080 port. Please ensure that no other application uses this port.

3)	Under exceptional conditions, you may want to upgrade PoseidonNext packages and not the entire stack.
For this, please run npm upgrade separately (to upgrade local modules, you have to run this command in C:\Program Files\kognifai\SDK\PoseidonNext folder).

## See Also
 
- [Dev stack Installation](Installation.md)
- [Prerequisites](Prerequisites.md)
- [Upgrading Dev Stack SDK](Upgrading%20Dev%20stack.md)
- [Stack Endpoint Values](Stack%20Endpoint%20Values.md)
- [Uninstallation](Uninstallation.md)
 
## Next Step
Find [Uninstallation](Uninstallation.md) to uninstall Galore.

## Previous Step
Find [Stack Endpoint Values](Stack%20Endpoint%20Values.md) step for information on Stack Endpoint Values.
