using System;
using System.Linq;

namespace WindowsServiceFramework.Extensions.Models
{
    public class PassiveServiceModel
    {
        public string ServiceName { get; set; }
        public Guid? ActiveId { get; set; }
        public DateTime? LastUpdateDateTime { get; set; }
    }
}
