//------------------------------------------------------------------------------
// <auto-generated>
//     Этот код создан по шаблону.
//
//     Изменения, вносимые в этот файл вручную, могут привести к непредвиденной работе приложения.
//     Изменения, вносимые в этот файл вручную, будут перезаписаны при повторном создании кода.
// </auto-generated>
//------------------------------------------------------------------------------

namespace CallCenter
{
    using System;
    using System.Collections.Generic;
    
    public partial class Applications
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public Applications()
        {
            this.SOSW = new HashSet<SOSW>();
        }
    
        public int ID { get; set; }
        public int ClientID { get; set; }
        public Nullable<System.DateTime> DateOfSubmission { get; set; }
        public Nullable<System.DateTime> DateSOSW { get; set; }
        public System.DateTime DateOfCompletion { get; set; }
        public int ServicesID { get; set; }
    
        public virtual Client Client { get; set; }
        public virtual Services Services { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<SOSW> SOSW { get; set; }

        public static implicit operator List<object>(Applications v)
        {
            throw new NotImplementedException();
        }
    }
}
