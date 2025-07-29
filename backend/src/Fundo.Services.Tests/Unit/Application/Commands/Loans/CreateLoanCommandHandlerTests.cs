using System;
using System.Threading;
using System.Threading.Tasks;
using FluentAssertions;
using Fundo.Application.Commands.Loans.Create;
using Fundo.Application.Interfaces;
using Fundo.Domain.Entities;
using Moq;
using Xunit;

namespace Fundo.Services.Tests.Unit.Application.Commands.Loans;

public class CreateLoanCommandHandlerTests
{
    [Fact]
    public async Task Handle_Should_Create_Loan_And_Return_Id()
    {
        // Arrange
        var command = new CreateLoanCommand(
            Amount: 1500m,
            CurrentBalance: 1500m,
            ApplicantName: "Maria Silva"
        );

        var mockLoanRepository = new Mock<ILoanRepository>();
        mockLoanRepository
            .Setup(repo => repo.AddAsync(It.IsAny<Loan>(), It.IsAny<CancellationToken>()))
            .Returns(Task.CompletedTask);

        var mockUnitOfWork = new Mock<IUnitOfWork>();
        mockUnitOfWork
            .Setup(uow => uow.LoanRepository)
            .Returns(mockLoanRepository.Object);

        mockUnitOfWork
            .Setup(uow => uow.CompleteAsync(It.IsAny<CancellationToken>()))
            .ReturnsAsync(1);

        var handler = new CreateLoanCommandHandler(mockUnitOfWork.Object);

        // Act
        var result = await handler.Handle(command, CancellationToken.None);

        // Assert
        result.Should().NotBeEmpty();
        mockLoanRepository.Verify(repo =>
            repo.AddAsync(It.Is<Loan>(loan =>
                loan.Amount == 1500m &&
                loan.CurrentBalance == 1500m &&
                loan.ApplicantName == "Maria Silva"
            ), It.IsAny<CancellationToken>()), Times.Once);

        mockUnitOfWork.Verify(uow => uow.CompleteAsync(It.IsAny<CancellationToken>()), Times.Once);
    }

    [Fact]
    public async Task Handle_Should_Create_Loan_With_Different_Values()
    {
        var command = new CreateLoanCommand(
            Amount: 5000m,
            CurrentBalance: 5000m,
            ApplicantName: "Carlos Mendes"
        );

        var mockLoanRepo = new Mock<ILoanRepository>();
        var mockUow = new Mock<IUnitOfWork>();

        mockLoanRepo.Setup(r => r.AddAsync(It.IsAny<Loan>(), It.IsAny<CancellationToken>()))
            .Returns(Task.CompletedTask);
        mockUow.Setup(u => u.LoanRepository).Returns(mockLoanRepo.Object);
        mockUow.Setup(u => u.CompleteAsync(It.IsAny<CancellationToken>())).ReturnsAsync(1);

        var handler = new CreateLoanCommandHandler(mockUow.Object);
        var result = await handler.Handle(command, CancellationToken.None);

        result.Should().NotBeEmpty();
    }

    [Fact]
    public async Task Handle_Should_Throw_When_CompleteAsync_Fails()
    {
        var command = new CreateLoanCommand(2000m, 2000m, "Joao");

        var mockRepo = new Mock<ILoanRepository>();
        var mockUow = new Mock<IUnitOfWork>();

        mockRepo.Setup(r => r.AddAsync(It.IsAny<Loan>(), It.IsAny<CancellationToken>()))
            .Returns(Task.CompletedTask);
        mockUow.Setup(u => u.LoanRepository).Returns(mockRepo.Object);
        mockUow.Setup(u => u.CompleteAsync(It.IsAny<CancellationToken>()))
            .ThrowsAsync(new Exception("Erro saving loan"));

        var handler = new CreateLoanCommandHandler(mockUow.Object);

        Func<Task> act = async () => await handler.Handle(command, CancellationToken.None);
        await act.Should().ThrowAsync<Exception>().WithMessage("Error");
    }

    [Fact]
    public async Task Handle_Should_Throw_When_Amount_Is_Negative()
    {
        var command = new CreateLoanCommand(-1000m, -1000m, "Negative");

        var mockRepo = new Mock<ILoanRepository>();
        var mockUow = new Mock<IUnitOfWork>();
        mockUow.Setup(uow => uow.LoanRepository).Returns(mockRepo.Object);

        var handler = new CreateLoanCommandHandler(mockUow.Object);

        Func<Task> act = async () => await handler.Handle(command, CancellationToken.None);

        await act.Should().ThrowAsync<ArgumentException>()
            .WithMessage("Loan amount must be greater than zero.");
    }

    [Fact]
    public async Task Handle_Should_Work_With_Delayed_Repository()
    {
        var command = new CreateLoanCommand(3000m, 3000m, "Delay Test");

        var mockRepo = new Mock<ILoanRepository>();
        mockRepo.Setup(r => r.AddAsync(It.IsAny<Loan>(), It.IsAny<CancellationToken>()))
            .Returns(async () =>
            {
                await Task.Delay(500); // Simula atraso
                return;
            });

        var mockUow = new Mock<IUnitOfWork>();
        mockUow.Setup(u => u.LoanRepository).Returns(mockRepo.Object);
        mockUow.Setup(u => u.CompleteAsync(It.IsAny<CancellationToken>())).ReturnsAsync(1);

        var handler = new CreateLoanCommandHandler(mockUow.Object);
        var result = await handler.Handle(command, CancellationToken.None);

        result.Should().NotBeEmpty();
    }

    [Fact]
    public async Task Handle_Should_Throw_When_LoanRepository_Is_Null()
    {
        var command = new CreateLoanCommand(1000m, 1000m, "Null Repo Test");

        var mockUow = new Mock<IUnitOfWork>();
        mockUow.Setup(u => u.LoanRepository).Returns((ILoanRepository)null!);

        var handler = new CreateLoanCommandHandler(mockUow.Object);

        Func<Task> act = async () => await handler.Handle(command, CancellationToken.None);
        await act.Should().ThrowAsync<NullReferenceException>();
    }

    [Fact]
    public async Task Handle_Should_Create_Loan_With_Correct_Capitalization()
    {
        var command = new CreateLoanCommand(1200m, 1200m, "JOANA SILVA");

        var mockRepo = new Mock<ILoanRepository>();
        var mockUow = new Mock<IUnitOfWork>();

        Loan? capturedLoan = null;

        mockRepo.Setup(r => r.AddAsync(It.IsAny<Loan>(), It.IsAny<CancellationToken>()))
            .Callback<Loan, CancellationToken>((l, _) => capturedLoan = l)
            .Returns(Task.CompletedTask);

        mockUow.Setup(u => u.LoanRepository).Returns(mockRepo.Object);
        mockUow.Setup(u => u.CompleteAsync(It.IsAny<CancellationToken>())).ReturnsAsync(1);

        var handler = new CreateLoanCommandHandler(mockUow.Object);
        var result = await handler.Handle(command, CancellationToken.None);

        result.Should().NotBeEmpty();
        capturedLoan!.ApplicantName.Should().Be("JOANA SILVA");
    }
}