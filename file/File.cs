using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.IO;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace AlphaRest.file
{
    public static class File
    {
        /// <summary>
        /// save File with auto uniq Name in Specific Path 
        /// </summary>
        /// <param name="file"></param>
        /// <param name="UploadPath">UploadPath can be like this : WebRootPath/Folder/Subfolder/SubOfSubFolder</param>
        /// <param name="AllowdExtention">WhitListTosave : new List<string>(){png,gif}</param>
        /// <returns>Return a tuple of bool an string  to chack state and name of file</returns>
        public static async Task<(bool, string)> SaveFileAsync(IFormFile file, string UploadPath, List<string> AllowdExtention)
        {

            try
            {
                if (file != null)
                {
                    string Message = Message = "فایل وارد شده مجاز نمی باشد";

                    //check file Type in Dot net core is Null 
                    //file.ContentType.StartsWith(fileType.ToString()))
                    string Extention = file.FileName.Substring(file.FileName.LastIndexOf('.'));
                    if (!AllowdExtention.Contains(Extention))
                    {
                        return (false, Message);
                    }
                    if (!Directory.Exists(UploadPath))
                    {
                        Directory.CreateDirectory(UploadPath);
                    }
                    string uinqName = Generatetoken();
                    var fileName = $"{uinqName}.{Extention}";

                    var filePath = Path.Combine(UploadPath, fileName);
                    using (var fileStream = new FileStream(filePath, FileMode.Create))
                    {
                        await file.CopyToAsync(fileStream).ConfigureAwait(false);
                    }
                    return (true, fileName);
                }
                else
                {
                    return (false, "No Files Found");
                }


            }
            catch (Exception ex)
            {
                return (false, ex.Message);
            }
        }



        /// <summary>
        /// save File with auto uniq Name in Specific Path 
        /// </summary>
        /// <param name="file"></param>
        /// <param name="Name">the Name without Extention</param>
        /// <param name="UploadPath">UploadPath can be like this : WebRootPath/Folder/Subfolder/SubOfSubFolder</param>
        /// <param name="AllowdExtention">WhitListTosave : new List<string>(){png,gif}</param>
        /// <returns>Return a tuple of bool an string  to chack state and name of file</returns>
        public static async Task<(bool, string)> SaveFileAsync(IFormFile file,string Name, string UploadPath, List<string> AllowdExtention)
        {

            try
            {
                if (file != null)
                {
                    string Message = Message = "فایل وارد شده مجاز نمی باشد";

                    //check file Type in Dot net core is Null 
                    //file.ContentType.StartsWith(fileType.ToString()))
                    string Extention = file.FileName.Substring(file.FileName.LastIndexOf('.'));
                    if (!AllowdExtention.Contains(Extention))
                    {
                        return (false, Message);
                    }
                    if (!Directory.Exists(UploadPath))
                    {
                        Directory.CreateDirectory(UploadPath);
                    }
                   
                    var fileName = $"{Name}.{Extention}";

                    var filePath = Path.Combine(UploadPath, fileName);
                    using (var fileStream = new FileStream(filePath, FileMode.Create))
                    {
                        await file.CopyToAsync(fileStream).ConfigureAwait(false);
                    }
                    return (true, fileName);
                }
                else
                {
                    return (false, "No Files Found");
                }


            }
            catch (Exception ex)
            {
                return (false, ex.Message);
            }
        }


        public static string Generatetoken()
        {
            return Regex.Replace(Convert.ToBase64String(Guid.NewGuid().ToByteArray()), "[/+=]", "");
        }

    }
}
