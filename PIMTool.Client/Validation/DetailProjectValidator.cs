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
        private List<ProjectResource> _existingProjects;
        private bool _isEditMode;
        public DetailProjectValidator(List<ProjectResource> projects, bool isEditMode)
        {
            _existingProjects = projects;
            _isEditMode = isEditMode;

            RuleFor(p => p.ProjectNumber)
                .NotNull().WithMessage(ValidationConstant.EmptyInput)
                .GreaterThan(ValidationConstant.Zero).WithMessage(ValidationConstant.LessThanZero)
                .LessThan(ValidationConstant.MaxProjectNumber).WithMessage(ValidationConstant.ProjectNumberOverMaxLength)
                .Must(CheckExistingProject).WithMessage(ValidationConstant.ProjectNumberExisted);

            RuleFor(p => p.ProjectName)
                .NotNull().WithMessage(ValidationConstant.EmptyInput)
                .NotEqual(ValidationConstant.EmptyString).WithMessage(ValidationConstant.EmptyInput)
                .MaximumLength(ValidationConstant.MaxCustomerLength).WithMessage(ValidationConstant.OverLengthInput);

            RuleFor(p => p.Member)
                .NotNull().WithMessage(ValidationConstant.EmptyInput)
                .Must(ValidateVisa).WithMessage(ValidationConstant.InvalidVisa);

            RuleFor(p => p.SelectedStatus)
                .NotNull().WithMessage(ValidationConstant.EmptyInput);

            RuleFor(p => p.Customer)
                .NotNull().WithMessage(ValidationConstant.EmptyInput)
                .NotEqual(ValidationConstant.EmptyString).WithMessage(ValidationConstant.EmptyInput)
                .MaximumLength(ValidationConstant.MaxCustomerLength).WithMessage(ValidationConstant.OverLengthInput)
                .Must(HasSpecialCharater).WithMessage(ValidationConstant.ContainSpecialChar);

            RuleFor(p => p.FinishDate)
                .GreaterThan(p => p.StartDate).WithMessage(ValidationConstant.InvalidEndDate);
        }

        private static bool ValidateVisa(string arg)
        {
            if (arg != null)
            {
                arg.Trim(' ');
                string[] members = arg.Split(',');
                foreach (var item in members)
                    if (item.Length != 3) return false;
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
