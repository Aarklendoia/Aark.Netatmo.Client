namespace Aark.Netatmo.SDK.Helpers
{
    /// <summary>
    /// Type of instruction to be applied.
    /// </summary>
    public enum SetpointMode
    {
        /// <summary>
        /// Manual mode.
        /// </summary>
        Manual,
        /// <summary>
        /// Max mode.
        /// </summary>
        Max,
        /// <summary>
        /// Off mode.
        /// </summary>
        Off,
        /// <summary>
        /// Schedule mode.
        /// </summary>
        Schedule,
        /// <summary>
        /// Away mode.
        /// </summary>
        Away,
        /// <summary>
        /// Frostguard mode.
        /// </summary>
        FrostGuard
    }
    /// <summary>
    /// Thermostat operating mode.
    /// </summary>
    public enum ThermostatMode
    {
        /// <summary>
        /// Schedule mode.
        /// </summary>
        Schedule,
        /// <summary>
        /// Away mode.
        /// </summary>
        Away,
        /// <summary>
        /// Frostguard mode.
        /// </summary>
        FrostGuard
    }
    /// <summary>
    /// Type of scheduling zone.
    /// </summary>
    public enum ZoneType
    {
        /// <summary>
        /// Day zone.
        /// </summary>
        Day,
        /// <summary>
        /// Night zone.
        /// </summary>
        Night,
        /// <summary>
        /// Away zone.
        /// </summary>
        Away,
        /// <summary>
        /// Frostguard zone.
        /// </summary>
        FrostGuard,
        /// <summary>
        /// Custom zone.
        /// </summary>
        Custom,
        /// <summary>
        /// Eco zone.
        /// </summary>
        Eco,
        /// <summary>
        /// Comfort zone.
        /// </summary>
        Comfort
    }

    /// <summary>
    /// Type of a room.
    /// </summary>
    public enum RoomType
    {
        /// <summary>
        /// Kitchen.
        /// </summary>
        Kitchen,
        /// <summary>
        /// Bedroom.
        /// </summary>
        Bedroom,
        /// <summary>
        /// Livingroom.
        /// </summary>
        Livingroom,
        /// <summary>
        /// Bathroom.
        /// </summary>
        Bathroom,
        /// <summary>
        /// Lobby.
        /// </summary>
        Lobby,
        /// <summary>
        /// Custom.
        /// </summary>
        Custom,
        /// <summary>
        /// Outdoor.
        /// </summary>
        Outdoor,
        /// <summary>
        /// Toilets.
        /// </summary>
        Toilets,
        /// <summary>
        /// Garage.
        /// </summary>
        Garage,
        /// <summary>
        /// Home office.
        /// </summary>
        HomeOffice,
        /// <summary>
        /// Dining room.
        /// </summary>
        DiningRoom,
        /// <summary>
        /// Corridor.
        /// </summary>
        Corridor,
        /// <summary>
        /// Stairs.
        /// </summary>
        Stairs
    }

    /// <summary>
    /// Tools for the types used by EnergyData.
    /// </summary>
    public static class EnergyHelper
    {
        /// <summary>
        /// Convert a <paramref name="value"/> to a <see cref="SetpointMode"/>
        /// </summary>
        /// <param name="value"></param>
        /// <returns>The <see cref="SetpointMode"/> corresponding to the input <paramref name="value"/></returns>
        public static SetpointMode ToSetpointMode(this string value)
        {
            switch (value)
            {
                case "manual":
                    return SetpointMode.Manual;
                case "max":
                    return SetpointMode.Max;
                case "schedule":
                    return SetpointMode.Schedule;
                case "away":
                    return SetpointMode.Away;
                case "hg":
                    return SetpointMode.FrostGuard;
                default:
                    return SetpointMode.Off;
            }
        }

        /// <summary>
        /// Convert a <paramref name="value"/> to a <see cref="ThermostatMode"/>
        /// </summary>
        /// <param name="value"></param>
        /// <returns>The <see cref="ThermostatMode"/> corresponding to the input <paramref name="value"/></returns>
        public static ThermostatMode ToThermostatMode(this string value)
        {
            switch (value)
            {
                case "schedule":
                    return ThermostatMode.Schedule;
                case "away":
                    return ThermostatMode.Away;
                case "frost_guard":
                    return ThermostatMode.FrostGuard;
                default:
                    return ThermostatMode.Schedule;
            }
        }

        /// <summary>
        /// Convert a <paramref name="value"/> to a <see cref="ZoneType"/>
        /// </summary>
        /// <param name="value"></param>
        /// <returns>The <see cref="ZoneType"/> corresponding to the input <paramref name="value"/></returns>
        public static ZoneType ToZoneType(this long value)
        {
            switch (value)
            {
                case 0:
                    return ZoneType.Day;
                case 1:
                    return ZoneType.Night;
                case 2:
                    return ZoneType.Away;
                case 3:
                    return ZoneType.FrostGuard;
                case 4:
                    return ZoneType.Custom;
                case 5:
                    return ZoneType.Eco;
                case 8:
                    return ZoneType.Comfort;
                default:
                    return ZoneType.Day;
            }
        }

        /// <summary>
        /// Convert a <paramref name="value"/> to a <see cref="RoomType"/>
        /// </summary>
        /// <param name="value"></param>
        /// <returns>The <see cref="RoomType"/> corresponding to the input <paramref name="value"/></returns>
        public static RoomType ToRoomType(this string value)
        {
            switch (value)
            {
                case "kitchen":
                    return RoomType.Kitchen;
                case "bedroom":
                    return RoomType.Bedroom;
                case "livingroom":
                    return RoomType.Livingroom;
                case "bathroom":
                    return RoomType.Bathroom;
                case "lobby":
                    return RoomType.Lobby;
                case "custom":
                    return RoomType.Custom;
                case "outdoor":
                    return RoomType.Outdoor;
                case "toilets":
                    return RoomType.Toilets;
                case "garage":
                    return RoomType.Garage;
                case "home_office":
                    return RoomType.HomeOffice;
                case "dining_room":
                    return RoomType.DiningRoom;
                case "Corridor":
                    return RoomType.Corridor;
                case "stairs":
                    return RoomType.Stairs;
                default:
                    return RoomType.Custom;
            }
        }
    }
}