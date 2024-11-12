using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;

namespace WebAPI.TaskAPI
{
    public interface IPreAdvice
    {
        IEnumerable<PreAdvice> GetPreadvice();
        void InsertPreAdvice(PreAdvice preadvice);
        Task<PreAdvice> UpdatePreAdvice(int id, PreAdvice updatePreadvice);
        Task<PreAdvice> getPreadvicebyId(int id);  

        int getTableTotalCount();  
        IEnumerable<PreAdvice> GetLimitedRecord(int skip ,int take); 

        List<PreAdvice> SearchPreAdvices(int skip ,int take,string searchValue);

        List<PreAdvice> ColumnFilter(int skip, int take, string columnName, string columnValue);

        string DeletePreAdvice(int id);

    // IEnumerable<PreAdvice> ReceiveAllEvents(EventValues eventValues);

        // Task<List<PreAdvice>> ReceiveAllEvents(EventValues eventValues);
    }
}