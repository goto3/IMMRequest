using System;
using System.Collections.Generic;
using Backend.BusinessLogic.Interface;
using Backend.Domain;
using IMMRequestDataImport;
using IMMRequestDataImport.domain;

namespace Backend.BusinessLogic
{
    public class DataImportLogic : IDataImportLogic
    {
        private ILogic<Area> areaLogic;
        private ILogic<Topic> topicLogic;
        private ITopicTypeLogic topicTypeLogic;

        public DataImportLogic(ILogic<Area> areaLogic, ILogic<Topic> topicLogic, ITopicTypeLogic topicTypeLogic)
        {
            this.areaLogic = areaLogic;
            this.topicLogic = topicLogic;
            this.topicTypeLogic = topicTypeLogic;
        }

        public List<DllInfo> GetAll()
        {
            var listDllInfo = new List<DllInfo>();
            var implementations = ReflectionHandling.CreateAllInstancesImplementing<IDataImport>("./ImportedDlls/");
            implementations.ForEach(imp => listDllInfo.Add(imp.GetDllInfo()));
            return listDllInfo;
        }

        public List<Area> Import(string dllName, List<ImportField> fields)
        {
            IDataImport importer = ReflectionHandling.CreateInstanceImplementing<IDataImport>(dllName, "./ImportedDlls/");
            var importedData = importer.GetNewData(fields);
            var listA = GenerateAreas(importedData);
            return listA;
        }

        private List<Area> GenerateAreas(List<ImportArea> importedData)
        {
            var listA = new List<Area>();
            foreach (ImportArea ia in importedData)
            {
                var newA = new Area() { Name = ia.Name };
                newA = areaLogic.Create(newA);
                newA.Topics = GenerateTopics(ia, newA);
                listA.Add(newA);
            }
            return listA;
        }

        private List<Topic> GenerateTopics(ImportArea ia, Area area)
        {
            var listT = new List<Topic>();
            foreach (ImportTopic it in ia.Topics)
            {
                var newT = new Topic() { Name = it.Name, Area = area };
                newT = topicLogic.Create(newT);
                newT.TopicTypes = GenerateTopicTypes(it, newT);
                listT.Add(newT);
            }
            return listT;
        }

        private List<TopicType> GenerateTopicTypes(ImportTopic it, Topic topic)
        {
            var listTT = new List<TopicType>();
            foreach (ImportTopicType itt in it.TopicTypes)
            {
                var newTT = new TopicType() { Name = itt.Name, Topic = topic };
                try
                {
                    newTT = topicTypeLogic.Create(newTT);
                }
                catch { }
                listTT.Add(newTT);
            }
            return listTT;
        }
    }
}