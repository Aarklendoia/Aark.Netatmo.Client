using System;
using System.Collections.Generic;
using System.Text;

namespace Aark.Netatmo.SDK.Energy
{
    /// <summary>
    /// Defines a person.
    /// </summary>
    public class Person
    {
        /// <summary>
        /// Id of the person.
        /// </summary>
        public string Id { get; private set; }
        /// <summary>
        /// Pseudonymous of the person if identified.
        /// </summary>
        public string Pseudo { get; private set; }
        /// <summary>
        /// Url of the image representing the person.
        /// </summary>
        public Uri Url { get; private set; }
    }
}
