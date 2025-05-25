var builder = DistributedApplication.CreateBuilder(args);

// Backing Services
var postgres = builder
         .AddPostgres("postgres")
         .WithPgAdmin()
         //.WithDataVolume()
         .WithLifetime(ContainerLifetime.Persistent);

var catalogDb = postgres.AddDatabase("catalogdb");

var cache = builder
    .AddRedis("cache")
    .WithRedisInsight()
    //.WithDataVolume()
    .WithLifetime(ContainerLifetime.Persistent);

var rabbitmq = builder
    .AddRabbitMQ("rabbitmq")
    .WithManagementPlugin()
    //.WithDataVolume()
    .WithLifetime(ContainerLifetime.Persistent);

var keycloak = builder
    .AddKeycloak("keycloak", 8080)
    //.WithDataVolume()
    .WithLifetime(ContainerLifetime.Persistent);

var ollama = builder
    .AddOllama("ollama", 11434)
    //.WithDataVolume()
    .WithLifetime(ContainerLifetime.Persistent)
    .WithOpenWebUI();

var chat = ollama.AddModel("chat", "llama3.2");
var embeddings = ollama.AddModel("embedding", "all-minilm");

//var llama = ollama.AddModel("llama3.2");
//var embedding = ollama.AddModel("all-minilm");

if (builder.ExecutionContext.IsRunMode)
{
    //Data volume don't work on ACA for
    postgres.WithDataVolume();
    keycloak.WithDataVolume();
    rabbitmq.WithDataVolume();
    cache.WithDataVolume();
    ollama.WithDataVolume();
}

// add projects
var catalog = builder
    .AddProject<Projects.Catalog>("catalog")
    .WithReference(catalogDb)
    .WithReference(rabbitmq)
     .WithReference(chat)
     .WithReference(embeddings)
    .WaitFor(catalogDb)
    .WaitFor(rabbitmq)
    .WaitFor(chat)
    .WaitFor(embeddings);

var basket = builder
    .AddProject<Projects.Basket>("basket")
    .WithReference(cache)
    .WithReference(catalog)
    .WithReference(rabbitmq)
    .WithReference(keycloak)
    .WaitFor(cache)
    .WaitFor(rabbitmq)
    .WaitFor(keycloak);

var webapp = builder
    .AddProject<Projects.WebApp>("webapp")
    .WithExternalHttpEndpoints()
    .WithReference(cache)
    .WithReference(catalog)
    .WithReference(basket)
    .WaitFor(catalog)
    .WaitFor(basket);
// add projects

builder.Build().Run();
