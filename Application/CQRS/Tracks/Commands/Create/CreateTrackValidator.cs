namespace Application.CQRS.Tracks.Commands.Create
{
    using FluentValidation;
    public class CreateTrackValidator : AbstractValidator<CreateTrack>
    {
        public CreateTrackValidator()
        {
            RuleFor(x => x.Name).NotEmpty();
            RuleFor(x => x.Name).MinimumLength(3);
            RuleFor(x => x.AlbumId).NotEmpty();     
        }        
    }
}
