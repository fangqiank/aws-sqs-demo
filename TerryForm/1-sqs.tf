provider "aws" {
	region = "us-west-2"
}

resource "aws_sqs_queue" "sqs_notification_queue" {
	name = var.queue_name
	message_retention_seconds = var.retention_period
	visitibility_timeout_seconds = var.visibility_timeout
	redrive_policy = jsonencode({
		"deadLetterTargetArn" = aws_sqs_queue.sqs_notification_dlq.arn
		"maxReceiveCount" = var.receive_count
	})
}

resource "aws_sqs_queue" "sqs_notification_dlq" {
	name = "${var.queue_name}-dlq"
	message_retention_seconds = var.retention_period
	visitibility_timeout_seconds = var.visibility_timeout
}

