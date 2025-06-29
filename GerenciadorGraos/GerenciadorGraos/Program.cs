using System;
using System.Linq;
using GerenciadorGraos.Models;
using GerenciadorGraos.Implementacoes;

class Program
{
    static void Main()
    {
        var graoRepo = new GraoJsonRepository();
        var siloRepo = new SiloSqlRepositorio(); // Agora usando LINQ to Entities (EF Core)
        var fornecedorRepo = new FornecedorJsonRepository();

        while (true)
        {
            Console.WriteLine("1 - Gerenciar Grãos");
            Console.WriteLine("2 - Gerenciar Silos");
            Console.WriteLine("3 - Gerenciar Fornecedores");
            Console.WriteLine("0 - Sair");
            var op = Console.ReadLine();

            if (op == "1") MenuGrao(graoRepo, fornecedorRepo);
            else if (op == "2") MenuSilo(siloRepo, graoRepo);
            else if (op == "3") MenuFornecedor(fornecedorRepo);
            else if (op == "0") break;
        }
    }

    static void MenuGrao(GraoJsonRepository graoRepo, FornecedorJsonRepository fornecedorRepo)
    {
        Console.WriteLine("1 - Adicionar novo grão");
        Console.WriteLine("2 - Listar todos os grãos");
        Console.WriteLine("3 - Atualizar informações de um grão");
        Console.WriteLine("4 - Remover um grão");
        Console.WriteLine("5 - Buscar grãos pelo nome");
        var op = Console.ReadLine();

        if (op == "1")
        {
            var grao = new Grao();
            Console.Write("Nome: ");
            grao.Nome = Console.ReadLine() ?? string.Empty;
            Console.Write("Número do lote: ");
            grao.NumeroLote = Console.ReadLine() ?? string.Empty;

            Console.Write("Peso (Kg): ");
            var pesoInput = Console.ReadLine();
            if (string.IsNullOrWhiteSpace(pesoInput) || !double.TryParse(pesoInput, out var peso))
            {
                Console.WriteLine("Peso inválido.");
                return;
            }
            grao.Peso = peso;

            Console.Write("Quantidade: ");
            var qtdInput = Console.ReadLine();
            if (string.IsNullOrWhiteSpace(qtdInput) || !int.TryParse(qtdInput, out var qtd))
            {
                Console.WriteLine("Quantidade inválida.");
                return;
            }
            grao.Quantidade = qtd;

            Console.Write("Data de entrega (yyyy-MM-dd): ");
            var dataInput = Console.ReadLine();
            if (string.IsNullOrWhiteSpace(dataInput) || !DateTime.TryParse(dataInput, out var dataEntrega))
            {
                Console.WriteLine("Data inválida.");
                return;
            }
            grao.DataEntrega = dataEntrega;

            Console.Write("Valor unitário (R$): ");
            var valorInput = Console.ReadLine();
            if (string.IsNullOrWhiteSpace(valorInput) || !decimal.TryParse(valorInput, out var valorUnitario))
            {
                Console.WriteLine("Valor inválido.");
                return;
            }
            grao.ValorUnitario = valorUnitario;

            Console.Write("ID do Fornecedor: ");
            var fornecedorInput = Console.ReadLine();
            if (string.IsNullOrWhiteSpace(fornecedorInput) || !Guid.TryParse(fornecedorInput, out var fornecedorId))
            {
                Console.WriteLine("ID inválido.");
                return;
            }
            grao.FornecedorId = fornecedorId;

            grao.AtualizarValorTotal();
            graoRepo.Adicionar(grao);
        }
        else if (op == "2")
        {
            foreach (var g in graoRepo.ObterTodos())
                Console.WriteLine($"{g.Id} - {g.Nome} - Lote: {g.NumeroLote} - Peso: {g.Peso}Kg - Quantidade: {g.Quantidade} - Valor Unitário: R$ {g.ValorUnitario:F2} - Total: R$ {g.ValorTotalLote:F2}");
        }
        else if (op == "3")
        {
            Console.Write("ID do Grão: ");
            var idInput = Console.ReadLine();
            if (string.IsNullOrWhiteSpace(idInput) || !Guid.TryParse(idInput, out var id))
            {
                Console.WriteLine("ID inválido.");
                return;
            }
            var grao = graoRepo.ObterPorId(id);
            if (grao == null) { Console.WriteLine("Não encontrado."); return; }

            Console.Write($"Novo nome [{grao.Nome}]: ");
            var novoNome = Console.ReadLine();
            if (!string.IsNullOrWhiteSpace(novoNome))
                grao.Nome = novoNome;

            Console.Write($"Novo número do lote [{grao.NumeroLote}]: ");
            var novoLote = Console.ReadLine();
            if (!string.IsNullOrWhiteSpace(novoLote))
                grao.NumeroLote = novoLote;

            Console.Write($"Novo peso (Kg) [{grao.Peso}]: ");
            var novoPeso = Console.ReadLine();
            if (!string.IsNullOrWhiteSpace(novoPeso) && double.TryParse(novoPeso, out var peso))
                grao.Peso = peso;

            Console.Write($"Nova quantidade [{grao.Quantidade}]: ");
            var novaQtd = Console.ReadLine();
            if (!string.IsNullOrWhiteSpace(novaQtd) && int.TryParse(novaQtd, out var qtd))
                grao.Quantidade = qtd;

            Console.Write($"Nova data de entrega (yyyy-MM-dd) [{grao.DataEntrega:yyyy-MM-dd}]: ");
            var novaData = Console.ReadLine();
            if (!string.IsNullOrWhiteSpace(novaData) && DateTime.TryParse(novaData, out var dataEntrega))
                grao.DataEntrega = dataEntrega;

            Console.Write($"Novo valor unitário (R$) [{grao.ValorUnitario}]: ");
            var novoValor = Console.ReadLine();
            if (!string.IsNullOrWhiteSpace(novoValor) && decimal.TryParse(novoValor, out var valorUnitario))
                grao.ValorUnitario = valorUnitario;

            Console.Write($"Novo ID do Fornecedor [{grao.FornecedorId}]: ");
            var novoFornecedor = Console.ReadLine();
            if (!string.IsNullOrWhiteSpace(novoFornecedor) && Guid.TryParse(novoFornecedor, out var fornecedorId))
                grao.FornecedorId = fornecedorId;

            grao.AtualizarValorTotal();
            graoRepo.Atualizar(grao);
            Console.WriteLine("Grão atualizado com sucesso!");
        }
        else if (op == "4")
        {
            Console.Write("ID do Grão: ");
            var idInput = Console.ReadLine();
            if (string.IsNullOrWhiteSpace(idInput) || !Guid.TryParse(idInput, out var id))
            {
                Console.WriteLine("ID inválido.");
                return;
            }
            graoRepo.Remover(id);
        }
        else if (op == "5")
        {
            Console.Write("Digite o nome do grão para buscar: ");
            var nomeBusca = Console.ReadLine();
            var resultados = graoRepo.ObterTodos().Where(g => g.Nome.Contains(nomeBusca ?? "", StringComparison.OrdinalIgnoreCase));
            foreach (var g in resultados)
                Console.WriteLine($"{g.Id} - {g.Nome} - Lote: {g.NumeroLote} - Peso: {g.Peso}Kg - Quantidade: {g.Quantidade} - Valor Unitário: R$ {g.ValorUnitario:F2} - Total: R$ {g.ValorTotalLote:F2}");
        }
    }

    static void MenuSilo(SiloSqlRepositorio siloRepo, GraoJsonRepository graoRepo)
    {
        Console.WriteLine("1 - Adicionar novo silo");
        Console.WriteLine("2 - Listar todos os silos (apenas os cadastrados no banco)");
        Console.WriteLine("3 - Inserir lote de grão em um silo");
        Console.WriteLine("4 - Remover um silo");
        Console.WriteLine("5 - Remover quantidade de grão de um silo");
        Console.WriteLine("6 - Buscar silos pelo nome (apenas os cadastrados no banco)");
        var op = Console.ReadLine();

        if (op == "1")
        {
            var silo = new Silo();
            Console.Write("Nome: ");
            silo.Nome = Console.ReadLine() ?? string.Empty;

            Console.Write("Capacidade máxima: ");
            var capMaxInput = Console.ReadLine();
            if (string.IsNullOrWhiteSpace(capMaxInput) || !double.TryParse(capMaxInput, out var capMax))
            {
                Console.WriteLine("Capacidade máxima inválida.");
                return;
            }
            silo.CapacidadeMaxima = capMax;

            Console.Write("Temperatura: ");
            var tempInput = Console.ReadLine();
            if (string.IsNullOrWhiteSpace(tempInput) || !double.TryParse(tempInput, out var temp))
            {
                Console.WriteLine("Temperatura inválida.");
                return;
            }
            silo.Temperatura = temp;

            Console.Write("Tipo do grão: ");
            silo.TipoGrao = Console.ReadLine() ?? string.Empty;

            silo.CapacidadeAtual = 0;
            silo.Id = Guid.NewGuid();
            siloRepo.Adicionar(silo);
            Console.WriteLine("Silo cadastrado com sucesso!");
        }
        else if (op == "2")
        {
            foreach (var s in siloRepo.ObterTodos())
                Console.WriteLine($"{s.Id} - {s.Nome} - Capacidade Máxima: {s.CapacidadeMaxima} - Atual: {s.CapacidadeAtual} - Temperatura: {s.Temperatura} - Tipo de Grão: {s.TipoGrao}");
        }
        else if (op == "3")
        {
            Console.Write("ID do Silo: ");
            var idInput = Console.ReadLine();
            if (string.IsNullOrWhiteSpace(idInput) || !Guid.TryParse(idInput, out var id))
            {
                Console.WriteLine("ID inválido.");
                return;
            }
            var silo = siloRepo.ObterPorId(id);
            if (silo == null) { Console.WriteLine("Silo não encontrado."); return; }

            Console.Write("Número do lote do grão a inserir: ");
            var lote = Console.ReadLine();
            if (string.IsNullOrWhiteSpace(lote))
            {
                Console.WriteLine("Lote inválido.");
                return;
            }
            var grao = graoRepo.ObterTodos().FirstOrDefault(g => g.NumeroLote == lote);
            if (grao == null)
            {
                Console.WriteLine("Grão não encontrado.");
                return;
            }

            if (!string.IsNullOrEmpty(silo.TipoGrao) && !silo.TipoGrao.Equals(grao.Nome, StringComparison.OrdinalIgnoreCase))
            {
                Console.WriteLine($"Este silo só pode armazenar grãos do tipo '{silo.TipoGrao}'.");
                return;
            }

            var novaCapacidade = (silo.CapacidadeAtual ?? 0) + grao.Peso;
            if (novaCapacidade > silo.CapacidadeMaxima)
            {
                Console.WriteLine("A carga excede a capacidade máxima do silo.");
                return;
            }

            silo.CapacidadeAtual = novaCapacidade;
            silo.TipoGrao = grao.Nome;
            siloRepo.Atualizar(silo);
            Console.WriteLine("Lote inserido no silo com sucesso!");
        }
        else if (op == "4")
        {
            Console.Write("ID do Silo: ");
            var idInput = Console.ReadLine();
            if (string.IsNullOrWhiteSpace(idInput) || !Guid.TryParse(idInput, out var id))
            {
                Console.WriteLine("ID inválido.");
                return;
            }
            siloRepo.Remover(id);
            Console.WriteLine("Silo removido com sucesso!");
        }
        else if (op == "5")
        {
            Console.Write("ID do Silo: ");
            var idInput = Console.ReadLine();
            if (string.IsNullOrWhiteSpace(idInput) || !Guid.TryParse(idInput, out var id))
            {
                Console.WriteLine("ID inválido.");
                return;
            }
            var silo = siloRepo.ObterPorId(id);
            if (silo == null) { Console.WriteLine("Silo não encontrado."); return; }

            Console.Write("Número do lote do grão a remover: ");
            var lote = Console.ReadLine();
            if (string.IsNullOrWhiteSpace(lote))
            {
                Console.WriteLine("Lote inválido.");
                return;
            }
            var grao = graoRepo.ObterTodos().FirstOrDefault(g => g.NumeroLote == lote && g.Nome.Equals(silo.TipoGrao, StringComparison.OrdinalIgnoreCase));
            if (grao == null)
            {
                Console.WriteLine("Grão não encontrado ou não corresponde ao tipo do silo.");
                return;
            }

            Console.Write($"Quantidade a remover do silo (em Kg, máximo {silo.CapacidadeAtual ?? 0}): ");
            var qtdInput = Console.ReadLine();
            if (string.IsNullOrWhiteSpace(qtdInput) || !double.TryParse(qtdInput, out var qtdRemover) || qtdRemover <= 0)
            {
                Console.WriteLine("Quantidade inválida.");
                return;
            }

            if ((silo.CapacidadeAtual ?? 0) < qtdRemover)
            {
                Console.WriteLine("Não há essa quantidade no silo.");
                return;
            }
            if (grao.Peso < qtdRemover)
            {
                Console.WriteLine("O lote não possui essa quantidade.");
                return;
            }

            silo.CapacidadeAtual -= qtdRemover;
            grao.Peso -= qtdRemover;
            grao.AtualizarValorTotal();
            siloRepo.Atualizar(silo);
            graoRepo.Atualizar(grao);

            Console.WriteLine("Quantidade removida do silo e do lote com sucesso!");
        }
        else if (op == "6")
        {
            Console.Write("Digite o nome do silo para buscar: ");
            var nomeBusca = Console.ReadLine();
            var silos = siloRepo.ObterTodos().Where(s => s.Nome.Contains(nomeBusca ?? "", StringComparison.OrdinalIgnoreCase));
            foreach (var s in silos)
                Console.WriteLine($"{s.Id} - {s.Nome} - Capacidade Máxima: {s.CapacidadeMaxima} - Atual: {s.CapacidadeAtual} - Temperatura: {s.Temperatura} - Tipo de Grão: {s.TipoGrao}");
        }
    }

    static void MenuFornecedor(FornecedorJsonRepository fornecedorRepo)
    {
        Console.WriteLine("1 - Adicionar novo fornecedor");
        Console.WriteLine("2 - Listar todos os fornecedores");
        Console.WriteLine("3 - Atualizar informações de um fornecedor");
        Console.WriteLine("4 - Remover um fornecedor");
        Console.WriteLine("5 - Buscar fornecedor pelo nome");
        var op = Console.ReadLine();

        if (op == "1")
        {
            var fornecedor = new Fornecedor();
            Console.Write("Nome: ");
            fornecedor.Nome = Console.ReadLine() ?? string.Empty;
            Console.Write("CNPJ/CAF: ");
            fornecedor.CnpjOuCaf = Console.ReadLine() ?? string.Empty;
            Console.Write("Telefone: ");
            fornecedor.Telefone = Console.ReadLine() ?? string.Empty;
            fornecedorRepo.Adicionar(fornecedor);
        }
        else if (op == "2")
        {
            foreach (var f in fornecedorRepo.ObterTodos())
                Console.WriteLine($"{f.Id} - {f.Nome} - {f.CnpjOuCaf}");
        }
        else if (op == "3")
        {
            Console.Write("ID do Fornecedor: ");
            var idInput = Console.ReadLine();
            if (string.IsNullOrWhiteSpace(idInput) || !Guid.TryParse(idInput, out var id))
            {
                Console.WriteLine("ID inválido.");
                return;
            }
            var fornecedor = fornecedorRepo.ObterPorId(id);
            if (fornecedor == null) { Console.WriteLine("Não encontrado."); return; }

            Console.Write($"Novo nome [{fornecedor.Nome}]: ");
            var novoNome = Console.ReadLine();
            if (!string.IsNullOrWhiteSpace(novoNome))
                fornecedor.Nome = novoNome;

            Console.Write($"Novo CNPJ/CAF [{fornecedor.CnpjOuCaf}]: ");
            var novoCnpj = Console.ReadLine();
            if (!string.IsNullOrWhiteSpace(novoCnpj))
                fornecedor.CnpjOuCaf = novoCnpj;

            Console.Write($"Novo telefone [{fornecedor.Telefone}]: ");
            var novoTel = Console.ReadLine();
            if (!string.IsNullOrWhiteSpace(novoTel))
                fornecedor.Telefone = novoTel;

            fornecedorRepo.Atualizar(fornecedor);
            Console.WriteLine("Fornecedor atualizado com sucesso!");
        }
        else if (op == "4")
        {
            Console.Write("ID do Fornecedor: ");
            var idInput = Console.ReadLine();
            if (string.IsNullOrWhiteSpace(idInput) || !Guid.TryParse(idInput, out var id))
            {
                Console.WriteLine("ID inválido.");
                return;
            }
            fornecedorRepo.Remover(id);
        }
        else if (op == "5")
        {
            Console.Write("Digite o nome do fornecedor para buscar: ");
            var nomeBusca = Console.ReadLine();
            var resultados = fornecedorRepo.ObterTodos().Where(f => f.Nome.Contains(nomeBusca ?? "", StringComparison.OrdinalIgnoreCase));
            foreach (var f in resultados)
                Console.WriteLine($"{f.Id} - {f.Nome} - {f.CnpjOuCaf}");
        }
    }
}