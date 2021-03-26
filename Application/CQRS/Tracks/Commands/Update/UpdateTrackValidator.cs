namespace Application.CQRS.Tracks.Commands.Update
{
    using FluentValidation;
    public class UpdateTrackValidator : AbstractValidator<UpdateTrack>
    {
        public UpdateTrackValidator()
        {
            RuleFor(x => x.Id).NotEmpty();
            RuleFor(x => x.AlbumId).NotEmpty();
            RuleFor(x => x.Name).NotEmpty();
            RuleFor(x => x.Name).MinimumLength(3);
        }
    }
}
