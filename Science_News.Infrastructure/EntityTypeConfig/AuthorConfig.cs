using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Science_News.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Science_News.Infrastructure.EntityTypeConfig
{
    public class AuthorConfig : BaseEntityConfig<Author>
    {

        public override void Configure(EntityTypeBuilder<Author> builder)
        {
            builder.HasKey(x => x.Id);
            builder.Property(x => x.FirstName).IsRequired(true);
            builder.Property(x => x.LastName).IsRequired(true);
            builder.Property(x => x.ImagePath).IsRequired(true);
            base.Configure(builder);



        }
    }
}
