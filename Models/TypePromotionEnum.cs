using System;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace tp1_restaurant.Models
{
    public enum TypePromotion
    {
        Comptoir,
        [Display(Name = "Salle à manger")]
        SalleManger,
        Livraison,
        Tous
    }
}
