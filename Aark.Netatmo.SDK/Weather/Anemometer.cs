using Aark.Netatmo.SDK.Helpers;
using Aark.Netatmo.SDK.Models;
using System;
using System.Collections.ObjectModel;
using System.Threading.Tasks;

namespace Aark.Netatmo.SDK.Weather
{
    /// <summary>
    /// Netatmo anenometer.
    /// </summary>
    public class Anemometer : ExtraDevice
    {
        //WindStrength(km/h), WindAngle(angles), Guststrength(km/h), GustAngle
        /// <summary>
        /// Current wind strength in km/h.
        /// </summary>
        public long? WindStrength { get; internal set; }
        /// <summary>
        /// Current wind angle in degrees.
        /// </summary>
        public long? WindAngle { get; internal set; }
        /// <summary>
        /// Current gust strength in km/h.
        /// </summary>
        public long? GustStrength { get; internal set; }
        /// <summary>
        /// Current gust angle in degrees.
        /// </summary>
        public long? GustAngle { get; internal set; }
        /// <summary>
        /// Current maximal wind strength. 
        /// </summary>
        public long? MaxWindStrength { get; internal set; }
        /// <summary>
        /// Current maximal wind angle.
        /// </summary>
        public long? MaxWindAngle { get; internal set; }
        /// <summary>
        /// Date of the maximal wind strength.
        /// </summary>
        public DateTime DateMaxWindStrength { get; internal set; }
        /// <summary>
        /// History of the wind strength measures for the period defined.
        /// </summary>
        public ObservableCollection<Measure> HistoryWindStrength { get; internal set; } = new ObservableCollection<Measure>();
        /// <summary>
        /// History of the wind angle measures for the period defined.
        /// </summary>
        public ObservableCollection<Measure> HistoryWindAngle { get; internal set; } = new ObservableCollection<Measure>();
        /// <summary>
        /// History of the gust strength measures for the period defined.
        /// </summary>
        public ObservableCollection<Measure> HistoryGustStrength { get; internal set; } = new ObservableCollection<Measure>();
        /// <summary>
        /// History of the gust angle measures for the period defined.
        /// </summary>
        public ObservableCollection<Measure> HistoryGustAngle { get; internal set; } = new ObservableCollection<Measure>();
        /// <summary>
        /// History of the dateof the max gust strength measures for the period defined.
        /// </summary>
        public ObservableCollection<Measure> HistoryDateMaxGust { get; internal set; } = new ObservableCollection<Measure>();

        internal Anemometer(APICommands aPICommands) : base(aPICommands)
        {

        }

        internal override void Load(WeatherModule weatherModule, string baseId)
        {
            Available = true;
            BaseId = baseId;
            Id = weatherModule.Id;
            Name = weatherModule.ModuleName;
            LastSetup = weatherModule.LastSetup.ToLocalDateTime();
            BatteryPercent = weatherModule.BatteryPercent;
            Reachable = weatherModule.Reachable;
            Firmware = weatherModule.Firmware;
            LastMessage = weatherModule.LastMessage.ToLocalDateTime();
            LastSeen = weatherModule.LastSeen.ToLocalDateTime();
            RadioFrequenceStatus = weatherModule.RfStatus.ToSignalStatus();
            BatteryStatus = weatherModule.BatteryVp.ToAnenometerBatteryStatus();
            if (Reachable)
            {
                Time = weatherModule.DashboardData.TimeUtc.ToLocalDateTime();
                WindStrength = weatherModule.DashboardData.WindStrength;
                WindAngle = weatherModule.DashboardData.WindAngle;
                MaxWindStrength = weatherModule.DashboardData.MaxWindStr;
                MaxWindAngle = weatherModule.DashboardData.MaxWindAngle;
                DateMaxWindStrength = weatherModule.DashboardData.DateMaxWindStr.ToLocalDateTime();
                GustStrength = weatherModule.DashboardData.GustStrength;
                GustAngle = weatherModule.DashboardData.GustAngle;
            }
        }

        /// <summary>
        /// Loads the historical data according to the defined filter.
        /// </summary>
        /// <param name="measuresFilters">List of filters to be applied.</param>
        /// <returns></returns>
        public override async Task LoadMeasuresAsync(MeasuresFilters measuresFilters)
        {
            _valueIndex = 0;
            MeasureScale measureScale = GetScaleFromDateRange(_dateBegin, _dateEnd);
            _measuresData = await _aPICommands.GetMeasure(BaseId, measureScale, measuresFilters, _dateBegin, _dateEnd, Id);
            if (_measuresData == null)
                return;
            // Wind
            if (measuresFilters.HasFlag(MeasuresFilters.WindStrength))
                LoadData(_measuresData, HistoryWindStrength);
            if (measuresFilters.HasFlag(MeasuresFilters.WindAngle))
                LoadData(_measuresData, HistoryWindAngle);
            // Gust
            if (measuresFilters.HasFlag(MeasuresFilters.GustStrength))
                LoadData(_measuresData, HistoryGustStrength);
            if (measuresFilters.HasFlag(MeasuresFilters.GustAngle))
                LoadData(_measuresData, HistoryGustAngle);
            if (measuresFilters.HasFlag(MeasuresFilters.DateMaxGust))
                LoadData(_measuresData, HistoryDateMaxGust);
        }
    }
}
