
# Free-IS4 is an Identity Server 4 middleware implementation on top of .NET 6 framework 
[![Codacy Badge](https://app.codacy.com/project/badge/Grade/6e3be1117d8e49a0a555790b78cf64af)](https://www.codacy.com/gh/BaiGanio/Free-IS4/dashboard?utm_source=github.com&amp;utm_medium=referral&amp;utm_content=BaiGanio/Free-IS4&amp;utm_campaign=Badge_Grade)
- Official documentation for implementing Identity Server 4 middleware from scratch:  
  - [docs.identityserver.io](http://docs.identityserver.io/en/release/quickstarts/0_overview.html)
- Our documentation for additional setup could be found in our project [`WIKI`](https://github.com/BaiGanio/IS4/wiki)

**This project is a stateless WebApi-based service.**

**It is used as an authority for issuing and validating tokens for client applications and for users.**

    This service provides protected (authorized) access to the `bgapi` web project.
    It uses InMemory Client definitions and Scopes.
    The Free-IS4 protect the request and validates them by jwt tokens.    
- `ids4` site: [https://free-is4.azurewebsites.net](https://free-is4.azurewebsites.net)
- `discovery endpoint`:[ https://free-is4.azurewebsites.net/.well-known/openid-configuration]( https://free-is4.azurewebsites.net/.well-known/openid-configuration)
***

# `Local Setup`
****
  - Open `IdentityServer4`solution with `Visual Studio 2017 Community` or `Visul Studio Code` as well.
  - In `IS4` Web API project look for `PublishProfiles`. Then open `launchSettings.json` file. 
  - Be sure that `applicationUrl` points to `http://localhost:5000/` - this is the default port on wich the local server is listening. 
  
  ![image](https://raw.githubusercontent.com/BaiGanio/IS4/master/readme/initial-setup.png)
  
  - Build your solution with key combo `Ctrl` + `Shift` + `B`. It should compile with no errors.
  - Start the project by pressing `Ctrl` + `F5`. Web project will open automatically on `http://localhost:5000/` in your default browser.
  - Also you can benefit from secure `https://localhost:44350/` connection which is set as configuration in `launchSettings.json` 
  
  ![image](https://raw.githubusercontent.com/BaiGanio/IS4/master/readme/ids4-secure.png)
  
  - You can always check IDS metadata by adding `.well-known/openid-configuration` after your project url. 
  - In this particular case it should look like 
    - `http://localhost:5000/.well-known/openid-configuration` 
    - or
    - `https://localhost:44350/.well-known/openid-configuration`:
  
  ![image](https://raw.githubusercontent.com/BaiGanio/IS4/master/readme/ids4-metadata.png)
  
  - To verify that your local `ids4` setup is working use `Postman`
    - In ids4core20 Web API project look for Client.cs file. Be sure to look for `Local` region. Search for `private static Client LocalClient`. We aware of this one. We need his values to be able to reqest the IDS4.
    - Open `Postman` client and select POST
    - Fill...
    - Result
    
  ![image](https://raw.githubusercontent.com/BaiGanio/IS4/master/readme/postman-local-client-verify.png)  
