namespace Data.Migrations
{
    using System.Data.Entity.Migrations;
    using System.Linq;

    internal sealed class Configuration : DbMigrationsConfiguration<Data.Context>
    {
        public Configuration()
        {
            AutomaticMigrationsEnabled = false;
        }

        protected override void Seed(Data.Context context)
        {
            //clearing the DB
            var books = context.Books.ToList();

            context.Books.RemoveRange(books);

            context.SaveChanges();
        }
    }
}
