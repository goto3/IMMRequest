using System;
using Backend.Domain;
using Backend.BusinessLogic.Interface;
using Backend.Repository.Interface;
using System.Collections.Generic;
using Backend.Tools;

namespace Backend.BusinessLogic
{
    public class AdditionalFieldDataLogic : ILogic<AdditionalFieldData>
    {
        private IRepository<AdditionalFieldData> additionalFieldRepository;
        private ILogic<AdditionalField> additionalFieldLogic;

        public AdditionalFieldDataLogic(IRepository<AdditionalFieldData> additionalFieldDataRepo,
            ILogic<AdditionalField> additionalFieldLogic)
        {
            this.additionalFieldRepository = additionalFieldDataRepo;
            this.additionalFieldLogic = additionalFieldLogic;
        }

        public AdditionalFieldData Create(AdditionalFieldData field)
        {
            Validate(field);
            additionalFieldRepository.Add(field);
            additionalFieldRepository.Save();
            return field;
        }

        public AdditionalFieldData Get(Guid id)
        {
            return additionalFieldRepository.Get(id);
        }

        public IEnumerable<AdditionalFieldData> GetAll()
        {
            return additionalFieldRepository.GetAll();
        }

        public void Remove(Guid id)
        {
            AdditionalFieldData af = additionalFieldRepository.Get(id);
            additionalFieldRepository.Remove(af);
            additionalFieldRepository.Save();
        }

        public void Validate(AdditionalFieldData field)
        {
            ValidateAdditionalField(field);
            field.AdditionalField = additionalFieldLogic.Get(field.AdditionalField.Id);
            ValidateAdditionalField(field);
            if (field.Data == null || field.Data.Length == 0 || String.IsNullOrEmpty(field.Data[0]))
            {
                throw new BackendException("ERR_ADDITIONALFIELDDATA_DATA_NULL_EMPTY",
                    field.AdditionalField.Id.ToString());
            }
            if (!IsValidData(field))
            {
                throw new BackendException("ERR_ADDITIONALFIELDDATA_INVALID",
                    field.AdditionalField.Id.ToString(), field.AdditionalField.FieldType);
            }
            if (!IsValidPossibleValues(field))
            {
                throw new BackendException("ERR_ADDITIONALFIELDDATA_OUTOFRANGE",
                    field.AdditionalField.Id.ToString(), field.AdditionalField.PossibleValues[0], field.AdditionalField.PossibleValues[1]);
            }
            if (!field.AdditionalField.MultipleValues)
            {
                field.Data = new string[] { field.Data[0] };
            }
        }

        private void ValidateAdditionalField(AdditionalFieldData field)
        {
            if (field.AdditionalField == null)
            {
                throw new BackendException("ERR_ADDITIONALFIELDDATA_FIELD_NULL_NOT_FOUND");
            }
        }

        private bool IsValidData(AdditionalFieldData field)
        {
            if (field.AdditionalField.FieldType == "Bool")
            {
                return field.Data[0].ToLower() == "true" || field.Data[0].ToLower() == "false";
            }
            if (field.AdditionalField.FieldType == "Date")
            {
                DateTime dateValue;
                var listData = new List<String>(field.Data);
                return listData.TrueForAll(data => DateTime.TryParse(data, out dateValue));
            }
            else if (field.AdditionalField.FieldType == "Integer")
            {
                int number;
                var listData = new List<String>(field.Data);
                return listData.TrueForAll(data => Int32.TryParse(data, out number));
            }
            return true;
        }

        private bool IsValidPossibleValues(AdditionalFieldData field)
        {
            bool valid = true;
            if (field.AdditionalField.PossibleValues != null && field.AdditionalField.PossibleValues.Length > 0)
            {
                if (field.AdditionalField.FieldType == "Date")
                {
                    foreach (string str in field.Data)
                    {
                        DateTime data = DateTime.Parse(str);
                        DateTime start = DateTime.Parse(field.AdditionalField.PossibleValues[0]);
                        DateTime finish = DateTime.Parse(field.AdditionalField.PossibleValues[1]);
                        valid = valid && finish.Ticks >= data.Ticks && start.Ticks <= data.Ticks;
                    }
                }
                else if (field.AdditionalField.FieldType == "Integer")
                {
                    foreach (string str in field.Data)
                    {
                        int data = Int32.Parse(str);
                        int start = Int32.Parse(field.AdditionalField.PossibleValues[0]);
                        int finish = Int32.Parse(field.AdditionalField.PossibleValues[1]);
                        valid = valid && finish >= data && start <= data;
                    }
                }
                else if (field.AdditionalField.FieldType == "Text")
                {
                    foreach (string str in field.Data)
                    {
                        List<String> list = new List<String>(field.AdditionalField.PossibleValues);
                        valid = valid && list.Exists(p => p == str);
                    }
                }
            }
            return valid;
        }

    }
}
