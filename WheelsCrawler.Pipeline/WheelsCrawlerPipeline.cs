using System.Collections.Generic;
using System.Threading.Tasks;
using AutoMapper;
using WheelsCrawler.Data.Dto;
using WheelsCrawler.Data.Models;
using WheelsCrawler.Data.Repository;

namespace WheelsCrawler.Pipeline
{
    public class WheelsCrawlerPipeline<NEntity> : IWheelsCrawlerPipeline<NEntity> where NEntity : class, IEntity
    {
        private readonly IGenericRepository<NEntity> _repository;

        public WheelsCrawlerPipeline()
        {
            _repository = new GenericRepository<NEntity>();
        }

        public async Task Run(IEnumerable<NEntity> entityList)
        {
            foreach (var entity in entityList)
            {
                await _repository.CreateAsync(entity);
                if (await _repository.SaveAll())
                    continue;

            }
        }
    }
}