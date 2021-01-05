using FluentValidation;
using System;
using System.Collections.Generic;
using System.Text;

namespace Application.CQRS.Genres.Commands.Update
{
   public class UpdateGenreCommandValidator: AbstractValidator<UpdateGenreCommand>
    {
        public UpdateGenreCommandValidator()
        {
            RuleFor(x => x.Id).NotEmpty();
            RuleFor(x => x.Name).NotEmpty();
            RuleFor(x => x.Name).MinimumLength(3);
            RuleFor(x => x.Description).NotEmpty();
        }
    }
}
