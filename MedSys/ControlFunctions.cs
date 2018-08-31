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
       
        //Создание сущностей
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
            newPatient.MedCard.Illness = new List<Illness>();
            newPatient.MedCard.Record = new List<Record>();
            newPatient.MedCard.Shelf = "Не приписано";
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

        //Проверка на повторения и добавление в БД
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
            dbContext.SaveChanges();
            return null;
        }

        static public string AddTimeForVisitToPatient(Patient patient, TimeForVisit visit)
        {
            visit = dbContext.TimeForVisitSet.Find(visit.Id);
            patient = (Patient)dbContext.PersonSet.Find(patient.Id);
            visit.Patient = patient;
            patient.TimeForVisit.Add(visit);
            dbContext.SaveChanges();
            return null;
        }

        static public string AddJob(Job job)
        {
            //Проверка наличия такой работы
            var searchResult = (from Job j in dbContext.JobSet
                                where j.Name == job.Name
                                select j).ToList();
            if (searchResult != null)
                return "Такая работа уже есть";
            else;


            //Добавление в БД
            dbContext.JobSet.Add(job);
            dbContext.SaveChanges();
            return null;
        }

        static public string AddTimeForVisit(TimeForVisit timeForVisit)
        {
            //Проверка не прошло ли время, в которое хотят поставить время для приёмов
            if (timeForVisit.VisitTime < DateTime.Today)
                return String.Format("Нельзя назначить приём в указанную дату. Сегодня уже {0}", DateTime.Today);
            else;

            //Проверка наличия совпадений по врачу
            var searchResult = (from t in dbContext.TimeForVisitSet
                                where t.VisitTime == timeForVisit.VisitTime &&
                                      t.Doctor.Id == timeForVisit.Doctor.Id
                                select t).ToList();
            if (searchResult != null)
                return "Такое время у такого врача уже занято";
            else;
            
            //Проверка кабинета
            searchResult = (from t in dbContext.TimeForVisitSet
                            where t.VisitTime == timeForVisit.VisitTime &&
                                  t.Cabinet.Id == timeForVisit.Cabinet.Id
                            select t).ToList();
            if (searchResult != null)
                return "Кабинет занят в это время";

            //Добавление в БД
            dbContext.TimeForVisitSet.Add(timeForVisit);
            dbContext.SaveChanges();
            return null;
        }

        static public string AddCabinet(Cabinet cabinet)
        {
            
            //Проверка наличия совпадений по этажу, комнате, корпусу
            var searchResult = (from c in dbContext.CabinetSet
                                where c.Corpus == cabinet.Corpus &&
                                      c.Floor == cabinet.Floor &&
                                      c.Num == cabinet.Num
                                select c).ToList();
            if (searchResult != null)
                return "Такой кабинет уже существует";
            else;
            
            //Добавление в БД
            dbContext.CabinetSet.Add(cabinet);
            dbContext.SaveChanges();
            return null;
        }

        static public string AddIllness(Illness illness)
        {

            //Проверка наличия совпадений по названию
            var searchResult = (from ill in dbContext.IllnessSet
                                where ill.Name == illness.Name
                                select ill).ToList();
            if (searchResult != null)
                return "Такая болезнь уже записана";
            else;

            //Добавление в БД
            dbContext.IllnessSet.Add(illness);
            dbContext.SaveChanges();
            return null;
        }

        static public string AddRecord(Record record)
        {
            //Добавление в БД
            dbContext.RecordSet.Add(record);
            dbContext.SaveChanges();
            return null;
        }

        static public Person EditPerson(Person person, string fullName,
                                            DateTime birthDate,
                                            string docType,
                                            string docNum,
                                            string adress,
                                            string gender,
                                            string insuranceNum,
                                            string password,
                                            string bloodType = "0000",
                                            Job    job = null,
                                            string education = "0000")
        {
            person = dbContext.PersonSet.Find(person.Id);

            person.Adress = adress;
            person.BirthDate = birthDate;
            person.Document = CreateDocument(docType, docNum);
            person.FullName = fullName;
            person.Gender = gender;
            person.InsuranceNum = insuranceNum;
            person.Password = password;

            if (person is Patient)
            {
                (person as Patient).BloodType = bloodType;
            }
            else;

            if (person is Doctor)
            {
                (person as Doctor).Job = job;
                (person as Doctor).Education = education;
            }
            else;

            dbContext.SaveChanges();

            return person;
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


            Patient patA = ControlFunctions.CreatePatient("Иванов Иван Иванович", DateTime.Parse("11.12.1990"), "Паспорт", "0000000003", "Home,3", "Enough", "Мужской", "12347", "2");
            Cabinet cabA = ControlFunctions.CreateCabinet("1", 1, 1);
            TimeForVisit timeA = ControlFunctions.CreateTimeForVisit(cabA, docA, DateTime.Parse("11.11.2000"));
            patA.TimeForVisit.Add(timeA);
            timeA.Patient = patA;
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
