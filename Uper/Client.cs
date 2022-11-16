using System;
using System.ComponentModel;

namespace Uper

{
    //using System.Data;
    internal class Client
    {
        Client(int id, string FitstName, string LastName, string Patronymic, DateTime Birthday, DateTime RegistrationDate,
        string Email, string Phone, string PhotoPath, string GenderCode)
        { }


        // Private properties.
        private int _Id = 0;
        private string _FitstName = null;
        private string _LastName = null;
        private string _Patronymic = null;
        private DateTime _Birthday = DateTime.Now;
        private DateTime _RegistrationDate = DateTime.Now;
        private string _Email = null;
        private string _Phone = null;
        private string _PhotoPath = null;
        private string _GenderCode = null;

        // Public properties.
        public int Id
        {
            get { return this._Id; }
            set
            {
                this._Id = value;
            }
        }
        public string FitstName
        {
            get { return this._FitstName; }
            set
            {
                if (this._FitstName != value)
                {
                    this._FitstName = value;
                }
            }
        }
        public string LastName
        {
            get { return this._LastName; }
            set
            {
                if (this._LastName != value)
                {
                    this._LastName = value;
                }
            }
        }
        public string Patronymic
        {
            get { return this._Patronymic; }
            set
            {
                if (this._Patronymic != value)
                {
                    this._Patronymic = value;
                }
            }
        }
        public DateTime Birthday
        {
            get { return this._Birthday; }
            set
            {
                if (this._Birthday != value)
                    this._Birthday = value;
            }
        }
        public DateTime RegistrationDate
        {
            get { return this._RegistrationDate; }
            set
            {
                if (this._RegistrationDate != value)
                {
                    this._RegistrationDate = value;
                }
            }
        }
        public string Email
        {
            get { return this._Email; }
            set
            {
                if (this._Email != value)
                    this._Email = value;
            }
        }
        public string Phone
        {
            get { return this._Phone; }
            set
            {
                if (this._Phone != value)
                    this._Phone = value;
            }
        }
        public string PhotoPath
        {
            get { return this._PhotoPath; }
            set
            {
                if (this._PhotoPath != value)
                    this._PhotoPath = value;
            }
        }
        public string GenderCode
        {
            get { return this._GenderCode; }
            set
            {
                if (this._GenderCode != value)
                    this._GenderCode = value;
            }
        }

        //// Implement INotifyPropertyChanged interface.
        //public event PropertyChangedEventHandler PropertyChanged;

        //private void NotifyPropertyChanged(int Id)
        //{
        //    if (PropertyChanged != null)
        //    {
        //        PropertyChanged(this, new PropertyChangedEventArgs(Id));
        //    }
        //}

        // Implement IEditableObject interface.
        //    public void BeginEdit()
        //    {
        //        if (m_Editing == false)
        //        {
        //            temp_Task = this.MemberwiseClone() as Task;
        //            m_Editing = true;
        //        }
        //    }

        //    public void CancelEdit()
        //    {
        //        if (m_Editing == true)
        //        {
        //            this.ProjectName = temp_Task.ProjectName;
        //            this.TaskName = temp_Task.TaskName;
        //            this.DueDate = temp_Task.DueDate;
        //            this.Complete = temp_Task.Complete;
        //            m_Editing = false;
        //        }
        //    }

        //    public void EndEdit()
        //    {
        //        if (m_Editing == true)
        //        {
        //            temp_Task = null;
        //            m_Editing = false;
        //        }
        //    }
        //}
        // Requires using System.Collections.ObjectModel;
        //public class Tasks : ObservableCollection<Task>
        //{
        //    // Creating the Tasks collection in this way enables data binding from XAML.
        //}
        //Процедура в базе
        //      INSERT INTO Client(FirstName, LastName, Patronymic, Birthday, RegistrationDate,
        //      Email, Phone, PhotoPath, GenderCode)

        //      VALUES(@firstName, @lastName, @patronymic, @birthday, @registrationDate,
        //      @email, @phone, @photoPath, @genderCode)

    }
}
