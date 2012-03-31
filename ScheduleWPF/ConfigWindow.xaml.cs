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
using System.Windows.Shapes;
using ScheduleCommon;
using Microsoft.Win32;
namespace ScheduleWPF
{
    /// <summary>
    /// Interaction logic for ConfigWindow.xaml
    /// </summary>
    public partial class ConfigWindow : Window
    {
        public ConfigWindow()
        {
            InitializeComponent();
        }

        private void buttonClose_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            listGroups.ItemsSource = Configuration.Instance.Groups;
            listCourses.ItemsSource = Configuration.Instance.Courses;
            listRooms.ItemsSource = Configuration.Instance.Rooms;
            listProfessors.ItemsSource = Configuration.Instance.Professors;
            listConstraints.ItemsSource = Configuration.Instance.Constraints;
            comboProfessors.ItemsSource = Configuration.Instance.Professors;
        }

        private void buttonCreateGroup_Click(object sender, RoutedEventArgs e)
        {
            if (txtGroupName.Text.Trim() != string.Empty)
            {
                StudentGroup newGroup = new StudentGroup(txtGroupName.Text);
                if (Configuration.Instance.Groups.Contains(newGroup))
                {
                    MessageBox.Show("Group already exists!");
                    return;
                }
                Configuration.Instance.Groups.Add(newGroup);
            }
            else MessageBox.Show("A group name is required.");
        }

        private void buttonDeleteGroup_Click(object sender, RoutedEventArgs e)
        {
            Configuration.Instance.Groups.Remove((StudentGroup)listGroups.SelectedItem);
        }

        private void buttonCreateCourse_Click(object sender, RoutedEventArgs e)
        {
            if (comboProfessors.SelectedIndex == -1)
            {
                MessageBox.Show("A professor is required.");
                return;
            }
            if (comboCourseType.SelectedIndex == -1)
            {
                MessageBox.Show("A course type is required.");
                return;
            }
            if (txtCourseName.Text.Trim() != string.Empty)
            {
                Course newCourse = new Course(txtCourseName.Text, comboProfessors.SelectedItem as Professor, comboCourseType.SelectedIndex == 0 ? CourseType.NormalCourse : CourseType.ComputerCourse);
                Configuration.Instance.Courses.Add(newCourse);
            }
            else MessageBox.Show("A course name is required.");
        }

        private void buttonDeleteCourse_Click(object sender, RoutedEventArgs e)
        {
            Configuration.Instance.Courses.Remove((Course)listCourses.SelectedItem);
        }

        private void buttonCreateProfessor_Click(object sender, RoutedEventArgs e)
        {
            if (txtProfessorName.Text.Trim() != string.Empty)
            {
                Professor newProfessor = new Professor(txtProfessorName.Text);
                Configuration.Instance.Professors.Add(newProfessor);
            }
            else MessageBox.Show("A professor name is required.");
        }

        private void buttonDeleteProfessor_Click(object sender, RoutedEventArgs e)
        {
            Configuration.Instance.Professors.Remove((Professor)listProfessors.SelectedItem);
        }

        private void buttonCreateRoom_Click(object sender, RoutedEventArgs e)
        {
            if (comboRoomType.SelectedIndex == -1)
            {
                MessageBox.Show("A room type is required.");
                return;
            }
            if (txtRoomName.Text.Trim() != string.Empty)
            {
                Room newRoom = new Room(txtRoomName.Text, comboRoomType.SelectedIndex == 0 ? CourseType.NormalCourse : CourseType.ComputerCourse);
                Configuration.Instance.Rooms.Add(newRoom);
            }
            else MessageBox.Show("A room name is required.");
        }

        private void buttonDeleteRoom_Click(object sender, RoutedEventArgs e)
        {
            Configuration.Instance.Rooms.Remove((Room)listRooms.SelectedItem);
        }

        private void buttonSave_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                SaveFileDialog saveDialog = new SaveFileDialog();
                saveDialog.Filter = "Schedule configuration|*.schcf";
                saveDialog.DefaultExt = ".schcf";
                if (saveDialog.ShowDialog().Value == true)
                {
                    Configuration.Instance.SaveToFile(saveDialog.FileName);
                }
            }
            catch (Exception)
            {
                MessageBox.Show("Oops. Couldn't save!");
            }
        }

        private void buttonLoad_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                OpenFileDialog openDialog = new OpenFileDialog();
                openDialog.Filter = "Schedule configuration|*.schcf";
                if (openDialog.ShowDialog().Value == true)
                {
                    Configuration.Instance.LoadFromFile(openDialog.FileName);
                }
            }
            catch (Exception)
            {
                MessageBox.Show("Oops. Couldn't load!");
            }
        }
    }
}
