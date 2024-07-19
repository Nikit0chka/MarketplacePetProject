# MarketplacePetProject

Ссылка на схему в draw.io

https://drive.google.com/file/d/15QByGuUrsMn7pveBWmnDItB48j6H4E4z/view?usp=sharing

Пример аналогичного проекта 

https://github.com/aforesaid/RtuItLab

Проект **Marketplace** основной проект с стандартным контейнером.

Команды запуска rabbitMq в докере

docker run -d --hostname my-rabbit-host --name my-rabbit -e RABBITMQ_DEFAULT_USER=user -e RABBITMQ_DEFAULT_PASS=password rabbitmq:3-management

docker run -it --rm --name rabbitmq -p 5672:5672 -p 15672:15672 rabbitmq:3-management
