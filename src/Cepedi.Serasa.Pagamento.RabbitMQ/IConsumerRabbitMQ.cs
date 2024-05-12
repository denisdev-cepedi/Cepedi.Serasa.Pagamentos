namespace Cepedi.Serasa.Pagamento.RabbitMQ;

public interface IConsumerRabbitMQ<T>
{
    Task IniciaLeituraMensagens(CancellationToken cancellationToken);
    void Finaliza();
}
