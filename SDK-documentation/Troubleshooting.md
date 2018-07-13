## Known issues and Workarounds [![Gitter Join the chat](https://badges.gitter.im/Join%20Chat.svg)](https://gitter.im/kognifai/Lobby)

### 1) If Docker doesn't work

After Galore Dev stack installation, if you encounter issues with Docker then do the following steps to run Docker automatically every time when you login:

 To start Docker automatically when you log in:
   
  1.	Right-click on the Docker and select **Settings**.
  
   The Docker Settings page is displayed.
  
  2.	From the Docker Settings page, Under **General**, select the **Start Docker when you log in** check box.
   
   The Docker starts every time when you start or restart your computer.

### 2) When SDK doesn't work but the Docker is running with no issues
If your Docker is running and still issues persist with Galore Dev Stack SDK, do the following steps:
   -	Start Docker container
   -	Start or restart Galore services

 To start the Docker container and start the Galore services:
   
 1.	Run the following command in Command Prompt or PowerShell to start the Docker container which contains **courchbase-galore**  image.
    
  ```powershell
      docker start db500
   ```
  
 2. Right-click on  the following services to start or restart the **Galore services** manually from Services Manager.
   
  - 	Kognifai Galore Config Services 
  -	 Kognifai Galore Core Services
  -	 Kognifai Galore LogRelay Services
    
  
### 3)	Uninstallation of a Galore package fails, when Galore API or PoseidonNext hosting is in progress and you want to uninstall the Galore package. 
Please ensure to stop these hostings before running the uninstallation or upgrade command.

### 4)	PoseidonNext uses a dedicated 8080 port. Please ensure that no other application uses this port.

### 5)	Under exceptional conditions, you may want to upgrade PoseidonNext packages and not the entire stack.
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
