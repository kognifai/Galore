## Dev stack Installation
To start the installation, run the following command:
```
choco install poseidon-galore-dev-stack
```
You can also pass options and switches for customizing the installation. For example, pass the ‘y’ switch (choco install poseidon-galore-dev-stack -y) which will confirm all the prompts asked during installation.

Refer the following link for a complete list of default options and switches:

https://chocolatey.org/docs/commands-reference#default-options-and-switches

### Additional Installation Steps

There is a dependency on **docker-for-windows** package. If in case, this is not previously installed, chocolatey will install it and during the installation a system reboot is required. At this point of time, you see the following message in the console window:
 
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
 
This opens a windows PowerShell window and starts the Galore API in a self-hosted environment.
 
To start PoseidonNext, host the environment type and run the following command in a separate window:
```
poseidon-dev-host --next –applications “C:\Program Files\kognifai\SDK\PoseidonNext”
```
PoseidonNext is now hosted on port 8080. You can now open a browser and navigate to http://localhost:8080/poseidon-home for the home page of PoseidonNext application and http://localhost:8080/galore-configtool for Galore config tool.
 
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

