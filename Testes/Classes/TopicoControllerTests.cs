using System;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc.Testing;
using Xunit;

namespace Testes.Classes;

public class TopicoControllerTests : IClassFixture<WebApplicationFactory<Program>>
{
    private readonly HttpClient _client;

    public TopicoControllerTests(WebApplicationFactory<Program> factory)
    {
        _client = factory.CreateClient();
    }

    [Fact]
    public async Task GetTopico_ParaIdExistente_DeveRetornarStatusCode200()
    {
        var topicoIdExistente = 1;
        var url = $"/topico/{topicoIdExistente}";
        var response = await _client.GetAsync(url);
        response.EnsureSuccessStatusCode();
        Assert.Equal(HttpStatusCode.OK, response.StatusCode);
    }

    [Fact]
    public async Task PostTopico_DeveRetornarStatusCode201_AoCriarNovoTopico()
    {
        var novoTopico = new
        {
            Titulo = "Novo Tópico",
            Descricao = "Descrição do novo tópico",
            UsuarioId = 1,
            DataCriacao =  DateTime.Now
        };
        var response = await _client.PostAsJsonAsync("/topico",  novoTopico);
        
        response.EnsureSuccessStatusCode();
        Assert.Equal(HttpStatusCode.Created, response.StatusCode);
    }

    [Fact]
    public async Task PutTopico_ParaIdExistente_DeveRetornarStatusCode204_AoAtualizarTopico()
    {
        var topicoIdExistente = 1;
        var url = $"/topico/{topicoIdExistente}";

        var topicoAtualizado = new
        {
            Titulo = "Tópico Atualizado",
            Descricao = "Descrição atualizada do tópico"
        };
        var response = await _client.PostAsJsonAsync(url, topicoAtualizado);
        Assert.Equal(HttpStatusCode.NoContent, response.StatusCode);
    }

    [Fact]
    public async Task DeleteTopico_ParaIdExistente_DeveRetornarStatusCode204_AoExcluirTopico()
    {
        var topicoIdExistente = 1;
        var url = $"/topico/{topicoIdExistente}";
        var response = await _client.DeleteAsync(url);
        Assert.Equal(HttpStatusCode.NoContent, response.StatusCode);
    }
}
