using System;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace tp1_restaurant.Models
{
    public class Promotion
    {
        [Key]
        public int PromotionId { get; set; }

        [Required(ErrorMessage = "Le champ Type de promotion est obligatoire.")]
        [DisplayName("Type de promotion")]
        public TypePromotion TypePromotion { get; set; }

        [Required(ErrorMessage = "Le champ Taux applicable est obligatoire.")]
        [DisplayName("Taux applicable")]
        public int TauxApplicable { get; set; }

        [Required(ErrorMessage = "Le champ Description de la promotion est obligatoire.")]
        [MaxLength(300, ErrorMessage = "Le champ Description de la promotion à un maximum de 300 charactères.")]
        [DisplayName("Description")]
        public string DescriptionPromotion { get; set; }

        [Required(ErrorMessage = "Le champ Date de début est obligatoire.")]
        [DataType(DataType.Date, ErrorMessage = "Format de Date invalide.")]
        [DisplayName("Date de début")]
        public DateTime DateDebut { get; set; }

        [DataType(DataType.Date, ErrorMessage = "Format de Date invalide.")]
        [DisplayName("Date de fin")]
        public DateTime DateFin { get; set; }
    }
}
