using SQLite;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Text;

namespace PracticeXamarin.Models
{
    public enum AssessmentType
    {
        Objective,
        Performance
    }

    [Table("Assessments")]
    public class Assessment
    {
        private int _id;
        [PrimaryKey, AutoIncrement]
        public int Id
        {
            get
            {
                return _id;
            }
            set
            {
                this._id = value;
                OnPropertyChanged(nameof(Id));
            }
        }
        private int _courseId;
        public int CourseId
        {
            get
            {
                return _courseId;
            }
            set
            {
                this._courseId = value;
                OnPropertyChanged(nameof(CourseId));
            }
        }
        private string _name;
        public string Name
        {
            get
            {
                return _name;
            }
            set
            {
                this._name = value;
                OnPropertyChanged(nameof(Name));
            }
        }
        private AssessmentType _type;
        public AssessmentType Type
        {
            get
            {
                return _type;
            }
            set
            {
                this._type = value;
                OnPropertyChanged(nameof(Type));
            }
        }

        private DateTime _dueDate;
        public DateTime DueDate
        {
            get
            {
                return _dueDate;
            }
            set
            {
                this._dueDate = value;
                OnPropertyChanged(nameof(DueDate));
            }
        }

        private Boolean _showNotifications;
        public Boolean ShowNotifications
        {
            get
            {
                return _showNotifications;
            }
            set
            {
                this._showNotifications = value;
                OnPropertyChanged(nameof(ShowNotifications));
            }
        }

        public event PropertyChangedEventHandler PropertyChanged;
        private void OnPropertyChanged(string propertyName)
        {
            this.PropertyChanged?.Invoke(this,
              new PropertyChangedEventArgs(propertyName));
        }
    }
}
