using LMS.API.Data;
using LMS.API.Mappings;
using LMS.API.Repositories;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddDbContext<LMSDbContext>(options =>
{
    options.UseSqlServer(builder.Configuration.GetConnectionString("LMSConnectionString"));
});
builder.Services.AddScoped<IUserRepository, SQLUserRepository>();
builder.Services.AddScoped<IMemberRepository, SQLMemberRepository>();
builder.Services.AddScoped<IBookRepository, SQLBookRepository>();
builder.Services.AddAutoMapper(typeof(AutoMapperProfiles));

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
