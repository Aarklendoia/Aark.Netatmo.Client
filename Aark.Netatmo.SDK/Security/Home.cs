using Aark.Netatmo.SDK.Common;
using Aark.Netatmo.SDK.Helpers;
using Aark.Netatmo.SDK.Models.Security;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Text;

namespace Aark.Netatmo.SDK.Security
{
    /// <summary>
    /// A Home contains Persons, Place, Cameras, Smoke detectors and Events.
    /// </summary>
    public class Home
    {
        /// <summary>
        /// id of the home.
        /// </summary>
        public string Id { get; private set; }
        /// <summary>
        /// name of the home.
        /// </summary>
        public string Name { get; private set; }
        /// <summary>
        /// Location informations of the weather station.
        /// </summary>
        public Location Place { get; internal set; }
        /// <summary>
        /// Persons recently detected by the camera.
        /// </summary>
        public ObservableCollection<Person> Persons { get; private set; } = new ObservableCollection<Person>();
        /// <summary>
        /// Event detected by the camera.
        /// </summary>
        public ObservableCollection<SecurityEvent> Events { get; private set; } = new ObservableCollection<SecurityEvent>();

        // TODO modules, user, place

        internal bool Load(HomeData.Home home)
        {
            if (home.Cameras.Count == 0 && home.SmokeDetectors.Count == 0)
                return false;
            Id = home.Id;
            Name = home.Name;
            Place = new Location()
            {
                City = home.Place.City,
                Country = home.Place.Country,
                TimeZone = home.Place.Timezone
            };
            foreach (HomeData.Person person in home.Persons)
            {
                Person newPerson = new Person
                {
                    Id = person.Id,
                    Pseudo = person.Pseudo,
                    Url = person.Face.Url,
                    LastSeen = person.LastSeen.ToLocalDateTime(),
                    OutOfSight = person.OutOfSight,
                    Version = person.Face.Version
                };
                Persons.Add(newPerson);
            }
            foreach (HomeData.Event securityEvent in home.Events)
            {
                Snapshot snapshot = new Snapshot()
                {
                    Id = securityEvent.Snapshot.Id,
                    Key = securityEvent.Snapshot.Key,
                    Url = securityEvent.Snapshot.Url,
                    Version = securityEvent.Snapshot.Version
                };
                SecurityEvent newEvent = new SecurityEvent
                {
                    Id = securityEvent.Id,
                    Type = securityEvent.Type.ToEventType(),
                    Time = securityEvent.Time.ToLocalDateTime(),
                    PersonId = securityEvent.PersonId,
                    Snapshot = snapshot,
                    DeviceId = securityEvent.DeviceId,
                    Message = securityEvent.Message,
                    VideoId = securityEvent.VideoId,
                    VideoStatus = securityEvent.VideoStatus.ToVideoStatus(),
                    IsArrival = securityEvent.IsArrival
                };
                if (newEvent.Type == EventType.Alim)
                {
                    newEvent.SubType = securityEvent.SubType.ToEventSubType(SubEventCategory.Alim);
                }
                if (newEvent.Type == EventType.SD)
                {
                    newEvent.SubType = securityEvent.SubType.ToEventSubType(SubEventCategory.SD);
                }
                foreach (string eventItem in securityEvent.EventList)
                    newEvent.EventList.Add(eventItem.ToEventSubType(SubEventCategory.List));
                Events.Add(newEvent);
            }
            return true;
        }
    }
}
