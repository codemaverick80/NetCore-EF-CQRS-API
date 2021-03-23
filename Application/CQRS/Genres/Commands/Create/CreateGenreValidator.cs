namespace Application.CQRS.Genres.Commands.Create
{
using FluentValidation;
    public class CreateGenreValidator : AbstractValidator<CreateGenre>
    {
        public CreateGenreValidator()
        {
            RuleFor(x => x.Name).NotEmpty();
            RuleFor(x => x.Name).MinimumLength(3);
            RuleFor(x => x.Description).NotEmpty();

        }
    }
}
