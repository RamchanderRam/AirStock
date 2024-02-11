
//using Azure.Data.Tables;
using AirStock.Common.Models;
using Newtonsoft.Json;
using System.Text;
#nullable disable

namespace AirStock.Common.Models
{
    //public class ServiceRoutine
    //{
    //}

    public interface IServiceRoutine
    {
        public Task<T> GetAsync<T>(string url, Dictionary<string, string> headers = null);
        public Task<T> PostJsonDataAsync<T>(string url, object data, Dictionary<string, string> headers = null);
        public Task<T> PutJsonDataAsync<T>(string url, object data, Dictionary<string, string> headers = null);
        public Task<T> DeleteJsonDataAsync<T>(string url, object data, Dictionary<string, string> headers = null);

    }
    public class ServiceRoutine : IServiceRoutine
    {
        private readonly IHttpClientFactory _clientFactory;
        private readonly string _accessToken;
        private readonly HttpClient _client;
        public ServiceRoutine(IHttpClientFactory clientFactory, IGetAccessToken getAccessToken)
        {
            _clientFactory = clientFactory;
            _accessToken = getAccessToken.AccessTokenAsync().Result;
            _client = _clientFactory.CreateClient("InternalAPIClient");
            _client.DefaultRequestHeaders.Add("Authorization", "bearer " + _accessToken);
        }
        public async Task<T> GetAsync<T>(string url, Dictionary<string, string> headers = null)
        {
            try
            {
                if (headers != null)
                    GetRequestObject(headers);
                var response = await _client.GetAsync(url);
                if (response.IsSuccessStatusCode)
                {
                    var jsonString = await response.Content.ReadAsStringAsync();
                    var fetchData = FetchDataFromJson(jsonString);
                    var apiResult = fetchData != null ? JsonConvert.DeserializeObject<T>(fetchData) : default(T);
                    return apiResult;
                }
                return default(T);

            }
            catch (Exception ex)
            {
                return default(T);
            }

        }

        public async Task<T> PostJsonDataAsync<T>(string url, object data, Dictionary<string, string> headers = null)
        {
            HttpContent cData = new StringContent(JsonConvert.SerializeObject(data), Encoding.UTF8, "application/json");

            if (headers != null)
                GetRequestObject(headers);
            try
            {
                var response = await _client.PostAsync(url, cData);
                if (response.IsSuccessStatusCode)
                {
                    var jsonString = await response.Content?.ReadAsStringAsync();
                    var fetchData = FetchDataFromJson(jsonString);
                    var apiResult = fetchData != null ? JsonConvert.DeserializeObject<T>(fetchData) : default(T);
                    return apiResult;
                }
            }
            catch (Exception ex)
            {
                throw new Exception(ex.InnerException.ToString());
            }



            return default(T);
        }
        public async Task<T> PutJsonDataAsync<T>(string url, object data, Dictionary<string, string> headers = null)
        {
            HttpContent cData = new StringContent(JsonConvert.SerializeObject(data), Encoding.UTF8, "application/json");
            if (headers != null)
                GetRequestObject(headers);
            var response = await _client.PutAsync(url, cData);

            if (response.IsSuccessStatusCode)
            {
                var jsonString = await response.Content.ReadAsStringAsync();
                var fetchData = FetchDataFromJson(jsonString);
                var apiResult = fetchData != null ? JsonConvert.DeserializeObject<T>(fetchData) : default(T);

                return apiResult;
            }

            return default(T);
        }
        public async Task<T> DeleteJsonDataAsync<T>(string url, object data, Dictionary<string, string> headers = null)
        {
            HttpContent cData = new StringContent(JsonConvert.SerializeObject(data), Encoding.UTF8, "application/json");
            if (headers != null)
                GetRequestObject(headers);
            var response = await _client.DeleteAsync(url);

            if (response.IsSuccessStatusCode)
            {
                var jsonString = await response.Content.ReadAsStringAsync();
                var fetchData = FetchDataFromJson(jsonString);
                var apiResult = fetchData != null ? JsonConvert.DeserializeObject<T>(fetchData) : default(T);
                return apiResult;
            }

            return default(T);
        }
        public HttpClient GetRequestObject(Dictionary<string, string> headers)
        {
            if (headers != null && headers.Count > 0)
            {
                foreach (var item in headers)
                {
                    if (_client.DefaultRequestHeaders.Contains(item.Key))
                    {
                        _client.DefaultRequestHeaders.Remove(item.Key);
                        _client.DefaultRequestHeaders.Add(item.Key, item.Value);
                    }
                    //if (!_client.DefaultRequestHeaders.Contains("X-ApiKey"))
                    //    _client.DefaultRequestHeaders.Add(item.Key, item.Value);
                    if (item.Key == "Authorization" && string.IsNullOrEmpty(item.Value))
                        _client.DefaultRequestHeaders.Remove(item.Key);
                }
            }
            return _client;
        }
        private string FetchDataFromJson(string jsonData)
        {
            var data = JsonConvert.DeserializeObject<ResponseModel>(jsonData);
            if (data != null)
            {
                return data.Data?.ToString();
            }
            return null;
        }
    }
}
