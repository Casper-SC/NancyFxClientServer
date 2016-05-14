using System.Runtime.Serialization;

namespace Data
{
    [DataContract(Name = "locationStateContainer")]
    public class LocationStateContainer
    {
        [DataMember(Name = "states", IsRequired = true)]
        public LocationState[] States { get; set; }
    }

    [DataContract(Name = "locationState")]
    public class LocationState
    {
        public LocationState()
        {
            MainEntranceClosed = true;
            WindowClosed = true;
        }

        /// <summary>
        /// Идентификатор помещения.
        /// </summary>
        [DataMember(Name = "id", IsRequired = true)]
        public int Id { get; set; }

        /// <summary>
        /// Главный вход закрыт.
        /// </summary>
        [DataMember(Name = "mainEntranceClosed", IsRequired = true)]
        public bool MainEntranceClosed { get; set; }

        /// <summary>
        /// Окно закрыто.
        /// </summary>
        [DataMember(Name = "windowClosed", IsRequired = true)]
        public bool WindowClosed { get; set; }
    }
}
