using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WindowsAppEngG01.Utils
{
    static class UseBackend
    {
        private static bool backend = false;

        public static bool Backend { get => backend; set => backend = value; }
    }
}
