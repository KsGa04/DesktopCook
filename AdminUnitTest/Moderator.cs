//------------------------------------------------------------------------------
// <auto-generated>
//     Этот код создан по шаблону.
//
//     Изменения, вносимые в этот файл вручную, могут привести к непредвиденной работе приложения.
//     Изменения, вносимые в этот файл вручную, будут перезаписаны при повторном создании кода.
// </auto-generated>
//------------------------------------------------------------------------------

namespace AdminUnitTest
{
    using System;
    using System.Collections.Generic;
    
    public partial class Moderator
    {
        public int IdModerator { get; set; }
        public string Mail { get; set; }
        public string Password { get; set; }
        public string NikName { get; set; }
        public Nullable<System.DateTime> DateOfBirth { get; set; }
        public Nullable<int> IdCategory { get; set; }
    
        public virtual Category Category { get; set; }
    }
}