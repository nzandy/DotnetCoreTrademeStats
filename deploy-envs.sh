# set environment variables used in deploy.sh and AWS task-definition.json:
export IMAGE_NAME=tmswebapi
export IMAGE_VERSION=latest

export AWS_DEFAULT_REGION=us-east-2
export AWS_ECS_CLUSTER_NAME=default
export AWS_VIRTUAL_HOST=api.rentalstats.co.nz