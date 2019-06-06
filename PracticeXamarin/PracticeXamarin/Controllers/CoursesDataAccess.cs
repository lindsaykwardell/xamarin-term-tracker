using SQLite;
using System.Collections.Generic;
using System.Linq;
using Xamarin.Forms;
using System.Collections.ObjectModel;
using PracticeXamarin.Interfaces;
using Models;
using System;
using PracticeXamarin.Models;

namespace PracticeXamarin.Controllers
{
    public class CoursesDataAccess
    {
        private SQLiteConnection database;
        private static object collisionLock = new object();
        public ObservableCollection<Course> Courses { get; set; }

        public CoursesDataAccess()
        {
            database = DependencyService.Get<IDatabaseConnection>().DbConnection();
            database.CreateTable<Course>();
            this.Courses = new ObservableCollection<Course>(database.Table<Course>());
        }

        public void AddNewCourse(Course newCourse)
        {
            this.Courses.
              Add(newCourse);
            this.SaveAllCourses();
        }

        public void UpdateCourse(Course course)
        {
            var foundCourse = this.Courses.FirstOrDefault(t => t.Id == course.Id);
            if (foundCourse != null)
            {
                foundCourse.Name = course.Name;
                foundCourse.StartDate = course.StartDate;
                foundCourse.EndDate = course.EndDate;
                foundCourse.Status = course.Status;
                foundCourse.InstructorName = course.InstructorName;
                foundCourse.InstructorPhone = course.InstructorPhone;
                foundCourse.InstructorEmail = course.InstructorEmail;
                foundCourse.Notes = course.Notes;
                foundCourse.ShowNotifications = course.ShowNotifications;
                this.SaveAllCourses();
            }
        }

        public void SaveAllCourses()
        {
            foreach(var course in this.Courses)
            {
                if(course.Id != 0)
                {
                    database.Update(course);
                }
                else
                {
                    database.Insert(course);
                }
            }
        }

        public void DeleteCourse(Course course)
        {
            var id = course.Id;
            if (id != 0)
            {
                lock (collisionLock)
                {
                    database.Delete<Course>(id);
                }
            }
            this.Courses.Remove(course);
        }
    }
}
