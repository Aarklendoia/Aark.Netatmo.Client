using Aark.Netatmo.SDK.Helpers;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Text;

namespace Aark.Netatmo.SDK.Energy
{
    /// <summary>
    /// Represents a zone of the heating schedule.
    /// </summary>
    public class Zone
    {
        /// <summary>
        /// Zone identifier.
        /// </summary>
        public string Id { get; private set; }
        /// <summary>
        /// 
        /// </summary>
        public ZoneType ZoneType { get; private set; }
        /// <summary>
        /// Name of the zone.
        /// </summary>
        public string Name { get; private set; }
        /// <summary>
        /// Temperature list requested and scheduled for each room.
        /// </summary>
        public ObservableCollection<RoomData> Rooms { get; private set; }
    }
}
