using System;
using System.Collections.Generic;
using System.Linq;
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
using ScheduleCommon;
namespace ScheduleWPF
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        DaysModel model = new DaysModel();
        public MainWindow()
        {
            InitializeComponent();
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            comboClass.ItemsSource = Configuration.Instance.Groups;
            
        }

        private void comboClass_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            listMonday.ItemsSource = model.CurrentSchedule[0][(StudentGroup)comboClass.SelectedItem];
            listTuesday.ItemsSource = model.CurrentSchedule[1][(StudentGroup)comboClass.SelectedItem];
            listWednesday.ItemsSource = model.CurrentSchedule[2][(StudentGroup)comboClass.SelectedItem];
            listThursday.ItemsSource = model.CurrentSchedule[3][(StudentGroup)comboClass.SelectedItem];
            listFriday.ItemsSource = model.CurrentSchedule[4][(StudentGroup)comboClass.SelectedItem];
        }
    }
}
