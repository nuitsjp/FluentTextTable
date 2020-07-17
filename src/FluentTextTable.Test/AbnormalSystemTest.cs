using System;
using Xunit;

namespace FluentTextTable.Test
{
    public class AbnormalSystemTest
    {
        [Fact]
        public void WhenVerticalAlignmentIsOutOfRange()
        {
            var table = Build.TextTable<User>(builder =>
            {
                builder
                    .Columns.Add(x => x.Id).VerticalAlignmentAs((VerticalAlignment) 3);
            });
            Assert.Throws<ArgumentOutOfRangeException>(() => table.ToString(new[] {new User()}));
        }
        
        [Fact]
        public void WhenHorizontalAlignmentIsOutOfRange()
        {
            var table = Build.TextTable<User>(builder =>
            {
                builder
                    .Columns.Add(x => x.Id).HorizontalAlignmentAs((HorizontalAlignment) 4);
            });
            Assert.Throws<ArgumentOutOfRangeException>(() => table.ToString(new[] {new User()}));
        }
        

        private class User
        {
            public int Id { get; set; }
            public string Name { get; set; }
            public DateTime Birthday;
            public string Parents { get; set; }
            public string[] Occupations { get; set; }
        }

    }
}