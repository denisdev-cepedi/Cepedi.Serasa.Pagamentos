namespace Cepedi.Serasa.Pagamento.Dominio.Services.RabbitMQ;
public interface IProducerRabbitMQ
{
    void SendMessage(string message);
}
