using System;

namespace Aark.Netatmo.SDK.Security
{
    /// <summary>
    /// Defines a person.
    /// </summary>
    public class Person
    {
        /// <summary>
        /// Identifier of the person.
        /// </summary>
        public string Id { get; set; }
        /// <summary>
        /// Pseudonymous of the person if identified.
        /// </summary>
        public string Pseudo { get; set; }
        /// <summary>
        /// Url of the image representing the person.
        /// </summary>
        public Uri Url { get; set; }
        /// <summary>
        /// Last time the person was seen.
        /// </summary>
        public DateTime LastSeen { get; set; }
        /// <summary>
        /// Version of the picture.
        /// </summary>
        public long Version { get; set; }
        /// <summary>
        /// Indicates whether the person is considered absent because he or she has not seen for some time.
        /// </summary>
        public bool OutOfSight { get; set; }
    }
}
