using FluentValidation;
using TesteVericode.Domain.DTO;

namespace TesteVericode.Core.Validators
{
    public class UpdateTarefaDTOValidator : AbstractValidator<UpdateTarefaDTO>
    {
        public UpdateTarefaDTOValidator()
        {
            RuleFor(x => x.Descricao).NotEmpty().WithMessage("Descricao is required");
            RuleFor(x => x.Data).NotEmpty().WithMessage("Data is required");
            RuleFor(x => x.Status).NotEmpty().WithMessage("Status is required");
        }
    }
}
