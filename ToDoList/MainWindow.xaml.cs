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

        private ObservableCollection<Task> filteredTasks; 
        private bool showAllTasks = true;
        


        public DateOnly? SelectedDate { get; set; }
        public MainWindow()
        {
            InitializeComponent();
            DataContext = this;
            TasksListView.ItemsSource = Tasks;
            TaskCalendar.SelectedDate = DateTime.Today;
            SelectedDate = DateOnly.FromDateTime(DateTime.Today);
            ShowAllTasks();

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

        private void EditTaskButton_Click(object sender, RoutedEventArgs e)
        {
            if (TasksListView.SelectedItem == null)
            {
                MessageBox.Show("Пожалуйста, выберите задачу для редактирования!");
                return;
            }

            
            var selectedTask = (Task)TasksListView.SelectedItem;

            if(selectedTask.IsCompleted)
            {
                MessageBox.Show("Невозможно редактировать выполненную задачу");
                return;
                   
            }

            TaskTextBox.Text = selectedTask.Title;
            TaskCalendar.SelectedDate = selectedTask.DateTask.ToDateTime(TimeOnly.MinValue);
            Tasks.Remove(selectedTask);

        }

        private void DeleteTaskButton_Click(object sender, RoutedEventArgs e)
        {
            if (TasksListView.SelectedItem == null)
            {
                MessageBox.Show("Пожалуйста, выберите задачу для удаления!");
                return;
            }

            var selectedTask = (Task)TasksListView.SelectedItem;

            var result = MessageBox.Show($"Вы уверены, что хотите удалить задачу \"{selectedTask.Title}\"?",
                "Подтверждение удаления", MessageBoxButton.YesNo, MessageBoxImage.Question);

            if (result == MessageBoxResult.Yes)
            {
                Tasks.Remove(selectedTask);
            }
        }

        private void ShowAllTasks()
        {
            showAllTasks = true;
            TasksListView.ItemsSource = Tasks;
        }

        private void ShowTasksForDate(DateOnly date)
        {
            showAllTasks = false;
            filteredTasks = new ObservableCollection<Task>(
            Tasks.Where(task => task.DateTask == date)
            );
            TasksListView.ItemsSource = filteredTasks;
        }

        private void TaskCalendar_SelectedDatesChanged(object sender, SelectionChangedEventArgs e)
        {
            if (TaskCalendar.SelectedDate != null)
            {
                var selectedDate = DateOnly.FromDateTime(TaskCalendar.SelectedDate.Value);
                ShowTasksForDate(selectedDate);
            }
        }

        private void ShowAllTasksButton_Click(object sender, RoutedEventArgs e)
        {
            ShowAllTasks();
        }
    }
}
