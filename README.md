# ProjetoFinal

### Desenvolvido por: 
- Gustavo Finkler Haas
- Pablo Emanuel Cechim de Lima
- Tawan Vitor Silva de Oliveira

# 🌾 Gerenciador de Grãos

Sistema em C# para gerenciar grãos, silos e fornecedores de forma simples via terminal. Utiliza **JSON** e **Entity Framework Core** com banco SQLite para persistência de dados.

---

## 🧭 Funcionalidades

### 📦 Grãos
- Adicionar, listar, atualizar e remover grãos
- Buscar grãos por nome (LINQ)
- Vínculo com fornecedor
- Cálculo automático do valor total do lote

### 🏭 Silos
- Adicionar e listar silos
- Inserir e remover carga de grãos (validação de tipo e capacidade)
- Buscar silos por tipo de grão (LINQ to Entities)

### 🧑‍🌾 Fornecedores
- Adicionar, listar, atualizar e remover fornecedores
- Buscar por nome (LINQ)

---

## 🗂 Estrutura do Projeto

```
GerenciadorGraos/
│
├── GerenciadorGraos/
│   ├── Entidades/
│   │   └── IEntidade.cs
│   │
│   ├── Implementacoes/
│   │   ├── FornecedorJsonRepository.cs
│   │   ├── GenericJsonRepository.cs
│   │   ├── GraoJsonRepository.cs
│   │   ├── SiloJsonRepository.cs
│   │   └── SiloSqlRepositorio.cs
│   │
│   ├── Interfaces/
│   │   ├── IFornecedorRepository.cs
│   │   ├── IGraoRepository.cs
│   │   ├── IRepository.cs
│   │   └── ISiloRepository.cs
│   │
│   ├── Migrations/
│   │   ├── 20250628210455_AtualizaSilo.cs
│   │   ├── 20250628210455_AtualizaSilo.Designer.cs
│   │   └── SiloDbContextModelSnapshot.cs
│   │
│   ├── Models/
│   │   └── (Classes de domínio: Grao.cs, Silo.cs, Fornecedor.cs)
│   │
│   ├── GerenciadorGraos.csproj
│   ├── GerenciadorGraos.db
│   ├── Program.cs
│   ├── SiloDbContext.cs
│   ├── fornecedores.json
│   ├── graos.json
│   ├── silos.json
│
├── GerenciadorGraos.sln
├── .gitignore
├── LICENSE
└── README.md
```

---

## ▶️ Como Executar

1. **Clone o repositório:**
   ```bash
   git clone https://github.com/seu-usuario/gerenciador-graos.git
   cd gerenciador-graos
   ```

2. **Restaure os pacotes:**
   ```bash
   dotnet restore
   ```

3. **Crie o banco (opcional, se necessário):**
   ```bash
   dotnet ef database update
   ```

4. **Execute:**
   ```bash
   dotnet run --project GerenciadorGraos
   ```

---

## ✅ Requisitos

- [.NET 6.0 ou superior](https://dotnet.microsoft.com/en-us/download)
- EF Core + SQLite
- Permissão de leitura/escrita no diretório para JSON

---

## 💡 Melhorias Futuras

- Interface gráfica (WPF ou Web)
- Exportar relatórios (PDF ou Excel)
- Login de usuários com permissões
- Integração com APIs de previsão do tempo para os silos

---

## 📄 Licença

Distribuído sob a licença definida no arquivo `LICENSE`.


