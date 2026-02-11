# DynamoDB Table para ToDoTasks
resource "aws_dynamodb_table" "todo_tasks" {
  name           = var.dynamodb_table_name
  billing_mode   = "PAY_PER_REQUEST"
  hash_key       = "Id"

  attribute {
    name = "Id"
    type = "S"  # String (Guid em string)
  }

  tags = {
    Name = "${var.project_name}-dynamodb-table"
  }
}

