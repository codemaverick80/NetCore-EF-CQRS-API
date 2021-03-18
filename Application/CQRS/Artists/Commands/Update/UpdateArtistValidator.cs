namespace Application.CQRS.Artists.Commands.Update
{
    using FluentValidation;
    using System;
    using System.Collections.Generic;
    using System.Text;
    public class UpdateArtistValidator : AbstractValidator<UpdateArtist>
    {
        public UpdateArtistValidator()
        {
            RuleFor(x => x.Name).NotEmpty();
            RuleFor(x => x.Name).MinimumLength(3);
            RuleFor(x => x.YearActive).NotEmpty();
            RuleFor(x => x.Biography).NotEmpty();
            RuleFor(x => x.Born).NotEmpty();
        }
    }
}
