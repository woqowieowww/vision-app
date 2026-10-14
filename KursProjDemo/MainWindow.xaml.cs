using System;
using System.Windows;

namespace KursProjDemo
{
    public partial class MainWindow : Window
    {
        private Exercise _selectedExercise;

        public MainWindow()
        {
            InitializeComponent();
            DataContext = XmlDataService.LoadExercisesWithStatus();
            ExercisesList.SelectionChanged += ExercisesList_SelectionChanged;
        }
        private void ExercisesList_SelectionChanged(object sender, RoutedEventArgs e)
        {
            _selectedExercise = ExercisesList.SelectedItem as Exercise;
        }
        private void StartExercise_Click(object sender, RoutedEventArgs e)
        {
            if (_selectedExercise == null)
            {
                MessageBox.Show("Выберите упражнение из списка!", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Warning);
                return;
            }

            var exerciseWindow = new ExerciseWindow(_selectedExercise);
            exerciseWindow.ShowDialog();
        }

        private void MarkAsDone_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                if (_selectedExercise == null)
                {
                    MessageBox.Show("Выберите упражнение из списка!", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Warning);
                    return;
                }

                System.Diagnostics.Debug.WriteLine($"Отмечаем выполненным: {_selectedExercise.Name}");
                int recordId = DatabaseService.AddExerciseRecord(_selectedExercise.Name);
                System.Diagnostics.Debug.WriteLine($"Добавлена запись с ID: {recordId}");

                DatabaseService.UpdateExerciseCompletion(recordId, true);

                RefreshExercisesList();

                MessageBox.Show($"Упражнение '{_selectedExercise.Name}' отмечено как выполненное.",
                                "Статус", MessageBoxButton.OK, MessageBoxImage.Information);
            }
            catch { }
        }

        private void RefreshExercisesList()
        {
            try
            {
                System.Diagnostics.Debug.WriteLine("Обновление списка упражнений...");
                var exercises = XmlDataService.LoadExercisesWithStatus();
                if (exercises == null || exercises.Count == 0)
                {
                    System.Diagnostics.Debug.WriteLine("Список упражнений пуст!");
                }
                else
                {
                    System.Diagnostics.Debug.WriteLine($"Загружено упражнений: {exercises.Count}");
                }
                DataContext = exercises;
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine($"Ошибка при обновлении списка: {ex}");
            }
        }

        private void ShowHistory_Click(object sender, RoutedEventArgs e)
        {
            var historyWindow = new HistoryWindow();
            historyWindow.Show();
        }

        private void ShowHelp_Click(object sender, RoutedEventArgs e)
        {
            var helpWindow = new HelpWindow();
            helpWindow.Show();
        }
    }
}