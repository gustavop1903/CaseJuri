using Amazon.DynamoDBv2;
using Amazon.DynamoDBv2.DataModel;

namespace CaseJuri.Infrastructure.Dynamo;

public class DynamoContext
{
    public DynamoDBContext Context { get; }

    public DynamoContext(IAmazonDynamoDB client)
    {
        Context = new DynamoDBContextBuilder()
            .WithDynamoDBClient(() => client)
            .Build();
    }
}
