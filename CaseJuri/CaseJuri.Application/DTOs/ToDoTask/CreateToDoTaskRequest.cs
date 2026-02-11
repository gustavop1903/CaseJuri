using System.ComponentModel.DataAnnotations;


namespace CaseJuri.Application.DTOs;

public record CreateToDoTaskRequest
{
    [Required]
    public string Titulo { get; set; } = default!;
    
    [Required]
    public string Descricao { get; set; } = default!;

    [Required]
    public string CriadoPor { get; set; } = default!;
}
