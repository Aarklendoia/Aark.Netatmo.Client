using Aark.Netatmo.SDK.Helpers;
using Aark.Netatmo.SDK.Models;
using System;
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
            if (measuresFilters.HasFlag(MeasuresFilters.Rain))
                LoadData(_measuresData, HistoryRain);
            if (measuresFilters.HasFlag(MeasuresFilters.SumRain))
                LoadData(_measuresData, HistorySumRain);
        }
    }
}
