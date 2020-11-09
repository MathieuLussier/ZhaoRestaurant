using System;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace tp1_restaurant.Models
{
    public enum TypeReservation
    {
        [Display(Name = "Salle à manger")]
        SalleManger,
        [Display(Name = "Salon Privé")]
        SalonPrive
    }
}
