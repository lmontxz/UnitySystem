using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

// ===============================================================
// 1. REGISTRO DOS SERVIÇOS 
// ===============================================================
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(c =>
{
    c.SwaggerDoc("v1", new()
    {
        Title = "Unity System",
        Version = "V1",
        Description = "API para controle de estoque centralizado"
    });
});

// Conectando no Azure SQL
builder.Services.AddDbContext<AppDbContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));

var app = builder.Build();

// ===============================================================
// 1.5. APLICAR MIGRATIONS AUTOMATICAMENTE 
// Isso vai lá no banco do Azure e cria a tabela 'Items' sozinho!
// ===============================================================
using (var scope = app.Services.CreateScope())
{
    var db = scope.ServiceProvider.GetRequiredService<AppDbContext>();
    db.Database.Migrate();
}

// ===============================================================
// 2. CONFIGURAÇÃO DE MIDDLEWARES (Swagger)
// ===============================================================
app.UseSwagger();
app.UseSwaggerUI(c =>
{
    c.SwaggerEndpoint("/swagger/v1/swagger.json", "Unity System API");
    c.RoutePrefix = ""; 
});

// ===============================================================
// 3. ROTAS ( conectadas no Banco de Dados)
// ===============================================================

// Mensagem de Boas-Vindas
app.MapGet("/info", () => new
{
    service = "Unity System API",
    version = "1.0.0",
    description = "API conectada ao Azure SQL Server",
    timestamp = DateTime.UtcNow
});

// GET = LER TUDO
app.MapGet("/items", async (AppDbContext db) =>
{
    // Vai no Azure e busca a lista real
    var items = await db.Items.ToListAsync();
    return Results.Ok(items);
})
.WithName("GetAllItems").WithTags("Items").WithSummary("Lista todos os itens cadastrados");

// GET = LER POR ID
app.MapGet("/items/{id}", async (int id, AppDbContext db) =>
{
    var item = await db.Items.FindAsync(id);
    if (item is null)
        return Results.NotFound(new { message = $"Item com id={id} não encontrado." });
    
    return Results.Ok(item);
})
.WithName("GetItemById").WithTags("Items").WithSummary("Busca um item pelo ID");

// POST = CRIAR
app.MapPost("/items", async (ItemRequest request, AppDbContext db) =>
{
    if (string.IsNullOrWhiteSpace(request.Name))
        return Results.BadRequest(new { message = "O campo 'name' é obrigatório e não pode ser vazio." });
    
    var item = new Item
    {
        Name = request.Name.Trim(),
        Quantidade = request.Quantidade,
        EmpresaId = request.EmpresaId.Trim(),
        CreatedAt = DateTime.UtcNow
    };
    
    db.Items.Add(item);
    await db.SaveChangesAsync(); // Salva de verdade no banco!
    
    return Results.Created($"/items/{item.Id}", item);
})
.WithName("CreateItem").WithTags("Items").WithSummary("Cria um novo item no banco");

// PUT = ATUALIZAR
app.MapPut("/items/{id}", async (int id, ItemRequest request, AppDbContext db) =>
{
    if (string.IsNullOrWhiteSpace(request.Name))
        return Results.BadRequest(new { message = "O campo 'name' é obrigatório e não pode ser vazio." });
    
    var item = await db.Items.FindAsync(id);
    if (item is null)
        return Results.NotFound(new { message = $"Item com id={id} não encontrado." });
    
    item.Name = request.Name.Trim();
    item.Quantidade = request.Quantidade;
    item.EmpresaId = request.EmpresaId.Trim();

    await db.SaveChangesAsync(); // Grava a alteração
    
    return Results.Ok(item);
})
.WithName("UpdateItem").WithTags("Items").WithSummary("Atualiza um item existente");

// DELETE = DELETAR
app.MapDelete("/items/{id}", async (int id, AppDbContext db) =>
{
    var item = await db.Items.FindAsync(id);
    if (item is null)
        return Results.NotFound(new { message = $"Item com id={id} não encontrado." });

    db.Items.Remove(item);
    await db.SaveChangesAsync(); // Exclui definitivamente

    return Results.NoContent();
})
.WithName("DeleteItem").WithTags("Items").WithSummary("Remove um item pelo ID");

app.Run();

// ===============================================================
// 4. MODELOS E CONTEXTO DO BANCO DE DADOS
// ===============================================================
 
// O Entity Framework trabalha muito melhor com classes na hora de atualizar dados (PUT).
public class Item
{
    public int Id { get; set; }
    public string Name { get; set; } = string.Empty;
    public int Quantidade { get; set; }
    public string EmpresaId { get; set; } = string.Empty;
    public DateTime CreatedAt { get; set; }
}

public record ItemRequest(string Name, int Quantidade, string EmpresaId);

//  classe "Ponte" entre o C# e o SQL Server
public class AppDbContext : DbContext
{
    public AppDbContext(DbContextOptions<AppDbContext> options) : base(options) { }
    public DbSet<Item> Items => Set<Item>();
}