//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace PEMO_DATA_BANKING.Models
{
    using System;
    using System.Collections.Generic;
    
    public partial class Miner
    {
        public int Miner_id { get; set; }
        public Nullable<int> Association_id { get; set; }
        public string FirstName { get; set; }
        public string MiddleName { get; set; }
        public string LastName { get; set; }
        public string Longitude { get; set; }
        public string Latitude { get; set; }
    
        public virtual Association Association { get; set; }
    }
}