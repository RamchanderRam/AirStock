using AirStock.Models;
using AirStock.DAL;
using Microsoft.EntityFrameworkCore;
using AirStock.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AirStock.Common.Models;


namespace AirStock.Services
{
    public class JobCardService : IJobCardService
    {
        private readonly StockContext _dbContext;

        public JobCardService(StockContext dbContext)
        {
            _dbContext = dbContext;
        }

        private JobCard GetEntityJobCard(JobCardModel jobCardModel)
        {
            var entity = new JobCard()
            {
                JobCardId = jobCardModel.JobCardId,
                BriefDescriptionOfWork = jobCardModel.BriefDescriptionOfWork,
                SpareForService = jobCardModel.SpareForService,
                JobCardQuantity = jobCardModel.JobCardQuantity,
                Cost = jobCardModel.Cost,
                FittedBy = jobCardModel.FittedBy,
                Total = jobCardModel.Total,
                DateIn = jobCardModel.DateIn,
                DateOut = jobCardModel.DateOut,
                TimeIn = jobCardModel.TimeIn,
                TimeOut = jobCardModel.TimeOut,
                ChiefMechanicSignature = jobCardModel.ChiefMechanicSignature,
                VehicleId = jobCardModel.VehicleId
            };

            return entity;
        }

        public static JobCardModel GetServiceJobCard(JobCard entity)
        {
            var jobCardModel = new JobCardModel()
            {
                JobCardId = entity.JobCardId,
                BriefDescriptionOfWork = entity.BriefDescriptionOfWork,
                SpareForService = entity.SpareForService,
                JobCardQuantity = entity.JobCardQuantity,
                Cost = entity.Cost,
                FittedBy = entity.FittedBy,
                Total = entity.Total,
                DateIn = entity.DateIn,
                DateOut = entity.DateOut,
                TimeIn = entity.TimeIn,
                TimeOut = entity.TimeOut,
                ChiefMechanicSignature = entity.ChiefMechanicSignature,
                VehicleId = entity.VehicleId
            };

            return jobCardModel;
        }

        public async Task<List<JobCardModel>> GetJobCards(JobCardModel jobCardModel)
        {
            // Implement your logic to fetch job cards
            //var exclusionList = new JobCardModel();
            //var jobCards = await _dbContext.JobCards
            //    .Where(j => j.VehicleId == jobCardModel.VehicleId)
            //    .Select(j => GetServiceJobCard(j))
            //    .ToListAsync();
            //exclusionList = GetServiceJobCard(jobCards);

            //return exclusionList;


            var stockList = new List<JobCardModel>();

            var jobCards = await _dbContext.JobCards.ToListAsync();
            if (!jobCards?.Any() ?? true)
            {
                throw new KeyNotFoundException("No records found");
            }
            var stocks = jobCards.Select(s => GetServiceJobCard(s)).ToList();

            return stocks;
        }

        public async Task<JobCardModel> GetByIdAsync(int id)
        {
            // Implement your logic to get a job card by ID
            var entity = await _dbContext.JobCards.FindAsync(id);
            return entity != null ? GetServiceJobCard(entity) : null;
        }

        public async Task<bool> CreateJobCard(JobCardModel entity)
        {
            // Implement your logic to add a job card
            var dbJobCard = GetEntityJobCard(entity);
            _dbContext.JobCards.Add(dbJobCard);
            await _dbContext.SaveChangesAsync();
            return true;
        }
        public async Task<bool> UpdateJobCard(JobCardModel entity)
        {
            // Implement your logic to update a job card
            var dbJobCard = await _dbContext.JobCards.FindAsync(entity.JobCardId);
            if (dbJobCard != null)
            {
                var entityJobCard = GetEntityJobCard(entity);
                entityJobCard.JobCardId = dbJobCard.JobCardId;
                _dbContext.Entry(dbJobCard).CurrentValues.SetValues(entityJobCard);
                await _dbContext.SaveChangesAsync();
            }
            return true;
        }
        public async Task<bool> RemoveJobCard(JobCardModel entity)
        {
            // Implement your logic to remove a job card
            var dbJobCard = await _dbContext.JobCards.FindAsync(entity.JobCardId);
            if (dbJobCard != null)
            {
                _dbContext.JobCards.Remove(dbJobCard);
                await _dbContext.SaveChangesAsync();
            }
            return true;
        }

        public async Task SaveChangesAsync()
        {
            await _dbContext.SaveChangesAsync();
        }
    }
}
