using System;

namespace Kavosh.Services.DTOs
{
    public class PersonDto
    {
        public Guid Id { get; set; }
        public string FullName { get; set; }
        public string Mobile { get; set; }
        public string CodeMelli { get; set; }
        public string Address { get; set; }
    }
}