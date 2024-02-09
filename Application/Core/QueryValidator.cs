using System.Globalization;
using Domain.Core;
using Domain.Enums;
using FluentValidation;

namespace Application.Core
{
    public class QueryValidator : AbstractValidator<QueryFilter>
    {
        public QueryValidator()
        {
            EQueryFilters[] numericValueTypesOnly = {
                                EQueryFilters.GreaterThan,
                                EQueryFilters.LesserThan,
                                EQueryFilters.LesserThanOrEqual,
                                EQueryFilters.GreaterThanOrEqual
                        };
            RuleFor(x => x.QueryCriteria)
                    .NotEmpty();
            RuleFor(x => x.QueryType)
                    .NotEmpty()
                    .IsEnumName(typeof(EQueryFilters)).WithMessage("Invalid operand type.");
            RuleFor(x => x.Value)
                    .NotEmpty()
                    .Must(y =>
                            int.TryParse(y, out _) ||
                            DateTime.TryParseExact(y, new[]
                            {
                                "yyyy-MM-dd",
                                "yyyy-MM-dd'T'HH:mm:ssK"
                            },
                            CultureInfo.InvariantCulture, DateTimeStyles.None, out _) ||
                            double.TryParse(y, out _)
                    )
                    .When(x =>
                            Enum.TryParse<EQueryFilters>(x.QueryType, out EQueryFilters filter) &&
                            numericValueTypesOnly.Contains(filter)
                    )
                    .WithMessage("Incorrect data type for operand in query, use integers or if it is a date, start with YYYY-MM-DD.");
        }
    }
}