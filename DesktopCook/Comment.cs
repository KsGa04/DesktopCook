//------------------------------------------------------------------------------
// <auto-generated>
//     Этот код создан по шаблону.
//
//     Изменения, вносимые в этот файл вручную, могут привести к непредвиденной работе приложения.
//     Изменения, вносимые в этот файл вручную, будут перезаписаны при повторном создании кода.
// </auto-generated>
//------------------------------------------------------------------------------

namespace DesktopCook
{
    using System;
    using System.Collections.Generic;
    
    public partial class Comment
    {
        public int IdComment { get; set; }
        public string NameComment { get; set; }
        public System.DateTime DateComement { get; set; }
        public int IdUser { get; set; }
        public int IdRecipe { get; set; }
    
        public virtual Recipe Recipe { get; set; }
        public virtual Users Users { get; set; }
    }
}
