using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;

namespace AirBB.Models
{
    public class PastYearAttribute : ValidationAttribute, IClientModelValidator
    {
        private int maxYears;
        public PastYearAttribute(int years) {
            maxYears = years;
        }

        protected override ValidationResult? IsValid(object? value, ValidationContext ctx)
        {
            if (value is DateTime builtYear)
            {
                int yearDiff = DateTime.Today.Year - builtYear.Year;

                if (builtYear > DateTime.Today)
                {
                    return new ValidationResult($"{ctx.DisplayName} cannot be in the future.");
                }

                if (yearDiff > maxYears)
                {
                    return new ValidationResult($"{ctx.DisplayName} cannot be more than {maxYears} years old.");
                }

                return ValidationResult.Success;
            }

            return new ValidationResult($"{ctx.DisplayName} is not a valid date.");
        }

        public void AddValidation(ClientModelValidationContext ctx)
        {
            if (!ctx.Attributes.ContainsKey("data-val"))
                ctx.Attributes.Add("data-val", "true");
            ctx.Attributes.Add("data-val-pastyear-years",
                maxYears.ToString());
            ctx.Attributes.Add("data-val-pastyear", 
                GetMsg(ctx.ModelMetadata.DisplayName ?? ctx.ModelMetadata.Name ?? "Date"));
        }

        private string GetMsg(string name) => 
            base.ErrorMessage ?? $"{name} must be at least {maxYears} years ago."; 
    }
}
