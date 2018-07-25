
## Prerequisites

Refer [Prerequisites](Prerequisites.md) to know more about how to install the Chocolatey package, and how to set the  KDI Jfrog as one of the npm source before you begin the Dev Stack installation.


## Dev Stack Installation    [![Gitter Join the chat](https://badges.gitter.im/Join%20Chat.svg)](https://gitter.im/kognifai/Lobby)

Run the following command to start the Galore Dev Stack installation:
```
choco install poseidon-galore-dev-stack
```
You can also pass options and switches to customize the installation. For example, passing the ‘y’ switch (choco install poseidon-galore-dev-stack -y) confirms all the prompts during installation.

Refer the following link for a complete list of default options and switches:

https://chocolatey.org/docs/commands-reference#default-options-and-switches

### Additional Installation Steps

There is a dependency on **docker-for-windows** package. If this is not previously installed, chocolatey will install it and you must restart your system after the installation. At this point of time, the following message is displayed in the console window.
 
 ![](.%20Images/MicrosoftTeams-image.png)
 
 > Note: If you encounter any Docker error after restarting your system, see [If Docker Doesn't Work](Troubleshooting.md) to resolve it.

> If the Dev Stack SDK doesn't work, see [When SDK doesn't work but the Docker is running with no issues](Troubleshooting.md) to resolve it.
 
You can now safely close the console and restart Windows. After restart, run the following command to continue the core services installation:
```
choco install galorecoreservices
```

Following are the three system resources which get updated as a result of the Docker installation. You must restart your system after the Docker installation.

1.	**Windows Registry**- Docker uses the Windows registry to track the location of the installed software. This is for its internal use.
2.	**User groups**- Docker needs access to Windows file system for storing data files. Docker creates a user group for that purpose and adds a current user to it. Docker internally uses this user group for various user related information.
3.	**Environment variable**- This is used for other tools (e.g. cmd.exe and powershell.exe) to find path of the Docker related executables. 

### Post-installation
Galore core service is installed as a windows service and it automatically starts after installation. 

To start a Galore API 
- Open a new command prompt and type “galoreapi” command and press **Enter**. 

![](.%20Images/2018-06-22%2017_22_38-Administrator_%20Command%20Prompt.png)
 
This opens Windows PowerShell and starts the Galore API in a self-hosted environment.

![](.%20Images/2018-06-22%2017_23_33-Administrator_%20C__WINDOWS_System32_WindowsPowerShell_v1.0_powershell.exe.png )
 
To start PoseidonNext hosting environment, run the following command in a separate window:
```
poseidon-dev-host --applications "C:\Program Files\kognifai\SDK\PoseidonNext"
```
![](.%20Images/2018-06-22%2017_25_34-Cmder.png)

PoseidonNext is now hosted on port 8080. You can open a browser and navigate to http://localhost:8080/poseidon-home for the home page of PoseidonNext application and http://localhost:8080/galore-configtool for Galore config tool.

![](.%20Images/Poseidon%20Next.png)
 
### Installation Parameters

Apart from the default switches and options, you can also pass few configuration parameters along with the installation command as shown below:

```
choco install poseidon-galore-dev-stack --params="'/Authority:http://127.0.0.1:80/Security/auth'
```
If these parameters are not passed, the default values are set. The complete list of parameters that can be passed are listed in the following table:

|Name|	Description|	Default Value
|-------------------------|---------------|--------
Authority|	URL of the Platform authentication server that issues tokens|	http://localhost:8080/Security/auth
ApiUrl|	Galore API URL|	http://localhost:5050



## Key points you should consider after Galore Dev Stack installation

1.	Always **login as an administrator user** to perform write operations (e.g. creating asset nodes, events and etc.)

2. For more information on "Resolving problems with the SDK", see [Troubleshooting](Troubleshooting.md) specifically Known issues 1 and 2.


## See Also
 
- [Prerequisites](Prerequisites.md)

- [Upgrading Dev Stack SDK](Upgrading%20Dev%20stack.md)

- [Stack Endpoint Values](Stack%20Endpoint%20Values.md)

- [Troubleshooting](Troubleshooting.md)

- [Uninstallation](Uninstallation.md)
 


## Next Step

Find [Upgrading](Upgrading%20Dev%20stack.md) for the Galore installation.

## Previous Step

Find [Prerequisites](Prerequisites.md) for more information on Prerequisites.
