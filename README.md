
## Getting Started

These instructions will get you a copy of the project up and running on your local machine for development and testing purposes. 

## Tool used: 
1. Visual studio 2019
2. Framework: .Net core 2.0

## How to run the projects: 
1. Run project with visual studio
2. Configure Postman collection (Find the postman file from 03. PostMan)
3. Change the API header 
For security purposes an “Authorization” object is used. The value of the object is calculated based on .Net Core Basic Authentication method. Below is the format for specifying the “Authorization” key :

```
Authorization : Basic eXZOeFdVNGNLa3E5SllvTnIxNXNkZnNzZGZz
```
The key is built in below format :

```
{{SecretKey}}{{::}}{{TimeStamp}}
```
* {{SecretKey}}  -  Secret key is the authentication key shared separately via email.You can find the value from appsettings file
* {{::}}  -  Delimiter separating the two values
* {{TimeStamp}}  -  Current time stamp value

***For Example :  abcSecretKey:: 2019/08/06 15:48:27**

Finally the UTF-8 encoding is applied and the resultant value is converted to Base64 format.

{{AuthorizationToken}} : **Convert to Base 64 ( Encode to UTF-8 ({{SecretKey}}{{::}}{{TimeStamp}} )**

Hence the  Authorization key is , Authorization : **Basic {{AuthorizationToken}}**

You may use https://www.base64encode.org/ to convert the date to base64

