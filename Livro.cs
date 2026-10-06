public class Livro
{
    private string _titulo;
    private string _autor;

    public string Titulo
    {
        get
        {
            return _titulo;
        }

        set
        {
            if (string.IsNullOrWhiteSpace(value))
            {
                throw new ArgumentException(
                    "O título do livro não pode ficar vazio."
                );
            }

            _titulo = value.Trim();
        }
    }

    public string Autor
    {
        get
        {
            return _autor;
        }

        set
        {
            if (string.IsNullOrWhiteSpace(value))
            {
                throw new ArgumentException(
                    "O autor não pode ficar vazio."
                );
            }

            _autor = value.Trim();
        }
    }

    public Livro(string titulo, string autor)
    {
        Titulo = titulo;
        Autor = autor;
    }

    public void ExibirDados()
    {
        Console.WriteLine($"Livro: {Titulo}");
        Console.WriteLine($"Autor: {Autor}");
    }
}