using FluentValidation;

namespace Application.CQRS.Albums.Commands.Create
{
    public class CreateAlbumValidator:AbstractValidator<CreateAlbum>
    {

        public CreateAlbumValidator()
        {
            RuleFor(x => x.Name).NotEmpty();
            RuleFor(x => x.Name).MinimumLength(3);
            RuleFor(x => x.ArtistId).NotEmpty();
            RuleFor(x => x.GenreId).NotEmpty();            
        }
    }
}
