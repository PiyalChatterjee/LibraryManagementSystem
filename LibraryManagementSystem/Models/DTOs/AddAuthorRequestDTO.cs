﻿namespace LMS.API.Models.DTOs
{
    public class AddAuthorRequestDTO
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string? Biography { get; set; }
        public DateTime? DateOfBirth { get; set; }
    }
}
