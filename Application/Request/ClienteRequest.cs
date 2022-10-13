using System.ComponentModel.DataAnnotations;

namespace Application.Models
{
    public class ClienteRequest
    {
        [Required]
        [StringLength(10, ErrorMessage = "Máximo de caracteres excedido (10).")]
        public string dni { get; set; }
        [Required]
        [StringLength(25, ErrorMessage = "Máximo de caracteres excedido (25).")]
        public string name { get; set; }
        [Required]
        [StringLength(25, ErrorMessage = "Máximo de caracteres excedido (25).")]
        public string lastname { get; set; }
        [Required]
        [StringLength(200, ErrorMessage = "Máximo de caracteres excedido (200).")]
        public string address { get; set; }
        [Required]
        [StringLength(13, ErrorMessage = "Máximo de caracteres excedido (13).")]
        public string phoneNumber { get; set; }
    }
}
