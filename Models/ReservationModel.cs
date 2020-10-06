using System;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace tp1_restaurant.Models {
    public class Reservation {

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

        [Required(ErrorMessage = "Le champ Date et heure de réservation est obligatoire.")]
        [DisplayName("Date et heure de réservation")]
        public DateTime DateHeureReservation { get; set; }

        [Required(ErrorMessage = "Le champ Numéro de téléphone est obligatoire.")]
        [DataType(DataType.PhoneNumber, ErrorMessage = "Format de Numéro de téléphone invalide.")]
        [DisplayName("Numéro de téléphone")]
        public string NumeroTelephone { get; set; }

        [Required(ErrorMessage = "Le champ Nombre de personnes est obligatoire.")]
        [DisplayName("Nombre de personnes")]
        public int NombrePersonnes { get; set; }
    }
}