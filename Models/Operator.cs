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
    
    public partial class Operator
    {
        public int Person_id { get; set; }
        public string FirstName { get; set; }
        public string MiddleName { get; set; }
        public string LastName { get; set; }
        public bool isCompany { get; set; }
        public string Company_Name { get; set; }
        public System.DateTime DateCreated { get; set; }
        public Nullable<System.DateTime> DateDeleted { get; set; }
        public string Status { get; set; }
    }
}
