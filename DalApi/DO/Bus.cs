using System;



namespace DO
{
    /// <summary>
    /// The technical details of the buses
    /// </summary>
    
    
     public class Bus
    {
        /// <summary>
        /// The license number of the bus, the format depends on the lincensing date
        /// </summary>
        public string LicenseNumber { set; get; }
        /// <summary>
        /// The date when the bus was created
        /// </summary>
        public DateTime LicensingDate { set; get; }
        /// <summary>
        /// The number of kilometers that the bus had done 
        /// </summary>
        public long Mileage { set; get; }
        /// <summary>
        /// The number of kilometers that the bus can travel until it has no more fuel
        /// </summary>
        public int FuelTank { set; get; }
        /// <summary>
        /// The status of the bus : if it is in travel, in fueling, ready to go or in care
        /// </summary>
        public BusStatus Status { set; get; }
        /// <summary>
        /// If the bus travels only in the city or if it's a bus between two different cities
        /// </summary>
        public bool InCity { set; get; }
        /// <summary>
        /// If there is a possibility to pay by ourselves
        /// </summary>
        public bool SelfPayment { set; get; }
        /// <summary>
        /// Boolean variable that says if the bus is deleted from the system or not
        /// </summary>
        public bool Deleted { set; get; } = false;
        public override string ToString()
        {
            return this.ToStringProperty();
        }
    }
}
