namespace Application.CQRS.Albums.Commands.Create
{
    using FluentValidation;
    using System.Text.RegularExpressions;

    public class CreateAlbumValidator : AbstractValidator<CreateAlbum>
    {
        public CreateAlbumValidator()
        {
            RuleFor(x => x.Name).NotEmpty();
            RuleFor(x => x.Name).MinimumLength(3);
            RuleFor(x => x.ArtistId).NotEmpty();
            RuleFor(x => x.GenreId).NotEmpty();
            RuleFor(x => x.Rating).Must(ValidRating).WithMessage("Rating must be a number between 1-5");
            RuleFor(x => x.Year).Must(ValidYear).WithMessage("Year must be a four digit number (1700-2199");
        }


        private bool ValidRating(string value)
        {
            if (string.IsNullOrWhiteSpace(value))
            {
                return true;
            }

            int num;
            if (!int.TryParse(value.ToString(), out num))
            {
                return false;
            }
            if (num <= 0 || num >= 5)
            {
                return false;
            }
            return true;
        }

        private bool ValidYear(string value)
        {
            if (string.IsNullOrWhiteSpace(value))
            {
                return true;
            }
            int num;
            if (!int.TryParse(value.ToString(), out num))
            {
                return false;
            }
            if (!Regex.IsMatch(value, "^(17|18|19|20|21)[0-9][0-9]"))
            {
                return false;
            }
            return true;
        }


    }
}
