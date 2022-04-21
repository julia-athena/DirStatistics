using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.Hosting;

//����������� ����������
//�� ��� ��������: ���������� ������������, ���������� ��������, ��������� ��������������, ����� ������������ IHostBuilder � IWebHostBuilder
var builder = WebApplication.CreateBuilder(args);

//������� ��������� WebApplication - ������������ ��� ���������� ���������� ������� (��������� ���������) � ���������
//��������� ����������: IHost - ������, ��������� �����, IApplicationBuilder - ���������� ���������, IEndpointRouteBuilder - ���������� �������� �����
var app = builder.Build();
//������ ����������
app.Run();
//���� 3 ������� ��� ��������� ����������:
//WebHost.CreateDefaultBuilder() ������� � ASP.NET Core 2.x
//Host.CreateDefaultBuilder() ������ �� ��������� � .NET Core 3.x � .NET 5
//WebApplication.CreateBuilder(): .NET 6
//�� ���� ���������� ������� ASP.NET Core ������������ ��������� �� 2 �����.
//� .NET 6 ��������� ��������� ��������� � C#, BCL � ASP.NET Core, � ������ �� ����� ���� � ����� �����.
