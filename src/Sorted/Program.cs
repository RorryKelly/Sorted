using MediatR;
using Microsoft.EntityFrameworkCore;
using Sorted.Application.Commands;
using Sorted.Application.Common;
using Sorted.Application.Query;
using Sorted.Domain;
using Sorted.Domain.ValueObject;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
var connectionString = builder.Configuration.GetConnectionString("DefaultConnection");
builder.Services.AddDbContext<SqlDbContext>(options =>
    options.UseSqlServer(connectionString));

builder.Services.AddDefaultIdentity<User>(options => options.SignIn.RequireConfirmedAccount = true)
    .AddEntityFrameworkStores<SqlDbContext>();
builder.Services.AddScoped<IIdentityService, IdentityService>();
// Learn more about configuring OpenAPI at https://aka.ms/aspnet/openapi
builder.Services.AddOpenApi();
builder.Services.AddMediatR(cfg => cfg.RegisterServicesFromAssemblyContaining<Program>());

//users
builder.Services.AddScoped<IRequestHandler<CreateUserCommand, string>, CreateUserHandler>();

//Expense
builder.Services.AddScoped<IRequestHandler<CreateExpenseCommand, string>, CreateExpenseHandler>();
builder.Services.AddScoped<IQueryHandler<GetExpenseQuery, List<GetExpenseResponse>>, GetExpenseHandler>();
builder.Services.AddScoped<ICreateRepository<Expense, string>, ExpenseRepository>();
builder.Services.AddScoped<IReadRepository<GetExpenseQuery, Expense>, ExpenseRepository>();

//Invoice
builder.Services.AddScoped<IRequestHandler<CreateInvoiceCommand, string>, CreateInvoiceHandler>();
builder.Services.AddScoped<IQueryHandler<GetInvoiceQuery, List<GetInvoiceResponse>>, GetInvoiceHandler>();
builder.Services.AddScoped<ICreateRepository<Invoice, string>, InvoiceRepository>();
builder.Services.AddScoped<IReadRepository<GetInvoiceQuery, Invoice>, InvoiceRepository>();

//Job
builder.Services.AddScoped<IRequestHandler<CreateJobCommand, string>, CreateJobHandler>();
builder.Services.AddScoped<IQueryHandler<GetJobQuery, List<GetJobResponse>>, GetJobHandler>();
builder.Services.AddScoped<ICreateRepository<Job, string>, JobRepository>();
builder.Services.AddScoped<IReadRepository<GetJobQuery, Job>, JobRepository>();

//Quote
builder.Services.AddScoped<IRequestHandler<CreateQuoteCommand, string>, CreateQuoteHandler>();
builder.Services.AddScoped<IQueryHandler<GetQuoteQuery, List<GetQuoteResponse>>, GetQuoteHandler>();
builder.Services.AddScoped<ICreateRepository<Quote, string>, QuoteRepository>();
builder.Services.AddScoped<IReadRepository<GetQuoteQuery, Quote>, QuoteRepository>();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.MapOpenApi();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
