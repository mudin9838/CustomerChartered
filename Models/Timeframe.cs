//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace CustomerChartered.Models
{
    using System;
    using System.Collections.Generic;
    
    public partial class Timeframe
    {
        public string TimeframeId { get; set; }
        public string TimeframeName { get; set; }
        public string ServiceDetailId { get; set; }
    
        public virtual ServiceDetail ServiceDetail { get; set; }
    }
}