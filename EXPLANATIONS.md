# Fort

1.	Created Database and tables in SQL Server.
2.	Created empty Asp.Net Core web API project
3.	Using  Scaffold-DbContext command added DB context classes
    Added coneection string in <b>appsettings.json</b> file 
4.	Installed required NuGet packages
5.	Added Classes and Controllers based on requirement
6.	For API testing  we used <a href='https://learning.postman.com/docs/sending-requests/requests/'><b> POSTMAN </b> </a>  App.
7.	In Master folder  we attached  <b>DB Backup file </b>, <b>Project  Source code </b> and  <b>Explanation.pdf</b> file.
8.	Step by step with screenshots explained in <a href='https://github.com/sheksha001/Fort/blob/master/EXPLANATIONS.pdf'> EXPLANATIONS.pdf</a> file.

<span style="color: #ff0000; font-weight: bold;">Note : </span>In my system Docker not installing. it's showing <a href='https://github.com/sheksha001/Fort/blob/master/Docker_InstallitionError.jpeg'>error</a>. That's why API project created and executed in local.
Please follow below steps for API Source code open and  execution

<b>Code Execution Process: </b>
    
1. Download project from <a href='https://github.com/sheksha001/Fort'> GitHub </a>
2. Download and restore <a href='https://github.com/sheksha001/Fort/blob/master/FortCodeDB.bak'> DB backup file </a> in SQL SERVER-2019
3. Open Project in Visual Studio-2019 and modify ConnectionStrings <a hrf='https://github.com/sheksha001/Fort/blob/master/Fort/appsettings.json'><b>appsettings.json</b></a> file 
       
       ex: ConnectionStrings": {"FortDB": "Server=VALI-PC\\SQLEXPRESS;Database=FortCode;UID=sa;PWD=123;"}
       
4. Now Clck 'Build' and 'Debug' (IIS Express) then empty page will be open in web broser (https://localhost:44362/)
5. Now Open <b>PostMan App </b> and Select Request Type, Add URL, Body and Header
     
        ex:   Request Type: GET
                     URL: https://localhost:44362/api/City/GetCity?cityId=6
              Header
                     Key: Authorization
                   Value: Bearer ---Authentication key   ex: Bearer eyJhbGciOiJ........................
               Body: {  "EmailAddress":"sheksha@gmail.com", "Password":"Test@1122" }
                    
 6. Pleasse review <a href='https://github.com/sheksha001/Fort/blob/master/EXPLANATIONS.pdf'> EXPLANATIONS.pdf</a> for testing.
                 
