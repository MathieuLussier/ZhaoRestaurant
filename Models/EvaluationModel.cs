using System;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;


namespace tp1_restaurant.Models
{
    public class Evaluation
    {
        public int Id { get; set; }

        [Required(ErrorMessage = "Le champ Prénom est obligatoire.")]
        [MaxLength(100, ErrorMessage = "Le champ Nom à un maximum de 100 charactères.")]
        [DisplayName("Prénom")]
        public string Prenom { get; set; }

        [Required(ErrorMessage = "Le champ Nom est obligatoire.")]
        [MaxLength(100, ErrorMessage = "Le champ Nom à un maximum de 100 charactères.")]
        public string Nom { get; set; }

        [Required(ErrorMessage = "Le champ Type de réservation est obligatoire.")]
        [DisplayName("Type de réservation")]
        public TypeReservation TypeReservation { get; set; }

        [Required(ErrorMessage = "Le champ Courriel est obligatoire.")]
        [DataType(DataType.EmailAddress, ErrorMessage = "Format de Courriel invalide.")]
        public string Courriel { get; set; }

        [Required(ErrorMessage = "Le champ Date de la visite est obligatoire.")]
        [DataType(DataType.Date, ErrorMessage = "Format de Date invalide.")]
        [DisplayName("Date de la visite")]
        public DateTime Datevisite  { get; set; }

        [Required(ErrorMessage = "Le champ Qualité du repas est obligatoire.")]
        [Range(1, 5, ErrorMessage = "La valeur du champ Qualité du repas doit être comprise entre 1 et 5.")]
        [DisplayName("Qualité du repas")]
        public int QualiteRepas { get; set; }

        [Required(ErrorMessage = "Le champ Qualité du service est obligatoire.")]
        [Range(1, 5, ErrorMessage = "La valeur du champ Qualité du service doit être comprise entre 1 et 5.")]
        [DisplayName("Qualité du service")]
        public int QualiteService { get; set; }

        [MaxLength(300, ErrorMessage = "Le champ Commentaires à un maximum de 300 charactères.")]
        public string Commentaires { get; set; }
    }
}
