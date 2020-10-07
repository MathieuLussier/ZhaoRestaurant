using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using tp1_restaurant.Models;

namespace tp1_restaurant.Data
{
    public class ReservationData
    {
        private List<Reservation> reservations = new List<Reservation>();

        public ReservationData()
        {
            feedReservationsData();
        }

        private void feedReservationsData()
        {
            loadData();

            if (reservations.Count == 0)
            {
                Reservation reservation1 = new Reservation
                {
                    Prenom = "Mathieu",
                    Nom = "Lussier",
                    TypeReservation = TypeReservation.SalleManger,
                    Courriel = "Mathieu.Lussier@hotmail.com",
                    DateHeureReservation = new DateTime(2020, 10, 6, 18, 30, 0),
                    NumeroTelephone = "(438) 889-4324",
                    NombrePersonnes = 2
                };

                reservations.Add(reservation1);

                saveData();
            }
        }

        public void loadData()
        {
            try
            {
                using (StreamReader r = new StreamReader("reservations.json"))
                {
                    string json = r.ReadToEnd();
                    reservations = JsonConvert.DeserializeObject<List<Reservation>>(json);
                }
            } catch
            {
                return;
            }
        }

        public void saveData()
        {
            JsonSerializer serializer = new JsonSerializer();
            using (StreamWriter sw = new StreamWriter("reservations.json"))
            using (JsonWriter writer = new JsonTextWriter(sw))
            {
                serializer.Serialize(writer, reservations);
            }
        }

        public List<Reservation> GetReservations()
        {
            loadData();

            return reservations;
        }

        public Reservation GetReservationById(int id)
        {
            loadData();

            Reservation reservation = reservations.Find(m => m.Id == id);

            return reservation;
        }

        public void CreateReservation(Reservation reservation)
        {
            loadData();

            reservation.Id = reservations.Max(m => m.Id) + 1;
            reservations.Add(reservation);

            saveData();
        }

        public void EditReservation(Reservation reservation)
        {
            loadData();

            int index = reservations.FindIndex(m => m.Id == reservation.Id);
            reservations[index] = reservation;

            saveData();
        }

        public void DeleteReservationById(int id)
        {
            loadData();

            reservations.RemoveAll(m => m.Id == id);

            saveData();
        } 
    }
}
