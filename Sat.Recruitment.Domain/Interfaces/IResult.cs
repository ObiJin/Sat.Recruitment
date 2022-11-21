using System;
using System.Collections.Generic;
using System.Text;

namespace Sat.Recruitment.Domain.Interfaces
{
    public interface IResult
    {
        public bool IsSuccess { get; set; }
        public string Errors { get; set; }
    }
}
