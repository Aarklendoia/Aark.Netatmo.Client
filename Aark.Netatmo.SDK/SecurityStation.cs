using Aark.Netatmo.SDK.Models;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Text;
using System.Threading.Tasks;
using Aark.Netatmo.SDK.Security;
using Aark.Netatmo.SDK.Models.Security;

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
                home.Load(datahome);
                Homes.Add(home);
            }
            return true;
        }
    }
}
