output "api_base_url" {
  description = "Base URL da API"
  value = aws_api_gateway_stage.stage.invoke_url
}

output "api_tasks_endpoint" {
  description = "Endpoint para tarefas"
  value       = "${aws_api_gateway_stage.stage.invoke_url}/api/tasks"
}

output "lambda_function_name" {
  description = "Nome da função Lambda"
  value       = aws_lambda_function.api.function_name
}

output "dynamodb_table_name" {
  description = "Nome da tabela DynamoDB"
  value       = aws_dynamodb_table.todo_tasks.name
}

output "dynamodb_table_arn" {
  description = "ARN da tabela DynamoDB"
  value       = aws_dynamodb_table.todo_tasks.arn
}

output "lambda_role_arn" {
  description = "ARN da role IAM da Lambda"
  value       = aws_iam_role.lambda_role.arn
}

output "cloudwatch_log_group" {
  description = "CloudWatch Log Group para Lambda"
  value       = aws_cloudwatch_log_group.lambda_logs.name
}
