using System;

namespace Aark.Netatmo.SDK.Security
{
    /// <summary>
    /// Defines a snapshot.
    /// </summary>
    public class Snapshot
    {
        /// <summary>
        /// Id of the snapshot.
        /// </summary>
        public string Id { get; set; }
        /// <summary>
        /// Version of the snapshot.
        /// </summary>
        public long Version { get; set; }
        /// <summary>
        /// Key of the snapshot.
        /// </summary>
        public string Key { get; set; }
        /// <summary>
        /// URL of the snapshot.
        /// </summary>
        public Uri Url { get; set; }
    }
}