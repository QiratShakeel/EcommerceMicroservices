docker compose up -d sqlserver rabbitmq

Start-Sleep -Seconds 20

docker compose up -d catalogservice

Start-Sleep -Seconds 10

docker compose up -d identityservice

Start-Sleep -Seconds 10

docker compose up -d orderservice

Start-Sleep -Seconds 10

docker compose up -d paymentservice

Start-Sleep -Seconds 5

docker compose up -d apigateway