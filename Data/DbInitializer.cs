using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Internal;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using tp1_restaurant.Models;

namespace tp1_restaurant.Data
{
    public static class DbInitializer
    {
        public static void Initialize(ZhaoContext context)
        {
            if (!context.Evaluations.Any())
                DbInitializer.InitEvaluation(context);

            if (!context.Promotions.Any())
                DbInitializer.InitPromotion(context);

            if (!context.Reservations.Any())
                DbInitializer.InitReservation(context);

            context.SaveChanges();
        }

        private static void InitEvaluation(ZhaoContext context)
        {
            var Evaluations = new List<Evaluation>()
            {
                new Evaluation 
                {
                    Prenom = "Mathieu",
                    Nom = "Lussier",
                    TypeReservation = TypeReservation.SalleManger,
                    Courriel = "Mathieu.Lussier@hotmail.com",
                    Datevisite = new DateTime(2020, 10, 6),
                    QualiteRepas = 4,
                    QualiteService = 3,
                    Commentaires = "Aucun commentaire"
                },
                new Evaluation
                {
                    Prenom = "Vincent",
                    Nom = "Ménard",
                    TypeReservation = TypeReservation.SalleManger,
                    Courriel = "Vincent.Ménard@hotmail.com",
                    Datevisite = new DateTime(2019, 11, 9),
                    QualiteRepas = 3,
                    QualiteService = 5,
                    Commentaires = "Aucun commentaire"
                }
            };

            foreach (Evaluation eval in Evaluations)
            {
                context.Evaluations.Add(eval);
            }
        }

        private static void InitPromotion(ZhaoContext context)
        {
            var Promotions = new List<Promotion>()
            {
                new Promotion
                {
                    TypePromotion = TypePromotion.Livraison,
                    TauxApplicable = 15,
                    DescriptionPromotion = "Prend en des frittes",
                    DateDebut = new DateTime(2018, 10, 6),
                    DateFin = new DateTime(2022, 11, 7)
                }
            };

            foreach (Promotion prom in Promotions)
            {
                context.Promotions.Add(prom);
            }
        }

        private static void InitReservation(ZhaoContext context)
        {
            var Reservations = new List<Reservation>()
            {
                new Reservation
                {
                    Prenom = "Mathieu",
                    Nom = "Lussier",
                    TypeReservation = TypeReservation.SalleManger,
                    Courriel = "Mathieu.Lussier@hotmail.com",
                    DateHeureReservation = new DateTime(2020, 10, 6),
                    NumeroTelephone = "(438) 222-3344",
                    NombrePersonnes = 1
                }
            };

            foreach (Reservation reserv in Reservations)
            {
                context.Reservations.Add(reserv);
            }
        }
    }
}
