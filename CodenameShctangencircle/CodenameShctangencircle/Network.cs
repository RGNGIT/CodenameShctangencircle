using System;
using System.IO;
using System.Net;
using System.Net.NetworkInformation;
using System.Text;

namespace CodenameShctangencircle
{
    class Network
    {

        public Network(NetworkCredential credential, string URL)
        {
            this.credential = credential;
            this.URL = URL;
        }

        NetworkCredential credential;
        string URL;

        public bool Ping(string URL, out string Ping)
        {
            Ping ping = new Ping();
            PingReply reply = ping.Send(URL);
            if (reply.Status == IPStatus.Success)
            {
                Ping = $"Сервер активен: {reply.RoundtripTime}мс";
                return true;
            }
            else
            {
                Ping = "Нет соединения!";
                return false;
            }
        }

        public byte[] GetInput(Uri InputUri)
        {
            WebClient Request = new WebClient()
            {
                Credentials = credential
            };
            try
            {
                byte[] InputData = Request.DownloadData(InputUri.ToString());
                return InputData;
            }
            catch (Exception e)
            {
                if (e.HResult != -2146233079)
                {
                    Console.WriteLine($"Произошло исключение: {e.Message}");
                }
                return null;
            }
        }

        public void SendOutput(byte[] OutputData)
        {
            try
            {
                string PFN = new FileInfo("Input.shc").Name;
                string UploadURL = $"ftp://{URL}/files/ShctangenNetwork/{PFN}";
                FtpWebRequest request = WebRequest.Create(UploadURL) as FtpWebRequest;
                request.Method = WebRequestMethods.Ftp.UploadFile;
                request.Credentials = credential;
                request.Proxy = null;
                request.KeepAlive = true;
                request.UseBinary = true;
                byte[] fileContents = OutputData;
                request.ContentLength = fileContents.Length;
                Stream requestStream = request.GetRequestStream();
                requestStream.Write(fileContents, 0, fileContents.Length);
                requestStream.Close();
                FtpWebResponse response = (FtpWebResponse)request.GetResponse();
                Console.WriteLine(response.StatusDescription);
            }
            catch (Exception e)
            {
                Console.WriteLine($"Произошло исключение: {e.Message}");
            }
        }

    }
}
