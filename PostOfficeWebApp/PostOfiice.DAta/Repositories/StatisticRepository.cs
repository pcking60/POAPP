﻿using PostOffice.Common.ViewModels;
using PostOfiice.DAta.Infrastructure;
using System.Collections;
using System.Collections.Generic;
using System;
using System.Data.SqlClient;
using System.Linq;
using System.Globalization;

namespace PostOfiice.DAta.Repositories
{
    public interface IStatisticRepository : IRepository<UnitStatisticViewModel>
    {
        IEnumerable<UnitStatisticViewModel> GetUnitStatistic(string fromDate, string toDate);
        IEnumerable<ReportFunction1> ReportFunction1(string fromDate, string toDate);
        IEnumerable<ReportFunction1> ReportFunction1(string fromDate, string toDate, int districtId);
        IEnumerable<ReportFunction1> ReportFunction1(string fromDate, string toDate, int districtId, int unitId);
        IEnumerable<ReportFunction1> RP1(string fromDate, string toDate, int districtId, int unitId);
        IEnumerable<RP1Advance> RP1Advance();
        IEnumerable<RP2_1> RP2_1();
    }

    public class StatisticRepository : RepositoryBase<UnitStatisticViewModel>, IStatisticRepository
    {
        public StatisticRepository(IDbFactory dbFactory) : base(dbFactory)
        {
        }

        public IEnumerable<UnitStatisticViewModel> GetUnitStatistic(string fromDate, string toDate)
        {
            var parameters = new SqlParameter[]{
                new SqlParameter("@fromDate", fromDate),
                new SqlParameter("@toDate", toDate)
            };
            return DbContext.Database.SqlQuery<UnitStatisticViewModel>("getUnitStatistic @fromDate,@toDate", parameters);
        }

        public IEnumerable<ReportFunction1> ReportFunction1(string fromDate, string toDate)
        {
            var parameters = new SqlParameter[] {
                new SqlParameter("@fromDate", fromDate),
                new SqlParameter("@toDate",toDate)
            };
            return DbContext.Database.SqlQuery<ReportFunction1>("reportFunction1 @fromDate,@toDate", parameters);
        }
        public IEnumerable<ReportFunction1> ReportFunction1(string fromDate, string toDate, int districtId)
        {
            var parameters = new SqlParameter[] {
                new SqlParameter("@fromDate", fromDate),
                new SqlParameter("@toDate",toDate),
                new SqlParameter("@districtId", districtId)
            };
            return DbContext.Database.SqlQuery<ReportFunction1>("reportFunction1_1 @fromDate,@toDate,@districtId", parameters);
        }
        public IEnumerable<ReportFunction1> ReportFunction1(string fromDate, string toDate, int districtId, int unitId)
        {
            var parameters = new SqlParameter[] {
                new SqlParameter("@fromDate", fromDate),
                new SqlParameter("@toDate",toDate),
                new SqlParameter("@districtId", districtId),
                new SqlParameter("@unitId", unitId)
            };
            return DbContext.Database.SqlQuery<ReportFunction1>("reportFunction1_2 @fromDate,@toDate,@districtId,@unitId", parameters);
        }

        public IEnumerable<ReportFunction1> RP1(string fromDate, string toDate, int districtId, int unitId)
        {
            var parameters = new SqlParameter[] {
                new SqlParameter("@fromDate", fromDate),
                new SqlParameter("@toDate",toDate),
                new SqlParameter("@districtId", districtId),
                new SqlParameter("@unitId", unitId)
            };
            return DbContext.Database.SqlQuery<ReportFunction1>("RP1 @fromDate,@toDate,@districtId,@unitId", parameters);
        }

        public IEnumerable<RP1Advance> RP1Advance()
        {
            
            var query = ((from td in DbContext.TransactionDetails
                          join t in DbContext.Transactions
                          on td.TransactionId equals t.ID
                          join s in DbContext.Services
                          on t.ServiceId equals s.ID
                          where
                            t.ServiceId == 1556 || t.ServiceId == 1600
                          group new { s, td } by new
                          {
                              s.Name,
                              s.VAT
                          } into g select g).ToList()
                        .Select(g => new RP1Advance
                        {
                            Revenue = (g.Sum(p => p.td.Money) / Convert.ToDecimal(g.Key.VAT)),
                            Tax = (g.Sum(p => p.td.Money) - g.Sum(p => p.td.Money) / Convert.ToDecimal(g.Key.VAT)),
                            TotalMoney = g.Sum(p => p.td.Money)
                        })).ToList();
         
            return query;

        }

        public IEnumerable<RP2_1> RP2_1()
        {
            var query = ((from td in DbContext.TransactionDetails
                          join t in DbContext.Transactions
                          on td.TransactionId equals t.ID
                          join s in DbContext.Services
                          on t.ServiceId equals s.ID
                          join sg in DbContext.ServiceGroups
                          on s.GroupID equals sg.ID
                          where
                            sg.MainServiceGroupId==1
                          group new { s, td } by new
                          {
                              s.Name,
                              s.VAT
                          } into g
                          select g).ToList()
                        .Select(g => new RP2_1
                        {
                            Revenue = (g.Sum(p => p.td.Money) / Convert.ToDecimal(g.Key.VAT)),
                            Tax = (g.Sum(p => p.td.Money) - g.Sum(p => p.td.Money) / Convert.ToDecimal(g.Key.VAT)),
                            TotalMoney = g.Sum(p => p.td.Money)
                        })).ToList();

            return query;
        }
    }
}