﻿using CinemasService.Dtos;
using CinemasService.Errors;
using FluentValidation;

namespace CinemasService.Validators
{
    public class LayoutCreateDtoValidator : AbstractValidator<LayoutCreateDto>
    {
        public LayoutCreateDtoValidator()
        {
            ClassLevelCascadeMode = CascadeMode.Stop;
            RuleFor(x => x.Name)
                .NotEmpty().WithMessage(LayoutErrors.Create.NameRequired)
                .MaximumLength(100).WithMessage(LayoutErrors.Create.NameMaxLength);

            RuleFor(x => x.Rows)
                .NotEmpty().WithMessage(LayoutErrors.Create.RowsRequired)
                .GreaterThan(0).WithMessage(LayoutErrors.Create.DimensionsTooSmall);

            RuleFor(x => x.Columns)
                .NotEmpty().WithMessage(LayoutErrors.Create.ColumnsRequired)
                .GreaterThan(0).WithMessage(LayoutErrors.Create.DimensionsTooSmall);

            RuleFor(x => x.Name)
                .NotEmpty().WithMessage(LayoutErrors.Create.NameRequired)
                .MaximumLength(100).WithMessage(LayoutErrors.Create.NameMaxLength);
            RuleFor(x => x.Elements)
                .NotEmpty()
                .WithMessage(LayoutErrors.Create.ElementsRequired);
        }
    }
}
