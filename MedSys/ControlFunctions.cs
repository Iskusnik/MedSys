using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Text.RegularExpressions;

namespace MedSys
{
    public class ControlFunctions
    {
        static ModelMedContainer dbContext = new ModelMedContainer();
        static Regex nameCheck = new Regex("([А-Я]{1})([а-я]*)$");
        static Regex numCheck = new Regex("[0-9]*$");


        static public bool LoginResult(string login, 
                                       string password, 
                                       out string msg, 
                                       out Person searchResult)
        {
            msg = null;
            searchResult = null;

            if (login == "" || password == "")
            {
                msg = "Заполните поля";
                return false;
            }

            try
            {
                string fullnName = login.Split('_')[0];
                DateTime birthDate = DateTime.Parse(login.Split('_')[1]);
                searchResult = (Person)from Person p in dbContext.PersonSet where p.BirthDate == birthDate && p.FullName == fullnName select p;
                if (searchResult.Password == password)
                    return true;
                else
                {

                    msg = "Логин или пароль введены неверно";
                    return false;
                }
            }
            catch(Exception e)
            {
                msg = "Неверный формат";
                return false;
            }
        }

        
        static public Doctor CreateDoctor(string fullName, 
                                          DateTime birthDate, 
                                          string docType, 
                                          string docNum, 
                                          Job job, 
                                          string adress,
                                          string education,
                                          string gender,
                                          string insuranceNum,
                                          string password)
        {
            Doctor newDoctor = new Doctor();
            newDoctor.Adress = adress;
            newDoctor.BirthDate = birthDate;
            newDoctor.Document = CreateDocument(docType, docNum);
            newDoctor.Education = education;
            newDoctor.FullName = fullName;
            newDoctor.Gender = gender;
            newDoctor.InsuranceNum = insuranceNum;
            newDoctor.Job = job;
            newDoctor.Password = password;
            newDoctor.Record = new List<Record>();
            newDoctor.TimeForVisit = new List<TimeForVisit>();

            return newDoctor;
        }


        static public Document CreateDocument(string docType, string docNum)
        {
            Document document = new Document();
            document.Type = docType;
            document.Num = docNum;
            return document;
        }


        static public Patient CreatePatient(string fullName,
                                          DateTime birthDate,
                                          string docType,
                                          string docNum,
                                          string adress,
                                          string bloodType,
                                          string gender,
                                          string insuranceNum,
                                          string password)
        {
            Patient newPatient = new Patient();
            newPatient.Adress = adress;
            newPatient.BirthDate = birthDate;
            newPatient.Document = CreateDocument(docType, docNum);
            newPatient.BloodType = bloodType;
            newPatient.FullName = fullName;
            newPatient.Gender = gender;
            newPatient.InsuranceNum = insuranceNum;
            newPatient.Password = password;
            newPatient.MedCard = new MedCard();
            newPatient.TimeForVisit = new List<TimeForVisit>();

            return newPatient;
        }
    }   
}
