using System;
using System.IO;
using System.Net;
using System.Threading.Tasks;
using SetareSazBot.DAL;
using SetareSazBot.Domain.Entity;
using SetareSazBot.Service.Interface;

namespace SetareSazBot.Service
{
    public class ExceptionLogService : IExceptionLogService
    {
        private readonly ApplicationDbContext _context;

        public ExceptionLogService(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<ExceptionLogEntity> LogException(Exception exception, string chatId = null, string title = null)
        {
            var json = "";
            if (exception is WebException webException)
            {
                using (var stream = webException.Response.GetResponseStream())
                using (var reader = new StreamReader(stream ?? throw new InvalidOperationException()))
                {
                    json = await reader.ReadToEndAsync();

                }
            }

            var exceptionLog = new ExceptionLogEntity
            {
                ChatId = chatId,
                Title = title,
                Exception = exception.ToString(),
                WebException = json,

            };

            _context.ExceptionLogs.Add(exceptionLog);
            await _context.SaveChangesAsync();

            return exceptionLog;
        }
    }
}