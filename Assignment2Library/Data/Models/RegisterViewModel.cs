namespace Assignment2Library.Data.Models;
using System;
using System.ComponentModel.DataAnnotations;

public class RegisterViewModel
{
    public InputModel Input { get; set; } = new InputModel();

    public class InputModel
    {
        private string _email = "";
        [Required]
        [EmailAddress]
        public string Email
        {
            get
            {
                Console.WriteLine($"Getting Email: {_email}");
                return _email;
            }
            set
            {
                Console.WriteLine($"Setting Email to: {value}");
                _email = value;
            }
        }

        private string _password = "";
        [Required]
        [DataType(DataType.Password)]
        public string Password
        {
            get
            {
                Console.WriteLine("Getting Password");
                return _password;
            }
            set
            {
                Console.WriteLine("Setting Password");
                _password = value;
            }
        }

        private string _confirmPassword = "";
        [DataType(DataType.Password)]
        [Compare("Password", ErrorMessage = "The password and confirmation password do not match.")]
        public string ConfirmPassword
        {
            get
            {
                Console.WriteLine("Getting ConfirmPassword");
                return _confirmPassword;
            }
            set
            {
                Console.WriteLine("Setting ConfirmPassword");
                _confirmPassword = value;
            }
        }

        private string _firstName = "";
        [Required]
        public string FirstName
        {
            get
            {
                Console.WriteLine($"Getting FirstName: {_firstName}");
                return _firstName;
            }
            set
            {
                Console.WriteLine($"Setting FirstName to: {value}");
                _firstName = value;
            }
        }

        private string _lastName = "";
        [Required]
        public string LastName
        {
            get
            {
                Console.WriteLine($"Getting LastName: {_lastName}");
                return _lastName;
            }
            set
            {
                Console.WriteLine($"Setting LastName to: {value}");
                _lastName = value;
            }
        }
    }
}
