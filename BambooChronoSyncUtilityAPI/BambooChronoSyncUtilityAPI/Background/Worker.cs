using BambooChronoSyncUtility.Application;
using BambooChronoSyncUtility.Application.Models;

namespace BambooChronoSyncUtilityAPI.Background
{
    public class Worker : BackgroundService
    {
        private readonly ILogger<Worker> _logger;
        //private readonly IConfiguration _config;
        private readonly IWorkerOption _workerOption;
        private readonly ISynchronizer _synchronizer;
        private string PeriodType { get; set; } = string.Empty;  // Период запуска синхронизации
        private int DayOfPeriodStart { get; set; }  //  День запуска в периоде
        private string TimeOfDayStart { get; set; } = string.Empty;  //  Время в дне
        private DateTime _date;
        private TimeSpan _period;
        private readonly int eps = 100000;
        private bool isWorking = false;
        public Worker(ISynchronizer synchronizer, IWorkerOption workerOption, ILogger<Worker> logger)
        {
            _logger = logger;
            _workerOption = workerOption;
            _synchronizer = synchronizer;
            
        }

        protected override async Task ExecuteAsync(CancellationToken stoppingToken)
        {
            GetData();
            _logger.LogInformation("Background Synchronizer running at: {time}", DateTimeOffset.Now);
            _logger.LogInformation($"Date : {_date}  period : {_period}");
            while (!stoppingToken.IsCancellationRequested)
            {
                //  Проверка на время выполнения синхронизации
                var x = DateTime.UtcNow;
                var y = _date;
                var z = (y - x).Duration().TotalMilliseconds;
                if (z < eps && !isWorking)
                {
                    isWorking = true;
                    //  Синхронизация
                    await _synchronizer.Synchronize();
                    isWorking = false;
                }

                //  Получение новых данных о дате и периоде
                GetData();
                //  Пересчёт периода
                var newperiod = _date - DateTime.UtcNow;
                //  По идее - проверка на отрицательное число. И выяснить - почему?
                _logger.LogInformation("Background Synchronizer running at: {time}", DateTimeOffset.Now);
                await Task.Delay(newperiod, stoppingToken);
            }
        }
        public override Task StopAsync(CancellationToken stoppingToken)
        {
            isWorking = false;
            _logger.LogInformation("Background Synchronizer is stopping.");

            return base.StopAsync(stoppingToken);
        }
        private void GetData()
        {
            _workerOption.GetDate(out DateTime date, out TimeSpan period);
            _date = date;
            _period = period;
        }
    }
}
