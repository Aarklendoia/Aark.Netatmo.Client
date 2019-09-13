using Aark.Netatmo.SDK.Helpers;
using System.Collections.Generic;

namespace Aark.Netatmo.SDK.Energy
{
    /// <summary>
    /// Define a room.
    /// </summary>
    public class Room
    {
        /// <summary>
        /// Id oh the room.
        /// </summary>
        public string Id { get; private set; }
        /// <summary>
        /// Name of the room.
        /// </summary>
        public string Name { get; private set; }
        /// <summary>
        /// Type of the room.
        /// </summary>
        public RoomType RoomType { get; private set; }
        /// <summary>
        /// Modules associated to this room.
        /// </summary>
        public List<long> ModuleIds { get; private set; }
    }
}