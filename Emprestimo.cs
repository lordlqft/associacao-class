/*
    Classe associativa responsável por representar
    o relacionamento entre um Aluno e um Livro.

    Além de relacionar os dois objetos, esta classe
    também possui informações e regras próprias
    do empréstimo.
*/
public class Emprestimo
{
    /*
        Atributos privados da classe.

        _aluno e _livro representam os objetos
        envolvidos no empréstimo.

        As datas pertencem ao próprio empréstimo.
    */
    private Aluno _aluno;
    private Livro _livro;
    private DateOnly _dataEmprestimo;
    private DateOnly _dataPrevistaDevolucao;

    /*
        DateOnly? permite armazenar null.

        Enquanto o livro não for devolvido,
        ainda não existe uma data de devolução.
    */
    private DateOnly? _dataDevolucao;


    /*
        Propriedade responsável pelo acesso ao aluno.

        Um empréstimo obrigatoriamente precisa
        estar relacionado a um aluno.
    */
    public Aluno Aluno
    {
        get
        {
            return _aluno;
        }

        set
        {
            if (value == null)
            {
                throw new ArgumentException(
                    "O empréstimo precisa possuir um aluno."
                );
            }

            _aluno = value;
        }
    }


    /*
        Propriedade responsável pelo acesso ao livro.

        Um empréstimo obrigatoriamente precisa
        estar relacionado a um livro.
    */
    public Livro Livro
    {
        get
        {
            return _livro;
        }

        set
        {
            if (value == null)
            {
                throw new ArgumentException(
                    "O empréstimo precisa possuir um livro."
                );
            }

            _livro = value;
        }
    }


    /*
        Data em que o empréstimo foi realizado.

        O private set permite a leitura fora da classe,
        mas impede que a data seja alterada diretamente
        por outras partes do sistema.
    */
    public DateOnly DataEmprestimo
    {
        get
        {
            return _dataEmprestimo;
        }

        private set
        {
            _dataEmprestimo = value;
        }
    }


    /*
        Data prevista para devolução.

        Existe uma regra de negócio:
        a devolução prevista não pode acontecer
        antes da data do empréstimo.
    */
    public DateOnly DataPrevistaDevolucao
    {
        get
        {
            return _dataPrevistaDevolucao;
        }

        private set
        {
            if (value < DataEmprestimo)
            {
                throw new ArgumentException(
                    "A data prevista não pode ser anterior ao empréstimo."
                );
            }

            _dataPrevistaDevolucao = value;
        }
    }


    /*
        Data real da devolução.

        O private set impede alterações diretas.

        Para registrar a devolução será necessário
        utilizar o método RegistrarDevolucao(),
        garantindo que as regras sejam verificadas.
    */
    public DateOnly? DataDevolucao
    {
        get
        {
            return _dataDevolucao;
        }

        private set
        {
            _dataDevolucao = value;
        }
    }


    /*
        Construtor da classe.

        Para criar um empréstimo é necessário
        informar um aluno, um livro, a data do empréstimo
        e a data prevista para devolução.

        As propriedades são utilizadas para aproveitar
        as validações existentes nos setters.
    */
    public Emprestimo(Aluno aluno, Livro livro, DateOnly dataEmprestimo,
        DateOnly dataPrevistaDevolucao)
    {
        Aluno = aluno;
        Livro = livro;
        DataEmprestimo = dataEmprestimo;
        DataPrevistaDevolucao = dataPrevistaDevolucao;

        /*
            No momento da criação do empréstimo,
            o livro ainda não foi devolvido.
        */
        DataDevolucao = null;
    }


    /*
        Registra a devolução do livro.

        Regras:
        - um empréstimo não pode ser devolvido duas vezes;
        - a devolução não pode acontecer antes
          da data do empréstimo.
    */
    public void RegistrarDevolucao(DateOnly dataDevolucao)
    {
        if (DataDevolucao != null)
        {
            throw new InvalidOperationException(
                "Este livro já foi devolvido."
            );
        }

        if (dataDevolucao < DataEmprestimo)
        {
            throw new ArgumentException(
                "A devolução não pode ocorrer antes do empréstimo."
            );
        }

        DataDevolucao = dataDevolucao;
    }


    /*
        Verifica se existe atraso.

        Se o livro já foi devolvido, compara a data
        da devolução com a data prevista.

        Se ainda não foi devolvido, compara a data atual
        com a data prevista.
    */
    public bool EstaAtrasado()
    {
        DateOnly dataComparacao;

        if (DataDevolucao != null)
        {
            /*
                Como DataDevolucao é DateOnly?,
                usamos .Value para obter o valor DateOnly.
            */
            dataComparacao = DataDevolucao.Value;
        }
        else
        {
            /*
                DateTime.Now retorna data e horário.

                DateOnly.FromDateTime() converte
                para somente a data.
            */
            dataComparacao =
                DateOnly.FromDateTime(DateTime.Now);
        }

        return dataComparacao > DataPrevistaDevolucao;
    }


    /*
        Calcula a quantidade de dias de atraso.

        Se não houver atraso, retorna 0.
    */
    public int CalcularDiasAtraso()
    {
        DateOnly dataComparacao;

        if (DataDevolucao != null)
        {
            dataComparacao = DataDevolucao.Value;
        }
        else
        {
            dataComparacao =
                DateOnly.FromDateTime(DateTime.Now);
        }

        /*
            Se a devolução aconteceu antes ou exatamente
            na data prevista, não existe atraso.
        */
        if (dataComparacao <= DataPrevistaDevolucao)
        {
            return 0;
        }

        /*
            DayNumber representa numericamente
            a posição da data no calendário.

            A diferença entre os dois valores
            fornece a quantidade de dias de atraso.
        */
        return dataComparacao.DayNumber
            - DataPrevistaDevolucao.DayNumber;
    }


    /*
        Calcula o valor da multa.

        Regra:
        cada dia de atraso gera uma multa de R$ 2,50.
    */
    public decimal CalcularMulta()
    {
        /*
            const indica que o valor não será alterado.

            O sufixo m indica que o número
            deve ser tratado como decimal.
        */
        const decimal VALOR_MULTA_DIA = 2.50m;

        return CalcularDiasAtraso() * VALOR_MULTA_DIA;
    }


    /*
        Exibe um resumo completo do empréstimo.

        Como Aluno e Livro são objetos,
        conseguimos acessar suas propriedades
        utilizando a notação de ponto.
    */
    public void ExibirResumo()
    {
        Console.WriteLine("===== EMPRÉSTIMO =====");

        Console.WriteLine($"Aluno: {Aluno.Nome}");
        Console.WriteLine($"Matrícula: {Aluno.Matricula}");

        Console.WriteLine($"Livro: {Livro.Titulo}");
        Console.WriteLine($"Autor: {Livro.Autor}");

        /*
            dd/MM/yyyy define o formato
            utilizado para exibir a data.
        */
        Console.WriteLine(
            $"Data do empréstimo: {DataEmprestimo:dd/MM/yyyy}"
        );

        Console.WriteLine(
            $"Devolução prevista: {DataPrevistaDevolucao:dd/MM/yyyy}"
        );

        /*
            Se DataDevolucao possuir valor,
            significa que o livro já foi devolvido.
        */
        if (DataDevolucao != null)
        {
            Console.WriteLine(
                $"Devolvido em: {DataDevolucao.Value:dd/MM/yyyy}"
            );
        }
        else
        {
            Console.WriteLine(
                "Situação: livro ainda não devolvido."
            );
        }

        /*
            Operador ternário:

            condição ? valorSeVerdadeiro : valorSeFalso
        */
        Console.WriteLine(
            $"Está atrasado: {(EstaAtrasado() ? "Sim" : "Não")}"
        );

        Console.WriteLine(
            $"Dias de atraso: {CalcularDiasAtraso()}"
        );

        /*
            F2 exibe o valor com duas casas decimais.
        */
        Console.WriteLine(
            $"Multa: R$ {CalcularMulta():F2}"
        );
    }
}