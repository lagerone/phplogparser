using Autofac;
using PhpLogParser.PhpLog;

namespace PhpLogParser
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var container = ContainerConfig.Configure();
            using (var scope = container.BeginLifetimeScope())
            {
                var logService = scope.Resolve<ILogService>();
                logService.ImportPhpLog();
            }
        }
    }
}
