﻿using FluentValidation;
using System;
using System.Collections.Generic;
using System.Text;

namespace Application.CQRS.Genres.Commands.Update
{
   public class UpdateGenreCommandValidator: AbstractValidator<UpdateGenreCommand>
    {
        public UpdateGenreCommandValidator()
        {
            RuleFor(x => x.Id).NotEmpty().WithMessage("Genre id must not be empty while updating");
            RuleFor(x => x.Name).NotEmpty().WithMessage("Genre name must not be empty while updating");
            RuleFor(x => x.Name).MinimumLength(3).WithMessage("Genre name should be at least 3 characters");
            RuleFor(x => x.Description).NotEmpty().WithMessage("Genre description must not be empty while updating");
        }
    }
}
