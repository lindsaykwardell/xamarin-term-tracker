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
    public class AssessmentsDataAccess
    {
        private SQLiteConnection database;
        private static object collisionLock = new object();
        public ObservableCollection<Assessment> Assessments { get; set; }

        public AssessmentsDataAccess()
        {
            database = DependencyService.Get<IDatabaseConnection>().DbConnection();
            database.CreateTable<Assessment>();
            this.Assessments = new ObservableCollection<Assessment>(database.Table<Assessment>());
        }

        public void AddNewAssessment(Assessment newAssessment)
        {
            this.Assessments.
              Add(newAssessment);
            this.SaveAllAssessments();
        }

        public void UpdateAssessment(Assessment assessment)
        {
            var found = this.Assessments.FirstOrDefault(t => t.Id == assessment.Id);
            if (found != null)
            {
                found.Name = assessment.Name;
                found.Type = assessment.Type;
                found.DueDate = assessment.DueDate;
                found.ShowNotifications = assessment.ShowNotifications;
                this.SaveAllAssessments();
            }
        }

        public void SaveAllAssessments()
        {
            foreach(var course in this.Assessments)
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

        public void DeleteAssessment(Assessment assessment)
        {
            var id = assessment.Id;
            if (id != 0)
            {
                lock (collisionLock)
                {
                    database.Delete<Assessment>(id);
                }
            }
            this.Assessments.Remove(assessment);
        }
    }
}
