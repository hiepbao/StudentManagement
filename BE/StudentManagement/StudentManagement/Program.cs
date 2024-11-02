using Microsoft.EntityFrameworkCore;
using StudentManagement.Models;

var builder = WebApplication.CreateBuilder(args);

string strcnn = builder.Configuration.GetConnectionString("cnn");
builder.Services.AddDbContext<StudentManagementContext>(options => options.UseSqlServer(strcnn));

// Add services to the container.
builder.Services.AddControllers().AddJsonOptions(options =>
{
    options.JsonSerializerOptions.ReferenceHandler = System.Text.Json.Serialization.ReferenceHandler.IgnoreCycles;
});
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddSignalR(); // Thêm dịch vụ SignalR vào container

var app = builder.Build();

// Configure CORS
app.UseCors(builder => builder
    .WithOrigins("http://localhost:3000") // Thay địa chỉ bằng domain của ứng dụng ReactJS hoặc các domain cần thiết khác
    .AllowAnyHeader()
    .AllowAnyMethod()
    .AllowCredentials());

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

// Map NotificationHub để client có thể kết nối
app.MapHub<StudentManagement.Hub.NotificationHub>("/notificationHub");

app.Run();