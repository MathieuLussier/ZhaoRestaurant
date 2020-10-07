using System;
using System.ComponentModel;

namespace tp1_restaurant.Models
{
    public enum TypePromotion
    {
        Comptoir,
        [Description("Salle à manger")]
        SalleManger,
        Livraison,
        Tous
    }
}
