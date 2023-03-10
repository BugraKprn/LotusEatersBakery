using System;
using System.Collections.Generic;
using System.Text;
using System.Net;
using System.Web;
using System.IO;
using System.Diagnostics;

namespace System
{
    public class WebHelper
    {
        public static string[] CountryCodes = new string[] { "ac", "ad", "ae", "af", "ag", "ai", "al", "am", "an",
            "ao", "aq", "ar", "as", "at", "au", "aw", "ax", "az", "ba", "bb", "bd", "be", "bf", "bg", "bh", "bi",
            "bj", "bm", "bn", "bo", "br", "bs", "bt", "bv", "bw", "by", "bz", "ca", "cc", "cd", "cf", "cg", "ch",
            "ci", "ck", "cl", "cm", "cn", "co", "cr", "cu", "cv", "cx", "cy", "cz", "de", "dj", "dk", "dm", "do",
            "dz", "ec", "ee", "eg", "er", "es", "et", "eu", "fi", "fj", "fk", "fm", "fo", "fr", "ga", "gb", "gd",
            "ge", "gf", "gg", "gh", "gi", "gl", "gm", "gn", "gp", "gq", "gr", "gs", "gt", "gu", "gw", "gy", "hk",
            "hm", "hn", "hr", "ht", "hu", "id", "ie", "il", "im", "in", "io", "iq", "ir", "is", "it", "je", "jm",
            "jo", "jp", "ke", "kg", "kh", "ki", "km", "kn", "kp", "kr", "kw", "ky", "kz", "la", "lb", "lc", "li",
            "lk", "lr", "ls", "lt", "lu", "lv", "ly", "ma", "mc", "md", "me", "mg", "mh", "mk", "ml", "mm", "mn",
            "mo", "mp", "mq", "mr", "ms", "mt", "mu", "mv", "mw", "mx", "my", "mz", "na", "nc", "ne", "nf", "ng",
            "ni", "nl", "no", "np", "nr", "nu", "nz", "om", "pa", "pe", "pf", "pg", "ph", "pk", "pl", "pm", "pn",
            "pr", "ps", "pt", "pw", "py", "qa", "re", "ro", "rs", "ru", "rw", "sa", "sb", "sc", "sd", "se", "sg",
            "sh", "si", "sj", "sk", "sl", "sm", "sn", "so", "sr", "st", "su", "sv", "sy", "sz", "tc", "td", "tf",
            "tg", "th", "tj", "tk", "tl", "tm", "tn", "to", "tp", "tr", "tt", "tv", "tw", "tz", "ua", "ug", "uk",
            "us", "uy", "uz", "va", "vc", "ve", "vg", "vi", "vn", "vu", "wf", "ws", "ye", "yt", "za", "zm", "zw" };

        public static string[] TLDs = new string[] { "aero", "asia", "biz", "cat", "com", "co", "coop", "edu", "gov", "gen",
            "info", "int", "jobs", "k12", "mil", "mobi", "museum", "name", "net", "org", "pro", "tel", "tr", "travel", "web", "xxx" };

        public static bool IsCountryCode(string code)
        {
            foreach (string item in CountryCodes)
            {
                if (item == code)
                {
                    return true;
                }
            }
            return false;
        }

        public static bool IsTLD(string code)
        {
            foreach (string item in TLDs)
            {
                if (item == code)
                {
                    return true;
                }
            }
            return false;
        }

        /// <summary>
        /// Url'in düzgünlüğünü kontrol etmez. www2 bir subdomain olarak algılanır.
        /// </summary>
        /// <param name="url"></param>
        /// <returns></returns>
        public static bool IsSubDomain(string url)
        {
            url = url.Trim().ToLowerInvariant();
            if (url.StartsWith("http://"))
                url = url.Substring("http://".Length);
            if (url.StartsWith("www."))
                url = url.Substring("www.".Length);

            if (url.EndsWith(".tc"))
            {
                return false;
            }

            string[] parts = url.Split('.');

            if (parts.Length == 2)
            {
                //sadece iki parçadan oluşan bir url sub-domain olamaz
                return false;
            }

            int last = parts.Length - 1;

            if (IsCountryCode(parts[last]))
            {
                //En sonda ülke kodu varsa en fazla üç parça olabilir
                if (parts.Length > 3) return true;

                //Ülke kodundan önceki parça tld değilse bu url bir subdomain olmalı örn: aaa.example.us
                if (!IsTLD(parts[last - 1])) return true;

                return false;
            }
            else
            {
                //Eğer sonda ülke kodu yoksa sadece ükü parçadan oluşmalı örn: example.com
                if (parts.Length > 2)
                    return true;

                return false;
            }
        }

        public static bool IsDomain(string url)
        {
            url = url.Trim().ToLowerInvariant();
            if (url.StartsWith("http://"))
                url = url.Substring("http://".Length);
            if (url.StartsWith("www."))
                url = url.Substring("www.".Length);

            if (!url.Contains("."))
            {
                return false;
            }

            string[] parts = url.Split('.');

            int last = parts.Length - 1;

            if (IsCountryCode(parts[last]))
                return true;

            if (IsTLD(parts[last]))
                return true;

            return false;
        }

        public static string ExcludeSubDomains(string url)
        {
            url = url.Trim().ToLowerInvariant();
            if (url.StartsWith("http://"))
                url = url.Substring("http://".Length);
            if (url.StartsWith("www."))
                url = url.Substring("www.".Length);

            string[] parts = url.Split('.');
            int last = parts.Length - 1;

            string result = "";
            if (IsCountryCode(parts[last]))
            {
                result = parts[last];
                if (IsTLD(parts[last - 1]))
                    result = parts[last - 2] + "." + parts[last - 1] + "." + result;
                else
                    result = parts[last - 1] + "." + result;
            }
            else
            {
                result = parts[last - 1] + "." + parts[last];
            }

            return result;
        }

        public static string GetContent(string url, Dictionary<string, string> headers = null)
        {
            string s = "";
            return GetContent(url, ref s, headers);
        }

        public static string GetContent(string url, ref string content_type, Dictionary<string, string> headers = null)
        {
            System.Net.WebClient client = new System.Net.WebClient();
            client.Proxy.Credentials = System.Net.CredentialCache.DefaultNetworkCredentials;

            string content = "";
            try
            {
                client.Headers.Add("user-agent", "Mozilla/4.0 (compatible; MSIE 6.0; Windows NT 5.2; .NET CLR 1.0.3705;)");

                if (headers != null)
                {
                    foreach (var item in headers)
                    {
                        client.Headers[item.Key] = item.Value;
                    }
                }

                content = client.DownloadString(url);
                string s = client.ResponseHeaders["Content-Type"] ?? "";
                content_type = s;
                //bu if bir durum böyle başka durumlar da olabilir, onları da || ile eklemek lazım
                if (s.ToLower().Contains("utf-8") && client.Encoding != Encoding.UTF8)
                {
                    content = Encoding.UTF8.GetString(client.Encoding.GetBytes(content));
                }
                else if (content.Contains("Ã¼") || content.Contains("ÅŸ") || content.Contains("â€"))
                {
                    content = Encoding.UTF8.GetString(client.Encoding.GetBytes(content));
                }
                /*if (client.Encoding!=Encoding.ASCII)
                {
                    Encoding srcEncoding = client.Encoding;
                    if (s.ToLower().Contains("utf-8") && client.Encoding != Encoding.UTF8)
                        srcEncoding = Encoding.UTF8;

                    content = Encoding.Default.GetString(
                        Encoding.Convert(srcEncoding, Encoding.Default, client.Encoding.GetBytes(content)));
                }*/
            }
            catch
            {
                //TODO: Error handling
                client.Dispose();
                throw;
            }
            client.Dispose();

            return content;
        }

        public static HttpWebRequest GetRequest(string url)
        {
            HttpWebRequest req = (HttpWebRequest)HttpWebRequest.Create(url);
            req.Proxy.Credentials = System.Net.CredentialCache.DefaultNetworkCredentials;
            req.UserAgent = "Mozilla/4.0 (compatible; MSIE 6.0; Windows NT 5.2; .NET CLR 1.0.3705;)";
            req.Timeout = 10000;
            return req;
        }

        public static long GetFileSize(string url)
        {
            HttpWebRequest req = (HttpWebRequest)HttpWebRequest.Create(url);
            req.Proxy.Credentials = System.Net.CredentialCache.DefaultNetworkCredentials;
            req.Method = "HEAD";
            req.Timeout = 2000;
            try
            {
                WebResponse response = req.GetResponse();
                response.Close();
                return response.ContentLength;
            }
            catch
            {
                return 0;
            }
        }

        public static string ConvertRelativeUrlToAbsolute(string relativeUrl, string baseUrl)
        {
            return (new Uri(new Uri(baseUrl), relativeUrl)).ToString();
        }

        public static void GetFile(string url, string filename)
        {
            System.Net.WebClient client = new System.Net.WebClient();
            client.Proxy.Credentials = System.Net.CredentialCache.DefaultNetworkCredentials;

            try
            {
                client.DownloadFile(url, filename);
            }
            catch { }
            client.Dispose();
        }

        public static bool IsWebPage(string url)
        {
            return !IsBinary(url);
        }

        public static bool IsBinary(string url)
        {
            if (url.Length < 4) return false;

            string ext = url.Substring(url.Length - 4, 4).ToLower();

            if (ext.EndsWith(".bmp")) return true;
            if (ext.EndsWith(".jpg")) return true;
            if (ext.EndsWith(".png")) return true;
            if (ext.EndsWith(".ppt")) return true;
            if (ext.EndsWith(".pdf")) return true;
            if (ext.EndsWith(".zip")) return true;
            if (ext.EndsWith(".gif")) return true;
            if (ext.EndsWith(".doc")) return true;
            if (ext.EndsWith(".xls")) return true;
            if (ext.EndsWith(".wmv")) return true;
            if (ext.EndsWith(".avi")) return true;
            if (ext.EndsWith(".mp3")) return true;
            if (ext.EndsWith(".mp4")) return true;

            return false;
        }

        public static CookieContainer mySession = new CookieContainer();

        public static void DisableNagleforPerformance()
        {
            ServicePointManager.UseNagleAlgorithm = false;
            ServicePointManager.Expect100Continue = false;
            ServicePointManager.CheckCertificateRevocationList = false;
            ServicePointManager.DefaultConnectionLimit = ServicePointManager.DefaultPersistentConnectionLimit;
        }

        public static string PostData(string url, Dictionary<string, string> data)
        {
            string responseFromServer = string.Empty;

            // Create a request using a URL that can receive a post. 
            //WebRequest request = WebRequest.Create(url);            
            HttpWebRequest request = (HttpWebRequest)HttpWebRequest.Create(url);
            request.Proxy.Credentials = System.Net.CredentialCache.DefaultNetworkCredentials;
            request.CookieContainer = mySession;

            // Set the Method property of the request to POST.
            request.Method = "POST";
            // Create POST data and convert it to a byte array.
            string postData = null;
            foreach (string key in data.Keys)
            {
                if (!string.IsNullOrEmpty(postData)) postData += "&";
                postData += key + "=" + HttpUtility.UrlEncode(Encoding.UTF8.GetBytes(data[key] ?? ""));
            }
            byte[] byteArray = Encoding.UTF8.GetBytes(postData);
            // Set the ContentType property of the WebRequest.
            request.ContentType = "application/x-www-form-urlencoded";
            // Set the ContentLength property of the WebRequest.
            request.ContentLength = byteArray.Length;
            // Get the request stream.
            try
            {
                using (Stream dataStream = request.GetRequestStream())
                {
                    // Write the data to the request stream.
                    dataStream.Write(byteArray, 0, byteArray.Length);
                    // Close the Stream object.
                    dataStream.Close();
                }
            }
            catch (Exception ex)
            {
                request.Abort();

                throw ex;
            }

            try
            {
                // Get the response.
                using (WebResponse response = request.GetResponse())
                using (Stream dataStream = response.GetResponseStream())
                {
                    // Display the status.
                    Console.WriteLine(((HttpWebResponse)response).StatusDescription);

                    // Open the stream using a StreamReader for easy access.
                    using (StreamReader reader = new StreamReader(dataStream))
                    {
                        // Read the content.
                        responseFromServer = reader.ReadToEnd();
                        reader.Close();
                    }

                    // Clean up the streams.
                    dataStream.Close();
                    response.Close();
                }
            }
            catch (Exception ex)
            {
                request.Abort();

                throw ex;
            }

            return responseFromServer;

        }
        public static string PostData(string url, string json)
        {
            string responseFromServer = string.Empty;

            // Create a request using a URL that can receive a post. 
            //WebRequest request = WebRequest.Create(url);            
            HttpWebRequest request = (HttpWebRequest)HttpWebRequest.Create(url);
            ServicePointManager.ServerCertificateValidationCallback += delegate { return true; };
            ServicePointManager.Expect100Continue = false;
            request.Proxy.Credentials = System.Net.CredentialCache.DefaultNetworkCredentials;
            request.CookieContainer = mySession;

            // Set the Method property of the request to POST.
            request.Method = "POST";
            // Create POST data and convert it to a byte array.
            string postData = json;

            byte[] byteArray = Encoding.UTF8.GetBytes(postData);
            // Set the ContentType property of the WebRequest.
            request.ContentType = "application/x-www-form-urlencoded";
            // Set the ContentLength property of the WebRequest.
            request.ContentLength = byteArray.Length;
            // Get the request stream.
            try
            {
                using (Stream dataStream = request.GetRequestStream())
                {
                    // Write the data to the request stream.
                    dataStream.Write(byteArray, 0, byteArray.Length);
                    // Close the Stream object.
                    dataStream.Close();
                }
            }
            catch (Exception ex)
            {
                request.Abort();

                throw ex;
            }

            try
            {
                using (WebResponse response = request.GetResponse())
                using (Stream dataStream = response.GetResponseStream())
                {
                    // Display the status.
                    Console.WriteLine(((HttpWebResponse)response).StatusDescription);
                    // Open the stream using a StreamReader for easy access.
                    using (StreamReader reader = new StreamReader(dataStream))
                    {
                        // Read the content.
                        responseFromServer = reader.ReadToEnd();
                        reader.Close();
                    }

                    // Clean up the streams.
                    dataStream.Close();
                    response.Close();
                }
            }
            catch (Exception ex)
            {
                request.Abort();

                throw ex;
            }

            return responseFromServer;
        }

        public static string UploadData(string url, string postData)
        {
            WebClient wUpload = new WebClient();
            wUpload.Proxy.Credentials = System.Net.CredentialCache.DefaultNetworkCredentials;
            Byte[] bPostArray = Encoding.UTF8.GetBytes(postData);
            Byte[] bResponse = wUpload.UploadData(url, "POST", bPostArray);
            Char[] sReturnChars = Encoding.UTF8.GetChars(bResponse);
            string sWebPage = new string(sReturnChars);
            return sWebPage;
        }
        //public static string PostData(string url, string json)
        //{
        //    string responseFromServer = string.Empty;

        //    // Create a request using a URL that can receive a post. 
        //    //WebRequest request = WebRequest.Create(url);            
        //    HttpWebRequest request = (HttpWebRequest)HttpWebRequest.Create(url);

        //    ServicePointManager.Expect100Continue = false;

        //    //request.Proxy.Credentials = System.Net.CredentialCache.DefaultNetworkCredentials;
        //    //request.Proxy = null;
        //    request.CookieContainer = mySession;

        //    // Set the Method property of the request to POST.
        //    request.Method = "POST";
        //    // Create POST data and convert it to a byte array.
        //    string postData = json;

        //    byte[] byteArray = Encoding.UTF8.GetBytes(postData);

        //    // Set the ContentType property of the WebRequest.
        //    request.ContentType = "application/x-www-form-urlencoded";
        //    // Set the ContentLength property of the WebRequest.
        //    request.ContentLength = byteArray.Length;
        //    // Get the request stream.
        //    try
        //    {
        //        using (Stream dataStream = request.GetRequestStream())
        //        {
        //            // Write the data to the request stream.
        //            dataStream.Write(byteArray, 0, byteArray.Length);
        //            // Close the Stream object.
        //            dataStream.Close();
        //        }
        //    }
        //    catch (Exception ex)
        //    {
        //        request.Abort();

        //        throw ex;
        //    }

        //    try
        //    {
        //        var debug = "";

        //        using (WebResponse response = request.GetResponse())
        //        using (Stream dataStream = response.GetResponseStream())
        //        {
        //            // Display the status.
        //            Console.WriteLine(((HttpWebResponse)response).StatusDescription);
        //            // Open the stream using a StreamReader for easy access.
        //            using (StreamReader reader = new StreamReader(dataStream))
        //            {
        //                // Read the content.
        //                responseFromServer = reader.ReadToEnd();
        //                reader.Close();
        //            }

        //            debug = response.Headers["CustomDebugHeader"];

        //            // Clean up the streams.
        //            dataStream.Close();
        //            response.Close();
        //        }
        //    }
        //    catch (Exception ex)
        //    {
        //        var resp = "";
        //        var exWeb = ex as WebException;
        //        if (exWeb != null)
        //        {
        //            using (Stream dataStream = exWeb.Response.GetResponseStream())
        //            {
        //                resp = new StreamReader(dataStream).ReadToEnd();
        //            }
        //        }

        //        request.Abort();

        //        if (!string.IsNullOrEmpty(resp))
        //            throw new ApplicationException(resp);
        //        else
        //            throw ex;
        //    }

        //    return responseFromServer;
        //}

    }
}