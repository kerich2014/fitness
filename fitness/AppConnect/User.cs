//------------------------------------------------------------------------------
// <auto-generated>
//     Этот код создан по шаблону.
//
//     Изменения, вносимые в этот файл вручную, могут привести к непредвиденной работе приложения.
//     Изменения, вносимые в этот файл вручную, будут перезаписаны при повторном создании кода.
// </auto-generated>
//------------------------------------------------------------------------------

namespace fitness.AppConnect
{
    using System;
    using System.Collections.Generic;
    
    public partial class User
    {
        public int id { get; set; }
        public string lName { get; set; }
        public string fName { get; set; }
        public string mName { get; set; }
        public string email { get; set; }
        public string phone { get; set; }
        public int role { get; set; }
        public string login { get; set; }
        public string password { get; set; }
    
        public virtual Role Role1 { get; set; }
    }
}