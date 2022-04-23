using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

//����������� ����������
//�� ��� ��������: ���������� ������������, ���������� ��������, ��������� ��������������, ����� ������������ IHostBuilder � IWebHostBuilder
var builder = WebApplication.CreateBuilder(args);



builder.Services.AddControllers(); // ���������  ����������� ������� ��� ������������ API

//������� ��������� WebApplication - ������������ ��� ���������� ���������� ������� (��������� ���������) � ���������
//��������� ����������: IHost - ������, ��������� �����, IApplicationBuilder - ���������� ���������, IEndpointRouteBuilder - ���������� �������� �����
var app = builder.Build();

app.MapControllers(); //����������� �������� ����������� API ��� �������� �����.

app.Run();
//���� 3 ������� ��� ��������� ����������:
//WebHost.CreateDefaultBuilder() ������� � ASP.NET Core 2.x
//Host.CreateDefaultBuilder() ������ �� ��������� � .NET Core 3.x � .NET 5
//WebApplication.CreateBuilder(): .NET 6
//�� ���� ���������� ������� ASP.NET Core ������������ ��������� �� 2 �����.
//� .NET 6 ��������� ��������� ��������� � C#, BCL � ASP.NET Core, � ������ �� ����� ���� � ����� �����.
