using System;
using System.ComponentModel.DataAnnotations;
using Testro.TestingManagement.WebApi.DataAccess;

namespace Testro.TestingManagement.WebApi.Attributes
{
    public class EntityIdAttribute : ValidationAttribute
    {
        private readonly Type _entityType;

        public EntityIdAttribute(Type entityType)
        {
            _entityType = entityType;
        }
        
        protected override ValidationResult IsValid(object value,
            ValidationContext validationContext)
        {
            var db = (DatabaseContext) validationContext.GetService(typeof(DatabaseContext));
            var entity = db.Find(_entityType, value);

            if (entity is null)
            {
                return new ValidationResult("Not found.");
            }

            return ValidationResult.Success;
        }
    }
}