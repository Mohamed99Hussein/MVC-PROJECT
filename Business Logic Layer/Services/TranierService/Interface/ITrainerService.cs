using Business_Logic_Layer.ViewModels.TrainerViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Business_Logic_Layer.Services.TranierService.Interface
{
    public interface ITrainerService
    {
        IEnumerable<TrainerViewModel> GetTrainers();

        bool CreateTrainer(CreateTrainerViewModel createdTrainer);

        TrainerViewModel? GetTrainerDetails(int TrainerId);

        TrainerToUpdateViewModel? GetTrainerToUpdate(int TrainerId);

        bool UpdateTrainer(int TrainerId,TrainerToUpdateViewModel UpdatedTrainer);

        bool DeleteTrainer(int TrainerId);
    }
}
