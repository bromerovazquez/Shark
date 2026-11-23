using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace Drim.Interfaces.MVC.Helpers
{
    public static class FTPManager
    {

        public static string ftphost_Adaia
        {
            get
            {
                return new ConfigurationBuilder().AddJsonFile("appsettings.json").Build().GetSection("ftphost_Adaia").Value;
            }
        }

        public static string ftpUser_Adaia
        {
            get
            {
                return new ConfigurationBuilder().AddJsonFile("appsettings.json").Build().GetSection("ftpUser_Adaia").Value;
            }
        }


        public static string ftpPass_Adaia
        {
            get
            {
                return new ConfigurationBuilder().AddJsonFile("appsettings.json").Build().GetSection("ftpPass_Adaia").Value;
            }
        }


        public static string ftpDirectorio_de_Gestion
        {
            get
            {
                return new ConfigurationBuilder().AddJsonFile("appsettings.json").Build().GetSection("ftpDirectorio_de_Gestion").Value;
            }
        }

        public static string ftpDirectorio_a_Gestion
        {
            get
            {
                return new ConfigurationBuilder().AddJsonFile("appsettings.json").Build().GetSection("ftpDirectorio_a_Gestion").Value;
            }
        }

        public static string LocalDirectorio_de_Adaia
        {
            get
            {
                return new ConfigurationBuilder().AddJsonFile("appsettings.json").Build().GetSection("LocalDirectorio_de_Adaia").Value;
            }
        }


        public static string LocalDirectorio_a_Adaia
        {
            get
            {
                return new ConfigurationBuilder().AddJsonFile("appsettings.json").Build().GetSection("LocalDirectorio_a_Adaia").Value;
            }
        }



        public static string ReaderFileLocal(string origenPath, out string responseData)
        {
            string result = string.Empty;
            responseData = string.Empty;

            try
            {
                 responseData = File.ReadAllText(origenPath);
            }
            catch (WebException e)
            {
                result = e.Message;
            }

            return result;

        }

        public static string ReaderFileFtp(string origenPath, string user, string pass,out string responseData) 
        {
            string result = string.Empty;
            responseData = string.Empty;

            try
            {
                using (WebClient request = new WebClient())
                {
                    request.Credentials = new NetworkCredential(user, pass);
                    byte[] newFileData = request.DownloadData(origenPath);
                    responseData = System.Text.Encoding.UTF8.GetString(newFileData);
                }
            }
            catch (WebException e)
            {
                result = e.Message;
            }

            return result;
        }


        public static string DownloadFileFTP(string origenPath, string destinoPath, string user, string pass)
        {
            string result = string.Empty;

            try
            {

                using (WebClient request = new WebClient())
                {
                    request.Credentials = new NetworkCredential(user, pass);
                    request.DownloadFile(origenPath, destinoPath);
                }
            }
            catch (Exception ex)
            {
                result = ex.Message;
            }

            return result;
        }

        public static string UploadFileFTP(string origenPath, string destinoPath, string user, string pass)
        {
            string result = string.Empty;

            try
            {
                using (WebClient request = new WebClient())
                {
                    request.Credentials = new NetworkCredential(user, pass);
                    request.UploadFile(origenPath, WebRequestMethods.Ftp.UploadFile, destinoPath);
                }
            }
            catch (Exception ex)
            {
                result = ex.Message;
            }

            return result;
        }

        public static string DeleteFileOnFtpServer(string origenPath, string user, string pass)
        {

            string result = string.Empty;

            try
            {

                Uri serverUri = new Uri(origenPath);


                if (serverUri.Scheme != Uri.UriSchemeFtp)
                {
                    result = "La url no es valida";
                }
                // Get the object used to communicate with the server.
                FtpWebRequest request = (FtpWebRequest)WebRequest.Create(serverUri);
                request.Credentials = new NetworkCredential(user, pass);
                request.Method = WebRequestMethods.Ftp.DeleteFile;

                FtpWebResponse response = (FtpWebResponse)request.GetResponse();
                //Console.WriteLine("Delete status: {0}", response.StatusDescription);
                response.Close();

            }
            catch (Exception ex)
            {
                result = ex.Message;
            }

            return result;
        }


        public static bool FileExists(string origenPath, string user, string pass)
        {

            var request = (FtpWebRequest)WebRequest.Create(origenPath);
            request.Credentials = new NetworkCredential(user, pass);
            request.Method = WebRequestMethods.Ftp.GetFileSize;

            try
            {
                FtpWebResponse response = (FtpWebResponse)request.GetResponse();
                return true;
            }
            catch (WebException ex)
            {
                FtpWebResponse response = (FtpWebResponse)ex.Response;
                if (response.StatusCode == FtpStatusCode.ActionNotTakenFileUnavailable)
                    return false;
            }
            return false;
        }


        public static List<string> GetFilesFtp(string origenPath, string user, string pass)
        {

            List<string> result = new List<string>();

            try
            {
                FtpWebRequest request = (FtpWebRequest)WebRequest.Create(origenPath);
                request.Method = WebRequestMethods.Ftp.ListDirectory;

                

                request.Credentials = new NetworkCredential(user, pass);
                FtpWebResponse response = (FtpWebResponse)request.GetResponse();
                Stream responseStream = response.GetResponseStream();
                StreamReader reader = new StreamReader(responseStream);
                string names = reader.ReadToEnd();

                reader.Close();
                response.Close();

                result = names.Split(new string[] { "\r\n" }, StringSplitOptions.RemoveEmptyEntries).ToList();
            }
            catch (Exception)
            {
                throw;
            }

            return result;
        }


        public static List<string> GetFilesLocal(string localDirectorio)
        {
            string[] files = Directory.GetFiles(localDirectorio);
            return files.ToList();
        }
    }
}
