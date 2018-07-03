## Prerequisites [![Gitter Join the chat](https://badges.gitter.im/Join%20Chat.svg)](https://gitter.im/kognifai/Lobby)

Following are the prerequisites of Galore Dev Stack Installation:
### 1. Chocolatey Package ###
Install Chocolatey from its official website https://chocolatey.org/install

### 2.	Enable Hyper-V and containers ###
Galore uses Couchbase as the data store which is hosted on the Docker. Hyper-V must be enabled for installing and running the Docker. 

The following links provide more information and quick tips on how to:

 [Install Hyper-V on Windows 10](https://docs.microsoft.com/en-us/virtualization/hyper-v-on-windows/quick-start/enable-hyper-v) and 
 [Manually enable Docker for Windows prerequisites](https://success.docker.com/article/manually-enable-docker-for-windows-prerequisites)

### 3. npm JFrog source ###
Some of the npm packages in the stack are currently  available only in the private **KDI JFrog** repository. This is expected to change.
The node package manager needs to be configured with JFrog as a source. Follow these instructions to set this up.
[npm source jfrog](https://kognifai.visualstudio.com/Kognifai%20Core/_wiki/wikis/PoseidonNext.wiki?wikiVersion=GBwikiMaster&pagePath=%2FPoseidon%20developers%2FDeveloper%20guides%2FJFrog%3A%20Configure%20NPM%20to%20use%20KDI%20JFrog)

### 4. KDI JFrog chocolatey repository ###
All the chocolatey packages are available in the private **KDI JFrog** repository. 
For chocolatey to properly discover and install these, we must add this repository as one of the sources for chocolatey. 

To accomplish this, run the following command in command prompt/Powershell under elevated privileges (Run as administrator).
```
choco source add -n=<NAME> -s="https://kdi.jfrog.io/kdi/api/nuget/chocolatey-local" -u="<USERNAME>" -p=<PASSWORD> --priority=2
```
Where, ```<NAME>``` refers to any unique name for example "kdi-choco" etc.

If you already have a KDI jfrog user credential then enter the username in the ```<USERNAME>``` field and encrypted password in the <PASSWORD> field. 

- Note - -To get the encrypted password, log in to **KDI jfrog** and go to **User Profile** 
          -Click on **Authentication Settings**.  
          - Copy and paste the value of field called “Encrypted Password."

![](.%20Images/2018-06-21%2018_53_06-kdi.png)

If you have KDI Integration AD, enter your email address in the ```<USERNAME>``` field and API key in the ```<PASSWORD>``` field. 

- Note - -  To get the API key, log in to KDI jfrog using the AD account. 
           -Go to **User Profile** section and copy and paste the value for the field called “API Key."

![](.%20Images/2018-06-22%2017_19_05-kdi.png)

## Next Step

Once you are ready for the dev stack installation, refer [Installation](Installation.md) procedures.
