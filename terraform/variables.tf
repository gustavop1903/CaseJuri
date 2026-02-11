variable "aws_region" {
  description = "AWS region"
  type        = string
  default     = "us-east-1"
}

variable "environment" {
  description = "Environment name"
  type        = string
  default     = "dev"
}

variable "project_name" {
  description = "Project name"
  type        = string
  default     = "CaseJuri"
}

variable "lambda_function_name" {
  description = "Lambda function name"
  type        = string
  default     = "casejuri-api"
}

variable "dynamodb_table_name" {
  description = "DynamoDB table name for tasks"
  type        = string
  default     = "ToDoTasks"
}

variable "lambda_runtime" {
  description = "Lambda runtime"
  type        = string
  default     = "dotnet8"
}

variable "lambda_memory_size" {
  description = "Lambda memory size in MB"
  type        = number
  default     = 512
}

variable "lambda_timeout" {
  description = "Lambda timeout in seconds"
  type        = number
  default     = 60
}

variable "aws_profile" {
  description = "Lambda function name"
  type        = string
  default     = "default"
}