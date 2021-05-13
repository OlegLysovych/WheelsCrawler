using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using HtmlAgilityPack;
using HtmlAgilityPack.CssSelectors.NetCore;
using WheelsCrawler.Data.Attributes;
using WheelsCrawler.Data.Dto;
using WheelsCrawler.Data.Models;
using WheelsCrawler.Data.Repository;

namespace WheelsCrawler.Processor
{
    public class WheelsCrawlerProcessor<TEntity, NEntity> : IWheelsCrawlerProcessor<TEntity, NEntity> where TEntity : class, IEntity where NEntity : class, IEntity
    {
        private readonly IGenericRepository<NEntity> _repository;
        public WheelsCrawlerProcessor()
        {
            _repository = new GenericRepository<NEntity>();
        }
        public WheelsCrawlerProcessor(IGenericRepository<NEntity> repository)
        {
            _repository = repository;
        }
        public async Task<IEnumerable<NEntity>> Process(HtmlDocument document)
        {
            var entitiesAtOnePage = GetColumnNameValuePairsFromHtml(document);

            List<NEntity> listEntities = new List<NEntity>();
            foreach (var nameValueDictionary in entitiesAtOnePage)
            {
                var processorEntity = ReflectionHelper.CreateNewEntity<TEntity>();//need to be here crawl entity and before add check with mapping

                foreach (var pair in nameValueDictionary)
                {
                    ReflectionHelper.TrySetProperty(processorEntity, pair.Key, pair.Value);
                }

                var a = _repository.GetAll().AsEnumerable();//nentity

                var config = new MapperConfiguration(c =>
                {
                    c.AddProfile<AutoMapperProfiles>();
                });
                IMapper mapper = new Mapper(config);
                var entityToSave = mapper.Map<NEntity>(processorEntity as TEntity);

                if (a.Count() == 0 || a.All(x => x.Equals(entityToSave) == false))//map for compare
                    listEntities.Add(entityToSave);
            }

            return listEntities;
        }

        private static List<Dictionary<string, object>> GetColumnNameValuePairsFromHtml(HtmlDocument document)
        {
            var columnNameValueDictionary = new Dictionary<string, object>();
            var entitiesAtOnePage = new List<Dictionary<string, object>>();

            var entityExpression = ReflectionHelper.GetEntityExpression<TEntity>();
            var propertyExpressions = ReflectionHelper.GetPropertyAttributes<TEntity>();

            var entityNodes = document.DocumentNode?.SelectNodes(entityExpression);
            foreach (var entityNode in entityNodes)
            {
                columnNameValueDictionary = new Dictionary<string, object>();
                foreach (var expression in propertyExpressions)
                {
                    var columnName = expression.Key;
                    object columnValue = null;
                    var fieldExpression = expression.Value.Item2;

                    switch (expression.Value.Item1)
                    {
                        case SelectorType.XPath:
                            var node = entityNode?.SelectSingleNode(fieldExpression);

                            if (node != null)
                                if (fieldExpression.Contains("src"))
                                    columnValue = node.GetAttributeValue("src", "photo link");//need to do it without hardcoding
                                else if (fieldExpression.Contains("href"))
                                    columnValue = node.GetAttributeValue("href", "car link");
                                else
                                    columnValue = node.InnerText.Trim();

                            break;
                        case SelectorType.CssSelector:
                            var nodeCss = entityNode.QuerySelector(fieldExpression);
                            if (nodeCss != null)
                                columnValue = nodeCss.InnerText;
                            break;
                        case SelectorType.FixedValue:
                            if (Int32.TryParse(fieldExpression, out var result))
                            {
                                columnValue = result;
                            }
                            break;
                        default:
                            break;
                    }
                    columnNameValueDictionary.Add(columnName, columnValue);
                }
                entitiesAtOnePage.Add(columnNameValueDictionary);
            }

            return entitiesAtOnePage;
        }
    }
}