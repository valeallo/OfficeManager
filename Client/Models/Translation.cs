﻿using Client.Interface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Client.Models
{
    public class Translation : IPreparableItem
    {
        public string Name { get; set; }
        public int PreparationTime { get; set; }
        public bool IsReady { get; set; }
        private Guid Guid { get; set; }
        public int Price { get; private set; }
        public Translation(string language,  int preparationTime, int price)
        {
            Name = language;
            PreparationTime = preparationTime;
            IsReady = false;
            Guid = Guid.NewGuid();
            Price = price;
        }


        public void MarkAsReady()
        {
            IsReady = true;
      
        }
    }
}
