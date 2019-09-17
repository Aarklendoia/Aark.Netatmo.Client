﻿using Aark.Netatmo.SDK.Helpers;
using Aark.Netatmo.SDK.Models;
using System;
using System.Collections.ObjectModel;
using System.Threading.Tasks;

namespace Aark.Netatmo.SDK.Weather
{
    /// <summary>
    /// Netatmo indoor weather station module.
    /// </summary>
    public class IndoorModule : ExtraDevice
    {
        /// <summary>
        /// Current temperature in °C.
        /// </summary>
        public double? Temperature { get; internal set; }
        /// <summary>
        /// Current min temperature in °C.
        /// </summary>
        public double? TemperatureMin { get; internal set; }
        /// <summary>
        /// Date of the current minimal temperature.
        /// </summary>
        public DateTime TemperatureMinDate { get; internal set; }
        /// <summary>
        /// Current max temperature in °C.
        /// </summary>
        public double? TemperatureMax { get; internal set; }
        /// <summary>
        /// Date of the current maximal temperature.
        /// </summary>
        public DateTime TemperatureMaxDate { get; internal set; }
        /// <summary>
        /// Current temperature trend.
        /// </summary>
        public string TemperatureTrend { get; internal set; }
        /// <summary>
        /// Current humidity in %.
        /// </summary>
        public long? Humidity { get; internal set; }
        /// <summary>
        /// History of the temperature measures for the period defined.
        /// </summary>
        public ObservableCollection<Measure> HistoryTemperatures { get; internal set; } = new ObservableCollection<Measure>();
        /// <summary>
        /// History of the minimal temperature measures for the period defined.
        /// </summary>
        public ObservableCollection<Measure> HistoryMinTemperatures { get; private set; } = new ObservableCollection<Measure>();
        /// <summary>
        /// History of the maximal temperature measures for the period defined.
        /// </summary>
        public ObservableCollection<Measure> HistoryMaxTemperatures { get; private set; } = new ObservableCollection<Measure>();
        /// <summary>
        /// History of the Co2 measures for the period defined.
        /// </summary>
        public ObservableCollection<Measure> HistoryCo2 { get; internal set; } = new ObservableCollection<Measure>();
        /// <summary>
        /// History of the date of minimal Co2 measures for the period defined.
        /// </summary>
        public ObservableCollection<Measure> HistoryDateMinCo2 { get; internal set; } = new ObservableCollection<Measure>();
        /// <summary>
        /// History of the date of mmaximal Co2 measures for the period defined.
        /// </summary>
        public ObservableCollection<Measure> HistoryDateMaxCo2 { get; internal set; } = new ObservableCollection<Measure>();
        /// <summary>
        /// History of the humidity measures for the period defined.
        /// </summary>
        public ObservableCollection<Measure> HistoryHumidity { get; internal set; } = new ObservableCollection<Measure>();
        /// <summary>
        /// History of the minimal humidity measures for the period defined.
        /// </summary>
        public ObservableCollection<Measure> HistoryMinHumidity { get; internal set; } = new ObservableCollection<Measure>();
        /// <summary>
        /// History of the maximal humidity measures for the period defined.
        /// </summary>
        public ObservableCollection<Measure> HistoryMaxHumidity { get; internal set; } = new ObservableCollection<Measure>();
        /// <summary>
        /// History of the date of maximal humidity measures for the period defined.
        /// </summary>
        public ObservableCollection<Measure> HistoryDateMaxHumidity { get; internal set; } = new ObservableCollection<Measure>();

        internal IndoorModule(APICommands aPICommands) : base(aPICommands)
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
            // Temperatures
            if (measuresFilters.HasFlag(MeasuresFilters.Temperature))
                LoadData(_measuresData, HistoryTemperatures);
            if (measuresFilters.HasFlag(MeasuresFilters.MinTemperature))
                LoadData(_measuresData, HistoryMinTemperatures);
            if (measuresFilters.HasFlag(MeasuresFilters.MaxTemperature))
                LoadData(_measuresData, HistoryMaxTemperatures);
            // Co2
            if (measuresFilters.HasFlag(MeasuresFilters.Co2))
                LoadData(_measuresData, HistoryCo2);
            if (measuresFilters.HasFlag(MeasuresFilters.DateMinCo2))
                LoadData(_measuresData, HistoryDateMinCo2);
            if (measuresFilters.HasFlag(MeasuresFilters.DateMaxCo2))
                LoadData(_measuresData, HistoryDateMaxCo2);
            // Humidity
            if (measuresFilters.HasFlag(MeasuresFilters.Humidity))
                LoadData(_measuresData, HistoryHumidity);
            if (measuresFilters.HasFlag(MeasuresFilters.MinHumidity))
                LoadData(_measuresData, HistoryMinHumidity);
            if (measuresFilters.HasFlag(MeasuresFilters.MaxHumidity))
                LoadData(_measuresData, HistoryMaxHumidity);
            if (measuresFilters.HasFlag(MeasuresFilters.DateMaxHumidity))
                LoadData(_measuresData, HistoryDateMaxHumidity);
        }
    }
}