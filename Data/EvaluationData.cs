using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using tp1_restaurant.Models;

namespace tp1_restaurant.Data
{
    public class EvaluationData
    {
        private List<Evaluation> evaluations = new List<Evaluation>();

        public EvaluationData()
        {
            feedEvaluationsData();
        }

        private void feedEvaluationsData()
        {
            loadData();

            if (evaluations.Count == 0)
            {
                Evaluation evaluation1 = new Evaluation
                {
                    Prenom = "Mathieu",
                    Nom = "Lussier",
                    TypeReservation = TypeReservation.SalleManger,
                    Courriel = "Mathieu.Lussier@hotmail.com",
                    Datevisite = new DateTime(2020, 10, 6),
                    QualiteRepas = 4,
                    QualiteService = 4,
                    Commentaires = "Lorem Ipsum is simply dummy text of the printing and typesetting industry. Lorem Ipsum has been the industry's standard dummy text ever since the 1500s, when an unknown printer took a galley of type and scrambled it to make a type specimen book. It has survived not only five centuries, but also the leap into electronic typesetting, remaining essentially unchanged."
                };

                evaluations.Add(evaluation1);

                saveData();
            }
        }

        public void loadData()
        {
            try
            {
                using (StreamReader r = new StreamReader("evaluations.json"))
                {
                    string json = r.ReadToEnd();
                    evaluations = JsonConvert.DeserializeObject<List<Evaluation>>(json);
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
            using (StreamWriter sw = new StreamWriter("evaluations.json"))
            using (JsonWriter writer = new JsonTextWriter(sw))
            {
                serializer.Serialize(writer, evaluations);
            }
        }

        public List<Evaluation> GetEvaluations()
        {
            loadData();

            return evaluations;
        }

        public Evaluation GetEvaluationById(int id)
        {
            loadData();

            Evaluation evaluation = evaluations.Find(m => m.Id == id);

            return evaluation;
        }

        public void CreateEvaluation(Evaluation evaluation)
        {
            loadData();

            evaluation.Id = evaluations.Max(m => m.Id) + 1;
            evaluations.Add(evaluation);

            saveData();
        }

        public void EditEvaluation(Evaluation evaluation)
        {
            loadData();

            int index = evaluations.FindIndex(m => m.Id == evaluation.Id);
            evaluations[index] = evaluation;

            saveData();
        }

        public void DeleteEvaluationById(int id)
        {
            loadData();

            evaluations.RemoveAll(m => m.Id == id);

            saveData();
        }
    }
}
