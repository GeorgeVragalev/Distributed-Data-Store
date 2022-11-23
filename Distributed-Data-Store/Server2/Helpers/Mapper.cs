using Server2.Models;

namespace Server2.Helpers;

public static class Mapper
{
    public static Data Map(this DataModel dataModel)
    {
       
        return new Data()
        {
            Id = dataModel.Id,
            StreamData = ConvertToByte(dataModel.File),
            ContentType = dataModel.File.ContentType,
            FileName = dataModel.File.FileName
        };
    }

    private static string ConvertToByte(IFormFile file)
    {
        using (var ms = new MemoryStream())
        {
            file.CopyTo(ms);
            var fileBytes = ms.ToArray();
            string s = Convert.ToBase64String(fileBytes);
            return s;
        }
    }
}