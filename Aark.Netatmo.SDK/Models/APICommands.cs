using Aark.Netatmo.SDK.Helpers;
using Aiolos.Models.Netatmo.Model;
using Aiolos.Models.Netatmo.Models;
using System;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;
using System.Web;

namespace Aark.Netatmo.SDK.Models
{
    internal class APICommands
    {
        private string _accessToken = "";
        private DateTime _expireAt = DateTime.Now;
        private string _resfreshToken = "";
        private string _errorMessage = "";

        private static readonly string host = "https://api.netatmo.com";
        private static readonly string authPath = "/oauth2/token";
        private static readonly string apiPath = "/api";

        internal string ApplicationId { get; set; }
        internal string ApplicationSecret { get; set; }
        internal string Username { get; set; }
        internal string Password { get; set; }
        internal string Scope { get; set; }

        internal string GetLastError()
        {
            return _errorMessage;
        }

        #region Authentification
        private async Task<bool> AuthentificationAsync()
        {
            if (Username == "" || Password == "")
                return false;

            var parameters = HttpUtility.ParseQueryString(string.Empty);
            parameters["grant_type"] = "password";
            parameters["client_id"] = ApplicationId;
            parameters["client_secret"] = ApplicationSecret;
            parameters["username"] = Username;
            parameters["password"] = Password;
            parameters["scope"] = Scope;

            using (HttpClient client = new HttpClient())
            using (HttpRequestMessage request = new HttpRequestMessage())
            {
                request.Method = HttpMethod.Post;
                request.RequestUri = new Uri(host + authPath);
                request.Content = new StringContent(parameters.ToString(), Encoding.UTF8, "application/x-www-form-urlencoded");
                HttpResponseMessage response = await client.SendAsync(request);
                string responseBody = await response.Content.ReadAsStringAsync();
                AccessData accessData = AccessData.FromJson(responseBody);
                _accessToken = accessData.AccessToken;
                _expireAt = DateTime.Now.AddSeconds(accessData.ExpiresIn);
                _resfreshToken = accessData.RefreshToken;
                if (_accessToken == null)
                {
                    ErrorData _errorData = ErrorData.FromJson(responseBody);
                    _errorMessage = _errorData.Error;
                    return false;
                }
                return true;
            }
        }

        private async Task<bool> ResfreshAuthentificationAsync()
        {
            var parameters = HttpUtility.ParseQueryString(string.Empty);
            parameters["grant_type"] = "refresh_token";
            parameters["refresh_token"] = _resfreshToken;
            parameters["client_id"] = ApplicationId;
            parameters["client_secret"] = ApplicationSecret;

            using (HttpClient client = new HttpClient())
            using (HttpRequestMessage request = new HttpRequestMessage())
            {
                request.Method = HttpMethod.Post;
                request.RequestUri = new Uri(host + authPath);
                request.Content = new StringContent(parameters.ToString(), Encoding.UTF8, "application/x-www-form-urlencoded");
                HttpResponseMessage response = await client.SendAsync(request);
                string responseBody = await response.Content.ReadAsStringAsync();
                AccessData accessData = AccessData.FromJson(responseBody);
                _accessToken = accessData.AccessToken;
                _expireAt = DateTime.Now.AddSeconds(accessData.ExpiresIn);
                _resfreshToken = accessData.RefreshToken;
                return true;
            }
        }

        private async Task<bool> CheckConnectionAsync()
        {
            if (_accessToken.Length == 0)
                return await AuthentificationAsync();
            else
            {
                if (DateTime.Now.CompareTo(_expireAt) > 0)
                    return await ResfreshAuthentificationAsync();
                else
                    return true;
            }
        }
        #endregion

        #region WeatherStation
        internal async Task<StationData> GetStationsData(string deviceId = "", bool favorite = false)
        {
            if (!await CheckConnectionAsync())
                return null;
            var parameters = HttpUtility.ParseQueryString(string.Empty);
            parameters["access_token"] = _accessToken;
            if (deviceId.Length > 0)
                parameters["device_id"] = deviceId;
            parameters["get_favorites"] = favorite.ToString();

            using (HttpClient client = new HttpClient())
            using (HttpRequestMessage request = new HttpRequestMessage())
            {
                request.Method = HttpMethod.Post;
                request.RequestUri = new Uri(host + apiPath + "/getstationsdata");
                request.Content = new StringContent(parameters.ToString(), Encoding.UTF8, "application/x-www-form-urlencoded");
                HttpResponseMessage response = await client.SendAsync(request);
                string responseBody = await response.Content.ReadAsStringAsync();
                StationData stationData = StationData.FromJson(responseBody);
                if (stationData.Body == null)
                {
                    ErrorMeasuresData errorMeasuresData = ErrorMeasuresData.FromJson(responseBody);
                    _errorMessage = errorMeasuresData.Error.Message;
                    return null;
                }
                return stationData;
            }
        }

        internal async Task<MeasuresData> GetMeasure(string deviceId, MeasureScale scale, MeasuresFilters measureType, DateTime? dateBegin, DateTime? dateEnd, string moduleId = "", int limit = 1024)
        {
            if (!await CheckConnectionAsync())
                return null;
            var parameters = HttpUtility.ParseQueryString(string.Empty);
            parameters["access_token"] = _accessToken;
            parameters["device_id"] = deviceId;
            parameters["scale"] = scale.ToJsonString();
            parameters["type"] = measureType.ToJsonString();
            if (dateBegin != null)
                parameters["date_begin"] = dateBegin.FromLocalDateTime().ToString();
            if (dateEnd != null)
                parameters["date_end"] = dateEnd.FromLocalDateTime().ToString();
            parameters["optimize"] = true.ToString().ToLower();
            parameters["limit"] = limit.ToString();
            if (moduleId.Length > 0)
                parameters["module_id"] = moduleId;

            using (HttpClient client = new HttpClient())
            using (HttpRequestMessage request = new HttpRequestMessage())
            {
                request.Method = HttpMethod.Post;
                request.RequestUri = new Uri(host + apiPath + "/getmeasure");
                request.Content = new StringContent(parameters.ToString(), Encoding.UTF8, "application/x-www-form-urlencoded");
                HttpResponseMessage response = await client.SendAsync(request);
                string responseBody = await response.Content.ReadAsStringAsync();
                MeasuresData measuresData = MeasuresData.FromJson(responseBody);
                if (measuresData.Body == null)
                {
                    ErrorMeasuresData errorMeasuresData = ErrorMeasuresData.FromJson(responseBody);
                    _errorMessage = errorMeasuresData.Error.Message;
                    return null;
                }
                return measuresData;
            }
        } 
        #endregion

        internal async Task<HomesData> GetHomesData()
        {
            if (!await CheckConnectionAsync())
                return null;
            var parameters = HttpUtility.ParseQueryString(string.Empty);

            using (HttpClient client = new HttpClient())
            using (HttpRequestMessage request = new HttpRequestMessage())
            {
                request.Method = HttpMethod.Post;
                request.RequestUri = new Uri(host + apiPath + "/homesdata");

                request.Headers.Authorization = new AuthenticationHeaderValue("Bearer", _accessToken);
                request.Content = new StringContent(parameters.ToString(), Encoding.UTF8, "application/x-www-form-urlencoded");
                HttpResponseMessage response = await client.SendAsync(request);
                string responseBody = await response.Content.ReadAsStringAsync();
                HomesData homesData = HomesData.FromJson(responseBody);
                if (homesData.Body == null)
                {
                    ErrorMeasuresData errorMeasuresData = ErrorMeasuresData.FromJson(responseBody);
                    _errorMessage = errorMeasuresData.Error.Message;
                    return null;
                }
                return homesData;
            }
        }
    }
}
