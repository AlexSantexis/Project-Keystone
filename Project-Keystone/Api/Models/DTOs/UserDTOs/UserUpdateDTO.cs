﻿using System.ComponentModel.DataAnnotations;

namespace Project_Keystone.Api.Models.DTOs.UserDTOs
{
    public class UserUpdateDTO
    {
        public string CurrentEmail { get; set; } = string.Empty;

        [Required]
        [StringLength(50)]
        public string Firstname { get; set; } = string.Empty;
        [Required]
        [StringLength(50)]
        public string Lastname { get; set; } = string.Empty;

        [Required]
        [EmailAddress]
        public string Email { get; set; } = string.Empty;
    }
}
