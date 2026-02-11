
using AutoMapper;
using CaseJuri.Application.DTOs;
using CaseJuri.Application.Interfaces;
using CaseJuri.Domain.Entities;

namespace CaseJuri.Application.UseCases.Tasks
{
    public class CreateToDoTaskUseCase
    {
        private readonly IToDoTaskRepository _repository;

        public CreateToDoTaskUseCase(
            IToDoTaskRepository repository
        )
        {
            _repository = repository;
        }

        public async Task ExecuteAsync(CreateToDoTaskRequest request)
        {
            var task = new ToDoTask(
                request.Titulo,
                request.Descricao,
                request.CriadoPor
            );

            await _repository.CreateAsync(task);

        }
    }
}