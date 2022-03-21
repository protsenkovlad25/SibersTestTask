using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SibersTestTask.Models.Sort
{
    //Критерии проектов для сортировки
    public enum SortProjectState
    {
        NameAsc,
        NameDesc,
        PriorityAsc,
        PriorityDesc,
        CustomerCompanyAsc,
        CustomerCompanyDesc,
        ContractorCompanyAsc,
        ContractorCompanyDesc,
        DirectorAsc,
        DirectorDesc
    }
}
