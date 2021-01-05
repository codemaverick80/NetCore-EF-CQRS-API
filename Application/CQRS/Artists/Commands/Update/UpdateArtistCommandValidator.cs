using FluentValidation;
using System;
using System.Collections.Generic;
using System.Text;

namespace Application.CQRS.Artists.Commands.Update
{
   public class UpdateArtistCommandValidator:AbstractValidator<UpdateArtistCommand>
    {
        public UpdateArtistCommandValidator()
        {
            RuleFor(x => x.Name).NotEmpty();
            RuleFor(x => x.Name).MinimumLength(3);
            RuleFor(x => x.YearActive).NotEmpty();
            RuleFor(x => x.Biography).NotEmpty();
            RuleFor(x => x.Born).NotEmpty();
        }
    }
}
