using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using WebAPI.TaskAPI;

namespace WebAPI.TaskAPI
{
    public class PreAdviceService:IPreAdvice
    {
        private readonly PreAdviceDbContext _context;

        public PreAdviceService(PreAdviceDbContext context){
            _context = context;          
        }

        public IEnumerable<PreAdvice> GetPreadvice(){
           return _context.PreAdvices.ToList();
        }

        // Insertion
        public void InsertPreAdvice(PreAdvice preadvice){
            _context.PreAdvices.Add(preadvice);
            _context.SaveChanges();
        }

        //get by id

        public async Task<PreAdvice> getPreadvicebyId(int id){
            try{
                var preadd = await _context.PreAdvices.FirstOrDefaultAsync(g => g.preAdviceId == id);
                if(preadd == null){
                    throw new InvalidOperationException("Id not found");
                }
                return preadd;
            }
            catch(Exception ex){
                throw ex;
            }
        }

        // Update
        public async Task<PreAdvice> UpdatePreAdvice(int id, PreAdvice updatePreadvice)
        {
            using (var transaction = _context.Database.BeginTransaction())
            {
                try
                { 
                    var existingEntity = await _context.PreAdvices.FindAsync(id);
                    if (existingEntity == null)
                    {
                        throw new Exception($"Entity with id {id} not found.");
                    }
                     _context.Entry(existingEntity).CurrentValues.SetValues(updatePreadvice);
                      await _context.SaveChangesAsync();
                      transaction.Commit();
                      return existingEntity;           
                }
                catch (Exception ex)
                {
                    transaction.Rollback();
                    throw ex;
                }
           }
        }

        // Delete

        public string DeletePreAdvice(int id){
            var entity = _context.PreAdvices.Find(id);
            if(entity != null){
                _context.PreAdvices.Remove(entity);
                _context.SaveChanges();
            }
            return "Id Deleted";
        }

        //Lazy loading count

        public int getTableTotalCount(){
            IQueryable<PreAdvice> query = _context.PreAdvices;
            var total = query.Count();
            return total;
    
        }

        // Get the specify record count
        public IEnumerable<PreAdvice> GetLimitedRecord(int skip ,int take){
            IQueryable<PreAdvice> query = _context.PreAdvices;
            return query.Skip(skip).Take(take).ToList();
        }

        // Global search 
        public List<PreAdvice> SearchPreAdvices(int skip ,int take,string searchValue)
        {
            if(searchValue != null){
            var query = from a in _context.PreAdvices
            where a.depot.Contains(searchValue) ||
             a.liner.Contains(searchValue) ||
             a.redelAuthNo.Contains(searchValue) ||
             a.vesselCarrier.Contains(searchValue) ||
             a.vesselName.Contains(searchValue)
             select a;
             return query.Skip(skip).Take(take).ToList();
                
            }
            else{
                return null;
            }         

        }

        // Column Filter
        public List<PreAdvice> ColumnFilter(int skip, int take, string columnName, string columnValue)
        {
            var query = _context.PreAdvices.AsQueryable();
            columnName = columnName.ToLower();
            if (!string.IsNullOrEmpty(columnName) && !string.IsNullOrEmpty(columnValue))
            {
                switch (columnName)
                {
                    case "depot":
                    query = from a in _context.PreAdvices
                    where a.depot.Contains(columnValue)
                    select a;
                 
                    break;
                    case "liner":
                    query = from a in _context.PreAdvices
                    where a.liner.Contains(columnValue)
                    select a;
                    break;
                    case "redelauthno":
                    query = from a in _context.PreAdvices
                    where a.redelAuthNo.Contains(columnValue)
                    select a;
                    break;
                    case "vesselcarrier":
                    query = from a in _context.PreAdvices
                    where a.vesselCarrier.Contains(columnValue)
                    select a;
                    break;
                    case "vesselname":
                    query = from a in _context.PreAdvices
                    where a.vesselName.Contains(columnValue)
                    select a;
                    break;
                    default:
                    break;
                }
            }
            var filteredRecords = query.Skip(skip).Take(take).ToList();
            return filteredRecords;  
        }
    }
 }

 // Receive all the events

//         public Task<List<PreAdvice>> ReceiveAllEvents(EventValues eventValues){
//             try
//             {
//                 var first = eventValues.first;
//                 var take = eventValues.take;
//                 var globalFilters = eventValues.globalFilter != null ? eventValues.globalFilter : null;
//                 var columnName = eventValues.columnName;
//                 var columnValue = eventValues.columnValue;
//                 IQueryable<PreAdvice> query = _context.PreAdvices;
//                 columnName = columnName.ToLower();
//                 if (globalFilters != null)
//                 {
//                     query = query.Where(a => a.depot.Contains(globalFilters) ||
//                              a.liner.Contains(globalFilters) ||
//                              a.redelAuthNo.Contains(globalFilters) ||
//                              a.vesselCarrier.Contains(globalFilters) ||
//                              a.vesselName.Contains(globalFilters));                           
//                 }
//                 if (!string.IsNullOrEmpty(columnName) && !string.IsNullOrEmpty(columnValue))
//                 {
//                     var globalFit = query.Where(a => a.depot.Contains(globalFilters) ||
//                              a.liner.Contains(globalFilters) ||
//                              a.redelAuthNo.Contains(globalFilters) ||
//                              a.vesselCarrier.Contains(globalFilters) ||
//                              a.vesselName.Contains(globalFilters) ||
//                              _context.PreAdvices.Any(b => b.depot.Contains(columnValue))
//                           );
//                 switch (columnName)
//                 {
//                     case "depot":
//                     if(!string.IsNullOrEmpty(globalFilters) ){
//                           query =  globalFit.Where(x => x.depot.Contains(columnValue));   
//                     }
//                     else{
//                          query = from a in _context.PreAdvices
//                          where a.depot.Contains(columnValue)
//                          select a;
//                     } 
//                     break;
//                     case "liner":
//                     if(!string.IsNullOrEmpty(globalFilters) ){
//                           query =  globalFit.Where(x => x.liner.Contains(columnValue));   
//                     }
//                     else{
//                         query = from a in _context.PreAdvices
//                         where a.liner.Contains(columnValue)
//                         select a;
//                     }    
//                     break;
//                     case "redelauthno":
//                     if(!string.IsNullOrEmpty(globalFilters) ){
//                         query =  globalFit.Where(x => x.redelAuthNo.Contains(columnValue));   
//                     }
//                     else{

//                         query = from a in _context.PreAdvices
//                         where a.redelAuthNo.Contains(columnValue)
//                         select a;
//                     }             
//                     break;
//                     case "vesselcarrier":
//                      if(!string.IsNullOrEmpty(globalFilters) ){
//                         query =  globalFit.Where(x => x.vesselCarrier.Contains(columnValue));   
//                     }
//                     else{
//                         query = from a in _context.PreAdvices
//                         where a.vesselCarrier.Contains(columnValue)
//                         select a;
//                     }
//                     break;
//                     case "vesselname":
//                     if(!string.IsNullOrEmpty(globalFilters)){
//                         query =  globalFit.Where(x => x.vesselName.Contains(columnValue));   
//                     }
//                     else{
//                         query = from a in _context.PreAdvices
//                         where a.vesselName.Contains(columnValue)
//                         select a;
//                     }                  
//                     break;
//                     default:
//                     break;
//                 }
//             }
//             var result = query.Skip(first).Take(take).ToList();
//             return Task.FromResult(result);              
//             }
//             catch (Exception ex){
//                 throw ex;
//             }
//         }

// public IEnumerable<PreAdvice> ReceiveAllEvents(EventValues eventValues)
// {
//     try
//     {
//         if (eventValues == null)
//         {
//             throw new ArgumentNullException(nameof(eventValues), "EventValues parameter is null.");
//         }

//         var first = eventValues.first;
//         var take = eventValues.take;
//         var globalFilters = eventValues.globalFilter;
//         var columnName = eventValues.columnName;
//         var columnValue = eventValues.columnValue;

//         IQueryable<PreAdvice> query = _context.PreAdvices;

//         if (globalFilters != "null")
//         {
//             query = query.Where(a => a.depot.Contains(globalFilters) ||
//                                      a.liner.Contains(globalFilters) ||
//                                      a.redelAuthNo.Contains(globalFilters) ||
//                                      a.vesselCarrier.Contains(globalFilters) ||
//                                      a.vesselName.Contains(globalFilters));
//         }

//         if (!string.IsNullOrEmpty(columnName) && !string.IsNullOrEmpty(columnValue))
//         {
//             var globalFit = query.Where(a => a.depot.Contains(globalFilters) ||
//                                              a.liner.Contains(globalFilters) ||
//                                              a.redelAuthNo.Contains(globalFilters) ||
//                                              a.vesselCarrier.Contains(globalFilters) ||
//                                              a.vesselName.Contains(globalFilters) ||
//                                              _context.PreAdvices.Any(b => b.depot.Contains(columnValue))
//                                       );

//             switch (columnName.ToLower())
//             {
//                 case "depot":
//                     query = globalFit.Where(x => x.depot.Contains(columnValue));
//                     break;
//                 case "liner":
//                     query = globalFit.Where(x => x.liner.Contains(columnValue));
//                     break;
//                 case "redelauthno":
//                     query = globalFit.Where(x => x.redelAuthNo.Contains(columnValue));
//                     break;
//                 case "vesselcarrier":
//                     query = globalFit.Where(x => x.vesselCarrier.Contains(columnValue));
//                     break;
//                 case "vesselname":
//                     query = globalFit.Where(x => x.vesselName.Contains(columnValue));
//                     break;
//                 default:
//                     break;
//             }
//         }

//         return query.Skip(first).Take(take).ToList();
//     }
//     catch (Exception ex)
//     {
       
//         throw new Exception("An error occurred while processing the request.", ex);
//     }
// }
