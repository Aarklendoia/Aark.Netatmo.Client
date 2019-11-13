using Aark.Netatmo.SDK.Helpers;
using System;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;
using System.Web;
using System.Globalization;
using Aark.Netatmo.SDK.Models.Common;
using Aark.Netatmo.SDK.Models.Weather;
using Aark.Netatmo.SDK.Models.Security;
using Aark.Netatmo.SDK.Models.Energy;

namespace Aark.Netatmo.SDK.Models
{
    internal class APICommands
    {
        private string _accessToken = "";
        private DateTime _expireAt = DateTime.Now;
        private string _resfreshToken = "";
        private string _errorMessage = "";

        private const string host = "https://api.netatmo.com";
        private const string authPath = "/oauth2/token";
        private const string apiPath = "/api";

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
            if (string.IsNullOrEmpty(Username) || string.IsNullOrEmpty(Password))
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
                HttpResponseMessage response = await client.SendAsync(request).ConfigureAwait(false);
                string responseBody = await response.Content.ReadAsStringAsync().ConfigureAwait(false);
                AccessData accessData = new AccessData().FromJson(responseBody);
                _accessToken = accessData.AccessToken;
                _expireAt = DateTime.Now.AddSeconds(accessData.ExpiresIn);
                _resfreshToken = accessData.RefreshToken;
                if (_accessToken == null)
                {
                    ErrorData _errorData = new ErrorData().FromJson(responseBody);
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
                HttpResponseMessage response = await client.SendAsync(request).ConfigureAwait(false);
                string responseBody = await response.Content.ReadAsStringAsync().ConfigureAwait(false);
                AccessData accessData = new AccessData().FromJson(responseBody);
                _accessToken = accessData.AccessToken;
                _expireAt = DateTime.Now.AddSeconds(accessData.ExpiresIn);
                _resfreshToken = accessData.RefreshToken;
                return true;
            }
        }

        private async Task<bool> CheckConnectionAsync()
        {
            if (_accessToken.Length == 0)
                return await AuthentificationAsync().ConfigureAwait(false);
            else
            {
                if (DateTime.Now.CompareTo(_expireAt) > 0)
                    return await ResfreshAuthentificationAsync().ConfigureAwait(false);
                else
                    return true;
            }
        }
        #endregion

        #region WeatherStation
        internal async Task<StationData> GetStationsData(string deviceId = "", bool favorite = false)
        {
            if (!await CheckConnectionAsync().ConfigureAwait(false))
                return null;
            var parameters = HttpUtility.ParseQueryString(string.Empty);
            parameters["access_token"] = _accessToken;
            if (deviceId.Length > 0)
                parameters["device_id"] = deviceId;
            parameters["get_favorites"] = favorite.ToString(CultureInfo.InvariantCulture);

            using (HttpClient client = new HttpClient())
            using (HttpRequestMessage request = new HttpRequestMessage())
            {
                request.Method = HttpMethod.Post;
                request.RequestUri = new Uri(host + apiPath + "/getstationsdata");
                request.Content = new StringContent(parameters.ToString(), Encoding.UTF8, "application/x-www-form-urlencoded");
                HttpResponseMessage response = await client.SendAsync(request).ConfigureAwait(false);
                string responseBody = await response.Content.ReadAsStringAsync().ConfigureAwait(false);
                StationData stationData = new StationData().FromJson(responseBody);
                if (stationData.Status != "ok")
                {
                    ErrorMeasuresData errorMeasuresData = new ErrorMeasuresData().FromJson(responseBody);
                    _errorMessage = errorMeasuresData.Error.Message;
                    return null;
                }
                return stationData;
            }
        }

        internal async Task<MeasuresData> GetMeasure(string deviceId, MeasureScale scale, MeasuresFilters measureType, DateTime? dateBegin, DateTime? dateEnd, string moduleId = "", int limit = 1024)
        {
            if (!await CheckConnectionAsync().ConfigureAwait(false))
                return null;
            var parameters = HttpUtility.ParseQueryString(string.Empty);
            parameters["access_token"] = _accessToken;
            parameters["device_id"] = deviceId;
            parameters["scale"] = scale.ToJsonString();
            parameters["type"] = measureType.ToJsonString();
            if (dateBegin != null)
                parameters["date_begin"] = dateBegin.FromLocalDateTime().ToString(CultureInfo.InvariantCulture);
            if (dateEnd != null)
                parameters["date_end"] = dateEnd.FromLocalDateTime().ToString(CultureInfo.InvariantCulture);
            parameters["optimize"] = true.ToString(CultureInfo.InvariantCulture).ToUpperInvariant();
            parameters["limit"] = limit.ToString(CultureInfo.InvariantCulture);
            if (moduleId.Length > 0)
                parameters["module_id"] = moduleId;

            using (HttpClient client = new HttpClient())
            using (HttpRequestMessage request = new HttpRequestMessage())
            {
                request.Method = HttpMethod.Post;
                request.RequestUri = new Uri(host + apiPath + "/getmeasure");
                request.Content = new StringContent(parameters.ToString(), Encoding.UTF8, "application/x-www-form-urlencoded");
                HttpResponseMessage response = await client.SendAsync(request).ConfigureAwait(false);
                string responseBody = await response.Content.ReadAsStringAsync().ConfigureAwait(false);
                MeasuresData measuresData = new MeasuresData().FromJson(responseBody);
                if (measuresData.Content == null)
                {
                    ErrorMeasuresData errorMeasuresData = new ErrorMeasuresData().FromJson(responseBody);
                    _errorMessage = errorMeasuresData.Error.Message;
                    return null;
                }
                return measuresData;
            }
        }
        #endregion

        #region EnergyStation
        internal async Task<HomesData> GetHomesData()
        {
            if (!await CheckConnectionAsync().ConfigureAwait(false))
                return null;
            var parameters = HttpUtility.ParseQueryString(string.Empty);

            using (HttpClient client = new HttpClient())
            using (HttpRequestMessage request = new HttpRequestMessage())
            {
                request.Method = HttpMethod.Post;
                request.RequestUri = new Uri(host + apiPath + "/homesdata");

                request.Headers.Authorization = new AuthenticationHeaderValue("Bearer", _accessToken);
                request.Content = new StringContent(parameters.ToString(), Encoding.UTF8, "application/x-www-form-urlencoded");
                HttpResponseMessage response = await client.SendAsync(request).ConfigureAwait(false);
                string responseBody = await response.Content.ReadAsStringAsync().ConfigureAwait(false);
                HomesData homesData = new HomesData().FromJson(responseBody);
                if (homesData.Status != "ok")
                {
                    ErrorMeasuresData errorMeasuresData = new ErrorMeasuresData().FromJson(responseBody);
                    _errorMessage = errorMeasuresData.Error.Message;
                    return null;
                }
                return homesData;
            }
        }

        internal async Task<HomeStatus> GetHomeStatus(string homeId)
        {
            if (!await CheckConnectionAsync().ConfigureAwait(false))
                return null;
            var parameters = HttpUtility.ParseQueryString(string.Empty);
            parameters["home_id"] = homeId;
            parameters["device_types"] = ""; // array of device types

            using (HttpClient client = new HttpClient())
            using (HttpRequestMessage request = new HttpRequestMessage())
            {
                request.Method = HttpMethod.Post;
                request.RequestUri = new Uri(host + apiPath + "/homestatus");

                request.Headers.Authorization = new AuthenticationHeaderValue("Bearer", _accessToken);
                request.Content = new StringContent(parameters.ToString(), Encoding.UTF8, "application/x-www-form-urlencoded");
                HttpResponseMessage response = await client.SendAsync(request).ConfigureAwait(false);
                string responseBody = await response.Content.ReadAsStringAsync().ConfigureAwait(false);
                HomeStatus homeStatus = new HomeStatus().FromJson(responseBody);
                if (homeStatus.Status != "ok")
                {
                    ErrorMeasuresData errorMeasuresData = new ErrorMeasuresData().FromJson(responseBody);
                    _errorMessage = errorMeasuresData.Error.Message;
                    return null;
                }
                return homeStatus;
            }
        }

        internal async Task<RoomMeasures> GetRoomMeasures(string homeId, string roomId, MeasureScale scale, MeasuresFilters measureType, DateTime? dateBegin, DateTime? dateEnd, int limit = 1024)
        {
            if (!await CheckConnectionAsync().ConfigureAwait(false))
                return null;
            var parameters = HttpUtility.ParseQueryString(string.Empty);
            parameters["home_id"] = homeId;
            parameters["room_id"] = roomId;
            parameters["scale"] = scale.ToJsonString();
            parameters["type"] = measureType.ToJsonString();
            if (dateBegin != null)
                parameters["date_begin"] = dateBegin.FromLocalDateTime().ToString(CultureInfo.InvariantCulture);
            if (dateEnd != null)
                parameters["date_end"] = dateEnd.FromLocalDateTime().ToString(CultureInfo.InvariantCulture);
            parameters["optimize"] = true.ToString(CultureInfo.InvariantCulture).ToUpperInvariant();
            parameters["limit"] = limit.ToString(CultureInfo.InvariantCulture);

            using (HttpClient client = new HttpClient())
            using (HttpRequestMessage request = new HttpRequestMessage())
            {
                request.Method = HttpMethod.Post;
                request.RequestUri = new Uri(host + apiPath + "/getroommeasure");

                request.Headers.Authorization = new AuthenticationHeaderValue("Bearer", _accessToken);
                request.Content = new StringContent(parameters.ToString(), Encoding.UTF8, "application/x-www-form-urlencoded");
                HttpResponseMessage response = await client.SendAsync(request).ConfigureAwait(false);
                string responseBody = await response.Content.ReadAsStringAsync().ConfigureAwait(false);
                RoomMeasures roomMeasures = new RoomMeasures().FromJson(responseBody);
                if (roomMeasures.Body == null)
                {
                    ErrorMeasuresData errorMeasuresData = new ErrorMeasuresData().FromJson(responseBody);
                    _errorMessage = errorMeasuresData.Error.Message;
                    return null;
                }
                return roomMeasures;
            }
        }

        internal async Task<SimpleAnswer> SetThermMod(string homeId, ThermostatMode thermalMode, DateTime endTime)
        {
            if (!await CheckConnectionAsync().ConfigureAwait(false))
                return null;
            var parameters = HttpUtility.ParseQueryString(string.Empty);
            parameters["home_id"] = homeId;
            parameters["mode"] = thermalMode.FromThermostatMode();
            parameters["endtime"] = endTime.FromLocalDateTime().ToString(CultureInfo.InvariantCulture);

            using (HttpClient client = new HttpClient())
            using (HttpRequestMessage request = new HttpRequestMessage())
            {
                request.Method = HttpMethod.Post;
                request.RequestUri = new Uri(host + apiPath + "/setthermmode");

                request.Headers.Authorization = new AuthenticationHeaderValue("Bearer", _accessToken);
                request.Content = new StringContent(parameters.ToString(), Encoding.UTF8, "application/x-www-form-urlencoded");
                HttpResponseMessage response = await client.SendAsync(request).ConfigureAwait(false);
                string responseBody = await response.Content.ReadAsStringAsync().ConfigureAwait(false);
                SimpleAnswer simpleAnswer = new SimpleAnswer().FromJson(responseBody);
                if (simpleAnswer.Status == null)
                {
                    ErrorMeasuresData errorMeasuresData = new ErrorMeasuresData().FromJson(responseBody);
                    _errorMessage = errorMeasuresData.Error.Message;
                    return null;
                }
                return simpleAnswer;
            }
        }
        #endregion

        #region SecurityStation
        internal async Task<HomeData> GetHomeData(string homeId = "", int size = 30)
        {
            if (!await CheckConnectionAsync().ConfigureAwait(false))
                return null;
            var parameters = HttpUtility.ParseQueryString(string.Empty);
            parameters["home_id"] = homeId;
            parameters["size"] = size.ToString(CultureInfo.InvariantCulture);

            using (HttpClient client = new HttpClient())
            using (HttpRequestMessage request = new HttpRequestMessage())
            {
                request.Method = HttpMethod.Post;
                request.RequestUri = new Uri(host + apiPath + "/gethomedata");

                request.Headers.Authorization = new AuthenticationHeaderValue("Bearer", _accessToken);
                request.Content = new StringContent(parameters.ToString(), Encoding.UTF8, "application/x-www-form-urlencoded");
                HttpResponseMessage response = await client.SendAsync(request).ConfigureAwait(false);
                string responseBody = await response.Content.ReadAsStringAsync().ConfigureAwait(false);
                HomeData homeData = new HomeData().FromJson(responseBody);
                if (homeData.Status != "ok")
                {
                    ErrorMeasuresData errorMeasuresData = new ErrorMeasuresData().FromJson(responseBody);
                    _errorMessage = errorMeasuresData.Error.Message;
                    return null;
                }
                return homeData;
            }
        }
        #endregion
    }
}
