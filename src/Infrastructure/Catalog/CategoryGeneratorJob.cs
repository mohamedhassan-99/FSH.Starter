using Ardalis.Specification;
using FSH.Starter.Application.Catalog.Categories;
using FSH.Starter.Application.Common.Interfaces;
using FSH.Starter.Application.Common.Persistence;
using FSH.Starter.Domain.Catalog;
using FSH.Starter.Shared.Notifications;
using Hangfire;
using Hangfire.Console.Extensions;
using Hangfire.Console.Progress;
using Hangfire.Server;
using MediatR;
using Microsoft.Extensions.Logging;

namespace FSH.Starter.Infrastructure.Catalog;

public class CategoryGeneratorJob : ICategoryGeneratorJob
{
    private readonly ILogger<CategoryGeneratorJob> _logger;
    private readonly ISender _mediator;
    private readonly IReadRepository<Category> _repository;
    private readonly IProgressBarFactory _progressBar;
    private readonly PerformingContext _performingContext;
    private readonly INotificationSender _notifications;
    private readonly ICurrentUser _currentUser;
    private readonly IProgressBar _progress;

    public CategoryGeneratorJob(
        ILogger<CategoryGeneratorJob> logger,
        ISender mediator,
        IReadRepository<Category> repository,
        IProgressBarFactory progressBar,
        PerformingContext performingContext,
        INotificationSender notifications,
        ICurrentUser currentUser)
    {
        _logger = logger;
        _mediator = mediator;
        _repository = repository;
        _progressBar = progressBar;
        _performingContext = performingContext;
        _notifications = notifications;
        _currentUser = currentUser;
        _progress = _progressBar.Create();
    }

    private async Task NotifyAsync(string message, int progress, CancellationToken cancellationToken)
    {
        _progress.SetValue(progress);
        await _notifications.SendToUserAsync(
            new JobNotification()
            {
                JobId = _performingContext.BackgroundJob.Id,
                Message = message,
                Progress = progress
            },
            _currentUser.GetUserId().ToString(),
            cancellationToken);
    }

    [Queue("notdefault")]
    public async Task GenerateAsync(int nSeed, CancellationToken cancellationToken)
    {
        await NotifyAsync("Your job processing has started", 0, cancellationToken);

        foreach (int index in Enumerable.Range(1, nSeed))
        {
            await _mediator.Send(
                new CreateCategoryRequest
                {
                    Name = $"Category Random - {Guid.NewGuid()}",
                    Description = "Funny description"
                },
                cancellationToken);

            await NotifyAsync("Progress: ", nSeed > 0 ? (index * 100 / nSeed) : 0, cancellationToken);
        }

        await NotifyAsync("Job successfully completed", 0, cancellationToken);
    }

    [Queue("notdefault")]
    [AutomaticRetry(Attempts = 5)]
    public async Task CleanAsync(CancellationToken cancellationToken)
    {
        _logger.LogInformation("Initializing Job with Id: {jobId}", _performingContext.BackgroundJob.Id);

        var items = await _repository.ListAsync(new RandomCategoriesSpec(), cancellationToken);

        _logger.LogInformation("Categorys Random: {categoryCount} ", items.Count.ToString());

        foreach (var item in items)
        {
            await _mediator.Send(new DeleteCategoryRequest(item.Id), cancellationToken);
        }

        _logger.LogInformation("All random categories deleted.");
    }
}

public class RandomCategoriesSpec : Specification<Category>
{
    public RandomCategoriesSpec() =>
        Query.Where(b => !string.IsNullOrEmpty(b.Name) && b.Name.Contains("Category Random"));
}