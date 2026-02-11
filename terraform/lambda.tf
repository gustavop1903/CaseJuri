# Lambda Function
resource "aws_lambda_function" "api" {
  filename            = data.archive_file.lambda_zip.output_path
  function_name       = var.lambda_function_name
  role                = aws_iam_role.lambda_role.arn
  handler             = "CaseJuri.API"
  runtime             = var.lambda_runtime
  memory_size         = var.lambda_memory_size
  timeout             = var.lambda_timeout
  source_code_hash    = data.archive_file.lambda_zip.output_base64sha256

  environment {
    variables = {
      ASPNETCORE_ENVIRONMENT = var.environment
      Database__UseLocal     = "false"
      Database__DynamoRegion = var.aws_region
    }
  }

  depends_on = [aws_iam_role_policy_attachment.lambda_logs]
}

# Data source para o arquivo ZIP
data "archive_file" "lambda_zip" {
  type        = "zip"
  source_dir  = "${path.module}/../CaseJuri/CaseJuri.API/publish"
  output_path = "${path.module}/lambda_function.zip"
}

# CloudWatch Log Group
resource "aws_cloudwatch_log_group" "lambda_logs" {
  name              = "/aws/lambda/${aws_lambda_function.api.function_name}"
  retention_in_days = 5
}

# Lambda Permission para API Gateway
resource "aws_lambda_permission" "api_gateway" {
  statement_id  = "AllowAPIGatewayInvoke"
  action        = "lambda:InvokeFunction"
  function_name = aws_lambda_function.api.function_name
  principal     = "apigateway.amazonaws.com"
  source_arn    = "${aws_api_gateway_rest_api.api.execution_arn}/*/*"
}

