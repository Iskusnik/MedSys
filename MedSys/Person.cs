//------------------------------------------------------------------------------
// <auto-generated>
//     Этот код создан по шаблону.
//
//     Изменения, вносимые в этот файл вручную, могут привести к непредвиденной работе приложения.
//     Изменения, вносимые в этот файл вручную, будут перезаписаны при повторном создании кода.
// </auto-generated>
//------------------------------------------------------------------------------

namespace MedSys
{
    using System;
    using System.Collections.Generic;
    
    public partial class Person
    {
        public int Id { get; set; }
        public string FullName { get; set; }
        public System.DateTime BirthDate { get; set; }
        public string InsuranceNum { get; set; }
        public string Adress { get; set; }
        public string Gender { get; set; }
        public string Password { get; set; }
    
        public virtual Document Document { get; set; }
    }
}
