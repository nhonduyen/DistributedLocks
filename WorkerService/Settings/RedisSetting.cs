using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WorkerService.Settings
{
    public class RedisSetting
    {
        public string RedisUrl { get; set; }
        public bool Ssl { get; set; }
        public bool AbortOnConnectFail { get; set; }
        public double AbsoluteExpirationRelativeToNow { get; set; }
        public double SlidingExpiration { get; set; }
        public double Expriry { get; set; }
        public double Wait { get; set; }
        public double Retry { get; set; }
    }
}
