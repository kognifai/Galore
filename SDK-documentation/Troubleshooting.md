## Known issues and Workarounds [![Gitter Join the chat](https://badges.gitter.im/Join%20Chat.svg)](https://gitter.im/kognifai/Lobby)


1)	Uninstallation of a package fails, when Galore API or PoseidonNext hosting is in progress and you want to uninstall it. 
Please ensure to stop these hostings before running the uninstallation or upgrade command.

2)	PoseidonNext uses a dedicated 8080 port. Please ensure that no other application uses this port.

3)	Under exceptional conditions, you may want to upgrade PoseidonNext packages and not the entire stack.
For this, please run npm upgrade separately (to upgrade local modules, you have to run this command in C:\Program Files\kognifai\SDK\PoseidonNext folder).

## Next Step
Find [Uninstallation](Uninstallation.md) to uninstall Galore.
