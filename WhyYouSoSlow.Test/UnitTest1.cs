using System.Linq;
using WhyYouSoSlow.Entity;
using Microsoft.EntityFrameworkCore;
using Xunit;

namespace WhyYouSoSlow.Test {
  public class UnitTest1 {

    [Fact]
    public void CreateDatabase() {
      var options =
        new DbContextOptionsBuilder<WhyYouSoSlowDb>()
          .UseSqlServer("Server=localhost;Database=WhyYouSoSlow7;User Id=localsa;")
          .Options;
      new WhyYouSoSlowDb(options)
        .Database.EnsureCreated();
    }

    [Fact]
    public void SlowQuery() {
      var options =
        new DbContextOptionsBuilder<WhyYouSoSlowDb>()
          .UseSqlServer("Server=localhost;Database=WhyYouSoSlow7;User Id=localsa;")
          .Options;
      new WhyYouSoSlowDb(options)
        .Blog
        .Include(x => x.Posts)
          .ThenInclude(x => x.PostInstances)
          .ThenInclude(x => x.Comments)
          .ThenInclude(x => x.AcquiredComments)
          .ThenInclude(x => x.Tag_AcquiredComments)
          .ThenInclude(x => x.Tag)
        .FirstOrDefault();
    }

  }
}
