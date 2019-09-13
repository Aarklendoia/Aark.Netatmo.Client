using Aark.Netatmo.SDK.Models;

namespace Aark.Netatmo.SDK.Energy
{
    /// <summary>
    /// Define the list of devices of the energy station. 
    /// </summary>
    public class EnergyDevice
    {
        /// <summary>
        /// Relay for connected valves.
        /// </summary>
        public Relay Relay { get; private set; }

        internal EnergyDevice(APICommands aPICommands)
        {
            Relay = new Relay();
        }
    }
}