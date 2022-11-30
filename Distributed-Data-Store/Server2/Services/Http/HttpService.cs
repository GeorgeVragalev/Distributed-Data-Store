using System.Text;
using Newtonsoft.Json;
using Server2.Helpers;
using Server2.Models;

namespace Server2.Services.Http;

public class HttpService : IHttpService
{
    public async Task<KeyValuePair<int, Data>?> GetById(int id, string url)
    {
        try
        {
            using var client = new HttpClient();

            var response = await client.GetAsync($"{url}/get/{id}");
            
            var dataAsJson = await response.Content.ReadAsStringAsync();
            var deserialized = JsonConvert.DeserializeObject<KeyValuePair<int, Data>>(dataAsJson);
            
            return deserialized;
        }
        catch (Exception e)
        {
            return null;
        }
    }
    

    public async Task<IDictionary<int, Data>?> GetAll(string url)
    {
        try
        {
            using var client = new HttpClient();

            var response = await client.GetAsync($"{url}/all");
            
            var dataAsJson = await response.Content.ReadAsStringAsync();
            var deserialized = JsonConvert.DeserializeObject<IDictionary<int, Data>>(dataAsJson);
            
            PrintConsole.Write($"Got data from url {url}", ConsoleColor.Green);
            return deserialized;
        }
        catch (Exception e)
        {
            PrintConsole.Write($"Failed get from {url}", ConsoleColor.DarkRed);
        }

        return null;
    }

    public async Task<Data> Update(int id, Data data, string url)
    {
        try
        {
            var json = JsonConvert.SerializeObject(id);
            var content = new StringContent(json, Encoding.UTF8, "application/json");

            using var client = new HttpClient();

            var response = await client.PutAsync($"{url}/update/{id}" , content);
            PrintConsole.Write($"Updated data from url {url}, id: {id}", ConsoleColor.Green);
            return null;
        }
        catch (Exception e)
        {
            PrintConsole.Write($"Failed to update id: {id} from {url}", ConsoleColor.DarkRed);
        }

        return null;
    }

    public async Task<ResultSummary?> Save(Data data, string url)
    {
        try
        {
            var json = JsonConvert.SerializeObject(data);
            var content = new StringContent(json, Encoding.UTF8, "application/json");

            using var client = new HttpClient();

            var response = await client.PostAsync($"{url}", content);
            
            var dataAsJson = await response.Content.ReadAsStringAsync();
            var deserialized = JsonConvert.DeserializeObject<ResultSummary>(dataAsJson);
            
            PrintConsole.Write($"Saved data to url {url}", ConsoleColor.Green);

            return deserialized;
        }
        catch (Exception e)
        {
            PrintConsole.Write($"Failed save to {url}", ConsoleColor.DarkRed);
        }

        return null;
    }

    public async Task<ResultSummary?> Delete(int id, string url)
    {
        try
        {
            using var client = new HttpClient();

            var response = await client.DeleteAsync($"{url}/delete/{id}");
            
            var dataAsJson = await response.Content.ReadAsStringAsync();
            var deserialized = JsonConvert.DeserializeObject<ResultSummary>(dataAsJson);
            
            PrintConsole.Write($"Deleted data from url {url} with id: {id}", ConsoleColor.Green);

            return deserialized;
        }
        catch (Exception e)
        {
            PrintConsole.Write($"Failed to delete from {url} id: {id}", ConsoleColor.DarkRed);
        }

        return null;
    }
}