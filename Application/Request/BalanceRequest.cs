using System.ComponentModel.DataAnnotations;

namespace Application.Request
{
    public class BalanceRequest
    {
        [Required]
        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:YYYY-MM-DD}")]
        public DateTime from { get; set; }
        [Required]
        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:YYYY-MM-DD}")]
        public DateTime to { get; set; }
    }
}
