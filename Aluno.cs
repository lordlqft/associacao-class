public class Aluno
{
    private string _nome;
    private string _matricula;

    public string Nome
    {
        get
        {
            return _nome;
        }

        set
        {
            if (string.IsNullOrWhiteSpace(value))
            {
                throw new ArgumentException(
                    "O nome do aluno não pode ficar vazio."
                );
            }

            _nome = value.Trim();
        }
    }

    public string Matricula
    {
        get
        {
            return _matricula;
        }

        set
        {
            if (string.IsNullOrWhiteSpace(value))
            {
                throw new ArgumentException(
                    "A matrícula não pode ficar vazia."
                );
            }

            _matricula = value.Trim();
        }
    }

    public Aluno(string nome, string matricula)
    {
        Nome = nome;
        Matricula = matricula;
    }

    public void ExibirDados()
    {
        Console.WriteLine($"Aluno: {Nome}");
        Console.WriteLine($"Matrícula: {Matricula}");
    }
}