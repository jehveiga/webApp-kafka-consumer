namespace ConsoleApp.Kafka.Consumer.Console.Entities
{
    public class Pessoa
    {
        public Pessoa(Guid id, string nome, string cpf, DateTime dataNascimento)
        {
            Id = id;
            Nome = nome;
            Cpf = cpf;
            DataNascimento = dataNascimento;
        }

        public Guid Id { get; private set; }
        public string Nome { get; private set; }
        public string Cpf { get; private set; } 
        public DateTime DataNascimento { get; private set; }

        public override string ToString()
        {
            return $"ID: {Id} - Nome: {Nome} - CPF: {Cpf} - Data de Nascimento: {DataNascimento}";
        }
    }
}
