using System;
using Backend.Domain;
using Backend.BusinessLogic.Interface;
using Backend.Repository.Interface;
using System.Collections.Generic;
using System.Linq;
using Backend.Tools;

namespace Backend.BusinessLogic
{
    public class AdditionalFieldLogic : ILogic<AdditionalField>
    {
        private IRepository<AdditionalField> additionalFieldRepository;

        public AdditionalFieldLogic(IRepository<AdditionalField> additionalFieldRepo)
        {
            this.additionalFieldRepository = additionalFieldRepo;
        }

        public AdditionalField Create(AdditionalField newField)
        {
            Validate(newField);
            additionalFieldRepository.Add(newField);
            additionalFieldRepository.Save();
            return newField;
        }

        public AdditionalField Get(Guid id)
        {
            return additionalFieldRepository.Get(id);
        }

        public IEnumerable<AdditionalField> GetAll()
        {
            return additionalFieldRepository.GetAll();
        }

        public void Remove(Guid id)
        {
            AdditionalField af = additionalFieldRepository.Get(id);
            additionalFieldRepository.Remove(af);
            additionalFieldRepository.Save();
        }

        public void Validate(AdditionalField field)
        {
            if (String.IsNullOrEmpty(field.Name))
            {
                throw new BackendException("ERR_ADDITIONALFIELD_NAME_NULL_EMPTY");
            }
            if (String.IsNullOrEmpty(field.FieldType))
            {
                throw new BackendException("ERR_ADDITIONALFIELD_FIELDTYPE_NULL_EMPTY");
            }
            if (field.FieldType != "Text" && field.FieldType != "Integer" && field.FieldType != "Date" && field.FieldType != "Bool")
            {
                throw new BackendException("ERR_ADDITIONALFIELD_FIELDTYPE_INVALID");
            }
            if (field.FieldType == "Bool")
            {
                field.PossibleValues = null;
            }
            ValidatePossibleValues(field.PossibleValues, field.FieldType);
        }

        private void ValidatePossibleValues(string[] possibleValues, string fieldType)
        {
            if (possibleValues != null && possibleValues.Length > 0)
            {
                try
                {
                    if (fieldType == "Date")
                    {
                        DateTime dateValue;
                        possibleValues.ToList().ForEach(pv => dateValue = DateTime.Parse(pv));
                    }
                    else if (fieldType == "Integer")
                    {
                        int intValue;
                        possibleValues.ToList().ForEach(pv => intValue = int.Parse(pv));
                    }
                }
                catch
                {
                    throw new BackendException("ERR_ADDITIONALFIELD_POSSIBLEVALUES_PARSE", fieldType);
                }
                var valid = true;
                if (fieldType == "Date")
                {
                    valid = possibleValues.Count() == 2 && DateTime.Parse(possibleValues[0]) <= DateTime.Parse(possibleValues[1]);
                }
                else if (fieldType == "Integer")
                {
                    valid = possibleValues.Count() == 2 && int.Parse(possibleValues[0]) <= int.Parse(possibleValues[1]);
                }
                if (!valid)
                {
                    throw new BackendException("ERR_ADDITIONALFIELD_POSSIBLEVALUES_INVALID", fieldType);
                }
            }
        }

    }
}
