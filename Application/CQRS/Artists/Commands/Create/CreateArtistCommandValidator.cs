using FluentValidation;
using System;
using System.Collections.Generic;
using System.Text;

namespace Application.CQRS.Artists.Commands.Create
{
   public class CreateArtistCommandValidator: AbstractValidator<CreateArtistCommand>
    {
        public CreateArtistCommandValidator()
        {
            RuleFor(x => x.Name).NotEmpty();
            RuleFor(x => x.Name).MinimumLength(3);
            RuleFor(x => x.YearActive).NotEmpty();
            RuleFor(x => x.Biography).NotEmpty();
            RuleFor(x => x.Born).NotEmpty();

        }
    }
}
