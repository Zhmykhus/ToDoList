using System.Collections.ObjectModel;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace ToDoList
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public ObservableCollection<Task> Tasks = new ObservableCollection<Task>
    {
        new Task { Id = 1, Title = "Мяу", DateTask = new DateOnly(2025, 9, 28), IsCompleted = false },
        new Task { Id = 2, Title = "Мяу2", DateTask = new DateOnly(2025, 9, 28), IsCompleted = false },
    };


        public DateOnly? SelectedDate { get; set; }
        public MainWindow()
        {
            InitializeComponent();
            DataContext = this;
            TasksListView.ItemsSource = Tasks;
            TaskCalendar.SelectedDate = DateTime.Today;
            SelectedDate = DateOnly.FromDateTime(DateTime.Today);

        }


        private void AddTaskButton_Click(object sender, RoutedEventArgs e)
        {
            if (TaskCalendar.SelectedDate == null)
            {
                MessageBox.Show("Пожалуйста, выберите дату!");
                return;
            }

            if (string.IsNullOrWhiteSpace(TaskTextBox.Text))
            {
                MessageBox.Show("Пожалуйста, введите текст задачи!");
                return;
            }

            var selectedDate = DateOnly.FromDateTime(TaskCalendar.SelectedDate.Value);

            var newTask = new Task
            {
                Id = Tasks.Count + 1,
                Title = TaskTextBox.Text.Trim(),
                DateTask = selectedDate,
                IsCompleted = false
            };
            Tasks.Add(newTask);
            TaskTextBox.Text = string.Empty;
        }


    }
}
}