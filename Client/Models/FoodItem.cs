﻿using Client.Interface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Client.Models
{
    public class FoodItem : IPreparableItem
    {
        public string Name { get; set; }
        public int PreparationTime { get; set; } 

        public bool IsReady { get; set; }
        private Guid Guid { get; set; }

        public FoodItem(string name, int preparationTime)
        {
            Name = name;
            PreparationTime = preparationTime;
            IsReady = false;
            Guid = Guid.NewGuid();

        }


        public void MarkAsReady()
        {
            IsReady = true;
          
        }
    }
}
