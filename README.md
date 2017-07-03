# MobileTec.FaceAPI
Proof of concept about cognitive service with facial recognition of azure's Web APIS

This project was created, for  the proof of concept is about facial recognition  of azure's Web APIS.

This project is created in base the lab and article created by Shashangka Shekhar in the web site www.codeprojects.com (https://www.codeproject.com/Articles/1164651/Face-API-Using-ASP-Net-MVC)
and used the library Microsoft.ProjectOxford.Face like base  on this solution and implement the client this library was taked from
(https://github.com/Microsoft/ProjectOxford-ClientSDK/tree/master/Face/Windows) please if you wanna to change for use your api and
new key please change of the url in MobileTec.FaceAPI.PoC.FaceClient --> FaceServiceClient.cs  after of this change please modify 
Keys from MobileTec.FaceAPI.PoC.Web --> Web.config  in the tag "appSetings"   and rewrite FaceServiceKey for the value of new ke
