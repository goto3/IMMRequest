using System;
using Backend.Domain;
using Backend.BusinessLogic.Interface;
using Backend.Repository.Interface;
using System.Collections.Generic;
using Backend.Tools;

namespace Backend.BusinessLogic
{
    public class ApplicantLogic
    {
        public static void Validate(Applicant entity)
        {
            if (entity == null)
            {
                throw new BackendException("ERR_APPLICANT_NULL");
            }
            if (String.IsNullOrEmpty(entity.Email))
            {
                throw new BackendException("ERR_APPLICANT_EMAIL_NULL_EMPTY");
            }
            if (String.IsNullOrEmpty(entity.Name))
            {
                throw new BackendException("ERR_APPLICANT_NAME_NULL_EMPTY");
            }
            if (!EmailValidation.IsValid(entity.Email))
            {
                throw new BackendException("ERR_APPLICANT_EMAIL_INVALID");
            }
        }

    }
}
