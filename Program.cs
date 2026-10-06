Aluno aluno1 = new Aluno(
    "Arthur Morgan",
    "AL000001"
);

Livro livro1 = new Livro(
    "Código Limpo",
    "Robert C. Martin"
);

Emprestimo emprestimo1 = new Emprestimo(aluno1, livro1, new DateOnly(2026, 10, 1), new DateOnly(2026, 10, 8));

emprestimo1.RegistrarDevolucao(new DateOnly(2026, 10, 12));
emprestimo1.ExibirResumo();

Emprestimo emprestimo2 = new Emprestimo(
    aluno1,
    livro1,
    new DateOnly(2029, 10, 1),
    new DateOnly(2026, 10, 8)
);

emprestimo2.RegistrarDevolucao(new DateOnly(2026, 10, 12));
emprestimo2.ExibirResumo();

Emprestimo emprestimo11 = new Emprestimo(
    aluno1,
    livro1,
    new DateOnly(2026, 10, 1),
    new DateOnly(2026, 10, 8)
);

emprestimo11.EstaAtrasado();
emprestimo11.ExibirResumo();