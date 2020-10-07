using System;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace tp1_restaurant.Models {
    public class Reservation {
        public int Id { get; set; }

        [Required(ErrorMessage = "Le champ Pr�nom est obligatoire.")]
        [MaxLength(100, ErrorMessage = "Le champ Nom � un maximum de 100 charact�res.")]
        [DisplayName("Pr�nom")]
        public string Prenom { get; set; }

        [Required(ErrorMessage = "Le champ Nom est obligatoire.")]
        [MaxLength(100, ErrorMessage = "Le champ Nom � un maximum de 100 charact�res.")]
        public string Nom { get; set; }

        [Required(ErrorMessage = "Le champ Type de r�servation est obligatoire.")]
        [DisplayName("Type de r�servation")]
        public TypeReservation TypeReservation { get; set; }

        [Required(ErrorMessage = "Le champ Courriel est obligatoire.")]
        [DataType(DataType.EmailAddress, ErrorMessage = "Format de Courriel invalide.")]
        public string Courriel { get; set; }

        [Required(ErrorMessage = "Le champ Date et heure de r�servation est obligatoire.")]
        [DataType(DataType.DateTime, ErrorMessage = "Format de Date invalide.")]
        [DisplayName("Date et heure de r�servation")]
        public DateTime DateHeureReservation { get; set; }

        [Required(ErrorMessage = "Le champ Num�ro de t�l�phone est obligatoire.")]
        [DataType(DataType.PhoneNumber, ErrorMessage = "Format de Num�ro de t�l�phone invalide.")]
        [DisplayName("Num�ro de t�l�phone")]
        public string NumeroTelephone { get; set; }

        [Required(ErrorMessage = "Le champ Nombre de personnes est obligatoire.")]
        [Range(1, Int32.MaxValue, ErrorMessage = "Le Nombre de personnes doit �tre de minimum de 1.")]
        [DisplayName("Nombre de personnes")]
        public int NombrePersonnes { get; set; }
    }
}