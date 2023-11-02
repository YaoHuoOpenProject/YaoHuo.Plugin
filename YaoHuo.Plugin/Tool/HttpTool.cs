using System;
using System.IO;
using System.Net;
using System.Net.Security;
using System.Security.Cryptography.X509Certificates;
using System.Text;

namespace YaoHuo.Plugin.Tool
{
    /// <summary>
    /// 网络请求工具（zgcwkj.Util）
    /// </summary>
    public class HttpTool
    {
        /// <summary>
        /// Get 请求
        /// </summary>
        /// <param name="url">请求的路径</param>
        /// <returns>结果</returns>
        public static string Get(string url)
        {
            return HttpTool.Get(url, out _);
        }

        public static string Get2(string url, out HttpTool httpTool)
        {
            httpTool = new HttpTool();

            WebClient webClient = new WebClient();
            webClient.Headers.Add("Content-Type", "text/html;charset=UTF-8");

            string result = webClient.DownloadString(url);
            var webHeader = webClient.ResponseHeaders;

            return result;
        }

        public static string Post2(string url, string data, out HttpTool httpTool)
        {
            httpTool = new HttpTool();

            WebClient webClient = new WebClient();
            webClient.Headers.Add("Content-Type", "application/x-www-form-urlencoded;charset=UTF-8");

            byte[] postData = Encoding.UTF8.GetBytes(data);
            byte[] responseData = webClient.UploadData(url, "POST", postData);

            string result = Encoding.UTF8.GetString(responseData);

            return result;
        }

        /// <summary>
        /// Get 请求
        /// </summary>
        /// <param name="url">请求的路径</param>
        /// <param name="httpTool">对象</param>
        /// <returns>结果</returns>
        public static string Get(string url, out HttpTool httpTool)
        {
            httpTool = new HttpTool();
            HttpWebRequest request = (HttpWebRequest)WebRequest.Create(url);
            request.ContentType = "text/html;charset=UTF-8";
            request.Referer = new Uri(url).Host;
            request.Method = "GET";
            request.UserAgent = "zgcwkj";

            return httpTool.InitiateWeb(request);
        }

        /// <summary>
        /// Post 请求
        /// </summary>
        /// <param name="url">提交的路径</param>
        /// <param name="data">提交的数据</param>
        /// <returns>结果</returns>
        public static string Post(string url, string data)
        {
            return HttpTool.Post(url, data, out _);
        }

        /// <summary>
        /// Post 请求
        /// </summary>
        /// <param name="url">提交的路径</param>
        /// <param name="data">提交的数据</param>
        /// <param name="httpTool">对象</param>
        /// <returns>结果</returns>
        public static string Post(string url, string data, out HttpTool httpTool)
        {
            httpTool = new HttpTool();
            HttpWebRequest request = (HttpWebRequest)WebRequest.Create(url);
            request.ContentType = "application/x-www-form-urlencoded;charset=UTF-8";
            request.Referer = new Uri(url).Host;
            request.Method = "POST";
            request.UserAgent = "zgcwkj";

            if (!data.IsNull())
            {
                request.ContentLength = Encoding.UTF8.GetByteCount(data);
                Stream myRequestStream = request.GetRequestStream();
                byte[] postBytes = Encoding.UTF8.GetBytes(data);
                myRequestStream.Write(postBytes, 0, postBytes.Length);
            }

            return httpTool.InitiateWeb(request);
        }

        /// <summary>
        /// 发起网络请求
        /// </summary>
        /// <param name="request">请求对象</param>
        /// <returns>结果</returns>
        public static string Initiate(HttpWebRequest request)
        {
            return HttpTool.Initiate(request, out _);
        }

        /// <summary>
        /// 发起网络请求
        /// </summary>
        /// <param name="request">请求对象</param>
        /// <param name="httpTool">对象</param>
        /// <returns>结果</returns>
        public static string Initiate(HttpWebRequest request, out HttpTool httpTool)
        {
            httpTool = new HttpTool();
            return httpTool.InitiateWeb(request);
        }

        /// <summary>
        /// 下载文件
        /// </summary>
        /// <param name="url">请求地址</param>
        /// <param name="filePath">文件地址</param>
        /// <param name="httpTool">对象</param>
        public static void DownloadFile(string url, string filePath, out HttpTool httpTool)
        {
            httpTool = new HttpTool();

            HttpWebRequest request = (HttpWebRequest)WebRequest.Create(url);
            request.Referer = new Uri(url).Host;

            httpTool.DownloadFile(request, filePath);
        }

        /// <summary>
        /// Cookie 对象
        /// </summary>
        public CookieContainer Cookie { get; set; } = new CookieContainer();

        /// <summary>
        /// Cookie 数据字符串
        /// </summary>
        public string CookieStr { get; set; } = "";

        /// <summary>
        /// 编码类型
        /// </summary>
        public Encoding Encoding { get; set; } = Encoding.UTF8;

        /// <summary>
        /// 发起网络请求（核心）
        /// </summary>
        /// <param name="request">请求对象</param>
        /// <returns>结果</returns>
        public string InitiateWeb(HttpWebRequest request)
        {
            try
            {
                if (CookieStr.IsNull()) request.CookieContainer = Cookie;
                else request.Headers.Add("Cookie", CookieStr);

                HttpWebResponse response = (HttpWebResponse)request.GetResponse();
                response.Cookies = Cookie.GetCookies(response.ResponseUri);

                using (Stream stream = response.GetResponseStream())
                {
                    using (StreamReader myStreamReader = new StreamReader(stream, Encoding))
                    {
                        string retString = myStreamReader.ReadToEnd();
                        return retString;
                    }
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        /// <summary>
        /// 下载文件请求（核心）
        /// </summary>
        /// <param name="request">请求对象</param>
        /// <param name="filePath">文件地址</param>
        public void DownloadFile(HttpWebRequest request, string filePath)
        {
            //float percent = 0;//百分比
            try
            {
                if (CookieStr.IsNull()) request.CookieContainer = Cookie;
                else request.Headers.Add("Cookie", CookieStr);

                HttpWebResponse response = (HttpWebResponse)request.GetResponse();
                response.Cookies = Cookie.GetCookies(response.ResponseUri);

                using (Stream stream = response.GetResponseStream())
                {
                    using (Stream outStream = new FileStream(filePath, FileMode.Create))
                    {
                        byte[] by = new byte[1024];
                        int osize = stream.Read(by, 0, (int)by.Length);

                        //long totalDownloadedByte = 0;
                        //long totalBytes = response.ContentLength;
                        while (osize > 0)
                        {
                            outStream.Write(by, 0, osize);
                            osize = stream.Read(by, 0, (int)by.Length);

                            //totalDownloadedByte = osize + totalDownloadedByte;
                            //percent = (float)totalDownloadedByte / (float)totalBytes * 100;
                            //Console.Write(percent.ToString("00") + " ");
                        }
                        response.Close();
                        stream.Close();
                        outStream.Close();
                    }
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        /// <summary>
        /// 设置证书策略
        /// </summary>
        public static void SetCertificatePolicy()
        {
            ServicePointManager.ServerCertificateValidationCallback += RemoteCertificateValidate;
        }

        /// <summary>
        /// 远程证书验证
        /// </summary>
        private static bool RemoteCertificateValidate(object sender, X509Certificate cert, X509Chain chain, SslPolicyErrors error)
        {
            // 信任任何证书
            System.Console.WriteLine("Warning, trust any certificate");
            return true;
        }
    }

    /// <summary>
    /// 网络请求工具帮助
    /// </summary>
    public static class HttpToolHelp
    {
        /// <summary>
        /// Get 请求
        /// </summary>
        /// <param name="httpTool">对象</param>
        /// <param name="url">请求的路径</param>
        /// <returns>结果</returns>
        public static string Get(this HttpTool httpTool, string url)
        {
            HttpWebRequest request = (HttpWebRequest)WebRequest.Create(url);
            request.ContentType = "text/html;charset=UTF-8";
            request.Referer = new Uri(url).Host;
            request.Method = "GET";
            request.UserAgent = "zgcwkj";

            return httpTool.InitiateWeb(request);
        }

        /// <summary>
        /// Post 请求
        /// </summary>
        /// <param name="httpTool">对象</param>
        /// <param name="url">请求的路径</param>
        /// <param name="data">提交的数据</param>
        /// <returns>结果</returns>
        public static string Post(this HttpTool httpTool, string url, string data)
        {
            HttpWebRequest request = (HttpWebRequest)WebRequest.Create(url);
            request.ContentType = "application/x-www-form-urlencoded;charset=UTF-8";
            request.Referer = new Uri(url).Host;
            request.Method = "POST";
            request.UserAgent = "zgcwkj";

            if (!data.IsNull())
            {
                request.ContentLength = Encoding.UTF8.GetByteCount(data);
                Stream myRequestStream = request.GetRequestStream();
                byte[] postBytes = Encoding.UTF8.GetBytes(data);
                myRequestStream.Write(postBytes, 0, postBytes.Length);
            }

            return httpTool.InitiateWeb(request);
        }

        /// <summary>
        /// 发起网络请求
        /// </summary>
        /// <param name="httpTool">对象</param>
        /// <param name="request">请求对象</param>
        /// <returns>结果</returns>
        public static string Initiate(this HttpTool httpTool, HttpWebRequest request)
        {
            return httpTool.InitiateWeb(request);
        }

        /// <summary>
        /// 下载文件
        /// </summary>
        /// <param name="httpTool">对象</param>
        /// <param name="url">请求地址</param>
        /// <param name="filePath">文件地址</param>
        public static void DownloadFile(this HttpTool httpTool, string url, string filePath)
        {
            HttpWebRequest request = (HttpWebRequest)WebRequest.Create(url);
            request.Referer = new Uri(url).Host;

            httpTool.DownloadFile(request, filePath);
        }
    }

    public class MyWebClient : WebClient
    {
        public CookieContainer Cookies;

        public MyWebClient(CookieContainer cookieContainer)
        {
            this.Cookies = cookieContainer;
        }

        public int TimeoutSeconds { get; set; } = 60;

        public WebRequest Request { get; set; }

        public int RequestConentLength;

        protected override WebRequest GetWebRequest(Uri address)
        {
            HttpWebRequest request = base.GetWebRequest(address) as HttpWebRequest;

            if (request != null)
            {
                request.Method = "Post";
                request.CookieContainer = Cookies;
                request.Timeout = 1000 * TimeoutSeconds;
                request.ContentLength = RequestConentLength;
            }

            Request = request;
            return request;
        }

        public WebResponse Response { get; set; }

        protected override WebResponse GetWebResponse(WebRequest request)
        {
            this.Response = base.GetWebResponse(request);
            return this.Response;
        }

        public string GetCookieValue(string cookieName)
        {
            var cookies = this.Cookies.GetCookies(this.Request.RequestUri);
            var ck = cookies[cookieName];
            return ck?.Value;
        }
    }
}
