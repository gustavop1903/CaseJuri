using Moq;
using Xunit;
using CaseJuri.Application.Interfaces;
using CaseJuri.Application.UseCases.Tasks;
using CaseJuri.Domain.Entities;
using CaseJuri.Domain.Enums;

namespace CaseJuri.Tests.Application.UseCases
{
    public class CompleteToDoTaskUseCaseTests
    {
        [Fact]
        public async Task ExecuteAsync_CompletesTask_WhenTaskExists()
        {
            var taskId = Guid.NewGuid();
            var task = new ToDoTask("Task", "Desc", "User1");

            var mockRepo = new Mock<IToDoTaskRepository>();
            mockRepo.Setup(r => r.GetByIdAsync(taskId)).ReturnsAsync(task);

            var useCase = new CompleteToDoTaskUseCase(mockRepo.Object);

            await useCase.ExecuteAsync(taskId);

            Assert.Equal(StatusTask.Concluida, task.Status);
            Assert.NotNull(task.DataConclusao);
            mockRepo.Verify(r => r.UpdateAsync(task), Times.Once);
        }

        [Fact]
        public async Task ExecuteAsync_ThrowsException_WhenTaskNotFound()
        {
            var taskId = Guid.NewGuid();
            var mockRepo = new Mock<IToDoTaskRepository>();
            mockRepo.Setup(r => r.GetByIdAsync(taskId)).ReturnsAsync((ToDoTask)null!);

            var useCase = new CompleteToDoTaskUseCase(mockRepo.Object);
            await Assert.ThrowsAsync<Exception>(() => useCase.ExecuteAsync(taskId));
        }
    }
}
