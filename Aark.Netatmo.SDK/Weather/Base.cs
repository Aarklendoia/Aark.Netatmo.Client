using Aark.Netatmo.SDK.Helpers;
using Aark.Netatmo.SDK.Models;
using Aark.Netatmo.SDK.Models.Weather;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Text;
using System.Threading.Tasks;

namespace Aark.Netatmo.SDK.Weather
{
    /// <summary>
    /// Base of the weather station.
    /// </summary>
    public class Base : CommonWeatherDevice
    {
        /// <summary>
        /// Date when the Weather station was set up.
        /// </summary>
        public DateTime DateSetup { get; internal set; }
        /// <summary>
        /// Wifi status per Base station.
        /// </summary>
        public SignalStatus WifiStatus { get; internal set; }
        /// <summary>
        /// Indicates whether a calibration is in progress.
        /// </summary>
        public bool CO2Calibrating { get; internal set; }
        /// <summary>
        /// Station name.
        /// </summary>
        public string StationName { get; internal set; }
        /// <summary>
        /// Current temperature in °C.
        /// </summary>
        public double Temperature { get; internal set; }
        /// <summary>
        /// Current Co2 in ppm.
        /// </summary>
        public long Co2 { get; internal set; }
        /// <summary>
        /// Current humidity in %.
        /// </summary>
        public long Humidity { get; internal set; }
        /// <summary>
        /// Current noise in dB.
        /// </summary>
        public long Noise { get; internal set; }
        /// <summary>
        /// Current mbar.
        /// </summary>
        public double Pressure { get; internal set; }
        /// <summary>
        /// Current max temperature in °C.
        /// </summary>
        public double TemperatureMax { get; internal set; }
        /// <summary>
        /// Current min temperature in °C.
        /// </summary>
        public double TemperatureMin { get; internal set; }
        /// <summary>
        /// Current temperature trend.
        /// </summary>
        public Trend TemperatureTrend { get; internal set; }
        /// <summary>
        /// Pressure trend.
        /// </summary>
        public Trend PresureTrend { get; internal set; }
        /// <summary>
        /// Date of the current maximal temperature.
        /// </summary>
        public DateTime TemperatureMaxDate { get; internal set; }
        /// <summary>
        /// Date of the current minimal temperature.
        /// </summary>
        public DateTime TemperatureMinDate { get; internal set; }
        /// <summary>
        /// Current absolute pressure.
        /// </summary>
        public double AbsolutePressure { get; internal set; }
        /// <summary>
        /// Date of last upgrade.
        /// </summary>
        public DateTime LastUpgrade { get; internal set; }
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
        /// <summary>
        /// History of the noise measures for the period defined.
        /// </summary>
        public ObservableCollection<Measure> HistoryNoise { get; internal set; } = new ObservableCollection<Measure>();
        /// <summary>
        /// History of the minimal noise measures for the period defined.
        /// </summary>
        public ObservableCollection<Measure> HistoryMinNoise { get; internal set; } = new ObservableCollection<Measure>();
        /// <summary>
        /// History of the maximal noise measures for the period defined.
        /// </summary>
        public ObservableCollection<Measure> HistoryMaxNoise { get; internal set; } = new ObservableCollection<Measure>();
        /// <summary>
        /// History of the date of minimal noise measures for the period defined.
        /// </summary>
        public ObservableCollection<Measure> HistoryDateMinNoise { get; internal set; } = new ObservableCollection<Measure>();
        /// <summary>
        /// History of the date of maximal noise measures for the period defined.
        /// </summary>
        public ObservableCollection<Measure> HistoryDateMaxNoise { get; internal set; } = new ObservableCollection<Measure>();
        /// <summary>
        /// History of the temperature pressure for the period defined.
        /// </summary>
        public ObservableCollection<Measure> HistoryPressure { get; internal set; } = new ObservableCollection<Measure>();
        /// <summary>
        /// History of the minimal pressure measures for the period defined.
        /// </summary>
        public ObservableCollection<Measure> HistoryMinPressure { get; internal set; } = new ObservableCollection<Measure>();
        /// <summary>
        /// History of the maximal pressure measures for the period defined.
        /// </summary>
        public ObservableCollection<Measure> HistoryMaxPressure { get; internal set; } = new ObservableCollection<Measure>();
        /// <summary>
        /// History of the date of minimal pressure measures for the period defined.
        /// </summary>
        public ObservableCollection<Measure> HistoryDateMinPressure { get; internal set; } = new ObservableCollection<Measure>();
        /// <summary>
        /// History of the date of maximal pressure measures for the period defined.
        /// </summary>
        public ObservableCollection<Measure> HistoryDateMaxPressure { get; internal set; } = new ObservableCollection<Measure>();


        internal Base(APICommands aPICommands) : base(aPICommands)
        {

        }

        internal void Load(StationData.Device device)
        {
            Id = device.Id;
            DateSetup = device.DateSetup.ToLocalDateTime();
            LastSetup = device.LastSetup.ToLocalDateTime();
            Name = device.ModuleName;
            Firmware = device.Firmware;
            LastUpgrade = device.LastSetup.ToLocalDateTime();
            WifiStatus = device.WifiStatus.ToSignalStatus();
            Reachable = device.Reachable;
            CO2Calibrating = device.Co2Calibrating;
            StationName = device.StationName;
            if (Reachable)
            {
                Time = device.DashboardData.TimeUtc.ToLocalDateTime();
                Temperature = device.DashboardData.Temperature;
                TemperatureMax = device.DashboardData.MaxTemp;
                TemperatureMaxDate = device.DashboardData.DateMaxTemp.ToLocalDateTime();
                TemperatureMin = device.DashboardData.MinTemp;
                TemperatureMinDate = device.DashboardData.DateMinTemp.ToLocalDateTime();
                TemperatureTrend = device.DashboardData.TempTrend.ToTrend();
                Co2 = device.DashboardData.Co2;
                Humidity = device.DashboardData.Humidity;
                Noise = device.DashboardData.Noise;
                Pressure = device.DashboardData.Pressure;
                AbsolutePressure = device.DashboardData.AbsolutePressure;
                PresureTrend = device.DashboardData.PressureTrend.ToTrend();
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
            _measuresData = await _aPICommands.GetMeasure(Id, measureScale, measuresFilters, _dateBegin, _dateEnd).ConfigureAwait(false);
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
            // Noise
            if (measuresFilters.HasFlag(MeasuresFilters.Noise))
                LoadData(_measuresData, HistoryNoise);
            if (measuresFilters.HasFlag(MeasuresFilters.MinNoise))
                LoadData(_measuresData, HistoryMinNoise);
            if (measuresFilters.HasFlag(MeasuresFilters.MaxNoise))
                LoadData(_measuresData, HistoryMaxNoise);
            if (measuresFilters.HasFlag(MeasuresFilters.DateMinNoise))
                LoadData(_measuresData, HistoryDateMinNoise);
            if (measuresFilters.HasFlag(MeasuresFilters.DateMaxNoise))
                LoadData(_measuresData, HistoryDateMaxNoise);
            // Pressure
            if (measuresFilters.HasFlag(MeasuresFilters.Pressure))
                LoadData(_measuresData, HistoryPressure);
            if (measuresFilters.HasFlag(MeasuresFilters.MinPressure))
                LoadData(_measuresData, HistoryMinPressure);
            if (measuresFilters.HasFlag(MeasuresFilters.MaxPressure))
                LoadData(_measuresData, HistoryMaxPressure);
            if (measuresFilters.HasFlag(MeasuresFilters.DateMinPressure))
                LoadData(_measuresData, HistoryDateMinPressure);
            if (measuresFilters.HasFlag(MeasuresFilters.DateMaxPressure))
                LoadData(_measuresData, HistoryDateMaxPressure);
        }
    }
}
