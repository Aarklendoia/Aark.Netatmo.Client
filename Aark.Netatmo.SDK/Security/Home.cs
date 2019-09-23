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
        /// Persons detected by the camera.
        /// </summary>
        public ObservableCollection<Person> Persons { get; private set; } = new ObservableCollection<Person>();
    }
}
