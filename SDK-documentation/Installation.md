
## Dev stack Installation    [![Gitter Join the chat](https://badges.gitter.im/Join%20Chat.svg)](https://gitter.im/kognifai/Lobby)

Run the following command to start the Galore Dev Stack installation:
```
choco install poseidon-galore-dev-stack
```
You can also pass options and switches for customizing the installation. For example, passing the ‘y’ switch (choco install poseidon-galore-dev-stack -y) confirms all the prompts during installation.

Refer the following link for a complete list of default options and switches:

https://chocolatey.org/docs/commands-reference#default-options-and-switches

### Additional Installation Steps

There is a dependency on **docker-for-windows** package. If this is not previously installed, chocolatey will install it and during the installation a system reboot is required. At this point of time, the following message is displayed in the console window:
 
 ![](.%20Images/MicrosoftTeams-image.png)
 
You can now safely close the console and restart windows. After restart, run the following command to continue the installation of core services:
```
choco install galorecoreservices
```
After Docker installation, three system resources are updated which needs windows restart to take effect.

1.	Windows Registry - Docker uses the Windows registry to track the installed software location for its internal use.
2.	User groups - Docker needs windows file system access for storing data files. Docker creates a user group for that purpose and adds current user to it. Docker internally uses this user group for various user related information.
3.	Environment variable - It is used for other tools (e.g. cmd.exe and powershell.exe) to find path of the Docker related executables. 

### Post-Installation
Galore core service is installed as a windows service and it starts automatically. To start a Galore API, open a new command prompt and type “galoreapi” command and press **enter**. 

![](.%20Images/2018-06-22%2017_22_38-Administrator_%20Command%20Prompt.png)
 
This opens a windows PowerShell window and starts the Galore API in a self-hosted environment.

![](.%20Images/2018-06-22%2017_23_33-Administrator_%20C__WINDOWS_System32_WindowsPowerShell_v1.0_powershell.exe.png )
 
To start PoseidonNext hosting environment, run the following command in a separate window:
```
poseidon-dev-host --applications "C:\Program Files\kognifai\SDK\PoseidonNext"
```
![](.%20Images/2018-06-22%2017_25_34-Cmder.png)

PoseidonNext is now hosted on port 8080. You can now open a browser and navigate to http://localhost:8080/poseidon-home for the home page of PoseidonNext application and http://localhost:8080/galore-configtool for Galore config tool.

![](.%20Images/Poseidon%20Next.png)
 
### Installation Parameters

Apart from the default switches and options, you can also pass few configuration parameters along with the installation command as following:

```
choco install poseidon-galore-dev-stack --params="'/Authority:http://127.0.0.1:80/Security/auth'
```
If these parameters are not passed, the default values are set. The complete list of parameters that can be passed are listed as following:

|Name|	Description|	Default Value
|-------------------------|---------------|--------
Authority|	URL of the Platform authentication server that issues tokens|	http://localhost:8080/Security/auth
ApiUrl|	Galore API URL|	http://localhost:5050


## Next Step

Find [Upgrading](Upgrading%20Dev%20stack.md) for the Galore installation.
