# FinancialFlow

## Descrição da Solução
Esta solução visa atender a necessidade de um comerciante em controlar seu fluxo de caixa diário, gerenciando lançamentos (débitos e créditos) e gerando relatórios de saldo diário consolidado.

## Estrutura do Projeto
A solução está estruturada em quatro camadas principais e o CORE (onde utilizar classes que são reutilizaveis):

## Domain: 
 Contém as entidades, agregados, valores de objetos, serviços de domínio e interfaces de repositórios.
## Application: 
Contém os serviços de aplicação, DTOs (Data Transfer Objects) e interfaces de serviços de aplicação.
## Infrastructure:
Contém as implementações de repositórios, contexto de banco de dados e serviços de infraestrutura.
## API: :
Contém os controladores e configurações da API.
Requisitos de Negócio
Serviço que faça o controle de lançamentos
Serviço do consolidado diário

## importante

 A aplicação possui um docker compose para subir banco de dados
 É preciso utilizar update-database para criar o banco de dados (utilização de migrations do EF)
 Tem a implementação do RabbitMQ para disparo para fila e já está no docker compose
