using System.Net;

namespace PartitionLeader.Services;

public static class FtpService
{
    public static void Test()
    {
        FtpUploadAsync("ftp://ftp.dlptest.com/", "dlpuser", "rNrKYTX9g7z3RgJRmxWuGHbeu", 
            "https://dlptest.com/DLP_Test_FTP_FileZilla.xml");
    }

    private static async Task<FtpStatusCode> FtpUploadAsync(string uri, string userName, string password, string filePath)
    {
        var request = (FtpWebRequest)WebRequest.Create(uri);
        request.Method = WebRequestMethods.Ftp.UploadFile;
        request.Credentials = new NetworkCredential(userName, password);

        await using (var fileStream = new FileStream(filePath, FileMode.Open, FileAccess.Read))
        await using (var requestStream = request.GetRequestStream())
        {
            await fileStream.CopyToAsync(requestStream);
        }

        using (var response = (FtpWebResponse)await request.GetResponseAsync())
        {
            return response.StatusCode;
        }
    }
    
}