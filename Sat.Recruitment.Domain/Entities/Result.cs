using Sat.Recruitment.Domain.Interfaces;

namespace Sat.Recruitment.Domain.Entities
{ 
    public sealed class Result : IResult
    {
        public bool IsSuccess { get; set; }
        public string Errors { get; set; }
    }
}
