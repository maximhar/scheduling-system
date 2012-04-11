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
        DaysModel model;
        public MainWindow()
        {
            InitializeComponent();
        }
        
        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            model = (DaysModel)this.DataContext;
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

        private void buttonOpenConfig_Click(object sender, RoutedEventArgs e)
        {
            ConfigWindow configurationWindow = new ConfigWindow();
            configurationWindow.ShowDialog();
        }

        private void RemoveButton_Click(object sender, RoutedEventArgs e)
        {
            var itemToRemove = (Class)((Button)sender).DataContext;
            model.RemoveClass(itemToRemove);
        }

        private void ComboBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            try
            {
                ((Class)((ComboBox)sender).DataContext).Room = (Room)((ComboBox)sender).SelectedItem;
                model.EvaluateConstraints();
            }
            catch { }
        }
    }
}
