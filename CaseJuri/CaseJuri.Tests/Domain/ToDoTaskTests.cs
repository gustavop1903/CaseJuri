using System;
using System.ComponentModel.DataAnnotations;
using Xunit;
using CaseJuri.Domain.Entities;
using CaseJuri.Domain.Enums;

namespace CaseJuri.Tests.Domain
{
    public class ToDoTaskTests
    {
        
        [Fact]
        public void Create_InitializesFields()
        {
            var titulo = "Teste";
            var descricao = "Descrição de teste";
            var criadoPor = "Gustavo";

            var tarefa = new ToDoTask(titulo, descricao, criadoPor);

            Assert.Equal(titulo, tarefa.Titulo);
            Assert.Equal(descricao, tarefa.Descricao);
            Assert.Equal(StatusTask.Pendente, tarefa.Status);
            Assert.Equal(criadoPor, tarefa.CriadoPor);
            Assert.True((DateTime.UtcNow - tarefa.DataCriacao) < TimeSpan.FromSeconds(2));
        }
    }
}
