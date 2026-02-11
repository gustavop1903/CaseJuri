using Amazon.DynamoDBv2.DataModel;

namespace CaseJuri.Infrastructure.Dynamo.Schema;

[DynamoDBTable("ToDoTasks")]
public class ToDoTaskItem
{
    [DynamoDBHashKey]
    public string Id { get; set; } = default!;

    public string Titulo { get; set; } = default!;
    public string Descricao { get; set; } = default!;
    public string Status { get; set; } = default!;
    public string CriadoPor { get; set; } = default!;
    public DateTime DataCriacao { get; set; }
    public DateTime? DataConclusao { get; set; }
}
