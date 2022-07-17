﻿using RWADatabaseLibrary.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Admin.Models
{
    public class TagCount
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public int Count { get; set; }

        public bool Visible { get => Count > 0; }

    }
}