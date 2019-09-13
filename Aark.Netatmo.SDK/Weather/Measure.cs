using System;
using System.Collections.Generic;
using System.Text;

namespace Aark.Netatmo.SDK.Weather
{
    /// <summary>
    /// Measure.
    /// </summary>
    public class Measure
    {
        /// <summary>
        /// Date of the measure.
        /// </summary>
        public DateTime Timestamp { get; private set; }
        /// <summary>
        /// Value of the measure.
        /// </summary>
        public dynamic Value { get; private set; }

        /// <summary>
        /// Create a new measure.
        /// </summary>
        /// <param name="timestamp">Date of the measure.</param>
        /// <param name="value">Value of the measure.</param>
        public Measure(DateTime timestamp, dynamic value)
        {
            Timestamp = timestamp;
            Value = value;
        }
    }
}
