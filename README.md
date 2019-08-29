# AlphaRest
Simple upload and save file  with api in .net standard

<b>How to use :</b>

    Install-Package AlphaRest 
    
    
<b>Upload File : </b>

    await AlphaRest.Api.ConnectApi.SendFileAsync(file,new List<string>() {"png","jpg" },(2*1024),"yourAdress");
    
    
<b>Save File  with uniqName: </b>

    await AlphaRest.file.File.SaveFileAsync(file, "yourpath", new List<string>() { "png", "jpg" });

<b>Save File  with CustomName: </b>


    await AlphaRest.file.File.SaveFileAsync(file,"YourCustomeName", "yourpath", new List<string>() { "png", "jpg" });
