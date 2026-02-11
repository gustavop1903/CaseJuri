namespace CaseJuri.Application.DTOs;

public record ToDoTaskResponseDto
{
    public Guid Id { get; set; }
    public string Titulo { get; set; } = default!;
    public string Descricao { get; set; } = default!;
    public string Status { get; set; } = default!;
    public string CriadoPor { get; set; } = default!;
    public DateTime DataCriacao { get; set; }
    public DateTime? DataConclusao { get; set; }
}