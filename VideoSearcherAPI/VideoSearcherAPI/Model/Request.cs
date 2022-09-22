using System;
using System.Collections.Generic;
using System.Text;

namespace VideoSearcherAPI.Model
{
    public class Request
    {
        public string? videoUrl { get; set; }
        public string? keyWord { get; set; }
    }
}
