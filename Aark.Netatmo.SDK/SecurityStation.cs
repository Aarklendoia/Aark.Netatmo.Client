using Aark.Netatmo.SDK.Models;
using Aark.Netatmo.SDK.Models.Security;
using Aark.Netatmo.SDK.Security;
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
    }
}
