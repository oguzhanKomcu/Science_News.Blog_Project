using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Science_News.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Science_News.Infrastructure.EntityTypeConfig
{
    public class AppUserConfig : BaseEntityConfig<AppUser>
    {
        public override void Configure(EntityTypeBuilder<AppUser> builder)
        {
            builder.HasKey(x => x.Id);
            builder.Property(x => x.NormalizedEmail).IsRequired(false);
            builder.Property(x => x.UserName).IsRequired(true);
            builder.Property(x => x.ImagePath).IsRequired(false);

            base.Configure(builder);
        }
    }
}
