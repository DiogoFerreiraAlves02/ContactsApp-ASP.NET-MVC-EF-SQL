using ContactsApp.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace ContactsApp.Data.Map {
    public class ContactMap : IEntityTypeConfiguration<Contact> {
        public void Configure(EntityTypeBuilder<Contact> builder) {
            builder.HasKey(x => x.Id);
            builder.HasOne(x => x.User);
        }
    }
}
