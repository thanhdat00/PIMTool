using FluentValidation;
using PIMTool.Client.Presentation.ViewModels;
using PIMTool.Services.Resource;
using System;
using System.Collections.Generic;
using System.Linq;

namespace PIMTool.Client.Validation
{
    class DetailProjectValidator : AbstractValidator<DetailProjectViewModel>
    {
        private List<ProjectDto> _existingProjects;
        private List<EmployeeDto> _existingEmployees;
        private bool _isEditMode;

        public const int MaxProjectNumber = 10000000;
        public const int MaxProjectNameLength = 100;
        public const int MaxCustomerLength = 50;
        public const int Zero = 0;
        public DetailProjectValidator(List<ProjectDto> projects,List<EmployeeDto> existingEmployees, bool isEditMode)
        {
            _existingProjects = projects;
            _existingEmployees = existingEmployees;
            _isEditMode = isEditMode;

            RuleFor(p => p.ProjectNumber)
                .NotNull().WithMessage(ValidationResource.EmptyInput)
                .GreaterThan(Zero).WithMessage(ValidationResource.LessThanZero)
                .LessThan(MaxProjectNumber).WithMessage(ValidationResource.ProjectNumberOverMaxLength)
                .Must(CheckExistingProject).WithMessage(ValidationResource.ProjectNumberExisted);

            RuleFor(p => p.Name)
                .NotNull().WithMessage(ValidationResource.EmptyInput)
                .NotEqual(ValidationResource.EmptyString).WithMessage(ValidationResource.EmptyInput)
                .MaximumLength(MaxCustomerLength).WithMessage(ValidationResource.OverLengthInput);

            RuleFor(p => p.Customer)
                .NotNull().WithMessage(ValidationResource.EmptyInput)
                .NotEqual(ValidationResource.EmptyString).WithMessage(ValidationResource.EmptyInput)
                .MaximumLength(MaxCustomerLength).WithMessage(ValidationResource.OverLengthInput)
                .Must(HasSpecialCharater).WithMessage(ValidationResource.ContainSpecialChar);

            RuleFor(p => p.Member)
                .NotNull().WithMessage(ValidationResource.EmptyInput)
                .Must(ValidateVisa).WithMessage("Invalid Visa");

            RuleFor(p => p.FinishDate)
                .GreaterThan(p => p.StartDate).WithMessage(ValidationResource.InvalidEndDate);
        }

        private bool ValidateVisa(string arg)
        {
            if (arg != null)
            {
                arg.Trim(' ');
                string[] members = arg.Split(',');
                foreach (var item in members)
                {
                    if (item.Length != 3) return false;
                    bool checkExisted = false;
                    foreach(var employee in _existingEmployees)
                    {
                        if (item.Equals(employee))
                        {
                            checkExisted = true;
                            break;
                        }
                    }
                    if (!checkExisted) return false;
                }
            }
            return true;
        }

        private bool CheckExistingProject(int value)
        {
            if (!_isEditMode)
                foreach (var p in _existingProjects)
                {
                    if (value.Equals(p.ProjectNumber)) return false;
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
