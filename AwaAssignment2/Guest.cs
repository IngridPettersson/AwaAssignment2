using System;
using System.Collections.Generic;
using System.Text;

namespace awa_assignment_2
{
    class Guest
    {
        // Private field
        private string name;
        // Public property
        public string Name 
        { 
            get 
            { 
                return name; 
            } 
            set 
            { 
                name = value; 
            } 
        }

        private double yearsAsFriends;
        public double YearsAsFriends 
        { 
            get 
            { 
                return yearsAsFriends; 
            } 
            set 
            { 
                yearsAsFriends = value; 
            } 
        }

        private string comment;
        public string Comment 
        { 
            get 
            { 
                return comment; 
            } 
            set 
            { 
                comment = value; 
            } 
        }

        private double surpriseSaladGrade;
        public double SurpriseSaladGrade
        {
            get 
            { 
                return surpriseSaladGrade; 
            }
            set
            {
                if (value <= 5 && value >= 0)
                    surpriseSaladGrade = value;
                else
                    surpriseSaladGrade = -1;
            }
        }

        // Constructor
        public Guest(string name, double yearsAsFriends)
        {
            this.Name = name;
            this.YearsAsFriends = yearsAsFriends;

            Console.WriteLine($"NAME: {name}\nYEARS AS FRIENDS: {yearsAsFriends}\n");
        }

        // Constructor
        public Guest(string name, double surpriseSaladGrade, string comment)
        {
            this.Name = name;
            this.SurpriseSaladGrade = surpriseSaladGrade;
            this.Comment = comment;

            string commentToGuest;
            if (WelcomeAgain())
                commentToGuest = "I knew I could count on you! :)";
            else
                commentToGuest = "Apple for you next time - alone!";

            Console.WriteLine($"NAME: {name}\nGRADE: {surpriseSaladGrade}\nCOMMENT: {comment}\nCOMMENT FROM YOU TO GUEST: {commentToGuest}\n");
        }

        public bool WelcomeAgain()
        {
            return surpriseSaladGrade >= 4;
        }
    }
}
