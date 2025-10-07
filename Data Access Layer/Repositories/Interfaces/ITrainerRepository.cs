using Data_Access_Layer.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Data_Access_Layer.Repositories.Interfaces
{
    internal interface ITrainerRepository
    {
        // Get all Trainers
        IEnumerable<Trainer> GetAllTrainers();

        //  Get Trainer by id
        Trainer? GetTrainer(int id);

        //  Add Trainer
        int AddTrainer(Trainer trainer);

        //  Update Trainer
        int UpdateTrainer(Trainer trainer);

        //  Delete Trainer
        int DeleteTrainer(int id);
    }
}
