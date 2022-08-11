using System;
using Backend.Domain;
using Backend.Tools;

namespace Backend.WebApi.Models
{
    public class ErrorDT
    {
        public string ErrorCode { get; set; }
        public int Status { get; set; }
        public string Details { get; set; }

        public ErrorDT(Exception ex)
        {
            if (!(ex is BackendException))
            {
                if (ex is Microsoft.Data.SqlClient.SqlException)
                {
                    ErrorCode = "Database error.";
                }
                else
                {
                    ErrorCode = "ERROR";
                }
                Status = 500;
                Details = ex.Message;
            }
            else
            {
                Status = 400;
                var BackendException = (BackendException)ex;
                string msg;
                switch (BackendException.Message)
                {
                    //Area
                    case "ERR_AREA_NOT_FOUND":
                        msg = "Area {0} not found";
                        Status = 404;
                        break;
                    //Topic
                    case "ERR_TOPIC_NOT_FOUND":
                        msg = "Topic {0} not found";
                        Status = 404;
                        break;
                    //TopicType                    
                    case "ERR_TOPICTYPE_NOT_FOUND":
                        msg = "TopicType {0} not found";
                        Status = 404;
                        break;
                    case "ERR_TOPICTYPE_NAME_NULL_ERROR":
                        msg = "TopicType.Name required";
                        break;
                    case "ERR_TOPICTYPE_TOPIC_NULL_NOTFOUND":
                        msg = "Topic.Id not found or empty";
                        break;
                    case "ERR_TOPICTYPE_NAME_DUPLICATE":
                        msg = "Already exists another TopicType with that name for that Topic";
                        break;
                    case "ERR_TOPICTYPE_ADDITONALFIELD_NAME_DUPLICATE":
                        msg = "All fields in TopicType.AdditionalFields needs to have different name";
                        break;
                    //AdditionalField
                    case "ERR_ADDITIONALFIELD_NAME_NULL_EMPTY":
                        msg = "AdditionalField.Name required";
                        break;
                    case "ERR_ADDITIONALFIELD_FIELDTYPE_NULL_EMPTY":
                        msg = "AdditionalField.FieldType required";
                        break;
                    case "ERR_ADDITIONALFIELD_FIELDTYPE_INVALID":
                        msg = "AdditionalField.FieldType invalid, must be 'Text', 'Date' or 'Integer'";
                        break;
                    case "ERR_ADDITIONALFIELD_POSSIBLEVALUES_PARSE":
                        msg = "AdditionalField.PossibleValues invalid, all items need to be of type: '{0}'";
                        break;
                    case "ERR_ADDITIONALFIELD_POSSIBLEVALUES_INVALID":
                        msg = "AdditionalField.PossibleValues invalid, has to specify a range of type '{0}'. Example: ['5', '10']";
                        break;
                    case "ERR_ADDITIONALFIELD_NAME_DUPLICATE":
                        msg = "AdditionalField.Name must be unique within TopicType.AdditionalFields";
                        break;
                    //AdditionalFieldData
                    case "ERR_ADDITIONALFIELDDATA_DATA_NULL_EMPTY":
                        msg = "AdditionalField '{0}' data attribute cannot be null or empty";
                        break;
                    case "ERR_ADDITIONALFIELDDATA_INVALID":
                        msg = "AdditionalField '{0}' data attribute is not of type {1}";
                        break;
                    case "ERR_ADDITIONALFIELDDATA_OUTOFRANGE":
                        msg = "AdditionalField '{0}' data attribute is out of range. Must be between '{1}' and '{2}'";
                        break;
                    case "ERR_ADDITIONALFIELDDATA_FIELD_NULL_NOT_FOUND":
                        msg = "AdditionalField ID attribute null or not found";
                        break;
                    //Request
                    case "ERR_REQUEST_NOT_FOUND":
                        msg = "Request id not found";
                        Status = 404;
                        break;
                    case "ERR_REQUEST_DETAILS_NULL_EMPTY":
                        msg = "Request.Details required, max 2000 characters";
                        break;
                    case "ERR_REQUEST_DETAILS_LONG":
                        msg = "Request.Details cannot be longer than 2000 characters";
                        break;
                    case "ERR_REQUEST_TOPICTYPE_NULL_NOTFOUND":
                        msg = "Request.TopicType null or not found, make sure TopicType.ID exists";
                        Status = 404;
                        break;
                    case "ERR_REQUEST_MISSING_ADDITIONALFIELD":
                        msg = "Request.AdditionalFields not containing AdditionalField '{0}'";
                        break;
                    case "ERR_REQUEST_TOO_MANY_ADDITIONALFIELDS":
                        msg = "Request.AdditionalFields containing too many fields";
                        break;
                    case "ERR_REQUEST_STATUS_PARSE":
                        msg = "Request status must be one of these: {Created, In review, Accepted, Denied, Closed}";
                        break;
                    case "ERR_REQUEST_DATES_INVALID":
                        msg = "Start date should be before end date.";
                        break;
                    case "ERR_REQUEST_DATE_FORMAT":
                        msg = "Invalid Date format.";
                        break;
                    //Applicant
                    case "ERR_APPLICANT_NULL":
                        msg = "Missing Applicant information";
                        break;
                    case "ERR_APPLICANT_EMAIL_NULL_EMPTY":
                        msg = "Applicant.Email required";
                        break;
                    case "ERR_APPLICANT_NAME_NULL_EMPTY":
                        msg = "Applicant.Name required";
                        break;
                    case "ERR_APPLICANT_EMAIL_INVALID":
                        msg = "Applicant.Email is not a valid email address";
                        break;
                    //User
                    case "ERR_USER_NOT_FOUND":
                        msg = "User {0} not found in the database";
                        Status = 404;
                        break;
                    case "ERR_USER_NULL":
                        msg = "User cannot be null";
                        break;
                    case "ERR_USER_EMAIL_NULL_EMPTY":
                        msg = "User.Email cannot be null or empty";
                        break;
                    case "ERR_USER_EMAIL_INVALID":
                        msg = "User.Email is not a valid email address";
                        break;
                    case "ERR_USER_PASSWORD_NULL_EMPTY":
                        msg = "User.Password cannot be null or empty";
                        break;
                    case "ERR_USER_DUPLICATE":
                        msg = "Already exists another user with that email address";
                        break;
                    //UsersController
                    case "ERR_USERS_GET_INCORRECT":
                        msg = "User {0} not found in the database";
                        break;
                    //Session
                    case "ERR_SESSION_EMAIL":
                        msg = "Missing email field";
                        break;
                    case "ERR_SESSION_PASSWORD":
                        msg = "Missing password field";
                        break;
                    case "ERR_SESSION_CREDENTIALS":
                        msg = "Email or password invalid";
                        break;
                    case "ERR_SESSION_TOKEN_NULL_NOT_FOUND":
                        msg = "Session token not found or null. Set a 'token' header with a valid Guid session token";
                        break;
                    //Auth
                    case "ERR_AUTH_MISSING":
                        msg = "Auth header missing. Need to specify a session token in the Auth header";
                        Status = 401;
                        break;
                    case "ERR_AUTH_INVALID":
                        msg = "Auth header parse error, make sure it is a valid Guid format";
                        Status = 401;
                        break;
                    case "ERR_AUTH_INCORRECT":
                        msg = "Auth token not found in the database";
                        Status = 401;
                        break;
                    //DataImport
                    case "ERR_DATAIMPORT_DLL_NOT_FOUND":
                        msg = "Could not find the DLL. Make sure it exists and has the name '{0}'.";
                        break;

                    default:
                        msg = BackendException.Message;
                        break;
                }
                try { msg = String.Format(msg, BackendException.Args); } catch { }
                ErrorCode = BackendException.Message;
                Details = msg;
            }

        }

    }
}