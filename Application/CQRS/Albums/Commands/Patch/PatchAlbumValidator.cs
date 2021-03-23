namespace Application.CQRS.Albums.Commands.Patch
{
    using FluentValidation;
    public class PatchAlbumValidator : AbstractValidator<PatchAlbum>
    {
        public PatchAlbumValidator()
        {
            RuleFor(x => x.Name).NotEmpty();
            RuleFor(x => x.Name).MinimumLength(3);
            RuleFor(x => x.ArtistId).NotEmpty();
            RuleFor(x => x.GenreId).NotEmpty();
            RuleFor(x => x.Label).NotEmpty();
        }
    }
}
