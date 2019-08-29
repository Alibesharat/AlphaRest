using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.IO;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;

namespace AlphaRest.Api
{


    public  static partial class ConnectApi
    {
        static HttpClient client = new HttpClient();


        /// <summary>
        /// Send File From HttpClinet
        /// </summary>
        /// <param name="file">IFormFile file</param>
        /// <param name="AllowdExtention">set allowed AllowdExtention to Post file set with dot like .png</param>
        /// <param name="GatewayUrl">api url</param>
        /// <param name="maxLength">max Length in Byte</param>
        /// <returns>dynamic Type and ResultConteract </returns>
        public static async Task<dynamic> SendFileAsync(IFormFile file, List<string> AllowdExtentions, int maxLength, string GatewayUrl)
        {
            try
            {

                if (file == null && file.Length <= 0)
                    return ("File Not Found");
                var ext = file.FileName.Substring(file.FileName.LastIndexOf('.')).ToLower();
                if (!AllowdExtentions.Contains(ext))
                    return ($"Extention : ({ext}) is Not Allowd !");
                if (file.Length > maxLength)
                    return ($"file  Length: ({file.Length}) is bigger than {maxLength} !");

                try
                {

                    byte[] data;
                    using (var br = new BinaryReader(file.OpenReadStream()))
                        data = br.ReadBytes((int)file.OpenReadStream().Length);

                    ByteArrayContent bytes = new ByteArrayContent(data);
                    MultipartFormDataContent multiContent = new MultipartFormDataContent
                        {
                            { bytes, "file",file.FileName }
                        };
                    var result = await client.PostAsync(GatewayUrl, multiContent);
                    result.EnsureSuccessStatusCode();
                    var resultConteract = await result.Content.ReadAsStringAsync();
                    if (!string.IsNullOrWhiteSpace(resultConteract))
                    {

                        return resultConteract;

                    }
                    else
                    {
                        return ("Get Result was faild");

                    }


                }
                catch (Exception ex)
                {
                    return (ex);

                }


            }
            catch (Exception ex)
            {
                return (ex);
            }


        }


        /// <summary>
        /// Send Model From HttpClinet
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="value"></param>
        /// <param name="Url"></param>
        /// <param name="methode"> def = Post </param>
        /// <returns></returns>
        public static async Task<T> SendModel<T>(object value, string Url, ApiMethode methode = ApiMethode.Post)
        {
            T Data = default(T);
            try
            {
                ServicePointManager.Expect100Continue = false;
                switch (methode)
                {
                    case ApiMethode.Post:

                        var result = await client.PostAsJsonAsync(Url, value);
                        result.EnsureSuccessStatusCode();
                        Data = await result.Content.ReadAsAsync<T>();
                        break;
                    case ApiMethode.Get:
                        var results = await client.GetAsync(Url);
                        results.EnsureSuccessStatusCode();
                        Data = await results.Content.ReadAsAsync<T>();
                        // Data = JsonConvert.DeserializeObject<T>(results);

                        break;
                }
                return Data;
            }
            catch (System.Exception ex)
            {

                //TODO : Log Ex
                return Data;
            }
        }


    }
    public enum ApiMethode
    {

        Post,
        Get
    }





}


