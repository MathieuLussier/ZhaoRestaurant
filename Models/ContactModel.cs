using System;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace tp1_restaurant.Models {
    public class Contact {

        [Required(ErrorMessage = "Le champ Prénom est obligatoire.")]
        [MaxLength(100, ErrorMessage = "Le champ Prénom à un maximum de 100 charactères.")]
        [DisplayName("Prénom")]
        public string Prenom { get; set; }

        [Required(ErrorMessage = "Le champ Nom est obligatoire.")]
        [MaxLength(100, ErrorMessage = "Le champ Nom à un maximum de 100 charactères.")]
        public string Nom { get; set; }

        [Required(ErrorMessage = "Le champ Courriel est obligatoire.")]
        [DataType(DataType.EmailAddress, ErrorMessage = "Format de Courriel invalide.")]
        public string Courriel { get; set; }

        [Required(ErrorMessage = "Le champ Numéro de téléphone est obligatoire.")]
        [DataType(DataType.PhoneNumber, ErrorMessage = "Format de Numéro de téléphone invalide.")]
        [DisplayName("Téléphone")]
        public string NumeroTelephone { get; set; }

        [Required(ErrorMessage = "Le champ Message est obligatoire.")]
        [MaxLength(500, ErrorMessage = "Le champ Message à un maximum de 300 charactères.")]
        public string Message { get; set; }
    }
}