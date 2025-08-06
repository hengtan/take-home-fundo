using System;
using System.Threading;
using System.Threading.Tasks;
using FluentAssertions;
using Fundo.Application.Commands.Loans.RegisterPayment;
using Fundo.Application.Interfaces;
using Fundo.Domain.Entities;
using Microsoft.Extensions.Logging;
using Moq;
using Xunit;

namespace Fundo.Services.Tests.Unit.Application.Queries.Loans;

public class RegisterPaymentAndHistoryTests
{
    [Fact]
    public async Task Handle_ShouldAddHistory_WhenPaymentIsValid()
    {
        var loanId = Guid.NewGuid();
        var command = new RegisterPaymentCommand(loanId, 500m);

        var loan = Loan.Create(loanId, 1000m, 1000m, "Test User");

        var mockRepo = new Mock<ILoanRepository>();
        mockRepo.Setup(r => r.GetByIdAsync(loanId, It.IsAny<CancellationToken>()))
            .ReturnsAsync(loan);

        var mockUnit = new Mock<IUnitOfWork>();
        mockUnit.SetupGet(u => u.LoanRepository).Returns(mockRepo.Object);
        mockUnit.Setup(u => u.CompleteAsync(It.IsAny<CancellationToken>()))
            .ReturnsAsync(1);

        var mockHistoryRepo = new Mock<IHistoryRepository>();

        History? addedHistory = null;
        mockHistoryRepo.Setup(r => r.AddAsync(It.IsAny<History>(), It.IsAny<CancellationToken>()))
            .Callback<History, CancellationToken>((h, _) => addedHistory = h)
            .Returns(Task.CompletedTask);

        var logger = Mock.Of<ILogger<RegisterPaymentCommandHandler>>();
        var handler = new RegisterPaymentCommandHandler(mockUnit.Object, mockHistoryRepo.Object, logger);

        // Act
        var result = await handler.Handle(command, CancellationToken.None);

        // Assert
        result.IsSuccess.Should().BeTrue();

        mockHistoryRepo.Verify(r => r.AddAsync(It.IsAny<History>(), It.IsAny<CancellationToken>()), Times.Once);

        addedHistory.Should().NotBeNull();
        addedHistory!.LoandId.Should().Be(loanId);
        addedHistory.Description.Should().Contain("Payment of 500");
    }

    [Fact]
    public async Task Handle_ShouldSetLoanAsPaid_AndAddHistory_WhenFullPaymentIsMade()
    {
        var loanId = Guid.NewGuid();
        var command = new RegisterPaymentCommand(loanId, 1000m);

        var loan = Loan.Create(loanId, 1000m, 1000m, "Test User");

        var mockRepo = new Mock<ILoanRepository>();
        mockRepo.Setup(r => r.GetByIdAsync(loanId, It.IsAny<CancellationToken>()))
            .ReturnsAsync(loan);

        var mockUnit = new Mock<IUnitOfWork>();
        mockUnit.SetupGet(u => u.LoanRepository).Returns(mockRepo.Object);
        mockUnit.Setup(u => u.CompleteAsync(It.IsAny<CancellationToken>()))
            .ReturnsAsync(1);

        var mockHistoryRepo = new Mock<IHistoryRepository>();
        History? addedHistory = null;
        mockHistoryRepo.Setup(r => r.AddAsync(It.IsAny<History>(), It.IsAny<CancellationToken>()))
            .Callback<History, CancellationToken>((h, _) => addedHistory = h)
            .Returns(Task.CompletedTask);

        var logger = Mock.Of<ILogger<RegisterPaymentCommandHandler>>();
        var handler = new RegisterPaymentCommandHandler(mockUnit.Object, mockHistoryRepo.Object, logger);

        // Act
        var result = await handler.Handle(command, CancellationToken.None);

        // Assert
        result.IsSuccess.Should().BeTrue();
        loan.Status.Should().Be(LoanStatus.Paid);
        mockHistoryRepo.Verify(r => r.AddAsync(It.IsAny<History>(), It.IsAny<CancellationToken>()), Times.Once);
        addedHistory.Should().NotBeNull();
        addedHistory!.LoandId.Should().Be(loanId);
        addedHistory.Description.Should().Contain("Payment of 1000");
    }

    [Fact]
    public async Task Handle_ShouldReturnFailure_WhenLoanDoesNotExist()
    {
        var loanId = Guid.NewGuid();
        var command = new RegisterPaymentCommand(loanId, 300m);

        var mockRepo = new Mock<ILoanRepository>();
        mockRepo.Setup(r => r.GetByIdAsync(loanId, It.IsAny<CancellationToken>()))
            .ReturnsAsync((Loan?)null);

        var mockUnit = new Mock<IUnitOfWork>();
        mockUnit.SetupGet(u => u.LoanRepository).Returns(mockRepo.Object);

        var mockHistoryRepo = new Mock<IHistoryRepository>();

        var logger = Mock.Of<ILogger<RegisterPaymentCommandHandler>>();
        var handler = new RegisterPaymentCommandHandler(mockUnit.Object, mockHistoryRepo.Object, logger);

        // Act
        var result = await handler.Handle(command, CancellationToken.None);

        // Assert
        result.IsSuccess.Should().BeFalse();
        result.Error.Should().NotBeNull();
        result.Error!.Message.Should().Contain("Loan not found");
        mockHistoryRepo.Verify(r => r.AddAsync(It.IsAny<History>(), It.IsAny<CancellationToken>()), Times.Never);
    }
}