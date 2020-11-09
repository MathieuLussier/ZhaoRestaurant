using System;
using System.ComponentModel.DataAnnotations.Schema;

namespace tp1_restaurant.Models
{
    public class ErrorViewModel
    {
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public string RequestId { get; set; }

        public bool ShowRequestId => !string.IsNullOrEmpty(RequestId);
    }
}
