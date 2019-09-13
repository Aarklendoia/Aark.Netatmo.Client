namespace Aark.Netatmo.SDK.Helpers
{
    /// <summary>
    /// Unit system.
    /// </summary>
    public enum UnitSystem
    {
        /// <summary>
        /// Metric system.
        /// </summary>
        Metric,
        /// <summary>
        /// Imperial system.
        /// </summary>
        Imperial
    }
    /// <summary>
    /// Unit uses for the wind.
    /// </summary>
    public enum WindUnit
    {
        /// <summary>
        /// Kilometers per hour.
        /// </summary>
        kph,
        /// <summary>
        /// Meters per hour.
        /// </summary>
        mph,
        /// <summary>
        /// Meters per second.
        /// </summary>
        ms,
        /// <summary>
        /// Beaufort.
        /// </summary>
        beaufort,
        /// <summary>
        /// Knot.
        /// </summary>
        knot
    }
    /// <summary>
    /// Pressure unit.
    /// </summary>
    public enum PressureUnit
    {
        /// <summary>
        /// Millibar.
        /// </summary>
        mbar,
        /// <summary>
        /// Inch of mercury.
        /// </summary>
        inHg,
        /// <summary>
        /// Millimetre of mercury.
        /// </summary>
        mmHg,
        /// <summary>
        /// Torr.
        /// </summary>
        torr
    }
    /// <summary>
    /// Feeling algorithm.
    /// </summary>
    public enum FeelLikeAlgo
    {
        /// <summary>
        /// Humidex.
        /// </summary>
        humidex,
        /// <summary>
        /// Heat index.
        /// </summary>
        heatIndex
    }

    /// <summary>
    /// Tools to manage units.
    /// </summary>
    public static class UnitHelper
    {

        /// <summary>
        /// Convert a <paramref name="value"/> to a <see cref="UnitSystem"/>
        /// </summary>
        /// <param name="value"></param>
        /// <returns>The <see cref="UnitSystem"/> corresponding to the input <paramref name="value"/></returns>
        public static UnitSystem ToUnitSystem(this long value)
        {
            if (value == 0)
                return UnitSystem.Metric;
            else
                return UnitSystem.Imperial;
        }

        /// <summary>
        /// Convert a <paramref name="value"/> to a <see cref="WindUnit"/>
        /// </summary>
        /// <param name="value"></param>
        /// <returns>The <see cref="WindUnit"/> corresponding to the input <paramref name="value"/></returns>
        public static WindUnit ToWindUnit(this long value)
        {
            switch (value)
            {
                case 1:
                    return WindUnit.mph;
                case 2:
                    return WindUnit.ms;
                case 3:
                    return WindUnit.beaufort;
                case 4:
                    return WindUnit.knot;
                default:
                    return WindUnit.kph;
            }
        }

        /// <summary>
        /// Convert a <paramref name="value"/> to a <see cref="PressureUnit"/>
        /// </summary>
        /// <param name="value"></param>
        /// <returns>The <see cref="PressureUnit"/> corresponding to the input <paramref name="value"/></returns>
        public static PressureUnit ToPressureUnit(this long value)
        {
            switch (value)
            {
                case 1:
                    return PressureUnit.inHg;
                case 2:
                    return PressureUnit.mmHg; // torr ?
                default:
                    return PressureUnit.mbar;
            }
        }

        /// <summary>
        /// Convert a <paramref name="value"/> to a <see cref="FeelLikeAlgo"/>
        /// </summary>
        /// <param name="value"></param>
        /// <returns>The <see cref="FeelLikeAlgo"/> corresponding to the input <paramref name="value"/></returns>
        public static FeelLikeAlgo ToFeelLikeAlgo(this long value)
        {
            if (value == 0)
                return FeelLikeAlgo.humidex;
            else
                return FeelLikeAlgo.heatIndex;
        }
    }
}
