using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Science_News.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Science_News.Infrastructure.EntityTypeConfig
{
    public class CategoryConfig : BaseEntityConfig<Category>
    {
        public override void Configure(EntityTypeBuilder<Category> builder)
        {
            builder.HasKey(x => x.Id);
            builder.Property(x => x.Name).IsRequired(true);

            base.Configure(builder);

        }
    }
}
