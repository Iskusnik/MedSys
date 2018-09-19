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
        public static ModelMedContainer dbContext = new ModelMedContainer();
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

                var info = (from Person p in dbContext.PersonSet
                            where p.BirthDate == birthDate
                            && p.FullName == fullnName
                            select p).ToList();

                searchResult = info.First();
                if (searchResult.Password == password)
                    return true;
                else
                {

                    msg = "Логин или пароль введены неверно";
                    return false;
                }
            }
            catch (Exception e)
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
            newPatient.BirthDate = birthDate.Date;
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
                                          string jobName,
                                          string adress,
                                          string education,
                                          string gender,
                                          string insuranceNum,
                                          string password)
        {
            var jobs = (from j in dbContext.JobSet where j.Name == jobName select j).ToArray();
            Job job = jobs[0];
            birthDate = birthDate.Date;
            Doctor newDoctor = new Doctor();
            newDoctor.Adress = adress;
            newDoctor.BirthDate = birthDate;
            newDoctor.Document = CreateDocument(docType, docNum);

            newDoctor.Education = education;
            newDoctor.FullName = fullName;
            newDoctor.Gender = gender;
            newDoctor.InsuranceNum = insuranceNum;
            newDoctor.Job.Add(job);
            newDoctor.Password = password;

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

        static public TimeForVisit CreateTimeForVisit(string docInfo,
                                                      string corpusName,
                                                      string cabinetNum,
                                                      string date,
                                                      string time)
        {
            TimeForVisit timeForVisit = new TimeForVisit();




            string[] docFIODB = docInfo.Split('_');
            string docFIO = docFIODB[0];
            DateTime docDB = DateTime.Parse(docFIODB[1]);
            Doctor doctor = (Doctor)(from pers in dbContext.PersonSet where pers is Doctor && pers.FullName == docFIO && pers.BirthDate == docDB select pers).ToList()[0];



            Corpus corpus = (from c in dbContext.CorpusSet where c.Name == corpusName select c).ToArray()[0];
            int num = int.Parse(cabinetNum);
            Cabinet cabinet = (from c in corpus.Cabinet where c.Num == num select c).ToArray()[0];


            DateTime dateValue = DateTime.Parse(date);
            DateTime timeValue = DateTime.Parse(time);
            DateTime visitTime = dateValue.Add(timeValue.TimeOfDay);



            timeForVisit.Cabinet = cabinet;
            timeForVisit.Doctor = doctor;
            timeForVisit.VisitTime = visitTime;

            return timeForVisit;
        }

        static public Cabinet CreateCabinet(string corpusName,
                                            int floor,
                                            int num)
        {
            Cabinet cabinet = new Cabinet();
            Corpus corpus = (from c in dbContext.CorpusSet where c.Name == corpusName select c).ToList()[0];
            cabinet.Corpus = corpus;
            cabinet.Floor = floor;
            cabinet.Num = num;
            cabinet.TimeForVisit = new List<TimeForVisit>();

            return cabinet;
        }

        static public Corpus CreateCorpus(int floors, string name)
        {
            Corpus corpus = new Corpus();
            corpus.Cabinet = new List<Cabinet>();
            corpus.Floors = floors;
            corpus.Name = name;

            return corpus;
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
            if (searchResult.Count != 0)
                return "Человек с таким ФИО и датой рождения уже зарегистрирован";
            else;

            //Проверка документов
            searchResult = (from Person p in dbContext.PersonSet
                            where p.Document.Type == person.Document.Type && p.Document.Num == person.Document.Num
                            select p).ToList();
            if (searchResult.Count != 0)
                return "Человек с таким удостоверением личности уже зарегистрирован";
            else;

            //Проверка полиса
            searchResult = (from Person p in dbContext.PersonSet
                            where p.InsuranceNum == person.InsuranceNum
                            select p).ToList();
            if (searchResult.Count != 0)
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
        static public string AddJobToDoctor(int id, string jobName)
        {
            Job job = (from Job j in dbContext.JobSet where j.Name == jobName select j).ToList()[0];
            Doctor doctor = (Doctor)dbContext.PersonSet.Find(id);

            if (job.Name == "Нет должности")
            {
                doctor.Job.Clear();
            }
            else
                if (doctor.Job.Count != 0 && doctor.Job.First().Name == "Нет должности")
                doctor.Job.Remove(doctor.Job.First());
            else;


            job.Doctor.Add(doctor);
            doctor.Job.Add(job);

            dbContext.SaveChanges();
            return null;
        }
        static public string RemoveJobFromDoctor(int id, string jobName)
        {
            
            Job job = (from Job j in dbContext.JobSet where j.Name == jobName select j).ToList()[0];
            Doctor doctor = (Doctor)dbContext.PersonSet.Find(id);

            job.Doctor.Remove(doctor);
            doctor.Job.Remove(job);
            if (doctor.Job.Count == 0)
                AddJobToDoctor(doctor.Id, "Нет должности");
            
            dbContext.SaveChanges();
            return null;
        }


        static public string AddJob(Job job)
        {
            //Проверка наличия такой работы
            var searchResult = (from Job j in dbContext.JobSet
                                where j.Name == job.Name
                                select j).ToList();
            if (searchResult.Count != 0)
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
            if (searchResult.Count != 0)
                return "Такое время у такого врача уже занято";
            else;

            //Проверка кабинета
            searchResult = (from t in dbContext.TimeForVisitSet
                            where t.VisitTime == timeForVisit.VisitTime &&
                                  t.Cabinet.Id == timeForVisit.Cabinet.Id
                            select t).ToList();
            if (searchResult.Count != 0)
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
                                where c.Corpus.Id == cabinet.Corpus.Id &&
                                      c.Num == cabinet.Num
                                select c).ToList();
            if (searchResult.Count != 0)
                return "Такой кабинет уже существует";
            else;

            //Добавление в БД
            dbContext.CabinetSet.Add(cabinet);
            dbContext.SaveChanges();
            return null;
        }

        static public string AddCorpus(Corpus corpus)
        {

            //Проверка наличия совпадений по этажу, комнате, корпусу
            var searchResult = (from c in dbContext.CorpusSet
                                where c.Name == corpus.Name
                                select c).ToList();
            if (searchResult.Count != 0)
                return "Такой корпус уже существует";
            else;

            //Добавление в БД
            dbContext.CorpusSet.Add(corpus);
            dbContext.SaveChanges();
            return null;
        }

        static public string AddIllness(Illness illness)
        {

            //Проверка наличия совпадений по названию
            var searchResult = (from ill in dbContext.IllnessSet
                                where ill.Name == illness.Name
                                select ill).ToList();
            if (searchResult.Count != 0)
                return "Такая болезнь уже записана";
            else;

            //Добавление в БД
            dbContext.IllnessSet.Add(illness);
            dbContext.SaveChanges();
            return null;
        }

        static public string AddRecord(Record record)
        {
            //Проверка наличия совпадений по названию
            var searchResult = (from rec in dbContext.RecordSet
                                where rec.Date == record.Date &&
                                      rec.Doctor == record.Doctor
                                select rec).ToList();
            if (searchResult.Count != 0)
                return "Доктор уже делал такую запись в это время";
            else;

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
                                            string education = "0000")
        {
            person = dbContext.PersonSet.Find(person.Id);

            person.Adress = adress;
            person.BirthDate = birthDate;
            person.Document.Type = docType;
            person.Document.Num = docNum;

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
                (person as Doctor).Education = education;
            }
            else;
            dbContext.Entry(person).State = System.Data.Entity.EntityState.Modified;
            dbContext.SaveChanges();

            return person;
        }

        static public TimeForVisit EditVisit(TimeForVisit visit,
                                             string docInfo,
                                             string patInfo,
                                             string corpusName,
                                             string cabinetNum,
                                             string date,
                                             string time)
        {

            visit = dbContext.TimeForVisitSet.Find(visit.Id);




            string[] docFIODB = docInfo.Split('_');
            string docFIO = docFIODB[0];
            DateTime docDB = DateTime.Parse(docFIODB[1]);
            Doctor doctor = (Doctor)(from pers in dbContext.PersonSet where pers is Doctor && pers.FullName == docFIO && pers.BirthDate == docDB select pers).ToList()[0];


            Patient patient = null;
            if (patInfo != "Нет пациента" && patInfo != "")
            {
                string[] patFIODB = patInfo.Split('_');
                string patFIO = patFIODB[0];
                DateTime patDB = DateTime.Parse(patFIODB[1]);
                patient = (Patient)(from pers in dbContext.PersonSet where pers is Patient && pers.FullName == patFIO && pers.BirthDate == patDB select pers).ToList()[0];
            }


            Corpus corpus = (from c in dbContext.CorpusSet where c.Name == corpusName select c).ToArray()[0];
            int num = int.Parse(cabinetNum);
            Cabinet cabinet = (from c in corpus.Cabinet where c.Num == num select c).ToArray()[0];


            DateTime dateValue = DateTime.Parse(date);
            DateTime timeValue = DateTime.Parse(time);
            DateTime visitTime = dateValue.Add(timeValue.TimeOfDay);

            visit.Cabinet = cabinet;
            visit.Doctor = doctor;
            visit.Patient = patient;
            visit.VisitTime = visitTime;

            dbContext.Entry(visit).State = System.Data.Entity.EntityState.Modified;
            dbContext.SaveChanges();

            return visit;
        }

        static public Record EditRecord(Record record,
                                             string docInfo,
                                             string patInfo,
                                             string info,
                                             string date,
                                             string time)
        {

            record = dbContext.RecordSet.Find(record.Id);




            string[] docFIODB = docInfo.Split('_');
            string docFIO = docFIODB[0];
            DateTime docDB = DateTime.Parse(docFIODB[1]);
            Doctor doctor = (Doctor)(from pers in dbContext.PersonSet where pers is Doctor && pers.FullName == docFIO && pers.BirthDate == docDB select pers).ToList()[0];


            Patient patient = null;
            string[] patFIODB = patInfo.Split('_');
            string patFIO = patFIODB[0];
            DateTime patDB = DateTime.Parse(patFIODB[1]);
            patient = (Patient)(from pers in dbContext.PersonSet where pers is Patient && pers.FullName == patFIO && pers.BirthDate == patDB select pers).ToList()[0];



            DateTime dateValue = DateTime.Parse(date);
            DateTime timeValue = DateTime.Parse(time);
            DateTime dateResult = dateValue.Add(timeValue.TimeOfDay);

            record.Doctor = doctor;
            record.MedCard = patient.MedCard;
            record.Date = dateResult;

            dbContext.Entry(record).State = System.Data.Entity.EntityState.Modified;
            dbContext.SaveChanges();

            return record;
        }

        static public Cabinet EditCabinet(Cabinet cabinet,
                                         string corpusName,
                                         int floor,
                                         int num)
        {

            cabinet = dbContext.CabinetSet.Find(cabinet.Id);

            Corpus corpus = (from c in dbContext.CorpusSet where c.Name == corpusName select c).ToList()[0];

            cabinet.Corpus = corpus;
            cabinet.Floor = floor;
            cabinet.Num = num;

            dbContext.Entry(cabinet).State = System.Data.Entity.EntityState.Modified;
            dbContext.SaveChanges();

            return cabinet;
        }

        static public void RemovePerson(int id)
        {
            Person pers = dbContext.PersonSet.Find(id);
            dbContext.PersonSet.Remove(pers);
            dbContext.DocumentSet.Remove(pers.Document);

            if (pers is Patient)
            {
                dbContext.MedCardSet.Remove((pers as Patient).MedCard);

                foreach (Record rec in (pers as Patient).MedCard.Record)
                    dbContext.RecordSet.Remove(rec);


                foreach (Illness ill in (pers as Patient).MedCard.Illness)
                    ill.MedCard.Remove((pers as Patient).MedCard);

                (pers as Patient).MedCard.Illness.Clear();



                foreach (TimeForVisit t in (pers as Patient).TimeForVisit)
                    dbContext.TimeForVisitSet.Remove(t);

                (pers as Patient).TimeForVisit.Clear();

            }
            else
            {
                foreach (Job j in (pers as Doctor).Job)
                    j.Doctor.Remove((pers as Doctor));

                foreach (TimeForVisit t in (pers as Doctor).TimeForVisit)
                    dbContext.TimeForVisitSet.Remove(t);

                (pers as Doctor).TimeForVisit.Clear();


                foreach (Record rec in (pers as Doctor).Record)
                    dbContext.RecordSet.Remove(rec);
            }
            dbContext.SaveChanges();
        }
        static public void RemoveJob(string jobName)
        {
            Job job = (from j in dbContext.JobSet where j.Name == jobName select j).ToList()[0];
            Job jobNull = (from j in dbContext.JobSet where j.Name == "Нет должности" select j).ToList()[0];

            foreach (Doctor d in job.Doctor)
            {
                d.Job.Remove(job);
                if (d.Job.Count == 0)
                    d.Job.Add(jobNull);
            }
            Doctor[] doctors = job.Doctor.ToArray();

            foreach (Doctor d in doctors)
                job.Doctor.Remove(d);

            dbContext.JobSet.Remove(job);
            dbContext.SaveChanges();
        }
        static public void RemoveIllness(string illnessName)
        {
            Illness illness = (from j in dbContext.IllnessSet where j.Name == illnessName select j).ToList()[0];
            
            foreach (MedCard m in illness.MedCard)
            {
                m.Illness.Remove(illness);
            }

            dbContext.IllnessSet.Remove(illness);
            dbContext.SaveChanges();
        }
        static public void RemoveCabinet(string cabinetName)
        {
            string corpusName = cabinetName.Split(':')[0];
            int cabinetNum = int.Parse(cabinetName.Split(':')[1]);
            Corpus corpus = (from c in dbContext.CorpusSet where c.Name == corpusName select c).ToArray()[0];
            Cabinet cabinet = (from c in corpus.Cabinet where c.Num == cabinetNum select c).ToArray()[0];

            TimeForVisit[] tfvs = cabinet.TimeForVisit.ToArray();
            foreach (TimeForVisit tfv in tfvs)
                dbContext.TimeForVisitSet.Remove(tfv);

            cabinet.TimeForVisit.Clear();

            dbContext.CabinetSet.Remove(cabinet);
            dbContext.SaveChanges();
        }
        static public void RemoveRecord(int id)
        {
            Record record = dbContext.RecordSet.Find(id);
            dbContext.RecordSet.Remove(record);
            dbContext.SaveChanges();
        }
        static public void RemoveVisit(int id)
        {
            TimeForVisit visit = dbContext.TimeForVisitSet.Find(id);
            dbContext.TimeForVisitSet.Remove(visit);
            dbContext.SaveChanges();
        }
        static public void RemoveCorpus(string corpusName)
        {
            Corpus corpus = (from c in dbContext.CorpusSet where c.Name == corpusName select c).ToList()[0];
            dbContext.CorpusSet.Remove(corpus);
            dbContext.SaveChanges();
        }
    }
}
//Предупреждение об удалении
//Таблица корпусов
//Несколько работ 
//Врач может быть пациентом?

/*class MyContextInitializer : DropCreateDatabaseAlways<ModelMedContainer>
    {
        protected override void Seed(ModelMedContainer db)
        {
            Job jobA = ControlFunctions.CreateJob("Главврач");
            db.JobSet.Add(jobA);
            Job jobB = ControlFunctions.CreateJob("Терапевт");
            db.JobSet.Add(jobB);
            db.SaveChanges();

            Doctor docA = ControlFunctions.CreateDoctor("Александров Александр Иванович", DateTime.Parse("11.11.1990"), "Паспорт РФ", "0000000001", "Главврач", "Home,1", "Enough", "Мужской", "12345", "2");
            db.PersonSet.Add(docA);

            
            Doctor docB = ControlFunctions.CreateDoctor("Иванов Иван Иванович", DateTime.Parse("11.11.1980"), "Паспорт РФ", "0000000002", "Терапевт", "Home,2", "Enough", "Мужской", "12346", "2");
            db.PersonSet.Add(docB);


            db.SaveChanges();




            Doctor docC = ControlFunctions.CreateDoctor("Иванов Иван Иванович", DateTime.Parse("11.11.1955"), "Паспорт РФ", "0000000003", "Терапевт", "Home,2", "Enough", "Мужской", "123473", "2");
            ControlFunctions.AddPerson(docC);
           // db.PersonSet.Add(docC);





            Patient patA = ControlFunctions.CreatePatient("Иванов Иван Иванович", DateTime.Parse("11.12.1980"), "Паспорт РФ", "0000000003", "Home,3", "+1", "Мужской", "12347", "2");
            Cabinet cabA = ControlFunctions.CreateCabinet("1", 1, 1);


            TimeForVisit timeA = ControlFunctions.CreateTimeForVisit(cabA, docB, DateTime.Parse("11.11.2000"));
            TimeForVisit timeB = ControlFunctions.CreateTimeForVisit(cabA, docB, new DateTime(2000,12,11,13,15,17));
            db.TimeForVisitSet.Add(timeB);
            db.TimeForVisitSet.Add(timeA);
            patA.TimeForVisit.Add(timeA);
            timeA.Patient = patA;


            Illness illA = ControlFunctions.CreateIllness("Что-то можно, что-то нельзя.", "Болезнь А");
            Illness illAA = ControlFunctions.CreateIllness("Что-то можно, что-то нельзя.", "Болезнь А");
            Illness illB = ControlFunctions.CreateIllness("Что-то можно, что-то нельзя.", "Болезнь Б");
            db.IllnessSet.Add(illA);
            illA.MedCard.Add(patA.MedCard);
            patA.MedCard.Illness.Add(illA);

            db.IllnessSet.Add(illB);
            illB.MedCard.Add(patA.MedCard);
            patA.MedCard.Illness.Add(illB);


            db.PersonSet.Add(patA);



            Record recA = ControlFunctions.CreateRecord(DateTime.Now, docB, "111", patA.MedCard);
            Record recB = ControlFunctions.CreateRecord(DateTime.Parse("11.11.2000"), docB, "222", patA.MedCard);
            Record recC = ControlFunctions.CreateRecord(DateTime.Parse("11.11.2010"), docA, "333", patA.MedCard);

            db.RecordSet.Add(recA);
            db.RecordSet.Add(recB);
            db.RecordSet.Add(recC);

            db.JobSet.Add(ControlFunctions.CreateJob("testJob"));
            db.SaveChanges();
        }
    }

    public partial class ModelMedContainer : DbContext
    {

        static ModelMedContainer()
        {
            Database.SetInitializer<ModelMedContainer>(new MyContextInitializer());
        }*/
        
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
