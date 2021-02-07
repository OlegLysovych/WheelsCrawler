using System;
using System.Collections.Generic;
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
    public class WheelsCrawlerProcessor<TEntity, NEntity> : IWheelsCrawlerProcessor<TEntity, NEntity> where TEntity : class, IEntity where NEntity : class
    {
        public async Task<IEnumerable<NEntity>> Process(HtmlDocument document)
        {
            var nameValueDictionary = GetColumnNameValuePairsFromHtml(document);

            var processorEntity = ReflectionHelper.CreateNewEntity<NEntity>();
            foreach (var pair in nameValueDictionary)
            {
                ReflectionHelper.TrySetProperty(processorEntity, pair.Key, pair.Value);
            }
            var config = new MapperConfiguration(c => c.CreateMap<NEntity,TEntity>());
            IMapper mapper = new Mapper(config);
            // var entityToSave =  mapper.Map<NEntity>(processorEntity); 

            return new List<NEntity>
            {
                processorEntity as NEntity
            };
        }

        private static Dictionary<string, object> GetColumnNameValuePairsFromHtml(HtmlDocument document)
        {
            var columnNameValueDictionary = new Dictionary<string, object>();

            var entityExpression = ReflectionHelper.GetEntityExpression<TEntity>();
            var propertyExpressions = ReflectionHelper.GetPropertyAttributes<TEntity>();
            
            var entityNode = document.DocumentNode.SelectSingleNode(entityExpression);

            foreach (var expression in propertyExpressions)
            {
                var columnName = expression.Key;
                object columnValue = null;
                var fieldExpression = expression.Value.Item2;

                switch (expression.Value.Item1)
                {
                    case SelectorType.XPath:
                        var node = entityNode.SelectSingleNode(fieldExpression);

                        if (node != null)
                            if (fieldExpression.Contains("src"))
                                columnValue = node.GetAttributeValue("src", "photo link");//need to do it without hardcoding
                            else if (fieldExpression.Contains("href"))
                                columnValue = node.GetAttributeValue("href", "car link");
                            else
                                columnValue = node.InnerText.Trim();//Need to delete white spaces from innertext

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

            return columnNameValueDictionary;
        }
    }
}