using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Text;

namespace Aark.Netatmo.SDK.Energy
{
    /// <summary>
    /// Define a heating schedule.
    /// </summary>
    public class Schedule
    {
        /// <summary>
        /// Id of the schedule.
        /// </summary>
        public string Id { get; private set; }
        /// <summary>
        /// Zones of the schedule.
        /// </summary>
        public ObservableCollection<Zone> Zones { get; private set; }
        /// <summary>
        /// List of heating values for the zone.
        /// </summary>
        public ObservableCollection<Time> Times { get; private set; }
    }
}
