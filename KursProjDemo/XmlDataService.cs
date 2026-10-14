using System;
using System.Collections.Generic;
using System.IO;
using System.Windows;
using System.Xml.Serialization;

namespace KursProjDemo
{
    public static class XmlDataService
    {

        public static List<Exercise> LoadExercises()
        {
            string filePath = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "Exercises.xml");
            if (!File.Exists(filePath))
            {
                MessageBox.Show($"Файл {filePath} не найден.", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
                return new List<Exercise>();
            }

            try
            {
                var serializer = new XmlSerializer(typeof(List<Exercise>));
                using (var reader = new StreamReader(filePath))
                {
                    return (List<Exercise>)serializer.Deserialize(reader);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Ошибка загрузки упражнений: {ex.Message}", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
                return new List<Exercise>();
            }
        }

        public static List<Exercise> LoadExercisesWithStatus()
        {
            try
            {
                var exercises = LoadExercises();
                if (exercises == null || exercises.Count == 0)
                {
                    Console.WriteLine("Список упражнений пуст!");
                    return new List<Exercise>();
                }

                var history = DatabaseService.GetExerciseHistory();
                if (history == null)
                {
                    Console.WriteLine("История выполнения пуста!");
                    return exercises;
                }

                foreach (var exercise in exercises)
                {
                    exercise.IsCompleted = history.Exists(h => h.MethodName == exercise.Name && h.IsCompleted);
                }

                return exercises;
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Ошибка загрузки упражнений со статусом: {ex.Message}");
                return new List<Exercise>();
            }
        }
    }
}