﻿using Science_News.Domain.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Science_News.Domain.Entities
{
    public class Category : IBaseEntity
    {

        public int Id { get; set; }
        public string Name { get; set; }

        public Status Status { get; set; }
        public DateTime CreateDate { get; set; }
        public DateTime? UpdateDate { get; set; }
        public DateTime? DeleteDate { get; set; }

        public List<Post> Posts { get; set; }

    }
}
