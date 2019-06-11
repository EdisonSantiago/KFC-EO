using System;

namespace Oulanka.Web.Core.Models
{
    public class CheckBoxModel
    {
        public Guid Value { get; set; }
        public string Text { get; set; }
        public bool IsChecked { get; set; }
    }
}