namespace Application.CQRS.Albums.Commands.Update
{
    using FluentValidation;
    public class UpdateAlbumValidator : AbstractValidator<UpdateAlbum>
    {
        public UpdateAlbumValidator()
        {
            RuleFor(x => x.Name).NotEmpty();
            RuleFor(x => x.Name).MinimumLength(3);
            RuleFor(x => x.ArtistId).NotEmpty();
            RuleFor(x => x.GenreId).NotEmpty();
            RuleFor(x => x.Label).NotEmpty();
        }
    }
}
