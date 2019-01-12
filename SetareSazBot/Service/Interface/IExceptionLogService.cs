using System;
using System.Threading.Tasks;
using SetareSazBot.Domain.Entity;

namespace SetareSazBot.Service.Interface
{
    public interface IExceptionLogService
    {
        Task<ExceptionLogEntity> LogException(Exception exception, string chatId = null, string title = null);
    }
}
