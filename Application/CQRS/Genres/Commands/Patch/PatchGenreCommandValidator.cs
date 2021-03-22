namespace Application.CQRS.Genres.Commands.Patch
{
    using FluentValidation;
    public class PatchGenreCommandValidator : AbstractValidator<PatchGenreCommand>
    {
        public PatchGenreCommandValidator()
        {
            RuleFor(x => x.Id).NotEmpty().WithMessage("Genre id must not be empty while updating");
            RuleFor(x => x.Name).NotEmpty().WithMessage("Genre name must not be empty while updating");
            RuleFor(x => x.Name).MinimumLength(3).WithMessage("Genre name should be at least 3 characters");
            RuleFor(x => x.Description).NotEmpty().WithMessage("Genre description must not be empty while updating");
        }
    }
}
