using System.ComponentModel.DataAnnotations;
using CaseJuri.Domain.Enums;
namespace CaseJuri.Domain.Entities
{
    public class ToDoTask
    {
        public Guid Id { get; private set; }
        public string Titulo { get; private set; }
        public string Descricao { get; private set; }
        public StatusTask Status { get; private set; }
        public string CriadoPor { get; private set; }
        public DateTime DataCriacao { get; private set; }
        public DateTime? DataConclusao { get; private set; }
    
        public ToDoTask() { }

        public ToDoTask(string titulo, string descricao, string criadoPor)
        {
            Id = Guid.NewGuid();
            Titulo = titulo;
            Descricao = descricao;
            Status = StatusTask.Pendente;
            CriadoPor = criadoPor;
            DataCriacao = DateTime.UtcNow;
        }

        public void Update(string titulo, string descricao)
        {
            if (Status == StatusTask.Concluida)
                throw new ValidationException("Tarefas concluídas não podem ser editadas.");

            if (string.IsNullOrWhiteSpace(titulo))
                throw new ArgumentException("Título é obrigatório");

            Titulo = titulo;
            Descricao = descricao;
        }

        public void Start()
        {
            if (Status != StatusTask.Pendente)
                throw new InvalidOperationException("A tarefa só pode ser iniciada se estiver pendente.");

            Status = StatusTask.EmAndamento;
        }

        public void Finish()
        {
            if (Status == StatusTask.Concluida)
                throw new InvalidOperationException("A tarefa já está concluída.");

            Status = StatusTask.Concluida;
            DataConclusao = DateTime.UtcNow;
        }
    }
}