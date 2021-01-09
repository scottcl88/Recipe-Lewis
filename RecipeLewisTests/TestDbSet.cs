using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using Microsoft.EntityFrameworkCore.ChangeTracking.Internal;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;

namespace RecipeLewisTests
{
    public class TestDbSet<T> : DbSet<T> where T : class
    {
        readonly ObservableCollection<T> _data;
        readonly IQueryable _query;

        public TestDbSet()
        {
            _data = new ObservableCollection<T>();
            _query = _data.AsQueryable();
        }

        public override EntityEntry<T> Add(T item)
        {
            _data.Add(item);
            return null;
        }
    }
}
