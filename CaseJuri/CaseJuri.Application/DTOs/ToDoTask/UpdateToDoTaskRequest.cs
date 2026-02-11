using CaseJuri.Domain.Entities;

namespace CaseJuri.Application.DTOs;

public record UpdateToDoTaskRequest
{
    public string Titulo { get; set; } = default!;
    public string Descricao { get; set; } = default!;
}
