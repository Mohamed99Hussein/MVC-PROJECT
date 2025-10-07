using Data_Access_Layer.Data.Contexts;
using Data_Access_Layer.Entities;
using Data_Access_Layer.Repositories.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Data_Access_Layer.Repositories.Classes
{
    internal class TrainerRepository : ITrainerRepository
    {
        private readonly GymSystemDBContext DbContext;
        public TrainerRepository(GymSystemDBContext context)
        {
            DbContext = context;
        }
        public int AddTrainer(Trainer trainer)
        {
            DbContext.Trainers.Add(trainer);
            return DbContext.SaveChanges();// Returns how many rows were affected

        }

        public int DeleteTrainer(int id)
        {
            var TrainerToDelete = DbContext.Trainers.Find(id);
            if (TrainerToDelete == null) return 0;
            DbContext.Trainers.Remove(TrainerToDelete);
            return DbContext.SaveChanges();// Returns how many rows were affected

        }

        public IEnumerable<Trainer> GetAllTrainers()
        {
            return DbContext.Trainers.ToList();

        }

        public Trainer? GetTrainer(int id)
        {
            return DbContext.Trainers.Find(id);

        }

        public int UpdateTrainer(Trainer trainer)
        {
            var TrainerToUpdate = DbContext.Trainers.Find(trainer.Id);
            if (TrainerToUpdate == null) return 0;
            DbContext.Trainers.Update(TrainerToUpdate);
            return DbContext.SaveChanges();// Returns how many rows were affected

        }
    }
}
