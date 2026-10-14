# vision-app — упражнения для зрения

![C#](https://img.shields.io/badge/C%23-239120?logo=csharp&logoColor=white)
![.NET Framework](https://img.shields.io/badge/.NET%20Framework-4.8-512BD4?logo=dotnet&logoColor=white)
![WPF](https://img.shields.io/badge/UI-WPF-informational)
![Platform](https://img.shields.io/badge/platform-Windows-0078D6?logo=windows&logoColor=white)

Настольное приложение для Windows: подборка **гимнастики для глаз** с таймером, подсказками и историей выполнения. Написано на C# и WPF, оформлено в стиле Material Design.

## Возможности

- **15 упражнений** с описанием, иллюстрацией и длительностью — от 30 секунд до 5 минут.
- **Таймер** упражнения: кнопка «Начать» запускает отсчёт.
- Кнопка **«Выполнено»** — отметить упражнение, если прервали его досрочно.
- **История** выполненных упражнений в локальной базе SQLite.
- **Справка** и советы по каждому упражнению (`Tips.txt`).
- Интерфейс на [Material Design in XAML](https://github.com/MaterialDesignInXAML/MaterialDesignInXamlToolkit).

## Упражнения

| Упражнение | Длительность |
|---|---|
| Пальминг | 2 мин |
| Метка на стекле | 1 мин |
| Цветовые круги | 3 мин |
| Горизонтальные движения | 1 мин |
| Вертикальные движения | 1 мин |
| Фокусировка на пальце | 2 мин |
| Солнечные зайчики | 1 мин |
| Моргание | 30 с |
| Вращение глазами | 1 мин |
| Просмотр вдаль | 5 мин |
| Массаж век | 2 мин |
| Диагональные движения | 2 мин |
| Фокусировка на сетке | 1,5 мин |
| Смена фокуса | 2 мин |
| Световые контрасты | 2 мин |

Список и длительности хранятся в `KursProjDemo/bin/Debug/Exercises.xml`.

## Стек

| Компонент | Версия |
|---|---|
| .NET Framework | 4.8 |
| UI | WPF |
| MaterialDesignThemes / MaterialDesignColors | 5.2.1 |
| Microsoft.Xaml.Behaviors.Wpf | 1.1.39 |
| System.Data.SQLite | 1.0.117.0 |
| EntityFramework | 6.4.4 |

## Сборка и запуск

Нужны Windows и [Visual Studio 2022](https://visualstudio.microsoft.com/) с рабочей нагрузкой «Разработка классических приложений .NET».

```bash
git clone https://github.com/woqowieowww/vision-app.git
```

1. Откройте `KursProjDemo.sln`.
2. Дождитесь восстановления NuGet-пакетов (или *Сервис → Диспетчер пакетов NuGet → Восстановить пакеты*).
3. Выберите конфигурацию `Debug` и нажмите **F5**.

База `exercises.db` с историей создаётся при первом запуске. Папки `packages/`, `obj/` и почти всё содержимое `bin/` в репозиторий не входят — они появляются при сборке.

## Структура

```text
vision-app/
├── KursProjDemo.sln
└── KursProjDemo/
    ├── MainWindow.xaml(.cs)        выбор упражнения
    ├── ExerciseWindow.xaml(.cs)    выполнение упражнения с таймером
    ├── HistoryWindow.xaml(.cs)     история выполнения
    ├── HelpWindow.xaml(.cs)        справка
    ├── DatabaseService.cs          работа с SQLite (история)
    ├── XmlDataService.cs           чтение Exercises.xml
    ├── Exercise.cs                 модель упражнения
    ├── Images/                     иллюстрации упражнений
    ├── Tips.txt                    советы по выполнению
    └── bin/Debug/Exercises.xml     список упражнений
```
