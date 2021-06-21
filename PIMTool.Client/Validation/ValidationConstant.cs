namespace PIMTool.Client.Validation
{
    public class ValidationConstant
    {
        public const string EmptyInput = "Input muust not be empty";
        public const string InvalidVisa = "Input Visa in invalid";
        public const string LessThanZero = "Input must be more than 0";
        public const string ProjectNumberOverMaxLength = "Project number must be less than 8 digit";
        public const string ProjectNumberExisted = "Project number already existed";
        public const string ContainSpecialChar = "Input must not contain special character";
        public const string InvalidEndDate = "Finish date must be after the start date";
        public const string OverLengthInput = "Input is too long";
        public const string EmptyString = "";
        public const int MaxProjectNumber = 10000000;
        public const int MaxProjectNameLength = 100;
        public const int MaxCustomerLength = 50;
        public const int Zero = 0;
    }
}
