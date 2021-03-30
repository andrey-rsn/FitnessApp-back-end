﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace FitnessWebApp.Models
{
    public class RegistrationViewModel
    {
        [Required]
        [Display(Name = "Логин")]
        public string UserLogin { get; set; }

        [Required]
        [UIHint("password")]
        [Display(Name = "Пароль")]
        public string Password { get; set; }

        [Required]
        [Display(Name = "Имя")]
        public string Name { get; set; }
        [Required]
        [Display(Name = "Возраст")]
        public string Age { get; set; }
        [Required]
        [Display(Name = "Вес")]
        public int Weight { get; set; }
        [Required]
        [Display(Name = "Рост")]
        public int Height { get; set; }
        [Required]
        [Display(Name = "Пол")]
        public string Gender { get; set; }
       
        [Display(Name = "Email")]
        public string Email { get; set; }

    }
}
