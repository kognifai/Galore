## Prerequisites [![Gitter Join the chat](https://badges.gitter.im/Join%20Chat.svg)](https://gitter.im/kognifai/Lobby)

Following are the prerequisites of installing Galore Dev Stack:
1.  Install Chocolatey (https://chocolatey.org/install)
2.	Enable **Hyper-V** and **containers**. 
    Galore uses Couchbase as the data store which is hosted on Docker and for installing and running Docker, Hyper-V must be enabled. 
    The following links provide more information and quick tips on how to:
     -  [Install Hyper-V on Windows 10](https://docs.microsoft.com/en-us/virtualization/hyper-v-on-windows/quick-start/enable-hyper-v)
     -  [Manually enable Docker for Windows prerequisites](https://success.docker.com/article/manually-enable-docker-for-windows-prerequisites)
3.	Galore config tool is currently available from [KDI jfrog npm] repository. If the repository is not setup, please refer the following link:
https://kognifai.visualstudio.com/Kognifai%20Core/_wiki/wikis/PoseidonNext.wiki?wikiVersion=GBwikiMaster&pagePath=%2FPoseidon%20developers%2FDeveloper%20guides%2FJFrog%3A%20Configure%20NPM%20to%20use%20KDI%20JFrog

4.	All the chocolatey packages are available from the private **KDI jfrog** repository. 
For chocolatey to properly discover and install these, we must add this repository as one of the sources for chocolatey. 
To accomplish this, the following command must be run in command prompt/Powershell under elevated privileges (Run as administrator).
```
choco source add -n=<NAME> -s="https://kdi.jfrog.io/kdi/api/nuget/chocolatey-local" -u="<USERNAME>" -p=<PASSWORD> --priority=2
```
Where, ```<NAME>``` can be any unique name like kdi-choco and so on.
If a KDI jfrog username and password are provided to you then enter the provided username in the ```<USERNAME>``` placeholder and encrypted password in the <PASSWORD> placeholder. 
To get the encrypted password, log in to **KDI jfrog** and go to **User Profile** and then **Authentication Settings**. 
Copy and paste the value of field called “Encrypted Password."

![](Images/2018-06-21%2018_53_06-kdi.png)

If you have KDI Integration AD, enter your email address in the ```<USERNAME>``` placeholder and API key in the ```<PASSWORD>```  placeholder. 
To get the API key, log in to KDI jfrog using the AD account. 
Go to **User Profile** section and copy and paste the value for the field called “API Key."

![](Images/2018-06-22%2017_19_05-kdi.png)

## Next Step

Once you are ready for the dev stack installation, refer [Installation](Installation.md) procedures.
