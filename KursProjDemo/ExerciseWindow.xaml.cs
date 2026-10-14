using System;
using System.Windows;
using System.Windows.Threading;

namespace KursProjDemo
{
    public partial class ExerciseWindow : Window
    {
        private readonly Exercise _exercise;
        private int _remainingTime;
        private DispatcherTimer _timer;
        private int _recordId;

        public ExerciseWindow(Exercise exercise)
        {
            InitializeComponent();
            _exercise = exercise;

            Closing += ExerciseWindow_Closing;
            try
            {
                string imagePath = $"Images/{_exercise.Image}";
                ExerciseImage.Source = new System.Windows.Media.Imaging.BitmapImage(new Uri(imagePath, UriKind.RelativeOrAbsolute));
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Ошибка загрузки изображения: {ex.Message}");
            }

            ExerciseDescription.Text = _exercise.Description;
            _remainingTime = _exercise.Duration;

            if (_remainingTime <= 0)
            {
                MessageBox.Show("Длительность упражнения должна быть больше 0 секунд.", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Warning);
                Close();
                return;
            }
            TimerText.Text = TimeSpan.FromSeconds(_remainingTime).ToString(@"mm\:ss");
            _recordId = DatabaseService.AddExerciseRecord(_exercise.Name);
            StartTimer();
        }

        private void ExerciseWindow_Closing(object sender, System.ComponentModel.CancelEventArgs e)
        {
            _timer.Stop();
        }

        private void StartTimer()
        {
            _timer = new DispatcherTimer
            {
                Interval = TimeSpan.FromSeconds(1)
            };
            _timer.Tick += Timer_Tick;
            _timer.Start();
        }

        private void Timer_Tick(object sender, EventArgs e)
        {
            if (_remainingTime > 0)
            {
                _remainingTime--;
                TimerText.Text = TimeSpan.FromSeconds(_remainingTime).ToString(@"mm\:ss");
            }
            else
            {
                _timer.Stop();
                if (IsLoaded)
                {
                    MessageBox.Show("Время истекло!", "Таймер", MessageBoxButton.OK, MessageBoxImage.Information);
                }
                Close();
            }
        }

        private void MarkAsDoneButton_Click(object sender, RoutedEventArgs e)
        {
            _timer.Stop();
            DatabaseService.UpdateExerciseCompletion(_recordId, true);
            MessageBox.Show($"Упражнение '{_exercise.Name}' отмечено как выполненное.", "Статус", MessageBoxButton.OK, MessageBoxImage.Information);
            Close();
        }
    }
}