var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.AddServiceDefaults();

//more like a connection string for the database
builder.AddNpgsqlDbContext<ProductDbContext>(connectionName: "catalogdb");
builder.Services.AddScoped<ProductService>();


// Learn more about configuring OpenAPI at https://aka.ms/aspnet/openapi
builder.Services.AddOpenApi();

var app = builder.Build();

// Configure the HTTP request pipeline.
app.MapDefaultEndpoints();
app.UseMigration();
app.MapProductEndpoints();


if (app.Environment.IsDevelopment())
{
    app.MapOpenApi();
}

app.UseHttpsRedirection();

app.Run();
