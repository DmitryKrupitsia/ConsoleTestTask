using HysConsoleTestTask.Models;
using Microsoft.EntityFrameworkCore;

namespace HysConsoleTestTask.Services
{
    internal class TaskService:ITaskService
    {
        private readonly HysTestTaskDbContext _context;

        public TaskService(HysTestTaskDbContext context)
        {
            _context = context;
        }

        public void RunTask1()
        {
            var today = DateOnly.FromDateTime(DateTime.Now);

            Console.WriteLine("--- Customers with only active TV products ---");
            var onlyTv = _context.Customers
                .Where(c => _context.TvProducts.Any(tv => tv.CustomerId == c.Id && tv.StartDate < today && (tv.EndDate == null || tv.EndDate > today))
                         && !_context.DslProducts.Any(dsl => dsl.CustomerId == c.Id && dsl.StartDate < today && (dsl.EndDate == null || dsl.EndDate > today)))
                .ToList();

            foreach (var c in onlyTv)
                Console.WriteLine($"[TV only] {c.Id}: {c.Email} ({c.Address})");

            Console.WriteLine("--- Customers with only active DSL products ---");
            var onlyDsl = _context.Customers
                .Where(c => _context.DslProducts.Any(dsl => dsl.CustomerId == c.Id && dsl.StartDate < today && (dsl.EndDate == null || dsl.EndDate > today))
                         && !_context.TvProducts.Any(tv => tv.CustomerId == c.Id && tv.StartDate < today && (tv.EndDate == null || tv.EndDate > today)))
                .ToList();

            foreach (var c in onlyDsl)
                Console.WriteLine($"[DSL only] {c.Id}: {c.Email} ({c.Address})");
        }

        public void RunTask2()
        {
            var today = DateOnly.FromDateTime(DateTime.Now);

            Console.WriteLine("--- Overlapping active TV products ---");

            var overlaps = (from tv1 in _context.TvProducts
                            from tv2 in _context.TvProducts
                            where tv1.CustomerId == tv2.CustomerId
                                  && tv1.Id < tv2.Id
                                  && tv1.StartDate < today
                                  && (tv1.EndDate == null || tv1.EndDate > today)
                                  && tv2.StartDate < today
                                  && (tv2.EndDate == null || tv2.EndDate > today)
                                  && tv1.StartDate <= (tv2.EndDate ?? DateOnly.MaxValue)
                                  && tv2.StartDate <= (tv1.EndDate ?? DateOnly.MaxValue)
                            select new
                            {
                                CustomerId = tv1.CustomerId,
                                Product1 = tv1.Product,
                                Product2 = tv2.Product,
                                StartDate1=tv1.StartDate,
                                EndDate1=tv1.EndDate,
                                StartDate2=tv2.StartDate,
                                EndDate2=tv2.EndDate
                            }).ToList();

            foreach (var o in overlaps)
            {
                Console.WriteLine($"Customer {o.CustomerId} has overlapping TV products: {o.Product1} & {o.Product2}");
            }
        }

        public void RunTask3()
        {
            Console.WriteLine("--- Customers with same Email and Address but different IDs (TV + DSL) ---");

            var result = (from c1 in _context.Customers
                          from c2 in _context.Customers
                          where c1.Id != c2.Id
                                && c1.Email == c2.Email
                                && c1.Address == c2.Address
                                && _context.TvProducts.Any(tv => tv.CustomerId == c1.Id)
                                && _context.DslProducts.Any(dsl => dsl.CustomerId == c2.Id)
                          select new
                          {
                              CustomerIdTv = c1.Id,
                              CustomerIdDsl = c2.Id,
                              StartDate = _context.TvProducts
                                          .Where(tv => tv.CustomerId == c1.Id)
                                          .Select(tv => tv.StartDate)
                                          .Union(
                                              _context.DslProducts
                                              .Where(dsl => dsl.CustomerId == c2.Id)
                                              .Select(dsl => dsl.StartDate)
                                          ).Max()
                          }).Distinct().ToList();

            foreach (var r in result)
            {
                Console.WriteLine($"TV Account: {r.CustomerIdTv}, DSL Account: {r.CustomerIdDsl}, Last StartDate: {r.StartDate:yyyy-MM-dd}");
            }
        }
    }
}
