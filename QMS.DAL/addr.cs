//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace QMS.DAL
{
    using System;
    using System.Collections.Generic;
    
    public partial class addr
    {
        public int gid { get; set; }
        public Nullable<long> tlid { get; set; }
        public string fromhn { get; set; }
        public string tohn { get; set; }
        public string side { get; set; }
        public string zip { get; set; }
        public string plus4 { get; set; }
        public string fromtyp { get; set; }
        public string totyp { get; set; }
        public Nullable<int> fromarmid { get; set; }
        public Nullable<int> toarmid { get; set; }
        public string arid { get; set; }
        public string mtfcc { get; set; }
        public string statefp { get; set; }
    }
}