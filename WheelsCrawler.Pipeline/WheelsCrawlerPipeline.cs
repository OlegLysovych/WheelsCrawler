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
        public Url Url { get; set; }
        private readonly IGenericRepository<NEntity> _repository;

        public WheelsCrawlerPipeline()
        {
            _repository = new GenericRepository<NEntity>();
        }
        public WheelsCrawlerPipeline(IGenericRepository<NEntity> repository)
        {
            _repository = repository;
        }

        public async Task Run(IEnumerable<NEntity> entityList)
        {
            foreach (var entity in entityList)
            {
                var entityToAdd = entity as Car;
                entityToAdd.RelatedQueryUrl = Url;
                await _repository.CreateAsync(entity);
                if (await _repository.SaveAll())
                    continue;
            }
        }
    }
}