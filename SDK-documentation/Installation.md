## Installation
To start the installation, run following command:
```
choco install poseidon-galore-dev-stack
```
You can also pass options and switches for customizing the installation. For example pass the ‘y’ switch (choco install poseidon-galore-dev-stack -y) will confirm all prompts asked during installation.
For complete list of default options and switches that can be passed, please follow below given link
https://chocolatey.org/docs/commands-reference#default-options-and-switches

### Additional Installation Steps
There is a dependency on “docker-for-windows” package. If in case this is not previously installed, chocolatey will install it and during installation a system reboot is required. At this point of time you will see a message in the console window as shown below:
 
You can now safely close the console and restart windows. After restart, run the following command to continue installation of core services:
```
choco install galorecoreservices
```
The reason for this restart is because on Docker installation following 3 system resources are updated which needs a restart of windows to take effect.
1.	Windows Registry - Docker uses it to track the installed software location for its internal use.
2.	User groups - Docker needs windows file system access for storing data files. Docker creates a user group for that purpose and adds current user to it. Docker internally uses this user group for various user related information.
3.	Environment variable - It is used for other tools (e.g. cmd.exe and powershell.exe) to find path of the Docker related executables. 

### Post-Installation
Galore core services is installed as a windows service and will start up automatically. To start Galore API, open a new command prompt and type “galoreapi” command and press enter. 
 
This will open a windows PowerShell window and start the galore API in a self-hosted environment.
 
To start PoseidonNext hosting environment type and run the following command in a separate window:
poseidon-dev-host --next –applications “C:\Program Files\kognifai\SDK\PoseidonNext”
 
PoseidonNext is now hosted on port 8080. You can now open a browser and navigate to http://localhost:8080/poseidon-home for the home page of PoseidonNext application and http://localhost:8080/galore-configtool for galore config tool.
 
### Installation Parameters
Apart from the default switches and options, you can also pass few configuration parameters along with the installation command as shown below:
choco install poseidon-galore-dev-stack --params="'/Authority:http://127.0.0.1:80/Security/auth'
If these parameters are not passed, the default values are set. The complete list of parameters that can be passed are given below

|Name|	Description|	Default Value
|-------------------------|---------------|--------
Authority|	Url of Platform authentication server that will issue tokens|	http://localhost:8080/Security/auth
ApiUrl|	Galore api url|	http://localhost:5050

