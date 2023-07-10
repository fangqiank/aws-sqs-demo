variable "queue_name" {
	type = string
	description = "The name of the queue needs to be created"
}

variable "retention_period" {
	type = number 
	description = "How long do we want to keep the message in the queue"
	default = 8000
}

variable "visibility_timeout" {
	type = number 
	description = "How long does the consumer have before the message is removed"
	default = 60
}

variable "receive_count" {
	type = number 
	description = "the number of the time taht message can be received befor going tothe DLQ"
	default = 3
}
