var builder = WebApplication.CreateBuilder(args);
var app = builder.Build();

// Rota 1: Boas Vindas 
app.MapGet("/", () => "🌐 Unity System: Sua empresa conectada em um só lugar.");

// Rota 2: Status Técnico 
app.MapGet("/status", () => new { 
    sistema = "Unity System",
    versao = "1.0.0",
    status = "Online" 
});
// ESTADO EM MEMÓRIA (simula o "banco de dados" do serviço)
var produtosMundiais = new[] {
    new { Id = 1, Nome = "Pao de Sal", preco = 1.50, TenantID = "fornodouro"},
    new { Id = 2, Nome = "Oakley Juliet", preco = 800.00, TenantID = "lflupas" },
    new { Id = 3, Nome = "Mouse Pad Escola", preco = 20.00, TenantID = "escola"}
};
// Filtar Produtos por empresa 
app.MapGet("/produtos", (string empresa) => 
{
    var resultado = produtosMundiais.Where(p => p.TenantID == empresa.ToLower());

    if (!resultado.Any()) return Results.NotFound("Empresa não cadastrada no Unity System.");

    return Results.Ok(new {
        Mensagem = $"Listando itens para: {empresa.ToUpper()}",
        Itens = resultado
    });
});
// 3. Rota de RH (Comum para todos os inquilinos)
app.MapGet("/rh/status", () => new { modulo = "Recursos Humanos", status = "Ativo" });

app.Run();
