using System;
using System.IO;
using System.Windows;

namespace KursProjDemo
{
    public partial class HelpWindow : Window
    {
        private const string TipsFilePath = "Tips.txt";

        public HelpWindow()
        {
            InitializeComponent();
            try
            {
                if (File.Exists(TipsFilePath))
                {
                    string tips = File.ReadAllText(TipsFilePath);
                    HelpTextBlock.Text = tips;
                }
                else
                {
                    HelpTextBlock.Text = "Файл с советами не найден.";
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Ошибка при загрузке справки: {ex.Message}", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }
    }
}