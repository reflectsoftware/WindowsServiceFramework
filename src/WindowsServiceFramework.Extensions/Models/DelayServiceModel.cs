using System;
using System.Linq;

namespace WindowsServiceFramework.Extensions.Models
{
    public class DelayServiceModel
    {
        public Guid? DelayId { get; set; }
        public int? DelayTypeId { get; set; }
        public DateTime? Scheduled { get; set; }
        public string Data { get; set; }
    }
}
