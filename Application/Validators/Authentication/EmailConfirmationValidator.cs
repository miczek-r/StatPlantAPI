using Application.DTOs.Authentication;
using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Validators.Authentication
{
    public class EmailConfirmationValidator : AbstractValidator<EmailConfirmationDTO>
    {
        public EmailConfirmationValidator()
        {
            RuleFor(x => x.UserId).NotEmpty();
            RuleFor(x => x.ConfirmationToken).NotEmpty();
        }
    }
}
