using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Models.Dtos.Responses
{
    public class Response<T, Tfull> where T : class where Tfull : class
    {
        public bool IsSuccess { get; set; }
        public string? ErrorMessage { get; set; } = null;
        public T? Data { get; set; } = null;
        public Tfull? Entity { get; set; } = null;
    }
}
