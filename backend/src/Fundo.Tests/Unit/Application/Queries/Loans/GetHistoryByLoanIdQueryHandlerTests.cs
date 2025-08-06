using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using FluentAssertions;
using Fundo.Application.Interfaces;
using Fundo.Application.Queries.Loan.History;
using Fundo.Domain.Entities;
using Microsoft.Extensions.Logging;
using Moq;
using Xunit;

namespace Fundo.Services.Tests.Unit.Application.Queries.Loans;

 public class GetHistoryByLoanIdQueryHandlerTests
    {
        [Fact]
        public async Task Handle_ShouldReturnListOfHistoryDetails_WhenHistoryExists()
        {
            // Arrange
            var loanId = Guid.NewGuid();
            var histories = new List<History>
            {
                new(loanId, "Payment of 100 made", DateTime.UtcNow),
                new(loanId, "Payment of 200 made", DateTime.UtcNow)
            };

            var mockHistoryRepo = new Mock<IHistoryRepository>();
            mockHistoryRepo.Setup(r => r.GetByLoanIdAsync(loanId, It.IsAny<CancellationToken>()))
                .ReturnsAsync(histories);

            var logger = Mock.Of<ILogger<GetHistoryDetailsByLoanIdQueryHandler>>();
            var handler = new GetHistoryDetailsByLoanIdQueryHandler(mockHistoryRepo.Object, logger);

            var query = new GetHistoryDetailsByLoanIdQuery(loanId);

            // Act
            var result = await handler.Handle(query, CancellationToken.None);

            // Assert
            result.Should().NotBeNull();
            result.Should().HaveCount(2);
            result[0].Description.Should().Be("Payment of 100 made");
            result[1].Description.Should().Be("Payment of 200 made");
        }

        [Fact]
        public async Task Handle_ShouldReturnEmptyList_WhenNoHistoryExists()
        {
            // Arrange
            var loanId = Guid.NewGuid();
            var histories = new List<History>();

            var mockHistoryRepo = new Mock<IHistoryRepository>();
            mockHistoryRepo.Setup(r => r.GetByLoanIdAsync(loanId, It.IsAny<CancellationToken>()))
                .ReturnsAsync(histories);

            var logger = Mock.Of<ILogger<GetHistoryDetailsByLoanIdQueryHandler>>();
            var handler = new GetHistoryDetailsByLoanIdQueryHandler(mockHistoryRepo.Object, logger);

            var query = new GetHistoryDetailsByLoanIdQuery(loanId);

            // Act
            var result = await handler.Handle(query, CancellationToken.None);

            // Assert
            result.Should().NotBeNull();
            result.Should().BeEmpty();
        }

    }