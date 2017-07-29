﻿using PostOffice.Common.ViewModels;
using PostOfiice.DAta.Repositories;
using System.Collections.Generic;
using System;

namespace PostOffice.Service
{
    public interface IStatisticService
    {
        IEnumerable<RevenueStatisticViewModel> GetRevenueStatistic(string fromDate, string toDate);
        IEnumerable<UnitStatisticViewModel> GetUnitStatistic(string fromDate, string toDate);
        IEnumerable<ReportFunction1> ReportFunction1(string fromDate, string toDate);
        IEnumerable<ReportFunction1> ReportFunction1(string fromDate, string toDate, int districtId);
        IEnumerable<ReportFunction1> ReportFunction1(string fromDate, string toDate, int districtId, int unitId);
        IEnumerable<ReportFunction1> RP1(string fromDate, string toDate, int districtId, int unitId);
        IEnumerable<RP1Advance> RP1Advance();
    }

    public class StatisticService : IStatisticService
    {
        private ITransactionRepository _transactionRepository;
        private IStatisticRepository _statisticRepository;

        public StatisticService(ITransactionRepository transactionRepository, IStatisticRepository statisticRepository)
        {
            _transactionRepository = transactionRepository;
            _statisticRepository = statisticRepository;
        }

        public IEnumerable<RevenueStatisticViewModel> GetRevenueStatistic(string fromDate, string toDate)
        {
            return _transactionRepository.GetRevenueStatistic(fromDate, toDate);
        }
        public IEnumerable<UnitStatisticViewModel> GetUnitStatistic(string fromDate, string toDate)
        {
            return _statisticRepository.GetUnitStatistic(fromDate, toDate);
        }

        public IEnumerable<ReportFunction1> ReportFunction1(string fromDate, string toDate)
        {
            return _statisticRepository.ReportFunction1(fromDate, toDate);
        }
        public IEnumerable<ReportFunction1> ReportFunction1(string fromDate, string toDate, int districtId)
        {
            return _statisticRepository.ReportFunction1(fromDate, toDate, districtId);
        }
        public IEnumerable<ReportFunction1> ReportFunction1(string fromDate, string toDate, int districtId, int unitId)
        {
            return _statisticRepository.ReportFunction1(fromDate, toDate, districtId, unitId);
        }

        public IEnumerable<ReportFunction1> RP1(string fromDate, string toDate, int districtId, int unitId)
        {
            return _statisticRepository.RP1(fromDate, toDate, districtId, unitId);
        }

        public IEnumerable<RP1Advance> RP1Advance() 
        {
            return _statisticRepository.RP1Advance();
        }
    }
}