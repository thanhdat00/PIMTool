using FluentValidation;
using PIMTool.Client.Presentation.ViewModels;
using System;
using System.Linq;

namespace PIMTool.Client.Validation
{
    class EditProjectValidator : AbstractValidator<EditProjectViewModel>
    {
        public EditProjectValidator()
        {
            RuleFor(p => p.ProjectName)
                .NotNull().WithMessage(ValidationConstant.EmptyInput)
                .NotEqual(ValidationConstant.EmptyString).WithMessage(ValidationConstant.EmptyInput)
                .MaximumLength(ValidationConstant.MaxCustomerLength).WithMessage(ValidationConstant.OverLengthInput);

            RuleFor(p => p.Member)
                .NotNull().WithMessage(ValidationConstant.EmptyInput)
                .Must(ValidateVisa).WithMessage(ValidationConstant.InvalidVisa);

            RuleFor(p => p.Customer)
                .NotNull().WithMessage(ValidationConstant.EmptyInput)
                .NotEqual(ValidationConstant.EmptyString).WithMessage(ValidationConstant.EmptyInput)
                .MaximumLength(ValidationConstant.MaxCustomerLength).WithMessage(ValidationConstant.OverLengthInput)
                .Must(HasSpecialCharater).WithMessage(ValidationConstant.ContainSpecialChar);

            RuleFor(p => p.FinishDate)
                .GreaterThan(p => p.StartDate).WithMessage(ValidationConstant.InvalidEndDate);
        }

        //TODO: Validate VISA which contains 3 char and only upcase char
        private static bool ValidateVisa(string arg)
        {
            if (arg != null)
            {
                arg.Trim(' ');
                string[] members = arg.Split(',');
                foreach (var item in members)
                {
                    if (item.Length != 3) return false;
                }
            }
            return true;
        }
        private static bool HasSpecialCharater(string s)
        {
            if (s == null) return false;
            return !s.Any(ch => !Char.IsLetterOrDigit(ch));
        }
    }
}
