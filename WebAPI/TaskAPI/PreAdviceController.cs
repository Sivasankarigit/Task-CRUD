using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;

using WebAPI.TaskAPI;

namespace WebAPI.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class PreAdiceController : ControllerBase
    {
        private readonly IPreAdvice _itask;

        public PreAdiceController(IPreAdvice itask){    
           _itask=itask;
        }

        [HttpGet]
        [Route("GetPreadivce")]
        public IActionResult GetPreadvice(){
            try
            {
                var result=_itask.GetPreadvice();
                return Ok(result);
            }
            catch(Exception ex)
            {
                return BadRequest(ex.Message);

            }
        }

        [HttpPost]
        [Route("InsertPreadivce")]

         public IActionResult InsertPreadvice(PreAdvice task){
            try
            {
                _itask.InsertPreAdvice(task);
                return Ok(task);
            }
            catch(Exception ex)
            {
                return BadRequest(ex.Message);

            }
        }

        [HttpPut("UpdatePreAdvice")]

        public async Task<IActionResult> UpdatePreAdvice(PreAdvice preAdvice,[FromQuery] int id){
            try{
                var updatedGrade=await _itask.UpdatePreAdvice(id,preAdvice);
                return Ok(updatedGrade);
            }
            catch(Exception ex){
                return BadRequest(ex.Message);
            }
        }

        [HttpGet("GetId/{id}")]

        public async Task<ActionResult<PreAdvice>> GetbyId(int id){
            try{
                var grade = await _itask.getPreadvicebyId(id);
                return Ok(grade);
            }
            catch(InvalidOperationException ex){
                return NotFound(ex.Message);
            } 
        }

        // Lazy load 

        [HttpGet("GetCount")]

        public IActionResult GetCount(){
            try{
                var count = _itask.getTableTotalCount();
                return Ok(count);
            }
            catch(InvalidOperationException ex){
                 return NotFound(ex.Message);

            }
        }

        [HttpGet("GetLimitedRecord")]

          public IActionResult GetLimitedRecord(int skip , int take){
            try{
                var count = _itask.GetLimitedRecord(skip,take);
                return Ok(count);
            }
            catch(InvalidOperationException ex){
                 return NotFound(ex.Message);

            }
        }

        [HttpGet("GlobalSearch")]

          public IActionResult SearchPreAdvices(int skip ,int take,string search){
            try{
                Console.WriteLine(search);
                var count = _itask.SearchPreAdvices(skip,take,search);
                return Ok(count);
            }
            catch(InvalidOperationException ex){
                 return NotFound(ex.Message);
            }
        }

          [HttpGet("ColumnFilter")]

          public IActionResult ColumnFilter(int skip, int take, string columnName, string columnValue){
            try{
               
                var count = _itask.ColumnFilter(skip,take,columnName,columnValue);
                return Ok(count);
            }
            catch(InvalidOperationException ex){
                 return NotFound(ex.Message);
            }
        }

        [HttpDelete("Delete/{id}")]

        public IActionResult DeletePreAdvice(int id){
            try{
                var delete = _itask.DeletePreAdvice(id);
                return Ok(new { message = "Deleted Successfully" });

            }
            catch(InvalidOperationException ex){
                return NotFound(ex.Message);
            }
        }

        // [HttpGet("ReceiveAllEvent")]
        //  public IActionResult ReceiveAllEvents([FromQuery] EventValues eventValues){
        //     try{
        //         var ReceiveAllEvent= _itask.ReceiveAllEvents(eventValues);
        //         return Ok(ReceiveAllEvent);
        //     }
        //     catch(Exception ex){
        //         return BadRequest(ex.Message);
        //     }
        // }


        
    }
}