﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FitnessWebApp.Models
{
    public class UserProfileViewModel
    {
        public string Name { get; set; }
       
        public int MetricAge { get; set; }

      
        public string MetricGoal { get; set; }

      
        public List<string> HealthProblems { get; set; }

        
        public int MetricHeight { get; set; }

        
        public int MetricPullUps { get; set; }

        
        public int MetricPushUps { get; set; }

        
        public int MetricWeight { get; set; }
        
        public string MetricGender { get; set; }

    }
}
