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

        /// <summary>
        /// Проверка логина и пароля
        /// </summary>
        /// <param name="login"></param>
        /// <param name="password"></param>
        /// <param name="msg"> Сообщение об ошибке </param>
        /// <param name="searchResult"> Результат - человек с таким логином и паролем </param>
        /// <returns></returns>
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
                var info = (from Person p in dbContext.PersonSet where p.BirthDate == birthDate && p.FullName == fullnName select p).ToList();
                searchResult = info.First();
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

        static public Document CreateDocument(string docType,
                                              string docNum)
        {
            Document document = new Document();
            document.Type = docType;
            document.Num = docNum;
            return document;
        }

        static public Job CreateJob(string name)
        {
            Job job = new Job();
            job.Name = name;
            return job;
        }

        static public Record CreateRecord(DateTime date, 
                                          Doctor doctor, 
                                          string info, 
                                          MedCard medCard)
        {
            Record record = new Record();
            record.Date = date;
            record.Doctor = doctor;
            record.Info = info;
            record.MedCard = medCard;
            return record;
        }

        static public MedCard CreateMedCard(Patient patient,
                                            string shelf)
        {
            MedCard medCard = new MedCard();
            medCard.Illness = new List<Illness>();
            medCard.Patient = patient;
            medCard.Record = new List<Record>();
            medCard.Shelf = shelf;
            return medCard;
        }

        static public TimeForVisit CreateTimeForVisit(Cabinet cabinet, 
                                                      Doctor doctor,
                                                      DateTime visitTime)
        {
            TimeForVisit timeForVisit = new TimeForVisit();
            timeForVisit.Cabinet = cabinet;
            timeForVisit.Doctor = doctor;
            timeForVisit.VisitTime = visitTime;
            
            return timeForVisit;
        }

        static public Cabinet CreateCabinet(string corpus,
                                            int floor,
                                            int num)
        {
            Cabinet cabinet = new Cabinet();
            cabinet.Corpus = corpus;
            cabinet.Floor = floor;
            cabinet.Num = num;
            cabinet.TimeForVisit = new List<TimeForVisit>();

            return cabinet;
        }

        static public Illness CreateIllness(string info,
                                            string name)
        {
            Illness illness = new Illness();
            illness.Info = info;
            illness.MedCard = new List<MedCard>();
            illness.Name = name;

            return illness;
        }

        /// <summary>
        /// Проверка данных персоны с данными в БД на совпадения
        /// </summary>
        /// <param name="person"></param>
        /// <returns></returns>
        static public string AddPerson(Person person)
        {
            //Проверка ФИО и ДР
            var searchResult = (from Person p in dbContext.PersonSet
                               where p.FullName == person.FullName && p.BirthDate == person.BirthDate select p).ToList();
            if (searchResult != null)
                return "Человек с таким ФИО и датой рождения уже зарегистрирован";
            else;

            //Проверка документов
            searchResult = (from Person p in dbContext.PersonSet
                                where p.Document.Type == person.Document.Type && p.Document.Num == person.Document.Num
                                select p).ToList();
            if (searchResult != null)
                return "Человек с таким удостоверением личности уже зарегистрирован";
            else;

            //Проверка полиса
            searchResult = (from Person p in dbContext.PersonSet
                            where p.InsuranceNum == person.InsuranceNum
                            select p).ToList();
            if (searchResult != null)
                return "Номер страхового полиса занят";

            //Добавление в БД
            dbContext.PersonSet.Add(person);
            return null;
        }

        static public string AddJob(Job job)
        {
            //Проверка ФИО и ДР
            var searchResult = (from Person p in dbContext.PersonSet
                                where p.FullName == person.FullName && p.BirthDate == person.BirthDate
                                select p).ToList();
            if (searchResult != null)
                return "Человек с таким ФИО и датой рождения уже зарегистрирован";
            else;

            //Проверка документов
            searchResult = (from Person p in dbContext.PersonSet
                            where p.Document.Type == person.Document.Type && p.Document.Num == person.Document.Num
                            select p).ToList();
            if (searchResult != null)
                return "Человек с таким удостоверением личности уже зарегистрирован";
            else;

            //Проверка полиса
            searchResult = (from Person p in dbContext.PersonSet
                            where p.InsuranceNum == person.InsuranceNum
                            select p).ToList();
            if (searchResult != null)
                return "Номер страхового полиса занят";

            //Добавление в БД
            dbContext.PersonSet.Add(person);
            return null;
        }

    }
}


/*
//------------------------------------------------------------------------------
// <auto-generated>
//     Этот код создан по шаблону.
//
//     Изменения, вносимые в этот файл вручную, могут привести к непредвиденной работе приложения.
//     Изменения, вносимые в этот файл вручную, будут перезаписаны при повторном создании кода.
// </auto-generated>
//------------------------------------------------------------------------------

namespace MedSys
{
    using System;
    using System.Data.Entity;
    using System.Data.Entity.Infrastructure;

    class MyContextInitializer : DropCreateDatabaseAlways<ModelMedContainer>
    {
        protected override void Seed(ModelMedContainer db)
        {
            Job jobA = ControlFunctions.CreateJob("Главврач");
            Doctor docA = ControlFunctions.CreateDoctor("Александров Александр Иванович", DateTime.Parse("11.11.1980"), "Паспорт", "0000000001", jobA, "Home,1", "Enough", "Мужской", "12345", "2");
            db.PersonSet.Add(docA);

            Job jobB = ControlFunctions.CreateJob("Терапевт");
            db.JobSet.Add(jobB);
            Doctor docB = ControlFunctions.CreateDoctor("Иванов Иван Иванович", DateTime.Parse("11.12.1980"), "Паспорт", "0000000002", jobB, "Home,2", "Enough", "Мужской", "12346", "2");
            db.PersonSet.Add(docB);


            Patient patA = ControlFunctions.CreatePatient("Иванов Иван Иванович", DateTime.Parse("11.12.1990"), "Паспорт", "0000000002", "Home,3", "Enough", "Мужской", "12347", "2");
            db.PersonSet.Add(patA);


            db.SaveChanges();
        }
    }

    public partial class ModelMedContainer : DbContext
    {

        static ModelMedContainer()
        {
            Database.SetInitializer<ModelMedContainer>(new MyContextInitializer());
        }

        public ModelMedContainer()
            : base("name=ModelMedContainer")
        {
            Database.SetInitializer<ModelMedContainer>(new MyContextInitializer());
        }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            throw new UnintentionalCodeFirstException();
        }

        public virtual DbSet<Person> PersonSet { get; set; }
        public virtual DbSet<MedCard> MedCardSet { get; set; }
        public virtual DbSet<Record> RecordSet { get; set; }
        public virtual DbSet<TimeForVisit> TimeForVisitSet { get; set; }
        public virtual DbSet<Illness> IllnessSet { get; set; }
        public virtual DbSet<Job> JobSet { get; set; }
        public virtual DbSet<Document> DocumentSet { get; set; }
        public virtual DbSet<Cabinet> CabinetSet { get; set; }
    }
}

     */
