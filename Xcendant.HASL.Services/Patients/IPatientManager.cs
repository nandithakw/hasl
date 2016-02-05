using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xcendant.HASL.DataAccess.Patients;
using Xcendant.HASL.Entities;

namespace Xcendant.HASL.Services.Patients
{
    public interface IPatientManager : ICRUDLogicManager<Patient, IPatientFacade>
    {


    }
}
