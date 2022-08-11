using Microsoft.VisualStudio.TestTools.UnitTesting;
using Backend.Domain;
using Backend.Repository.Interface;
using Moq;
using Backend.BusinessLogic.Interface;
using Backend.Tools;
using System;
using System.Collections.Generic;

namespace Backend.BusinessLogic.Tests
{
    [TestClass]
    public class ApplicantTest
    {

        [TestMethod] //Empty appplicant data
        [BackendExpectedException(typeof(BackendException), "ERR_APPLICANT_NULL")]
        public void ValidateApplicantTest1()
        {
            ApplicantLogic.Validate(null);
        }

        [TestMethod] //Empty appplicant data
        [BackendExpectedException(typeof(BackendException), "ERR_APPLICANT_EMAIL_NULL_EMPTY")]
        public void ValidateApplicantTest2()
        {
            var applicant = new Applicant();

            ApplicantLogic.Validate(applicant);
        }

        [TestMethod] //Applicant Email invalid
        [BackendExpectedException(typeof(BackendException), "ERR_APPLICANT_NAME_NULL_EMPTY")]
        public void ValidateApplicantTest3()
        {
            var applicant = new Applicant() { Email = "user@email.com" };

            ApplicantLogic.Validate(applicant);
        }

        [TestMethod] //Applicant Email invalid
        [BackendExpectedException(typeof(BackendException), "ERR_APPLICANT_EMAIL_INVALID")]
        public void ValidateApplicantTest4()
        {
            var applicant = new Applicant()
            {
                Email = "not a email address",
                Name = "Applicant name",
                PhoneNumber = "Applicant Phone Number"
            };

            ApplicantLogic.Validate(applicant);
        }

    }
}

