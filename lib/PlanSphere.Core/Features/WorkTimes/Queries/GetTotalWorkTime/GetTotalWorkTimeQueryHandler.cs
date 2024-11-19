using System.Xml;
using Domain.Entities;
using Domain.Entities.EmbeddedEntities;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using PlanSphere.Core.Attributes;
using PlanSphere.Core.Enums;
using PlanSphere.Core.Interfaces.Repositories;

namespace PlanSphere.Core.Features.WorkTimes.Queries.GetTotalWorkTime;

[HandlerType(HandlerType.SystemApi)]
public class GetTotalWorkTimeQueryHandler(
    IWorkTimeRepository workTimeRepository,
    ILogger<GetTotalWorkTimeQueryHandler> logger
): IRequestHandler<GetTotalWorkTimeQuery, string>
{
    private readonly IWorkTimeRepository _workTimeRepository = workTimeRepository ?? throw new ArgumentNullException(nameof(workTimeRepository));
    private readonly ILogger<GetTotalWorkTimeQueryHandler> _logger = logger ?? throw new ArgumentNullException(nameof(logger));
    
    public async Task<string> Handle(GetTotalWorkTimeQuery request, CancellationToken cancellationToken)
    {
        _logger.BeginScope("Fetching total work time for a given period.");
        var query = GetTime(request);
        var workTimes = await query.ToListAsync(cancellationToken);
        
        var totalWorkTime = CalculateTotalWorkTime(workTimes);
        string isoDuration = XmlConvert.ToString(totalWorkTime);
        
        return isoDuration;

    }

    private IQueryable<WorkTime> GetTime(GetTotalWorkTimeQuery request)
    {
        var query = _workTimeRepository.GetQueryable().Where(workTime => workTime.UserId == request.UserId);

        query = request.WorkTimeType switch
        {
            WorkTimeType.Regular => query.Where(x => x.WorkTimeType == WorkTimeType.Regular),
            WorkTimeType.Overtime => query.Where(x => x.WorkTimeType == WorkTimeType.Overtime),
            WorkTimeType.Sick => query.Where(x => x.WorkTimeType == WorkTimeType.Sick),
            WorkTimeType.Vacation => query.Where(x => x.WorkTimeType == WorkTimeType.Vacation),
            WorkTimeType.Absent => query.Where(x => x.WorkTimeType == WorkTimeType.Absent),
            _ => throw new ArgumentOutOfRangeException(nameof(request.Period), $"Unsupported type: {request.WorkTimeType}")
        };
        return GetPeriodTime(query, request);
    }

    private IQueryable<WorkTime> GetPeriodTime(IQueryable<WorkTime> query, GetTotalWorkTimeQuery request)
    {
        var today = DateTime.Today;

        return request.Period switch
        {
            Periods.Day => query.Where(workTime =>
                workTime.StartDateTime.Date == today.Date || workTime.EndDateTime.HasValue && workTime.EndDateTime.Value.Date == today.Date),

            Periods.Week =>
                query.Where(workTime =>
                    workTime.StartDateTime.Date >= GetStartOfWeek(today) &&
                    workTime.StartDateTime.Date <= GetEndOfWeek(today)),

            Periods.Month =>
                query.Where(workTime =>
                    workTime.StartDateTime.Date >= new DateTime(today.Year, today.Month, 1) &&
                    workTime.StartDateTime.Date < new DateTime(today.Year, today.Month, 1).AddMonths(1)),
            Periods.Year =>
                query.Where(workTime =>
                    workTime.StartDateTime.Date >= new DateTime(today.Year, 1, 1) &&
                    workTime.StartDateTime.Date < new DateTime(today.Year, 12, 31)),
            Periods.Total => query,
            _ => throw new ArgumentOutOfRangeException(nameof(request.Period), $"Unsupported period: {request.Period}")
        };
    }
    
    private static DateTime GetStartOfWeek(DateTime date)
    {
        var diff = (int)date.DayOfWeek - (int)DayOfWeek.Monday;
        return date.AddDays(-diff).Date;
    }

    private static DateTime GetEndOfWeek(DateTime date)
    {
        return GetStartOfWeek(date).AddDays(6);
    }
    
    private TimeSpan CalculateTotalWorkTime(IEnumerable<WorkTime> workTimes)
    {
        return workTimes.Aggregate(TimeSpan.Zero, (total, workTime) =>
        {
            var endDateTime = workTime.EndDateTime ?? DateTime.Now;
            return total + (endDateTime - workTime.StartDateTime);
        });
    }


}