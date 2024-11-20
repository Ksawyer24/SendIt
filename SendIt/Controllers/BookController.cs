using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using SendIt.Data;
using SendIt.Dto;
using SendIt.Models;
using SendIt.Repo;

namespace SendIt.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    //[Authorize(Roles = "Admin")]
    public class BookController : ControllerBase
    {
        private readonly SendItDbContext sendItDbContext;
        private readonly IBookRepo bookRepo;
        private readonly IMapper mapper;

        public BookController(SendItDbContext sendItDbContext,IBookRepo bookRepo,IMapper mapper)
        {
            this.sendItDbContext = sendItDbContext;
            this.bookRepo = bookRepo;
            this.mapper = mapper;
        }


        [HttpGet("total")]
        public async Task<IActionResult> GetAll()
        {
            var domain = await bookRepo.GetAllBooksAsync();


            var domdto = mapper.Map<List<BookDto>>(domain);


            return Ok(domdto);

        }


        [HttpGet]
        [Route("{id:long}")]

        public async Task<IActionResult> GetById([FromRoute] long id)
        {




            var eco = await bookRepo.GetBookAsync(id);

            if (eco == null)
            {
                return NotFound();
            }

            return Ok(mapper.Map<BookDto>(eco));

        }




        [HttpPost("add")]
        public async Task<IActionResult> AddStaff([FromBody] AddBookDto addBookDto)
        {
            var ecodomainmodel = mapper.Map<Book>(addBookDto);


            ecodomainmodel = await bookRepo.AddAllBooksAsync(ecodomainmodel);


            var ecoDto = mapper.Map<BookDto>(ecodomainmodel);


            return CreatedAtAction(nameof(GetById), new { id = ecoDto.Id }, ecoDto);


        }

    }
}
