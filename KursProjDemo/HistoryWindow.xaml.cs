using System;
using System.Collections.Generic;
using System.Windows;

namespace KursProjDemo
{
    public partial class HistoryWindow : Window
    {
        public HistoryWindow()
        {
            InitializeComponent();
            var history = DatabaseService.GetExerciseHistory();
            var records = new List<ExerciseHistoryRecord>();
            foreach (var record in history)
            {
                records.Add(new ExerciseHistoryRecord
                {
                    MethodName = record.MethodName,
                    CompletionDate = record.CompletionDate.ToString("yyyy-MM-dd HH:mm:ss"),
                    Status = record.IsCompleted ? "Выполнено" : "Не выполнено"
                });
            }
            HistoryDataGrid.ItemsSource = records;
        }
        private void CloseButton_Click(object sender, RoutedEventArgs e)
        {
            Close();
        }
    }
}