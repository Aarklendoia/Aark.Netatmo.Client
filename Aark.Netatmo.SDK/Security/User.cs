using System.Globalization;

namespace Aark.Netatmo.SDK.Security
{
    /// <summary>
    /// User related information.
    /// </summary>
    public class User
    {
        /// <summary>
        /// Language used by the user.
        /// </summary>
        public string Language { get; set; }
        /// <summary>
        /// Culture uses by the user.
        /// </summary>
        public CultureInfo CultureInfo { get; set; }
        /// <summary>
        /// Country of the user.
        /// </summary>
        public RegionInfo RegionInfo { get; set; }
    }
}