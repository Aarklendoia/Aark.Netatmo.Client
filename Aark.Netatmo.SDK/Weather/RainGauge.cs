using Aark.Netatmo.SDK.Helpers;
using Aark.Netatmo.SDK.Models;
using Aark.Netatmo.SDK.Models.Weather;
using System.Collections.ObjectModel;
using System.Threading.Tasks;

namespace Aark.Netatmo.SDK.Weather
{
    /// <summary>
    /// Netatmo raingauge.
    /// </summary>
    public class RainGauge : ExtraDevice
    {
        /// <summary>
        /// Rainfall over the last 24 hours.
        /// </summary>
        public double? SumRainLast24h { get; internal set; }
        /// <summary>
        /// Rainfall over the last hour.
        /// </summary>
        public double? SumRainLastHour { get; internal set; }
        /// <summary>
        /// Current rain in mm.
        /// </summary>
        public double? Rain { get; internal set; }
        /// <summary>
        /// History of the rain measures for the period defined.
        /// </summary>
        public ObservableCollection<Measure> HistoryRain { get; internal set; } = new ObservableCollection<Measure>();
        /// <summary>
        /// History of the sum rain measures for the period defined.
        /// </summary>
        public ObservableCollection<Measure> HistorySumRain { get; internal set; } = new ObservableCollection<Measure>();

        internal RainGauge(APICommands aPICommands) : base(aPICommands)
        {

        }

        internal override void Load(StationData.WeatherModule weatherModule, string baseId)
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
            BatteryStatus = weatherModule.BatteryVp.ToRainGaugeBatteryStatus();
            if (Reachable)
            {
                Time = weatherModule.DashboardData.TimeUtc.ToLocalDateTime();
                Rain = weatherModule.DashboardData.Rain;
                SumRainLastHour = weatherModule.DashboardData.SumRain1;
                SumRainLast24h = weatherModule.DashboardData.SumRain24;
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
            _measuresData = await _aPICommands.GetMeasure(BaseId, measureScale, measuresFilters, _dateBegin, _dateEnd, Id).ConfigureAwait(false);
            if (_measuresData == null)
                return;
            if (measuresFilters.HasFlag(MeasuresFilters.Rain))
                LoadData(_measuresData, HistoryRain);
            if (measuresFilters.HasFlag(MeasuresFilters.SumRain))
                LoadData(_measuresData, HistorySumRain);
        }
    }
}
