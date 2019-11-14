using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Text;
using Aark.Netatmo.SDK.Helpers;

namespace Aark.Netatmo.SDK.Security
{
    /// <summary>
    /// Defines a security event.
    /// </summary>
    public class SecurityEvent
    {
        /// <summary>
        /// Identifier of the event.
        /// </summary>
        public string Id { get; set; }
        /// <summary>
        /// Type of the event.
        /// </summary>
        public EventType Type { get; set; }
        /// <summary>
        /// Time of occurence of event.
        /// </summary>
        public DateTime Time { get; set; }
        /// <summary>
        /// Identifier of the person the event is about (if any).
        /// </summary>
        public string PersonId { get; set; }
        /// <summary>
        /// Snapshot .
        /// </summary>
        public Snapshot Snapshot { get; set; }
        /// <summary>
        /// Device that detected the event.
        /// </summary>
        public string DeviceId { get; set; }
        /// <summary>
        /// User facing event description.
        /// </summary>
        public string Message { get; set; }
        /// <summary>
        /// Subtypes of SD and Alim events.
        /// </summary>
        public EventSubType SubType { get; set; }
        /// <summary>
        /// Identifier of the video.
        /// </summary>
        public string VideoId { get; set; }
        /// <summary>
        /// Status of the video.
        /// </summary>
        public VideoStatus VideoStatus { get; set; }
        /// <summary>
        /// If person was considered "away" before being seen during this event.
        /// </summary>
        public bool? IsArrival { get; set; }
        /// <summary>
        /// If several events are detected in the same video, they will be aggregated in this array. 
        /// </summary>
        public ObservableCollection<EventSubType> EventList { get; } = new ObservableCollection<EventSubType>();
    }
}