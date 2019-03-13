# WhereToDoAPI
API for CIT368 Group Project 1 - WhereToDo

### Application Information
The API can be accessed at: https://wheretodoapi.azurewebsites.net/

The structure of this project was created with guidance provided by a Lynda.com class
"Building and Securing RESTful APIs in ASP.NET Core" by Nate Barbettini. 

Classes derived from this course are marked, along with unique classes specific to this project within a header comment for each file.

The primary locations of interest for this project are probably:
1. Controllers (Folder) 
   * UsersController.cs
   * ListController.cs
2. Services (Folder)
   * DefaultUserService.cs
   * DefaultListService.cs
----
### Primary API Actions
#### *User Actions*
1. Create a User Account: <br />
   **POST** to **_/users_** route with JSON body:
   ```json
   {
      "username": "testUsername",
      "password": "Password123!"
   }
   ```
2. Authenticate User for Login: <br />
   **POST** to **_/token_** route with 'x-www-form-urlencoded' KEY/VALUE pairs:
   
   | KEY          | VALUE         |
   | :-----       | :-----        |
   | grant_type   | password      |
   | username     | testUsername  |
   | password     | Password123!  |
   
   This will return something similar to:
   ```json
   {
      "scope": "roles",
      "token_type": "Bearer",
      "access_token": "token_value_string"
   }
   ```
   The *_token_value_string_* should be included within an Authorization header for all subsequent requests to the API. The header should follow the KEY/VALUE pair format of:
   
   | KEY             | VALUE                        |
   | :-----          | :-----                       | 
   | Authorization   | Bearer token_value_string    |
   
3. Modify User Account (Change Password): <br />
   **PATCH** to **_/users_** route with JSON body:
   ```json
   {
      "currentpassword": "Password123!",
      "newpassword": "Password456!"
   }
   ```

#### *Task (List) Actions*
1. Add a Task (List): <br />
   **POST** to **_/list_** route with JSON body:
   ```json
   {
      "title": "Sample List POST",
      "location": "GitHub README",
      "lat": "10.000",
      "long": "10.000"
   }
   ```
2. Modify a Task (List): <br />
   **PATCH** to **_/list/{listID}_** route with JSON body:
   ```json
   [
      { "op": "replace", "path": "/title", "value": "Modified Task/List Title" }
   ]
   ```
   This PATCH supports all PATCH operations. The example above modifies the title of the List. The same concept can be applied to /status to update the status of a particular List.
   
3. **DELETE** to **_/list{listID}_** route. <br />
   There is no body to this request. All that is needed is the Authorization header, the same as all other actions.
