using AutoMapper;
using Business_Logic_Layer.Services.TranierService.Interface;
using Business_Logic_Layer.ViewModels.TrainerViewModels;
using Data_Access_Layer.Entities;
using Data_Access_Layer.Unit_Of_Work.Class;
using Data_Access_Layer.Unit_Of_Work.Interface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Business_Logic_Layer.Services.TranierService.Class
{
    public class TrainerService : ITrainerService
    {
        private readonly IUnitOfWork unitOfWork;
        private readonly IMapper mapper;

        public TrainerService(IUnitOfWork unitOfWork,IMapper mapper)
        {
            this.unitOfWork = unitOfWork;
            this.mapper = mapper;
        }

        public bool CreateTrainer(CreateTrainerViewModel createdTrainer)
        {
            if(CheckEmail(createdTrainer.Email)&&CheckPhone(createdTrainer.Phone))
                return false;

            //var Trainer = new Trainer()
            //{
            //    Email = createdTrainer.Email,
            //    Phone = createdTrainer.Phone,
            //    Gender = createdTrainer.Gender,
            //    DateOfBirth = createdTrainer.DateOfBirth,
            //    Address = new Addess()
            //    {
            //        BuildingNumber = createdTrainer.BuildingNumber,
            //        Street = createdTrainer.Street,
            //        City = createdTrainer.City,
            //    },
            //    Specialities = createdTrainer.Specialities,
            //    CreatedAt = DateTime.Now,
            //};

            var Trainer = mapper.Map<Trainer>(createdTrainer);

            unitOfWork.GetRepository<Trainer>().Add(Trainer);

            return unitOfWork.SaveChanges() > 0;
            
        }

        public bool DeleteTrainer(int TrainerId)
        {
            var trainer = unitOfWork.GetRepository<Trainer>().GetById(TrainerId);
            if (trainer is null || HasActiveSessions(trainer))
                return false;

            unitOfWork.GetRepository<Trainer>().Delete(trainer);

            return unitOfWork.SaveChanges() > 0;

        }

        public TrainerViewModel? GetTrainerDetails(int TrainerId)
        {
            var Trainer = unitOfWork.GetRepository<Trainer>().GetById(TrainerId);
             if (Trainer is null) 
                return null;

            //return new TrainerViewModel()
            //{
            //    Id = Trainer.Id,
            //    Name = Trainer.Name,
            //    Phone = Trainer.Phone,
            //    Email = Trainer.Email,
            //    Specialization = Trainer.Specialities.ToString(),

            //};
            var TrainerViewModel = mapper.Map<TrainerViewModel>(Trainer);
            return TrainerViewModel;
        }

        public IEnumerable<TrainerViewModel> GetTrainers()
        {
            var Trainers = unitOfWork.GetRepository<Trainer>().GetAll();
                if (Trainers is null || Trainers.Count() == 0)
                         return [];

            //var TrainerViewModels = Trainers.Select(x => new TrainerViewModel
            //{
            //    Id = x.Id,
            //    Name = x.Name,
            //    Phone = x.Phone,
            //    Email = x.Email,
            //    Specialization = x.Specialities.ToString(),
            //});
            var TrainerViewModels = mapper.Map<IEnumerable<TrainerViewModel>>(Trainers);

            return TrainerViewModels;
        }

        public TrainerToUpdateViewModel? GetTrainerToUpdate(int TrainerId)
        {
            var Trainer = unitOfWork.GetRepository<Trainer>().GetById(TrainerId);
             if (Trainer is null)
                return null;

            //return new TrainerToUpdateViewModel()
            //{
            //    Name = Trainer.Name,
            //    Phone = Trainer.Phone,
            //    Email = Trainer.Email,
            //    Speciality = Trainer.Specialities,
            //    BuildingNumber = Trainer.Address.BuildingNumber,
            //    City = Trainer.Address.City,
            //    Street = Trainer.Address.Street,
            //};

             var TrainerToUpdateViewModel = mapper.Map<TrainerToUpdateViewModel>(Trainer);
                return TrainerToUpdateViewModel;

        }

        public bool UpdateTrainer(int TrainerId, TrainerToUpdateViewModel UpdatedTrainer)
        {
            var Trainer = unitOfWork.GetRepository<Trainer>().GetById(TrainerId);
            if (Trainer is null||CheckEmail(UpdatedTrainer.Email)||CheckPhone(UpdatedTrainer.Phone))
                 return false;

            //Trainer.Email = UpdatedTrainer.Email;
            //Trainer.Address.BuildingNumber = UpdatedTrainer.BuildingNumber;
            //Trainer.Address.City = UpdatedTrainer.City;
            //Trainer.Address.Street = UpdatedTrainer.Street;
            //Trainer.Phone = UpdatedTrainer.Phone;
            //Trainer.Specialities = UpdatedTrainer.Speciality;
            //Trainer.UpdatedAt = DateTime.Now;

            mapper.Map(UpdatedTrainer, Trainer);


            unitOfWork.GetRepository<Trainer>().Update(Trainer);

                 return unitOfWork.SaveChanges() > 0;

        }

        private bool CheckEmail(string email)
        {
            return unitOfWork.GetRepository<Trainer>().GetAll(X => X.Email == email).Any();
        }

        private bool CheckPhone(string phone)
        {
            return unitOfWork.GetRepository<Trainer>().GetAll(X => X.Phone == phone).Any();
        }
    
        private bool HasActiveSessions(Trainer trainer)
        {
            var ActiveSessions = unitOfWork.GetRepository<Session>()
                .GetAll(x=>x.TrainerId==trainer.Id && x.StartTime > DateTime.Now).Any();

            return ActiveSessions;

        }
    
    }
}
