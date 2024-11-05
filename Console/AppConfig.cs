using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProductConsole
{
    public class AppConfig
    {
        public string AppName { get; set; } = default!;
        public string Version { get; set; } = default!;
        public string BaseUri { get; set; } = default!;
        public string BookRoute { get; set; } = default!;
    }
}
