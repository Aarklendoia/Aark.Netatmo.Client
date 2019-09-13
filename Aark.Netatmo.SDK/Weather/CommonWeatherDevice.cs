using Aark.Netatmo.SDK.Helpers;
using Aark.Netatmo.SDK.Models;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Threading.Tasks;

namespace Aark.Netatmo.SDK.Weather
{
    /// <summary>
    /// Define the common properties and functions of a weather device.
    /// </summary>
    public abstract class CommonWeatherDevice
    {
        internal APICommands _aPICommands;
        internal MeasuresData _measuresData;
        internal DateTime _dateEnd;
        internal DateTime _dateBegin;
        internal int _valueIndex = 0;

        /// <summary>
        /// Id of the device.
        /// </summary>
        public string Id { get; internal set; }
        /// <summary>
        /// Name of the device.
        /// </summary>
        public string Name { get; internal set; }
        /// <summary>
        /// Last setup date of the device.
        /// </summary>
        public DateTime LastSetup { get; internal set; }
        /// <summary>
        /// Indicates whether the device is reachable.
        /// </summary>
        public bool Reachable { get; internal set; }
        /// <summary>
        /// Firmware version.
        /// </summary>
        public long Firmware { get; internal set; }
        /// <summary>
        /// Timestamp when data was measured.
        /// </summary>
        public DateTime Time { get; internal set; }

        internal CommonWeatherDevice(APICommands aPICommands)
        {
            _aPICommands = aPICommands;
        }

        /// <summary>
        /// Defined a period from 1 hour to 1024 months.
        /// </summary>
        /// <param name="dateBegin">Date de début de la période.</param>
        /// <param name="dateEnd">Date de fin de la période.</param>
        /// <returns><see cref="CommonWeatherDevice"/></returns>
        public CommonWeatherDevice DefineDateRange(DateTime dateBegin, DateTime dateEnd)
        {
            if (dateBegin != _dateBegin || dateEnd != _dateEnd)
            {
                _dateBegin = dateBegin;
                _dateEnd = dateEnd;
            }
            return this;
        }

        /// <summary>
        /// Loads the historical data according to the defined filter.
        /// </summary>
        /// <param name="measuresFilters">List of filters to be applied.</param>
        /// <returns></returns>
        public abstract Task LoadMeasuresAsync(MeasuresFilters measuresFilters);

        internal int LoadData(MeasuresData measuresData, ObservableCollection<Measure> measures)
        {
            measures.Clear();
            foreach (Body body in measuresData.Body)
            {
                int valueIndex = 0;
                foreach (List<double?> value in body.Value)
                {
                    AddMeasures(measures, body.BegTime.ToLocalDateTime().AddSeconds(body.StepTime * valueIndex), value);
                    valueIndex++;
                }
            }
            _valueIndex++;
            return measures.Count;
        }

        internal void AddMeasures(ObservableCollection<Measure> measures, DateTime begTime, List<double?> values)
        {
            if (values[_valueIndex] != null)
            {
                if (values[_valueIndex] > 10000) // The value is a date.
                    measures.Add(new Measure(begTime, ((long)values[_valueIndex]).ToLocalDateTime()));
                else
                    measures.Add(new Measure(begTime, values[_valueIndex]));
            }
        }

        internal MeasureScale GetScaleFromDateRange(DateTime dateBegin, DateTime dateEnd)
        {
            switch ((dateEnd - dateBegin).TotalDays)
            {
                case double result when (result >= 0 && result <= 4):
                    return MeasureScale.Max;
                case double result when (result > 4 && result <= 21):
                    return MeasureScale.ThirtyMinutes;
                case double result when (result > 21 && result <= 42):
                    return MeasureScale.OneHour;
                case double result when (result > 42 && result <= 128):
                    return MeasureScale.ThreeHours;
                case double result when (result > 128 && result <= 1024):
                    return MeasureScale.OneDay;
                case double result when (result > 1024 && result <= 7168):
                    return MeasureScale.OneWeek;
                case double result when (result > 7168):
                    return MeasureScale.OneMonth;
                default:
                    return MeasureScale.OneMonth;
            }

        }
    }
}