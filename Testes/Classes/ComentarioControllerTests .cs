using System.Net;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc.Testing;
using Xunit;

namespace Forum.WebAPI.Tests;

public class ComentarioControllerTests : IClassFixture<WebApplicationFactory<Program>>
{
    private readonly HttpClient _client;

    public ComentarioControllerTests(WebApplicationFactory<Program> factory)
    {
        _client = factory.CreateClient();
    }


    [Fact]
    public async Task DeveCriarNovoComentario()
    {
        var topicoIdExistente = 1;
        var comentarioDto = new
        {
            descricao = "Este é um novo comentário.",
            usuarioId = 1,
            topicoId = 1
        };

        var response = await _client.PostAsJsonAsync($"/comentario/{topicoIdExistente}",  comentarioDto);
    
        response.EnsureSuccessStatusCode();
        Assert.Equal(HttpStatusCode.Created, response.StatusCode);
    }

        [Fact]
    public async Task PutComentario_ParaIdExistente_DeveRetornarStatusCode204_AoAtualizarComentario()
    {
        var topicoIdExistente = 1;
        var comentarioIdExistente = 1;
        var url = $"/comentario/{topicoIdExistente}/{comentarioIdExistente}";

        var comentarioAtualizado = new
        {
            Titulo = "Comentario Atualizado",
            Descricao = "Descrição atualizada do Comentario"
        };
        var response = await _client.PostAsJsonAsync(url, comentarioAtualizado);
        Assert.Equal(HttpStatusCode.NoContent, response.StatusCode);
    }

    [Fact]
    public async Task DeleteComentario_ParaIdExistente_DeveRetornarStatusCode204_AoExcluirComentario()
    {
        var topicoIdExistente = 1;
        var comentarioIdExistente = 1;
        var url = $"/comentario/{topicoIdExistente}/{comentarioIdExistente}";
        var response = await _client.DeleteAsync(url);
        Assert.Equal(HttpStatusCode.NoContent, response.StatusCode);
    }
}

