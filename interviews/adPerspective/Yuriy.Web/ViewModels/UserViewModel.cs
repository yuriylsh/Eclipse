﻿namespace Yuriy.Web.ViewModels
{
    public class UserViewModel
    {
        public UserViewModel(int id, string firstName, string lastName)
        {
            Id = id;
            FirstName = firstName;
            LastName = lastName;
        }

        public int Id { get; }
        public string FirstName { get;}
        public string LastName { get; }
    }
}