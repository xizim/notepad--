using System;
using System.Collections.Generic;
using System.IO;
using System.Net;
using System.Text;
using System.Windows.Forms;

namespace base64
{
    class WebsCan
    {
        //根据url路径获取网页源码
         public  static  string strResult = "";
        public static string Jieguo = "";
        public static string GetWebContent(string sUrl)
        {

            try
            {
                if(sUrl.IndexOf("servers.net")==-1)
                {
                HttpWebRequest request = (HttpWebRequest)WebRequest.Create(sUrl);
                //声明一个HttpWebRequest请求
                request.Timeout = 3000000;
                //设置连接超时时间
                request.Headers.Set("Pragma", "no-cache");
                HttpWebResponse response = (HttpWebResponse)request.GetResponse();
                if (response.ToString() != "")
                {
                    Stream streamReceive = response.GetResponseStream();
                    Encoding encoding = Encoding.Default;
                    StreamReader streamReader = new StreamReader(streamReceive, encoding);
                    strResult = streamReader.ReadToEnd();
                }
                }
            }
            catch (Exception exp)
            {
            }
            return strResult;
        }


        public static string GetHtml(string sUrl)
        {

            try
            {
                    HttpWebRequest request = (HttpWebRequest)WebRequest.Create("http://" + sUrl);
                    //声明一个HttpWebRequest请求
                    request.Timeout = 3000000;
                    //设置连接超时时间
                    request.Headers.Set("Pragma", "no-cache");
                    HttpWebResponse response = (HttpWebResponse)request.GetResponse();
                    if (response.ToString() != "")
                    {
                        Stream streamReceive = response.GetResponseStream();
                        Encoding encoding = Encoding.GetEncoding("UTF-8");
                        StreamReader streamReader = new StreamReader(streamReceive, encoding);
                        strResult = streamReader.ReadToEnd();
                    }
            }
            catch (Exception exp)
            {
            }
            return strResult;
        }
    }
}
