namespace Aark.Netatmo.SDK.Helpers
{
    /// <summary>
    /// Lists the types of possible events.
    /// </summary>
    public enum EventType
    {
        /// <summary>
        /// Event triggered when Welcome detects a face.
        /// </summary>
        Person,
        /// <summary>
        /// Event triggered when geofencing implies the person has left the home.
        /// </summary>
        PersonAway,
        /// <summary>
        /// Event triggered when Welcome detects a motion.
        /// </summary>
        Movement,
        /// <summary>
        /// Event triggered when Presence detects a human, a car or an animal.
        /// </summary>
        Outdoor,
        /// <summary>
        /// When the camera connects to Netatmo servers.
        /// </summary>
        Connection,
        /// <summary>
        /// When the camera loses connection to Netatmo servers.
        /// </summary>
        Disconnection,
        /// <summary>
        /// Whenever monitoring is resumed.
        /// </summary>
        On,
        /// <summary>
        /// Whenever monitoring is suspended.
        /// </summary>
        Off,
        /// <summary>
        /// When the camera is booting.
        /// </summary>
        Boot,
        /// <summary>
        /// Event triggered by the SD card status change.
        /// </summary>
        SD,
        /// <summary>
        /// Event triggered by the power supply status change.
        /// </summary>
        Alim,
        /// <summary>
        /// Event triggered when the video summary of the last 24 hours is available.
        /// </summary>
        DailySummary,
        /// <summary>
        /// A new module has been paired with Welcome.
        /// </summary>
        NewModule,
        /// <summary>
        /// Module is connected with Welcome (after disconnection).
        /// </summary>
        ModuleConnect,
        /// <summary>
        /// Module lost its connection with Welcome.
        /// </summary>
        ModuleDisconnect,
        /// <summary>
        /// Module's battery is low.
        /// </summary>
        ModuleLowBattery,
        /// <summary>
        /// Module's firmware update is over.
        /// </summary>
        ModuleEndUpdate,
        /// <summary>
        /// Tag detected a big move.
        /// </summary>
        TagBigMove,
        /// <summary>
        /// Tag detected a small move.
        /// </summary>
        TagSmallMove,
        /// <summary>
        /// Tag was uninstalled.
        /// </summary>
        TagUninstalled,
        /// <summary>
        /// Tag detected the door/window was left open.
        /// </summary>
        TagOpen,
        /// <summary>
        /// Whenever the smoke detection is activated or deactivated.
        /// </summary>
        Hush,
        /// <summary>
        /// Whenever smoke is detected or smoke is cleared.
        /// </summary>
        Smoke,
        /// <summary>
        /// When the smoke detector is ready or tampered.
        /// </summary>
        Tampered,
        /// <summary>
        /// When wifi status is updated.
        /// </summary>
        WifiStatus,
        /// <summary>
        /// When the battery is too low.
        /// </summary>
        BatteryStatus,
        /// <summary>
        /// When the detection chamber is dusty or clean.
        /// </summary>
        DetectionChamberStatus,
        /// <summary>
        /// Sound test results.
        /// </summary>
        SoundTest,
    }

    /// <summary>
    /// Provides details for "SD" and "Alim" events.
    /// </summary>
    public enum SubEventCategory
    {
        /// <summary>
        /// List of events.
        /// </summary>
        List,
        /// <summary>
        /// SD card event.
        /// </summary>
        SD,
        /// <summary>
        /// Alimentation event.
        /// </summary>
        Alim
    }

    /// <summary>
    /// Subtypes of SD and Alim events.
    /// </summary>
    public enum EventSubType
    {
        /// <summary>
        /// Type of event unknown.
        /// </summary>
        Unknown,
        /// <summary>
        /// Human seen.
        /// </summary>
        Human,
        /// <summary>
        /// Car seen.
        /// </summary>
        Vehicle,
        /// <summary>
        /// Animal seen.
        /// </summary>
        Animal,
        /// <summary>
        /// Missing SD card.
        /// </summary>
        MissingSDCard,
        /// <summary>
        /// SD card inserted.
        /// </summary>
        SDCardInserted,
        /// <summary>
        /// SD card formated.
        /// </summary>
        SDCardFormated,
        /// <summary>
        /// Working SD card.
        /// </summary>
        WorkingSDCard,
        /// <summary>
        /// Defective SD card.
        /// </summary>
        DefectiveSDCard,
        /// <summary>
        /// Incompatible SD card speed.
        /// </summary>
        IncompatibleSDCardSpeed,
        /// <summary>
        /// Insufficient SD card space.
        /// </summary>
        InsufficientSDcardSpace,
        /// <summary>
        /// Incorrect power adapter.
        /// </summary>
        IncorrectPowerAdapter,
        /// <summary>
        /// Correct power adapter.
        /// </summary>
        CorrectPowerAdapter
    }

    /// <summary>
    /// Status of the video.
    /// </summary>
    public enum VideoStatus
    {
        /// <summary>
        /// Recording.
        /// </summary>
        Recording,
        /// <summary>
        /// Deleted.
        /// </summary>
        Deleted,
        /// <summary>
        /// Available.
        /// </summary>
        Available
    }

    /// <summary>
    /// Type of module.
    /// </summary>
    public enum SecurityModuleType
    {
        /// <summary>
        /// Tag.
        /// </summary>
        Tag
    }

    /// <summary>
    /// Type of the camera.
    /// </summary>
    public enum CameraType
    {
        /// <summary>
        /// Camera
        /// </summary>
        Camera
    }

    /// <summary>
    /// Tools for the types used by SecurityData.
    /// </summary>
    public static class SecurityHelper
    {
        /// <summary>
        /// Convert a <paramref name="value"/> to a <see cref="EventType"/>
        /// </summary>
        /// <param name="value"></param>
        /// <returns>The <see cref="EventType"/> corresponding to the input <paramref name="value"/></returns>
        public static EventType ToEventType(this string value)
        {
            switch (value)
            {
                case "Alim":
                    return EventType.Alim;
                case "battery_status":
                    return EventType.BatteryStatus;
                case "Boot":
                    return EventType.Boot;
                case "Connection":
                    return EventType.Connection;
                case "daily_summary":
                    return EventType.DailySummary;
                case "detection_chamber_status":
                    return EventType.DetectionChamberStatus;
                case "Disconnection":
                    return EventType.Disconnection;
                case "hush":
                    return EventType.Hush;
                case "module_connect":
                    return EventType.ModuleConnect;
                case "module_disconnect":
                    return EventType.ModuleDisconnect;
                case "module_end_update":
                    return EventType.ModuleEndUpdate;
                case "module_low_battery":
                    return EventType.ModuleLowBattery;
                case "Movement":
                    return EventType.Movement;
                case "new_module":
                    return EventType.NewModule;
                case "Off":
                    return EventType.Off;
                case "On":
                    return EventType.On;
                case "Outdoor":
                    return EventType.Outdoor;
                case "Person":
                    return EventType.Person;
                case "PersonAway":
                    return EventType.PersonAway;
                case "SD":
                    return EventType.SD;
                case "smoke":
                    return EventType.Smoke;
                case "sound_test":
                    return EventType.SoundTest;
                case "tag_big_move":
                    return EventType.TagBigMove;
                case "tag_open":
                    return EventType.TagOpen;
                case "tag_small_move":
                    return EventType.TagSmallMove;
                case "tag_uninstalled":
                    return EventType.TagUninstalled;
                case "tampered":
                    return EventType.Tampered;
                case "wifi_status":
                    return EventType.WifiStatus;
                default:
                    return EventType.Off;
            }
        }

        /// <summary>
        /// Convert a <paramref name="value"/> to a <see cref="EventSubType"/>
        /// </summary>
        /// <param name="value"></param>
        /// <param name="category"></param>
        /// <returns>The <see cref="EventSubType"/> corresponding to the input <paramref name="value"/></returns>
        public static EventSubType ToEventSubType(this string value, SubEventCategory category)
        {
            switch (category)
            {
                case SubEventCategory.List:
                    switch (value)
                    {
                        case "animal":
                            return EventSubType.Animal;
                        case "human":
                            return EventSubType.Human;
                        case "vehicle":
                            return EventSubType.Vehicle;
                        default:
                            return EventSubType.Human;
                    }
                case SubEventCategory.Alim:
                    switch (value)
                    {
                        case "1":
                            return EventSubType.MissingSDCard;
                        case "2":
                            return EventSubType.SDCardInserted;
                        case "3":
                            return EventSubType.SDCardFormated;
                        case "4":
                            return EventSubType.WorkingSDCard;
                        case "5":
                            return EventSubType.DefectiveSDCard;
                        case "6":
                            return EventSubType.IncompatibleSDCardSpeed;
                        case "7":
                            return EventSubType.InsufficientSDcardSpace;
                        default:
                            return EventSubType.Unknown;
                    }
                case SubEventCategory.SD:
                    switch (value)
                    {
                        case "1":
                            return EventSubType.IncorrectPowerAdapter;
                        case "2":
                            return EventSubType.CorrectPowerAdapter;
                        default:
                            return EventSubType.Unknown;
                    }
                default:
                    return EventSubType.Unknown;
            }
        }

        /// <summary>
        /// Convert a <paramref name="value"/> to a <see cref="VideoStatus"/>
        /// </summary>
        /// <param name="value"></param>
        /// <returns>The <see cref="VideoStatus"/> corresponding to the input <paramref name="value"/></returns>
        public static VideoStatus ToVideoStatus(this string value)
        {
            switch (value)
            {
                case "available":
                    return VideoStatus.Available;
                case "deleted":
                    return VideoStatus.Deleted;
                case "recording":
                    return VideoStatus.Recording;
                default:
                    return VideoStatus.Recording;
            }
        }

        /// <summary>
        /// Convert a <paramref name="value"/> to a <see cref="SecurityModuleType"/>
        /// </summary>
        /// <param name="value"></param>
        /// <returns>The <see cref="SecurityModuleType"/> corresponding to the input <paramref name="value"/></returns>
        public static SecurityModuleType ToSecurityModuleType(this string value)
        {
            switch (value)
            {
                case "NACamDoorTag":
                    return SecurityModuleType.Tag;
                default:
                    return SecurityModuleType.Tag;
            }
        }

        /// <summary>
        /// Convert a <paramref name="value"/> to a <see cref="CameraType"/>
        /// </summary>
        /// <param name="value"></param>
        /// <returns>The <see cref="CameraType"/> corresponding to the input <paramref name="value"/></returns>
        public static CameraType ToCameraType(this string value)
        {
            switch (value)
            {
                case "NACamera":
                    return CameraType.Camera;
                default:
                    return CameraType.Camera;
            }
        }
    }
}
