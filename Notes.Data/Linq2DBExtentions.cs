using LinqToDB;
using LinqToDB.Linq;
using Notes.Data.EFProvider;
using System;
using System.Collections.Generic;
using linq = System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using System.Linq.Expressions;
using System.Reflection;
using System.Linq;

namespace Notes.Data
{
    public static class Linq2DBExtentions
    {

        public static Task<int> UpAsync<TSource>(this IUpdatable<TSource> uper)
            where TSource : EntityBase, new()
        {

            return uper.Set(e => e.UpdateTime, DateTime.Now)
              .UpdateAsync();
        }

        public static Task<int> DelAsync<TSource>(this linq.IQueryable<TSource> uper)
          where TSource : EntityBase, new()
        {

            return uper.Set(e => e.UpdateTime, DateTime.Now)
                .Set(e => e.Deleted, 1)
              .UpdateAsync();
        }


        public static IQueryable<T> Qa<T>(this ITable<T> source)
            where T : EntityBase
        {
            return source.Where(s => s.Deleted == 0);
        }


        public static Task<int> AddAsync<TSource>(this ITable<TSource> source, Expression<Func<TSource>> setter)
            where TSource : EntityBase
        {

            var input = (MemberInitExpression)setter.Body;

            var delPro = GetPro("Deleted", typeof(TSource));
            var timePro = GetPro("CreationTime", typeof(TSource));

            var bindings = new List<MemberBinding>();

            var time = DateTime.Now;

            bindings.Add(Expression.Bind(delPro, Expression.Constant(0)));
            bindings.Add(Expression.Bind(timePro, Expression.Constant(time)));

            foreach (var item in input.Bindings)
            {
                bindings.Add(item);
            }

            var memberInit = Expression.MemberInit(Expression.New(typeof(TSource)), bindings.ToArray());


            setter = Expression.Lambda<Func<TSource>>(memberInit);


            return source.InsertAsync(setter);
        }



        private static IDictionary<string, PropertyInfo[]> propertyDict = new Dictionary<string, PropertyInfo[]>();


        private static PropertyInfo GetPro(string name, Type source)
        {
            if (propertyDict.TryGetValue(source.FullName, out PropertyInfo[] values))
            {
                return values.FirstOrDefault(f => f.Name == name);
            }

            values = source.GetProperties();

            propertyDict.Add(source.FullName, values);


            return values.FirstOrDefault(f => f.Name == name);
        }


    }
}
