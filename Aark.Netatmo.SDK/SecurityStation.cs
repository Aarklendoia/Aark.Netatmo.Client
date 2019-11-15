using Aark.Netatmo.SDK.Helpers;
using Aark.Netatmo.SDK.Models;
using Aark.Netatmo.SDK.Models.Common;
using Aark.Netatmo.SDK.Models.Security;
using Aark.Netatmo.SDK.Security;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Globalization;
using System.Threading.Tasks;

namespace Aark.Netatmo.SDK
{
    /// <summary>
    /// Netatmo Security station.
    /// </summary>
    public class SecurityStation
    {
        private const string ApiUrl = "https://api.netatmo.com/api";
        private const string CameraImageUrl = ApiUrl + "/getcamerapicture";
        private const string LocalStream = "/index_local.m3u8";
        private const string LiveLocalStream = "/live" + LocalStream;
        private const string DistantStream = "/index.m3u8";
        private const string LiveDistantStream = "/live" + DistantStream;
        private const string VodPath = "/vod/";

        private readonly APICommands _aPICommands;

        /// <summary>
        /// List of the homes.
        /// </summary>
        public ObservableCollection<Home> Homes { get; private set; } = new ObservableCollection<Home>();
        /// <summary>
        /// User related information.
        /// </summary>
        public User User { get; set; } = new User();
        /// <summary>
        /// Specifies whether tags should be displayed in the timeline.
        /// </summary>
        public bool ShowTags { get; set; }

        internal SecurityStation(APICommands aPICommands)
        {
            _aPICommands = aPICommands;
        }

        internal async Task<bool> LoadDataAsync()
        {
            var homeData = await _aPICommands.GetHomeData().ConfigureAwait(false);
            if (homeData == null)
                return false;
            foreach (HomeData.Home datahome in homeData.Body.Homes)
            {
                Home home = new Home();
                if (home.Load(datahome))
                    Homes.Add(home);
            }
            User.RegionInfo = new RegionInfo(homeData.Body.User.Country);
            User.Language = homeData.Body.User.Lang;
            User.CultureInfo = new CultureInfo(homeData.Body.User.RegLocale);
            ShowTags = homeData.Body.GlobalInfo.ShowTags;
            return true;
        }

        internal async Task<bool> GetNextEvents(string homeId, string eventId)
        {
            foreach (Home home in Homes)
            {
                if (home.Id == homeId)
                {
                    NextEvents nextEvents = await _aPICommands.GetNextEvents(homeId, eventId).ConfigureAwait(false);
                    if (nextEvents != null)
                    {
                        home.AddEvents(nextEvents.Body.Events);
                        return true;
                    }
                }
            }
            return false;
        }

        internal async Task<ObservableCollection<SecurityEvent>> GetLastEventsFor(string homeId, string personId)
        {
            ObservableCollection<SecurityEvent> events = new ObservableCollection<SecurityEvent>();
            foreach (Home home in Homes)
            {
                if (home.Id == homeId)
                {
                    NextEvents nextEvents = await _aPICommands.GetLastEventOf(homeId, personId).ConfigureAwait(false);
                    if (nextEvents != null)
                    {
                        foreach (HomeData.Event rawEvent in nextEvents.Body.Events)
                        {
                            SecurityEvent newEvent = Home.CreateNewSecurityEvent(rawEvent);
                            events.Add(newEvent);
                        }
                        return events;
                    }
                }
            }
            return events;
        }

        internal async Task<bool> GetEventsUntil(string homeId, string eventId)
        {
            foreach (Home home in Homes)
            {
                if (home.Id == homeId)
                {
                    NextEvents nextEvents = await _aPICommands.GetEventsUntil(homeId, eventId).ConfigureAwait(false);
                    if (nextEvents != null)
                    {
                        home.AddEvents(nextEvents.Body.Events);
                        return true;
                    }
                }
            }
            return false;
        }

        internal static Uri GetCameraPicture(string imageId, string securityKey)
        {
            UriBuilder uriBuilder = new UriBuilder(CameraImageUrl)
            {
                Query = "image_id=" + imageId + "&key=" + securityKey
            };
            return uriBuilder.Uri;
        }

        internal async Task<Uri> GetLiveStream(string cameraId)
        {
            Uri liveStreamUri;
            if (GetCamera(cameraId, out Uri vpnUri))
            {
                Uri firstLocalUrl = await APICommands.Ping(vpnUri).ConfigureAwait(false);
                Uri secondLocalUrl = await APICommands.Ping(firstLocalUrl).ConfigureAwait(false);
                if (firstLocalUrl == secondLocalUrl)
                {
                    UriBuilder uriBuilder = new UriBuilder(vpnUri);
                    uriBuilder.Path += LiveLocalStream;
                    liveStreamUri = uriBuilder.Uri;
                }
                else
                {
                    UriBuilder uriBuilder = new UriBuilder(vpnUri);
                    uriBuilder.Path += LiveDistantStream;
                    liveStreamUri = uriBuilder.Uri;
                }
            }
            else
            {
                UriBuilder uriBuilder = new UriBuilder(vpnUri);
                uriBuilder.Path += LiveDistantStream;
                liveStreamUri = uriBuilder.Uri;
            }
            return liveStreamUri;
        }

        internal async Task<Uri> GetVodStream(string cameraId, string videoId)
        {
            Uri vodStreamUri;
            if (GetCamera(cameraId, out Uri vpnUri))
            {
                Uri firstLocalUrl = await APICommands.Ping(vpnUri).ConfigureAwait(false);
                Uri secondLocalUrl = await APICommands.Ping(firstLocalUrl).ConfigureAwait(false);
                if (firstLocalUrl == secondLocalUrl)
                {
                    UriBuilder uriBuilder = new UriBuilder(vpnUri);
                    uriBuilder.Path += VodPath + videoId + LocalStream;
                    vodStreamUri = uriBuilder.Uri;
                }
                else
                {
                    UriBuilder uriBuilder = new UriBuilder(vpnUri);
                    uriBuilder.Path += VodPath + videoId + DistantStream;
                    vodStreamUri = uriBuilder.Uri;
                }
            }
            else
            {
                UriBuilder uriBuilder = new UriBuilder(vpnUri);
                uriBuilder.Path += VodPath + videoId + DistantStream;
                vodStreamUri = uriBuilder.Uri;
            }
            return vodStreamUri;
        }

        private bool GetCamera(string cameraId, out Uri vpnUri)
        {
            vpnUri = null;
            foreach (Home home in Homes)
            {
                foreach (Camera camera in home.Cameras)
                {
                    if (camera.Id == cameraId)
                    {
                        vpnUri = camera.VpnUrl;
                        return camera.IsLocal;
                    }
                }
            }
            return false;
        }

        internal async Task<bool> SetPersonsHome(string homeId, List<string> personIds)
        {
            SimpleAnswer simpleAnswer = await _aPICommands.SetPersonsHome(homeId, personIds).ConfigureAwait(false);
            return simpleAnswer.Status == "ok";
        }

        internal async Task<bool> SetPersonAway(string homeId, string personId)
        {
            SimpleAnswer simpleAnswer = await _aPICommands.SetPersonAway(homeId, personId).ConfigureAwait(false);
            return simpleAnswer.Status == "ok";
        }

        internal async Task<bool> SetHomeEmpty(string homeId)
        {
            SimpleAnswer simpleAnswer = await _aPICommands.SetPersonAway(homeId).ConfigureAwait(false);
            return simpleAnswer.Status == "ok";
        }
    }
}