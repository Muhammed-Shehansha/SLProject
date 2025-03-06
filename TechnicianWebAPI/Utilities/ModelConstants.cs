namespace Constants
{
    public static class ModelConstants
    {
        // Regular Expression Patterns for Users
        public const string NamePattern = @"^[A-Za-z\s]+$";
        public const string UserNamePattern = @"^[A-Za-z][A-Za-z0-9]*$";
        public const string PasswordPattern = @"^(?=.*[a-z])(?=.*[A-Z])(?=.*\d)(?=.*[!@#$%^&*(),.?""':{}|<>]).{5,16}$";
        public const string EmailPattern = @"^[a-zA-Z0-9._%+-]+@[a-zA-Z0-9.-]+\.[a-zA-Z]{2,}$";
        public const string PhoneNumberPattern = @"^[6-9]\d{9}$";

        // Error Messages for Users
        public const string InvalidUserNameMessage = "Username must start with a letter and contain only letters or numbers.";
        public const string InvalidNameMessage = "Invalid Name. Only alphabetic characters and spaces are allowed.";
        public const string InvalidPasswordMessage = "Password didn't match the criteria.";
        public const string InvalidEmailMessage = "Invalid email Id.";
        public const string InvalidPhoneNumberMessage = "Invalid phone number. It must start with 6-9 and contain 10 digits.";
    }
}
