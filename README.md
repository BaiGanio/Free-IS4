
# Identity Server 4 (IDS4)
- Official documentation for implementing the IDS4 from scratch:  
  - [docs.identityserver.io](http://docs.identityserver.io/en/release/quickstarts/0_overview.html)
- Our documentation for additional setup could be found in our project [`WIKI`](https://github.com/BaiGanio/IdentityServer4/wiki)

**This project is a stateless WebApi-based service.**

**It is used as an authority for issuing and validating tokens for client applications and for users.**

    This service provides protected (authorized) access to the `bgapi` web project.
    It uses InMemory Client definitions and Scopes.
    The IDS4 protect the request and validates them by jwt tokens.    
- `ids4` site: [https://ids4core20.azurewebsites.net](https://ids4core20.azurewebsites.net)
- `discovery endpoint`:[ https://ids4core20.azurewebsites.net/.well-known/openid-configuration]( https://ids4core20.azurewebsites.net/.well-known/openid-configuration)
***

# IDS4 QuickStart
**`Local Development Environment Setup`**
  - Open `IdentityServer4`solution with `Visual Studio 2017 Community` or `Visul Studio Code` as well.
  - In `ids4core20` Web API project look for `PublishProfiles`. Then open `launchSettings.json` file. 
  - Be sure that `applicationUrl` points to `http://localhost:5000/` - this is the default port on wich the local server is listening. 
  
  ![image](https://raw.githubusercontent.com/BaiGanio/IdentityServer4/master/Docs/1/initial-setup.png)
  
  - Build your solution with key combo `Ctrl` + `Shift` + `B`. It should compile with no errors.
  - Start the project by pressing `Ctrl` + `F5`. Web project will open automatically on `http://localhost:5000/` in your default browser.
  - Also you can benefit from secure `https://localhost:44350/` connection which is set as configuration in `launchSettings.json` 
  
  ![image](https://raw.githubusercontent.com/BaiGanio/IdentityServer4/master/Docs/1/ids4-secure.png)
  
  - You can always check IDS metadata by adding `.well-known/openid-configuration` after your project url. 
  - In this particular case it should look like 
    - `http://localhost:5000/.well-known/openid-configuration` 
    - or
    - `https://localhost:44350/.well-known/openid-configuration`:
  
  ![image](https://raw.githubusercontent.com/BaiGanio/IdentityServer4/master/Docs/1/ids4-metadata.png)
  
  - To verify that your local `ids4` setup is working use `Postman`
    - In ids4core20 Web API project look for Client.cs file. Be sure to look for `Local` region. Search for `private static Client LocalClient`. We aware of this one. We need his values to be able to reqest the IDS4.
    - Open `Postman` client and select POST
    - Fill...
    - Result
    
  ![image](https://raw.githubusercontent.com/BaiGanio/IdentityServer4/dev/Docs/1/postman-local-client-verify.png)
    
