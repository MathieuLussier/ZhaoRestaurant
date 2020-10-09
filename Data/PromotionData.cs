using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using tp1_restaurant.Models;

namespace tp1_restaurant.Data
{
    public class PromotionData
    {
        private List<Promotion> promotions = new List<Promotion>();

        public PromotionData()
        {
            feedPromotionsData();
        }

        private void feedPromotionsData()
        {
            loadData();

            if (promotions.Count == 0)
            {
                Promotion promotion1 = new Promotion
                {
                    TypePromotion = TypePromotion.Livraison,
                    TauxApplicable = 5,
                    DescriptionPromotion = "Promotion à la livraison, acheter pour plus de 20$ pour un rabais de 5% sur la facture complète.",
                    DateDebut = new DateTime(2020, 11, 1),
                    DateFin = new DateTime(2020, 12, 1)
                };

                promotions.Add(promotion1);

                saveData();
            }
        }

        public void loadData()
        {
            try
            {
                using (StreamReader r = new StreamReader("Promotions.json"))
                {
                    string json = r.ReadToEnd();
                    promotions = JsonConvert.DeserializeObject<List<Promotion>>(json);
                }
            }
            catch
            {
                return;
            }
        }

        public void saveData()
        {
            JsonSerializer serializer = new JsonSerializer();
            using (StreamWriter sw = new StreamWriter("Promotions.json"))
            using (JsonWriter writer = new JsonTextWriter(sw))
            {
                serializer.Serialize(writer, promotions);
            }
        }

        public List<Promotion> GetPromotions()
        {
            loadData();

            return promotions;
        }

        public Promotion GetPromotionById(int id)
        {
            loadData();

            Promotion promotion = promotions.Find(m => m.Id == id);

            return promotion;
        }

        public void CreatePromotion(Promotion Promotion)
        {
            loadData();

            Promotion.Id = promotions.Max(m => m.Id) + 1;
            promotions.Add(Promotion);

            saveData();
        }

        public void EditPromotion(Promotion Promotion)
        {
            loadData();

            int index = promotions.FindIndex(m => m.Id == Promotion.Id);
            promotions[index] = Promotion;

            saveData();
        }

        public void DeletePromotionById(int id)
        {
            loadData();

            promotions.RemoveAll(m => m.Id == id);

            saveData();
        }
    }
}
